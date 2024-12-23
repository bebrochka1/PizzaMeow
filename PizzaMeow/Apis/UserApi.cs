using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;
using PizzaMeow.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace PizzaMeow.Apis
{
    public class UserApi : IApi
    {
        public void Register(WebApplication app)
        {
            app.MapPost("/users/{userId}/role/{roleId}", [Authorize(Roles = "Admin")] async (
                int userId,
                int roleId,
                IUserRepository repos) =>
            {
                try
                {
                    await repos.UpdateUserRoleAsync(userId, roleId);
                    await repos.SaveAsync();

                    return Results.Ok();
                }
                catch (ArgumentException ex) when (roleId > 3 || roleId < 0)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            })
                .Produces(StatusCodes.Status200OK)
                .WithName("Change user role manually")
                .WithTags("Users");

            app.MapPatch("/users/{userEmail}/setRole", [Authorize(Roles = "Admin")] async (
                IUserRepository userRepository,
                IRoleRepository roleRepository,
                string RoleName,
                string UserEmail) =>
            {
                Role roleInDb;

                try
                {
                    roleInDb = await roleRepository.GetRoleAsync(RoleName);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }

                User userInDb;

                try
                {
                    userInDb = await userRepository.GetUserAsync(UserEmail);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }

                await userRepository.UpdateUserRoleAsync(userInDb.Id, roleInDb.Id);
                await userRepository.SaveAsync();

                return Results.Ok();
            });

        }
    }
}
