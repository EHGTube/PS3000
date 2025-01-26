using PS3000.Controls;
using System.Collections.ObjectModel;
using static PS3000.Views.MainView;

namespace PS3000.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ObservableCollection<Surcharges> SurchargeData { get; set; }
    public ObservableCollection<Positions> InquiryPositions { get; set; }

    public StorageCoilCode StorageCoil { get; } = new();

    public MainViewModel()
    {
        // Example data

        SurchargeData = new ObservableCollection<Surcharges>
        {
            new Surcharges { Zuschlagsart = "Pro Auftrag", Beschreibung = "Werkszeugnis", Menge = 12.80 },
            new Surcharges { Zuschlagsart = "Pro Lieferung", Beschreibung = "Versandkosten", Menge = 79 },
            new Surcharges { Zuschlagsart = "Pro Charge", Beschreibung = "IK-Test", Menge = 79 },
            new Surcharges { Zuschlagsart = "Pro Stück", Beschreibung = "Schnittkosten", Menge = 79 },
            new Surcharges { Zuschlagsart = "Pro Meter", Beschreibung = "Schliffkosten", Menge = 79 },
            new Surcharges { Zuschlagsart = "Pro Bund", Beschreibung = "Verpackungskosten", Menge = 79 },
            new Surcharges { Zuschlagsart = "Pro Coil", Beschreibung = "Markierungskosten", Menge = 79 },
        };

        InquiryPositions = new ObservableCollection<Positions>
        {
            new Positions { PosNumber = 1 },
            new Positions { PosNumber = 2 },
            new Positions { PosNumber = 3 },
            new Positions { PosNumber = 1 },
            new Positions { PosNumber = 2 },
            new Positions { PosNumber = 3 },
            new Positions { PosNumber = 1 },
            new Positions { PosNumber = 2 },
            new Positions { PosNumber = 3 },
            new Positions { PosNumber = 1 },
            new Positions { PosNumber = 2 },
            new Positions { PosNumber = 3 },
        };
    }

    public class Surcharges
    {
        public string Zuschlagsart { get; set; }
        public string Beschreibung { get; set; }
        public double Menge { get; set; }
    }

    public class Positions
    {
        public int PosNumber { get; set; }
    }


}

