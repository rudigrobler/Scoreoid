using System.Xml.Schema;
using System.Xml.Serialization;

namespace Scoreoid
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class scores
    {
        [XmlElement("player", Form = XmlSchemaForm.Unqualified)]
        public player[] items { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class score
    {
        [XmlAttribute]
        public string created { get; set; }

        [XmlAttribute]
        public string difficulty { get; set; }

        [XmlAttribute]
        public string platform { get; set; }

        [XmlAttribute("score")]
        public string value { get; set; }
    }
}