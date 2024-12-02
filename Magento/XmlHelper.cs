using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Magento
{
    public class XmlHelper
    {
        public static List<Dictionary<string, string>> ReadXmlData(string filePath, string nodeName)
        {
            var dataList = new List<Dictionary<string, string>>();
            var doc = new XmlDocument();
            doc.Load(filePath);

            foreach (XmlNode node in doc.SelectNodes($"//{nodeName}/TestCase"))
            {
                var data = new Dictionary<string, string>();
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    data[childNode.Name] = childNode.InnerText;
                }
                dataList.Add(data);
            }

            return dataList;
        }
    }

}
