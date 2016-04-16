/* 
 * File: MockStorage.cs
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Urasandesu.Moq.Prig.Mixins.Urasandesu.Prig.Framework;
using Urasandesu.Prig.Framework;

namespace Urasandesu.Moq.Prig
{
    public class MockStorage : MockRepository, ICustomizable, ICustomizer
    {
        Dictionary<Delegate, MockProxy> m_storage = new Dictionary<Delegate, MockProxy>();
        Dictionary<Mock, MockProxy> m_mockToProxies = new Dictionary<Mock, MockProxy>();

        public MockStorage(MockBehavior defaultBehavior)
            : base(defaultBehavior)
        { }

        public new MockProxy<T> Create<T>() where T : class
        {
            var mock = base.Create<T>();
            return CreateProxy(mock);
        }

        public new MockProxy<T> Create<T>(MockBehavior behavior) where T : class
        {
            var mock = base.Create<T>(behavior);
            return CreateProxy(mock);
        }

        public new MockProxy<T> Create<T>(params object[] args) where T : class
        {
            var mock = base.Create<T>(args);
            return CreateProxy(mock);
        }

        public new MockProxy<T> Create<T>(MockBehavior behavior, params object[] args) where T : class
        {
            var mock = base.Create<T>(behavior, args);
            return CreateProxy(mock);
        }

        protected virtual MockProxy<T> CreateProxy<T>(Mock<T> mock) where T : class
        {
            var proxy = new MockProxy<T>(mock);
            m_mockToProxies[mock] = proxy;
            return proxy;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Assign(Delegate dlgt, MockProxy proxy)
        {
            m_storage[dlgt] = proxy;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Assign<T>(Func<TypedBehaviorPreparable<T>> func, MockProxy<T> proxy) where T : class
        {
            Assign((Delegate)func, (MockProxy)proxy);
        }

        public ICustomizable Customize(Action<ICustomizer> exp)
        {
            exp(this);
            return this;
        }

        MockProxy<T> ICustomizer.Do<T>(Func<TypedBehaviorPreparable<T>> func)
        {
            var preparable = func();
            var proxy = preparable.BodyBy(this);
            Assign(func, proxy);
            return proxy;
        }

        public override void Verify()
        {
            VerifyMocks(mock =>
            {
                var proxy = default(MockProxy);
                if (m_mockToProxies.TryGetValue(mock, out proxy))
                    proxy.Verify();
                else
                    mock.Verify();
            });
        }

        public override void VerifyAll()
        {
            VerifyMocks(mock =>
            {
                var proxy = default(MockProxy);
                if (m_mockToProxies.TryGetValue(mock, out proxy))
                    proxy.VerifyAll();
                else
                    mock.VerifyAll();
            });
        }

        public new IQueryable<T> Of<T>() where T : class
        {
            throw new NotSupportedException();
        }

        public new IQueryable<T> Of<T>(Expression<Func<T, bool>> specification) where T : class
        {
            throw new NotSupportedException();
        }

        public new T OneOf<T>() where T : class
        {
            throw new NotSupportedException();
        }

        public new T OneOf<T>(Expression<Func<T, bool>> specification) where T : class
        {
            throw new NotSupportedException();
        }
    }
}
