
using System.Collections.Generic;

namespace KDBTools.Repository.interfaces
{
    // TODO : confirm if AKDBAbstraction should be inside a separate project
    public abstract class AKDBAbstraction
    {
        public AKDBAbstraction(string connString)
        {
            this.ConnectionString = connString;
        }

        public string ConnectionString { get; }

        public abstract KWriteResult ExecuteWriteTransaction(string query);
        public abstract void ExecuteReadTransaction(string query, IKModelMapper mapper);

        public abstract void ExecuteReadSPTranasaction(string name, List<KSP_Param> lists, IKModelMapper mapper);
    }


    public struct KWriteResult
    {
        public int AffectedRowCount { get; internal set; }
        public long LastInsertedId { get; internal set; }
    }
}