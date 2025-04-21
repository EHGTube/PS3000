using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Dapper;
using Microsoft.Data.SqlClient;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MySql.Data.MySqlClient;



namespace PS3000.Controls;

public partial class OrdersViewModel : ObservableObject
{
    
    
    
    
    //Following is to prepare Content / Lists when Tab is selected, Enter Methods to run in PrepareAsync()
    [ObservableProperty]
    private bool _isActive;

    partial void OnIsActiveChanged(bool value)
    {
        if (value)
        {
            // This will be called when the tab becomes active
            PrepareCommand.Execute(null);
        }
    }

    [RelayCommand]
    private async Task PrepareAsync()
    {
        
    }     
}