// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.Logging;

namespace SampleApp
{
    internal static class LoggerExtensions
    {
        private static readonly ScopeMessage<string> _purchaseOrderScope = "PO:{PurchaseOrder}";

        private static readonly InformationMessage<DateTimeOffset, int> _programStarting = (nameof(ProgramStarting), "Starting at '{StartTime}' and 0x{Hello:X} is hex of 42");

        private static readonly InformationMessage<DateTimeOffset> _programStopping = (nameof(ProgramStopping), "Stopping at '{StopTime}'");

        /// <summary>
        /// Logs the scope "PO:{PurchaseOrder}"
        /// </summary>
        /// <param name="logger">The logger to write to</param>
        /// <param name="purchaseOrder">The {PurchaseOrder} message property</param>
        /// <returns></returns>
        public static IDisposable PurchaseOrderScope(this ILogger<Program> logger, string purchaseOrder)
        {
            return _purchaseOrderScope.Begin(logger, purchaseOrder);
        }

        /// <summary>
        /// Logs an informational message "Starting at '{StartTime}' and 0x{Hello:X} is hex of 42"
        /// </summary>
        /// <param name="logger">The logger to write to</param>
        /// <param name="startTime">The {StartTime} message property</param>
        /// <param name="hello">The {Hello} message property</param>
        public static void ProgramStarting(this ILogger<Program> logger, DateTimeOffset startTime, int hello)
        {
            _programStarting.Log(logger, startTime, hello);
        }

        /// <summary>
        /// Logs an informational message "Stopping at '{StopTime}'"
        /// </summary>
        /// <param name="logger">The logger to write to</param>
        /// <param name="stopTime">The {StopTime} message property</param>
        public static void ProgramStopping(this ILogger<Program> logger, DateTimeOffset stopTime)
        {
            _programStopping.Log(logger, stopTime);
        }
    }
}
