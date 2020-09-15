using Common.CommunicationBus;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommunicationBus;
using Newtonsoft.Json;

namespace CommunicationBus
{
    class Program
    {
        static void Main(string[] args)
        {
            StringToJSONConverter stringToJSON = new StringToJSONConverter();
            string URL = "";

            while (true)
            {
                Console.WriteLine("Unesite URL: ");
                URL = Console.ReadLine();
  
                string jsonRequest = stringToJSON.Convert(URL);
                Console.WriteLine(jsonRequest);

                XNode xmlNode = JsonConvert.DeserializeXNode(jsonRequest, "Request");

                CommunicationBusService CBService = new CommunicationBusService(xmlNode);
                Console.WriteLine(CBService.Run(xmlNode));

                XElement element = xmlNode.Document.Element("Request");
            }

        }
        
    }
}
