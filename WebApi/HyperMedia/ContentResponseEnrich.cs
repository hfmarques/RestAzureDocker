using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using WebApi.HyperMedia.Abstract;

namespace WebApi.HyperMedia;

public abstract class ContentResponseEnrich<T> : IResponseEnrich where T : ISupportHyperMedia
{
    public bool CanEnrich(Type contentType)
    {
        return contentType == typeof(T) || contentType == typeof(List<T>);
    }

    protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

    public bool CanEnrich(ResultExecutingContext response)
    {
        if (response.Result is OkObjectResult okObjectResult)
        {
            return okObjectResult.Value != null && 
                   CanEnrich(okObjectResult.Value.GetType());
        }

        return false;
    }

    public async Task Enrich(ResultExecutingContext response)
    {
        var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
        if (response.Result is OkObjectResult okObjectResult)
        {
            if (okObjectResult.Value is T model)
            {
                await EnrichModel(model, urlHelper);
            }

            if (okObjectResult.Value is List<T> collection)
            {
                var bag = new ConcurrentBag<T>(collection);
                Parallel.ForEach(bag, (element) => { EnrichModel(element, urlHelper); });
            }
        }

        await Task.FromResult<object>(null);
    }
}