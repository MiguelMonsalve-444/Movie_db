namespace Movie_db;

public class PageResult<T>
{
    public List<T> Values {get;}
    public int TotalCount {get;}

    public PageResult(List<T>values, int totalCount)
    {
        Values = values;
        TotalCount = totalCount;

    }
}