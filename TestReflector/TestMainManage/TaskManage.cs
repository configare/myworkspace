using System;
using System.Collections.Generic;
using System.Linq;
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
            CallContext.LogicalSetData("arg", liststeps);
            for (int i=0;i<liststeps.Count;i++)
            {
                if(liststeps[i].SubStepNum>0)
                {
                    ManualResetEvent itemevent = new ManualResetEvent(false);
                    
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ExcuteStep), itemevent);
                    
                   
                }
                else
                {
                    if (_ManualEvents.Count > 0)
                    {
                        WaitHandle.WaitAll(_ManualEvents.ToArray());//只是等待当前子任务的线程结束
                        
                    }
                    else
                        ExcuteStep();
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
        /// 单任务包含多
        /// </summary>
        private void ExcuteMultiStep(int mainStepid)
        {
           
        }
        public void ExcuteTaskModel()
        {
            Model_AutoMeteoTask model_task = new Model_AutoMeteoTask();
            if (model_task.ListSteps == null || model_task.ListSteps.Count == 0)
            {
                throw new Exception("task config xml intival  failed！");
            }
            List<TaskStep> liststeps = model_task.ListSteps.OrderBy(o => o.MainStepNum).ToList();//对当前集合按照执行编号排序--没必要

            List<ManualResetEvent> _ManualEvents = new List<ManualResetEvent>();
            CallContext.LogicalSetData("arg", liststeps);
            for (int i = 0; i < liststeps.Count; i++)
            {
                if (liststeps[i].SubStepNum > 0)
                {
                    ManualResetEvent itemevent = new ManualResetEvent(false);

                    ThreadPool.QueueUserWorkItem(new WaitCallback(ExcuteStep), itemevent);


                }
                else
                {
                    if (_ManualEvents.Count > 0)
                    {
                        WaitHandle.WaitAll(_ManualEvents.ToArray());//只是等待当前子任务的线程结束

                    }
                    else
                        ExcuteStep();
                }
            }
        }
    }
}
