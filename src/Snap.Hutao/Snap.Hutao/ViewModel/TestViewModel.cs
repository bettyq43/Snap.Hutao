﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Windows.AppLifecycle;
using Snap.Hutao.View.Dialog;

namespace Snap.Hutao.ViewModel;

/// <summary>
/// 测试视图模型
/// </summary>
[HighQuality]
[ConstructorGenerated]
[Injection(InjectAs.Scoped)]
internal sealed partial class TestViewModel : Abstraction.ViewModel
{
    private readonly IServiceProvider serviceProvider;
    private readonly ITaskContext taskContext;

    /// <inheritdoc/>
    protected override Task OpenUIAsync()
    {
        return Task.CompletedTask;
    }

    [Command("RestartAppCommand")]
    private void RestartApp(bool elevated)
    {
        AppInstance.Restart(string.Empty);
    }
}