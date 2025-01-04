using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using PizzaMeow.Apis;
using PizzaMeow.Application.DTOs;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMeow.Test.AuthApiTest
{
    public class AuthApiTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly HttpContext _context;

        public AuthApiTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _roleRepository = A.Fake<IRoleRepository>();
            _context = A.Fake<HttpContext>();
        }

        [Fact]
        public async Task AuthApi_VerifyUser_ShouldReturn_False_If_User_Provided_Wrong_Data()
        {
            //Arrange

            var api = new AuthApi();

            var testUserLoginDTO = new UseLoginDTO { Email = "TestEmail", Password = "TestWrongPassword" };

            var testUser = new User
            {
                Id = 1,
                Name = "Test",
                Email = "TestEmail",
                PhoneNumber = "Test",
                PasswordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword("TestPassword", BCrypt.Net.HashType.SHA256),
                RoleId = 1,
            };

            var testRole = new Role
            {
                Id = 1,
                RoleName = "Test",
            };

            A.CallTo(() => _userRepository.GetUserAsync(1)).Returns(testUser);
            A.CallTo(() => _roleRepository.GetRoleAsync(testUser.Id)).Returns(testRole);

            //Act

            
            bool result = api.VerifyUser(testUserLoginDTO.Password, testUser.PasswordHashed);
            //Assert

            result.Should().BeFalse();
            
        }
    }
}
