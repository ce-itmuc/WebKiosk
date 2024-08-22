namespace WebKiosk
{
    using log4net;
    using System.Configuration;
    using System.Windows;

    public partial class App : Application
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Application));

        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            log.Info("Web kiosk started");

            LoadConfiguration();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            log.Info("Web kiosk stopped");
        }

        private static void LoadConfiguration()
        {
            var model = WpfUtilities.GetResource<KioskModel>(nameof(KioskModel)) ?? throw new ApplicationException($"{nameof(KioskModel)} not found");
            var url = LoadConfigurationValue("Url") ?? throw new ApplicationException($"URL not set in application configuration");

            log.Info($"URL: {url}");
            model.Url = url;
        }

        private static string LoadConfigurationValue (string key)
        {
            try
            {
                var value = ConfigurationManager.AppSettings["Url"];
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ConfigurationErrorsException($"Value is null: {key}");
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException($"Cannot read value from configuration file: {key}", ex);
            }
        }
    }
}
