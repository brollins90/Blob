using System;
using System.Collections.Generic;
using System.Linq;

namespace BMonitor.Service.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<BindingDefinition> GetBindingDefinitionOf(this IEnumerable<Type> types, Type openGenericType)
        {
            return types.Select(type => new InterfaceTypeDefinition(type))
                .Where(d => d.ImplementsOpenGenericTypeOf(openGenericType))
                .Select(d => new BindingDefinition(d, openGenericType));
        }
        public static IEnumerable<BindingDefinition> GetDefinitionsWhereClosedGeneric(this IEnumerable<BindingDefinition> types)
        {
            return types.Where(x => !x.Implementation.ContainsGenericParameters);
        }

        public static bool IsOpenGeneric(this Type type, Type openGenericType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableFrom(openGenericType);
        }
    }

    public class BindingDefinition
    {
        public BindingDefinition(InterfaceTypeDefinition definition, Type openGenericType)
        {
            Implementation = definition.Implementation;
            Service = definition.GetService(openGenericType);
            GenericType = Service.GetGenericArguments()[0];
        }

        public Type Implementation { get; private set; }

        public Type Service { get; private set; }

        public Type GenericType { get; private set; }
    }

    public class InterfaceTypeDefinition
    {
        public InterfaceTypeDefinition(Type type)
        {
            Implementation = type;
            Interfaces = type.GetInterfaces();
        }

        /// <summary>
        /// The concrete implementation.
        /// </summary>
        public Type Implementation { get; private set; }

        /// <summary>
        /// The interfaces implemented by the implementation.
        /// </summary>
        public IEnumerable<Type> Interfaces { get; private set; }

        /// <summary>
        /// Returns a value indicating whether the implementation
        /// implements the specified open generic type.
        /// </summary>
        public bool ImplementsOpenGenericTypeOf(Type openGenericType)
        {
            return Interfaces.Any(i => i.IsOpenGeneric(openGenericType));
        }

        /// <summary>
        /// Returns the service type for the concrete implementation.
        /// </summary>
        public Type GetService(Type openGenericType)
        {
            return Interfaces.First(i => i.IsOpenGeneric(openGenericType))
                .GetGenericArguments()
                .Select(arguments => openGenericType.MakeGenericType(arguments))
                .First();
        }
    }

}
