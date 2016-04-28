using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ScanSystem
{
    public class SetConfig
    {
        public static void setValue(string key,string value)
        {
            string configPath = AppConfig();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(configPath);
            XmlNode xNode;
            XmlElement xElemUID;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElemUID = (XmlElement)xNode.SelectSingleNode(string.Format("//add[@key='{0}']", key));
            xElemUID.SetAttribute("value", value);
            xDoc.Save(configPath);
        }

        public static string AppConfig()
        {
            string strDirectoryPath = Application.StartupPath + @"\ScanSystem.exe.config";
            return strDirectoryPath;
        }
        public static string GetValue(string appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(AppConfig());
                XmlNode xNode;
                XmlElement xElem;
                xNode = xDoc.SelectSingleNode("//appSettings");
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
                if (xElem != null)
                    return xElem.GetAttribute("value");
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
