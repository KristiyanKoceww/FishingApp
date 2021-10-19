namespace MyFishingApp.Services.Data.Jwt
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    using Microsoft.IdentityModel.Tokens;

    public class JwtService : IJwtService
    {
        private string secureKey = "dhgkahghqgqlgdqkgalg";

        public string Generate(string id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payLoad = new JwtPayload(id, null, null, null, DateTime.Today.AddDays(1));

            var securityToken = new JwtSecurityToken(header, payLoad);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.secureKey);

            tokenHandler.ValidateToken(
                jwtToken,
                new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
