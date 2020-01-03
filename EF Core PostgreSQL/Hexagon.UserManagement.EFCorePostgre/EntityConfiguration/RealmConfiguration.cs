﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Hexagon.UserManagement.EFCorePostgre.Entity;

namespace Hexagon.UserManagement.EFCorePostgre.EntityConfiguration
{
    public class RealmConfiguration : IEntityTypeConfiguration<Realm>
    {
        public void Configure(EntityTypeBuilder<Realm> builder)
        {
            builder.HasKey(r => r.RlmId);
            builder.Property(r => r.RealmName).IsRequired(true).HasMaxLength(50);
            builder.HasMany(r => r.Users);
        }
    }
}