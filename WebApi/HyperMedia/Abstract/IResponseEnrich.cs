using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.HyperMedia.Abstract;

public interface IResponseEnrich
{
    bool CanEnrich(ResultExecutingContext response);
    Task Enrich(ResultExecutingContext response);
}