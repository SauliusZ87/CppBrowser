using System;
using System.Data.Common;
using System.Data.SQLite;

namespace CppBrowser.DB
{
    class DBSqlLite : DB, IDisposable
    {
        private string dbPath = null;
        private SQLiteConnection connection = null;
        private bool disposed = false;
        public DBSqlLite(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public DbDataReader ExecuteQuery(string query)
        {
            MakeConnection();

            if (null == connection)
                return null;

            SQLiteCommand command = new SQLiteCommand(query, connection);
            DbDataReader reader = command.ExecuteReader();

            command.Dispose();

            return reader;
        }

        private void MakeConnection()
        {
            if (null == connection)
            {
                string dbcstring = "Data Source=" + dbPath + ";Version=3;Read Only=True;PRAGMA journal_mode=WAL;";

                connection = new SQLiteConnection(dbcstring);
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
