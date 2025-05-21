using System;

using Python.Runtime.Native;
using Python.Runtime.Platform;

namespace Python.Runtime;

public unsafe partial class Runtime
{
    internal class _Delegates
    {
        private readonly ILibraryLoader libraryLoader = LibraryLoader.Instance;
        private readonly string pythonDll;

        public _Delegates(string pythonDll)
        {
            this.pythonDll = pythonDll;

            Py_IncRef = (delegate* unmanaged[Cdecl]<BorrowedReference, void>)GetFunctionByName(nameof(Py_IncRef), GetUnmanagedDll(pythonDll));
            Py_DecRef = (delegate* unmanaged[Cdecl]<StolenReference, void>)GetFunctionByName(nameof(Py_DecRef), GetUnmanagedDll(pythonDll));
            Py_Initialize = (delegate* unmanaged[Cdecl]<void>)GetFunctionByName(nameof(Py_Initialize), GetUnmanagedDll(pythonDll));
            Py_InitializeEx = (delegate* unmanaged[Cdecl]<int, void>)GetFunctionByName(nameof(Py_InitializeEx), GetUnmanagedDll(pythonDll));
            Py_IsInitialized = (delegate* unmanaged[Cdecl]<int>)GetFunctionByName(nameof(Py_IsInitialized), GetUnmanagedDll(pythonDll));
            Py_Finalize = (delegate* unmanaged[Cdecl]<void>)GetFunctionByName(nameof(Py_Finalize), GetUnmanagedDll(pythonDll));
            Py_NewInterpreter = (delegate* unmanaged[Cdecl]<PyThreadState*>)GetFunctionByName(nameof(Py_NewInterpreter), GetUnmanagedDll(pythonDll));
            Py_EndInterpreter = (delegate* unmanaged[Cdecl]<PyThreadState*, void>)GetFunctionByName(nameof(Py_EndInterpreter), GetUnmanagedDll(pythonDll));
            PyThreadState_New = (delegate* unmanaged[Cdecl]<PyInterpreterState*, PyThreadState*>)GetFunctionByName(nameof(PyThreadState_New), GetUnmanagedDll(pythonDll));
            PyThreadState_Get = (delegate* unmanaged[Cdecl]<PyThreadState*>)GetFunctionByName(nameof(PyThreadState_Get), GetUnmanagedDll(pythonDll));
            try
            {
                // Up until Python 3.13, this function was private and named
                // slightly differently.
                PyThreadState_GetUnchecked = (delegate* unmanaged[Cdecl]<PyThreadState*>)GetFunctionByName("_PyThreadState_UncheckedGet", GetUnmanagedDll(pythonDll));
            }
            catch (MissingMethodException)
            {

                PyThreadState_GetUnchecked = (delegate* unmanaged[Cdecl]<PyThreadState*>)GetFunctionByName(nameof(PyThreadState_GetUnchecked), GetUnmanagedDll(pythonDll));
            }
            try
            {
                PyGILState_Check = (delegate* unmanaged[Cdecl]<int>)GetFunctionByName(nameof(PyGILState_Check), GetUnmanagedDll(pythonDll));
            }
            catch (MissingMethodException e)
            {
                throw new NotSupportedException(Util.MinimalPythonVersionRequired, innerException: e);
            }
            PyGILState_Ensure = (delegate* unmanaged[Cdecl]<PyGILState>)GetFunctionByName(nameof(PyGILState_Ensure), GetUnmanagedDll(pythonDll));
            PyGILState_Release = (delegate* unmanaged[Cdecl]<PyGILState, void>)GetFunctionByName(nameof(PyGILState_Release), GetUnmanagedDll(pythonDll));
            PyGILState_GetThisThreadState = (delegate* unmanaged[Cdecl]<PyThreadState*>)GetFunctionByName(nameof(PyGILState_GetThisThreadState), GetUnmanagedDll(pythonDll));
            PyEval_InitThreads = (delegate* unmanaged[Cdecl]<void>)GetFunctionByName(nameof(PyEval_InitThreads), GetUnmanagedDll(pythonDll));
            PyEval_ThreadsInitialized = (delegate* unmanaged[Cdecl]<int>)GetFunctionByName(nameof(PyEval_ThreadsInitialized), GetUnmanagedDll(pythonDll));
            PyEval_AcquireLock = (delegate* unmanaged[Cdecl]<void>)GetFunctionByName(nameof(PyEval_AcquireLock), GetUnmanagedDll(pythonDll));
            PyEval_ReleaseLock = (delegate* unmanaged[Cdecl]<void>)GetFunctionByName(nameof(PyEval_ReleaseLock), GetUnmanagedDll(pythonDll));
            PyEval_AcquireThread = (delegate* unmanaged[Cdecl]<PyThreadState*, void>)GetFunctionByName(nameof(PyEval_AcquireThread), GetUnmanagedDll(pythonDll));
            PyEval_ReleaseThread = (delegate* unmanaged[Cdecl]<PyThreadState*, void>)GetFunctionByName(nameof(PyEval_ReleaseThread), GetUnmanagedDll(pythonDll));
            PyEval_SaveThread = (delegate* unmanaged[Cdecl]<PyThreadState*>)GetFunctionByName(nameof(PyEval_SaveThread), GetUnmanagedDll(pythonDll));
            PyEval_RestoreThread = (delegate* unmanaged[Cdecl]<PyThreadState*, void>)GetFunctionByName(nameof(PyEval_RestoreThread), GetUnmanagedDll(pythonDll));
            PyEval_GetBuiltins = (delegate* unmanaged[Cdecl]<BorrowedReference>)GetFunctionByName(nameof(PyEval_GetBuiltins), GetUnmanagedDll(pythonDll));
            PyEval_GetGlobals = (delegate* unmanaged[Cdecl]<BorrowedReference>)GetFunctionByName(nameof(PyEval_GetGlobals), GetUnmanagedDll(pythonDll));
            PyEval_GetLocals = (delegate* unmanaged[Cdecl]<BorrowedReference>)GetFunctionByName(nameof(PyEval_GetLocals), GetUnmanagedDll(pythonDll));
            Py_GetProgramName = (delegate* unmanaged[Cdecl]<IntPtr>)GetFunctionByName(nameof(Py_GetProgramName), GetUnmanagedDll(pythonDll));
            Py_SetProgramName = (delegate* unmanaged[Cdecl]<IntPtr, void>)GetFunctionByName(nameof(Py_SetProgramName), GetUnmanagedDll(pythonDll));
            Py_GetPythonHome = (delegate* unmanaged[Cdecl]<IntPtr>)GetFunctionByName(nameof(Py_GetPythonHome), GetUnmanagedDll(pythonDll));
            Py_SetPythonHome = (delegate* unmanaged[Cdecl]<IntPtr, void>)GetFunctionByName(nameof(Py_SetPythonHome), GetUnmanagedDll(pythonDll));
            Py_GetPath = (delegate* unmanaged[Cdecl]<IntPtr>)GetFunctionByName(nameof(Py_GetPath), GetUnmanagedDll(pythonDll));
            Py_SetPath = (delegate* unmanaged[Cdecl]<IntPtr, void>)GetFunctionByName(nameof(Py_SetPath), GetUnmanagedDll(pythonDll));
            Py_GetVersion = (delegate* unmanaged[Cdecl]<IntPtr>)GetFunctionByName(nameof(Py_GetVersion), GetUnmanagedDll(pythonDll));
            Py_GetPlatform = (delegate* unmanaged[Cdecl]<IntPtr>)GetFunctionByName(nameof(Py_GetPlatform), GetUnmanagedDll(pythonDll));
            Py_GetCopyright = (delegate* unmanaged[Cdecl]<IntPtr>)GetFunctionByName(nameof(Py_GetCopyright), GetUnmanagedDll(pythonDll));
            Py_GetCompiler = (delegate* unmanaged[Cdecl]<IntPtr>)GetFunctionByName(nameof(Py_GetCompiler), GetUnmanagedDll(pythonDll));
            Py_GetBuildInfo = (delegate* unmanaged[Cdecl]<IntPtr>)GetFunctionByName(nameof(Py_GetBuildInfo), GetUnmanagedDll(pythonDll));
            PyRun_SimpleStringFlags = (delegate* unmanaged[Cdecl]<StrPtr, in PyCompilerFlags, int>)GetFunctionByName(nameof(PyRun_SimpleStringFlags), GetUnmanagedDll(pythonDll));
            PyRun_StringFlags = (delegate* unmanaged[Cdecl]<StrPtr, RunFlagType, BorrowedReference, BorrowedReference, in PyCompilerFlags, NewReference>)GetFunctionByName(nameof(PyRun_StringFlags), GetUnmanagedDll(pythonDll));
            PyEval_EvalCode = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyEval_EvalCode), GetUnmanagedDll(pythonDll));
            Py_CompileStringObject = (delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference, int, in PyCompilerFlags, int, NewReference>)GetFunctionByName(nameof(Py_CompileStringObject), GetUnmanagedDll(pythonDll));
            PyImport_ExecCodeModule = (delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyImport_ExecCodeModule), GetUnmanagedDll(pythonDll));
            PyObject_HasAttrString = (delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, int>)GetFunctionByName(nameof(PyObject_HasAttrString), GetUnmanagedDll(pythonDll));
            PyObject_GetAttrString = (delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, NewReference>)GetFunctionByName(nameof(PyObject_GetAttrString), GetUnmanagedDll(pythonDll));
            PyObject_SetAttrString = (delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, BorrowedReference, int>)GetFunctionByName(nameof(PyObject_SetAttrString), GetUnmanagedDll(pythonDll));
            PyObject_HasAttr = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyObject_HasAttr), GetUnmanagedDll(pythonDll));
            PyObject_GetAttr = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_GetAttr), GetUnmanagedDll(pythonDll));
            PyObject_SetAttr = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyObject_SetAttr), GetUnmanagedDll(pythonDll));
            PyObject_GetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_GetItem), GetUnmanagedDll(pythonDll));
            PyObject_SetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyObject_SetItem), GetUnmanagedDll(pythonDll));
            PyObject_DelItem = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyObject_DelItem), GetUnmanagedDll(pythonDll));
            PyObject_GetIter = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_GetIter), GetUnmanagedDll(pythonDll));
            PyObject_Call = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_Call), GetUnmanagedDll(pythonDll));
            PyObject_CallObject = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_CallObject), GetUnmanagedDll(pythonDll));
            PyObject_RichCompareBool = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int, int>)GetFunctionByName(nameof(PyObject_RichCompareBool), GetUnmanagedDll(pythonDll));
            PyObject_IsInstance = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyObject_IsInstance), GetUnmanagedDll(pythonDll));
            PyObject_IsSubclass = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyObject_IsSubclass), GetUnmanagedDll(pythonDll));
            PyObject_ClearWeakRefs = (delegate* unmanaged[Cdecl]<BorrowedReference, void>)GetFunctionByName(nameof(PyObject_ClearWeakRefs), GetUnmanagedDll(pythonDll));
            PyCallable_Check = (delegate* unmanaged[Cdecl]<BorrowedReference, int>)GetFunctionByName(nameof(PyCallable_Check), GetUnmanagedDll(pythonDll));
            PyObject_IsTrue = (delegate* unmanaged[Cdecl]<BorrowedReference, int>)GetFunctionByName(nameof(PyObject_IsTrue), GetUnmanagedDll(pythonDll));
            PyObject_Not = (delegate* unmanaged[Cdecl]<BorrowedReference, int>)GetFunctionByName(nameof(PyObject_Not), GetUnmanagedDll(pythonDll));
            PyObject_Size = (delegate* unmanaged[Cdecl]<BorrowedReference, nint>)GetFunctionByName("PyObject_Size", GetUnmanagedDll(pythonDll));
            PyObject_Hash = (delegate* unmanaged[Cdecl]<BorrowedReference, nint>)GetFunctionByName(nameof(PyObject_Hash), GetUnmanagedDll(pythonDll));
            PyObject_Repr = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_Repr), GetUnmanagedDll(pythonDll));
            PyObject_Str = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_Str), GetUnmanagedDll(pythonDll));
            PyObject_Type = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_Type), GetUnmanagedDll(pythonDll));
            PyObject_Dir = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_Dir), GetUnmanagedDll(pythonDll));
            PyObject_GetBuffer = (delegate* unmanaged[Cdecl]<BorrowedReference, out Py_buffer, int, int>)GetFunctionByName(nameof(PyObject_GetBuffer), GetUnmanagedDll(pythonDll));
            PyBuffer_Release = (delegate* unmanaged[Cdecl]<ref Py_buffer, void>)GetFunctionByName(nameof(PyBuffer_Release), GetUnmanagedDll(pythonDll));
            try
            {
                PyBuffer_SizeFromFormat = (delegate* unmanaged[Cdecl]<StrPtr, IntPtr>)GetFunctionByName(nameof(PyBuffer_SizeFromFormat), GetUnmanagedDll(pythonDll));
            }
            catch (MissingMethodException)
            {
                // only in 3.9+
            }
            PyBuffer_IsContiguous = (delegate* unmanaged[Cdecl]<ref Py_buffer, char, int>)GetFunctionByName(nameof(PyBuffer_IsContiguous), GetUnmanagedDll(pythonDll));
            PyBuffer_GetPointer = (delegate* unmanaged[Cdecl]<ref Py_buffer, nint[], IntPtr>)GetFunctionByName(nameof(PyBuffer_GetPointer), GetUnmanagedDll(pythonDll));
            PyBuffer_FromContiguous = (delegate* unmanaged[Cdecl]<ref Py_buffer, IntPtr, IntPtr, char, int>)GetFunctionByName(nameof(PyBuffer_FromContiguous), GetUnmanagedDll(pythonDll));
            PyBuffer_ToContiguous = (delegate* unmanaged[Cdecl]<IntPtr, ref Py_buffer, IntPtr, char, int>)GetFunctionByName(nameof(PyBuffer_ToContiguous), GetUnmanagedDll(pythonDll));
            PyBuffer_FillContiguousStrides = (delegate* unmanaged[Cdecl]<int, IntPtr, IntPtr, int, char, void>)GetFunctionByName(nameof(PyBuffer_FillContiguousStrides), GetUnmanagedDll(pythonDll));
            PyBuffer_FillInfo = (delegate* unmanaged[Cdecl]<ref Py_buffer, BorrowedReference, IntPtr, IntPtr, int, int, int>)GetFunctionByName(nameof(PyBuffer_FillInfo), GetUnmanagedDll(pythonDll));
            PyNumber_Long = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Long), GetUnmanagedDll(pythonDll));
            PyNumber_Float = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Float), GetUnmanagedDll(pythonDll));
            PyNumber_Check = (delegate* unmanaged[Cdecl]<BorrowedReference, bool>)GetFunctionByName(nameof(PyNumber_Check), GetUnmanagedDll(pythonDll));
            PyLong_FromLongLong = (delegate* unmanaged[Cdecl]<long, NewReference>)GetFunctionByName(nameof(PyLong_FromLongLong), GetUnmanagedDll(pythonDll));
            PyLong_FromUnsignedLongLong = (delegate* unmanaged[Cdecl]<ulong, NewReference>)GetFunctionByName(nameof(PyLong_FromUnsignedLongLong), GetUnmanagedDll(pythonDll));
            PyLong_FromString = (delegate* unmanaged[Cdecl]<StrPtr, IntPtr, int, NewReference>)GetFunctionByName(nameof(PyLong_FromString), GetUnmanagedDll(pythonDll));
            PyLong_AsLongLong = (delegate* unmanaged[Cdecl]<BorrowedReference, long>)GetFunctionByName(nameof(PyLong_AsLongLong), GetUnmanagedDll(pythonDll));
            PyLong_AsUnsignedLongLong = (delegate* unmanaged[Cdecl]<BorrowedReference, ulong>)GetFunctionByName(nameof(PyLong_AsUnsignedLongLong), GetUnmanagedDll(pythonDll));
            PyLong_FromVoidPtr = (delegate* unmanaged[Cdecl]<IntPtr, NewReference>)GetFunctionByName(nameof(PyLong_FromVoidPtr), GetUnmanagedDll(pythonDll));
            PyLong_AsVoidPtr = (delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr>)GetFunctionByName(nameof(PyLong_AsVoidPtr), GetUnmanagedDll(pythonDll));
            PyFloat_FromDouble = (delegate* unmanaged[Cdecl]<double, NewReference>)GetFunctionByName(nameof(PyFloat_FromDouble), GetUnmanagedDll(pythonDll));
            PyFloat_FromString = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyFloat_FromString), GetUnmanagedDll(pythonDll));
            PyFloat_AsDouble = (delegate* unmanaged[Cdecl]<BorrowedReference, double>)GetFunctionByName(nameof(PyFloat_AsDouble), GetUnmanagedDll(pythonDll));
            PyNumber_Add = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Add), GetUnmanagedDll(pythonDll));
            PyNumber_Subtract = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Subtract), GetUnmanagedDll(pythonDll));
            PyNumber_Multiply = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Multiply), GetUnmanagedDll(pythonDll));
            PyNumber_TrueDivide = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_TrueDivide), GetUnmanagedDll(pythonDll));
            PyNumber_And = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_And), GetUnmanagedDll(pythonDll));
            PyNumber_Xor = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Xor), GetUnmanagedDll(pythonDll));
            PyNumber_Or = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Or), GetUnmanagedDll(pythonDll));
            PyNumber_Lshift = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Lshift), GetUnmanagedDll(pythonDll));
            PyNumber_Rshift = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Rshift), GetUnmanagedDll(pythonDll));
            PyNumber_Power = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Power), GetUnmanagedDll(pythonDll));
            PyNumber_Remainder = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Remainder), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceAdd = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceAdd), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceSubtract = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceSubtract), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceMultiply = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceMultiply), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceTrueDivide = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceTrueDivide), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceAnd = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceAnd), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceXor = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceXor), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceOr = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceOr), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceLshift = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceLshift), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceRshift = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceRshift), GetUnmanagedDll(pythonDll));
            PyNumber_InPlacePower = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlacePower), GetUnmanagedDll(pythonDll));
            PyNumber_InPlaceRemainder = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_InPlaceRemainder), GetUnmanagedDll(pythonDll));
            PyNumber_Negative = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Negative), GetUnmanagedDll(pythonDll));
            PyNumber_Positive = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Positive), GetUnmanagedDll(pythonDll));
            PyNumber_Invert = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyNumber_Invert), GetUnmanagedDll(pythonDll));
            PySequence_Check = (delegate* unmanaged[Cdecl]<BorrowedReference, bool>)GetFunctionByName(nameof(PySequence_Check), GetUnmanagedDll(pythonDll));
            PySequence_GetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, NewReference>)GetFunctionByName(nameof(PySequence_GetItem), GetUnmanagedDll(pythonDll));
            PySequence_SetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, BorrowedReference, int>)GetFunctionByName(nameof(PySequence_SetItem), GetUnmanagedDll(pythonDll));
            PySequence_DelItem = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, int>)GetFunctionByName(nameof(PySequence_DelItem), GetUnmanagedDll(pythonDll));
            PySequence_GetSlice = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, NewReference>)GetFunctionByName(nameof(PySequence_GetSlice), GetUnmanagedDll(pythonDll));
            PySequence_SetSlice = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, BorrowedReference, int>)GetFunctionByName(nameof(PySequence_SetSlice), GetUnmanagedDll(pythonDll));
            PySequence_DelSlice = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, int>)GetFunctionByName(nameof(PySequence_DelSlice), GetUnmanagedDll(pythonDll));
            PySequence_Size = (delegate* unmanaged[Cdecl]<BorrowedReference, nint>)GetFunctionByName(nameof(PySequence_Size), GetUnmanagedDll(pythonDll));
            PySequence_Contains = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PySequence_Contains), GetUnmanagedDll(pythonDll));
            PySequence_Concat = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PySequence_Concat), GetUnmanagedDll(pythonDll));
            PySequence_Repeat = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, NewReference>)GetFunctionByName(nameof(PySequence_Repeat), GetUnmanagedDll(pythonDll));
            PySequence_Index = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, nint>)GetFunctionByName(nameof(PySequence_Index), GetUnmanagedDll(pythonDll));
            PySequence_Count = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, nint>)GetFunctionByName(nameof(PySequence_Count), GetUnmanagedDll(pythonDll));
            PySequence_Tuple = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PySequence_Tuple), GetUnmanagedDll(pythonDll));
            PySequence_List = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PySequence_List), GetUnmanagedDll(pythonDll));
            PyBytes_AsString = (delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr>)GetFunctionByName(nameof(PyBytes_AsString), GetUnmanagedDll(pythonDll));
            PyBytes_FromString = (delegate* unmanaged[Cdecl]<IntPtr, NewReference>)GetFunctionByName(nameof(PyBytes_FromString), GetUnmanagedDll(pythonDll));
            PyByteArray_FromStringAndSize = (delegate* unmanaged[Cdecl]<IntPtr, nint, NewReference>)GetFunctionByName(nameof(PyByteArray_FromStringAndSize), GetUnmanagedDll(pythonDll));
            PyBytes_Size = (delegate* unmanaged[Cdecl]<BorrowedReference, nint>)GetFunctionByName(nameof(PyBytes_Size), GetUnmanagedDll(pythonDll));
            PyUnicode_AsUTF8 = (delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr>)GetFunctionByName(nameof(PyUnicode_AsUTF8), GetUnmanagedDll(pythonDll));
            PyUnicode_DecodeUTF16 = (delegate* unmanaged[Cdecl]<IntPtr, nint, IntPtr, IntPtr, NewReference>)GetFunctionByName(nameof(PyUnicode_DecodeUTF16), GetUnmanagedDll(pythonDll));
            PyUnicode_GetLength = (delegate* unmanaged[Cdecl]<BorrowedReference, nint>)GetFunctionByName(nameof(PyUnicode_GetLength), GetUnmanagedDll(pythonDll));
            PyUnicode_AsUTF16String = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyUnicode_AsUTF16String), GetUnmanagedDll(pythonDll));
            PyUnicode_ReadChar = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, int>)GetFunctionByName(nameof(PyUnicode_ReadChar), GetUnmanagedDll(pythonDll));
            PyUnicode_FromOrdinal = (delegate* unmanaged[Cdecl]<int, NewReference>)GetFunctionByName(nameof(PyUnicode_FromOrdinal), GetUnmanagedDll(pythonDll));
            PyUnicode_InternFromString = (delegate* unmanaged[Cdecl]<StrPtr, NewReference>)GetFunctionByName(nameof(PyUnicode_InternFromString), GetUnmanagedDll(pythonDll));
            PyUnicode_Compare = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyUnicode_Compare), GetUnmanagedDll(pythonDll));
            PyDict_New = (delegate* unmanaged[Cdecl]<NewReference>)GetFunctionByName(nameof(PyDict_New), GetUnmanagedDll(pythonDll));
            PyDict_GetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference>)GetFunctionByName(nameof(PyDict_GetItem), GetUnmanagedDll(pythonDll));
            PyDict_GetItemString = (delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, BorrowedReference>)GetFunctionByName(nameof(PyDict_GetItemString), GetUnmanagedDll(pythonDll));
            PyDict_SetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyDict_SetItem), GetUnmanagedDll(pythonDll));
            PyDict_SetItemString = (delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, BorrowedReference, int>)GetFunctionByName(nameof(PyDict_SetItemString), GetUnmanagedDll(pythonDll));
            PyDict_DelItem = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyDict_DelItem), GetUnmanagedDll(pythonDll));
            PyDict_DelItemString = (delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, int>)GetFunctionByName(nameof(PyDict_DelItemString), GetUnmanagedDll(pythonDll));
            PyMapping_HasKey = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyMapping_HasKey), GetUnmanagedDll(pythonDll));
            PyDict_Keys = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyDict_Keys), GetUnmanagedDll(pythonDll));
            PyDict_Values = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyDict_Values), GetUnmanagedDll(pythonDll));
            PyDict_Items = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyDict_Items), GetUnmanagedDll(pythonDll));
            PyDict_Copy = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyDict_Copy), GetUnmanagedDll(pythonDll));
            PyDict_Update = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyDict_Update), GetUnmanagedDll(pythonDll));
            PyDict_Clear = (delegate* unmanaged[Cdecl]<BorrowedReference, void>)GetFunctionByName(nameof(PyDict_Clear), GetUnmanagedDll(pythonDll));
            PyDict_Size = (delegate* unmanaged[Cdecl]<BorrowedReference, nint>)GetFunctionByName(nameof(PyDict_Size), GetUnmanagedDll(pythonDll));
            PySet_New = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PySet_New), GetUnmanagedDll(pythonDll));
            PySet_Add = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PySet_Add), GetUnmanagedDll(pythonDll));
            PySet_Contains = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PySet_Contains), GetUnmanagedDll(pythonDll));
            PyList_New = (delegate* unmanaged[Cdecl]<nint, NewReference>)GetFunctionByName(nameof(PyList_New), GetUnmanagedDll(pythonDll));
            PyList_GetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr, BorrowedReference>)GetFunctionByName(nameof(PyList_GetItem), GetUnmanagedDll(pythonDll));
            PyList_SetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, StolenReference, int>)GetFunctionByName(nameof(PyList_SetItem), GetUnmanagedDll(pythonDll));
            PyList_Insert = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, BorrowedReference, int>)GetFunctionByName(nameof(PyList_Insert), GetUnmanagedDll(pythonDll));
            PyList_Append = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyList_Append), GetUnmanagedDll(pythonDll));
            PyList_Reverse = (delegate* unmanaged[Cdecl]<BorrowedReference, int>)GetFunctionByName(nameof(PyList_Reverse), GetUnmanagedDll(pythonDll));
            PyList_Sort = (delegate* unmanaged[Cdecl]<BorrowedReference, int>)GetFunctionByName(nameof(PyList_Sort), GetUnmanagedDll(pythonDll));
            PyList_GetSlice = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, NewReference>)GetFunctionByName(nameof(PyList_GetSlice), GetUnmanagedDll(pythonDll));
            PyList_SetSlice = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, BorrowedReference, int>)GetFunctionByName(nameof(PyList_SetSlice), GetUnmanagedDll(pythonDll));
            PyList_Size = (delegate* unmanaged[Cdecl]<BorrowedReference, nint>)GetFunctionByName(nameof(PyList_Size), GetUnmanagedDll(pythonDll));
            PyTuple_New = (delegate* unmanaged[Cdecl]<nint, NewReference>)GetFunctionByName(nameof(PyTuple_New), GetUnmanagedDll(pythonDll));
            PyTuple_GetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr, BorrowedReference>)GetFunctionByName(nameof(PyTuple_GetItem), GetUnmanagedDll(pythonDll));
            PyTuple_SetItem = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, StolenReference, int>)GetFunctionByName(nameof(PyTuple_SetItem), GetUnmanagedDll(pythonDll));
            PyTuple_GetSlice = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, NewReference>)GetFunctionByName(nameof(PyTuple_GetSlice), GetUnmanagedDll(pythonDll));
            PyTuple_Size = (delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr>)GetFunctionByName(nameof(PyTuple_Size), GetUnmanagedDll(pythonDll));
            try
            {
                PyIter_Check = (delegate* unmanaged[Cdecl]<BorrowedReference, int>)GetFunctionByName(nameof(PyIter_Check), GetUnmanagedDll(pythonDll));
            }
            catch (MissingMethodException) { }
            PyIter_Next = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyIter_Next), GetUnmanagedDll(pythonDll));
            PyModule_New = (delegate* unmanaged[Cdecl]<StrPtr, NewReference>)GetFunctionByName(nameof(PyModule_New), GetUnmanagedDll(pythonDll));
            PyModule_GetDict = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference>)GetFunctionByName(nameof(PyModule_GetDict), GetUnmanagedDll(pythonDll));
            PyModule_AddObject = (delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, IntPtr, int>)GetFunctionByName(nameof(PyModule_AddObject), GetUnmanagedDll(pythonDll));
            PyImport_Import = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyImport_Import), GetUnmanagedDll(pythonDll));
            PyImport_ImportModule = (delegate* unmanaged[Cdecl]<StrPtr, NewReference>)GetFunctionByName(nameof(PyImport_ImportModule), GetUnmanagedDll(pythonDll));
            PyImport_ReloadModule = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyImport_ReloadModule), GetUnmanagedDll(pythonDll));
            PyImport_AddModule = (delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference>)GetFunctionByName(nameof(PyImport_AddModule), GetUnmanagedDll(pythonDll));
            PyImport_GetModuleDict = (delegate* unmanaged[Cdecl]<BorrowedReference>)GetFunctionByName(nameof(PyImport_GetModuleDict), GetUnmanagedDll(pythonDll));
            PySys_SetArgvEx = (delegate* unmanaged[Cdecl]<int, IntPtr, int, void>)GetFunctionByName(nameof(PySys_SetArgvEx), GetUnmanagedDll(pythonDll));
            PySys_GetObject = (delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference>)GetFunctionByName(nameof(PySys_GetObject), GetUnmanagedDll(pythonDll));
            PySys_SetObject = (delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference, int>)GetFunctionByName(nameof(PySys_SetObject), GetUnmanagedDll(pythonDll));
            PyType_Modified = (delegate* unmanaged[Cdecl]<BorrowedReference, void>)GetFunctionByName(nameof(PyType_Modified), GetUnmanagedDll(pythonDll));
            PyType_IsSubtype = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, bool>)GetFunctionByName(nameof(PyType_IsSubtype), GetUnmanagedDll(pythonDll));
            PyType_GenericNew = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyType_GenericNew), GetUnmanagedDll(pythonDll));
            PyType_GenericAlloc = (delegate* unmanaged[Cdecl]<BorrowedReference, nint, NewReference>)GetFunctionByName(nameof(PyType_GenericAlloc), GetUnmanagedDll(pythonDll));
            PyType_Ready = (delegate* unmanaged[Cdecl]<BorrowedReference, int>)GetFunctionByName(nameof(PyType_Ready), GetUnmanagedDll(pythonDll));
            _PyType_Lookup = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference>)GetFunctionByName(nameof(_PyType_Lookup), GetUnmanagedDll(pythonDll));
            PyObject_GenericGetAttr = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyObject_GenericGetAttr), GetUnmanagedDll(pythonDll));
            PyObject_GenericGetDict = (delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr, NewReference>)GetFunctionByName(nameof(PyObject_GenericGetDict), GetUnmanagedDll(PythonDLL));
            PyObject_GenericSetAttr = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyObject_GenericSetAttr), GetUnmanagedDll(pythonDll));
            PyObject_GC_Del = (delegate* unmanaged[Cdecl]<StolenReference, void>)GetFunctionByName(nameof(PyObject_GC_Del), GetUnmanagedDll(pythonDll));
            try
            {
                PyObject_GC_IsTracked = (delegate* unmanaged[Cdecl]<BorrowedReference, int>)GetFunctionByName(nameof(PyObject_GC_IsTracked), GetUnmanagedDll(pythonDll));
            }
            catch (MissingMethodException) { }
            PyObject_GC_Track = (delegate* unmanaged[Cdecl]<BorrowedReference, void>)GetFunctionByName(nameof(PyObject_GC_Track), GetUnmanagedDll(pythonDll));
            PyObject_GC_UnTrack = (delegate* unmanaged[Cdecl]<BorrowedReference, void>)GetFunctionByName(nameof(PyObject_GC_UnTrack), GetUnmanagedDll(pythonDll));
            _PyObject_Dump = (delegate* unmanaged[Cdecl]<BorrowedReference, void>)GetFunctionByName(nameof(_PyObject_Dump), GetUnmanagedDll(pythonDll));
            PyMem_Malloc = (delegate* unmanaged[Cdecl]<IntPtr, IntPtr>)GetFunctionByName(nameof(PyMem_Malloc), GetUnmanagedDll(pythonDll));
            PyMem_Realloc = (delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr>)GetFunctionByName(nameof(PyMem_Realloc), GetUnmanagedDll(pythonDll));
            PyMem_Free = (delegate* unmanaged[Cdecl]<IntPtr, void>)GetFunctionByName(nameof(PyMem_Free), GetUnmanagedDll(pythonDll));
            PyErr_SetString = (delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, void>)GetFunctionByName(nameof(PyErr_SetString), GetUnmanagedDll(pythonDll));
            PyErr_SetObject = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, void>)GetFunctionByName(nameof(PyErr_SetObject), GetUnmanagedDll(pythonDll));
            PyErr_ExceptionMatches = (delegate* unmanaged[Cdecl]<BorrowedReference, int>)GetFunctionByName(nameof(PyErr_ExceptionMatches), GetUnmanagedDll(pythonDll));
            PyErr_GivenExceptionMatches = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyErr_GivenExceptionMatches), GetUnmanagedDll(pythonDll));
            PyErr_NormalizeException = (delegate* unmanaged[Cdecl]<ref NewReference, ref NewReference, ref NewReference, void>)GetFunctionByName(nameof(PyErr_NormalizeException), GetUnmanagedDll(pythonDll));
            PyErr_Occurred = (delegate* unmanaged[Cdecl]<BorrowedReference>)GetFunctionByName(nameof(PyErr_Occurred), GetUnmanagedDll(pythonDll));
            PyErr_Fetch = (delegate* unmanaged[Cdecl]<out NewReference, out NewReference, out NewReference, void>)GetFunctionByName(nameof(PyErr_Fetch), GetUnmanagedDll(pythonDll));
            PyErr_Restore = (delegate* unmanaged[Cdecl]<StolenReference, StolenReference, StolenReference, void>)GetFunctionByName(nameof(PyErr_Restore), GetUnmanagedDll(pythonDll));
            PyErr_Clear = (delegate* unmanaged[Cdecl]<void>)GetFunctionByName(nameof(PyErr_Clear), GetUnmanagedDll(pythonDll));
            PyErr_Print = (delegate* unmanaged[Cdecl]<void>)GetFunctionByName(nameof(PyErr_Print), GetUnmanagedDll(pythonDll));
            PyCell_Get = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyCell_Get), GetUnmanagedDll(pythonDll));
            PyCell_Set = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyCell_Set), GetUnmanagedDll(pythonDll));
            PyGC_Collect = (delegate* unmanaged[Cdecl]<nint>)GetFunctionByName(nameof(PyGC_Collect), GetUnmanagedDll(pythonDll));
            PyCapsule_New = (delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, NewReference>)GetFunctionByName(nameof(PyCapsule_New), GetUnmanagedDll(pythonDll));
            PyCapsule_GetPointer = (delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr, IntPtr>)GetFunctionByName(nameof(PyCapsule_GetPointer), GetUnmanagedDll(pythonDll));
            PyCapsule_SetPointer = (delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr, int>)GetFunctionByName(nameof(PyCapsule_SetPointer), GetUnmanagedDll(pythonDll));
            PyLong_AsUnsignedSize_t = (delegate* unmanaged[Cdecl]<BorrowedReference, nuint>)GetFunctionByName("PyLong_AsSize_t", GetUnmanagedDll(pythonDll));
            PyLong_AsSignedSize_t = (delegate* unmanaged[Cdecl]<BorrowedReference, nint>)GetFunctionByName("PyLong_AsSsize_t", GetUnmanagedDll(pythonDll));
            PyDict_GetItemWithError = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference>)GetFunctionByName(nameof(PyDict_GetItemWithError), GetUnmanagedDll(pythonDll));
            PyException_GetCause = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyException_GetCause), GetUnmanagedDll(pythonDll));
            PyException_GetTraceback = (delegate* unmanaged[Cdecl]<BorrowedReference, NewReference>)GetFunctionByName(nameof(PyException_GetTraceback), GetUnmanagedDll(pythonDll));
            PyException_SetCause = (delegate* unmanaged[Cdecl]<BorrowedReference, StolenReference, void>)GetFunctionByName(nameof(PyException_SetCause), GetUnmanagedDll(pythonDll));
            PyException_SetTraceback = (delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int>)GetFunctionByName(nameof(PyException_SetTraceback), GetUnmanagedDll(pythonDll));
            PyThreadState_SetAsyncExcLLP64 = (delegate* unmanaged[Cdecl]<uint, BorrowedReference, int>)GetFunctionByName("PyThreadState_SetAsyncExc", GetUnmanagedDll(pythonDll));
            PyThreadState_SetAsyncExcLP64 = (delegate* unmanaged[Cdecl]<ulong, BorrowedReference, int>)GetFunctionByName("PyThreadState_SetAsyncExc", GetUnmanagedDll(pythonDll));
            PyType_GetSlot = (delegate* unmanaged[Cdecl]<BorrowedReference, TypeSlotID, IntPtr>)GetFunctionByName(nameof(PyType_GetSlot), GetUnmanagedDll(pythonDll));
            PyType_FromSpecWithBases = (delegate* unmanaged[Cdecl]<in NativeTypeSpec, BorrowedReference, NewReference>)GetFunctionByName(nameof(PyType_FromSpecWithBases), GetUnmanagedDll(PythonDLL));

            try
            {
                _Py_NewReference = (delegate* unmanaged[Cdecl]<BorrowedReference, void>)GetFunctionByName(nameof(_Py_NewReference), GetUnmanagedDll(pythonDll));
            }
            catch (MissingMethodException) { }
            try
            {
                _Py_IsFinalizing = (delegate* unmanaged[Cdecl]<int>)GetFunctionByName(nameof(_Py_IsFinalizing), GetUnmanagedDll(pythonDll));
            }
            catch (MissingMethodException) { }

            PyType_Type = GetFunctionByName(nameof(PyType_Type), GetUnmanagedDll(pythonDll));
            Py_NoSiteFlag = (int*)GetFunctionByName(nameof(Py_NoSiteFlag), GetUnmanagedDll(pythonDll));
        }


        private global::System.IntPtr GetUnmanagedDll(string? libraryName)
        {
            if (libraryName is null)
            {
                return IntPtr.Zero;
            }
            //Debug.WriteLine($"LOADING : {libraryName} !!!");
            //try
            //{
            return libraryLoader.Load(libraryName);
            //}
            //catch (Exception ex)
            //{
            //    return IntPtr.Zero;
            //}
        }

        private global::System.IntPtr GetFunctionByName(string functionName, global::System.IntPtr libraryHandle)
        {
            try
            {
                return libraryLoader.GetFunction(libraryHandle, functionName);
            }
            catch (MissingMethodException e) when (libraryHandle == IntPtr.Zero)
            {
                throw new BadPythonDllException(
                    "Runtime.PythonDLL was not set or does not point to a supported Python runtime DLL." +
                    " See https://github.com/pythonnet/pythonnet#embedding-python-in-net",
                    e);
            }
        }

        internal delegate* unmanaged[Cdecl]<BorrowedReference, void> Py_IncRef { get; }
        internal delegate* unmanaged[Cdecl]<StolenReference, void> Py_DecRef { get; }
        internal delegate* unmanaged[Cdecl]<void> Py_Initialize { get; }
        internal delegate* unmanaged[Cdecl]<int, void> Py_InitializeEx { get; }
        internal delegate* unmanaged[Cdecl]<int> Py_IsInitialized { get; }
        internal delegate* unmanaged[Cdecl]<void> Py_Finalize { get; }
        internal delegate* unmanaged[Cdecl]<PyThreadState*> Py_NewInterpreter { get; }
        internal delegate* unmanaged[Cdecl]<PyThreadState*, void> Py_EndInterpreter { get; }
        internal delegate* unmanaged[Cdecl]<PyInterpreterState*, PyThreadState*> PyThreadState_New { get; }
        internal delegate* unmanaged[Cdecl]<PyThreadState*> PyThreadState_Get { get; }
        internal delegate* unmanaged[Cdecl]<PyThreadState*> PyThreadState_GetUnchecked { get; }
        internal delegate* unmanaged[Cdecl]<int> PyGILState_Check { get; }
        internal delegate* unmanaged[Cdecl]<PyGILState> PyGILState_Ensure { get; }
        internal delegate* unmanaged[Cdecl]<PyGILState, void> PyGILState_Release { get; }
        internal delegate* unmanaged[Cdecl]<PyThreadState*> PyGILState_GetThisThreadState { get; }
        internal delegate* unmanaged[Cdecl]<void> PyEval_InitThreads { get; }
        internal delegate* unmanaged[Cdecl]<int> PyEval_ThreadsInitialized { get; }
        internal delegate* unmanaged[Cdecl]<void> PyEval_AcquireLock { get; }
        internal delegate* unmanaged[Cdecl]<void> PyEval_ReleaseLock { get; }
        internal delegate* unmanaged[Cdecl]<PyThreadState*, void> PyEval_AcquireThread { get; }
        internal delegate* unmanaged[Cdecl]<PyThreadState*, void> PyEval_ReleaseThread { get; }
        internal delegate* unmanaged[Cdecl]<PyThreadState*> PyEval_SaveThread { get; }
        internal delegate* unmanaged[Cdecl]<PyThreadState*, void> PyEval_RestoreThread { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference> PyEval_GetBuiltins { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference> PyEval_GetGlobals { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference> PyEval_GetLocals { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr> Py_GetProgramName { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, void> Py_SetProgramName { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr> Py_GetPythonHome { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, void> Py_SetPythonHome { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr> Py_GetPath { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, void> Py_SetPath { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr> Py_GetVersion { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr> Py_GetPlatform { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr> Py_GetCopyright { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr> Py_GetCompiler { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr> Py_GetBuildInfo { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, in PyCompilerFlags, int> PyRun_SimpleStringFlags { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, RunFlagType, BorrowedReference, BorrowedReference, in PyCompilerFlags, NewReference> PyRun_StringFlags { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, NewReference> PyEval_EvalCode { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference, int, in PyCompilerFlags, int, NewReference> Py_CompileStringObject { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference, NewReference> PyImport_ExecCodeModule { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, int> PyObject_HasAttrString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, NewReference> PyObject_GetAttrString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, BorrowedReference, int> PyObject_SetAttrString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyObject_HasAttr { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyObject_GetAttr { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, int> PyObject_SetAttr { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyObject_GetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, int> PyObject_SetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyObject_DelItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyObject_GetIter { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, NewReference> PyObject_Call { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyObject_CallObject { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int, int> PyObject_RichCompareBool { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyObject_IsInstance { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyObject_IsSubclass { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, void> PyObject_ClearWeakRefs { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, int> PyCallable_Check { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, int> PyObject_IsTrue { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, int> PyObject_Not { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint> PyObject_Size { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint> PyObject_Hash { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyObject_Repr { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyObject_Str { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyObject_Type { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyObject_Dir { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, out Py_buffer, int, int> PyObject_GetBuffer { get; }
        internal delegate* unmanaged[Cdecl]<ref Py_buffer, void> PyBuffer_Release { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, nint> PyBuffer_SizeFromFormat { get; }
        internal delegate* unmanaged[Cdecl]<ref Py_buffer, char, int> PyBuffer_IsContiguous { get; }
        internal delegate* unmanaged[Cdecl]<ref Py_buffer, nint[], IntPtr> PyBuffer_GetPointer { get; }
        internal delegate* unmanaged[Cdecl]<ref Py_buffer, IntPtr, IntPtr, char, int> PyBuffer_FromContiguous { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, ref Py_buffer, IntPtr, char, int> PyBuffer_ToContiguous { get; }
        internal delegate* unmanaged[Cdecl]<int, IntPtr, IntPtr, int, char, void> PyBuffer_FillContiguousStrides { get; }
        internal delegate* unmanaged[Cdecl]<ref Py_buffer, BorrowedReference, IntPtr, IntPtr, int, int, int> PyBuffer_FillInfo { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyNumber_Long { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyNumber_Float { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, bool> PyNumber_Check { get; }
        internal delegate* unmanaged[Cdecl]<long, NewReference> PyLong_FromLongLong { get; }
        internal delegate* unmanaged[Cdecl]<ulong, NewReference> PyLong_FromUnsignedLongLong { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, IntPtr, int, NewReference> PyLong_FromString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, long> PyLong_AsLongLong { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, ulong> PyLong_AsUnsignedLongLong { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, NewReference> PyLong_FromVoidPtr { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr> PyLong_AsVoidPtr { get; }
        internal delegate* unmanaged[Cdecl]<double, NewReference> PyFloat_FromDouble { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyFloat_FromString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, double> PyFloat_AsDouble { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_Add { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_Subtract { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_Multiply { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_TrueDivide { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_And { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_Xor { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_Or { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_Lshift { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_Rshift { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_Power { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_Remainder { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceAdd { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceSubtract { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceMultiply { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceTrueDivide { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceAnd { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceXor { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceOr { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceLshift { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceRshift { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlacePower { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyNumber_InPlaceRemainder { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyNumber_Negative { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyNumber_Positive { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyNumber_Invert { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, bool> PySequence_Check { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, NewReference> PySequence_GetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, BorrowedReference, int> PySequence_SetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, int> PySequence_DelItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, NewReference> PySequence_GetSlice { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, BorrowedReference, int> PySequence_SetSlice { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, int> PySequence_DelSlice { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint> PySequence_Size { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PySequence_Contains { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PySequence_Concat { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, NewReference> PySequence_Repeat { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, nint> PySequence_Index { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, nint> PySequence_Count { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PySequence_Tuple { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PySequence_List { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr> PyBytes_AsString { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, NewReference> PyBytes_FromString { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, nint, NewReference> PyByteArray_FromStringAndSize { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint> PyBytes_Size { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr> PyUnicode_AsUTF8 { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, nint, IntPtr, IntPtr, NewReference> PyUnicode_DecodeUTF16 { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint> PyUnicode_GetLength { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, int> PyUnicode_ReadChar { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyUnicode_AsUTF16String { get; }
        internal delegate* unmanaged[Cdecl]<int, NewReference> PyUnicode_FromOrdinal { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, NewReference> PyUnicode_InternFromString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyUnicode_Compare { get; }
        internal delegate* unmanaged[Cdecl]<NewReference> PyDict_New { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference> PyDict_GetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, BorrowedReference> PyDict_GetItemString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, int> PyDict_SetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, BorrowedReference, int> PyDict_SetItemString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyDict_DelItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, int> PyDict_DelItemString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyMapping_HasKey { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyDict_Keys { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyDict_Values { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyDict_Items { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyDict_Copy { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyDict_Update { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, void> PyDict_Clear { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint> PyDict_Size { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PySet_New { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PySet_Add { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PySet_Contains { get; }
        internal delegate* unmanaged[Cdecl]<nint, NewReference> PyList_New { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, BorrowedReference> PyList_GetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, StolenReference, int> PyList_SetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, BorrowedReference, int> PyList_Insert { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyList_Append { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, int> PyList_Reverse { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, int> PyList_Sort { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, NewReference> PyList_GetSlice { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, BorrowedReference, int> PyList_SetSlice { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint> PyList_Size { get; }
        internal delegate* unmanaged[Cdecl]<nint, NewReference> PyTuple_New { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, BorrowedReference> PyTuple_GetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, StolenReference, int> PyTuple_SetItem { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, nint, NewReference> PyTuple_GetSlice { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint> PyTuple_Size { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, int> PyIter_Check { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyIter_Next { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, NewReference> PyModule_New { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference> PyModule_GetDict { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, IntPtr, int> PyModule_AddObject { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyImport_Import { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, NewReference> PyImport_ImportModule { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyImport_ReloadModule { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference> PyImport_AddModule { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference> PyImport_GetModuleDict { get; }
        internal delegate* unmanaged[Cdecl]<int, IntPtr, int, void> PySys_SetArgvEx { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference> PySys_GetObject { get; }
        internal delegate* unmanaged[Cdecl]<StrPtr, BorrowedReference, int> PySys_SetObject { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, void> PyType_Modified { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, bool> PyType_IsSubtype { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, NewReference> PyType_GenericNew { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint, NewReference> PyType_GenericAlloc { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, int> PyType_Ready { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference> _PyType_Lookup { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, NewReference> PyObject_GenericGetAttr { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference, int> PyObject_GenericSetAttr { get; }
        internal delegate* unmanaged[Cdecl]<StolenReference, void> PyObject_GC_Del { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, int> PyObject_GC_IsTracked { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, void> PyObject_GC_Track { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, void> PyObject_GC_UnTrack { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, void> _PyObject_Dump { get; }
        internal delegate* unmanaged[Cdecl]<nint, IntPtr> PyMem_Malloc { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, nint, IntPtr> PyMem_Realloc { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, void> PyMem_Free { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, StrPtr, void> PyErr_SetString { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, void> PyErr_SetObject { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, int> PyErr_ExceptionMatches { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyErr_GivenExceptionMatches { get; }
        internal delegate* unmanaged[Cdecl]<ref NewReference, ref NewReference, ref NewReference, void> PyErr_NormalizeException { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference> PyErr_Occurred { get; }
        internal delegate* unmanaged[Cdecl]<out NewReference, out NewReference, out NewReference, void> PyErr_Fetch { get; }
        internal delegate* unmanaged[Cdecl]<StolenReference, StolenReference, StolenReference, void> PyErr_Restore { get; }
        internal delegate* unmanaged[Cdecl]<void> PyErr_Clear { get; }
        internal delegate* unmanaged[Cdecl]<void> PyErr_Print { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyCell_Get { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyCell_Set { get; }
        internal delegate* unmanaged[Cdecl]<nint> PyGC_Collect { get; }
        internal delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, NewReference> PyCapsule_New { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr, IntPtr> PyCapsule_GetPointer { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr, int> PyCapsule_SetPointer { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nuint> PyLong_AsUnsignedSize_t { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, nint> PyLong_AsSignedSize_t { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, BorrowedReference> PyDict_GetItemWithError { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyException_GetCause { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, NewReference> PyException_GetTraceback { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, StolenReference, void> PyException_SetCause { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, BorrowedReference, int> PyException_SetTraceback { get; }
        internal delegate* unmanaged[Cdecl]<uint, BorrowedReference, int> PyThreadState_SetAsyncExcLLP64 { get; }
        internal delegate* unmanaged[Cdecl]<ulong, BorrowedReference, int> PyThreadState_SetAsyncExcLP64 { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, IntPtr, NewReference> PyObject_GenericGetDict { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, TypeSlotID, IntPtr> PyType_GetSlot { get; }
        internal delegate* unmanaged[Cdecl]<in NativeTypeSpec, BorrowedReference, NewReference> PyType_FromSpecWithBases { get; }
        internal delegate* unmanaged[Cdecl]<BorrowedReference, void> _Py_NewReference { get; }
        internal delegate* unmanaged[Cdecl]<int> _Py_IsFinalizing { get; }
        internal IntPtr PyType_Type { get; }
        internal int* Py_NoSiteFlag { get; }
    }
}
