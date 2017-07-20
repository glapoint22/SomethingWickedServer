using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SomethingWickedServer.Models
{
    public partial class Something_WickedContext : DbContext
    {
        public Something_WickedContext(DbContextOptions<Something_WickedContext> options) : base(options) { }

        public virtual DbSet<Bios> Bios { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ShowcaseImages> ShowcaseImages { get; set; }
        public virtual DbSet<Songs> Songs { get; set; }
        public virtual DbSet<Venues> Venues { get; set; }
        public virtual DbSet<VideoGroups> VideoGroups { get; set; }
        public virtual DbSet<Videos> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bios>(entity =>
            {
                entity.HasKey(e => e.BioId)
                    .HasName("PK__Bios__4A3178A74C08A7EC");

                entity.Property(e => e.BioId).HasColumnName("BioID");

                entity.Property(e => e.Bio)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Bios)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Bios__MemberID__2A164134");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Members>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Thumbnail)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Photos>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Thumbnail)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.DateTime)
                    .HasName("PK_Schedule");

                entity.Property(e => e.VenueId).HasColumnName("VenueID");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.VenueId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Schedule__VenueI__36B12243");
            });

            modelBuilder.Entity<ShowcaseImages>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Songs>(entity =>
            {
                entity.HasKey(e => e.Song)
                    .HasName("PK__Songs__BC3BBF0F24D0ACD0");

                entity.Property(e => e.Song).HasColumnType("varchar(50)");

                entity.Property(e => e.Artist)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.VideoGroup).HasColumnType("varchar(20)");

                entity.Property(e => e.VideoId)
                    .HasColumnName("VideoID")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Songs__GenreID__4CA06362");
            });

            modelBuilder.Entity<Venues>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("URL")
                    .HasColumnType("varchar(2083)");
            });

            modelBuilder.Entity<VideoGroups>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Thumbnail)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Videos>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });
        }
    }
}