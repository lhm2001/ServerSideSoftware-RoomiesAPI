using System;
using Microsoft.EntityFrameworkCore;
using Roomies.API.Domain.Models;
using Roomies.API.Extensions;
using Roomies.API.Publication.Domain.Models;

namespace Roomies.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<FavouritePost> FavouritePosts { get; set; }
        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<Leaseholder> Leaseholders { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Domain.Models.Plan> Plans { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfilePaymentMethod> UserPaymentMethods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rule> Rules { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Rule Entity
            builder.Entity<Rule>().ToTable("Rules");

            builder.Entity<Rule>().HasKey( r=> r.Id);
            builder.Entity<Rule>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();


            //FavouritePost Entity Intermediate Table
            builder.Entity<FavouritePost>().ToTable("FavouritePosts");

            builder.Entity<FavouritePost>().HasKey
                (p => new { p.PostId, p.LeaseholderId });

            builder.Entity<FavouritePost>()
                 .HasOne(pt => pt.Post)
                 .WithMany(p => p.FavouritePosts)
                 .HasForeignKey(pt => pt.PostId);

            builder.Entity<FavouritePost>()
                .HasOne(pt => pt.Leaseholder)
                .WithMany(t => t.FavouritePosts)
                .HasForeignKey(pt => pt.LeaseholderId);


            //Profile Entity
            builder.Entity<Profile>().ToTable("Profiles");
                           
            builder.Entity<Profile>().HasKey(p => p.Id);
            builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Profile>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Profile>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Profile>().Property(p => p.CellPhone).IsRequired().HasMaxLength(9);
            builder.Entity<Profile>().Property(p => p.IdCard).IsRequired().HasMaxLength(8);
            builder.Entity<Profile>().Property(p => p.Description).IsRequired().HasMaxLength(240);
            builder.Entity<Profile>().Property(p => p.Birthday).IsRequired();
            builder.Entity<Profile>().Property(p => p.Department).IsRequired().HasMaxLength(25);
            builder.Entity<Profile>().Property(p => p.Province).IsRequired().HasMaxLength(25);
            builder.Entity<Profile>().Property(p => p.District).IsRequired().HasMaxLength(25);
            builder.Entity<Profile>().Property(p => p.Address).IsRequired().HasMaxLength(100);
            builder.Entity<Profile>().Property(p => p.StartSubscription).IsRequired();
            builder.Entity<Profile>().Property(p => p.EndSubsciption).IsRequired();

            //User Entity
            builder.Entity<User>().ToTable("Users");
                           
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.FirstName).IsRequired();
            builder.Entity<User>().Property(p => p.LastName).IsRequired();
            builder.Entity<User>().Property(p => p.Username).IsRequired();
            builder.Entity<User>().Property(p => p.PasswordHash).IsRequired();

            // Relationships 
            builder.Entity<Profile>()
                .HasOne(owp => owp.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(owp => owp.UserId);


            //Landlord Entity
            builder.Entity<Landlord>().ToTable("Landlords");


            // Relationships 
            builder.Entity<Landlord>()
                .HasMany(p => p.Posts)
                .WithOne(p => p.Landlord) ///
                .HasForeignKey(p => p.LandlordId);


            //leaseholder Entity
            builder.Entity<Leaseholder>().ToTable("Leaseholders");

           
            builder.Entity<Leaseholder>()
                .HasMany(p => p.Reviews)
                .WithOne(p => p.Leaseholder)
                .HasForeignKey(p => p.LeaseholderId);


            // PaymentMethod Entity
            builder.Entity<PaymentMethod>().ToTable("PaymentMethods");

            builder.Entity<PaymentMethod>().HasKey(e => e.Id);
            builder.Entity<PaymentMethod>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PaymentMethod>().Property(e => e.CVV).IsRequired().HasMaxLength(3);
            builder.Entity<PaymentMethod>().Property(e => e.ExpiryDate).IsRequired();
            //--------------------

            // Plan Entity
            builder.Entity<Domain.Models.Plan>().ToTable("Plans");

            // Constraints

            builder.Entity<Domain.Models.Plan>().HasKey(p => p.Id);
            builder.Entity<Domain.Models.Plan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Domain.Models.Plan>().Property(p => p.Price).IsRequired();
            builder.Entity<Domain.Models.Plan>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Domain.Models.Plan>().Property(p => p.Description).IsRequired().HasMaxLength(200);

            //builder.Entity<Plan>()
            //    .HasMany(p => p.Users)
            //    .WithOne(p => p.Plan)
            //    .HasForeignKey(p => p.PlanId);    
            
            // Posts Entity

            builder.Entity<Post>().ToTable("Posts");

            builder.Entity<Post>().HasKey(p => p.Id);
            builder.Entity<Post>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Entity<Post>().Property(p => p.Description).IsRequired().HasMaxLength(500);

            builder.Entity<Post>().Property(p => p.Address).IsRequired().HasMaxLength(50);
            builder.Entity<Post>().Property(p => p.Province).IsRequired().HasMaxLength(25);
            builder.Entity<Post>().Property(p => p.District).IsRequired().HasMaxLength(25);
            builder.Entity<Post>().Property(p => p.Department).IsRequired().HasMaxLength(25);
            builder.Entity<Post>().Property(p => p.Price).IsRequired();
            builder.Entity<Post>().Property(p => p.RoomQuantity).IsRequired();
            builder.Entity<Post>().Property(p => p.PostDate).IsRequired();

            //Relationships
            builder.Entity<Post>()
               .HasMany(p => p.Reviews)
               .WithOne(p => p.Post)
               .HasForeignKey(p => p.PostId);

            builder.Entity<Post>()
                .HasMany(p => p.Rules)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId);
            //-------------------------------------

            // Review Entity

            builder.Entity<Review>().ToTable("Reviews");

            builder.Entity<Review>().HasKey(e => e.Id);
            builder.Entity<Review>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Review>().Property(e => e.Content).IsRequired().HasMaxLength(300);
            builder.Entity<Review>().Property(e => e.Date).IsRequired();

            //UserPaymentMethod Entity Intermediate Table
            builder.Entity<ProfilePaymentMethod>().ToTable("ProfilePaymentMethods");

            builder.Entity<ProfilePaymentMethod>().HasKey(p => new { p.ProfileId, p.PaymentMethodId });

            builder.Entity<ProfilePaymentMethod>()
                 .HasOne(pt => pt.Profile)
                 .WithMany(p => p.ProfilePaymentMethods)
                 .HasForeignKey(pt => pt.ProfileId);

            builder.Entity<ProfilePaymentMethod>()
                .HasOne(pt => pt.PaymentMethod)
                .WithMany(t => t.UserPaymentMethods)
                .HasForeignKey(pt => pt.PaymentMethodId);

            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
