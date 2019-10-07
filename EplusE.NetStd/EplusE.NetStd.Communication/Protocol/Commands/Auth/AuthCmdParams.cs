using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class AuthCmdParams : EECmdParamBase
    {
        private byte _Level;

        private byte[] _Secret;

        private byte[] _SecretToChange = null;

        /// <summary>
        /// Constructor to create parameter for user authentication
        /// </summary>
        /// <param name="secret">Current secret ("password")</param>
        /// <param name="authLevel">Authentication level (0 = admin)</param>
        /// <param name="busAddr">The device bus address</param>
        public AuthCmdParams(string secret, byte authLevel) :
            base(0x0)
        {
            _Secret = StringHelper.StringToByteArray(secret);
            _Level = authLevel;
        }

        /// <summary>
        /// Constructor to create parameter for password change
        /// </summary>
        /// <param name="currentSecret">Current secret ("password")</param>
        /// <param name="newSecret">New secret ("password")</param>
        /// <param name="authLevel">Authentication level (0 = admin)</param>
        /// <param name="busAddr">The device bus address</param>
        public AuthCmdParams(string currentSecret, string newSecret, byte authLevel) :
            base(0x0)
        {
            _Secret = StringHelper.StringToByteArray(currentSecret);
            _SecretToChange = StringHelper.StringToByteArray(newSecret);
            _Level = authLevel;
        }

        public bool ChangePassword { get { return _SecretToChange != null && _SecretToChange.Length > 0; } }

        internal void PrepareStep0()
        {
            // First step: authentication request (get challenge)
            //... set bytes
        }

        internal void PrepareStep1(UInt32 challenge, bool changePassword = false)
        {
            //... set bytes
        }

    }
}