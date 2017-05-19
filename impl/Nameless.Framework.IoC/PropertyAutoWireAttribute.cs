using System;

namespace Nameless.Framework.IoC {

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class PropertyAutoWireAttribute : Attribute {
    }
}