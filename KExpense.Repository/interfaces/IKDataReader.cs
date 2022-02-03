namespace KExpense.Repository.interfaces
{
    public interface IKDataReader
    {
        int GetInt(string iD_COLUMN);
        bool Read();
        string GetString(string aDDRESS_COLUMN);
        decimal GetDecimal(string v);
    }
}