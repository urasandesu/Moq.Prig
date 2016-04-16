/* 
 * File: MockProxy.cs
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
using System.Linq.Expressions;

namespace Urasandesu.Moq.Prig
{
    public abstract class MockProxy : IVerifiable, IHideObjectMembers
    {
        protected Mock Source { get; private set; }

        List<Action> m_verifyActions = new List<Action>();
        protected List<Action> VerifyActions { get { return m_verifyActions; } }

        public MockProxy(Mock source)
        {
            Source = source;
        }

        public virtual MockBehavior Behavior { get { return Source.Behavior; } }
        public virtual bool CallBase { get { return Source.CallBase; } set { Source.CallBase = value; } }
        public virtual DefaultValue DefaultValue { get { return Source.DefaultValue; } set { Source.DefaultValue = value; } }
        public object Object { get { return Source.Object; } }
        public virtual MockProxy<TInterface> As<TInterface>() where TInterface : class { return new MockProxy<TInterface>(Source.As<TInterface>()); }
        public static MockProxy<T> Get<T>(T mocked) where T : class { return new MockProxy<T>(Mock.Get<T>(mocked)); }
        public static T Of<T>() where T : class { throw new NotSupportedException(); }
        public static T Of<T>(Expression<Func<T, bool>> predicate) where T : class { throw new NotSupportedException(); }
        public void SetReturnsDefault<TReturn>(TReturn value) { Source.SetReturnsDefault(value); }
        
        public void Verify() 
        {
            foreach (var verifyAction in VerifyActions)
                verifyAction();

            Source.Verify();
        }
        
        public void VerifyAll() 
        {
            foreach (var verifyAction in VerifyActions)
                verifyAction();

            Source.VerifyAll();
        }
    }
}
