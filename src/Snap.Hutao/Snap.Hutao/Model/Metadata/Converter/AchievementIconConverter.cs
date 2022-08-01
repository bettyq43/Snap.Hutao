﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.UI.Xaml.Data;

namespace Snap.Hutao.Model.Metadata.Converter;

/// <summary>
/// 角色头像转换器
/// </summary>
internal class AchievementIconConverter : IValueConverter
{
    private const string BaseUrl = "https://static.snapgenshin.com/AchievementIcon/{0}.png";

    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return new Uri(string.Format(BaseUrl, value));
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw Must.NeverHappen();
    }
}