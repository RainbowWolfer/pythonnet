namespace Python.Runtime
{
    using System;
    [AttributeUsage(AttributeTargets.Struct)]
    internal class NonCopyableAttribute : Attribute { }
}
