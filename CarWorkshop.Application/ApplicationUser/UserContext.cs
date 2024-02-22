using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.ApplicationUser
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        //wstrzykiwanie z kontenera zależności
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Edit: trzeba było dodać check czy User jest zalogowany przed wyciąganiem ID
        public CurrentUser? GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user == null)
            {
                //to w sytuacji gdy użytkownik nie został poprawnie zainicjalizowany
                throw new InvalidOperationException("Context user is not present");
            }
            if (!user.Identity.IsAuthenticated || user.Identity ==null)
            {
                return null;
            }
            
            //przypisanie Id
            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            return new CurrentUser(id, email, roles);
        }
    }
}
