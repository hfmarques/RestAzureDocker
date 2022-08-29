namespace WebApi.Data.Converter.Contract;

public interface IParser<TO, TD>
{
    TD? Parse(TO? origin);
    List<TD>? Parse(List<TO>? origin);
}