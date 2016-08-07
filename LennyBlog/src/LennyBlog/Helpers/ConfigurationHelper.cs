using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Helpers
{
    public class ConfigurationHelper
    {
        /// <summary>
        /// 得到Configuration
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static IConfiguration GetConfiguration(string fileName)
        {
            var expectedBasePath = Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.SetBasePath(expectedBasePath).AddJsonFile(fileName);

            configurationBuilder.AddEnvironmentVariables();
            IConfiguration Configuration = configurationBuilder.Build();

            return Configuration;
        }
    }
}
