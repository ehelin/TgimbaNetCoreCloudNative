using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DALNetCore.Models
{
    public partial class BucketListContext : DbContext
    {
        private bool useTestDatabase;

        public BucketListContext(bool userTestDatabase = false)
        {
            this.useTestDatabase = userTestDatabase;
        }

        public BucketListContext(DbContextOptions<BucketListContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BucketListItem> BucketListItems { get; set; }
        public virtual DbSet<BucketListUser> BucketListUsers { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<BuildStatistics> BuildStatistics { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<SystemStatistics> SystemStatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (useTestDatabase)
                {
                    // TODO - move to EnvironmentalConfig.GetDbSetting
                    optionsBuilder.UseNpgsql(
                        String.Format("Host={0};Database={1};Username={2};Password={3}",
                            "localhost",
                            "postgres",
                            "postgres",
                            "localPostgres"
                        ));
                }
                else
                {
                    // TODO - add production and move to EnvironmentalConfig.GetDbSetting()    
                    optionsBuilder.UseNpgsql(
                        String.Format("Host={0};Database={1};Username={2};Password={3}",
                            "",
                            "tgimba",
                            "",
                            ""
                    ));
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<BucketListItem>(entity =>
            {
                entity.ToTable("bucketlistitem", "bucket");

                entity.Property(e => e.BucketListItemId)
                    .HasColumnName("bucketlistitemid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Achieved).HasColumnName("achieved");

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .HasColumnName("category");

                entity.Property(e => e.CategorySortOrder).HasColumnName("categorysortorder");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created");

                entity.Property(e => e.Latitude)
                    .HasPrecision(18, 10)
                    .HasColumnName("latitude");

                entity.Property(e => e.ListItemName)
                    .HasColumnType("character varying")
                    .HasColumnName("listitemname");

                entity.Property(e => e.Longitude)
                    .HasPrecision(18, 10)
                    .HasColumnName("longitude");
            });

            modelBuilder.Entity<BucketListUser>(entity =>
            {
                entity.ToTable("bucketlistuser", "bucket");

                entity.Property(e => e.BucketListUserId)
                    .HasColumnName("bucketlistuserid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BucketListItemId).HasColumnName("bucketlistitemid");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.BucketListItem)
                    .WithMany(p => p.BucketListUsers)
                    .HasForeignKey(d => d.BucketListItemId)
                    .HasConstraintName("bucketlistuser_bucketlistitemid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BucketListUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("bucketlistuser_userid_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("bucketuser_pkey");

                entity.ToTable("bucketuser", "bucket");

                entity.Property(e => e.UserId)
                    .HasColumnName("userid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .HasColumnName("createdby");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Modified)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("modified");

                entity.Property(e => e.ModifiedBy)
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

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<BuildStatistics>(entity =>
            {
                entity.ToTable("buildstatistics", "bucket");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BuildNumber)
                    .HasMaxLength(500)
                    .HasColumnName("buildnumber");

                entity.Property(e => e.EndTime)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("endtime");

                entity.Property(e => e.StartTime)
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

                entity.Property(e => e.LogId)
                    .HasColumnName("logid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created");

                entity.Property(e => e.LogMessage)
                    .HasColumnType("character varying")
                    .HasColumnName("messate");
            });

            modelBuilder.Entity<SystemStatistics>(entity =>
            {
                entity.ToTable("systemstatistics", "bucket");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AzureFunctionIsUp).HasColumnName("azurefunctionisup");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created");

                entity.Property(e => e.DatabaseIsUp).HasColumnName("databaseisup");

                entity.Property(e => e.WebsiteIsUp).HasColumnName("websiteisup");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
