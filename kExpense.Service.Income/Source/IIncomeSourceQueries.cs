namespace kExpense.Service.Income.Source
{
    public interface IIncomeSourceQueries
    {
        string FindSourceLikeThis<T>(T parameters);
        string InsertSourceDetails<T>(T parameters);
        string FindSourceDetails<T>(T parameters);
        string InsertSource<T>(T parameters);
        string FindSourcesLikeThis<T>(T args);
    }

  



}
