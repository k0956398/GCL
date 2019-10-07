namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetPortSettingsCmdResult : EECmdResultBase
    {
        public ComPortSettings ComSettings { get; private set; }

        public byte? ModuleState { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 1)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                if (Data.Length > 1)
                    ModuleState = Data[1];

                ComPortSettings cps = new ComPortSettings();

                // Decode settings
                byte cpsCoded = Data[0];

                // Bits 0..2: Baudrate
                //   Bit 7 = 0: (000 = 4k8, 001 = 9k6, 010 = 19k2, 011 = 38k4, 100 = 57k6, 101 = 76k8, 110 = 115k2)
                //   Bit 7 = 1: (000 = 300, 001 = 600, 010 = 1k2,  011 = 2k4)
                // Bits 3..4: Parität (01 = None, 10 = Odd, 11 = Even)
                // Bit 5: Stoppbits (0 = 1 Stoppbit, 1 = 2 Stoppbits)
                // Bit 6: Datenbits (0 = 8 Datenbits, 1 = 7 Datenbits)
                if (0x80 == (cpsCoded & 0x80))
                {
                    // Bit 7 is 1
                    switch (cpsCoded & 0x07)
                    {
                        case 0: cps.Baudrate = 300; break;
                        case 1: cps.Baudrate = 600; break;
                        case 2: cps.Baudrate = 1200; break;
                        case 3: cps.Baudrate = 2400; break;
                        default: Code = EECmdResultCode.InvalidResult; break;
                    }
                }
                else
                {
                    // Bit 7 is 0
                    switch (cpsCoded & 0x07)
                    {
                        case 0: cps.Baudrate = 4800; break;
                        case 1: cps.Baudrate = 9600; break;
                        case 2: cps.Baudrate = 19200; break;
                        case 3: cps.Baudrate = 38400; break;
                        case 4: cps.Baudrate = 57600; break;
                        case 5: cps.Baudrate = 76800; break;
                        case 6: cps.Baudrate = 115200; break;
                        default: Code = EECmdResultCode.InvalidResult; break;
                    }
                }
                switch (cpsCoded & 0x18)
                {
                    case (1 * 8): cps.Parity = System.IO.Ports.Parity.None; break;
                    case (2 * 8): cps.Parity = System.IO.Ports.Parity.Odd; break;
                    case (3 * 8): cps.Parity = System.IO.Ports.Parity.Even; break;
                    default: Code = EECmdResultCode.InvalidResult; break;
                }
                switch (cpsCoded & 0x20)
                {
                    case (0 * 32): cps.Stopbits = System.IO.Ports.StopBits.One; break;
                    case (1 * 32): cps.Stopbits = System.IO.Ports.StopBits.Two; break;
                    default: Code = EECmdResultCode.InvalidResult; break;
                }
                switch (cpsCoded & 0x40)
                {
                    case (0 * 64): cps.Databits = 8; break;
                    case (1 * 64): cps.Databits = 7; break;
                    default: Code = EECmdResultCode.InvalidResult; break;
                }

                if (Code == EECmdResultCode.Success)
                    ComSettings = cps;
            }
        }
    }
}