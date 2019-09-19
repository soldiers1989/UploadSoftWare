using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Test.model
{
    public static class XmlUtils
    {
        public static string Serialize<T>(this T obj, bool omitXmlDeclaration = false)
        {
            var sb = new StringBuilder();
            using (var xw = XmlWriter.Create(sb, new XmlWriterSettings()
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                ConformanceLevel = ConformanceLevel.Auto,
                Indent = true
            }))
            {
                var xs = new XmlSerializer(obj.GetType());
                xs.Serialize(xw, obj);
            }

            return sb.ToString();
        }
 
    }
}
