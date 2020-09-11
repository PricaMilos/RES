using Common.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Common.CommunicationBus
{
    public class XmlToDBAdapter
    {
        public string SQL { get; set; }
        private ISqlQueryExecutor queryExecutor;
        public XmlToDBAdapter()
        {
            queryExecutor = new SqlQueryExecutor();
        }

        public XmlToDBAdapter(ISqlQueryExecutor queryExecutor)
        {
            this.queryExecutor = queryExecutor;
        }

        public string Convert(XNode node)
        {
            XElement element = node.Document.Element("Request");
            string metod = element.Element("Verb").Value;
            string verb = element.Element("Noun").Value;
            string[] deo = verb.Split('/');
            string SQL = "";
            if (metod.ToUpper() == "GET")
            {
                SQL = GenerateSelect(element);
            }
            else if (metod.ToUpper() == "POST")
            {
                SQL = GenerateInsert(element);
            }
            else if (metod.ToUpper() == "PATCH")
            {
                SQL = GenerateUpdate(element);
            }
            else if (metod.ToUpper() == "DELETE")
            {
                SQL = GenerateDelete(element);
            }
            else
            {
                SQL = "";
            }

            string result = queryExecutor.ExecuteSQLreturnJSON(SQL);

            var xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(
                Encoding.ASCII.GetBytes(result ), new XmlDictionaryReaderQuotas()));//JSON TO XML
            xml.ToString();

            return result;
        }
        
        private string GenerateSelect(XElement element)
        {
            string sqlQuery = "";
            string noun = element.Element("Noun").Value;
            string query = element.Element("Query").Value;
            string fields = element.Element("Fields").Value;

            if (string.IsNullOrEmpty(fields))
                fields = "*";
            else
                fields = fields.Replace(";", ",");

            string[] nounParts = noun.Split('/');
            sqlQuery = $"SELECT {fields} FROM {nounParts[0]} WHERE ";
            if (!string.IsNullOrEmpty(nounParts[1]))
                sqlQuery += $"Id = {nounParts[1]}";

            if (string.IsNullOrEmpty(query))
                query = "";
            else if (!noun.Contains("Id"))
                query = query.Replace(";", " and ");
            else
                query = " and " + query.Replace(";", " and ");

            sqlQuery += $"{query};";

            return sqlQuery;
        }
        private string GenerateInsert(XElement element)
        {
            string noun = element.Element("Noun").Value.Split('/')[0];
            string query = element.Element("Query").Value;
            string fields = element.Element("Fields").Value;
            return $"INSERT Into {noun} ({query.Replace(";", ", ")}) VALUES ({fields.Replace(";", ", ")});"; ;
        }
        private string GenerateUpdate(XElement element)
        {
            string noun = element.Element("Noun").Value.Split('/')[0];
            string query = element.Element("Query").Value;
            string fields = element.Element("Fields").Value;
            string[] nounSplited = noun.Split('/');

            return $"UPDATE {nounSplited[0]} SET {fields.Replace(";", ",")} WHERE {query.Replace(";", " and ")};";
        }
        private string GenerateDelete(XElement element)
        {
            string sqlQuery = "";
            string noun = element.Element("Noun").Value.Split('/')[0];
            string query = element.Element("Query").Value;
            string fields = element.Element("Fields").Value;
            string[] nounSplited = noun.Split('/');

            return $"DELETE FROM {nounSplited[0]} WHERE {query.Replace(";", " and ")};"; ;
        }
    }
}
