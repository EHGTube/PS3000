﻿using Avalonia.Controls;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PS3000.ViewModels;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PS3000.Views;

public partial class MainView : UserControl
{



    public MainView()
    {
        InitializeComponent();
    }

    //public async void ButtonClick(object? sender, EventArgs e)
    //{
    //    var box = MessageBoxManager
    //      .GetMessageBoxStandard("Caption", "Are you sure you would like to delete appender_replace_page_1?",
    //          ButtonEnum.YesNo);

    //    var result = await box.ShowAsync();
    //}
}
