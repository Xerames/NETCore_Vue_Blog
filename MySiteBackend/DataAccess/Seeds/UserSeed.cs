using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Seeds
{
    internal class UserSeed : IEntityTypeConfiguration<User>
    {
        private const string AdminUserId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F7";
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var admin = new User
            {
                Id = AdminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "Admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            admin.PasswordHash = PassGenerate(admin);
            builder.HasData(admin);
        }

        public string PassGenerate(User user)
        {
            var passHash = new PasswordHasher<User>();
            return passHash.HashPassword(user, "159357456qW");
        }
    }


}
