namespace KExpense.Repository.interfaces
{
    public interface IKLogTool
    {
        void Log(string msg);
        void LogObj<T>(T obj);
    }
}
