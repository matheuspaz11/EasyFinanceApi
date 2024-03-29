namespace EasyFinanceApi.Helpers.Util
{
    public class ConfigHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int GetDefaultUserId()
        {
            int userId = _configuration.GetValue<int>("AppSettings:DefaultUserId");

            return userId;
        }
    }
}
