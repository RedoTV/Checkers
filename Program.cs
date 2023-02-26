using Checkers.Data;
using Checkers.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/auth/login";
                    options.LoginPath = "/auth/login";
                    options.LogoutPath = "/auth/logout";
                });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
	
//Connect Services
builder.Services.AddTransient<ILobbyService,LobbyService>();
builder.Services.AddTransient<ILoginService,LoginService>();

//Get database connection string from appsettins.json
string dbConnection = builder.Configuration.GetConnectionString("Database");
//Add Database to project and set connection string
builder.Services.AddDbContext<CheckersDbContext>(options=>options.UseSqlServer(dbConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
