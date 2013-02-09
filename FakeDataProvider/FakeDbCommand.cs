using System;
using System.Data;

namespace FakeDataProvider
{
    public class FakeDbCommand : IDbCommand
    {
        private readonly FakeDataParameterCollection _dataParameterCollection = new FakeDataParameterCollection();
        private FakeDbConnection _fakeDbConnection;

        public FakeDbCommand()
        { 
        }

        public FakeDbCommand(IDbConnection dbConnection)
        {
            Connection = dbConnection;
        }

        public void Dispose()
        {
        }

        public void Prepare()
        {
        }

        public void Cancel()
        {
        }

        public IDbDataParameter CreateParameter()
        {
            return new FakeDbDataParameter();
        }

        public int ExecuteNonQuery()
        {
            return _fakeDbConnection.OnExecute(this).RecordsAffected;
        }

        public IDataReader ExecuteReader()
        {
            return _fakeDbConnection.OnExecute(this);
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return _fakeDbConnection.OnExecute(this);
        }

        public object ExecuteScalar()
        {
            return null;
        }

        public IDbConnection Connection
        {
            get { return _fakeDbConnection; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                if (!(value is FakeDbConnection))
                {
                    throw new ArgumentException("dbConnection must be of type FakeDbConnection");
                }

                _fakeDbConnection = value as FakeDbConnection;
            }
        }

        public IDbTransaction Transaction
        {
            get;
            set;
        }

        public string CommandText
        {
            get;
            set;
        }

        public int CommandTimeout
        {
            get;
            set;
        }

        public CommandType CommandType
        {
            get;
            set;
        }

        public IDataParameterCollection Parameters
        {
            get { return _dataParameterCollection; }
        }

        public UpdateRowSource UpdatedRowSource
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
