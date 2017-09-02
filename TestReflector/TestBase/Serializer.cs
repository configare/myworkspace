using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace TestBase
{
    public static class Serializer
    {
        /// <summary>
        /// 文本化XML序列化
        /// </summary>
        /// <param name="item">对象</param>
        public static string ToXml<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(sb, xmlWriterSettings))
            {
                serializer.Serialize(writer, item);
                return sb.ToString().Trim();
            }
        }
        /// <summary>
        /// 文本化XML反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public static T FromXml<T>(string str)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (XmlReader reader = new XmlTextReader(new StringReader(str)))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public static void ToXmlFile<T>(T item, string path)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            FileStream stream = null;
            stream = File.Open(path, FileMode.OpenOrCreate);
            if (stream == null) return;
            TextWriter txtWriter = new StreamWriter(stream);
            var setting = new XmlWriterSettings();
            setting.Indent = true;//允许缩进
            using (XmlWriter writer = XmlWriter.Create(txtWriter, setting))
            {
                serializer.Serialize(writer, item);
            }
            txtWriter.Flush();
            txtWriter.Dispose();
        }


       
    }
}
