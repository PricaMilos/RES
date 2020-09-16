using Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.CommunicationBus;

namespace Tests
{
    [TestClass]
    public class TestXmlToDB
    {
        [TestMethod]

        //GET/Resources/1
        public void TestGenerateGet()
        {
            Request request = new Request("GET", "Resurs/1", null, null);
            string json = JsonConvert.SerializeObject(request);
            XmlToDBAdapter adapter = new XmlToDBAdapter();
            string sql = adapter.Convert(JsonConvert.DeserializeXNode(json, "Request"));
            string expected = "SELECT * FROM Resources WHERE Id = 1;";
            Assert.AreEqual(sql, expected);
        }

        [TestMethod]
        //GET/Resources//Name='Petar';Description='opis'/Name;Description
        public void TestGenerateGet2()
        {
            Request request = new Request("GET", "Resources/", "Name='Petar';Description='opis'", "Name;Description");
            string json = JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented);
            XmlToDBAdapter adapter = new XmlToDBAdapter();
            string sql = adapter.Convert(JsonConvert.DeserializeXNode(json, "Request"));
            string expected = "SELECT Name,Description FROM Resources WHERE Name='Petar' and Description='opis';";
            Assert.AreEqual(sql, expected);
        }

        [TestMethod]
        //GET/Resources//Name='Petar';Description='opis'"
        public void TestGenerateGet3()
        {
            Request request = new Request("GET", "Resources/", "Name='Petar';Description='opis'", null);
            string json = JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented);
            XmlToDBAdapter adapter = new XmlToDBAdapter();
            string sql = adapter.Convert(JsonConvert.DeserializeXNode(json, "Request"));
            string expected = "SELECT * FROM Resources WHERE Name='Petar' and Description='opis';";
            Assert.AreEqual(sql, expected);
        }

        [TestMethod]
        //POST/Resources//Name;Description/'Milan';'Description'
        public void TestGeneratePost()
        {
            Request request = new Request("POST", "Resources/", "Name;Description", "'Milan';'Description'");
            string json = JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented);
            XmlToDBAdapter adapter = new XmlToDBAdapter();
            string sql = adapter.Convert(JsonConvert.DeserializeXNode(json, "Request"));
            string expected = "INSERT Into Resources (Name, Description) VALUES ('Milan', 'Description');";
            Assert.AreEqual(sql, expected);
        }

        [TestMethod]
        //DELETE/Resources//Name='Milan';Description='opis'
        public void TestGenerateDelete()
        {
            Request request = new Request("DELETE", "Resources/", "Name='Milan';Description='opis", null);
            string json = JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented);
            XmlToDBAdapter adapter = new XmlToDBAdapter();
            string sql = adapter.Convert(JsonConvert.DeserializeXNode(json, "Request"));
            string expected = "DELETE FROM Resources WHERE Name='Milan' and Description='opis;";
            Assert.AreEqual(sql, expected);
        }

        [TestMethod]
        //"PATCH/Resources//Name='Milan'/Name='Marko'"
        public void TestGeneratePatch()
        {
            Request request = new Request("PATCH", "Resources/", "Name='Milan'", "Name='Marko'");
            string json = JsonConvert.SerializeObject(request, Newtonsoft.Json.Formatting.Indented);
            XmlToDBAdapter adapter = new XmlToDBAdapter();
            string sql = adapter.Convert(JsonConvert.DeserializeXNode(json, "Request"));
            string expected = "UPDATE Resources SET Name='Marko' WHERE Name='Milan';";
            Assert.AreEqual(sql, expected);
        }

    }
}
