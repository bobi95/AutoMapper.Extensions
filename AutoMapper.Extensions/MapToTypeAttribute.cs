using System;

namespace AutoMapper.Extensions
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class MapToTypeAttribute : Attribute
    {
        public MapToTypeAttribute(Type type)
        {
            if (type == null)
                throw new NullReferenceException("Target type cannot be null.");

            if (type.IsAbstract)
                throw new Exception("Target class cannot be abstract.");

            TargetType = type;
            IsTwoWay = true;
        }

        public bool IsTwoWay { get; set; }

        public Type TargetType { get; private set; }
    }
}
