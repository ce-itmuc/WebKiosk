namespace WebKiosk
{
    using System.Windows;

    internal static class WpfUtilities
    {
        public static T? GetResource<T>(string resourceName) where T : class
        {
            return Application.Current.TryFindResource(resourceName) as T;
        }
    }
}
