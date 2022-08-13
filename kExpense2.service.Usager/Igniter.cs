using Microsoft.Extensions.Configuration;

namespace kExpense2.service.Usager
{
    public static class Igniter
    { 
        public static IUsagerService Ignite(IConfiguration configuration)
        {
            KDBTools.Konfigs_default config = new KDBTools.Konfigs_default(configuration);
            IUsagerService result = UsagerServiceFactory.New(config).Create;
            return result;
        }
    }
}
