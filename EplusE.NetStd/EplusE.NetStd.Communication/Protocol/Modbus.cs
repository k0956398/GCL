using System;

namespace EplusE.NetStd.Communication.Protocol
{
    /// <summary>
    /// Simple Modbus helper to decode/encode modbus protocol frames
    /// </summary>
    public class ModbusHelper
    {
        private readonly byte _slaveAddr;

        public ModbusHelper(byte slaveAddr)
        {
            _slaveAddr = slaveAddr;
        }

        public byte[] CreateRequest_ReadHoldingRegisters(ushort regStartAddr, ushort nrRegToRead)
        {
            byte[] frame = new byte[1 + 1 + 2 + 2 + 2];

            frame[0] = _slaveAddr;  // Slave address
            frame[1] = 0x03;        // Function code: read holding register

            // Data (start + nr)
            frame[2] = (byte)(regStartAddr >> 8);
            frame[3] = (byte)(regStartAddr);
            frame[4] = (byte)(nrRegToRead >> 8);
            frame[5] = (byte)(nrRegToRead);

            // CRC
            DataTypeConverter.ByteConverter.CopyToArray(CRC.CRC16_Modbus.GetCRC16(frame, frame.Length - 2), frame, 6);

            return frame;
        }

        public byte[] CreateResponse_ReadHoldingRegisters(byte[] registerData)
        {
            byte[] frame = new byte[1 + 1 + 1 + registerData.Length + 2];

            frame[0] = _slaveAddr;                  // Slave address
            frame[1] = 0x03;                        // Function code: read holding register
            frame[2] = (byte)registerData.Length;   // Byte count

            // Write register data
            int nrReg = registerData.Length / 2;
            for (int r = 0; r < nrReg; r++)
            {
                ushort regData = DataTypeConverter.ByteConverter.ToUInt16(registerData, (2 * r));
                DataTypeConverter.ByteConverter.CopyToArray(regData, frame, 3 + (2 * r));
            }

            // CRC
            DataTypeConverter.ByteConverter.CopyToArray(CRC.CRC16_Modbus.GetCRC16(frame, frame.Length - 2), frame, frame.Length - 2);

            return frame;
        }

        public ushort[] ReadResponse_ReadHoldingRegisters(byte[] frame)
        {
            if (frame == null)
                throw new ArgumentNullException("frame");
            if (frame.Length < 5)
                throw new ArgumentOutOfRangeException("frame", "Response length cannot be smaller than 5 bytes");

            // Check slave address
            if (frame[0] != _slaveAddr)
                return null;

            // Check function code (read holding register)
            if (frame[1] != 0x03)
                return null;

            // Check CRC
            if (CRC.CRC16_Modbus.GetCRC16(frame, frame.Length - 2) != DataTypeConverter.ByteConverter.ToUInt16(frame, frame.Length - 2))
                return null;

            // Get register data
            int nrReg = frame[2] / 2;
            ushort[] data = new ushort[nrReg];

            for (int r = 0; r < nrReg; r++)
                data[r] = DataTypeConverter.ByteConverter.ToUInt16(frame, 3 + (2 * r));

            return data;
        }

        public byte[] ReadResponse_ReadHoldingRegistersAsBytes(byte[] frame)
        {
            ushort[] data = ReadResponse_ReadHoldingRegisters(frame);

            if (data == null)
                return null;

            byte[] dataBytes = new byte[data.Length * 2];
            int idx = 0;

            foreach (ushort w in data)
            {
                DataTypeConverter.ByteConverter.CopyToArray(w, dataBytes, idx);
                idx += 2;
            }

            return dataBytes;
        }
    }
}