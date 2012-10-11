using System.Xml.Schema;
using System.Xml.Serialization;

namespace Scoreoid
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class players
    {
        [XmlElement("player", Form = XmlSchemaForm.Unqualified)]
        public player[] items { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class player
    {
        [XmlAttribute]
        public string username { get; set; }

        [XmlAttribute]
        public string password { get; set; }

        [XmlAttribute]
        public string unique_id { get; set; }

        [XmlAttribute]
        public string first_name { get; set; }

        [XmlAttribute]
        public string last_name { get; set; }

        [XmlAttribute]
        public string email { get; set; }

        [XmlAttribute]
        public string created { get; set; }

        [XmlAttribute]
        public string updated { get; set; }

        [XmlAttribute]
        public string bonus { get; set; }

        [XmlAttribute]
        public string achievements { get; set; }

        [XmlAttribute]
        public string best_score { get; set; }

        [XmlAttribute]
        public string gold { get; set; }

        [XmlAttribute]
        public string money { get; set; }

        [XmlAttribute]
        public string kills { get; set; }

        [XmlAttribute]
        public string lives { get; set; }

        [XmlAttribute]
        public string time_played { get; set; }

        [XmlAttribute]
        public string unlocked_levels { get; set; }

        [XmlAttribute]
        public string unlocked_items { get; set; }

        [XmlAttribute]
        public string inventory { get; set; }

        [XmlAttribute]
        public string last_level { get; set; }

        [XmlAttribute]
        public string current_level { get; set; }

        [XmlAttribute]
        public string current_time { get; set; }

        [XmlAttribute]
        public string current_bonus { get; set; }

        [XmlAttribute]
        public string current_kills { get; set; }

        [XmlAttribute]
        public string current_achievements { get; set; }

        [XmlAttribute]
        public string current_gold { get; set; }

        [XmlAttribute]
        public string current_unlocked_levels { get; set; }

        [XmlAttribute]
        public string current_unlocked_items { get; set; }

        [XmlAttribute]
        public string current_lifes { get; set; }

        [XmlAttribute]
        public string xp { get; set; }

        [XmlAttribute]
        public string energy { get; set; }

        [XmlAttribute]
        public string boost { get; set; }

        [XmlAttribute]
        public string latitude { get; set; }

        [XmlAttribute]
        public string longitude { get; set; }

        [XmlAttribute]
        public string game_state { get; set; }

        [XmlAttribute]
        public string platform { get; set; }

        [XmlAttribute]
        public string rank { get; set; }

        [XmlElement("score", Form = XmlSchemaForm.Unqualified)]
        public score[] scores { get; set; }
    }
}