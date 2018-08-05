using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmartCity3
{
    public partial class _1718_etu32294_DB_SmartContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<Breed> Breed { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<ApplicationUser> User { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        public _1718_etu32294_DB_SmartContext(DbContextOptions<_1718_etu32294_DB_SmartContext>options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").IsRequired();

                entity.Property(e => e.IdBreed).HasColumnName("idBreed").IsRequired();

                entity.Property(e => e.IdColor)
                    .IsRequired()
                    .HasColumnName("idColor")
                    .HasMaxLength(25);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(25);
                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasColumnName("idPerson")
                    .HasMaxLength(450);

                entity.HasOne(d => d.IdBreedNavigation)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(d => d.IdBreed)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Animal__idBreed__1DE57479");

                entity.HasOne(d => d.IdColorNavigation)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(d => d.IdColor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Animal__idColor__1ED998B2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(d => d.IdUser) 
                    .HasConstraintName("FK_ApplicationUser_idUser");
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").IsRequired();

                entity.Property(e => e.CoordX).HasColumnName("coordX");

                entity.Property(e => e.CoordY).HasColumnName("coordY");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(100);

                entity.Property(e => e.IdAnimal).HasColumnName("idAnimal").IsRequired();

                entity.Property(e => e.IdStatus).HasColumnName("idStatut").IsRequired();

                entity.HasOne(d => d.IdAnimalNavigation)
                    .WithMany(p => p.Announcement)
                    .HasForeignKey(d => d.IdAnimal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Announcem__idAni__267ABA7A");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Announcement)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Announcem__idSta__25869641");
            });

            modelBuilder.Entity<Breed>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).HasColumnName("id").IsRequired();
                entity.Property(e => e.IdSpecies).HasColumnName("idSpecies");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(25);

                entity.HasOne(d => d.IdSpeciesNavigation)
                    .WithMany(p => p.Breed)
                    .HasForeignKey(d => d.IdSpecies)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Breed__idSpecies__1920BF5C");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAnimal).HasColumnName("idAnimal");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(25);

                entity.HasOne(d => d.IdAnimalNavigation)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.IdAnimal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Images__idAnimal__22AA2996");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserName).HasMaxLength(25);

                entity.Property(e => e.PasswordHash).HasMaxLength(25);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);
               
            });

            modelBuilder.Entity<Species>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.State).HasMaxLength(20);
            });
            
        }
    }
}
