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
            bool newNumWasAdded = await myNumsService.SaveMyNumAsync(num);
            if (newNumWasAdded)
            {
                return Results.Ok();
			}

            return Results.BadRequest();
        });

        app.MapPut("/mynums", async (IMyNumsService myNumsService, Num num) =>
        {
            await myNumsService.UpdateMyNumAsync(num);
            return Results.NoContent();
        });
    }
}
