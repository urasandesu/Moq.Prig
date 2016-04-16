/* 
 * File: MockStorageTest.cs
 * 
 * Author: Akira Sugiura (urasandesu@gmail.com)
 * 
 * 
 * Copyright (c) 2016 Akira Sugiura
 *  
 *  This software is MIT License.
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 *  THE SOFTWARE.
 */



using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Diagnostics.Prig;
using Test.Urasandesu.Moq.Prig.TestUtilities.Mixins.System.Diagnostics.Prig;
using Urasandesu.Moq.Prig;
using Urasandesu.Moq.Prig.Mixins.Urasandesu.Prig.Framework;
using Urasandesu.Prig.Framework;

namespace Test.Urasandesu.Moq.Prig
{
    [TestFixture]
    class MockStorageTest
    {
        [Test]
        public void MockStorage_should_provide_fluent_setup_through_itself()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var ms = new MockStorage(MockBehavior.Strict);
                PProcessMixin.AutoBodyBy(ms);
                ms.Customize(c => c.
                        Do(PProcess.StartProcessStartInfo).Expect(_ => _(It.Is<ProcessStartInfo>(x =>
                            x.Arguments == "\"arg ument1\" \"argume nt2\""
                        ))).Returns(new PProxyProcess())
                   );

                // Act
                var proc = Process.Start(new ProcessStartInfo(Guid.NewGuid().ToString(), "\"arg ument1\" \"argume nt2\""));

                // Assert
                Assert.IsNotNull(proc);
                ms.Verify();
            }
        }

        [Test]
        public void MockStorage_should_provide_fluent_setup_through_MockProxy()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var ms = new MockStorage(MockBehavior.Strict);
                PProcess.StartStringString().BodyBy(ms).Expect(_ => _("file name", "arguments")).Returns(Process.GetCurrentProcess());

                // Act
                var proc = Process.Start("file name", "arguments");

                // Assert
                Assert.AreEqual(Process.GetCurrentProcess().Id, proc.Id);
                ms.Verify();
            }
        }



        [Test]
        public void CreateOfT_can_create_default_behavior_mock()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Strict);
            var m = ms.Create<ICloneable>();

            // Act, Assert
            Assert.Throws<MockException>(() => m.Object.Clone());
        }

        [Test]
        public void CreateOfTMockBehavior_can_create_specified_behavior_mock()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Default);
            var m = ms.Create<ICloneable>(MockBehavior.Strict);

            // Act, Assert
            Assert.Throws<MockException>(() => m.Object.Clone());
        }

        [Test]
        public void CreateOfTObjectArray_can_create_default_behavior_mock_of_the_type_that_has_constructor_with_parameters()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Strict);
            var m = ms.Create<ConstructorWithParameters>(42);
            m.Setup(_ => _.Parse("cba")).Returns(23);

            // Act, Assert
            Assert.Throws<MockException>(() => m.Object.Parse("abc"));
        }

        [Test]
        public void CreateOfTObjectArray_can_create_specified_behavior_mock_of_the_type_that_has_constructor_with_parameters()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Default);
            var m = ms.Create<ConstructorWithParameters>(MockBehavior.Strict, 42);
            m.Setup(_ => _.Parse("cba")).Returns(23);

            // Act, Assert
            Assert.Throws<MockException>(() => m.Object.Parse("abc"));
        }



        [Test]
        public void Verify_should_verify_only_verifiable_setup()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Default);
            var m = ms.Create<IComparable<int>>();
            m.Expect(_ => _.CompareTo(42)).Returns(0);
            m.Setup(_ => _.CompareTo(23)).Returns(1);

            // Act
            m.Object.CompareTo(42);
            m.Object.CompareTo(24);

            // Assert
            ms.Verify();
        }



        [Test]
        public void VerifyAll_should_verify_all_setup()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Default);
            var m = ms.Create<IComparable<int>>();
            m.Expect(_ => _.CompareTo(42)).Returns(0);
            m.Setup(_ => _.CompareTo(23)).Returns(1);

            // Act
            m.Object.CompareTo(42);
            m.Object.CompareTo(23);

            // Assert
            ms.VerifyAll();
        }



        [Test]
        public void OfOfT_should_throw_NotSupportedException_because_we_cannot_extract_mock_from_IQueryable()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Default);

            // Act, Assert
            Assert.Throws<NotSupportedException>(() => ms.Of<ICloneable>());
        }

        [Test]
        public void OfOfTExpressionOfFuncOfTOfBoolean_should_throw_NotSupportedException_because_we_cannot_extract_mock_from_IQueryable()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Default);

            // Act, Assert
            Assert.Throws<NotSupportedException>(() => ms.Of<ICloneable>(_ => _ is IComparable));
        }



        [Test]
        public void OneOfOfT_should_throw_NotSupportedException_because_we_cannot_extract_mock_from_IQueryable()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Default);

            // Act, Assert
            Assert.Throws<NotSupportedException>(() => ms.OneOf<ICloneable>());
        }

        [Test]
        public void OneOfOfTExpressionOfFuncOfTOfBoolean_should_throw_NotSupportedException_because_we_cannot_extract_mock_from_IQueryable()
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Default);

            // Act, Assert
            Assert.Throws<NotSupportedException>(() => ms.OneOf<ICloneable>(_ => _ is IComparable));
        }
    }



    public class ConstructorWithParameters
    {
        public ConstructorWithParameters(int value) { }
        public virtual int Parse(string s) { return int.Parse(s); }
    }
}

namespace Test.Urasandesu.Moq.Prig.TestUtilities.Mixins.System.Diagnostics.Prig
{
    public static class PProcessMixin
    {
        public static void AutoBodyBy(MockStorage ms)
        {
            var proc = new PProxyProcess();
            proc.AutoBodyBy(ms);

            ms.Customize(c => c.Do(PProcess.GetCurrentProcess).Setup(_ => _()).Returns(proc)).
               Customize(c => c.Do(PProcess.StartProcessStartInfo).Setup(_ => _(It.IsAny<ProcessStartInfo>())).Returns(proc));
        }
    }

    public static class PProxyProcessMixin
    {
        public static void AutoBodyBy(this PProxyProcess proc, MockStorage ms)
        {
            var procMod = new PProxyProcessModule();
            procMod.AutoBodyBy(ms);

            ms.Customize(c => c.Do(proc.StartInfoGet).Setup(_ => _(It.IsAny<Process>())).Returns(new ProcessStartInfo())).
               Customize(c => c.Do(proc.MainModuleGet).Setup(_ => _(It.IsAny<Process>())).Returns(procMod)).
               Customize(c => c.Do(proc.CloseMainWindow).Setup(_ => _(It.IsAny<Process>())).Returns(true));
        }
    }

    public static class PProxyProcessModuleMixin
    {
        public static void AutoBodyBy(this PProxyProcessModule procMod, MockStorage ms)
        {
            procMod.ModuleNameGet().Body = @this => Guid.NewGuid().ToString(); // to avoid NullReferenceException that is caused in Moq.
            ms.Customize(c => c.Do(procMod.FileNameGet).Setup(_ => _(It.IsAny<ProcessModule>())).Returns(Guid.NewGuid().ToString()));
        }
    }
}