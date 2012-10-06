using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreoid
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class players
    {
        [System.Xml.Serialization.XmlElementAttribute("player", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public player[] items { get; set; }
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class player
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string username { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string password { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unique_id { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string first_name { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string last_name { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string email { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string created { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string updated { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string bonus { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string achievements { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string best_score { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string gold { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string money { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string kills { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lives { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string time_played { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unlocked_levels { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unlocked_items { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string inventory { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string last_level { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string current_level { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string current_time { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string current_bonus { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string current_kills { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string current_achievements { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string current_gold { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string current_unlocked_levels { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string current_unlocked_items { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string current_lifes { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string xp { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string energy { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string boost { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string latitude { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string longitude { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string game_state { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string platform { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string rank { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("score", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public score[] scores { get; set; }
    }
}
