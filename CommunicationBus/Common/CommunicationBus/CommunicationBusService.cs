using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common.CommunicationBus
{
    public class CommunicationBusService
    {
        public XNode Node{ get; set; }
        XmlToDBAdapter xMLToSQL = new XmlToDBAdapter();

        public CommunicationBusService()
        {
        }

        public CommunicationBusService(XNode node)
        {

            Node = node;
        }
        public string Run(XNode node = null)
        {
            if(node != null)
            {
                Node = node;
            }
            return xMLToSQL.Convert(Node);
        }
    }
}
