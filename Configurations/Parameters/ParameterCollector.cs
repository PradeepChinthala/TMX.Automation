using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Configurations.Parameters
{
    public class ParameterCollector
    {
        public virtual void Collect(string location=null, List<string> collectionCriteria = null)
        {
            var xmlDoc = new XmlDocument();
            var parametersFileName = Parameter.GetEmbedded(location);
            xmlDoc.Load(parametersFileName);
            foreach(var sectionXpath in collectionCriteria)
            {
                // Select root node of provided section.
                var xmlNode = xmlDoc.SelectSingleNode(sectionXpath);
                // Inspect if provided section contains any child node,
                // if not log appropriate info message and continue further.
                if (xmlNode != null && xmlNode.HasChildNodes)
                {
                    // Collect value form each child node.
                    foreach (XmlNode node in xmlNode.ChildNodes)
                        Parameter.Add<string>(node.Name, node.InnerText.ToString());
                    // Log appropriate collect summary info message.
                    Logger.InfoFormat($"[Collected] - Parameters file: [{parametersFileName}]. Section xpath: [{sectionXpath}]. Parameters collected: [{xmlNode.ChildNodes.Count}].");
                }
                else
                {
                    Logger.InfoFormat($"[Skipping collection] - Parameters file: [{parametersFileName}]. Section xpath: [{sectionXpath}], there are no parameters or xpath is wrong.");
                }
            }
        }

        public virtual void CollectParmaters()
        {

        }
    }
}
