namespace dropkick.Tasks.MsSql
{
    using System;
    using System.Data;
    using System.IO;
    using DeploymentModel;


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
                return string.Format("Run SqlScritp '{0}' on server '{1}\\{2}' for database '{3}'", ScriptToRun, ServerName, InstanceName,
                                     DatabaseName);
            }
        }


        public string ScriptToRun { get; set; }

        public string InstanceName { get; set; }


        public override DeploymentResult VerifyCanRun()
        {
            var result = new DeploymentResult();

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

        public override DeploymentResult Execute()
        {
            string s = File.ReadAllText(ScriptToRun);

            //TODO: need to use the sql dmo stuff
            ExecuteSqlWithNoReturn(s);

            return new DeploymentResult();
        }

        private void ExecuteSqlWithNoReturn(string sql)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
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