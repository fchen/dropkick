namespace dropkick.Dsl.MsSql
{
    using System;
    using System.Data;
    using Engine;

    public class MsSqlTask :
        BaseSqlTask
    {
        public MsSqlTask(string serverName, string databaseName) : base(serverName, databaseName)
        {
        }

        public override void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public override string Name
        {
            get { return string.Format("SQL TASK on server '{0}' for database '{1}'", ServerName, DatabaseName); }
        }

        public string OutputSql { get; set; }

        public override VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();

            //can I connect to the server?
           
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

            //can I connect to the database?
            if(OutputSql != null)
                result.AddAlert(string.Format("I will run the sql '{0}'", OutputSql));

               
            return result;
        }

        public override void Execute()
        {
            using(var conn = GetConnection())
            {
              using(var cmd = conn.CreateCommand())
              {
                  //hmmm if its a script versus non-script
              }
            }
        }      
    }
}