using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserApp.Models.DB
{
    public partial class TestDbContext : DbContext
    {
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<CurrencyExchange> CurrencyExchange { get; set; }
        public virtual DbSet<CurrencyExchangeStory> CurrencyExchangeStory { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // Unable to generate entity type for table 'dbo.Company'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Symbol'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Exchange'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.EventsStory'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Divident'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.StockDistribution'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Split'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.IntradayPrice'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Spinoff'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.EoDPrice'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=test;User ID=KOZHEMYAKON;Password=dream108");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currencies>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CurrencyCode)
                    .HasColumnName("Currency_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyName)
                    .HasColumnName("Currency_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CurrencyExchange>(entity =>
            {
                entity.Property(e => e.Abbreviation).HasMaxLength(20);
            });

            modelBuilder.Entity<CurrencyExchangeStory>(entity =>
            {
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

      

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.PassHash)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.TokenKey)
                    .IsRequired()
                    .IsUnicode(false);
            });
        }
    }
}
