﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Snap.Hutao.Core;
using Snap.Hutao.Service.Navigation;

namespace Snap.Hutao.View.Helper;

/// <summary>
/// 导航项帮助类
/// </summary>
public sealed class NavHelper
{
    private static readonly DependencyProperty NavigateToProperty = Property<NavHelper>.Attach<Type>("NavigateTo");
    private static readonly DependencyProperty ExtraDataProperty = Property<NavHelper>.Attach<INavigationData>("ExtraData");

    /// <summary>
    /// 获取导航项的目标页面类型
    /// </summary>
    /// <param name="item">待获取的导航项</param>
    /// <returns>目标页面类型</returns>
    public static Type? GetNavigateTo(NavigationViewItem? item)
    {
        return item?.GetValue(NavigateToProperty) as Type;
    }

    /// <summary>
    /// 设置导航项的目标页面类型
    /// </summary>
    /// <param name="item">待设置的导航项</param>
    /// <param name="value">新的目标页面类型</param>
    public static void SetNavigateTo(NavigationViewItem item, Type value)
    {
        item.SetValue(NavigateToProperty, value);
    }

    /// <summary>
    /// 获取导航项的目标页面的额外数据
    /// </summary>
    /// <param name="item">待获取的导航项</param>
    /// <returns>目标页面类型的额外数据</returns>
    public static INavigationData? GetExtraData(NavigationViewItem? item)
    {
        return item?.GetValue(ExtraDataProperty) as INavigationData;
    }

    /// <summary>
    /// 设置导航项的目标页面类型
    /// </summary>
    /// <param name="item">待设置的导航项</param>
    /// <param name="value">新的目标页面类型</param>
    public static void SetExtraData(NavigationViewItem item, INavigationData value)
    {
        item.SetValue(ExtraDataProperty, value);
    }
}
