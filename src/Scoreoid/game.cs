using System.Xml.Schema;
using System.Xml.Serialization;

namespace Scoreoid
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class games
    {
        [XmlElement("game", Form = XmlSchemaForm.Unqualified)]
        public game[] items { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class game
    {
        [XmlAttribute]
        public string name { get; set; }

        [XmlAttribute]
        public string short_description { get; set; }

        [XmlAttribute]
        public string description { get; set; }

        [XmlAttribute]
        public string game_type { get; set; }

        [XmlAttribute]
        public string version { get; set; }

        [XmlAttribute]
        public string levels { get; set; }

        [XmlAttribute]
        public string platform { get; set; }

        [XmlAttribute]
        public string play_url { get; set; }

        [XmlAttribute]
        public string website_url { get; set; }

        [XmlAttribute]
        public string created { get; set; }

        [XmlAttribute]
        public string updated { get; set; }

        [XmlAttribute]
        public string players_count { get; set; }

        [XmlAttribute]
        public string scores_count { get; set; }

        [XmlAttribute]
        public string locked { get; set; }

        [XmlAttribute]
        public string status { get; set; }
    }
}