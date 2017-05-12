using System;
using System.Reflection;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace Nameless.Skeleton.Framework.Logging.Impl {

    /// <summary>
    /// log4net implementation of <see cref="ILoggerFactory"/>
    /// </summary>
    public sealed class LoggerFactory : ILoggerFactory {

        #region Public Constants Fields

        /// <summary>
        /// Gets the default log4net configuration file name.
        /// </summary>
        public const string DefaultConfigFileName = "log4net.config";

        #endregion

        #region Private Static Fields

        // Logger factory should be watch
        // only one file and be attached to only
        // one configuration file.
        private static bool _configureAndWatchReady;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="LoggerFactory"/>
        /// </summary>
        /// <param name="configFileName">The configuration file name.</param>
        public LoggerFactory(string configFileName = DefaultConfigFileName) {
            if (_configureAndWatchReady) { return; }

            var file = string.IsNullOrWhiteSpace(configFileName)
                ? DefaultConfigFileName
                : configFileName;
            var repository = LogManager.GetRepository(typeof(LoggerFactory).GetTypeInfo().Assembly);
            XmlConfigurator.ConfigureAndWatch(repository, GetConfigFile(file));

            _configureAndWatchReady = true;
        }

        #endregion

        #region Private Static Methods

        private static FileInfo GetConfigFile(string configFilePath) {
            return (!Path.IsPathRooted(configFilePath)
                ? new FileInfo(Path.Combine(typeof(LoggerFactory).GetTypeInfo().Assembly.GetDirectoryPath(), configFilePath))
                : new FileInfo(configFilePath));
        }

        #endregion

        #region ILoggerFactory Members

        /// <inheritdoc />
        public ILogger CreateLogger(Type type) {
            return new Logger(LogManager.GetLogger(type));
        }

        #endregion
    }
}