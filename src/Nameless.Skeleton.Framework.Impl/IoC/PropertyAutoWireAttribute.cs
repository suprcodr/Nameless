using System;

namespace Nameless.Skeleton.Framework.IoC {

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class PropertyAutoWireAttribute : Attribute {
    }
}