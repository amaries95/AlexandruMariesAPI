using AlexandruMaries.Data.Interfaces;
using AlexandruMaries.Data.RepoModels;
using AlexandruMaries.Data.RepoModels.NewFolder;
using AlexandruMaries.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlexandruMaries.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AlexandruMariesDbContext alexandruMariesDbContext;
		private readonly IConfiguration configuration;

		public UserRepository(AlexandruMariesDbContext alexandruMariesDbContext, IConfiguration configuration)
		{
			this.alexandruMariesDbContext = alexandruMariesDbContext;
			this.configuration = configuration;
		}

		public async Task<ServiceResponse<string>> Login(string username, string password)
		{
			var serviceResponse = new ServiceResponse<string>();
			var user = alexandruMariesDbContext.Users.FirstOrDefault(u => u.Username.ToLower().Equals(username.ToLower()));

			if (user == null)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = "User not found";
			}
			else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
			{
				serviceResponse.Success = false;
				serviceResponse.Message = "Wrong password";
			}
			else
			{
				serviceResponse.Data = CreateToken(user);
			}

			return serviceResponse;
		}

		public async Task<UserRespone> Register(string userName, string password)
		{
			CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

			var user = new User
			{
				Username = userName,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt
			};

			alexandruMariesDbContext.Users.Add(user);
			await alexandruMariesDbContext.SaveChangesAsync();

			return new UserRespone
			{
				Id = user.Id
			};
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using var hmac = new HMACSHA256();

			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using var hmac = new HMACSHA256(passwordSalt);
			var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

			return computeHash.SequenceEqual(passwordHash);
		}

		private string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Username)
			};

			SymmetricSecurityKey key = new (Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
			SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(3),
				SigningCredentials = credentials,
			};

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
