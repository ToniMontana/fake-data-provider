using System.Data;

namespace FakeDataProvider
{
    public class FakeDbTransaction : IDbTransaction
    {
        public FakeDbTransaction(IDbConnection dbConnection, IsolationLevel isolationLevel)
        {
            Connection = dbConnection;
            IsolationLevel = isolationLevel;
        }

        public void Dispose()
        {
        }

        public void Commit()
        {
        }

        public void Rollback()
        {
        }

        public IDbConnection Connection
        {
            get; protected set;
        }

        public IsolationLevel IsolationLevel
        {
            get; protected set;
        }
    }
}
