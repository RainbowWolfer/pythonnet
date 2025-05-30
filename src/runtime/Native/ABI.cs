namespace Python.Runtime.Native
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    internal static class ABI
    {
        public static int RefCountOffset { get; } = GetRefCountOffset();
        public static int ObjectHeadOffset => RefCountOffset;

        internal static void Initialize(Version version)
        {
            string offsetsClassSuffix = string.Format(CultureInfo.InvariantCulture,
                                                      "{0}{1}", version.Major, version.Minor);

            var thisAssembly = Assembly.GetExecutingAssembly();

            const string nativeTypeOffsetClassName = "Python.Runtime.NativeTypeOffset";
            string className = "Python.Runtime.TypeOffset" + offsetsClassSuffix;
            Type nativeOffsetsClass = thisAssembly.GetType(nativeTypeOffsetClassName, throwOnError: false);
            Type typeOffsetsClass =
                // Try platform native offsets first. It is only present when generated by setup.py
                nativeOffsetsClass ?? thisAssembly.GetType(className, throwOnError: false);
            if (typeOffsetsClass is null)
            {
                var types = thisAssembly.GetTypes().Select(type => type.Name).Where(name => name.StartsWith("TypeOffset"));
                string message = $"Searching for {className}, found {string.Join(",", types)}.";
                throw new NotSupportedException($"Python ABI v{version} is not supported: {message}");
            }

            var typeOffsets = (ITypeOffsets)Activator.CreateInstance(typeOffsetsClass);
            TypeOffset.Use(typeOffsets, nativeOffsetsClass == null ? ObjectHeadOffset : 0);
        }

        private static unsafe int GetRefCountOffset()
        {
            using var tempObject = Runtime.PyList_New(0);
            IntPtr* tempPtr = (IntPtr*)tempObject.DangerousGetAddress();
            int offset = 0;
            while (tempPtr[offset] != (IntPtr)1)
            {
                offset++;
                if (offset > 100)
                {
                    throw new InvalidProgramException("PyObject_HEAD could not be found withing reasonable distance from the start of PyObject");
                }
            }
            return offset * IntPtr.Size;
        }
    }
}
