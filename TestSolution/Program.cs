using DisserCore.Models.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TestSolution.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "jwtToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert JWT token"
    });
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
            },
        new string[]{}
        }
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 // ��������, ����� �� �������������� �������� ��� ��������� ������
                 ValidateIssuer = true,
                 // ������, �������������� ��������
                 ValidIssuer = AuthOptions.ISSUER,

                 // ����� �� �������������� ����������� ������
                 ValidateAudience = true,
                 // ��������� ����������� ������
                 ValidAudience = AuthOptions.AUDIENCE,
                 // ����� �� �������������� ����� �������������
                 ValidateLifetime = true,

                 // ��������� ����� ������������
                 IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                 // ��������� ����� ������������
                 ValidateIssuerSigningKey = true,
                 ClockSkew = TimeSpan.Zero
             };
         });
builder.Services.AddControllersWithViews();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.UseDeveloperExceptionPage();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors();

app.MapControllers();

app.Run();
