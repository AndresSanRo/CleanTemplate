using CleanTemplate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTemplate.Infrastructure.Context.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
        }
    }
}
