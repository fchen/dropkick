namespace dropkick.Dsl.MsSql
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Engine;

    public class MsSqlTask :
        Task
    {
        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public string Name
        {
            get { return string.Format("SQL TASK on server '{0}' for database '{1}'", ServerName, DatabaseName); }
        }

        public string ServerName { get; set; }
        public string DatabaseName { get; set; }

        public string OutputSql { get; set; }

        public VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();

            //can I connect to the server?
            var cs = new SqlConnectionStringBuilder
                     {
                         DataSource = ServerName,
                         InitialCatalog = DatabaseName,
                         IntegratedSecurity = true
                     };
            IDbConnection conn = null;
            try
            {

                conn = new SqlConnection(cs.ConnectionString);
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

            //can I connect to the database?

            result.AddAlert(string.Format("I will run the sql '{0}'", OutputSql));
            return result;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}