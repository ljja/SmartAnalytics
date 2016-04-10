using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAnalytics.Services.Models
{
    public class UserLocalPageItem
    {
        /// <summary>
        /// rowkey
        /// </summary>
        public string rowkey { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public string visitTime { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 访问页面
        /// </summary>
        public string visitPage { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// 用户追踪码
        /// </summary>
        public string traceCode { get; set; }


    }
}
