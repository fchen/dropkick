namespace dropkick.Tasks.MsSql
{
    using System.Data;
    using System.IO;
    using Configuration.Dsl;
    using Execution;
    using Verification;

    public class RunSqlScriptTask :
        BaseSqlTask
    {
        public RunSqlScriptTask(string serverName, string databaseName) : base(serverName, databaseName)
        {
        }

        public override string Name
        {
            get
            {
                return string.Format("Run SqlScritp '{0}' on server '{1}' for database '{2}'", ScriptToRun, ServerName,
                                     DatabaseName);
            }
        }


        public string ScriptToRun { get; set; }

        public override void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public override VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();

            base.TestConnectivity(result);


            if (ScriptToRun != null)
            {
                result.AddAlert(string.Format("I will run the sql script at '{0}'", ScriptToRun));
                if (File.Exists(ScriptToRun))
                {
                    result.AddGood(string.Format("I found the script '{0}'", ScriptToRun));
                }
                else
                {
                    result.AddAlert("I can't find '{0}'", ScriptToRun);
                }
            }

            return result;
        }

        public override ExecutionResult Execute()
        {
            string s = File.ReadAllText(ScriptToRun);
            ExecuteSqlWithNoReturn(s);

            return new ExecutionResult();
        }

        private void ExecuteSqlWithNoReturn(string sql)
        {
            using (IDbConnection conn = GetConnection())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}