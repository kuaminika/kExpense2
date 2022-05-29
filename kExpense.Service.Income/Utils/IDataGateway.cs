using KDBAbstractions.Repository.interfaces;
using System;
using System.Collections.Generic;

namespace kExpense.Service.Income.Utils
{
    public interface IDataGateway
    {
        T ExecuteReadOneQuery<T>(string query);
        KWriteResult Execute(string query);
        KWriteResult ExecuteInsert(string query);
        List<T> ExecuteReadManyResult<T>(string query);

       void ExecuteScalar(string query,IKModelMapper mapper);

    }





}
