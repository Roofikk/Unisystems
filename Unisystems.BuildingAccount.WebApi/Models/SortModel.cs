using Microsoft.AspNetCore.Mvc;

namespace Unisystems.BuildingAccount.WebApi.Models;

public class SortModel
{
    public string? SortBy { get; set; }
    public string Direction { get; set; } = "asc";
}
