using WebApi.Data.Vo;

namespace WebApi.Business;

public interface IPersonBusiness
{
    PersonVo Create(PersonVo personVo);
    PersonVo? FindById(long id);
    List<PersonVo> FindAll();
    PersonVo Update(PersonVo personVo);
    void Delete(long id);
}