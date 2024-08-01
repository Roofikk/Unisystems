namespace Unisystems.BuildingAccount.WebApi.Models;

public class PaginationModel
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
}
