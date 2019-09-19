using System;
using System.IO;
using System.Xml.Serialization;

namespace comModel
{
    public class XmlSerialize
    {
        public T DeserializeXML<T>(string xmlObj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xmlObj))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public T DeserializeXMLFromFile<T>(string file)
        {
            T t = default(T);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                FileStream fs = File.Open(file, FileMode.Open);
                t = (T)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
            return t;
        }

        public string SerializeXML<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize((TextWriter)writer, obj);
                return writer.ToString();
            }
        }

        public void SerializeXMLToFile<T>(T obj, string file)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                FileStream fs = File.Open(file, FileMode.Create);
                serializer.Serialize(fs, obj);
                fs.Close();
            }
            catch (Exception ex)
            {
                FileUtils.Log(ex.ToString());
            }
        }
    }
}

#region 
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Xml.Serialization;

//using System.IO;

//namespace comModel
//{
//    public class XmlSerialize
//    {
//        public T DeserializeXML<T>(string xmlObj)
//        {
//            XmlSerializer serializer = new XmlSerializer(typeof(T));
//            using (StringReader reader = new StringReader(xmlObj))
//            {
//                return (T)serializer.Deserialize(reader);
//            }
//        }

//        public string SerializeXML<T>(T obj)
//        {
//            XmlSerializer serializer = new XmlSerializer(obj.GetType());
//            using (StringWriter writer = new StringWriter())
//            {
//                serializer.Serialize((TextWriter)writer, obj);
//                return writer.ToString();
//            }
//        }
//    }
//}
#endregion