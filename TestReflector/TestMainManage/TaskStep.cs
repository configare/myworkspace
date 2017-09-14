using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMainManage
{
   public  class TaskStep
    {
        public TaskStep()
        {

        }
        /// <summary>
        /// 需要顺序运行的步骤
        /// </summary>
        public int? MainStepNum { get; set; }
        /// <summary>
        /// 包含的子步骤
        /// </summary>
        public List<TaskSubStep> TaskSubStep { get; set; }
        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Desc { get; set; }
       
    }
}
