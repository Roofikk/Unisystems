namespace Unisystems.ClassroomAccount.WebApi.Models;

public class PaginationModel
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
