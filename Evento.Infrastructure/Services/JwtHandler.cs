using Evento.Evento.Infrastructure.DTO;
using Evento.Evento.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Evento.Evento.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtDto CreateToken(Guid userId, string role)
        {
            throw new NotImplementedException();
        }

        //public JwtDto CreateToken(Guid userId, string role)
        //{
        //    var now = DateTime.UtcNow;
        //    var claims = new Claim[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
        //        new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
        //        new Claim(ClaimTypes.Role, role.ToString()),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Iat, now.Ticks.ToString()),
        //    };

        //    var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);
        //    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)));
        //}
    }
}
