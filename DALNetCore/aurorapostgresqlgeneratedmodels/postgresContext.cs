using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DALNetCore.aurorapostgresqlgeneratedmodels
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bucketlistitem> Bucketlistitems { get; set; }
        public virtual DbSet<Bucketlistuser> Bucketlistusers { get; set; }
        public virtual DbSet<Bucketuser> Bucketusers { get; set; }
        public virtual DbSet<Buildstatistic> Buildstatistics { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Systemstatistic> Systemstatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=localPostgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Bucketlistitem>(entity =>
            {
                entity.ToTable("bucketlistitem", "bucket");

                entity.Property(e => e.Bucketlistitemid)
                    .HasColumnName("bucketlistitemid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Achieved).HasColumnName("achieved");

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .HasColumnName("category");

                entity.Property(e => e.Categorysortorder).HasColumnName("categorysortorder");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created");

                entity.Property(e => e.Latitude)
                    .HasPrecision(18, 10)
                    .HasColumnName("latitude");

                entity.Property(e => e.Listitemname)
                    .HasColumnType("character varying")
                    .HasColumnName("listitemname");

                entity.Property(e => e.Longitude)
                    .HasPrecision(18, 10)
                    .HasColumnName("longitude");
            });

            modelBuilder.Entity<Bucketlistuser>(entity =>
            {
                entity.ToTable("bucketlistuser", "bucket");

                entity.Property(e => e.Bucketlistuserid)
                    .HasColumnName("bucketlistuserid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Bucketlistitemid).HasColumnName("bucketlistitemid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Bucketlistitem)
                    .WithMany(p => p.Bucketlistusers)
                    .HasForeignKey(d => d.Bucketlistitemid)
                    .HasConstraintName("bucketlistuser_bucketlistitemid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bucketlistusers)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("bucketlistuser_userid_fkey");
            });

            modelBuilder.Entity<Bucketuser>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("bucketuser_pkey");

                entity.ToTable("bucketuser", "bucket");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created");

                entity.Property(e => e.Createdby)
                    .HasMaxLength(255)
                    .HasColumnName("createdby");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Modified)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("modified");

                entity.Property(e => e.Modifiedby)
                    .HasMaxLength(255)
                    .HasColumnName("modifiedby");

                entity.Property(e => e.Password)
                    .HasColumnType("character varying")
                    .HasColumnName("password");

                entity.Property(e => e.Salt)
                    .HasColumnType("character varying")
                    .HasColumnName("salt");

                entity.Property(e => e.Token)
                    .HasMaxLength(1000)
                    .HasColumnName("token");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Buildstatistic>(entity =>
            {
                entity.ToTable("buildstatistics", "bucket");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Buildnumber)
                    .HasMaxLength(500)
                    .HasColumnName("buildnumber");

                entity.Property(e => e.Endtime)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("endtime");

                entity.Property(e => e.Starttime)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("starttime");

                entity.Property(e => e.Status)
                    .HasMaxLength(500)
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(500)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log", "bucket");

                entity.Property(e => e.Logid)
                    .HasColumnName("logid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created");

                entity.Property(e => e.Messate)
                    .HasColumnType("character varying")
                    .HasColumnName("messate");
            });

            modelBuilder.Entity<Systemstatistic>(entity =>
            {
                entity.ToTable("systemstatistics", "bucket");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Azurefunctionisup).HasColumnName("azurefunctionisup");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created");

                entity.Property(e => e.Databaseisup).HasColumnName("databaseisup");

                entity.Property(e => e.Websiteisup).HasColumnName("websiteisup");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
