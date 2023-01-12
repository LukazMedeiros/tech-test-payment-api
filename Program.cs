using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SaleContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("default")));

builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("default")));

builder.Services.AddDbContext<SellerContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("default")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
