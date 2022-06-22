﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.UI.Xaml.Controls;
using Snap.Hutao.Core.Threading;

namespace Snap.Hutao.View.Dialog;

/// <summary>
/// 添加用户对话框
/// </summary>
public sealed partial class UserDialog : ContentDialog
{
    /// <summary>
    /// 构造一个新的添加用户对话框
    /// </summary>
    public UserDialog()
    {
        InitializeComponent();
        XamlRoot = App.Window!.Content.XamlRoot;
    }

    /// <summary>
    /// 获取输入的Cookie
    /// </summary>
    /// <returns>输入的结果</returns>
    public async Task<Result<bool, string>> GetInputCookieAsync()
    {
        ContentDialogResult result = await ShowAsync();
        string cookie = InputText.Text;

        return new(result != ContentDialogResult.Secondary, cookie);
    }

    private void InputTextChanged(object sender, TextChangedEventArgs e)
    {
        string text = InputText.Text;

        bool inputEmpty = string.IsNullOrEmpty(text);

        (PrimaryButtonText, IsPrimaryButtonEnabled) = inputEmpty switch
        {
            true => ("请输入Cookie", false),
            false => ("确认", true),
        };
    }
}