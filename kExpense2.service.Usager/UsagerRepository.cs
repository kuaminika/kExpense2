using Dapper;
using KDBTools.interfaces;
using KDBTools.Repository.interfaces;
using System.Collections.Generic;

namespace kExpense2.service.Usager
{
    public interface IUsagerRepository
    {
        List<UsagerModel> GetSomeLikeThis(UsagerModel query);
    }

    public struct UsaerRepositoryArgs
    {
        public IKLogTool LogTool { get; internal set; }
        public QuerySet QuerySet { get; internal set; }
        public IDataGateway DataGateWay { get; internal set; }
    }

    internal class JustLogRepo : IUsagerRepository
    {
        private IKLogTool logTool;
        private QuerySet querySet;
        private IDataGateway dataGateway;

        public JustLogRepo(UsaerRepositoryArgs args)
        {
            this.logTool = args.LogTool;
            this.querySet = args.QuerySet;
            this.dataGateway = args.DataGateWay;
        }
        public List<UsagerModel> GetSomeLikeThis(UsagerModel query)
        {
            logTool.Log($"Entering {this.GetType()}.GetUsagers");
            DynamicParameters dp = dataGateway.FindDynamicParams(query);
            string queryStr = querySet.GetUsagersLikeThis(dp);
            logTool.Log($"would've been executing \n {queryStr}");
            List<UsagerModel> result = new List<UsagerModel>();
            logTool.Log($"Leaving and returning empty list {this.GetType()}.GetUsagers");
            return result;
        }
    }





    public interface IUsagerService
    {

        List<UsagerModel> Get(UsagerModel query);
    
    }
  

    internal class UsagerRepository: IUsagerRepository
    {
        IDataGateway dataGateway;
        IKLogTool logTool;
        private QuerySet querySet;

        public UsagerRepository(UsaerRepositoryArgs args)
        {
            this.logTool = args.LogTool;
            this.querySet = args.QuerySet;
            this.dataGateway = args.DataGateWay;
        }


        public List<UsagerModel> GetSomeLikeThis(UsagerModel query)
        {
            logTool.Log($"Entering {this.GetType()}.GetUsagers");
            DynamicParameters dp = dataGateway.FindDynamicParams(query);
            string queryStr = querySet.GetUsagersLikeThis(dp);
            logTool.Log($"executing \n {queryStr}");
            List<UsagerModel> result = dataGateway.ExecuteReadManyResult<UsagerModel>(queryStr);
            logTool.Log($"Leaving {this.GetType()}.GetUsagers");
            return result;
        }

    }
}
