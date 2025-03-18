using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using server.Models;

namespace server.Helpers
{
    public class JwtHelper
    {
        private readonly string _jwtSecret;

        // This saves a secret key fro signing the token 
        public JwtHelper(string jwtSecret)
        {
            _jwtSecret = jwtSecret;
        }

        // Generates a JWT token for the user
        public string GenerateToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), //Stores the user's ID
                new Claim(ClaimTypes.Email, user.Email), //Stores the user's email
            };

            //Converts key into format that can be used to sign the token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Creates the token with claims and expiration time
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(2), // Token expires in 2 hours
                claims: claims,
                signingCredentials: creds);

            // Returns the generated token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
