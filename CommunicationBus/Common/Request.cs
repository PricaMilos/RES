using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Request
    {
        public string Verb { get; set; }
        public string Noun { get; set; }
        public string Query { get; set; }
        public string Fields { get; set; }

        public Request(string verb, string noun, string query, string fields)
        {
            Verb = verb;
            Noun = noun;
            Query = query;
            Fields = fields;
        }
        public Request()
        {

        }
    }
}
