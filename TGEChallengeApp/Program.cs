using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TGEChallengeApp;
using TGEChallengeApp.Core.Interfaces;
using TGEChallengeApp.Core.Services;
using TGEChallengeApp.Core.Services.DataAccess;
using TGEChallengeApp.DataAccess;
using TGEChallengeApp.DataAccess.API;
using TGEChallengeApp.Interfaces;


class Program
{
    static void Main()
    {

        IHost _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddScoped<IDataAccessService, DataAccessService>();
            services.AddScoped<ITGEService, TGEService>();
            services.AddScoped<IDummyTGEChallengeAPI,  DummyTGEChallengeAPI>();
            services.AddScoped<ITGEChallengeAPIService, TGEChallengeAPIService>();

            services.AddScoped<IPostcodeManager, PostcodeManager>();

            services.AddScoped<ITGEChallengeApp, TGEChallengeAppRunner>();
        }).Build();

        var app = _host.Services.GetRequiredService<ITGEChallengeApp>();

        app.Run();

    }



}



