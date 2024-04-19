using CafeUrbania.Models;

namespace CafeUrbania.MinApi.Services.Interfaces;

public interface IContactService
{
    Contact Create(Contact contact);

    List<Categories> GetCategory();
}
