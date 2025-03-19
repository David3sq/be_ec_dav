// GlobalUsings.cs (opzionale, se preferisci metterli in un file separato)
// Puoi anche inserirli direttamente in Program.cs se preferisci.
global using ecommerce.auth_ecommerce.Dto;
global using ecommerce.auth_ecommerce.Models;
global using ecommerce.auth_ecommerce.Data;
global using ecommerce.auth_ecommerce.Mappers;
global using Microsoft.Extensions.Logging;
global using ecommerce.auth_ecommerce.Services;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.EntityFrameworkCore;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using Microsoft.Extensions.Configuration;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using AutoMapper;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.Filters;
global using System.Text.Json.Serialization;
global using System.Linq;
global using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Configurazione dei servizi nel container.

// Aggiunta dei controller.
builder.Services.AddControllers();

// Aggiunta degli endpoint per API Explorer (Swagger).
builder.Services.AddEndpointsApiExplorer();

// Configurazione di Swagger.
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });

    // Definizione della sicurezza JWT per Swagger.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Inserisci il token JWT nel formato: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// Configurazione dell'autenticazione JWT.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // La chiave segreta dovrebbe essere definita in appsettings.json in "Jwt:Key".
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

// (Opzionale) Registrazione del contesto DB e di altri servizi.
// builder.Services.AddDbContext<EcomContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// (Opzionale) Registrazione di servizi personalizzati, ad esempio l'AuthService.
// builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configurazione della pipeline HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Abilita i middleware di autenticazione e autorizzazione.
app.UseAuthentication();
app.UseAuthorization();

// Mappa i controller per gestire le richieste.
app.MapControllers();

app.Run();
