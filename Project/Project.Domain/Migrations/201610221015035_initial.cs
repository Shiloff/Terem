namespace Project.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApartmentCommentLikes",
                c => new
                    {
                        ApartmentCommentLikeId = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(),
                        ProfileId = c.Long(),
                        ApartmentCommentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ApartmentCommentLikeId)
                .ForeignKey("dbo.ApartmentComments", t => t.ApartmentCommentId, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .Index(t => t.ProfileId)
                .Index(t => t.ApartmentCommentId);
            
            CreateTable(
                "dbo.ApartmentComments",
                c => new
                    {
                        ApartmentCommentId = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(),
                        ProfileId = c.Long(),
                        ApartmentId = c.Long(),
                    })
                .PrimaryKey(t => t.ApartmentCommentId)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .Index(t => t.ProfileId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        ApartmentId = c.Long(nullable: false, identity: true),
                        ProfileId = c.Long(),
                        Country = c.String(),
                        Town = c.String(nullable: false),
                        Adress = c.String(nullable: false),
                        Flat = c.Int(),
                        Floor = c.Int(),
                        FloorTotal = c.Int(),
                        AreaTotal = c.Double(),
                        AreaLiving = c.Double(),
                        AreaKitchen = c.Double(),
                        Description = c.String(),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RemoveDate = c.DateTime(),
                        Published = c.Boolean(),
                        ApartmentTypeId = c.Int(),
                        DefaultPhotoId = c.Long(),
                    })
                .PrimaryKey(t => t.ApartmentId)
                .ForeignKey("dbo.ApartmentPhotoes", t => t.DefaultPhotoId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.ApartmentTypes", t => t.ApartmentTypeId)
                .Index(t => t.ProfileId)
                .Index(t => t.ApartmentTypeId)
                .Index(t => t.DefaultPhotoId);
            
            CreateTable(
                "dbo.ApartmentOptions",
                c => new
                    {
                        ApartmentOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ApartmentOptionId);
            
            CreateTable(
                "dbo.ApartmentPhotoes",
                c => new
                    {
                        ApartmentPhotoId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Link = c.String(),
                        ApartmentId = c.Long(),
                    })
                .PrimaryKey(t => t.ApartmentPhotoId)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.ApartmentPhotoLinks",
                c => new
                    {
                        ApartmentPhotoLinkId = c.Long(nullable: false, identity: true),
                        Link = c.String(),
                        TypeId = c.Int(nullable: false),
                        ApartmentPhotoId = c.Long(),
                    })
                .PrimaryKey(t => t.ApartmentPhotoLinkId)
                .ForeignKey("dbo.ApartmentPhotoes", t => t.ApartmentPhotoId)
                .Index(t => t.ApartmentPhotoId);
            
            CreateTable(
                "dbo.ApartmentVisitors",
                c => new
                    {
                        ProfileId = c.Long(nullable: false),
                        ApartmentId = c.Long(nullable: false),
                        LastDate = c.DateTime(),
                        Count = c.Int(),
                    })
                .PrimaryKey(t => new { t.ProfileId, t.ApartmentId })
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ProfileId = c.Long(nullable: false, identity: true),
                        New = c.Boolean(),
                        UserId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Town = c.String(),
                        Birfday = c.DateTime(),
                        AboutMe = c.String(),
                        ContactPhone = c.String(),
                        ProfileSexId = c.Int(),
                        ProfileActivityId = c.Int(),
                        ProfileSmokingId = c.Int(),
                        ProfileAlcoholId = c.Int(),
                        ProfileAnimalsId = c.Int(),
                        ProfileSexWhoId = c.Int(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        ImageLink = c.String(),
                        ImageType = c.String(),
                        ImageAvatarLink = c.String(),
                        ImageAvatarType = c.String(),
                        ImageAvatarBigLink = c.String(),
                        ImageAvatarBigType = c.String(),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.ProfileActivities", t => t.ProfileActivityId)
                .ForeignKey("dbo.ProfileAlcohols", t => t.ProfileAlcoholId)
                .ForeignKey("dbo.ProfileAnimals", t => t.ProfileAnimalsId)
                .ForeignKey("dbo.ProfileSexes", t => t.ProfileSexId)
                .ForeignKey("dbo.ProfileSexes", t => t.ProfileSexWhoId)
                .ForeignKey("dbo.ProfileSmokings", t => t.ProfileSmokingId)
                .Index(t => t.ProfileSexId)
                .Index(t => t.ProfileActivityId)
                .Index(t => t.ProfileSmokingId)
                .Index(t => t.ProfileAlcoholId)
                .Index(t => t.ProfileAnimalsId)
                .Index(t => t.ProfileSexWhoId);
            
            CreateTable(
                "dbo.ProfileActions",
                c => new
                    {
                        ProfileActionId = c.Long(nullable: false, identity: true),
                        ProfileActionTypeId = c.Int(),
                        ProfileId = c.Long(),
                        ProfileWhoId = c.Long(),
                        Text = c.String(),
                        Date = c.DateTime(),
                        ApartmentId = c.Long(),
                    })
                .PrimaryKey(t => t.ProfileActionId)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.Profiles", t => t.ProfileWhoId)
                .Index(t => t.ProfileId)
                .Index(t => t.ProfileWhoId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.ProfileActionComments",
                c => new
                    {
                        ProfileActionCommentId = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(),
                        ProfileId = c.Long(),
                        ProfileActionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileActionCommentId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.ProfileActions", t => t.ProfileActionId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.ProfileActionId);
            
            CreateTable(
                "dbo.ProfileActionCommentLikes",
                c => new
                    {
                        ProfileActionCommentLikeId = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(),
                        ProfileId = c.Long(),
                        ProfileActionCommentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileActionCommentLikeId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.ProfileActionComments", t => t.ProfileActionCommentId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.ProfileActionCommentId);
            
            CreateTable(
                "dbo.ProfileActionLikes",
                c => new
                    {
                        ProfileActionLikeId = c.Long(nullable: false, identity: true),
                        ProfileActionId = c.Long(nullable: false),
                        ProfileId = c.Long(),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProfileActionLikeId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.ProfileActions", t => t.ProfileActionId, cascadeDelete: true)
                .Index(t => t.ProfileActionId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.ProfileActivities",
                c => new
                    {
                        ProfileActivityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProfileActivityId);
            
            CreateTable(
                "dbo.ProfileAlcohols",
                c => new
                    {
                        ProfileAlcoholId = c.Int(nullable: false, identity: true),
                        Icon = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProfileAlcoholId);
            
            CreateTable(
                "dbo.ProfileAnimals",
                c => new
                    {
                        ProfileAnimalsId = c.Int(nullable: false, identity: true),
                        Icon = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProfileAnimalsId);
            
            CreateTable(
                "dbo.ProfileInteres",
                c => new
                    {
                        ProfileInteresId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentProfileInteresId = c.Int(),
                    })
                .PrimaryKey(t => t.ProfileInteresId)
                .ForeignKey("dbo.ProfileInteres", t => t.ParentProfileInteresId)
                .Index(t => t.ParentProfileInteresId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Long(nullable: false, identity: true),
                        ProfileIdFrom = c.Long(),
                        ProfileIdTo = c.Long(),
                        DT = c.DateTime(),
                        MessageText = c.String(),
                        Read = c.Boolean(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Profiles", t => t.ProfileIdFrom)
                .ForeignKey("dbo.Profiles", t => t.ProfileIdTo)
                .Index(t => t.ProfileIdFrom)
                .Index(t => t.ProfileIdTo);
            
            CreateTable(
                "dbo.ProfileSexes",
                c => new
                    {
                        ProfileSexId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.ProfileSexId);
            
            CreateTable(
                "dbo.ProfileSmokings",
                c => new
                    {
                        ProfileSmokingId = c.Int(nullable: false, identity: true),
                        Icon = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProfileSmokingId);
            
            CreateTable(
                "dbo.ApartmentTypes",
                c => new
                    {
                        ApartmentTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ApartmentTypeId);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        MessageChatId = c.Long(nullable: false, identity: true),
                        ProfileId = c.Long(nullable: false),
                        ConnectionId = c.String(),
                    })
                .PrimaryKey(t => t.MessageChatId);
            
            CreateTable(
                "dbo.RegionCities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Sort = c.Int(nullable: false),
                        State_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RegionStates", t => t.State_Id)
                .Index(t => t.State_Id);
            
            CreateTable(
                "dbo.RegionStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Sort = c.Int(nullable: false),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RegionCountries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.RegionCountries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageLasts",
                c => new
                    {
                        MessageLastId = c.Long(nullable: false, identity: true),
                        ProfileIdFrom = c.Long(),
                        ProfileIdTo = c.Long(),
                        DT = c.DateTime(),
                    })
                .PrimaryKey(t => t.MessageLastId)
                .ForeignKey("dbo.Profiles", t => t.ProfileIdFrom)
                .ForeignKey("dbo.Profiles", t => t.ProfileIdTo)
                .Index(t => t.ProfileIdFrom)
                .Index(t => t.ProfileIdTo);
            
            CreateTable(
                "dbo.ApartmentOptionApartments",
                c => new
                    {
                        ApartmentOption_ApartmentOptionId = c.Int(nullable: false),
                        Apartment_ApartmentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApartmentOption_ApartmentOptionId, t.Apartment_ApartmentId })
                .ForeignKey("dbo.ApartmentOptions", t => t.ApartmentOption_ApartmentOptionId, cascadeDelete: true)
                .ForeignKey("dbo.Apartments", t => t.Apartment_ApartmentId, cascadeDelete: true)
                .Index(t => t.ApartmentOption_ApartmentOptionId)
                .Index(t => t.Apartment_ApartmentId);
            
            CreateTable(
                "dbo.ProfileInteresProfiles",
                c => new
                    {
                        ProfileInteres_ProfileInteresId = c.Int(nullable: false),
                        Profile_ProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfileInteres_ProfileInteresId, t.Profile_ProfileId })
                .ForeignKey("dbo.ProfileInteres", t => t.ProfileInteres_ProfileInteresId, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.Profile_ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileInteres_ProfileInteresId)
                .Index(t => t.Profile_ProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageLasts", "ProfileIdTo", "dbo.Profiles");
            DropForeignKey("dbo.MessageLasts", "ProfileIdFrom", "dbo.Profiles");
            DropForeignKey("dbo.RegionStates", "Country_Id", "dbo.RegionCountries");
            DropForeignKey("dbo.RegionCities", "State_Id", "dbo.RegionStates");
            DropForeignKey("dbo.ApartmentCommentLikes", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ApartmentCommentLikes", "ApartmentCommentId", "dbo.ApartmentComments");
            DropForeignKey("dbo.ApartmentComments", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ApartmentComments", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Apartments", "ApartmentTypeId", "dbo.ApartmentTypes");
            DropForeignKey("dbo.Apartments", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ApartmentVisitors", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "ProfileSmokingId", "dbo.ProfileSmokings");
            DropForeignKey("dbo.Profiles", "ProfileSexWhoId", "dbo.ProfileSexes");
            DropForeignKey("dbo.Profiles", "ProfileSexId", "dbo.ProfileSexes");
            DropForeignKey("dbo.Messages", "ProfileIdTo", "dbo.Profiles");
            DropForeignKey("dbo.Messages", "ProfileIdFrom", "dbo.Profiles");
            DropForeignKey("dbo.ProfileInteresProfiles", "Profile_ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ProfileInteresProfiles", "ProfileInteres_ProfileInteresId", "dbo.ProfileInteres");
            DropForeignKey("dbo.ProfileInteres", "ParentProfileInteresId", "dbo.ProfileInteres");
            DropForeignKey("dbo.Profiles", "ProfileAnimalsId", "dbo.ProfileAnimals");
            DropForeignKey("dbo.Profiles", "ProfileAlcoholId", "dbo.ProfileAlcohols");
            DropForeignKey("dbo.Profiles", "ProfileActivityId", "dbo.ProfileActivities");
            DropForeignKey("dbo.ProfileActions", "ProfileWhoId", "dbo.Profiles");
            DropForeignKey("dbo.ProfileActionLikes", "ProfileActionId", "dbo.ProfileActions");
            DropForeignKey("dbo.ProfileActionLikes", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ProfileActionCommentLikes", "ProfileActionCommentId", "dbo.ProfileActionComments");
            DropForeignKey("dbo.ProfileActionCommentLikes", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ProfileActionComments", "ProfileActionId", "dbo.ProfileActions");
            DropForeignKey("dbo.ProfileActionComments", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ProfileActions", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ProfileActions", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentVisitors", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentPhotoLinks", "ApartmentPhotoId", "dbo.ApartmentPhotoes");
            DropForeignKey("dbo.Apartments", "DefaultPhotoId", "dbo.ApartmentPhotoes");
            DropForeignKey("dbo.ApartmentPhotoes", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentOptionApartments", "Apartment_ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentOptionApartments", "ApartmentOption_ApartmentOptionId", "dbo.ApartmentOptions");
            DropIndex("dbo.ProfileInteresProfiles", new[] { "Profile_ProfileId" });
            DropIndex("dbo.ProfileInteresProfiles", new[] { "ProfileInteres_ProfileInteresId" });
            DropIndex("dbo.ApartmentOptionApartments", new[] { "Apartment_ApartmentId" });
            DropIndex("dbo.ApartmentOptionApartments", new[] { "ApartmentOption_ApartmentOptionId" });
            DropIndex("dbo.MessageLasts", new[] { "ProfileIdTo" });
            DropIndex("dbo.MessageLasts", new[] { "ProfileIdFrom" });
            DropIndex("dbo.RegionStates", new[] { "Country_Id" });
            DropIndex("dbo.RegionCities", new[] { "State_Id" });
            DropIndex("dbo.Messages", new[] { "ProfileIdTo" });
            DropIndex("dbo.Messages", new[] { "ProfileIdFrom" });
            DropIndex("dbo.ProfileInteres", new[] { "ParentProfileInteresId" });
            DropIndex("dbo.ProfileActionLikes", new[] { "ProfileId" });
            DropIndex("dbo.ProfileActionLikes", new[] { "ProfileActionId" });
            DropIndex("dbo.ProfileActionCommentLikes", new[] { "ProfileActionCommentId" });
            DropIndex("dbo.ProfileActionCommentLikes", new[] { "ProfileId" });
            DropIndex("dbo.ProfileActionComments", new[] { "ProfileActionId" });
            DropIndex("dbo.ProfileActionComments", new[] { "ProfileId" });
            DropIndex("dbo.ProfileActions", new[] { "ApartmentId" });
            DropIndex("dbo.ProfileActions", new[] { "ProfileWhoId" });
            DropIndex("dbo.ProfileActions", new[] { "ProfileId" });
            DropIndex("dbo.Profiles", new[] { "ProfileSexWhoId" });
            DropIndex("dbo.Profiles", new[] { "ProfileAnimalsId" });
            DropIndex("dbo.Profiles", new[] { "ProfileAlcoholId" });
            DropIndex("dbo.Profiles", new[] { "ProfileSmokingId" });
            DropIndex("dbo.Profiles", new[] { "ProfileActivityId" });
            DropIndex("dbo.Profiles", new[] { "ProfileSexId" });
            DropIndex("dbo.ApartmentVisitors", new[] { "ApartmentId" });
            DropIndex("dbo.ApartmentVisitors", new[] { "ProfileId" });
            DropIndex("dbo.ApartmentPhotoLinks", new[] { "ApartmentPhotoId" });
            DropIndex("dbo.ApartmentPhotoes", new[] { "ApartmentId" });
            DropIndex("dbo.Apartments", new[] { "DefaultPhotoId" });
            DropIndex("dbo.Apartments", new[] { "ApartmentTypeId" });
            DropIndex("dbo.Apartments", new[] { "ProfileId" });
            DropIndex("dbo.ApartmentComments", new[] { "ApartmentId" });
            DropIndex("dbo.ApartmentComments", new[] { "ProfileId" });
            DropIndex("dbo.ApartmentCommentLikes", new[] { "ApartmentCommentId" });
            DropIndex("dbo.ApartmentCommentLikes", new[] { "ProfileId" });
            DropTable("dbo.ProfileInteresProfiles");
            DropTable("dbo.ApartmentOptionApartments");
            DropTable("dbo.MessageLasts");
            DropTable("dbo.RegionCountries");
            DropTable("dbo.RegionStates");
            DropTable("dbo.RegionCities");
            DropTable("dbo.Chats");
            DropTable("dbo.ApartmentTypes");
            DropTable("dbo.ProfileSmokings");
            DropTable("dbo.ProfileSexes");
            DropTable("dbo.Messages");
            DropTable("dbo.ProfileInteres");
            DropTable("dbo.ProfileAnimals");
            DropTable("dbo.ProfileAlcohols");
            DropTable("dbo.ProfileActivities");
            DropTable("dbo.ProfileActionLikes");
            DropTable("dbo.ProfileActionCommentLikes");
            DropTable("dbo.ProfileActionComments");
            DropTable("dbo.ProfileActions");
            DropTable("dbo.Profiles");
            DropTable("dbo.ApartmentVisitors");
            DropTable("dbo.ApartmentPhotoLinks");
            DropTable("dbo.ApartmentPhotoes");
            DropTable("dbo.ApartmentOptions");
            DropTable("dbo.Apartments");
            DropTable("dbo.ApartmentComments");
            DropTable("dbo.ApartmentCommentLikes");
        }
    }
}
