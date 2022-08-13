using KDBTools.interfaces;
using System.Collections.Generic;

namespace kExpense2.service.Usager
{
    public struct UsagerServiceArgs
    {
        public IUsagerRepository Repo { get; internal set; }
        public IKonfig Configs { get; internal set; }
        public IKLogTool LogTool { get; internal set; }
    }
    public class UsagerService : IUsagerService
    { 
        private IUsagerRepository repo;

        public UsagerService(UsagerServiceArgs args)
        { 
            this.repo = args.Repo;
        }

        public List<UsagerModel> Get(UsagerModel query)
        {
            List<UsagerModel> result =  this.repo.GetSomeLikeThis(query);
            return result;
        }
    }
}
