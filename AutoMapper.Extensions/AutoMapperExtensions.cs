using System;
using System.Linq;
using System.Reflection;

namespace AutoMapper.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void AutomapNamespace(this Assembly assembly, string targetNamespace)
        {
            var classes =
                assembly.DefinedTypes
                .Where(type => type.Namespace == targetNamespace)
                .ToArray();

            for (int i = 0; i < classes.Length; i++)
            {
                var type = classes[i];
                var attr = type.GetCustomAttribute<MapToTypeAttribute>();

                if (attr != null)
                    Automap(type, attr);
            }
        }

        public static void Automap(this Type type)
        {
            var attr = type.GetCustomAttribute<MapToTypeAttribute>();

            if (attr == null)
                throw new Exception("Type does not have MapTo attribute.");

            Automap(type, attr);
        }

        private static void Automap(Type type, MapToTypeAttribute attr)
        {
            var targetType = attr.TargetType;

            var typeProps = type.GetProperties()
                .Where(p =>
                    p.GetCustomAttribute<MapToMemberAttribute>() != null ||
                    p.GetCustomAttribute<IgnoreMemberAttribute>() != null)
                .ToArray();

            bool bothways = attr.IsTwoWay;

            // type -> target
            var mappingExpression = Mapper.CreateMap(type, targetType);

            // target -> type
            var reverseExpression = bothways ? Mapper.CreateMap(targetType, type) : null;

            // type props -> target props
            for (int i = 0; i < typeProps.Length; i++)
            {
                var prop = typeProps[i];

                var ignoreAttr = prop.GetCustomAttribute<IgnoreMemberAttribute>();

                if (ignoreAttr != null)
                {
                    // TODO: Make it work
                    mappingExpression.ForSourceMember(prop.Name, opts => opts.Ignore());

                    // TODO: And this
                    if (bothways && ignoreAttr.IsTwoWay)
                        reverseExpression.ForSourceMember(ignoreAttr.TargetMemberName, opts => opts.Ignore());

                    continue;
                }

                var targetName = prop.GetCustomAttribute<MapToMemberAttribute>().MemberName;

                mappingExpression.ForMember(targetName, x => x.MapFrom(prop.Name));

                if (bothways)
                    reverseExpression.ForMember(prop.Name, x => x.MapFrom(targetName));
            }

            mappingExpression.IgnoreAllPropertiesWithAnInaccessibleSetter();

            if (bothways)
                reverseExpression.IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
