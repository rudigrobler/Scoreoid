using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreoid
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class scores
    {
        [System.Xml.Serialization.XmlElementAttribute("player", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public player[] items { get; set; }
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class score
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string created { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string difficulty { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string platform { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute("score")]
        public string value { get; set; }
    }

}
