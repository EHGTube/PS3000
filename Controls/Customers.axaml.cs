using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using MySql;
using MySql.Data.MySqlClient;
using PS3000.Controls;
using PS3000.ViewModels;

namespace PS3000;

public partial class Customers : UserControl
{

    public Customers()
    {
        InitializeComponent();
    }
}