namespace Unisystems.BuildingAccount.WebApi.Models;

public class PaginationModel
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
}
