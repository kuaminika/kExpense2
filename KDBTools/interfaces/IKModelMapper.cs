using MySql.Data.MySqlClient;

namespace KDBTools.Repository.interfaces
{
    public interface IKModelMapper
    {
        void map(IKDataReader kdt);
    }
}