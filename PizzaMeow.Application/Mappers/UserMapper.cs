using BCrypt.Net;
using PizzaMeow.Application.DTOs;
using PizzaMeow.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMeow.Application.Mappers
{
    public static class UserMapper
    {
        public static User ToEntity(this UserRegisterDTO? userRegisterDTO) => new User
        {
            Name = userRegisterDTO.Name,
            Email = userRegisterDTO.Email,
            PhoneNumber = userRegisterDTO.PhoneNumber,
            PasswordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(userRegisterDTO.Password, BCrypt.Net.HashType.SHA256),
            RoleId = 2,
        };
    }
}
