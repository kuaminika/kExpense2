using MySql.Data.MySqlClient;

namespace KExpense.Repository.interfaces
{
    public interface IKModelMapper
    {
        void map(IKDataReader kdt);
    }
}