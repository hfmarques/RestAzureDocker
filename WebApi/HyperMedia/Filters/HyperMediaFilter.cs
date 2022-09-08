using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.HyperMedia.Filters;

public class HyperMediaFilter : ResultFilterAttribute
{
    private readonly HyperMediaFilterOptions hyperMediaFilterOptions;

    public HyperMediaFilter(HyperMediaFilterOptions hyperMediaFilterOptions)
    {
        this.hyperMediaFilterOptions = hyperMediaFilterOptions;
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        TryEnrichResult(context);
        base.OnResultExecuting(context);
    }

    private void TryEnrichResult(ResultExecutingContext context)
    {
        if (context.Result is not OkObjectResult okObjectResult) return;

        var enrich = hyperMediaFilterOptions
            .ContentResponseEnrichList
            .FirstOrDefault(x => x.CanEnrich(context));

        if (enrich != null)
            Task.FromResult(enrich.Enrich(context));
    }
}