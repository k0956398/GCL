using System;
using System.Collections.Generic;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetHardwareConfCmdResult : EECmdResultBase
    {
        public IEnumerable<HardwareComponent> ComponentList { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            if (Data.Length < 1)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                var compList = new List<HardwareComponent>();

                // Create command specific result from data
                int dataIdx = 0;
                int nrDescriptors = Data[dataIdx++];

                for (int i = 0; i < nrDescriptors; i++)
                {
                    int len = Data[dataIdx++];

                    var descriptor = new HardwareComponent();
                    descriptor.Component = Data[dataIdx++];
                    descriptor.Category = Data[dataIdx++];
                    descriptor.Type = Data[dataIdx++];

                    // Any attributes?
                    int attribLen = len - 3;
                    if (attribLen > 0)
                    {
                        descriptor.Attributes = new byte[len - 3];
                        Array.Copy(Data, dataIdx, descriptor.Attributes, 0, descriptor.Attributes.Length);
                        dataIdx += descriptor.Attributes.Length;
                    }
                    else
                        descriptor.Attributes = new byte[0];

                    // Any text?
                    int textLen = Data[dataIdx++];
                    if (textLen > 0)
                    {
                        descriptor.Text = StringHelper.ExtractStringContent(Data, dataIdx, textLen);
                        dataIdx += textLen;
                    }

                    compList.Add(descriptor);
                }

                ComponentList = compList;
            }
        }
    }
}