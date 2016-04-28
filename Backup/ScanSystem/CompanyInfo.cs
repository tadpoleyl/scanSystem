using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanSystem
{
    public class CompanyInfo
    {
        public CompanyInfo()
        {
        }
        private string id;
        public string Id
        {
            get { return id; }
            set { this.id = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        private string code;
        public string Code { get { return code; } set { this.code = value; } }
        private string dtTime;
        public string DtTime { get { return dtTime; } set { this.dtTime = value; } }

        private string num;
        public string Num { get { return num; } set { this.num = value; } }

        /// <summary>
        /// 默认值是0  0：表示已上传，1：表示扫描，但未上传
        /// </summary>
        private int status;
        /// <summary>
        /// 默认值是0  0：表示已上传，1：表示扫描，但未上传
        /// </summary>
        public int Status { get { return status; } set { this.status = value; } }
    }
}
