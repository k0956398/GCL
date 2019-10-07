namespace EplusE.NetStd.Communication
{
    public enum InterfaceType : byte
    {
        /// <summary>
        /// Emulation interface without real hardware connection.
        /// </summary>
        Emulation,

        /// <summary>
        /// Bluetooth Low Energy communication interface.
        /// </summary>
        BLE,

        /// <summary>
        /// UART communication interface.
        /// </summary>
        UART
    }
}