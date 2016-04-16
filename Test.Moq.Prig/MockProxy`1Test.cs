/* 
 * File: MockProxy`1Test.cs
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
using System.IO;
using Urasandesu.Moq.Prig;

namespace Test.Urasandesu.Moq.Prig
{
    [TestFixture]
    class MockProxy_1Test
    {
        [Test]
        public void ExpectExpressionOfActionOfT_can_setup_verifiable_mock()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            m.Expect(_ => _.WriteLine(42));

            // Act
            m.Object.WriteLine(42);

            // Assert
            m.Verify();
        }

        [Test]
        public void ExpectExpressionOfActionOfTString_can_setup_verifiable_mock_with_fail_message()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            var failMessage = Guid.NewGuid().ToString();
            m.Expect(_ => _.WriteLine(42), failMessage);

            // Act
            m.Object.WriteLine(43);

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }

        [Test]
        public void ExpectExpressionOfActionOfTTimes_can_setup_verifiable_mock_with_called_count()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            m.Expect(_ => _.WriteLine(42), Times.Exactly(2));

            // Act
            m.Object.WriteLine(42);
            m.Object.WriteLine(42);

            // Assert
            m.Verify();
        }

        [Test]
        public void ExpectExpressionOfActionOfTTimesString_can_setup_verifiable_mock_with_called_count_and_fail_message()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            var failMessage = Guid.NewGuid().ToString();
            m.Expect(_ => _.WriteLine(42), Times.Exactly(2), failMessage);

            // Act
            m.Object.WriteLine(42);

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }



        [Test]
        public void ExpectOfTResultExpressionOfFuncOfTOfTResult_can_setup_verifiable_mock()
        {
            // Arrange
            var m = new MockProxy<IComparable<int>>(new Mock<IComparable<int>>());
            m.Expect(_ => _.CompareTo(42)).Returns(1);

            // Act
            var result = m.Object.CompareTo(42);

            // Assert
            m.Verify();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void ExpectOfTResultExpressionOfFuncOfTOfTResultString_can_setup_verifiable_mock_with_fail_message()
        {
            // Arrange
            var m = new MockProxy<IComparable<int>>(new Mock<IComparable<int>>());
            var failMessage = Guid.NewGuid().ToString();
            m.Expect(_ => _.CompareTo(42), failMessage);

            // Act
            m.Object.CompareTo(43);

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }

        [Test]
        public void ExpectOfTResultExpressionOfFuncOfTOfTResultTimes_can_setup_verifiable_mock_with_called_count()
        {
            // Arrange
            var m = new MockProxy<IComparable<int>>(new Mock<IComparable<int>>());
            m.Expect(_ => _.CompareTo(42), Times.Exactly(2));

            // Act
            m.Object.CompareTo(42);
            m.Object.CompareTo(42);

            // Assert
            m.Verify();
        }

        [Test]
        public void ExpectOfTResultExpressionOfFuncOfTOfTResultTimesString_can_setup_verifiable_mock_with_called_count_and_fail_message()
        {
            // Arrange
            var m = new MockProxy<IComparable<int>>(new Mock<IComparable<int>>());
            var failMessage = Guid.NewGuid().ToString();
            m.Expect(_ => _.CompareTo(42), Times.Exactly(2), failMessage);

            // Act
            m.Object.CompareTo(42);

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }



        [Test]
        public void ExpectGetOfTPropertyExpressionOfFuncOfTOfTProperty_can_setup_verifiable_mock()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            m.ExpectGet(_ => _.NewLine).Returns("\n");

            // Act
            var result = m.Object.NewLine;

            // Assert
            m.Verify();
            Assert.AreEqual("\n", result);
        }

        [Test]
        public void ExpectGetOfTPropertyExpressionOfFuncOfTOfTPropertyString_can_setup_verifiable_mock_with_fail_message()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            var failMessage = Guid.NewGuid().ToString();
            m.ExpectGet(_ => _.NewLine, failMessage);

            // Act
            // nop

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }

        [Test]
        public void ExpectGetOfTPropertyExpressionOfFuncOfTOfTPropertyTimes_can_setup_verifiable_mock_with_called_count()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            m.ExpectGet(_ => _.NewLine, Times.Exactly(2));

            // Act
            { var _ = m.Object.NewLine; }
            { var _ = m.Object.NewLine; }

            // Assert
            m.Verify();
        }

        [Test]
        public void ExpectGetOfTPropertyExpressionOfFuncOfTOfTPropertyTimesString_can_setup_verifiable_mock_with_called_count_and_fail_message()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            var failMessage = Guid.NewGuid().ToString();
            m.ExpectGet(_ => _.NewLine, Times.Exactly(2), failMessage);

            // Act
            { var _ = m.Object.NewLine; }

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }



        [Test]
        public void ExpectSetActionOfT_can_setup_verifiable_mock()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            m.ExpectSet(_ => _.NewLine = "\n");

            // Act
            m.Object.NewLine = "\n";

            // Assert
            m.Verify();
        }

        [Test]
        public void ExpectSetActionOfTString_can_setup_verifiable_mock_with_fail_message()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            var failMessage = Guid.NewGuid().ToString();
            m.ExpectSet(_ => _.NewLine = "\n", failMessage);

            // Act
            // nop

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }

        [Test]
        public void ExpectSetActionOfTTimes_can_setup_verifiable_mock_with_called_count()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            m.ExpectSet(_ => _.NewLine = "\n", Times.Exactly(2));

            // Act
            m.Object.NewLine = "\n";
            m.Object.NewLine = "\n";

            // Assert
            m.Verify();
        }

        [Test]
        public void ExpectSetActionOfTTimesString_can_setup_verifiable_mock_with_called_count_and_fail_message()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            var failMessage = Guid.NewGuid().ToString();
            m.ExpectSet(_ => _.NewLine = "\n", Times.Exactly(2), failMessage);

            // Act
            m.Object.NewLine = "\n";

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }



        [Test]
        public void ExpectSetOfTPropertyActionOfT_can_setup_verifiable_mock()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            m.ExpectSet<string>(_ => _.NewLine = "\n");

            // Act
            m.Object.NewLine = "\n";

            // Assert
            m.Verify();
        }

        [Test]
        public void ExpectSetOfTPropertyActionOfTString_can_setup_verifiable_mock_with_fail_message()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            var failMessage = Guid.NewGuid().ToString();
            m.ExpectSet<string>(_ => _.NewLine = "\n", failMessage);

            // Act
            // nop

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }

        [Test]
        public void ExpectSetOfTPropertyActionOfTTimes_can_setup_verifiable_mock_with_called_count()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            m.ExpectSet(_ => _.NewLine = "\n", Times.Exactly(2));

            // Act
            m.Object.NewLine = "\n";
            m.Object.NewLine = "\n";

            // Assert
            m.Verify();
        }

        [Test]
        public void ExpectSetOfTPropertyActionOfTTimesString_can_setup_verifiable_mock_with_called_count_and_fail_message()
        {
            // Arrange
            var m = new MockProxy<TextWriter>(new Mock<TextWriter>());
            var failMessage = Guid.NewGuid().ToString();
            m.ExpectSet(_ => _.NewLine = "\n", Times.Exactly(2), failMessage);

            // Act
            m.Object.NewLine = "\n";

            // Assert
            try
            {
                m.Verify();
                Assert.Fail("We shouldn't get here!!");
            }
            catch (MockException e)
            {
                Assert.That(e.ToString(), Is.StringContaining(failMessage));
            }
        }
    }
}
