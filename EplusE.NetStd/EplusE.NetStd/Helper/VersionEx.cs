using System;
using System.Xml.Serialization;

namespace EplusE
{
    /// <summary>
    /// Serializable clone of the <see cref="System.Version" /> class.
    /// <locDE><para />Serialisierbarer Klon der Klasse <see cref="System.Version" />.</locDE>
    /// </summary>
    [Serializable]
    public class VersionEx : ICloneable, IComparable
    {
        // https://stackoverflow.com/questions/2085866/system-version-not-serialized

        #region Internal members

        private int _Build;
        private int _Major;
        private int _Minor;
        private int _Revision;

        #endregion Internal members

        #region Major

        /// <summary>
        /// Gets the major version number (1st number).
        /// <locDE><para />Holt die Hauptversionsnummer (erste Nummer).</locDE>
        /// </summary>
        [XmlAttribute]
        public int Major
        {
            get
            {
                return _Major;
            }
            set
            {
                _Major = value;
            }
        }

        #endregion Major

        #region Minor

        /// <summary>
        /// Gets the minor version number (2nd number).
        /// <locDE><para />Holt die Nebenversionsnummer (zweite Nummer).</locDE>
        /// </summary>
        [XmlAttribute]
        public int Minor
        {
            get
            {
                return _Minor;
            }
            set
            {
                _Minor = value;
            }
        }

        #endregion Minor

        #region Build

        /// <summary>
        /// Gets the build number (3rd number).
        /// <locDE><para />Holt die Buildnummer (dritte Nummer).</locDE>
        /// </summary>
        [XmlAttribute]
        public int Build
        {
            get
            {
                return _Build;
            }
            set
            {
                _Build = value;
            }
        }

        #endregion Build

        #region Revision

        /// <summary>
        /// Gets the Revision number (4th number).
        /// <locDE><para />Holt die Revisionsnummer (vierte Nummer).</locDE>
        /// </summary>
        [XmlAttribute]
        public int Revision
        {
            get
            {
                return _Revision;
            }
            set
            {
                _Revision = value;
            }
        }

        #endregion Revision

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="VersionEx" /> instance.
        /// <locDE><para />Erzeugt eine neue <see cref="VersionEx" /> Instanz.</locDE>
        /// </summary>
        public VersionEx()
        {
            _Major = 0;
            _Minor = 0;
            _Build = -1;
            _Revision = -1;
        }

        /// <summary>
        /// Creates a new <see cref="VersionEx" /> instance.
        /// <locDE><para />Erzeugt eine neue <see cref="VersionEx" /> Instanz.</locDE>
        /// </summary>
        /// <param name="version">The version, i.e. "1.0".<locDE><para />Die Version, z.B. "1.0".</locDE></param>
        public VersionEx(string version)
        {
            int build = -1;
            int revision = -1;
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }
            char[] delimiters = new char[1] { '.' };
            string[] parts = version.Split(delimiters);
            int partCount = parts.Length;
            if ((partCount < 2) || (partCount > 4))
            {
                throw new ArgumentException("Arg_VersionString");
            }

            int major = parts[0].Trim(new char[] { 'V', 'v', ' ', '\t' }).ToInt32(0);
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("version", "ArgumentOutOfRange_Version");
            }

