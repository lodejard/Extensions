// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// Represents a critical message which is pre-computed and strongly typed to reduce logging overhead.
    /// </summary>
    public struct CriticalMessage
    {
        private readonly Action<ILogger, Exception> _log;

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(EventId eventId, string formatString)
        {
            _log = LoggerMessage.Define(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string eventName, string formatString)
        {
            _log = LoggerMessage.Define(LogLevel.Critical, new EventId(eventId, eventName), formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string formatString)
        {
            _log = LoggerMessage.Define(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage"/> struct.
        /// </summary>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(string eventName, string formatString)
        {
            _log = LoggerMessage.Define(LogLevel.Critical, EventId.FromName(eventName), formatString);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        public void Log(ILogger logger) => _log(logger, default);

        /// <summary>
        /// Formats and writes a critical log message with exception details.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The <see cref="Exception"/> details to include with the log.</param>
       public void Log(ILogger logger, Exception exception) => _log(logger, exception);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage"/> from the given <see cref="ValueTuple{EventId, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="EventId"/> and format string to initialize the <see cref="CriticalMessage"/> struct.</param>
        public static implicit operator CriticalMessage((EventId eventId, string formatString) parameters) => new CriticalMessage(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage"/> from the given <see cref="ValueTuple{Int32, String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id, <see cref="string"/> event name, and format string to initialize the <see cref="CriticalMessage"/> struct.</param>
        public static implicit operator CriticalMessage((int eventId, string eventName, string formatString) parameters) => new CriticalMessage(parameters.eventId, parameters.eventName, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage"/> from the given <see cref="ValueTuple{Int32, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id and format string to initialize the <see cref="CriticalMessage"/> struct.</param>
        public static implicit operator CriticalMessage((int eventId, string formatString) parameters) => new CriticalMessage(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage"/> from the given <see cref="ValueTuple{String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="string"/> event name and format string to initialize the <see cref="CriticalMessage"/> struct.</param>
        public static implicit operator CriticalMessage((string eventName, string formatString) parameters) => new CriticalMessage(parameters.eventName, parameters.formatString);
    }

    /// <summary>
    /// Represents a critical message which is pre-computed and strongly typed to reduce logging overhead.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first position of the format string.</typeparam>
    public struct CriticalMessage<T1>
    {
        private readonly Action<ILogger, T1, Exception> _log;

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(EventId eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1>(LogLevel.Critical, new EventId(eventId, eventName), formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1}"/> struct.
        /// </summary>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1>(LogLevel.Critical, EventId.FromName(eventName), formatString);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        public void Log(ILogger logger, T1 value1) => _log(logger, value1, default);

        /// <summary>
        /// Formats and writes a critical log message with exception details.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The <see cref="Exception"/> details to include with the log.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
       public void Log(ILogger logger, Exception exception, T1 value1) => _log(logger, value1, exception);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1}"/> from the given <see cref="ValueTuple{EventId, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="EventId"/> and format string to initialize the <see cref="CriticalMessage{T1}"/> struct.</param>
        public static implicit operator CriticalMessage<T1>((EventId eventId, string formatString) parameters) => new CriticalMessage<T1>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1}"/> from the given <see cref="ValueTuple{Int32, String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id, <see cref="string"/> event name, and format string to initialize the <see cref="CriticalMessage{T1}"/> struct.</param>
        public static implicit operator CriticalMessage<T1>((int eventId, string eventName, string formatString) parameters) => new CriticalMessage<T1>(parameters.eventId, parameters.eventName, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1}"/> from the given <see cref="ValueTuple{Int32, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id and format string to initialize the <see cref="CriticalMessage{T1}"/> struct.</param>
        public static implicit operator CriticalMessage<T1>((int eventId, string formatString) parameters) => new CriticalMessage<T1>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1}"/> from the given <see cref="ValueTuple{String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="string"/> event name and format string to initialize the <see cref="CriticalMessage{T1}"/> struct.</param>
        public static implicit operator CriticalMessage<T1>((string eventName, string formatString) parameters) => new CriticalMessage<T1>(parameters.eventName, parameters.formatString);
    }

    /// <summary>
    /// Represents a critical message which is pre-computed and strongly typed to reduce logging overhead.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first position of the format string.</typeparam>
    /// <typeparam name="T2">The type of the value in the second position of the format string.</typeparam>
    public struct CriticalMessage<T1, T2>
    {
        private readonly Action<ILogger, T1, T2, Exception> _log;

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(EventId eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2>(LogLevel.Critical, new EventId(eventId, eventName), formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2}"/> struct.
        /// </summary>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2>(LogLevel.Critical, EventId.FromName(eventName), formatString);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
        public void Log(ILogger logger, T1 value1, T2 value2) => _log(logger, value1, value2, default);

        /// <summary>
        /// Formats and writes a critical log message with exception details.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The <see cref="Exception"/> details to include with the log.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
       public void Log(ILogger logger, Exception exception, T1 value1, T2 value2) => _log(logger, value1, value2, exception);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2}"/> from the given <see cref="ValueTuple{EventId, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="EventId"/> and format string to initialize the <see cref="CriticalMessage{T1, T2}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2>((EventId eventId, string formatString) parameters) => new CriticalMessage<T1, T2>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2}"/> from the given <see cref="ValueTuple{Int32, String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id, <see cref="string"/> event name, and format string to initialize the <see cref="CriticalMessage{T1, T2}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2>((int eventId, string eventName, string formatString) parameters) => new CriticalMessage<T1, T2>(parameters.eventId, parameters.eventName, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2}"/> from the given <see cref="ValueTuple{Int32, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id and format string to initialize the <see cref="CriticalMessage{T1, T2}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2>((int eventId, string formatString) parameters) => new CriticalMessage<T1, T2>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2}"/> from the given <see cref="ValueTuple{String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="string"/> event name and format string to initialize the <see cref="CriticalMessage{T1, T2}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2>((string eventName, string formatString) parameters) => new CriticalMessage<T1, T2>(parameters.eventName, parameters.formatString);
    }

    /// <summary>
    /// Represents a critical message which is pre-computed and strongly typed to reduce logging overhead.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first position of the format string.</typeparam>
    /// <typeparam name="T2">The type of the value in the second position of the format string.</typeparam>
    /// <typeparam name="T3">The type of the value in the third position of the format string.</typeparam>
    public struct CriticalMessage<T1, T2, T3>
    {
        private readonly Action<ILogger, T1, T2, T3, Exception> _log;

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(EventId eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3>(LogLevel.Critical, new EventId(eventId, eventName), formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3}"/> struct.
        /// </summary>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3>(LogLevel.Critical, EventId.FromName(eventName), formatString);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
        /// <param name="value3">The value at the third position in the format string.</param>
        public void Log(ILogger logger, T1 value1, T2 value2, T3 value3) => _log(logger, value1, value2, value3, default);

        /// <summary>
        /// Formats and writes a critical log message with exception details.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The <see cref="Exception"/> details to include with the log.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
        /// <param name="value3">The value at the third position in the format string.</param>
       public void Log(ILogger logger, Exception exception, T1 value1, T2 value2, T3 value3) => _log(logger, value1, value2, value3, exception);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3}"/> from the given <see cref="ValueTuple{EventId, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="EventId"/> and format string to initialize the <see cref="CriticalMessage{T1, T2, T3}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3>((EventId eventId, string formatString) parameters) => new CriticalMessage<T1, T2, T3>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3}"/> from the given <see cref="ValueTuple{Int32, String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id, <see cref="string"/> event name, and format string to initialize the <see cref="CriticalMessage{T1, T2, T3}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3>((int eventId, string eventName, string formatString) parameters) => new CriticalMessage<T1, T2, T3>(parameters.eventId, parameters.eventName, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3}"/> from the given <see cref="ValueTuple{Int32, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id and format string to initialize the <see cref="CriticalMessage{T1, T2, T3}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3>((int eventId, string formatString) parameters) => new CriticalMessage<T1, T2, T3>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3}"/> from the given <see cref="ValueTuple{String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="string"/> event name and format string to initialize the <see cref="CriticalMessage{T1, T2, T3}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3>((string eventName, string formatString) parameters) => new CriticalMessage<T1, T2, T3>(parameters.eventName, parameters.formatString);
    }

    /// <summary>
    /// Represents a critical message which is pre-computed and strongly typed to reduce logging overhead.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first position of the format string.</typeparam>
    /// <typeparam name="T2">The type of the value in the second position of the format string.</typeparam>
    /// <typeparam name="T3">The type of the value in the third position of the format string.</typeparam>
    /// <typeparam name="T4">The type of the value in the fourth position of the format string.</typeparam>
    public struct CriticalMessage<T1, T2, T3, T4>
    {
        private readonly Action<ILogger, T1, T2, T3, T4, Exception> _log;

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(EventId eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4>(LogLevel.Critical, new EventId(eventId, eventName), formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4}"/> struct.
        /// </summary>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4>(LogLevel.Critical, EventId.FromName(eventName), formatString);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
        /// <param name="value3">The value at the third position in the format string.</param>
        /// <param name="value4">The value at the fourth position in the format string.</param>
        public void Log(ILogger logger, T1 value1, T2 value2, T3 value3, T4 value4) => _log(logger, value1, value2, value3, value4, default);

        /// <summary>
        /// Formats and writes a critical log message with exception details.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The <see cref="Exception"/> details to include with the log.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
        /// <param name="value3">The value at the third position in the format string.</param>
        /// <param name="value4">The value at the fourth position in the format string.</param>
       public void Log(ILogger logger, Exception exception, T1 value1, T2 value2, T3 value3, T4 value4) => _log(logger, value1, value2, value3, value4, exception);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4}"/> from the given <see cref="ValueTuple{EventId, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="EventId"/> and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4>((EventId eventId, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4}"/> from the given <see cref="ValueTuple{Int32, String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id, <see cref="string"/> event name, and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4>((int eventId, string eventName, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4>(parameters.eventId, parameters.eventName, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4}"/> from the given <see cref="ValueTuple{Int32, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4>((int eventId, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4}"/> from the given <see cref="ValueTuple{String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="string"/> event name and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4>((string eventName, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4>(parameters.eventName, parameters.formatString);
    }

    /// <summary>
    /// Represents a critical message which is pre-computed and strongly typed to reduce logging overhead.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first position of the format string.</typeparam>
    /// <typeparam name="T2">The type of the value in the second position of the format string.</typeparam>
    /// <typeparam name="T3">The type of the value in the third position of the format string.</typeparam>
    /// <typeparam name="T4">The type of the value in the fourth position of the format string.</typeparam>
    /// <typeparam name="T5">The type of the value in the fifth position of the format string.</typeparam>
    public struct CriticalMessage<T1, T2, T3, T4, T5>
    {
        private readonly Action<ILogger, T1, T2, T3, T4, T5, Exception> _log;

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(EventId eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4, T5>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4, T5>(LogLevel.Critical, new EventId(eventId, eventName), formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4, T5>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> struct.
        /// </summary>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4, T5>(LogLevel.Critical, EventId.FromName(eventName), formatString);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
        /// <param name="value3">The value at the third position in the format string.</param>
        /// <param name="value4">The value at the fourth position in the format string.</param>
        /// <param name="value5">The value at the fifth position in the format string.</param>
        public void Log(ILogger logger, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5) => _log(logger, value1, value2, value3, value4, value5, default);

        /// <summary>
        /// Formats and writes a critical log message with exception details.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The <see cref="Exception"/> details to include with the log.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
        /// <param name="value3">The value at the third position in the format string.</param>
        /// <param name="value4">The value at the fourth position in the format string.</param>
        /// <param name="value5">The value at the fifth position in the format string.</param>
       public void Log(ILogger logger, Exception exception, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5) => _log(logger, value1, value2, value3, value4, value5, exception);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> from the given <see cref="ValueTuple{EventId, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="EventId"/> and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4, T5>((EventId eventId, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4, T5>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> from the given <see cref="ValueTuple{Int32, String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id, <see cref="string"/> event name, and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4, T5>((int eventId, string eventName, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4, T5>(parameters.eventId, parameters.eventName, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> from the given <see cref="ValueTuple{Int32, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4, T5>((int eventId, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4, T5>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> from the given <see cref="ValueTuple{String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="string"/> event name and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4, T5>((string eventName, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4, T5>(parameters.eventName, parameters.formatString);
    }

    /// <summary>
    /// Represents a critical message which is pre-computed and strongly typed to reduce logging overhead.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first position of the format string.</typeparam>
    /// <typeparam name="T2">The type of the value in the second position of the format string.</typeparam>
    /// <typeparam name="T3">The type of the value in the third position of the format string.</typeparam>
    /// <typeparam name="T4">The type of the value in the fourth position of the format string.</typeparam>
    /// <typeparam name="T5">The type of the value in the fifth position of the format string.</typeparam>
    /// <typeparam name="T6">The type of the value in the sixth position of the format string.</typeparam>
    public struct CriticalMessage<T1, T2, T3, T4, T5, T6>
    {
        private readonly Action<ILogger, T1, T2, T3, T4, T5, T6, Exception> _log;

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(EventId eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4, T5, T6>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4, T5, T6>(LogLevel.Critical, new EventId(eventId, eventName), formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> struct.
        /// </summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(int eventId, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4, T5, T6>(LogLevel.Critical, eventId, formatString);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> struct.
        /// </summary>
        /// <param name="eventName">The event name associated with the log.</param>
        /// <param name="formatString">The named format string</param>
        public CriticalMessage(string eventName, string formatString)
        {
            _log = LoggerMessage.Define<T1, T2, T3, T4, T5, T6>(LogLevel.Critical, EventId.FromName(eventName), formatString);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
        /// <param name="value3">The value at the third position in the format string.</param>
        /// <param name="value4">The value at the fourth position in the format string.</param>
        /// <param name="value5">The value at the fifth position in the format string.</param>
        /// <param name="value6">The value at the sixth position in the format string.</param>
        public void Log(ILogger logger, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6) => _log(logger, value1, value2, value3, value4, value5, value6, default);

        /// <summary>
        /// Formats and writes a critical log message with exception details.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The <see cref="Exception"/> details to include with the log.</param>
        /// <param name="value1">The value at the first position in the format string.</param>
        /// <param name="value2">The value at the second position in the format string.</param>
        /// <param name="value3">The value at the third position in the format string.</param>
        /// <param name="value4">The value at the fourth position in the format string.</param>
        /// <param name="value5">The value at the fifth position in the format string.</param>
        /// <param name="value6">The value at the sixth position in the format string.</param>
       public void Log(ILogger logger, Exception exception, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6) => _log(logger, value1, value2, value3, value4, value5, value6, exception);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> from the given <see cref="ValueTuple{EventId, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="EventId"/> and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4, T5, T6>((EventId eventId, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4, T5, T6>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> from the given <see cref="ValueTuple{Int32, String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id, <see cref="string"/> event name, and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4, T5, T6>((int eventId, string eventName, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4, T5, T6>(parameters.eventId, parameters.eventName, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> from the given <see cref="ValueTuple{Int32, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="int"/> event id and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4, T5, T6>((int eventId, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4, T5, T6>(parameters.eventId, parameters.formatString);

        /// <summary>
        /// Implicitly initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> from the given <see cref="ValueTuple{String, String}"/> parameters.
        /// </summary>
        /// <param name="parameters">The <see cref="string"/> event name and format string to initialize the <see cref="CriticalMessage{T1, T2, T3, T4, T5, T6}"/> struct.</param>
        public static implicit operator CriticalMessage<T1, T2, T3, T4, T5, T6>((string eventName, string formatString) parameters) => new CriticalMessage<T1, T2, T3, T4, T5, T6>(parameters.eventName, parameters.formatString);
    }

}
