using Jewellery.BLL.AbstractRepository;

using Jewellery.BLL.ConcreteRepository;

using Jewellery.Dal.JewelleryContext;
using Jewellery.Dal.Redis_Cache.Abstract;
using Jewellery.Dal.Redis_Cache.Concrete;
using Jewellery.Dal.Redis_Cache.Entities;
using Jewellery.Dal.Redis_Cache;
using Jewellery.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Jewellery.IOC.Container;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

ServiceContainer.ServiceConfiguration(builder.Services, builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
