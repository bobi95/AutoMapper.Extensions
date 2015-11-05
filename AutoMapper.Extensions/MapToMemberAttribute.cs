using System;
using System.Linq.Expressions;

namespace AutoMapper.Extensions
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MapToMemberAttribute : Attribute
    {
        public string MemberName { get; private set; }

        // This is a positional argument
        public MapToMemberAttribute(string memberName)
        {
            if (string.IsNullOrEmpty(memberName))
                throw new Exception("Member name must be set.");

            MemberName = memberName;
        }
    }
}
