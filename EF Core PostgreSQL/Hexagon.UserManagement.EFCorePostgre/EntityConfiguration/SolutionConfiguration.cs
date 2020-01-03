using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hexagon.UserManagement.EFCorePostgre.Entity
{
    public class SolutionConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(c => c.SlnId);
            builder.Property(c => c.SolutionName).HasMaxLength(200).IsRequired(true);
        }
    }
}