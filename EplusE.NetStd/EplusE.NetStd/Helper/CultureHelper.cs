namespace EplusE
{
    /// <summary>
    /// Culture helper class.
    /// <locDE><para />Kultur Hilfsklasse.</locDE>
    /// </summary>
    public static class CultureHelper
    {
        #region InvariantCulture

        private static System.Globalization.CultureInfo _InvariantCulture = null;

        /// <summary>
        /// Gets or sets the invariant culture.
        /// <locDE><para />Holt/setzt die fixierte englische Kultureinstellung.</locDE>
        /// </summary>
        /// <value>The invariant culture.<locDE><para />Die fixierte englische Kultureinstellung.</locDE></value>
        public static System.Globalization.CultureInfo InvariantCulture
        {
            get
            {
                if (null == _InvariantCulture)
                    _InvariantCulture = System.Globalization.CultureInfo.InvariantCulture;
                return _InvariantCulture;
            }

            set
            {
                _InvariantCulture = value;
            }
        }

        #endregion InvariantCulture

        #region GermanCulture

        private static System.Globalization.CultureInfo _GermanCulture = null;

        /// <summary>
        /// Gets or sets the german culture.
        /// <locDE><para />Holt/setzt die deutsche Kultureinstellung.</locDE>
        /// </summary>
        /// <value>The german culture.<locDE><para />Die deutsche Kultureinstellung.</locDE></value>
        public static System.Globalization.CultureInfo GermanCulture
        {
            get
            {
                if (null == _GermanCulture)
                    _GermanCulture = new System.Globalization.CultureInfo("de-DE");
                return _GermanCulture;
            }

            set
            {
                _GermanCulture = value;
            }
        }

        #endregion GermanCulture
    }
}