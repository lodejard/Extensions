using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.Logging.CodeGenerator
{
    public static class MessageStructGenerator
    {
        private static readonly string EndOfLine = @"
";

        public static IEnumerable<(string fileName, string fileContent)> Generate()
        {
            var logLevels = new[] { "Trace", "Debug", "Information", "Warning", "Error", "Critical" };
            var descriptions = new[] { "a trace", "a debug", "an informational", "a warning", "an error", "a critical" };

            var infos = Enumerable.Range(0, 7).Select(BuildInfo);
            
            foreach (var (logLevel, description) in logLevels.Zip(descriptions))
            {
                var fileName = $"{logLevel}Message.cs";
                var fileContent =
$@"// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.Logging
{{
{ForEach(EndOfLine, infos, info => $@"
    /// <summary>
    /// Represents {description} message which is pre-computed and strongly typed to reduce logging overhead.
    /// </summary>
{ForEach("", info.details, detail => $@"
    /// <typeparam name=""{detail.type}"">The type of the value in the {detail.position} position of the format string.</typeparam>
")}    public struct {logLevel}Message{info.structTypes}
    {{
        private readonly Action{info.actionTypes} _log;

        /// <summary>
        /// Initializes an instance of the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.
        /// </summary>
        /// <param name=""eventId"">The event id associated with the log.</param>
        /// <param name=""formatString"">The named format string</param>
        public {logLevel}Message(EventId eventId, string formatString)
        {{
            _log = LoggerMessage.Define{info.structTypes}(LogLevel.{logLevel}, eventId, formatString);
        }}

        /// <summary>
        /// Initializes an instance of the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.
        /// </summary>
        /// <param name=""eventId"">The event id associated with the log.</param>
        /// <param name=""eventName"">The event name associated with the log.</param>
        /// <param name=""formatString"">The named format string</param>
        public {logLevel}Message(int eventId, string eventName, string formatString)
        {{
            _log = LoggerMessage.Define{info.structTypes}(LogLevel.{logLevel}, new EventId(eventId, eventName), formatString);
        }}

        /// <summary>
        /// Initializes an instance of the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.
        /// </summary>
        /// <param name=""eventId"">The event id associated with the log.</param>
        /// <param name=""formatString"">The named format string</param>
        public {logLevel}Message(int eventId, string formatString)
        {{
            _log = LoggerMessage.Define{info.structTypes}(LogLevel.{logLevel}, eventId, formatString);
        }}

        /// <summary>
        /// Initializes an instance of the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.
        /// </summary>
        /// <param name=""eventName"">The event name associated with the log.</param>
        /// <param name=""formatString"">The named format string</param>
        public {logLevel}Message(string eventName, string formatString)
        {{
            _log = LoggerMessage.Define{info.structTypes}(LogLevel.{logLevel}, EventId.FromName(eventName), formatString);
        }}

        /// <summary>
        /// Formats and writes {description} log message.
        /// </summary>
        /// <param name=""logger"">The <see cref=""ILogger""/> to write to.</param>
{ForEach("", info.details, detail => $@"
        /// <param name=""{detail.arg}"">The value at the {detail.position} position in the format string.</param>
")}        public void Log({Join(", ", "ILogger logger", info.logParameters)}) => _log({Join(", ", "logger", info.logArguments, "default")});

        /// <summary>
        /// Formats and writes {description} log message with exception details.
        /// </summary>
        /// <param name=""logger"">The <see cref=""ILogger""/> to write to.</param>
        /// <param name=""exception"">The <see cref=""Exception""/> details to include with the log.</param>
{ForEach("", info.details, detail => $@"
        /// <param name=""{detail.arg}"">The value at the {detail.position} position in the format string.</param>
")}       public void Log({Join(", ", "ILogger logger", "Exception exception", info.logParameters)}) => _log({Join(", ", "logger", info.logArguments, "exception")});

        /// <summary>
        /// Implicitly initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> from the given <see cref=""ValueTuple{{EventId, String}}""/> parameters.
        /// </summary>
        /// <param name=""parameters"">The <see cref=""EventId""/> and format string to initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.</param>
        public static implicit operator {logLevel}Message{info.structTypes}((EventId eventId, string formatString) parameters) => new {logLevel}Message{info.structTypes}(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> from the given <see cref=""ValueTuple{{Int32, String, String}}""/> parameters.
        /// </summary>
        /// <param name=""parameters"">The <see cref=""int""/> event id, <see cref=""string""/> event name, and format string to initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.</param>
        public static implicit operator {logLevel}Message{info.structTypes}((int eventId, string eventName, string formatString) parameters) => new {logLevel}Message{info.structTypes}(parameters.eventId, parameters.eventName, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> from the given <see cref=""ValueTuple{{Int32, String}}""/> parameters.
        /// </summary>
        /// <param name=""parameters"">The <see cref=""int""/> event id and format string to initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.</param>
        public static implicit operator {logLevel}Message{info.structTypes}((int eventId, string formatString) parameters) => new {logLevel}Message{info.structTypes}(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> from the given <see cref=""ValueTuple{{String, String}}""/> parameters.
        /// </summary>
        /// <param name=""parameters"">The <see cref=""string""/> event name and format string to initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.</param>
        public static implicit operator {logLevel}Message{info.structTypes}((string eventName, string formatString) parameters) => new {logLevel}Message{info.structTypes}(parameters.eventName, parameters.formatString);
    }}
")}
}}
";
                yield return (fileName, TrimEmptyLine(fileContent));
            }
        }

        public static IEnumerable<(string fileName, string fileContent)> GenerateLogLevelMessage()
        {
            var infos = Enumerable.Range(0, 7).Select(BuildInfo);

            var logLevel = "LogLevel";
            var description = "a";

            var fileName = $"LogLevelMessage.cs";
            var fileContent =
$@"// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.Logging
{{
{ForEach(EndOfLine, infos, info => $@"
    /// <summary>
    /// Represents {description} message which is pre-computed and strongly typed to reduce logging overhead.
    /// </summary>
{ForEach("", info.details, detail => $@"
    /// <typeparam name=""{detail.type}"">The type of the value in the {detail.position} position of the format string.</typeparam>
")}    public struct {logLevel}Message{info.structTypes}
    {{
        private readonly Action{info.actionTypes} _log;

        /// <summary>
        /// Initializes an instance of the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.
        /// </summary>
        /// <param name=""logLevel"">The <see cref=""LogLevel""/> associated with the log.</param>
        /// <param name=""eventId"">The event id associated with the log.</param>
        /// <param name=""formatString"">The named format string</param>
        public {logLevel}Message(LogLevel logLevel, EventId eventId, string formatString)
        {{
            _log = LoggerMessage.Define{info.structTypes}(logLevel, eventId, formatString);
        }}

        /// <summary>
        /// Initializes an instance of the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.
        /// </summary>
        /// <param name=""logLevel"">The <see cref=""LogLevel""/> associated with the log.</param>
        /// <param name=""eventId"">The event id associated with the log.</param>
        /// <param name=""eventName"">The event name associated with the log.</param>
        /// <param name=""formatString"">The named format string</param>
        public {logLevel}Message(LogLevel logLevel, int eventId, string eventName, string formatString)
        {{
            _log = LoggerMessage.Define{info.structTypes}(logLevel, new EventId(eventId, eventName), formatString);
        }}

        /// <summary>
        /// Initializes an instance of the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.
        /// </summary>
        /// <param name=""logLevel"">The <see cref=""LogLevel""/> associated with the log.</param>
        /// <param name=""eventId"">The event id associated with the log.</param>
        /// <param name=""formatString"">The named format string</param>
        public {logLevel}Message(LogLevel logLevel, int eventId, string formatString)
        {{
            _log = LoggerMessage.Define{info.structTypes}(logLevel, eventId, formatString);
        }}

        /// <summary>
        /// Initializes an instance of the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.
        /// </summary>
        /// <param name=""logLevel"">The <see cref=""LogLevel""/> associated with the log.</param>
        /// <param name=""eventName"">The event name associated with the log.</param>
        /// <param name=""formatString"">The named format string</param>
        public {logLevel}Message(LogLevel logLevel, string eventName, string formatString)
        {{
            _log = LoggerMessage.Define{info.structTypes}(logLevel, EventId.FromName(eventName), formatString);
        }}

        /// <summary>
        /// Formats and writes {description} log message.
        /// </summary>
        /// <param name=""logger"">The <see cref=""ILogger""/> to write to.</param>
{ForEach("", info.details, detail => $@"
        /// <param name=""{detail.arg}"">The value at the {detail.position} position in the format string.</param>
")}        public void Log({Join(", ", "ILogger logger", info.logParameters)}) => _log({Join(", ", "logger", info.logArguments, "default")});

        /// <summary>
        /// Formats and writes {description} log message with exception details.
        /// </summary>
        /// <param name=""logger"">The <see cref=""ILogger""/> to write to.</param>
        /// <param name=""exception"">The <see cref=""Exception""/> details to include with the log.</param>
{ForEach("", info.details, detail => $@"
        /// <param name=""{detail.arg}"">The value at the {detail.position} position in the format string.</param>
")}       public void Log({Join(", ", "ILogger logger", "Exception exception", info.logParameters)}) => _log({Join(", ", "logger", info.logArguments, "exception")});

        /// <summary>
        /// Implicitly initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> from the given <see cref=""ValueTuple{{EventId, String}}""/> parameters.
        /// </summary>
        /// <param name=""parameters"">The <see cref=""EventId""/> and format string to initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.</param>
        public static implicit operator {logLevel}Message{info.structTypes}((LogLevel logLevel, EventId eventId, string formatString) parameters) => new {logLevel}Message{info.structTypes}(parameters.logLevel, parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> from the given <see cref=""ValueTuple{{Int32, String, String}}""/> parameters.
        /// </summary>
        /// <param name=""parameters"">The <see cref=""int""/> event id, <see cref=""string""/> event name, and format string to initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.</param>
        public static implicit operator {logLevel}Message{info.structTypes}((LogLevel logLevel, int eventId, string eventName, string formatString) parameters) => new {logLevel}Message{info.structTypes}(parameters.logLevel, parameters.eventId, parameters.eventName, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> from the given <see cref=""ValueTuple{{Int32, String}}""/> parameters.
        /// </summary>
        /// <param name=""parameters"">The <see cref=""int""/> event id and format string to initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.</param>
        public static implicit operator {logLevel}Message{info.structTypes}((LogLevel logLevel, int eventId, string formatString) parameters) => new {logLevel}Message{info.structTypes}(parameters.logLevel, parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> from the given <see cref=""ValueTuple{{String, String}}""/> parameters.
        /// </summary>
        /// <param name=""parameters"">The <see cref=""string""/> event name and format string to initialize the <see cref=""{logLevel}Message{info.doccommentTypes}""/> struct.</param>
        public static implicit operator {logLevel}Message{info.structTypes}((LogLevel logLevel, string eventName, string formatString) parameters) => new {logLevel}Message{info.structTypes}(parameters.logLevel, parameters.eventName, parameters.formatString);
    }}
")}
}}
";
                yield return (fileName, TrimEmptyLine(fileContent));

        }
        private static object Join(string separator, params string[] strings)
        {
            return string.Join(separator, strings.Where(x => !string.IsNullOrEmpty(x)));
        }

        public static (
            int count,
            string structTypes,
            string actionTypes,
            string doccommentTypes,
            string logParameters,
            string logArguments,
            IEnumerable<(string type, string arg, string position)> details)
            BuildInfo(int count)
        {
            var positions = new[] { default, "first", "second", "third", "fourth", "fifth", "sixth" };
            var types = Enumerable.Range(1, count).Select(index => $"T{index}");
            var args = Enumerable.Range(1, count).Select(index => $"value{index}");
            var details = Enumerable.Range(1, count).Select(index => (type: $"T{index}", arg: $"value{index}", position: positions[index]));

            if (count == 0)
            {
                return (0, "", "<ILogger, Exception>", "", "", "", details);
            }

            string structTypes = $"<{string.Join(", ", types)}>";
            string doccommentTypes = $"{{{string.Join(", ", types)}}}";
            string actionTypes = $"<ILogger, {string.Join(", ", types)}, Exception>";
            string logParameters = string.Join(", ", details.Select(detail => $"{detail.type} {detail.arg}"));
            string logArguments = string.Join(", ", details.Select(detail => $"{detail.arg}"));

            return (count, structTypes, actionTypes, doccommentTypes, logParameters, logArguments, details);
        }

        public static string ForEach<T>(string separator, IEnumerable<T> enumerable, Func<T, string> generator)
        {
            return string.Join(separator, enumerable.Select(generator).Select(TrimEmptyLine));
        }

        private static string TrimEmptyLine(string content)
        {
            if (content.StartsWith(EndOfLine))
            {
                content = content.Substring(EndOfLine.Length);
            }
            if (content.EndsWith(EndOfLine + EndOfLine))
            {
                content = content.Substring(0, content.Length - EndOfLine.Length);
            }
            return content;
        }
    }
}
