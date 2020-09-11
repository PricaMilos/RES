namespace Common.Database
{
    public interface ISqlQueryExecutor
    {
        string ExecuteSQLreturnJSON(string sQL);
    }
}