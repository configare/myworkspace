using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMainManage
{
    public class Model_AutoMeteoTask
    {
        public  Model_AutoMeteoTask()
        {
        }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string AutoTaskName { get; set; }
        /// <summary>
        /// 描述任务具体是干啥的
        /// </summary>
        public string TaskDesc { get; set; }
        /// <summary>
        /// 需要执行的步骤集合
        /// </summary>
        public List<TaskStep> ListSteps { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string Remark { get; set; }
         
    }
}
