
using System.Collections.Generic;

namespace KDBTools.Repository.interfaces
{
    public interface IKRepository<T>
    {
        List<T> GetAll(int org_id =0,string sortby="id");     
        T Record(T newRecord);
        int DeleteRecord(T victim);
        int DeleteRecordById(T victim);
        T GetById(int id);
        int UpdateRecord(T first);
    }
}
