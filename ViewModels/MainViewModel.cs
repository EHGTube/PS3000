using PS3000.Controls;
using System.Collections.ObjectModel;
using static PS3000.Views.MainView;

namespace PS3000.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        // Example data


    }
    
    public ObservableCollection<CustomersViewModel> Customers { get; } 
        = new ObservableCollection<CustomersViewModel>()
        {
            new CustomersViewModel() { Name = "Lukas" },
            new CustomersViewModel() { Name = "Thomas" },
        };
}

