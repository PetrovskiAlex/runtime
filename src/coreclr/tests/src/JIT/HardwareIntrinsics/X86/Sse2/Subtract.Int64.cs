// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void SubtractInt64()
        {
            var test = new SimpleBinaryOpTest__SubtractInt64();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                if (Sse2.IsSupported)
                {
                    // Validates basic functionality works, using Load
                    test.RunBasicScenario_Load();

                    // Validates basic functionality works, using LoadAligned
                    test.RunBasicScenario_LoadAligned();
                }

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                if (Sse2.IsSupported)
                {
                    // Validates calling via reflection works, using Load
                    test.RunReflectionScenario_Load();

                    // Validates calling via reflection works, using LoadAligned
                    test.RunReflectionScenario_LoadAligned();
                }

                // Validates passing a static member works
                test.RunClsVarScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing a static member works, using pinning and Load
                    test.RunClsVarScenario_Load();
                }

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                if (Sse2.IsSupported)
                {
                    // Validates passing a local works, using Load
                    test.RunLclVarScenario_Load();

                    // Validates passing a local works, using LoadAligned
                    test.RunLclVarScenario_LoadAligned();
                }

                // Validates passing the field of a local class works
                test.RunClassLclFldScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing the field of a local class works, using pinning and Load
                    test.RunClassLclFldScenario_Load();
                }

                // Validates passing an instance member of a class works
                test.RunClassFldScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing an instance member of a class works, using pinning and Load
                    test.RunClassFldScenario_Load();
                }

                // Validates passing the field of a local struct works
                test.RunStructLclFldScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing the field of a local struct works, using pinning and Load
                    test.RunStructLclFldScenario_Load();
                }

                // Validates passing an instance member of a struct works
                test.RunStructFldScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing an instance member of a struct works, using pinning and Load
                    test.RunStructFldScenario_Load();
                }
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class SimpleBinaryOpTest__SubtractInt64
    {
        private struct TestStruct
        {
            public Vector128<Int64> _fld1;
            public Vector128<Int64> _fld2;

            public static TestStruct Create()
            {
                var testStruct = new TestStruct();

                for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetInt64(); }
                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref testStruct._fld1), ref Unsafe.As<Int64, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());
                for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = TestLibrary.Generator.GetInt64(); }
                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref testStruct._fld2), ref Unsafe.As<Int64, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());

                return testStruct;
            }

            public void RunStructFldScenario(SimpleBinaryOpTest__SubtractInt64 testClass)
            {
                var result = Sse2.Subtract(_fld1, _fld2);

                Unsafe.Write(testClass._dataTable.outArrayPtr, result);
                testClass.ValidateResult(_fld1, _fld2, testClass._dataTable.outArrayPtr);
            }

            public void RunStructFldScenario_Load(SimpleBinaryOpTest__SubtractInt64 testClass)
            {
                fixed (Vector128<Int64>* pFld1 = &_fld1)
                fixed (Vector128<Int64>* pFld2 = &_fld2)
                {
                    var result = Sse2.Subtract(
                        Sse2.LoadVector128((Int64*)(pFld1)),
                        Sse2.LoadVector128((Int64*)(pFld2))
                    );

                    Unsafe.Write(testClass._dataTable.outArrayPtr, result);
                    testClass.ValidateResult(_fld1, _fld2, testClass._dataTable.outArrayPtr);
                }
            }
        }

        private static readonly int LargestVectorSize = 16;

        private static readonly int Op1ElementCount = Unsafe.SizeOf<Vector128<Int64>>() / sizeof(Int64);
        private static readonly int Op2ElementCount = Unsafe.SizeOf<Vector128<Int64>>() / sizeof(Int64);
        private static readonly int RetElementCount = Unsafe.SizeOf<Vector128<Int64>>() / sizeof(Int64);

        private static Int64[] _data1 = new Int64[Op1ElementCount];
        private static Int64[] _data2 = new Int64[Op2ElementCount];

        private static Vector128<Int64> _clsVar1;
        private static Vector128<Int64> _clsVar2;

        private Vector128<Int64> _fld1;
        private Vector128<Int64> _fld2;

        private SimpleBinaryOpTest__DataTable<Int64, Int64, Int64> _dataTable;

        static SimpleBinaryOpTest__SubtractInt64()
        {
            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetInt64(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref _clsVar1), ref Unsafe.As<Int64, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());
            for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = TestLibrary.Generator.GetInt64(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref _clsVar2), ref Unsafe.As<Int64, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());
        }

        public SimpleBinaryOpTest__SubtractInt64()
        {
            Succeeded = true;

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetInt64(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref _fld1), ref Unsafe.As<Int64, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());
            for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = TestLibrary.Generator.GetInt64(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref _fld2), ref Unsafe.As<Int64, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetInt64(); }
            for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = TestLibrary.Generator.GetInt64(); }
            _dataTable = new SimpleBinaryOpTest__DataTable<Int64, Int64, Int64>(_data1, _data2, new Int64[RetElementCount], LargestVectorSize);
        }

        public bool IsSupported => Sse2.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_UnsafeRead));

            var result = Sse2.Subtract(
                Unsafe.Read<Vector128<Int64>>(_dataTable.inArray1Ptr),
                Unsafe.Read<Vector128<Int64>>(_dataTable.inArray2Ptr)
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_Load));

            var result = Sse2.Subtract(
                Sse2.LoadVector128((Int64*)(_dataTable.inArray1Ptr)),
                Sse2.LoadVector128((Int64*)(_dataTable.inArray2Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_LoadAligned));

            var result = Sse2.Subtract(
                Sse2.LoadAlignedVector128((Int64*)(_dataTable.inArray1Ptr)),
                Sse2.LoadAlignedVector128((Int64*)(_dataTable.inArray2Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_UnsafeRead));

            var result = typeof(Sse2).GetMethod(nameof(Sse2.Subtract), new Type[] { typeof(Vector128<Int64>), typeof(Vector128<Int64>) })
                                     .Invoke(null, new object[] {
                                        Unsafe.Read<Vector128<Int64>>(_dataTable.inArray1Ptr),
                                        Unsafe.Read<Vector128<Int64>>(_dataTable.inArray2Ptr)
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Int64>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_Load));

            var result = typeof(Sse2).GetMethod(nameof(Sse2.Subtract), new Type[] { typeof(Vector128<Int64>), typeof(Vector128<Int64>) })
                                     .Invoke(null, new object[] {
                                        Sse2.LoadVector128((Int64*)(_dataTable.inArray1Ptr)),
                                        Sse2.LoadVector128((Int64*)(_dataTable.inArray2Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Int64>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_LoadAligned));

            var result = typeof(Sse2).GetMethod(nameof(Sse2.Subtract), new Type[] { typeof(Vector128<Int64>), typeof(Vector128<Int64>) })
                                     .Invoke(null, new object[] {
                                        Sse2.LoadAlignedVector128((Int64*)(_dataTable.inArray1Ptr)),
                                        Sse2.LoadAlignedVector128((Int64*)(_dataTable.inArray2Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Int64>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClsVarScenario));

            var result = Sse2.Subtract(
                _clsVar1,
                _clsVar2
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_clsVar1, _clsVar2, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClsVarScenario_Load));

            fixed (Vector128<Int64>* pClsVar1 = &_clsVar1)
            fixed (Vector128<Int64>* pClsVar2 = &_clsVar2)
            {
                var result = Sse2.Subtract(
                    Sse2.LoadVector128((Int64*)(pClsVar1)),
                    Sse2.LoadVector128((Int64*)(pClsVar2))
                );

                Unsafe.Write(_dataTable.outArrayPtr, result);
                ValidateResult(_clsVar1, _clsVar2, _dataTable.outArrayPtr);
            }
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_UnsafeRead));

            var left = Unsafe.Read<Vector128<Int64>>(_dataTable.inArray1Ptr);
            var right = Unsafe.Read<Vector128<Int64>>(_dataTable.inArray2Ptr);
            var result = Sse2.Subtract(left, right);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(left, right, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_Load));

            var left = Sse2.LoadVector128((Int64*)(_dataTable.inArray1Ptr));
            var right = Sse2.LoadVector128((Int64*)(_dataTable.inArray2Ptr));
            var result = Sse2.Subtract(left, right);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(left, right, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_LoadAligned));

            var left = Sse2.LoadAlignedVector128((Int64*)(_dataTable.inArray1Ptr));
            var right = Sse2.LoadAlignedVector128((Int64*)(_dataTable.inArray2Ptr));
            var result = Sse2.Subtract(left, right);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(left, right, _dataTable.outArrayPtr);
        }

        public void RunClassLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassLclFldScenario));

            var test = new SimpleBinaryOpTest__SubtractInt64();
            var result = Sse2.Subtract(test._fld1, test._fld2);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, _dataTable.outArrayPtr);
        }

        public void RunClassLclFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassLclFldScenario_Load));

            var test = new SimpleBinaryOpTest__SubtractInt64();

            fixed (Vector128<Int64>* pFld1 = &test._fld1)
            fixed (Vector128<Int64>* pFld2 = &test._fld2)
            {
                var result = Sse2.Subtract(
                    Sse2.LoadVector128((Int64*)(pFld1)),
                    Sse2.LoadVector128((Int64*)(pFld2))
                );

                Unsafe.Write(_dataTable.outArrayPtr, result);
                ValidateResult(test._fld1, test._fld2, _dataTable.outArrayPtr);
            }
        }

        public void RunClassFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassFldScenario));

            var result = Sse2.Subtract(_fld1, _fld2);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_fld1, _fld2, _dataTable.outArrayPtr);
        }

        public void RunClassFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassFldScenario_Load));

            fixed (Vector128<Int64>* pFld1 = &_fld1)
            fixed (Vector128<Int64>* pFld2 = &_fld2)
            {
                var result = Sse2.Subtract(
                    Sse2.LoadVector128((Int64*)(pFld1)),
                    Sse2.LoadVector128((Int64*)(pFld2))
                );

                Unsafe.Write(_dataTable.outArrayPtr, result);
                ValidateResult(_fld1, _fld2, _dataTable.outArrayPtr);
            }
        }

        public void RunStructLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructLclFldScenario));

            var test = TestStruct.Create();
            var result = Sse2.Subtract(test._fld1, test._fld2);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, _dataTable.outArrayPtr);
        }

        public void RunStructLclFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructLclFldScenario_Load));

            var test = TestStruct.Create();
            var result = Sse2.Subtract(
                Sse2.LoadVector128((Int64*)(&test._fld1)),
                Sse2.LoadVector128((Int64*)(&test._fld2))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, _dataTable.outArrayPtr);
        }
        
        public void RunStructFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructFldScenario));

            var test = TestStruct.Create();
            test.RunStructFldScenario(this);
        }

        public void RunStructFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructFldScenario_Load));

            var test = TestStruct.Create();
            test.RunStructFldScenario_Load(this);
        }

        public void RunUnsupportedScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunUnsupportedScenario));

            bool succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                succeeded = true;
            }

            if (!succeeded)
            {
                Succeeded = false;
            }
        }

        private void ValidateResult(Vector128<Int64> left, Vector128<Int64> right, void* result, [CallerMemberName] string method = "")
        {
            Int64[] inArray1 = new Int64[Op1ElementCount];
            Int64[] inArray2 = new Int64[Op2ElementCount];
            Int64[] outArray = new Int64[RetElementCount];

            Unsafe.WriteUnaligned(ref Unsafe.As<Int64, byte>(ref inArray1[0]), left);
            Unsafe.WriteUnaligned(ref Unsafe.As<Int64, byte>(ref inArray2[0]), right);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Int64, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector128<Int64>>());

            ValidateResult(inArray1, inArray2, outArray, method);
        }

        private void ValidateResult(void* left, void* right, void* result, [CallerMemberName] string method = "")
        {
            Int64[] inArray1 = new Int64[Op1ElementCount];
            Int64[] inArray2 = new Int64[Op2ElementCount];
            Int64[] outArray = new Int64[RetElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Int64, byte>(ref inArray1[0]), ref Unsafe.AsRef<byte>(left), (uint)Unsafe.SizeOf<Vector128<Int64>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Int64, byte>(ref inArray2[0]), ref Unsafe.AsRef<byte>(right), (uint)Unsafe.SizeOf<Vector128<Int64>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Int64, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector128<Int64>>());

            ValidateResult(inArray1, inArray2, outArray, method);
        }

        private void ValidateResult(Int64[] left, Int64[] right, Int64[] result, [CallerMemberName] string method = "")
        {
            bool succeeded = true;

            if ((long)(left[0] - right[0]) != result[0])
            {
                succeeded = false;
            }
            else
            {
                for (var i = 1; i < RetElementCount; i++)
                {
                    if ((long)(left[i] - right[i]) != result[i])
                    {
                        succeeded = false;
                        break;
                    }
                }
            }

            if (!succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"{nameof(Sse2)}.{nameof(Sse2.Subtract)}<Int64>(Vector128<Int64>, Vector128<Int64>): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"    left: ({string.Join(", ", left)})");
                TestLibrary.TestFramework.LogInformation($"   right: ({string.Join(", ", right)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }
        }
    }
}
