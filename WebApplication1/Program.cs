using DbConnection;
using Microsoft.EntityFrameworkCore;
using Helpers;
using Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddStackExchangeRedisCache(action =>
    {
        action.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
        action.InstanceName = "master";
    });
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

//jwt auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))
    };
    options.Events = new JwtBearerEvents()
    {
        OnTokenValidated = (context) =>
        {
            try
            {
                var sId = context.Principal.Claims.Where(x => x.Type.Split('/').LastOrDefault() == "nameidentifier").FirstOrDefault().Value;
                if (string.IsNullOrEmpty(sId))
                {
                    context.Fail("Unauthorized. Please re-login");
                }
                else
                {
                    Guid Id;
                    Guid.TryParse(sId, out Id);

                    SessionService.DefineAccount(Id);
                }
            }
            catch (Exception ex)
            {
                context.Fail("Unauthorized. Please re-login");
            }
            return Task.CompletedTask;
        }
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

DbInitializer.Initialize();

app.Run();
