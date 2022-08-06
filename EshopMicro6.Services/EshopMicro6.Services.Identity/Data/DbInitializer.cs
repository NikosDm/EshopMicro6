using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EshopMicro6.Services.Identity.Data.Initializer;
using EshopMicro6.Services.Identity.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace EshopMicro6.Services.Identity.Data;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManger;

    public DbInitializer(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManger = roleManger;
    }

    public void Initialize()
    {
        if (_roleManger.FindByNameAsync(SD.Admin).Result == null) 
        {
            _roleManger.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManger.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
        }
        else return;
        
        ApplicationUser adminUser = new ApplicationUser 
        {
            Email = "admin1@gmail.com",
            UserName = "admin1@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "1111111111",
            FirstName = "Nick",
            LastName = "Thom",
        };

        _userManager.CreateAsync(adminUser, "!Admin1234").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

        _userManager.AddClaimsAsync(adminUser, new Claim[] 
        {
            new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
            new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Admin)
        }).GetAwaiter().GetResult();

        ApplicationUser customerUser = new ApplicationUser 
        {
            Email = "customer1@gmail.com",
            UserName = "customer1@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "1111111111",
            FirstName = "Ian",
            LastName = "Scott",
        };

        _userManager.CreateAsync(customerUser, "!Customer1234").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

        _userManager.AddClaimsAsync(customerUser, new Claim[] 
        {
            new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
            new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Customer)
        }).GetAwaiter().GetResult();
    }
}
