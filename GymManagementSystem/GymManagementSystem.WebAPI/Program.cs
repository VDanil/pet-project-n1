using GymManagementSystem.Application;
using GymManagementSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// -- Repository
builder.Services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
builder.Services.AddScoped<IAdministratorsRepository, AdministratorsRepository>();
builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<ICoachesRepository, CoachesRepository>();
builder.Services.AddScoped<IGroupsRepository, GroupsRepository>();
builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
builder.Services.AddScoped<IVisitsRepository, VisitsRepository>();

// -- UseCases
builder.Services.AddTransient<IAdministratorUseCases, AdministratorUseCases>();
builder.Services.AddTransient<IClientUseCases, ClientUseCases>();
builder.Services.AddTransient<ICoachUseCases, CoachUseCases>();
builder.Services.AddTransient<IGroupUseCases, GroupUseCases>();
builder.Services.AddTransient<ISubscriptionUseCases, SubscriptionUseCases>();
builder.Services.AddTransient<ITimetableUseCases, TimetableUseCases>();
builder.Services.AddTransient<IVisitorUseCases, VisitorUseCases>();
builder.Services.AddTransient<IVisitUseCases, VisitUseCases>();

builder.Services.AddDbContext<GymManagementContext>(
    options => options.UseInMemoryDatabase("GymManagement")
    );

builder.Services.AddControllers();

// -- Enable Cross-Origin Requests (CORS)
builder.Services.AddCors(options => {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("https://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

// -- Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -- Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Administrator", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "administrator");
        });
    });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<GymManagementContext>();
        if (dbContext != null)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// TODO: Wrewrite controller layer so that classes cept single responsibility principle REMOVE VisitorController
// TODO: Wrewrite controller layer so that classes cept single responsibility principle REMOVE VisitorUseCases && AdministratorUseCases