using OrderMgmt.Domain.Rules.Impl;
using OrderMgmt.Domain.Rules.Interfaces;
using OrderMgmt.Domain.Services.Impl;
using OrderMgmt.Domain.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IOrderRule, LargeRepairNewCustomerRule>();
builder.Services.AddSingleton<IOrderRule, LargeRushHireOrderRule>();
builder.Services.AddSingleton<IOrderRule, LargeRepairOrderRule>();
builder.Services.AddSingleton<IOrderRule, RushOrderNewCustomerRule>();
builder.Services.AddSingleton<IOrderRule, DefaultOrderRule>();

builder.Services.AddSingleton<IOrderProcessor>(serviceProvider =>
{
    var rules = serviceProvider.GetServices<IOrderRule>();
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
