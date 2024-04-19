using CafeUrbania.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeUrbania.Models;

public class ContactServiceMock : IContactService
{
    public ContactServiceMock()
    {
    }

    public async Task<List<Categories>> GetCategory()
    {
        return new List<Categories>() { };
    }

    public async Task PostContact(Contact contact)
    {
    }
}
