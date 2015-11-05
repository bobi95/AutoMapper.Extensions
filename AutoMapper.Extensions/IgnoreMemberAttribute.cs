using System;

namespace AutoMapper.Extensions
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class IgnoreMemberAttribute : Attribute
    {
        public string TargetMemberName { get; private set; }

        public bool IsTwoWay
        {
            get { return !string.IsNullOrEmpty(TargetMemberName); }
        }

        public IgnoreMemberAttribute()
        {
        }

        public IgnoreMemberAttribute(string targetMemberName)
        {
            TargetMemberName = targetMemberName;
        }
    }
}
