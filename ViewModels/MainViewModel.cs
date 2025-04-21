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

    [ObservableProperty] private InquiriesViewModel _inquiriesViewModel;


    public ObservableCollection<CustomersViewModel> Customers { get; set; } = new()
    {
    };

    public CustomersViewModel CustomersViewModel { get; } = new CustomersViewModel();

    public StorageCoilViewModel StorageCoil { get; } = new StorageCoilViewModel();
    
    [ObservableProperty]
    private InquiriesViewModel _inquiries = new InquiriesViewModel();
    
    public OrdersViewModel Orders { get; } = new OrdersViewModel();
    
    private bool _auftrageTabSelected;
    public bool AuftrageTabSelected
    {
        get => _auftrageTabSelected;
        set
        {
            _auftrageTabSelected = value;
            OnPropertyChanged();
        
            // Use the Inquiries property (generated from _inquiries)
            if (value && Inquiries != null)
            {
                Inquiries.IsActive = true;
            }
        }
    }
    
}