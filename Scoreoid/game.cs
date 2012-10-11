
namespace Scoreoid
{
    using System;

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class games
    {
        [System.Xml.Serialization.XmlElementAttribute("game", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public game[] items { get; set; }
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class game
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string short_description { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string game_type { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string levels { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string platform { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string play_url { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string website_url { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string created { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string updated { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string players_count { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string scores_count { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string locked { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string status { get; set; }
    }
}
