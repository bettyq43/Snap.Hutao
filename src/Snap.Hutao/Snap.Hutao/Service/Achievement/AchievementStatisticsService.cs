﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Model.Entity;
using Snap.Hutao.Model.Entity.Database;
using Snap.Hutao.Model.Primitive;
using Snap.Hutao.ViewModel.Achievement;
using EntityAchievement = Snap.Hutao.Model.Entity.Achievement;
using MetadataAchievement = Snap.Hutao.Model.Metadata.Achievement.Achievement;

namespace Snap.Hutao.Service.Achievement;

[ConstructorGenerated]
[Injection(InjectAs.Scoped, typeof(IAchievementStatisticsService))]
internal sealed partial class AchievementStatisticsService : IAchievementStatisticsService
{
    private readonly IAchievementDbService achievementDbService;
    private readonly IServiceProvider serviceProvider;
    private readonly ITaskContext taskContext;

    /// <inheritdoc/>
    public async Task<List<AchievementStatistics>> GetAchievementStatisticsAsync(Dictionary<AchievementId, MetadataAchievement> achievementMap)
    {
        await taskContext.SwitchToBackgroundAsync();
        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<AchievementStatistics> results = new();
            foreach (AchievementArchive archive in appDbContext.AchievementArchives)
            {
                int finishedCount = await achievementDbService
                    .GetFinishedAchievementCountByArchiveIdAsync(archive.InnerId)
                    .ConfigureAwait(false);

                int totalCount = achievementMap.Count;

                List<EntityAchievement> achievements = await achievementDbService
                    .GetLatestFinishedAchievementListByArchiveIdAsync(archive.InnerId, 2)
                    .ConfigureAwait(false);

                results.Add(new()
                {
                    DisplayName = archive.Name,
                    FinishDescription = AchievementStatistics.Format(finishedCount, totalCount, out _),
                    Achievements = achievements.SelectList(entity => new AchievementView(entity, achievementMap[entity.Id])),
                });
            }

            return results;
        }
    }
}