namespace MyFishingApp.Services.Data.NEWJWTSERVICE
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;

    public class SignInManager
    {
        private readonly IRepository<RefreshToken> refreshTokenRepository;
        private readonly IRepository<ApplicationUser> appUserRepository;
        private readonly ILogger<SignInManager> logger;

        private readonly JWTAuthService jwtAuthService;
        private readonly JwtTokenConfig jwtTokenConfig;

        public SignInManager(
            ILogger<SignInManager> logger,
            JWTAuthService jwtAuthService,
            JwtTokenConfig jwtTokenConfig,
            IRepository<RefreshToken> refreshTokenRepository,
            IRepository<ApplicationUser> appUserRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
            this.appUserRepository = appUserRepository;
            this.logger = logger;
            this.jwtAuthService = jwtAuthService;
            this.jwtTokenConfig = jwtTokenConfig;
        }

        public static string ComputeSha256Hash(string password)
        {
            // Create a SHA256
            using SHA256 sha256Hash = SHA256.Create();

            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public async Task<SignInResult> SignIn(string userName, string password)
        {
            this.logger.LogInformation($"Validating user [{userName}]", userName);

            SignInResult result = new();

            if (string.IsNullOrWhiteSpace(userName))
            {
                return result;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return result;
            }

            var user = this.appUserRepository.All().Where(f => f.UserName == userName && f.PasswordHash == ComputeSha256Hash(password)).Select(x => new ApplicationUser
            {
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.MiddleName,
                Age = x.Age,
                Id = x.Id,
                Email = x.Email,
                Gender = x.Gender,
                MainImageUrl = x.MainImageUrl,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
                Roles = x.Roles.Select(x => new IdentityUserRole<string>
                {
                    RoleId = x.RoleId,
                    UserId = x.UserId,
                }).ToList(),
                Claims = x.Claims.Select(x => new IdentityUserClaim<string>
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    ClaimType = x.ClaimType,
                    ClaimValue = x.ClaimValue,

                }).ToList(),
                Logins = x.Logins.Select(x => new IdentityUserLogin<string>
                {
                    UserId = x.UserId,
                    LoginProvider = x.LoginProvider,
                    ProviderDisplayName = x.ProviderDisplayName,
                    ProviderKey = x.ProviderKey,
                }).ToList(),
                Posts = x.Posts.Select(x => new Post
                {
                    Id = x.Id,
                    ImageUrls = x.ImageUrls,
                    Title = x.Title,
                    User = x.User,
                    UserId = x.UserId,
                    Content = x.Content,
                    Votes = x.Votes,
                    Comments = x.Comments.Select(x => new Comment
                    {
                        Content = x.Content,
                        User = x.User,
                        UserId = x.UserId,
                        Id = x.Id,
                        Parent = x.Parent,
                        ParentId = x.ParentId,
                        Post = x.Post,
                        PostId = x.PostId,
                    }).ToList(),
                }).ToList(),
            }).FirstOrDefault();

            if (user != null)
            {
                var claims = this.BuildClaims(user);
                result.User = user;
                result.AccessToken = this.jwtAuthService.BuildToken(claims);
                result.RefreshToken = this.jwtAuthService.BuildRefreshToken();

                await this.refreshTokenRepository.AddAsync(
                     new RefreshToken
                     {
                         UserId = user.Id,
                         Token = result.RefreshToken,
                         IssuedAt = DateTime.Now,
                         ExpiresAt = DateTime.Now.AddMinutes(this.jwtTokenConfig.RefreshTokenExpiration),
                     });

                await this.refreshTokenRepository.SaveChangesAsync();

                result.Success = true;
            }

            return result;
        }

        public async Task<SignInResult> RefreshToken(string accessToken, string refreshToken)
        {
            ClaimsPrincipal claimsPrincipal = this.jwtAuthService.GetPrincipalFromToken(accessToken);
            SignInResult result = new();

            if (claimsPrincipal == null)
            {
                return result;
            }

            string id = claimsPrincipal.Claims.First(c => c.Type == "id").Value;
            var user = this.appUserRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (user == null)
            {
                return result;
            }

            var token = await this.refreshTokenRepository.All()
                    .Where(f => f.UserId == user.Id
                            && f.Token == refreshToken
                            && f.ExpiresAt >= DateTime.Now)
                    .FirstOrDefaultAsync();

            if (token == null)
            {
                return result;
            }

            var claims = this.BuildClaims(user);

            result.User = user;
            result.AccessToken = this.jwtAuthService.BuildToken(claims);
            result.RefreshToken = this.jwtAuthService.BuildRefreshToken();

            this.refreshTokenRepository.Delete(token);
            await this.refreshTokenRepository.AddAsync(
                 new RefreshToken
                 {
                     UserId = user.Id,
                     Token = result.RefreshToken,
                     IssuedAt = DateTime.Now,
                     ExpiresAt = DateTime.Now.AddMinutes(this.jwtTokenConfig.RefreshTokenExpiration),
                 });

            await this.refreshTokenRepository.SaveChangesAsync();

            result.Success = true;

            return result;
        }

        private Claim[] BuildClaims(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            };

            return claims;
        }
    }
}
