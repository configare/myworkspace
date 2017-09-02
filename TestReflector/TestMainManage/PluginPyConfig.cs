using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMainManage
{
    public class PluginPyConfig
    {
        public List<ItemConfig> ListItem
        {
            get; set;
        }
        public static PluginPyConfig Deserialize(string xmlpath)
        {

            if (!File.Exists(xmlpath)) return null;
            var xml = File.ReadAllText(xmlpath);
            PluginPyConfig model = TestBase.Serializer.FromXml<PluginPyConfig>(xml);
            return model;

           
        }
        public bool serialize()
        {
            PluginPyConfig listitem = new PluginPyConfig();
            listitem.ListItem = new List<ItemConfig>();
            ItemConfig item1 = new ItemConfig();
            item1.ClassName = "UsSharpen";
            item1.PluginFunName = "LayAnalysis";
            item1.PluginID = "0001";
            item1.PluginPath = "TestTools.dll";
            ItemConfig item2 = new ItemConfig();
            item2.ClassName = "UsSharpen";
            item2.PluginFunName = "UsharpAnalysis";
            item2.PluginID = "0002";
            item2.PluginPath = "TestTools.dll";

            listitem.ListItem.Add(item1);
            listitem.ListItem.Add(item2);

            var xml = TestBase.Serializer.ToXml<PluginPyConfig>(listitem);
            string xmlpath = @"D:\documents\visual studio 2015\testpie3d\TestReflector\TestReflector\bin\Debug\pgconfig.xml";
            File.WriteAllText(xmlpath, xml);
            //System.Threading.Thread.Sleep(2000);
            //PluginPyConfig config = TestBase.Serializer.FromXml<PluginPyConfig>(xmlpath);

            //Model_CXInputArgs model = new Model_CXInputArgs();
            //model.InputFileName = @"D:\Documents\Downloads\Strengthen_IR\Strengthen_IR\Strengthen_IR\bin\Debug\ResultPic\IR1原图_02014年8月28日161552.bmp";
            //model.OutPutFileName = @"D:\Documents\Downloads\Strengthen_IR\Strengthen_IR\Strengthen_IR\bin\Debug\ResultPic\IR1原图_02014年8月28日161552RH.bmp";
            //var xml = TestBase.Serializer.ToXml<Model_CXInputArgs>(model);
            //string xmlpath = @"D:\Documents\Downloads\Strengthen_IR\Strengthen_IR\Strengthen_IR\bin\Debug\ResultPic\test1.xml";
            //File.WriteAllText(xmlpath, xml);
            return true;
        }

    }
}
