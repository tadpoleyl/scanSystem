using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanSystem
{
    public class ScanInfo
    {
        public ScanInfo() { }
        private string imgName;
        public string ImgName
        {
            get { return imgName; }
            set { this.imgName = value; }
        }
        private DateTime dt;
        public DateTime Dt
        {
            get { return dt; }
            set { this.dt = value; }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set { this.type = value; }
        }
        private string size;
        public string Size
        {
            get { return size; }
            set { this.size = value; }
        }
        private string companyName;
        public string CompanyName
        {
            get { return companyName; }
            set { this.companyName = value; }
        }
        private string operation;
        public string Operation
        {
            get { return operation; }
            set { this.operation = value; }
        }
        private string add;
        public string Add { get { return add; } set { this.add = value; } }
    }
}
