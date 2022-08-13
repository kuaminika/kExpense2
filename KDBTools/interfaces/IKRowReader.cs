namespace KDBTools.Repository.interfaces
{
    public interface IKRowReader
    {
        bool YieldedResults { get; }
        bool Read();

    }
}