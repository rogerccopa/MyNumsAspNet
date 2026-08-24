using MyNumsApi.Models;
using MyNumsApi.Services;

namespace MyNumsApi.Endpoints;

public static class MyNumsEndpoints
{
    public static void MapMyNumsEndpoints(this WebApplication app)
    {
        app.MapGet("/mynums", async (IMyNumsService myNumsService) =>
        {
            var myNums = await myNumsService.GetMyNumsAsync();
            return Results.Ok(myNums);
        });

        app.MapPost("/mynums", async (IMyNumsService myNumsService, Num num) =>
        {
            await myNumsService.SaveMyNumAsync(num);
            return Results.Created($"/mynums/{num.Number}", num);
        });

        app.MapPut("/mynums", async (IMyNumsService myNumsService, Num num) =>
        {
            await myNumsService.UpdateMyNumAsync(num);
            return Results.NoContent();
        });
    }
}
