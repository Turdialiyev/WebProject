using Bulky.DataAccess.Data;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbcontext;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext appDbcontext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbcontext = appDbcontext;
        }

        public void Initialize()
        {
            try
            {
                if (_appDbcontext.Database.GetPendingMigrations().Count() > 0)
                {
                    _appDbcontext.Database.Migrate();
                }
            }
            catch (Exception ex) { }


            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "G'olibjon",
                    Email = "golibjonturdialitev0226@gmail.com",
                }, "Admin123*").GetAwaiter().GetResult(); ;

                IdentityUser user = _appDbcontext.Users.FirstOrDefault(u => u.Email == "golibjonturdialitev0226@gmail.com");
               
                _userManager.AddToRoleAsync(user!, SD.Role_Admin);

            }

            return;
        }
    }
}
