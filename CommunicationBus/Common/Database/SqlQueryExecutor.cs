using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public class SqlQueryExecutor : ISqlQueryExecutor
    {
        public string ExecuteSQLreturnJSON(string sQL)
        {
            CBDataContext dbContext = new CBDataContext();
            object result = null;
            if (!sQL.Contains("SELECT"))
                result = dbContext.Database.ExecuteSqlCommand(sQL);
            else if (sQL.Contains("Resurs"))
                result = dbContext.Resurs.SqlQuery(sQL).ToList();
            else if (sQL.Contains("Tip"))
                result = dbContext.Tip.SqlQuery(sQL).ToList();
            else if (sQL.Contains("TipVeze"))
                result = dbContext.TipVeze.SqlQuery(sQL).ToList();
            else if (sQL.Contains("Veza"))
                result = dbContext.Veza.SqlQuery(sQL).ToList();

            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }
    }
}
