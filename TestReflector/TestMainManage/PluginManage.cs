using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace TestMainManage
{
    public class PluginManage
    {
        /// <summary>
        /// 统一脚本功能读取
        /// </summary>
        /// <param name="PluginID">功能编号</param>
        /// <param name="xmlpath">功能配置文件</param>
        public static void ExcutePlugin(string PluginID, string xmlpath)
        {
            string str = typeof(PluginManage).Assembly.Location;
            FileInfo fi = new FileInfo(str);
            str = fi.DirectoryName;
            string pluconfig = AppDomain.CurrentDomain.BaseDirectory + "\\pgconfig.xml";//配置文件固定的，一般跟程序集放在一块
            PluginPyConfig config = PluginPyConfig.Deserialize(pluconfig);
            ItemConfig currentitem = config.ListItem.SingleOrDefault(o => o.PluginID == PluginID);
            System.Reflection.Assembly assembly_Sub = System.Reflection.Assembly.LoadFrom(str+"\\"+currentitem.PluginPath);//创建程序集实例
            Type t = assembly_Sub.GetType(currentitem.ClassName);
            object obj = System.Activator.CreateInstance(t);
            MethodInfo mi = t.GetMethod(currentitem.PluginFunName);//创建方法
            mi.Invoke(obj, new string[] { xmlpath });//传递参数
        }
        public static string GetCurrentTime()
        {
            Console.WriteLine("sssssssssssssssssssssssss");
            return string.Format("当前时间是：{0}，当前年是：{1}", DateTime.Now, 1998);
        }
        public static object GetxmlString(string PluginID, string xmlpath)
        {
            string str = typeof(PluginManage).Assembly.Location;
            FileInfo fi = new FileInfo(str);
            str = fi.DirectoryName;
            string pluconfig = str + "\\pgconfig.xml";//配置文件固定的，一般跟程序集放在一块
            PluginPyConfig config = PluginPyConfig.Deserialize(pluconfig);
            Console.WriteLine(config.ListItem[0].PluginFunName);
            Console.WriteLine(PluginID);
            ItemConfig currentitem = config.ListItem.SingleOrDefault(o => o.PluginID == PluginID);
            System.Reflection.Assembly assembly_Sub = System.Reflection.Assembly.LoadFrom(str + "\\" + currentitem.PluginPath);//创建程序集实例
            Type t = assembly_Sub.GetType(currentitem.ClassName);
            object obj = System.Activator.CreateInstance(t);
            MethodInfo mi = t.GetMethod(currentitem.PluginFunName);//创建方法
            Console.WriteLine(xmlpath);

            mi.Invoke(obj, new string[] { xmlpath });//传递参数
            return obj;

        }

    }
}
