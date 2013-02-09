using System;
using System.Data;

namespace FakeDataProvider
{
    public class FakeDbConnection : IDbConnection
    {
        protected ConnectionState ConnectionState = ConnectionState.Closed;

        public FakeDbConnection(string connectionString, Func<IDbCommand, IDataReader> onExecute)
        {
            OnExecute = onExecute;
            ConnectionString = connectionString;
        }

        public FakeDbConnection()
        {
            OnExecute = command => new FakeDataReader(0);
        }

        public void Dispose()
        {
        }

        public IDbTransaction BeginTransaction()
        {
            return new FakeDbTransaction(this, IsolationLevel.Serializable); 
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return new FakeDbTransaction(this, il);
        }

        public void Close()
        {
            ConnectionState = ConnectionState.Closed;
        }

        public void ChangeDatabase(string databaseName)
        {
        }

        public IDbCommand CreateCommand()
        {
            return new FakeDbCommand(this); 
        }

        public void Open()
        {
            ConnectionState = ConnectionState.Open;
        }

        public string ConnectionString { get; set; }

        public int ConnectionTimeout
        {
            get { return 0; }
        }

        public string Database
        {
            get { return string.Empty; }
        }

        public ConnectionState State
        {
            get { return ConnectionState; }
        }

        public Func<IDbCommand, IDataReader> OnExecute { get; set; }
    }
}
