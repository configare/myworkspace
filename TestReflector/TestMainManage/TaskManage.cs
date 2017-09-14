using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestMainManage
{
    public class TaskManage
    {
        public void ExcuteTask()
        {
            Model_AutoMeteoTask model_task = new Model_AutoMeteoTask();
            if(model_task.ListSteps==null||model_task.ListSteps.Count==0)
            {
                throw new Exception("task config xml intival  failed！");
            }
            List<TaskStep> liststeps=model_task.ListSteps.OrderBy(o => o.MainStepNum).ToList();//对当前集合按照执行编号排序
            List<ManualResetEvent> _ManualEvents = new List<ManualResetEvent>();
            for (int i=0;i<liststeps.Count;i++)
            {
               if(liststeps[i].TaskSubStep.Count<0)
                {
                    continue;
                }    
              else
                {
                    List<ManualResetEvent> _listManualevents = new List<ManualResetEvent>();
                    for(int j=0;j<liststeps[i].TaskSubStep.Count;j++)
                    { 
                        ManualResetEvent itemevent = new ManualResetEvent(false);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ExcuteSimpleiStep), itemevent);
                        _listManualevents.Add(itemevent);
                    }
                    if (_ManualEvents.Count > 0)
                    {
                        WaitHandle.WaitAll(_ManualEvents.ToArray());//等待线程池任务执行结束
                    }
                }
            }
        }
        /// <summary>
        /// 单任务执行方法
        /// </summary>
        /// <param name="cmdid"></param>
        /// <returns></returns>
        private void ExcuteStep()
        {
        }
        /// <summary>
        /// 单任务
        /// </summary>
        private void ExcuteSimpleiStep(object TaskSubStep)
        {
            TaskSubStep substep = TaskSubStep as TaskSubStep;
            string str = typeof(PluginManage).Assembly.Location;
            FileInfo fi = new FileInfo(str);
            str = fi.DirectoryName;
            string subid= string.IsNullOrEmpty(substep.MeteoCmdID) ?"": (string)substep.MeteoCmdID;
            if(string.IsNullOrEmpty(subid))
            {
                return;//没有找到该编号的命令程序
            }
            ItemConfig currentplugin = GetCurrentPluginBycmdID(subid);
            System.Reflection.Assembly assembly_Sub = System.Reflection.Assembly.LoadFrom(str + "\\" + currentplugin.PluginPath);//创建程序集实例
            Type t = assembly_Sub.GetType(currentplugin.ClassName);
            object obj = System.Activator.CreateInstance(t);
            MethodInfo mi = t.GetMethod(currentplugin.PluginFunName);//创建方法
            mi.Invoke(obj, new string[] { substep.InputArgsFile });//传递参数
        }
        private ItemConfig GetCurrentPluginBycmdID(string cmdid)
        {
            string str = typeof(PluginManage).Assembly.Location;
            string pluconfig = str + "\\pgconfig.xml";//配置文件固定的，一般跟程序集放在一块
            PluginPyConfig config = PluginPyConfig.Deserialize(pluconfig);
            return config.ListItem.SingleOrDefault(o => o.PluginID == cmdid);
        }

    }
}
