using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Avalonia.Interactivity;
using static PS3000.ViewModels.MainViewModel;
using DynamicData;
using static PS3000.StorageCoil;
using Avalonia.Styling;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;

namespace PS3000;

public partial class StorageCoil : UserControl
{

    public StorageCoil()
    {
        InitializeComponent();
    }
}