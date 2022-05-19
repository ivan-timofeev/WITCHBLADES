namespace Witchblades.Exceptions
{
    public class MissingConfigurationException : Exception
    {
        public string ConfigurationPath { get; private set; }

        public MissingConfigurationException(string configurationPath)
            : base($"Configuration of {configurationPath} not found")
        {
            ConfigurationPath = configurationPath;
        }
    }
}
