using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMainManage
{
    public class TaskStep
    {
        public  TaskStep()
        {
           
        }
        /// <summary>
        /// 需要顺序运行的步骤
        /// </summary>
        public int? MainStepNum { get; set; }
        /// <summary>
        /// 可以并行运行的步骤
        /// </summary>
        public int? SubStepNum { get; set; }
        /// <summary>
        /// Meteo系统定义的执行
        /// </summary>
        public string MeteoCmdID { get; set; }
        /// <summary>
        /// 输入参数配置文件
        /// </summary>
        public string InputArgsFile { get; set; }
        /// <summary>
        /// //运行步骤描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 步骤是否执行
        /// </summary>
        public bool? IsExcute { get; set; }
    }
}
