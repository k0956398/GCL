using System;

namespace EplusE.Measurement
{
    /// <summary>
    /// MVClass (and maybe MVCode) container class.
    /// MVClass is always set, MVCode *MAY* be set.
    /// </summary>
    public class MVClassOrMVCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MVClassOrMVCode" /> class.
        /// </summary>
        /// <param name="mvClass">The MVClass.</param>
        /// <param name="mvCode">The MVCode (optional, may be null).</param>
        public MVClassOrMVCode(MVClass mvClass, MVCode? mvCode = null)
        {
            this.MVClass = mvClass;
            this.MVCode = mvCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MVClassOrMVCode" /> class.
        /// </summary>
        /// <param name="mvCode">The MVCode.</param>
        public MVClassOrMVCode(MVCode mvCode)
            : this(MVEnumerator.GetClass(mvCode), mvCode)
        {
        }

        /// <summary>
        /// Is the MVCode value set?
        /// </summary>
        /// <value>Is the MVCode value set?</value>
        public bool IsMVCodeSet
        {
            get { return (null != this.MVCode); }
        }

        /// <summary>
        /// Gets the MVClass value.
        /// </summary>
        /// <value>
        /// The MVClass value.
        /// </value>
        public MVClass MVClass { get; private set; }

        /// <summary>
        /// Gets the MVCode value (may be null).
        /// </summary>
        /// <value>
        /// The MVCode value (may be null).
        /// </value>
        public MVCode? MVCode { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the
        /// current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj" /> parameter is null.
        /// </exception>
        /// <returns>
        /// true if the specified <see cref="T:System.Object" /> is equal to the
        /// current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is MVClassOrMVCode)
            {
                var other = (MVClassOrMVCode)obj;
                return Object.Equals(other.MVClass, this.MVClass);
            }
            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + this.MVClass.GetHashCode();
            return hash;
        }
    }
}