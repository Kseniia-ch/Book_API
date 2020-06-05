using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services;
using BookAPI.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BookAPI.Domain.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BookAPI.Extension;

namespace BookAPI.Services
{
    public class AuthService : IAuthService
    {

        private readonly AppSettings appSettings;
        private readonly IUserRepository userRepository;
        public AuthService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
        }
        public async Task<User> Authenticate(string login, string password)
        {
            var user = (await userRepository.ListAsync())
                                .SingleOrDefault(usr => usr.Login == login &&
                                                        usr.Password == password);  
            if (user == null)
                return null;

            user.GenerateToken(appSettings.Secret, appSettings.ExpiresMinutes);

            return user;
        }
    }
}