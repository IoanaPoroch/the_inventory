namespace Presentation.DTOs.Requests
{
    public record PaginationQuery(int Page = 1, int PageSize = 10);
}
