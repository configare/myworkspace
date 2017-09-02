using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestTools
{
    public class Model_CXInputArgs
    {
       
        /// <summary>
        /// 监测专题文件夹名称
        /// </summary>
        public string InputFileName
        {
            get; set;
        }
       
        /// <summary>
        /// 监测专题标识符
        /// </summary>
        public string OutPutFileName
        {
            get; set;
        }
        public static Model_CXInputArgs Deserialize(string xmlpath)
        {

            if (!File.Exists(xmlpath)) return null;
            var xml = File.ReadAllText(xmlpath);
            Model_CXInputArgs model = TestBase.Serializer.FromXml<Model_CXInputArgs>(xml);
            return model;

            //序列化文件
            //string xmlPath111 = GetXmlPath();
            //xmlPath111 = System.IO.Path.Combine(GetXmlPath(), "SNW111.xml");
            //xml = PIE.GF.Core.Helper.Serializer.ToXml<Product>(product);
            //File.WriteAllText(xmlPath111, xml);
        }
        public bool serialize()
        {
            Model_CXInputArgs model = new Model_CXInputArgs();
            model.InputFileName = @"D:\documents\visual studio 2015\testpie3d\TestReflector\TestReflector\bin\Debug\IR1原图_02014年8月28日161552.bmp";
            model.OutPutFileName = @"D:\documents\visual studio 2015\testpie3d\TestReflector\TestReflector\bin\Debug\IR1原图_02014年8月28日161552RH.bmp";
            var xml = TestBase.Serializer.ToXml<Model_CXInputArgs>(model);
            string xmlpath = @"D:\documents\visual studio 2015\testpie3d\TestReflector\TestReflector\bin\Debug\CXargs.xml";
            File.WriteAllText(xmlpath, xml);
            return true;
        }
    }
}
