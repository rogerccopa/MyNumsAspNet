using Microsoft.EntityFrameworkCore;
using MyNumsApi.Data;
using MyNumsApi.Models;

namespace MyNumsApi.Services;

public class MyNumsService : IMyNumsService
{
    private readonly AppDbContext _dbCtx;
    public MyNumsService(AppDbContext appDbCtx)
    {
        _dbCtx = appDbCtx;
    }

    public Task<List<Num>> GetMyNumsAsync()
    {
        return _dbCtx.Nums
            .Where(record => !record.Note.Contains("BAD"))
            .OrderBy(record => record.Note)
            .ToListAsync();
    }

    public Task<bool> SaveMyNumAsync(Num num)
    {
        var existingNum = _dbCtx.Nums.FirstOrDefault(n => n.Number == num.Number);
        if (existingNum is null)
        {
			_dbCtx.Nums.Add(num);
			_dbCtx.SaveChangesAsync();
			return Task.FromResult(true);
		}

        return Task.FromResult(false);
    }

    public Task UpdateMyNumAsync(Num num)
    {
        var existingNum = _dbCtx.Nums.FirstOrDefault(n => n.Number == num.Number);

        if (existingNum != null)
        {
            existingNum.Note = num.Note;
            _dbCtx.SaveChangesAsync();
        }

        return Task.CompletedTask;
    }
}
