using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanSystem
{
    public class BillfeeType
    {
        public BillfeeType() { }
        private string billFeeTypeName;
        public string BillFeeTypeName
        {
            get { return billFeeTypeName; }
            set { this.billFeeTypeName = value; }
        }
        private string detail;
        public string Detail
        {
            get { return detail; }
            set { this.detail = value; }
        }
        private string groupId;
        public string GroupId
        {
            get { return groupId; }
            set { this.groupId = value; }
        }
        private string id;
        public string Id
        {
            get { return id; }
            set { this.id = value; }
        }
        private string isEnable;
        public string IsEnable
        {
            get { return isEnable; }
            set { this.isEnable = value; }
        }

        private string level;
        public string Level
        {
            get { return level; }
            set { this.level = value; }
        }

        private string parent;
        public string Parent
        {
            get { return parent; }
            set { this.parent = value; }
        }

    }
}
