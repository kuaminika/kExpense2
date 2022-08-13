
using System.Collections.Generic;

namespace KDBTools.Repository.interfaces
{
    public interface IDataGateway
    {
        T ExecuteReadOneQuery<T>(string query);
        KWriteResult Execute(string query);
        KWriteResult ExecuteInsert(string query);
        List<T> ExecuteReadManyResult<T>(string query);

        void ExecuteScalar(string query, IKModelMapper mapper);

    }


    public static class DataGatewayExtensions{


        public static Dapper.DynamicParameters FindDynamicParams<T>(this IDataGateway dataGateway,T q)
        {
            var map = KSqlMapper.Create;
            Dapper.DynamicParameters reslt = new Dapper.DynamicParameters();
            foreach (var p in q.GetType().GetProperties())
            {
                if (!map.Has(p.PropertyType)) continue;
                reslt.Add(p.Name, p.GetValue(q), map.Get(p.PropertyType));
            }

            return reslt;
        }
    }
}
