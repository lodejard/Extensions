// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Http.Logging
{
    public class LoggingScopeHttpMessageHandler : DelegatingHandler
    {
        private ILogger _logger;

        public LoggingScopeHttpMessageHandler(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _logger = logger;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var stopwatch = ValueStopwatch.StartNew();

            using (Log.RequestPipelineScope(_logger, request))
            {
                var traceEnabled = _logger.IsEnabled(LogLevel.Trace);

                Log.RequestPipelineStart(_logger, request);
                if (traceEnabled)
                {
                    Log.RequestPipelineRequestHeader(_logger, request);
                }

                var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                Log.RequestPipelineEnd(_logger, response, stopwatch.GetElapsedTime());
                if (traceEnabled)
                {
                    Log.RequestPipelineResponseHeader(_logger, response);
                }

                return response;
            }
        }

        private static class Log
        {
            private static readonly ScopeMessage<HttpMethod, Uri> _requestPipelineScope = "HTTP {HttpMethod} {Uri}";

            private static readonly InformationMessage<HttpMethod, Uri> _requestPipelineStart =
                (100, nameof(RequestPipelineStart), "Start processing HTTP request {HttpMethod} {Uri}");

            private static readonly InformationMessage<double, HttpStatusCode> _requestPipelineEnd =
                (101, nameof(RequestPipelineEnd), "End processing HTTP request after {ElapsedMilliseconds}ms - {StatusCode}");

            private static readonly EventId _requestPipelineRequestHeader =
                (102, nameof(RequestPipelineRequestHeader));

            private static readonly EventId _requestPipelineResponseHeader =
                (103, nameof(RequestPipelineResponseHeader));

            public static IDisposable RequestPipelineScope(ILogger logger, HttpRequestMessage request)
            {
                return _requestPipelineScope.Begin(logger, request.Method, request.RequestUri);
            }

            public static void RequestPipelineStart(ILogger logger, HttpRequestMessage request)
            {
                _requestPipelineStart.Log(logger, request.Method, request.RequestUri);
            }

            public static void RequestPipelineEnd(ILogger logger, HttpResponseMessage response, TimeSpan duration)
            {
                _requestPipelineEnd.Log(logger, duration.TotalMilliseconds, response.StatusCode);
            }

            public static void RequestPipelineRequestHeader(ILogger logger, HttpRequestMessage request)
            {
                logger.Log(
                    LogLevel.Trace,
                    _requestPipelineRequestHeader,
                    new HttpHeadersLogValue(HttpHeadersLogValue.Kind.Request, request.Headers, request.Content?.Headers),
                    null,
                    (state, ex) => state.ToString());
            }

            public static void RequestPipelineResponseHeader(ILogger logger, HttpResponseMessage response)
            {
                logger.Log(
                    LogLevel.Trace,
                    _requestPipelineResponseHeader,
                    new HttpHeadersLogValue(HttpHeadersLogValue.Kind.Response, response.Headers, response.Content?.Headers),
                    null,
                    (state, ex) => state.ToString());
            }
        }
    }
}