            int minor = parts[1].Trim().ToInt32(0);
            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("version", "ArgumentOutOfRange_Version");
            }
            partCount -= 2;
            if (partCount > 0)
            {
                build = parts[2].Trim().ToInt32(0);
                if (build < 0)
                {
                    throw new ArgumentOutOfRangeException("build", "ArgumentOutOfRange_Version");
                }
                partCount--;
                if (partCount > 0)
                {
                    revision = parts[3].Trim().ToInt32(0);
                    if (revision < 0)
                    {
                        throw new ArgumentOutOfRangeException("revision", "ArgumentOutOfRange_Version");
                    }
                }
            }

            _Major = major;
            _Minor = minor;
            _Build = build;
            _Revision = revision;
        }

        /// <summary>
        /// Creates a new <see cref="VersionEx" /> instance.
        /// <locDE><para />Erzeugt eine neue <see cref="VersionEx" /> Instanz.</locDE>
        /// </summary>
        /// <param name="version">The version.<locDE><para />Die Version.</locDE></param>
        public VersionEx(System.Version version)
        {
            _Major = version.Major;
            _Minor = version.Minor;
            _Build = version.Build;
            _Revision = version.Revision;
        }

        /// <summary>
        /// Creates a new <see cref="VersionEx" /> instance.
        /// <locDE><para />Erzeugt eine neue <see cref="VersionEx" /> Instanz.</locDE>
        /// </summary>
        /// <param name="major">The major version number.<locDE><para />Die Hauptversionsnummer.</locDE></param>
        /// <param name="minor">The minor version number.<locDE><para />Die Nebenversionsnummer.</locDE></param>
        public VersionEx(int major, int minor)
        {
            int build = -1;
            int revision = -1;
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major", "ArgumentOutOfRange_Version");
            }
            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor", "ArgumentOutOfRange_Version");
            }

            _Major = major;
            _Minor = minor;
            _Build = build;
            _Revision = revision;
        }

        /// <summary>
        /// Creates a new <see cref="VersionEx" /> instance.
        /// <locDE><para />Erzeugt eine neue <see cref="VersionEx" /> Instanz.</locDE>
        /// </summary>
        /// <param name="major">The major version number.<locDE><para />Die Hauptversionsnummer.</locDE></param>
        /// <param name="minor">The minor version number.<locDE><para />Die Nebenversionsnummer.</locDE></param>
        /// <param name="build">The build number.<locDE><para />Die Buildnummer.</locDE></param>
        public VersionEx(int major, int minor, int build)
        {
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major", "ArgumentOutOfRange_Version");
            }
            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor", "ArgumentOutOfRange_Version");
            }
            if (build < 0)
            {
                throw new ArgumentOutOfRangeException("build", "ArgumentOutOfRange_Version");
            }

            _Major = major;
            _Minor = minor;
            _Build = build;
            _Revision = -1;
        }

        /// <summary>
        /// Creates a new <see cref="VersionEx" /> instance.
        /// <locDE><para />Erzeugt eine neue <see cref="VersionEx" /> Instanz.</locDE>
        /// </summary>
        /// <param name="major">The major version number.<locDE><para />Die Hauptversionsnummer.</locDE></param>
        /// <param name="minor">The minor version number.<locDE><para />Die Nebenversionsnummer.</locDE></param>
        /// <param name="build">The build number.<locDE><para />Die Buildnummer.</locDE></param>
        /// <param name="revision">The revision number.<locDE><para />Die Revisionsnummer.</locDE></param>
        public VersionEx(int major, int minor, int build, int revision)
        {
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major", "ArgumentOutOfRange_Version");
            }
            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor", "ArgumentOutOfRange_Version");
            }
            if (build < 0)
            {
                throw new ArgumentOutOfRangeException("build", "ArgumentOutOfRange_Version");
            }
            if (revision < 0)
            {
                throw new ArgumentOutOfRangeException("revision", "ArgumentOutOfRange_Version");
            }

            _Major = major;
            _Minor = minor;
            _Build = build;
            _Revision = revision;
        }

        #endregion Constructors

        #region ICloneable Members

        /// <summary>
        /// Clones this instance.
        /// <locDE><para />Klont diese Instanz.</locDE>
        /// </summary>
        /// <returns>Cloned instance.<locDE><para />Geklonte Instanz.</locDE></returns>
        public object Clone()
        {
            VersionEx version1 = new VersionEx();
            version1.Major = Major;
            version1.Minor = Minor;
            version1.Build = Build;
            version1.Revision = Revision;
            return version1;
        }

        #endregion ICloneable Members

        #region IComparable Members

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// <locDE><para />Vergleicht diese Instanz mit dem angegebenen Objekt selben Typs und liefert einen Integer der angibt, ob diese Instanz kleiner, größer oder gleich ist.</locDE>
        /// </summary>
        /// <param name="obj">An object to compare with this instance.<locDE><para />Ein Objekt, das mit dieser Instanz verglichen werden soll.</locDE></param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared:
        /// &lt; 0: This instance is less than <paramref name="obj" />; 0: This instance is equal to <paramref name="obj" />; &gt; 0: This instance is greater than <paramref name="obj" />.
        /// <locDE><para />Wert, der die relative Reihenfolge beschreibt:
        /// &lt; 0: Diese Instanz ist kleiner als <paramref name="obj" />; 0: Diese Instanz ist gleich <paramref name="obj" />; &gt; 0: Diese Instanz ist größer als <paramref name="obj" />.</locDE>
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (!(obj is VersionEx))
            {
                throw new ArgumentException("Arg_MustBeVersion");
            }
            VersionEx version = (VersionEx)obj;
            if (Major != version.Major)
            {
                if (Major > version.Major)
                {
                    return 1;
                }
                return -1;
            }
            if (Minor != version.Minor)
            {
                if (Minor > version.Minor)
                {
                    return 1;
                }
                return -1;
            }
            if (Build != version.Build)
            {
                if (Build > version.Build)
                {
                    return 1;
                }
                return -1;
            }
            if (Revision == version.Revision)
            {
                return 0;
            }
            if (Revision > version.Revision)
            {
                return 1;
            }
            return -1;
        }

        #endregion IComparable Members

        #region Equal, comparison operators, etc.

        /// <summary>
        /// Operator !=.<locDE><para />Operator !=.</locDE>
        /// </summary>
        /// <param name="v1">The version object #1.<locDE><para />Das Versionsobjekt #1.</locDE></param>
        /// <param name="v2">The version object #2.<locDE><para />Das Versionsobjekt #2.</locDE></param>
        /// <returns>True if not equal.<locDE><para />True wenn ungleich.</locDE></returns>
        public static bool operator !=(VersionEx v1, VersionEx v2)
        {
            return (v1 != v2);
        }

        /// <summary>
        /// Operator &lt;.<locDE><para />Operator &lt;.</locDE>
        /// </summary>
        /// <param name="v1">The version object #1.<locDE><para />Das Versionsobjekt #1.</locDE></param>
        /// <param name="v2">The version object #2.<locDE><para />Das Versionsobjekt #2.</locDE></param>
        /// <returns>True if lower.<locDE><para />True wenn kleiner.</locDE></returns>
        public static bool operator <(VersionEx v1, VersionEx v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException("v1");
            }
            return (v1.CompareTo(v2) < 0);
        }

        /// <summary>
        /// Operator &lt;=.<locDE><para />Operator &lt;=.</locDE>
        /// </summary>
        /// <param name="v1">The version object #1.<locDE><para />Das Versionsobjekt #1.</locDE></param>
        /// <param name="v2">The version object #2.<locDE><para />Das Versionsobjekt #2.</locDE></param>
        /// <returns>True if lower or equal.<locDE><para />True wenn kleiner oder gleich.</locDE></returns>
        public static bool operator <=(VersionEx v1, VersionEx v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException("v1");
            }
            return (v1.CompareTo(v2) <= 0);
        }

        /// <summary>
        /// Operator ==.<locDE><para />Operator ==.</locDE>
        /// </summary>
        /// <param name="v1">The version object #1.<locDE><para />Das Versionsobjekt #1.</locDE></param>
        /// <param name="v2">The version object #2.<locDE><para />Das Versionsobjekt #2.</locDE></param>
        /// <returns>True if equal.<locDE><para />True wenn gleich.</locDE></returns>
        public static bool operator ==(VersionEx v1, VersionEx v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// Operator &gt;.<locDE><para />Operator &gt;.</locDE>
        /// </summary>
        /// <param name="v1">The version object #1.<locDE><para />Das Versionsobjekt #1.</locDE></param>
        /// <param name="v2">The version object #2.<locDE><para />Das Versionsobjekt #2.</locDE></param>
        /// <returns>True if greater.<locDE><para />True wenn größer.</locDE></returns>
        public static bool operator >(VersionEx v1, VersionEx v2)
        {
            return (v2 < v1);
        }

        /// <summary>
        /// Operator &gt;=.<locDE><para />Operator &gt;=.</locDE>
        /// </summary>
        /// <param name="v1">The version object #1.<locDE><para />Das Versionsobjekt #1.</locDE></param>
        /// <param name="v2">The version object #2.<locDE><para />Das Versionsobjekt #2.</locDE></param>
        /// <returns>True if greater or equal.<locDE><para />True wenn größer oder gleich.</locDE></returns>
        public static bool operator >=(VersionEx v1, VersionEx v2)
        {
            return (v2 <= v1);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// <locDE><para />Ermittelt, ob das angegebene Objekt gleich dieser Instanz ist.</locDE>
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.
        /// <locDE><para />Das zu vergleichende Objekt.</locDE></param>
        /// <returns>
        /// True if the specified <see cref="System.Object" /> is equal to this instance; otherwise, false.
        /// <locDE><para />True, wenn das angegebene Objekt gleich dieser Instanz ist; sonst false.</locDE>
        /// </returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is VersionEx))
            {
                return false;
            }
            VersionEx version1 = (VersionEx)obj;
            if (((Major == version1.Major) && (Minor == version1.Minor)) && (Build == version1.Build) && (Revision == version1.Revision))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the hash code.
        /// <locDE><para />Holt den Hashwert.</locDE>
        /// </summary>
        /// <returns>The hash code.<locDE><para />Der Hashwert.</locDE></returns>
        public override int GetHashCode()
        {
            int num1 = 0;
            num1 |= ((Major & 15) << 0x1c);
            num1 |= ((Minor & 0xff) << 20);
            num1 |= ((Build & 0xff) << 12);
            return (num1 | Revision & 0xfff);
        }

        #endregion Equal, comparison operators, etc.

        #region ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// <locDE><para />Liefert einen <see cref="System.String" />, der diese Instanz repräsentiert.</locDE>
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// <locDE><para />Ein <see cref="System.String" />, der diese Instanz repräsentiert.</locDE>
        /// </returns>
        public override string ToString()
        {
            if (Build == -1)
            {
                return this.ToString(2);
            }
            if (Revision == -1)
            {
                return this.ToString(3);
            }
            return this.ToString(4);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// <locDE><para />Liefert einen <see cref="System.String" />, der diese Instanz repräsentiert.</locDE>
        /// </summary>
        /// <param name="fieldCount">The field count.<locDE><para />Die Feldanzahl.</locDE></param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// <locDE><para />Ein <see cref="System.String" />, der diese Instanz repräsentiert.</locDE>
        /// </returns>
        public string ToString(int fieldCount)
        {
            object[] objArray1;
            switch (fieldCount)
            {
                case 0:
                    {
                        return string.Empty;
                    }
                case 1:
                    {
                        return (Major.ToString());
                    }
                case 2:
                    {
                        return (Major.ToString() + "." + Minor.ToString());
                    }
            }
            if (Build == -1)
            {
                throw new ArgumentException(string.Format("ArgumentOutOfRange_Bounds_Lower_Upper {0},{1}", "0", "2"), "fieldCount");
            }
            if (fieldCount == 3)
            {
                objArray1 = new object[5] { Major, ".", Minor, ".", Build };
                return string.Concat(objArray1);
            }
            if (Revision == -1)
            {
                throw new ArgumentException(string.Format("ArgumentOutOfRange_Bounds_Lower_Upper {0},{1}", "0", "3"), "fieldCount");
            }
            if (fieldCount == 4)
            {
                objArray1 = new object[7] { Major, ".", Minor, ".", Build, ".", Revision };
                return string.Concat(objArray1);
            }
            throw new ArgumentException(string.Format("ArgumentOutOfRange_Bounds_Lower_Upper {0},{1}", "0", "4"), "fieldCount");
        }

        #endregion ToString

        #region ToVersion

        /// <summary>
        /// Converts to <see cref="System.Version" /> object.
        /// <locDE><para />Konvertiert zu <see cref="System.Version" /> Objekt.</locDE>
        /// </summary>
        /// <returns>A <see cref="System.Version" /> object.
        /// <locDE><para />Ein <see cref="System.Version" /> Objekt.</locDE>
        /// </returns>
        public System.Version ToVersion()
        {
            return new System.Version(Major, Minor, Build, Revision);
        }

        #endregion ToVersion
    }
}