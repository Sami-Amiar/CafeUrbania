
namespace CafeUrbania.Models.Services;

public interface IContactService
{
    Task<List<Categories>> GetCategory();
    Task PostContact(Contact contact);
}