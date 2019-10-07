using System;
using System.Collections.Generic;
using System.Linq;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    internal class DiscoveryCmd : EECmdBase<DiscoveryCmdParams, DiscoveryCmdResult>
    {
        public override IEECommandResult Execute(IEECommandParameter parameter)
        {
            if (parameter is DiscoveryCmdParams diasParams)
            {
                // Multiple transmitters might respond to this command
                ProtocolCallbacks.SendReceiveMulti(parameter, out IList<EE31CmdResponseData> responses);

                // i.e. EE600 (FSM Firmware): DIAS message is answered with NAK/FC => Invalid or wrong parameter
                // 00 00 52 02 15 FC 65
                if (null != responses && responses.Count >= 1)
                {
                    EE31CmdResponseData respData = responses[0];
                    if (EE31Result.NAK == respData.Result &&
                        null != respData.Response && respData.Response.Length >= 1 && respData.Response[0] == 0xFC)
                    {
                        // Retry query with standard 0x52 command (no parameters)
                        ProtocolCallbacks.SendReceiveMulti(parameter, out responses);
                    }
                }

                DiscoveryCmdResult result = new DiscoveryCmdResult();
                result.Code = EECmdResultCode.Success;
                result.Data = new byte[0];  // DiasCmdResult cannot use base class data buffer
                                            // because it needs special result data based on multiple responses (found devices)
                if (null != responses)
                {
                    foreach (EE31CmdResponseData responseData in responses)
                    {
                        if (EE31Result.ACK != responseData.Result)
                            continue;

                        if (IdentifyDevice(responseData, out ushort busAddr, out ushort hwCode, out string modelText, out byte nativeProtocol))
                        {
                            // Append result data
                            result.Devices.Add(new DiscoveryCmdResult.FoundDevice()
                            {
                                BusAddr = busAddr,
                                HwCode = hwCode,
                                ModelText = modelText,
                                NativeProtocol = nativeProtocol,
                            });
                        }
                    }
                }

                return result;
            }
            else
                return CreateResult(EECmdResultCode.InvalidParamClass, null, null);
        }

        private bool IdentifyDevice(EE31CmdResponseData responseData, out ushort busAddr, out ushort hwCode, out string modelText, out byte nativeProtocol)
        {
            nativeProtocol = 0x00;
            hwCode = 0x0;
            modelText = "";
            busAddr = 0;

            if (null == responseData || null == responseData.Response || null == responseData.FoundFrame)
                return false;

            busAddr = (ushort)responseData.RxBusAddr;

            // Check for DIAS response
            IList<int> possibleDiasDelimiters = responseData.Response.FindSequences(new byte[] { 0x00, 0xDE });
            if (possibleDiasDelimiters.Count > 0)
            {
                // Seems to be a DIAS response, check 0xED and CRC16 to be sure
                // <model name> 00 DE {di} np sp ED {crc}
                //   {di} ... discovery id (as set in command)
                //    np  ... native protocol code
                //    sp  ... switch to protocol code (should be 0xB1 for EE31 protocol)
                //  {crc} ... CRC16 over whole EE31 frame (first bus address byte to payload, without {crc}{checksum} obviously)
                int idx00DE = possibleDiasDelimiters[0];
                int idxED = idx00DE + 6;
                int idxCRC = idxED + 1;
                if (responseData.Response.Count() == (idxCRC + 2))
                {
                    UInt16 calcCRC16 = CRC.CRC16_Modbus.GetCRC16(responseData.FoundFrame, responseData.FoundFrame.Count() - 3);
                    UInt16 rxCRC16 = DataTypeConverter.ByteConverter.ToUInt16(responseData.Response, idxCRC);
                    if (0xED == responseData.Response[idxED] && rxCRC16 == calcCRC16)
                    {
                        // Identified as DIAS response
                        nativeProtocol = responseData.Response[idxED - 2];
                        byte switchProtocol = responseData.Response[idxED - 1];
                        if (0xAE == nativeProtocol || 0xAE == switchProtocol)
                        {
                            // Device is in Bootloader mode, needs firmware update!
                            if ('#' == responseData.Response[0])
                            {
                                // Model name holds '#' + HW code
                                int lengthModel = idx00DE;
                                if (3 == lengthModel)
                                {
                                    // HW code was transmitted as '#' + WORD value
                                    hwCode = DataTypeConverter.ByteConverter.ToUInt16(responseData.Response, 1);
                                }
                                if (5 == lengthModel)
                                {
                                    // HW code was transmitted as '#' + 4 ASCII chars (WORD in ASCII-Hex notation)
                                    string hex = "0x" + StringHelper.ExtractStringContent(responseData.Response, 1, 4);
                                    hwCode = DataTypeConverter.UInt16Converter.Parse(hex);
                                }
                            }
                            // Device in Bootloader mode cannot be used...
                            return false;
                        }
                    }
                }
            }

            // 30.03.2012: Update for extended answer payload from bus protocol devices (EE071, etc.)
            if ((responseData.Response.Length >= 1 && 0x06 == responseData.Response[0]) ||
                (5 == responseData.Response.Length && 4 == responseData.Response[0]))
                // Workarounds:
                // Some products (i.e. early EE1900) send ACK byte (0x06) twice
                // Some products (i.e. EE75) report another length byte (4) in front of payload data
                modelText = StringHelper.ExtractStringContent(responseData.Response, 1, responseData.Response.Length - 1);
            else
                modelText = StringHelper.ExtractStringContent(responseData.Response);

            return true;
        }
    }
}