using System.Net.Http.Json;

namespace CafeUrbania.Models.Services;

public class ContactService : IContactService
{
    private readonly HttpClient http;

    public ContactService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<List<Categories>> GetCategory()
    {
        var infos = await http.GetFromJsonAsync<List<Categories>>("v1/contact");
        return infos;
    }

    public async Task PostContact(Contact contact)
    {
        await http.PostAsJsonAsync("v1/contact", contact);
    }
}