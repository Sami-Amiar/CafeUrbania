using CafeUrbania.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CaféUrbania.Test;

public class ContactezNous_CategorieDemandeUnitTest
{
    readonly WebApplicationFactory<Program> _application;
    readonly HttpClient? _client;

    public ContactezNous_CategorieDemandeUnitTest()
    {
        _application = new WebApplicationFactory<Program>();
        _client = _application.CreateClient();
    }

    [Fact]
    public async Task Test029_CreateContact_ListeCategoriesDemande4Valeurs()
    {
        var result = await _client.GetAsync("/categoriesdemande");

        var formattedResponse = result.Content.ReadAsStringAsync().Result;
        List<Categories> infosObtenues = JsonConvert.DeserializeObject<List<Categories>>(formattedResponse);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(4, infosObtenues.Count);
    }

    [Fact]
    public async Task Test030_CreateContact_ListeCategoriesCategorieDescriptionCategorieDescriptionDeuxiemeElement()
    {
        var result = await _client.GetAsync("/categoriesdemande");

        var formattedResponse = result.Content.ReadAsStringAsync().Result;
        List<Categories> infosObtenues = JsonConvert.DeserializeObject<List<Categories>>(formattedResponse);

        Assert.Equal("Problème avec ma commande", infosObtenues[1].CategorieDescription);
    }
}
