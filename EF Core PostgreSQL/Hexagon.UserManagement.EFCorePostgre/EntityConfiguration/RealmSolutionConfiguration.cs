using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Hexagon.UserManagement.EFCorePostgre.Entity;

namespace Hexagon.UserManagement.EFCorePostgre.EntityConfiguration
{
    public class RealmSolutionConfiguration : IEntityTypeConfiguration<RealmSolution>
    {
        public void Configure(EntityTypeBuilder<RealmSolution> builder)
        {
            builder.HasKey(rs => new { rs.RlmId, rs.SlnId });

            builder.HasOne(pc => pc.Realm)
                .WithMany(p => p.RealmSolutions)
                .HasForeignKey(pc => pc.RlmId);

            builder.HasOne(pc => pc.Solution)
                .WithMany(c => c.RealmSolutions)
                .HasForeignKey(pc => pc.SlnId);
        }
    }
}