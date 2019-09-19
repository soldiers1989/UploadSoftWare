using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test.model
{
    [XmlRoot(ElementName = "apas_info")]
    public class createInfoXml
    {
        [XmlElement(ElementName = "serviceid")]
        public string Serviceid { get; set; }
        [XmlElement(ElementName = "projectname")]
        public string Projectname { get; set; }
        [XmlElement(ElementName = "applyname")]
        public string Applyname { get; set; }
        [XmlElement(ElementName = "mobile")]
        public string Mobile { get; set; }
        [XmlElement(ElementName = "phone")]
        public string Phone { get; set; }
        [XmlElement(ElementName = "address")]
        public string Address { get; set; }
        [XmlElement(ElementName = "postcode")]
        public string Postcode { get; set; }
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "contactman")]
        public string Contactman { get; set; }
        [XmlElement(ElementName = "legalman")]
        public string Legalman { get; set; }
        [XmlElement(ElementName = "idcard")]
        public string Idcard { get; set; }
        [XmlElement(ElementName = "create_time")]
        public string Create_time { get; set; }
        [XmlElement(ElementName = "receive_time")]
        public string Receive_time { get; set; }
    }
}
