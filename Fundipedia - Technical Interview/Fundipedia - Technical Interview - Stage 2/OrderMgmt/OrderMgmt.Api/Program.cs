using OrderMgmt.Domain.Rules.Interfaces;
using OrderMgmt.Domain.Services.Impl;
using OrderMgmt.Domain.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Load all rules via reflection
var orderRuleTypes = Assembly.GetAssembly(typeof(IOrderRule))
                             .GetTypes()
                             .Where(t => typeof(IOrderRule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                             .ToList();

foreach (var ruleType in orderRuleTypes)
{
    builder.Services.AddSingleton(typeof(IOrderRule), ruleType);
}

builder.Services.AddSingleton<IOrderProcessor>(serviceProvider =>
{
    var rules = serviceProvider
        .GetServices<IOrderRule>()
        .OrderBy(x => x.Priority);

    return new OrderProcessor(rules);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
