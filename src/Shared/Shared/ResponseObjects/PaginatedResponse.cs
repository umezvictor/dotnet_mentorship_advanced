namespace Shared.ResponseObjects;
public class PaginatedResponse<T> where T : class
{
	public T? Data { get; set; }
	public long TotalCount { get; set; }
	public int PageSize { get; set; }

}
