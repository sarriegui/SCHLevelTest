using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Modelo
{
    public class Modelo
    {
        private string _dataPath;

        public Modelo(string _dataPath)
        {
            this._dataPath = _dataPath;
        }
        public T Get<T>(string field, object value)
        {
            List<T> _list = LoadFile<T>();

            PropertyInfo _prop = typeof(T).GetProperty(field);

            if (_prop == null)
                throw new Exception("Field not in type");

            if (_list != null)
            {
                List<T> _lista =  _list.Where(_a => _prop.GetValue(_a, null).Equals(value)).Select(_a => _a).ToList();
                return _lista.FirstOrDefault();
            }
            else
                return default(T);
        }

        private List<T> LoadFile<T>()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            try
            {
                XDocument xdoc = XDocument.Load(string.Format(@"{0}\{1}.xml", _dataPath, typeof(T).Name));
                List<T> _list = new List<T>();

                var _auxList = xdoc.Descendants(typeof(T).Name).ToList();

                foreach (XElement _elem in _auxList)
                    _list.Add((T)XmlDeserializeFromString<T>(_elem.ToString(), typeof(T)));

                return _list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private T XmlDeserializeFromString<T>(string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            T result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }

        public List<T> GetAll<T>()
        {
            List<T> _list = LoadFile<T>();

            return (_list == null) ? null : _list;
        }

        public void Save<T>(List<T> _list)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, _list);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(string.Format(@"{0}\{1}.xml", _dataPath, typeof(T).Name));
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
