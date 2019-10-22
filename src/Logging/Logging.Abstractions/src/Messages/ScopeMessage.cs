// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.Logging
{
    public struct ScopeMessage
    {
    }

    public struct ScopeMessage<T1>
    {
        public ScopeMessage(Func<ILogger, T1, IDisposable> begin)
        {
            Begin = begin;
        }

        public Func<ILogger, T1, IDisposable> Begin { get; }

        public static implicit operator ScopeMessage<T1>(string formatString)
        {
            return new ScopeMessage<T1>(LoggerMessage.DefineScope<T1>(formatString));
        }
    }

    public struct ScopeMessage<T1, T2>
    {
        public ScopeMessage(Func<ILogger, T1, T2, IDisposable> begin)
        {
            Begin = begin;
        }

        public Func<ILogger, T1, T2, IDisposable> Begin { get; }

        public static implicit operator ScopeMessage<T1, T2>(string formatString)
        {
            return new ScopeMessage<T1, T2>(LoggerMessage.DefineScope<T1, T2>(formatString));
        }
    }
}
