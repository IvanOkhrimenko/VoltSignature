using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using VoltSignature.Model.Settings;

namespace VoltSignature.Core.Logger
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly LoggerSetting _section;
        private readonly string _enviroment;

        public LoggerProvider(IConfigurationSection section,string enviroment)
        {
            _section = section.Get<LoggerSetting>();
            _enviroment = enviroment;
        }
        public ILogger CreateLogger(string categoryName)
        {
            if(_section.LoggerType== LoggerTypes.FILE)
                return new FileLogger(_section.FilePath);
            if (_section.LoggerType == LoggerTypes.MONGO)
                throw new NotImplementedException();
            return new DevLogger(_enviroment);
        }

        public void Dispose()
        {
        }
    }
}
