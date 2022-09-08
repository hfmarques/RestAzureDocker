using WebApi.HyperMedia.Abstract;

namespace WebApi.HyperMedia.Filters;

public class HyperMediaFilterOptions
{
    public List<IResponseEnrich> ContentResponseEnrichList { get; set; }
}