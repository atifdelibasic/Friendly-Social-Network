using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtHelper
{
    public  static ClaimsPrincipal GetPrincipalFromToken(string token)
    {

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        var claims = securityToken.Claims;
        var claimsIdentity = new ClaimsIdentity(claims);
        return new ClaimsPrincipal(claimsIdentity);

    }
}
