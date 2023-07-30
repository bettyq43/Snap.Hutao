﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Core.ExceptionService;
using Snap.Hutao.Model.Entity;
using Snap.Hutao.Model.InterChange.GachaLog;
using Snap.Hutao.Service.Metadata;
using System.Collections.ObjectModel;

namespace Snap.Hutao.Service.GachaLog;

/// <summary>
/// 祈愿记录导入服务
/// </summary>
[ConstructorGenerated]
[Injection(InjectAs.Scoped, typeof(IUIGFImportService))]
internal sealed partial class UIGFImportService : IUIGFImportService
{
    private readonly ILogger<UIGFImportService> logger;
    private readonly MetadataOptions metadataOptions;
    private readonly IServiceProvider serviceProvider;
    private readonly IGachaLogDbService gachaLogDbService;
    private readonly ITaskContext taskContext;

    /// <inheritdoc/>
    public async ValueTask<GachaArchive> ImportAsync(GachaLogServiceMetadataContext context, UIGF uigf, ObservableCollection<GachaArchive> archives)
    {
        await taskContext.SwitchToBackgroundAsync();

        if (!metadataOptions.IsCurrentLocale(uigf.Info.Language))
        {
            string message = string.Format(SH.ServiceGachaUIGFImportLanguageNotMatch, uigf.Info.Language, metadataOptions.LanguageCode);
            ThrowHelper.InvalidOperation(message, null);
        }

        GachaArchiveOperation.GetOrAdd(serviceProvider, uigf.Info.Uid, archives, out GachaArchive? archive);
        Guid archiveId = archive.InnerId;

        long trimId = gachaLogDbService.GetOldestGachaItemIdByArchiveId(archiveId);

        logger.LogInformation("Last Id to trim with: [{Id}]", trimId);

        _ = uigf.IsCurrentVersionSupported(out UIGFVersion version);

        List<GachaItem> toAdd = version switch
        {
            UIGFVersion.Major2Minor3OrHigher => uigf.List
                .OrderByDescending(i => i.Id)
                .Where(i => i.Id < trimId)
                .Select(i => GachaItem.From(archiveId, i))
                .ToList(),
            UIGFVersion.Major2Minor2OrLower => uigf.List
                .OrderByDescending(i => i.Id)
                .Where(i => i.Id < trimId)
                .Select(i => GachaItem.From(archiveId, i, context.GetItemId(i)))
                .ToList(),
            _ => new(),
        };

        await gachaLogDbService.AddGachaItemsAsync(toAdd).ConfigureAwait(false);
        return archive;
    }
}