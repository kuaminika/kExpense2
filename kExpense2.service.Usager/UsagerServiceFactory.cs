using KDBTools.interfaces;
using System;

namespace kExpense2.service.Usager
{
    public interface IUsagerSeviceFactory
    {
        IUsagerService Create { get; }
    }
    public class UsagerServiceFactory: IUsagerSeviceFactory
    {
        private IKonfig configs;

        public UsagerServiceFactory(IKonfig konfig)
        {
            this.configs = konfig;
            
        }

        public IUsagerService Create
        {
            get {

                UsaerRepositoryArgs repoArgs = new UsaerRepositoryArgs();
  
                repoArgs.DataGateWay = new KDBTools.Repository.DataGateway(configs.ConnectionString);
                repoArgs.QuerySet = new QuerySet();
                //TODO: find a way to not hardcode things using config

                string logClassName = configs.GetOrDefault("LogToolType", typeof(KDBTools.KLogTool).Name);
                logClassName = $"{typeof(KDBTools.KLogTool).Namespace}.{logClassName}, {typeof(KDBTools.KLogTool).Assembly.FullName}";
                repoArgs.LogTool =(IKLogTool) Activator.CreateInstance(Type.GetType(logClassName) );

                IUsagerRepository repo = new UsagerRepository(repoArgs);
                UsagerServiceArgs args = new UsagerServiceArgs { Repo = repo,Configs = configs, LogTool = repoArgs.LogTool};

                IUsagerService result = new UsagerService(args);
                return result;




            }

        }



        public static IUsagerSeviceFactory New(IKonfig configs)
        {
            Type specimentType = typeof(UsagerServiceFactory);
            string className = configs.GetOrDefault("UsagerServiceFactoryType", specimentType.Name);        
            string finaleName = $"{specimentType.Namespace}.{className}"; 
            Type type = Type.GetType(finaleName);
            IUsagerSeviceFactory result = (IUsagerSeviceFactory)Activator.CreateInstance(type, new object[] { configs }); 
            return result;
        }
    }
}
