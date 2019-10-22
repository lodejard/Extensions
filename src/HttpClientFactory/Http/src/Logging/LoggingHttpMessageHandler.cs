// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Http.Logging
{
    public class LoggingHttpMessageHandler : DelegatingHandler
    {
        private ILogger _logger;

        public LoggingHttpMessageHandler(ILogger logger)
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
            var traceEnabled = _logger.IsEnabled(LogLevel.Trace);

            // Not using a scope here because we always expect this to be at the end of the pipeline, thus there's
            // not really anything to surround.
            Log.RequestStart(_logger, request);
            if (traceEnabled)
            {
                Log.RequestHeader(_logger, request);
            }

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            Log.RequestEnd(_logger, response, stopwatch.GetElapsedTime());
            if (traceEnabled)
            {
                Log.ResponseHeader(_logger, response);
            }

            return response;
        }

        private static class Log
        {
            private static readonly InformationMessage<HttpMethod, Uri> _requestStart =
                new InformationMessage<HttpMethod, Uri>(100, nameof(RequestStart), "Sending HTTP request {HttpMethod} {Uri}");
               //(100, nameof(RequestStart), "Sending HTTP request {HttpMethod} {Uri}");

            private static readonly InformationMessage<double, HttpStatusCode> _requestEnd =
               (101, nameof(RequestEnd), "Received HTTP response after {ElapsedMilliseconds}ms - {StatusCode}");

            private static readonly EventId _requestHeader =
               (102, nameof(RequestHeader));

            private static readonly EventId _responseHeader =
               (103, nameof(ResponseHeader));

            public static void RequestStart(ILogger logger, HttpRequestMessage request)
            {
                _requestStart.Log(logger, request.Method, request.RequestUri);
            }

            public static void RequestEnd(ILogger logger, HttpResponseMessage response, TimeSpan duration)
            {
                _requestEnd.Log(logger, duration.TotalMilliseconds, response.StatusCode);
            }

            public static void RequestHeader(ILogger logger, HttpRequestMessage request)
            {
                logger.Log(
                    LogLevel.Trace,
                    _requestHeader,
                    new HttpHeadersLogValue(HttpHeadersLogValue.Kind.Request, request.Headers, request.Content?.Headers),
                    null,
                    (state, ex) => state.ToString());
            }

            public static void ResponseHeader(ILogger logger, HttpResponseMessage response)
            {
                logger.Log(
                    LogLevel.Trace,
                    _responseHeader,
                    new HttpHeadersLogValue(HttpHeadersLogValue.Kind.Response, response.Headers, response.Content?.Headers),
                    null,
                    (state, ex) => state.ToString());
            }
        }
    }
}
