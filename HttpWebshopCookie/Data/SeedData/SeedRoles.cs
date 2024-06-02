﻿namespace HttpWebshopCookie.Data;

public static class SeedRole
{
    private static readonly string[] RoleNames = ["admin", "manager", "staff", "assistant", "companyrep", "customer"];
    public static void SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!roleManager.RoleExistsAsync(RoleNames[0]).Result)
        {
            IdentityRole role = new()
            {
                Name = RoleNames[0]
            };

            roleManager.CreateAsync(role).Wait();
        }

        if (!roleManager.RoleExistsAsync(RoleNames[1]).Result)
        {
            IdentityRole role = new()
            {
                Name = RoleNames[1]
            };

            roleManager.CreateAsync(role).Wait();
        }

        if (!roleManager.RoleExistsAsync(RoleNames[2]).Result)
        {
            IdentityRole role = new()
            {
                Name = RoleNames[2]
            };

            roleManager.CreateAsync(role).Wait();
        }

        if (!roleManager.RoleExistsAsync(RoleNames[3]).Result)
        {
            IdentityRole role = new()
            {
                Name = RoleNames[3]
            };

            roleManager.CreateAsync(role).Wait();
        }

        if (!roleManager.RoleExistsAsync(RoleNames[4]).Result)
        {
            IdentityRole role = new()
            {
                Name = RoleNames[4]
            };

            roleManager.CreateAsync(role).Wait();
        }

        if (!roleManager.RoleExistsAsync(RoleNames[5]).Result)
        {
            IdentityRole role = new()
            {
                Name = RoleNames[5]
            };

            roleManager.CreateAsync(role).Wait();
        }
    }
}