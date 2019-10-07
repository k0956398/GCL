using System.Collections.Generic;
using System.Xml.Serialization;

namespace EplusE
{
    /// <summary>
    /// XML serializable dictionary.
    /// <locDE><para />XML-serialisierbares Dictionary.</locDE>
    /// </summary>
    /// <typeparam name="TKey">The type of the key.<locDE><para />Der Typ des Schlüssels.</locDE></typeparam>
    /// <typeparam name="TValue">The type of the value.<locDE><para />Der Typ des Wertes.</locDE></typeparam>
    /// <seealso cref="System.Collections.Generic.Dictionary{TKey, TValue}" />
    /// <seealso cref="System.Xml.Serialization.IXmlSerializable" />
    [XmlRoot("Dictionary")]
    public class XmlSerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        // http://stackoverflow.com/questions/4154325/how-to-serialize-the-class-which-contains-dictionary

        #region IXmlSerializable Members

        /// <summary>
        /// This method is reserved and should not be used. When implementing the
        /// IXmlSerializable interface, you should return null (Nothing in Visual Basic)
        /// from this method, and instead, if specifying a custom schema is required, apply
        /// the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the
        /// class.
        /// <locDE><para />Diese Methode ist reserviert und sollte nicht verwendet werden.
        /// Wenn das IXmlSerializeable Interface implementiert wird, sollte Null zurückgeliefert werden
        /// und stattdessen, falls ein eigenes Schema nötig ist, die Klasse mit dem Attribut
        /// <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> versehen werden.</locDE>
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML
        /// representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" />
        /// method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" />
        /// method.
        /// <locDE><para />Ein <see cref="T:System.Xml.Schema.XmlSchema" />, das die XML-Darstellung dieses Objekts beschreibt und
        /// durch <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> erzeugt sowie durch
        /// <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> konsumiert wird.</locDE>
        /// </returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// <locDE><para />Erzeugt ein Objekt aus einer XML-Repräsentation.</locDE>
        /// </summary>
        /// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> stream from which
        /// the object is deserialized.
        /// <locDE><para />Der <see cref="T:System.Xml.XmlReader" /> Datenstrom aus dem das Objekt deserialisiert wird.</locDE>
        /// </param>
        public void ReadXml(System.Xml.XmlReader xmlReader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = xmlReader.IsEmptyElement;
            if (xmlReader.EOF)
                return;
            xmlReader.Read();
            if (wasEmpty)
                return;

            while (xmlReader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                xmlReader.ReadStartElement("Item");
                xmlReader.ReadStartElement("Key");
                TKey key = (TKey)keySerializer.Deserialize(xmlReader);
                xmlReader.ReadEndElement();
                xmlReader.ReadStartElement("Value");
                TValue value = (TValue)valueSerializer.Deserialize(xmlReader);
                xmlReader.ReadEndElement();
                this.Add(key, value);
                xmlReader.ReadEndElement();
                xmlReader.MoveToContent();
            }
            xmlReader.ReadEndElement();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// <locDE><para />Konvertiert ein Objekt in eine XML-Repräsentation.</locDE>
        /// </summary>
        /// <param name="xmlWriter">The <see cref="T:System.Xml.XmlWriter" /> stream to which
        /// the object is serialized.
        /// <locDE><para />Der <see cref="T:System.Xml.XmlWriter" /> Datenstrom in den das Objekt serialisiert wird.</locDE>
        /// </param>
        public void WriteXml(System.Xml.XmlWriter xmlWriter)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (TKey key in this.Keys)
            {
                xmlWriter.WriteStartElement("Item");
                xmlWriter.WriteStartElement("Key");
                keySerializer.Serialize(xmlWriter, key);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Value");
                TValue value = this[key];
                valueSerializer.Serialize(xmlWriter, value);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
        }

        #endregion IXmlSerializable Members
    }
}