using System.Data.Common;

namespace CppBrowser.DB
{
    public interface DB
    {
        DbDataReader ExecuteQuery(string query);
    }
}
