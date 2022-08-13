
using System.Collections.Generic;

namespace KDBTools.Repository.interfaces
{
    public  interface IKReadRepo<T>
    {
        List<T> Get(T queryObj);
    }

    public class NoReadRepo<T> : IKReadRepo<T>
    {
        public List<T> Get(T queryObj)
        {
            List<T> reslt = new List<T>();
            reslt.Add(queryObj);
            return reslt;
        }
    }

}
