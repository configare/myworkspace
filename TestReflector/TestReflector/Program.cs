using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReflector
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            test2(path);
        }
        public static  void test()
        {
            TestTools.Model_CXInputArgs model = new TestTools.Model_CXInputArgs();
            model.serialize();
            Console.ReadLine();
        }
        public static void test1()
        {
            TestMainManage.PluginPyConfig model = new TestMainManage.PluginPyConfig();
            model.serialize();
            Console.ReadLine();
        }
        public static void test2(string xmlpath)
        {
            TestMainManage.PluginManage.ExcutePlugin("0002", xmlpath);
           

        }
    }
}
