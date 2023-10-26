using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGEChallengeApp.Core.Interfaces;
using TGEChallengeApp.Core.Services.DataAccess;
using TGEChallengeApp.Core.Services;

namespace Tests
{
    public class Startup
    {
        public Startup()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
               .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<IDataAccessService, DataAccessService>();
            serviceCollection.AddSingleton<ITGEService, TGEService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
