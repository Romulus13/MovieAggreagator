using DataLayer.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataLayer.Configurations
{
    public class ImdbResultConfiguration :  IEntityTypeConfiguration<ImdbStoredResult>
    {
        public void Configure(EntityTypeBuilder<ImdbStoredResult> builder)
        {

            builder.ToTable("ImdbSearchResults");
            builder.HasKey(sr => sr.Id);

            builder.HasIndex(imdb => imdb.ImdbId).IsUnique();

            builder
                .Property(b => b.Title)
                .HasColumnType("nvarchar(1000)")
                .IsUnicode(true)
                .IsRequired();
            builder
                .Property(b => b.Description)
                .HasColumnType("nvarchar(4000)")
                .IsRequired();
            builder
                .Property(b => b.ImdbId)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder
               .Property(b => b.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();

            builder
                .Property(b => b.Image)
                .HasColumnType("nvarchar(1000)")
                .IsRequired();
            builder
               .Property(b => b.ResultType)
               .HasColumnType("nvarchar(200)")
               .IsRequired();

        }
    }
}
