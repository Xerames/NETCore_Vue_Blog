using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Seeds
{
    internal class RoleSeed : IEntityTypeConfiguration<Role>
    {
        private const string AdminRoleId = "2301D884-221A-4E7D-B509-0113DCC043E1";
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role
            {
                Id = AdminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
        }
    }
}
