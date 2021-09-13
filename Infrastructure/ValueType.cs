using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ddd.Infrastructure
{
    //Все Value-типы, согласно DDD, должны поддерживать семантику значений, то есть сравниваться по содержимому своих свойств.
    //Каждый раз реализовывать Equals, GetHashCode и ToString соответствующим образом — довольно муторное занятие.
    //Часто для этого создают базовый класс, наследование от которого реализует нужным образом все эти стандартные методы.
    //Это вам и предстоит сделать!

    //В рамках этого задания сравнивать Value-типы можно только по значению их публичных свойств, без учета значения полей.
    //Хотя как правильно это стоит делать на практике — вопрос дискуссионный и, скорее, предмет договорённостей в вашей команде.
    //В файле Infrastructure/ValueType.cs реализуйте класс ValueType так, чтобы проходили все тесты в файле ValueType_Tests.cs.
    //После решения этой задачи посмотрите подсказки!

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
                return (ElementwiseHashcode(typeProperties) * 397);
            }
        }

        // ReSharper disable once IdentifierTypo
        private int ElementwiseHashcode<T>(IEnumerable<T> items)
        {
            unchecked
            {
                return items.Select(t => t.GetHashCode()).Aggregate((res, next) => (res * 379) ^ next);
            }
        }

        public override string ToString()
        {
            var propsWithValues = typeProperties
                .Select(propInfo => Tuple.Create(propInfo.Name, propInfo.GetValue(this)?.ToString()))
                .OrderBy(tuple => tuple.Item1)
                .Select(tuple => string.Join(": ", tuple.Item1, tuple.Item2 ?? ""));
            return $"{typeName}({string.Join("; ", propsWithValues)})";
        }
    }
}