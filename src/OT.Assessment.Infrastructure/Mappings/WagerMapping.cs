using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.Assessment.Infrastructure.Entities;

namespace OT.Assessment.Infrastructure.Mappings
{
    public class WagerMapping : IEntityTypeConfiguration<Wager>
    {
        public void Configure(EntityTypeBuilder<Wager> builder)
        {
            builder.HasIndex(x => x.PlayerAccountId);
            builder.HasIndex(x => x.WagerId);
        }
    }
}
