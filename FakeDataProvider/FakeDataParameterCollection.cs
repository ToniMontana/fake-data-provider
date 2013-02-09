using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FakeDataProvider
{
    public class FakeDataParameterCollection : IDataParameterCollection
    {
        private readonly object _syncRoot = null;
        private readonly List<IDbDataParameter> _parameters = new List<IDbDataParameter>();
 
        public IEnumerator GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            _parameters.ToArray().CopyTo(array, index);
        }

        public int Count
        {
            get { return _parameters.Count; }
        }

        public object SyncRoot
        {
            get { return _syncRoot; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Add(object value)
        {
            if(value is IDbDataParameter)
            {
                _parameters.Add(value as IDbDataParameter);
                return _parameters.Count - 1;
            }

            return -1;
        }

        public bool Contains(object value)
        {
            if (value is IDbDataParameter)
            {
                return _parameters.Contains(value as IDbDataParameter);
            }

            return false;
        }

        public void Clear()
        {
            _parameters.Clear();
        }

        public int IndexOf(object value)
        {
            if (value is IDbDataParameter)
            {
                return _parameters.IndexOf(value as IDbDataParameter);               
            }

            return -1;
        }

        public void Insert  (int index, object value)
        {
            if (value is IDbDataParameter)
            {
                _parameters.Insert(0, value as IDbDataParameter);
            }

            throw new NullReferenceException();
        }

        public void Remove(object value)
        {
            if (value is IDbDataParameter)
            {
                _parameters.Remove(value as IDbDataParameter);
            }

            throw new ArgumentOutOfRangeException();
        }

        public void RemoveAt(int index)
        {
            _parameters.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get { return _parameters[index]; }
            set { Insert(index, value); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool Contains(string parameterName)
        {
            return _parameters.Any(x => x.ParameterName == parameterName);
        }

        public int IndexOf(string parameterName)
        {
            return _parameters.FindIndex(x => x.ParameterName == parameterName);
        }

        public void RemoveAt(string parameterName)
        {
            _parameters.RemoveAt(IndexOf(parameterName));
        }

        object IDataParameterCollection.this[string parameterName]
        {
            get
            {
                return _parameters.First(x => x.ParameterName == parameterName);
            }
            set
            {
                if (value is IDbDataParameter)
                {
                    int index = IndexOf(parameterName);
                    _parameters[index] = value as IDbDataParameter;
                }

                throw new NullReferenceException();
            }
        }
    }
}
