using KExpense.Repository.kModelMappers;
using System;
using System.Collections.Generic;

namespace KExpense.Repository.interfaces
{
    public abstract class AKDBAbstraction
    {
        public AKDBAbstraction(string connString)
        {
            this.ConnectionString = connString;
        }

        public string ConnectionString { get; }

        public abstract void ExecuteWriteTransaction(string query, IKModelMapper mapper);
        public abstract void ExecuteReadTransaction(string query, IKModelMapper mapper);

        public abstract void ExecuteReadSPTranasaction(string name, List<KSP_Param> lists, IKModelMapper mapper);
    }

}