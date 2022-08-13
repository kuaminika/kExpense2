namespace KDBTools.Repository.interfaces
{
    public interface IKDataReader: IKRowReader
    {
        int GetInt(string iD_COLUMN);
        string GetString(string aDDRESS_COLUMN);
        decimal GetDecimal(string v);
    }
}