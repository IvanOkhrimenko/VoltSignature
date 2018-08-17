using Microsoft.Extensions.Logging;
using System;

namespace VoltSignature.Core.Logger
{
    public class DevLogger : ILogger
    {
        private readonly string _enviroment;

        public DevLogger(string enviroment)
        {
            _enviroment = enviroment;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _enviroment.ToLower() == "development";
        }

        private ConsoleColor GetColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error:
                case LogLevel.Critical:
                    return ConsoleColor.Red;

                case LogLevel.Warning:
                    return ConsoleColor.Yellow;

                case LogLevel.Information:
                    return ConsoleColor.Green;

                default: return ConsoleColor.White;
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var color = Console.ForegroundColor;
            Console.ForegroundColor = GetColor(logLevel);
            Console.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {formatter(state, exception)}");
            Console.ForegroundColor = color;
        }
    }
}