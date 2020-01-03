﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Hexagon.UserManagement.EFCorePostgre.Entity;

namespace Hexagon.UserManagement.EFCorePostgre.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired(true).HasMaxLength(255);
        }
    }
}