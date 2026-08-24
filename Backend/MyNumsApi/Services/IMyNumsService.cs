using MyNumsApi.Models;

namespace MyNumsApi.Services;

public interface IMyNumsService
{
    Task<List<Num>> GetMyNumsAsync();
    Task<bool> SaveMyNumAsync(Num num);

    Task UpdateMyNumAsync(Num num);
}
