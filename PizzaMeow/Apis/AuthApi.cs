
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;
using System.Security.Claims;
using BCrypt.Net;
using FluentValidation;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PizzaMeow.Apis
{
    public class AuthApi : IApi
    {
        public void Register(WebApplication app)
        {
            app.MapPost("/login", LoginUser);

            app.MapPost("register", RegisterUser);

            app.MapGet("/unauthorized", Unauthorized);

            app.MapGet("/logout", Logout);
        }

        public async Task<IResult> LoginUser(HttpContext context, IUserRepository userRepos, IRoleRepository roleRepos, UserLoginDTO userDTO)
        {
            User userInDb;
            try
            {
                userInDb = await userRepos.GetUserAsync(userDTO.Email);
            }
            catch (ArgumentNullException ex)
            {
                return Results.NotFound(ex.Message);
            }

            var userVerified = VerifyUser(userDTO.Password, userInDb.PasswordHashed);
            if (userVerified)
            {
                var userRole = await roleRepos.GetRoleAsync(userInDb.RoleId);
                var claims = new List<Claim> {
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole.RoleName),
                            new Claim(ClaimsIdentity.DefaultNameClaimType, userInDb.Email)
                        };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                await context.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
                return Results.Ok();
            }
            else
            {
                return Results.Unauthorized();
            }
        }

        public async Task<IResult> RegisterUser(IUserRepository repos, UserRegisterDTO userDTO, HttpContext context, IValidator<UserRegisterDTO> validator)
        {
            {
                if (userDTO == null) return Results.BadRequest("Data should be provided");
                try
                {
                    var validationResults = await validator.ValidateAsync(userDTO);

                    if (validationResults.IsValid)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, "User"),
                            new Claim(ClaimsIdentity.DefaultNameClaimType, userDTO.Email)
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                        await repos.AddUserAsync(userDTO);
                        await repos.SaveAsync();

                        await context.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
                        var userInDb = await repos.GetUserAsync(userDTO.Email);
                        Console.WriteLine(userInDb.PasswordHashed);
                        return Results.Created();
                    }
                    else
                    {
                        return Results.ValidationProblem(validationResults.ToDictionary());
                    }
                }

                catch (ArgumentNullException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }
        }

        public async Task Unauthorized(HttpContext context) => await context.Response.WriteAsync("Unauthorized");
        
        public async Task Logout(HttpContext context) => await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        public bool VerifyUser(string userDTO, string userInDb)
        {
            if (string.IsNullOrWhiteSpace(userInDb))
            {
                throw new ArgumentException("Hash cannot be null or empty", nameof(userInDb));
            }

            return BCrypt.Net.BCrypt.EnhancedVerify(userDTO, userInDb, HashType.SHA256);
        }

    }
}
