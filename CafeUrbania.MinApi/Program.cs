using CafeUrbania.MinApi.Services;
using CafeUrbania.MinApi.Services.Interfaces;
using CafeUrbania.Models;
using MiniValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapGet("/orders", (IOrderService orderService) =>
{
    return orderService.GetOrders();
}).CacheOutput();

app.MapGet("/orders/{id}", (IOrderService orderService, int id) =>
{
    return orderService.GetOrderById(id);
});

app.MapPost("/contact", (Contact contact, IContactService contactService) =>
    !MiniValidator.TryValidate(contact, out var errors)
        ? Results.ValidationProblem(errors)
        : Results.Created("", contactService.Create(contact)));

app.MapGet("/categoriesdemande", (IContactService contactService) =>
{
    return Results.Ok(contactService.GetCategory());
});

app.MapGet("/menu", (IMenuService menuService) =>
{
    return menuService.GetMenuItems();
})
.CacheOutput();

app.MapGet("/Bonjour", () => "Hello World!");

app.Run();

// The reason why you need this partial class definition, is that by default the Program.cs file is compiled into a private class Program, which can not be accessed by other projects. By adding this public partial class, the test project will get access to Program and lets you write tests against it.
public partial class Program { }
