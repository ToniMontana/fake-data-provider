using System;
using System.Collections.Generic;
using System.Data;

namespace FakeDataProvider
{
    public class FakeDataReader : IDataReader
    {
        private readonly int _rowsAffected;
        private readonly string[] _columnNames;
        readonly List<object[]> _rows = new List<object[]>();
        private int _currentRowNo = -1;
        private bool _isClosed = false;
        private readonly Dictionary<string, int> _nameToOrdinal; 

        public FakeDataReader(int rowsAffected, params string[] columnNames)
        {
            _rowsAffected = rowsAffected;
            _columnNames = columnNames;

            _nameToOrdinal = new Dictionary<string, int>();
            for(int i=0;i<columnNames.Length;i++)
            {
                _nameToOrdinal.Add(columnNames[i], i);
            }
        }

        public void AddRow(params object[] columns)
        {
            _rows.Add(columns);
        }

        public void Dispose()
        {
            _rows.Clear();
        }

        public string GetName(int i)
        {
            return _columnNames[i];
        }

        public string GetDataTypeName(int i)
        {
            if (_rows.Count > 0)
            {
                return _rows[0][i].GetType().Name;
            }

            return _columnNames[i];
        }

        public Type GetFieldType(int i)
        {
            if (_rows.Count > 0)
            {
                return _rows[0][i].GetType();
            }

            return default(Type);
        }

        public object GetValue(int i)
        {
            return _rows[_currentRowNo][i];
        }

        public int GetValues(object[] values)
        {
            int coloumnCount = _rows[_currentRowNo].Length;
            Array.Copy(_rows[_currentRowNo], values, coloumnCount);
            return coloumnCount;
        }

        public int GetOrdinal(string name)
        {
            return _nameToOrdinal[name];
        }

        public bool GetBoolean(int i)
        {
            return (bool) _rows[_currentRowNo][i];
        }

        public byte GetByte(int i)
        {
            return (byte)_rows[_currentRowNo][i];
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            return (char)_rows[_currentRowNo][i];
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            return (Guid)_rows[_currentRowNo][i];
        }

        public short GetInt16(int i)
        {
            return (Int16)_rows[_currentRowNo][i];
        }

        public int GetInt32(int i)
        {
            return (Int32)_rows[_currentRowNo][i];
        }

        public long GetInt64(int i)
        {
            return (Int64)_rows[_currentRowNo][i];
        }

        public float GetFloat(int i)
        {
            return (float)_rows[_currentRowNo][i];
        }

        public double GetDouble(int i)
        {
            return (double)_rows[_currentRowNo][i];
        }

        public string GetString(int i)
        {
            return (string)_rows[_currentRowNo][i];
        }

        public decimal GetDecimal(int i)
        {
            return (decimal)_rows[_currentRowNo][i];
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)_rows[_currentRowNo][i];
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            return _rows[_currentRowNo][i] == DBNull.Value;
        }

        public int FieldCount
        {
            get { return _columnNames.Length; }
        }

        object IDataRecord.this[int i]
        {
            get { return _rows[_currentRowNo][i]; }
        }

        object IDataRecord.this[string name]
        {
            get { return _rows[_currentRowNo][GetOrdinal(name)]; }
        }

        public void Close()
        {
            _isClosed = true;
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            return false;
        }

        public bool Read()
        {
            _currentRowNo++;
            if (_currentRowNo > (_rows.Count - 1))
            {
                return false;
            }

            return true;
        }

        public int Depth
        {
            get { return 0; }
        }

        public bool IsClosed
        {
            get { return _isClosed; }
        }

        public int RecordsAffected
        {
            get { return _rowsAffected; }
        }
    }
}
