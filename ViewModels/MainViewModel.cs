using PS3000.Controls;
using System.Collections.ObjectModel;
using static PS3000.Views.MainView;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PS3000.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        // Example data
        _inquiriesViewModel = new InquiriesViewModel();

    }
    
    [ObservableProperty]
    private InquiriesViewModel _inquiriesViewModel;
    
    
    public ObservableCollection<CustomersViewModel> Customers { get; set; } = new()
    {
    };
    
    public CustomersViewModel CustomersViewModel { get; } = new CustomersViewModel();}