using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ddd.Infrastructure
{
    /// <summary>
    /// Базовый класс для всех Value типов.
    /// </summary>
    public class ValueType<T> where T : class
    {
        private static readonly PropertyInfo[] typeProperties;
        private static readonly string typeName;

        static ValueType()
        {
            var type = typeof(T);
            typeProperties = type.GetProperties();
            typeName = type.Name;
        }

        public override bool Equals(object obj) => Equals(obj as T);

        public bool Equals(T obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != this.GetType()) return false;

            PropertyInfo[] objProperties = obj.GetType().GetProperties();

            if (!typeProperties.SequenceEqual(objProperties)) return false;

            for (var i = 0; i < typeProperties.Length; i++)
            {
                var selfValue = typeProperties[i].GetValue(this);
                var objValue = objProperties[i].GetValue(obj);

                if (!object.Equals(selfValue, objValue))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var propsWithValues = GetPropsWithValues();

                var hash = 42;
                for (var i = 0; i < propsWithValues.Count; i++)
                {
                    hash = hash * 37 + propsWithValues[i].GetHashCode();
                }

                return hash;
            }
        }

        public override string ToString()
        {
            return $"{typeName}({string.Join("; ", GetPropsWithValues())})";
        }

        private List<string> GetPropsWithValues()
        {
            return typeProperties
                .Select(propInfo => Tuple.Create(propInfo.Name, propInfo.GetValue(this)?.ToString()))
                .OrderBy(tuple => tuple.Item1)
                .Select(tuple => string.Join(": ", tuple.Item1, tuple.Item2 ?? ""))
                .ToList();
        }
    }
}