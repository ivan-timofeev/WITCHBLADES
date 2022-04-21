namespace Witchblades.Backend.Api.Utils.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public string ConfigurationPath { get; private set; }
        public string? ConfigurationExample { get; private set; }

        public InvalidConfigurationException(string configurationPath)
            : base($"Invalid configuration of {configurationPath}")
        {
            ConfigurationPath = configurationPath;
        }

        public InvalidConfigurationException(
            string configurationPath,
            string configurationExample)
            : base($"Invalid configuration of {configurationPath}\n" +
                  $"Example of the correct configuration: {configurationExample}")
        {
            ConfigurationPath = configurationPath;
            ConfigurationExample = configurationExample;
        }
    }
}
