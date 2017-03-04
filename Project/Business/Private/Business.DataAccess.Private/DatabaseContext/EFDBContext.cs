using System.Data.Entity;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Business.DataAccess.Private.DatabaseContext
{
    public class EFDBContext : IdentityDbContext<ApplicationUserEntity>
    {
        public EFDBContext()
            : base("EFDBContext")
        {
            
        }

        public static EFDBContext Create()
        {
            return new EFDBContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileActionComment>()
                .HasRequired(t => t.ProfileAction)
                .WithMany(t => t.ProfileActionComments)
                .HasForeignKey(d => d.ProfileActionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProfileActionLike>()
                .HasRequired(t => t.ProfileAction)
                .WithMany(t => t.ProfileActionsLikes)
                .HasForeignKey(d => d.ProfileActionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProfileActionCommentLike>()
                .HasRequired(t => t.ProfileActionComment)
                .WithMany(t => t.ProfileActionsCommentsLikes)
                .HasForeignKey(d => d.ProfileActionCommentId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApartmentCommentLike>()
                .HasRequired(t => t.ApartmentComment)
                .WithMany(t => t.ApartmentCommentsLikes)
                .HasForeignKey(d => d.ApartmentCommentId)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(r => new {r.UserId, r.RoleId})
                .ToTable("AspNetUserRoles");

            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(l => l.UserId)
                .ToTable("AspNetUserLogins");

        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageLast> MessagesLast { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentType> ApartmentTypes { get; set; }
        public DbSet<ApartmentOption> ApartmentOptions { get; set; }
        public DbSet<ApartmentPhoto> ApartmentPhotos { get; set; }
        public DbSet<ApartmentPhotoLink> ApartmentPhotosLinks { get; set; }
        public DbSet<ApartmentVisitors> ApartmentVisitors { get; set; }
        public DbSet<ApartmentComment> ApartmentComments { get; set; }
        public DbSet<ApartmentCommentLike> ApartmentCommentLikes { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileSex> ProfileSex { get; set; }
        public DbSet<ProfileAlcohol> ProfileAlcohol { get; set; }
        public DbSet<ProfileSmoking> ProfileSmoking { get; set; }
        public DbSet<ProfileAnimals> ProfileAnimals { get; set; }
        public DbSet<ProfileInteres> ProfileInteres { get; set; }
        public DbSet<ProfileActivity> ProfileActivity { get; set; }
        public DbSet<ProfileAction> ProfileActions { get; set; }
        public DbSet<ProfileActionLike> ProfileActionsLikes { get; set; }
        public DbSet<ProfileActionComment> ProfileActionsComments { get; set; }
        public DbSet<ProfileActionCommentLike> ProfileActionsCommentsLikes { get; set; }


        public DbSet<RegionCountry> Countries { get; set; }
        public DbSet<RegionState> States { get; set; }
        public DbSet<RegionCity> Cities { get; set; }
    }
}
