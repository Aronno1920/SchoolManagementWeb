using System.Configuration;

namespace CoreLibrary
{
    public static class ConfigReader
    {
        public static string AppConnString
        {
            get
            {
                string strConnectionString = "";
                if (ConfigurationManager.ConnectionStrings["ConString"] != null)
                {
                    return ConfigurationManager.ConnectionStrings["ConString"].ToString();
                }
                return strConnectionString;
            }
        }
    }
}
