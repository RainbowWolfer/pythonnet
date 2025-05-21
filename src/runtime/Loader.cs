using System;

namespace Python.Runtime
{
    using static Runtime;

    [Obsolete("Only to be used from within Python")]
    internal static class Loader
    {
        public static unsafe int Initialize(IntPtr data, int size)
        {
            try
            {
                var dllPath = Encodings.UTF8.GetString((byte*)data.ToPointer(), size);

                if (!string.IsNullOrEmpty(dllPath))
                {
                    PythonDLL = dllPath;
                }
                else
                {
                    PythonDLL = null;
                }

                using var _ = Py.GIL();
                PythonEngine.InitExt();
            }
            catch (Exception exc)
            {
                Console.Error.Write(
                    $"Failed to initialize pythonnet: {exc}\n{exc.StackTrace}"
                );
                return 1;
            }

            return 0;
        }

        public static unsafe int Shutdown(IntPtr data, int size)
        {
            try
            {
                var command = Encodings.UTF8.GetString((byte*)data.ToPointer(), size);

                if (command == "full_shutdown")
                {
                    using var _ = Py.GIL();
                    PythonEngine.Shutdown();
                }
            }
            catch (Exception exc)
            {
                Console.Error.Write(
                    $"Failed to shutdown pythonnet: {exc}\n{exc.StackTrace}"
                );
                return 1;
            }

            return 0;
        }
    }
}
