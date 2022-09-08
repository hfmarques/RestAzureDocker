using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Vo;
using WebApi.HyperMedia.Constants;

namespace WebApi.HyperMedia.Enrich;

public class PersonEnrich : ContentResponseEnrich<PersonVo>
{
    private readonly object _lock;

    public PersonEnrich()
    {
        _lock = new object();
    }

    protected override Task EnrichModel(PersonVo content, IUrlHelper urlHelper)
    {
        var path = "api/v1/Person";
        var link = GetLink(content.Id, urlHelper, path);

        content.Links.Add(new HyperMediaLink
        {
            Action = HttpActionVerb.Get,
            Href = link,
            Rel = RelationType.Self,
            Type = ResponseTypeFormat.DefaultGet
        });
        content.Links.Add(new HyperMediaLink
        {
            Action = HttpActionVerb.Post,
            Href = link,
            Rel = RelationType.Self,
            Type = ResponseTypeFormat.DefaultPost
        });
        content.Links.Add(new HyperMediaLink
        {
            Action = HttpActionVerb.Put,
            Href = link,
            Rel = RelationType.Self,
            Type = ResponseTypeFormat.DefaultPut
        });
        content.Links.Add(new HyperMediaLink
        {
            Action = HttpActionVerb.Delete,
            Href = link,
            Rel = RelationType.Self,
            Type = "int"
        });

        return null;
    }

    private string GetLink(long id, IUrlHelper urlHelper, string path)
    {
        lock (_lock)
        {
            var url = new {controller = path, id};
            return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
        }
    }
}