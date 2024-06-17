using CleanTemplate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTemplate.Infrastructure.Context.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(b => b.Title).HasColumnName("Title").IsRequired();
            builder.Property(b => b.AuthorId).HasColumnName("AuthorId").IsRequired();
            builder.HasOne(b => b.Author).WithMany().HasForeignKey(b => b.AuthorId);
        }
    }
}
