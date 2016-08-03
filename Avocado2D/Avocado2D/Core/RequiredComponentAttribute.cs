using System;

namespace Avocado2D
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequiredComponentAttribute : Attribute
    {
        /// <summary>
        /// Gets the type of the component.
        /// </summary>
        public Type RequiredComponentType { get; }

        public RequiredComponentAttribute(Type cmpType)
        {
            RequiredComponentType = cmpType;
        }
    }
}