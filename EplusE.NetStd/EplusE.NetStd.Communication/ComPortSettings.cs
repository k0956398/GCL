using System;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// ComPortSettings data class.
    /// </summary>
    public class ComPortSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComPortSettings"/> class.
        /// </summary>
        public ComPortSettings()
        {
            SetDefaults();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComPortSettings"/> class.
        /// </summary>
        /// <param name="other">The other.</param>
        public ComPortSettings(ComPortSettings other)
        {
            CopyFrom(other);
        }

        /// <summary>
        /// Gets or sets the baudrate.
        /// </summary>
        /// <value>The baudrate.</value>
        public int Baudrate { get; set; }

        /// <summary>
        /// Gets or sets the databits.
        /// </summary>
        /// <value>The databits.</value>
        public int Databits { get; set; }

        /// <summary>
        /// Gets or sets the handshake.
        /// </summary>
        /// <value>The handshake.</value>
        public System.IO.Ports.Handshake Handshake { get; set; }

        /// <summary>
        /// Gets or sets the parity.
        /// </summary>
        /// <value>The parity.</value>
        public System.IO.Ports.Parity Parity { get; set; }

        /// <summary>
        /// Gets or sets the RX timeout [msec].
        /// </summary>
        /// <value>The RX timeout [msec].</value>
        public int RxTimeoutMSec { get; set; }

        /// <summary>
        /// Gets or sets the stopbits.
        /// </summary>
        /// <value>The stopbits.</value>
        public System.IO.Ports.StopBits Stopbits { get; set; }

        /// <summary>
        /// Gets or sets the TX timeout [msec].
        /// </summary>
        /// <value>The TX timeout [msec].</value>
        public int TxTimeoutMSec { get; set; }

        /// <summary>
        /// Applies settings from string, i.e. "9600,8,N,1" or "9600 8N1" or "9600_8N1".
        /// </summary>
        /// <param name="settings">The settings to apply, i.e. "9600,8,N,1" or "9600 8N1" or "9600_8N1".</param>
        public static ComPortSettings FromString(string settings)
        {
            // "9600,8,N,1" --> "9600,8,N,1" (no change) "9600 8N1" --> "9600,8N1" "9600_8N1" --> "9600,8N1"
            string work = settings.Replace(' ', ',').Replace('_', ',');
            if (string.IsNullOrEmpty(work) || !work.Contains(","))
                return null;

            // "9600,8,N,1" --> "9600" "9600,8N1" --> "9600"
            string baudRate = work.Split(',')[0];
            // "9600,8,N,1" --> "8N1" "9600 8N1" --> "8N1"
            work = work.Substring(baudRate.Length).Replace(",", "");
            if (3 != work.Length)
                return null;

            try
            {
                ComPortSettings cps = new ComPortSettings();
                cps.Baudrate = Convert.ToInt32(baudRate);
                switch (work[0])
                {
                    case '8': cps.Databits = 8; break;
                    case '7': cps.Databits = 7; break;
                    default: return null;
                }
                switch (work[1])
                {
                    case 'N': cps.Parity = System.IO.Ports.Parity.None; break;
                    case 'E': cps.Parity = System.IO.Ports.Parity.Even; break;
                    case 'O': cps.Parity = System.IO.Ports.Parity.Odd; break;
                    //case 'M': cps.Parity = Parity.Mark; break;
                    //case 'S': cps.Parity = Parity.Space; break;
                    default: return null;
                }
                switch (work[2])
                {
                    case 'N': cps.Stopbits = System.IO.Ports.StopBits.None; break;
                    case '1': cps.Stopbits = System.IO.Ports.StopBits.One; break;
                    //case '5': cps.Stopbits = StopBits.OnePointFive; break;
                    case '2': cps.Stopbits = System.IO.Ports.StopBits.Two; break;
                    default: return null;
                }
                return cps;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Is not equal operator.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Boolean operator !=(ComPortSettings a, ComPortSettings b)
        {
            return !Object.Equals(a, b);
        }

        /// <summary>
        /// Is equal operator.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Boolean operator ==(ComPortSettings a, ComPortSettings b)
        {
            return Object.Equals(a, b);
        }

        /// <summary>
        /// Copies from other <see cref="ComPortSettings"/> object.
        /// </summary>
        /// <param name="other">The other (source) <see cref="ComPortSettings"/> object.</param>
        public void CopyFrom(ComPortSettings other)
        {
            this.Baudrate = other.Baudrate;
            this.Databits = other.Databits;
            this.Stopbits = other.Stopbits;
            this.Parity = other.Parity;
            this.Handshake = other.Handshake;
            this.TxTimeoutMSec = other.TxTimeoutMSec;
            this.RxTimeoutMSec = other.RxTimeoutMSec;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current
        /// <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.
        /// </param>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see
        /// cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        public override bool Equals(Object obj)
        {
            if (obj is ComPortSettings)
            {
                ComPortSettings other = (ComPortSettings)obj;
                return (this.Baudrate.Equals(other.Baudrate) &&
                    this.Databits.Equals(other.Databits) &&
                    this.Stopbits.Equals(other.Stopbits) &&
                    this.Parity.Equals(other.Parity) &&
                    this.Handshake.Equals(other.Handshake) &&
                    this.TxTimeoutMSec.Equals(other.TxTimeoutMSec) &&
                    this.RxTimeoutMSec.Equals(other.RxTimeoutMSec));
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures
        /// like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Baudrate.GetHashCode();
            hash = (hash * 7) + Databits.GetHashCode();
            hash = (hash * 7) + Stopbits.GetHashCode();
            hash = (hash * 7) + Parity.GetHashCode();
            hash = (hash * 7) + Handshake.GetHashCode();
            hash = (hash * 7) + TxTimeoutMSec.GetHashCode();
            hash = (hash * 7) + RxTimeoutMSec.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Sets the default settings.
        /// </summary>
        public void SetDefaults()
        {
            Baudrate = 9600;
            Databits = 8;
            Stopbits = System.IO.Ports.StopBits.One;
            Parity = System.IO.Ports.Parity.None;
            Handshake = System.IO.Ports.Handshake.None;
            TxTimeoutMSec = System.IO.Ports.SerialPort.InfiniteTimeout;
            RxTimeoutMSec = System.IO.Ports.SerialPort.InfiniteTimeout;
            //TxTimeoutMSec = 2000;
            //RxTimeoutMSec = 2000;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string stopbits = "";
            switch (Stopbits)
            {
                //case System.IO.Ports.StopBits.None: stopbits = "0"; break; <-- not supported
                case System.IO.Ports.StopBits.One: stopbits = "1"; break;
                case System.IO.Ports.StopBits.OnePointFive: stopbits = "1.5"; break;
                case System.IO.Ports.StopBits.Two: stopbits = "2"; break;
            }
            return Baudrate + " " + Databits + Parity.ToString().Substring(0, 1) + stopbits;
        }
    }
}