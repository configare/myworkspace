using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestMainManage
{
    public class ItemConfig
    {
        //这是笔记本提交的
       //chenan  这是台式机提交的
        public string PluginID
        {
            get; set;
        }
        
        public string PluginPath
        {
            get; set;
        }
        
        public string ClassName
        {
            get;set;
        }
     
        public string PluginFunName
        {
            get; set;
        }
    }
}
