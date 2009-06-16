namespace dropkick.Dsl.MsSql
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Visitors.Verification;

    public abstract class BaseSqlTask :
        Task
    {
        protected BaseSqlTask(string serverName, string databaseName)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
        }

        public abstract void Inspect(DeploymentInspector inspector);
        public abstract string Name { get; }
        public abstract VerificationResult VerifyCanRun();
        public abstract void Execute();

        public string ServerName { get; set; }
        public string DatabaseName { get; set; }

        public void TestConnectivity(VerificationResult result)
        {
            IDbConnection conn = null;
            try
            {

                conn = GetConnection();
                conn.Open();
                result.AddGood("I can talk to the database");
            }
            catch (Exception)
            {
                result.AddError("I cannot open the connection");
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();

                }
            }
        }
        
        public IDbConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
        string GetConnectionString()
        {
            var cs = new SqlConnectionStringBuilder
            {
                DataSource = ServerName,
                InitialCatalog = DatabaseName,
                IntegratedSecurity = true
            };

            return cs.ConnectionString;
        }

        
    }
}