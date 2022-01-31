using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations
{
    public class YoutubeResultConfiguration :  IEntityTypeConfiguration<YoutubeStoredResult>
    {
        public void Configure(EntityTypeBuilder<YoutubeStoredResult> builder)
        {
            builder.ToTable("YoutubeSearchResults");

            builder.HasKey(sr => sr.Id);

            builder
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(b => b.Title)
                .IsRequired(false)
                .HasColumnType("nvarchar(500)")
                .IsUnicode();
            builder
                .Property(b => b.Description)
                .IsRequired(false)
                .HasColumnType("nvarchar(max)")
                .IsUnicode();
            builder
                .Property(b => b.ResourceId)
                .IsRequired(true)
                .HasColumnType("nvarchar(500)")
                .IsUnicode();


            builder
                .Property(b => b.Type)
                .HasColumnType("tinyint")
                .IsRequired(true);

            //could be full text
            builder.HasIndex(b => b.ResourceId).IsUnique(true);

            builder.HasIndex(b => b.Title).IsUnique(false).IsClustered(false).HasDatabaseName("IX_YoutubeSearchResults_Title");
        }
    }
}
