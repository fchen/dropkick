namespace dropkick.Dsl.MsSql
{
    using System;
    using System.Data;
    using System.IO;
    using Engine;

    public class RunSqlScriptTask :
        BaseSqlTask
    {
        public RunSqlScriptTask(string serverName, string databaseName) : base(serverName, databaseName)
        {
        }

        public override void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public override string Name
        {
            get { return string.Format("Run SqlScritp '{0}' on server '{1}' for database '{2}'", ScriptToRun, ServerName, DatabaseName); }
        }

        
        public string ScriptToRun { get; set; }

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
                    result.AddError("I can't find '{0}'", ScriptToRun);
                }
            }

            return result;
        }

        public override void Execute()
        {
            var s = File.ReadAllText(ScriptToRun);
            ExecuteSqlWithNoReturn(s);
        }

        void ExecuteSqlWithNoReturn(string sql)
        {
            using(var conn = GetConnection())
            {
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}