namespace HomeSevice.Model
{
    public class DataAccess
    {
        public static string GetConnection()
        {
            var getConfiguration = GetConfiguration();
            string data = getConfiguration.GetSection("Data").GetSection("ConnectionString").Value;
            return data;
        }
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
