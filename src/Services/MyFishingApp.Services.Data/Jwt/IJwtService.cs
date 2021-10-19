namespace MyFishingApp.Services.Data.Jwt
{
    using System.IdentityModel.Tokens.Jwt;

    public interface IJwtService
    {
        string Generate(string id);

        JwtSecurityToken Verify(string jwtToken);
    }
}
