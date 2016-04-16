/* 
 * File: MockProxy`1.cs
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
using Moq.Language;
using Moq.Language.Flow;
using System;
using System.Linq.Expressions;

namespace Urasandesu.Moq.Prig
{
    public class MockProxy<T> : MockProxy where T : class
    {
        protected new Mock<T> Source { get { return (Mock<T>)base.Source; } }

        public MockProxy(Mock<T> source)
            : base(source)
        { }

        public ISetup<T> Expect(Expression<Action<T>> expression) { return Expect(expression, Times.Once(), null); }
        public ISetup<T> Expect(Expression<Action<T>> expression, string failMessage) { return Expect(expression, Times.Once(), failMessage); }
        public ISetup<T> Expect(Expression<Action<T>> expression, Times times) { return Expect(expression, times, null); }
        public ISetup<T> Expect(Expression<Action<T>> expression, Times times, string failMessage)
        {
            VerifyActions.Add(() => Source.Verify(expression, times, failMessage));
            var setup = Source.Setup(expression);
            setup.Verifiable();
            return setup;
        }

        public ISetup<T, TResult> Expect<TResult>(Expression<Func<T, TResult>> expression) { return Expect(expression, Times.Once(), null); }
        public ISetup<T, TResult> Expect<TResult>(Expression<Func<T, TResult>> expression, string failMessage) { return Expect(expression, Times.Once(), failMessage); }
        public ISetup<T, TResult> Expect<TResult>(Expression<Func<T, TResult>> expression, Times times) { return Expect(expression, times, null); }
        public ISetup<T, TResult> Expect<TResult>(Expression<Func<T, TResult>> expression, Times times, string failMessage)
        {
            VerifyActions.Add(() => Source.Verify(expression, times, failMessage));
            var setup = Source.Setup(expression);
            setup.Verifiable();
            return setup;
        }

        public ISetupGetter<T, TProperty> ExpectGet<TProperty>(Expression<Func<T, TProperty>> expression) { return ExpectGet(expression, Times.Once(), null); }
        public ISetupGetter<T, TProperty> ExpectGet<TProperty>(Expression<Func<T, TProperty>> expression, string failMessage) { return ExpectGet(expression, Times.Once(), failMessage); }
        public ISetupGetter<T, TProperty> ExpectGet<TProperty>(Expression<Func<T, TProperty>> expression, Times times) { return ExpectGet(expression, times, null); }
        public ISetupGetter<T, TProperty> ExpectGet<TProperty>(Expression<Func<T, TProperty>> expression, Times times, string failMessage)
        {
            VerifyActions.Add(() => Source.VerifyGet(expression, times, failMessage));
            var setup = Source.SetupGet(expression);
            setup.Verifiable();
            return setup;
        }

        public ISetup<T> ExpectSet(Action<T> setterExpression) { return ExpectSet(setterExpression, Times.Once(), null); }
        public ISetup<T> ExpectSet(Action<T> setterExpression, string failMessage) { return ExpectSet(setterExpression, Times.Once(), failMessage); }
        public ISetup<T> ExpectSet(Action<T> setterExpression, Times times) { return ExpectSet(setterExpression, times, null); }
        public ISetup<T> ExpectSet(Action<T> setterExpression, Times times, string failMessage)
        {
            VerifyActions.Add(() => Source.VerifySet(setterExpression, times, failMessage));
            var setup = Source.SetupSet(setterExpression);
            setup.Verifiable();
            return setup;
        }

        public ISetupSetter<T, TProperty> ExpectSet<TProperty>(Action<T> setterExpression) { return ExpectSet<TProperty>(setterExpression, Times.Once(), null); }
        public ISetupSetter<T, TProperty> ExpectSet<TProperty>(Action<T> setterExpression, string failMessage) { return ExpectSet<TProperty>(setterExpression, Times.Once(), failMessage); }
        public ISetupSetter<T, TProperty> ExpectSet<TProperty>(Action<T> setterExpression, Times times) { return ExpectSet<TProperty>(setterExpression, times, null); }
        public ISetupSetter<T, TProperty> ExpectSet<TProperty>(Action<T> setterExpression, Times times, string failMessage)
        {
            VerifyActions.Add(() => Source.VerifySet(setterExpression, times, failMessage));
            var setup = Source.SetupSet<TProperty>(setterExpression);
            setup.Verifiable();
            return setup;
        }

        public new T Object { get { return Source.Object; } }
        public void Raise(Action<T> eventExpression, EventArgs args) { Source.Raise(eventExpression, args); }
        public void Raise(Action<T> eventExpression, params object[] args) { Source.Raise(eventExpression, args); }
        public ISetup<T> Setup(Expression<Action<T>> expression) { return Source.Setup(expression); }
        public ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression) { return Source.Setup(expression); }
        public MockProxy<T> SetupAllProperties() { Source.SetupAllProperties(); return this; }
        public ISetupGetter<T, TProperty> SetupGet<TProperty>(Expression<Func<T, TProperty>> expression) { return Source.SetupGet(expression); }
        public MockProxy<T> SetupProperty<TProperty>(Expression<Func<T, TProperty>> property) { Source.SetupProperty(property); return this; }
        public MockProxy<T> SetupProperty<TProperty>(Expression<Func<T, TProperty>> property, TProperty initialValue) { Source.SetupProperty(property, initialValue); return this; }
        public ISetup<T> SetupSet(Action<T> setterExpression) { return Source.SetupSet(setterExpression); }
        public ISetupSetter<T, TProperty> SetupSet<TProperty>(Action<T> setterExpression) { return Source.SetupSet<TProperty>(setterExpression); }
        public void Verify(Expression<Action<T>> expression) { Source.Verify(expression); }
        public void Verify<TResult>(Expression<Func<T, TResult>> expression) { Source.Verify(expression); }
        public void Verify(Expression<Action<T>> expression, string failMessage) { Source.Verify(expression, failMessage); }
        public void Verify(Expression<Action<T>> expression, Times times) { Source.Verify(expression, times); }
        public void Verify<TResult>(Expression<Func<T, TResult>> expression, string failMessage) { Source.Verify(expression, failMessage); }
        public void Verify<TResult>(Expression<Func<T, TResult>> expression, Times times) { Source.Verify(expression, times); }
        public void Verify(Expression<Action<T>> expression, Times times, string failMessage) { Source.Verify(expression, times, failMessage); }
        public void Verify<TResult>(Expression<Func<T, TResult>> expression, Times times, string failMessage) { Source.Verify(expression, times, failMessage); }
        public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression) { Source.VerifyGet(expression); }
        public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, string failMessage) { Source.VerifyGet(expression, failMessage); }
        public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Times times) { Source.VerifyGet(expression, times); }
        public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Times times, string failMessage) { Source.VerifyGet(expression, times, failMessage); }
        public void VerifySet(Action<T> setterExpression) { Source.VerifySet(setterExpression); }
        public void VerifySet(Action<T> setterExpression, string failMessage) { Source.VerifySet(setterExpression, failMessage); }
        public void VerifySet(Action<T> setterExpression, Times times) { Source.VerifySet(setterExpression, times); }
        public void VerifySet(Action<T> setterExpression, Times times, string failMessage) { Source.VerifySet(setterExpression, times, failMessage); }
        public ISetupConditionResult<T> When(Func<bool> condition) { return Source.When(condition); }
    }
}
