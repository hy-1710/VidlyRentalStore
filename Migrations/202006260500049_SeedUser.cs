namespace VidlyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9fdc26d7-7e08-4274-8b6f-094db3a9f4cd', N'hiteshri.y1710@gmail.com', 0, N'ALeh1aT1dOmh4fjL/BVb4jJHUc8LEd92KJtZOhPzN/BkdpKbu/EXqL6DBrgmMt7QxQ==', N'1b2f4959-5697-4216-bb4f-48a94b77eafb', NULL, 0, 0, NULL, 1, 0, N'hiteshri.y1710@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c9a28019-7cea-4c15-9149-fed97c50588f', N'admin@vidly.com', 0, N'AN3pbLiVBf8CNsGM5w8IE3T6oiq+xp5cztj2tf+ePmfyZ5/hFjbj8Ab7CC1OG72HBA==', N'541bb70e-2806-4dd3-a673-397e6c697362', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'aaf7fa29-d2f8-497a-b2b8-ad62acffc45f', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9a28019-7cea-4c15-9149-fed97c50588f', N'aaf7fa29-d2f8-497a-b2b8-ad62acffc45f')");
        }
        
        public override void Down()
        {
        }
    }
}
