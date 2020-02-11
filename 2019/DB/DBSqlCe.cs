using System;
using System.Data.Common;
using System.Data.SqlServerCe;

namespace CppBrowser.DB
{
    class DBSqlCe : DB, IDisposable
    {
        private string dbPath = null;
        private SqlCeConnection connection = null;
        private bool disposed = false;
        public DBSqlCe(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public DbDataReader ExecuteQuery(string query)
        {
            MakeConnection();

            if (null == connection)
                return null;

            SqlCeCommand command = connection.CreateCommand();
            command.CommandText = query;

            DbDataReader reader = command.ExecuteReader();

            command.Dispose();

            return reader;
        }

        private void MakeConnection()
        {
            if(null == connection)
            {
                string dbcstring = "Data Source=" + dbPath + ";Persist Security Info=False;";

                connection = new SqlCeConnection(dbcstring);
                connection.Open();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (null != connection)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            disposed = true;
        }
    }
}
