using System.Data;

namespace FakeDataProvider
{
    public class FakeDbDataParameter : IDbDataParameter
    {
        public DbType DbType
        {
            get;
            set;
        }

        public ParameterDirection Direction
        {
            get;
            set;
        }

        public bool IsNullable
        {
            get { return false; }
        }

        public string ParameterName
        {
            get;
            set;
        }

        public string SourceColumn
        {
            get;
            set;
        }

        public DataRowVersion SourceVersion
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }

        public byte Precision
        {
            get;
            set;
        }

        public byte Scale
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }

        public override int GetHashCode()
        {
            return ParameterName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as FakeDbDataParameter);
        }

        public bool Equals(FakeDbDataParameter other)
        {
            return ParameterName.Equals(other.ParameterName);
        }
    }
}
