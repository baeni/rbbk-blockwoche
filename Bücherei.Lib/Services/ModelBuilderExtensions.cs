using Bücherei.Lib.EntitiesRelational;
using Microsoft.EntityFrameworkCore;

namespace Bücherei.Lib.Services;

public static class ModelBuilderExtensions
{
    public static void SeedRel(this ModelBuilder modelBuilder)
    {
        var relData = SampleData.GetRel();

        modelBuilder.Entity<Autor>().HasData(relData.autorenRel);
        modelBuilder.Entity<BuechereiRel>().HasData(relData.buechereiRels);
        modelBuilder.Entity<Buch>().HasData(relData.buecherRel);


        // n to m

        List<AutorBuecherei> autorenBuechereienRel = new();

        for (int i = 1; i <= relData.buechereiRels.Length; i++)
        {
            var indexes = relData.libAuthorsIds[i];

            for (int j = 0; j < indexes.Count; j++)
            {
                autorenBuechereienRel.Add(new AutorBuecherei { AutorId = indexes[j], BuechereiId = i } );
            }
        }

        modelBuilder.Entity<BuechereiRel>()
            .HasMany(u => u.Autoren)
            .WithMany(r => r.Buechereien)
            .UsingEntity<AutorBuecherei>(j => j.HasData(
                autorenBuechereienRel
            ));

        //modelBuilder.Entity<User>()
        //    .HasMany(u => u.Roles)
        //    .WithMany(r => r.Users)
        //    .UsingEntity(j => j.HasData(
        //        new { UsersUserId = 1, RolesRoleId = 1 },
        //        new { UsersUserId = 2, RolesRoleId = 2 },
        //        new { UsersUserId = 3, RolesRoleId = 3 },
        //        new { UsersUserId = 3, RolesRoleId = 2 },
        //        new { UsersUserId = 3, RolesRoleId = 1 }
        //    ));

        //modelBuilder.Entity<Role>().HasData(
        //    new Role { RoleId = 1, RoleName = "Creator" },
        //    new Role { RoleId = 2, RoleName = "Approver" },
        //    new Role { RoleId = 3, RoleName = "Reviewer" }
        //    );
        //modelBuilder.Entity<ProjectDraft>().HasData(
        //    new ProjectDraft { Id = 1, Title = "Draft #1", Description = DescriptionConstants.Words100, LikeCount = 17, State = ProjectDraftState.Accepted, CreatedAt = new DateTime(2024, 8, 13), CreatorId = 1 },
        //    new ProjectDraft { Id = 2, Title = "Draft #2", Description = DescriptionConstants.Words200, LikeCount = 34, State = ProjectDraftState.Accepted, CreatedAt = new DateTime(2024, 8, 12), CreatorId = 2 },
        //    new ProjectDraft { Id = 3, Title = "Draft #3", Description = DescriptionConstants.Words100, LikeCount = 2, State = ProjectDraftState.Denied, CreatedAt = new DateTime(2024, 8, 14), CreatorId = 3 },
        //    new ProjectDraft { Id = 4, Title = "Draft #4", Description = DescriptionConstants.Words200, LikeCount = 0, State = ProjectDraftState.Accepted, CreatedAt = new DateTime(2024, 8, 1), CreatorId = 1 },
        //    new ProjectDraft { Id = 5, Title = "Draft #5", Description = DescriptionConstants.Words100, LikeCount = 345, State = ProjectDraftState.Denied, CreatedAt = new DateTime(2024, 7, 13), CreatorId = 1 },
        //    new ProjectDraft { Id = 6, Title = "Draft #6", Description = DescriptionConstants.Words200, LikeCount = 65, State = ProjectDraftState.Accepted, CreatedAt = new DateTime(2024, 8, 13), CreatorId = 2 },
        //    new ProjectDraft { Id = 7, Title = "Draft #7", Description = DescriptionConstants.Search, LikeCount = 7, State = ProjectDraftState.Accepted, CreatedAt = new DateTime(2024, 7, 23), CreatorId = 3 },
        //    new ProjectDraft { Id = 8, Title = "Draft #8", Description = DescriptionConstants.Words100, LikeCount = 8, State = ProjectDraftState.Denied, CreatedAt = new DateTime(2024, 8, 13), CreatorId = 2 },
        //    new ProjectDraft { Id = 9, Title = "Draft #9", Description = DescriptionConstants.Search, LikeCount = 24, State = ProjectDraftState.Accepted, CreatedAt = new DateTime(2024, 6, 4), CreatorId = 3 },
        //    new ProjectDraft { Id = 10, Title = "Draft #10", Description = DescriptionConstants.Words200, LikeCount = 1, State = ProjectDraftState.Accepted, CreatedAt = new DateTime(2024, 8, 13), CreatorId = 2 },
        //    new ProjectDraft { Id = 11, Title = "Draft #11", Description = DescriptionConstants.Search, LikeCount = 9, State = ProjectDraftState.Denied, CreatedAt = new DateTime(2024, 8, 15), CreatorId = 3 },
        //    new ProjectDraft { Id = 12, Title = "Draft #12", Description = DescriptionConstants.Words100, LikeCount = 2, State = ProjectDraftState.Accepted, CreatedAt = new DateTime(2024, 8, 2), CreatorId = 1 },
        //    new ProjectDraft { Id = 13, Title = "Draft #13", Description = DescriptionConstants.Words200, LikeCount = 43, State = ProjectDraftState.Denied, CreatedAt = new DateTime(2024, 4, 3), CreatorId = 2 }
        //    );
        //modelBuilder.Entity<FeedbackTemplateHolder>().HasData(
        //    new FeedbackTemplateHolder { Id = 1, UserId = 1 },
        //    new FeedbackTemplateHolder { Id = 2, UserId = 2 },
        //    new FeedbackTemplateHolder { Id = 3, UserId = 3 }
        //    );
        //modelBuilder.Entity<User>().HasData(
        //    new User { UserId = 1, FirstName = "Miro", LastName = "Klose", Email = "miroklose@google.de" },
        //    new User { UserId = 2, FirstName = "Klaus", LastName = "Kammerer", Email = "klauskammerer@google.de" },
        //    new User { UserId = 3, FirstName = "Hansi", LastName = "Holzbein", Email = "hansiholzbein@google.de" }
        //    );
        //modelBuilder.Entity<User>()
        //    .HasMany(u => u.Roles)
        //    .WithMany(r => r.Users)
        //    .UsingEntity(j => j.HasData(
        //        new { UsersUserId = 1, RolesRoleId = 1 },
        //        new { UsersUserId = 2, RolesRoleId = 2 },
        //        new { UsersUserId = 3, RolesRoleId = 3 },
        //        new { UsersUserId = 3, RolesRoleId = 2 },
        //        new { UsersUserId = 3, RolesRoleId = 1 }
        //    ));
    }
}
