using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;

namespace PS3000;

public partial class Orders : UserControl
{
    public Orders()
    {
        InitializeComponent();

        //OrderNotes.Items.Add("Notizen Spaltplan");
        //OrderNotes.Items.Add("Notizen Spalten");
        //OrderNotes.Items.Add("Notizen Rohrstraße");
        //OrderNotes.Items.Add("Notizen Glühofen");
        //OrderNotes.Items.Add("Notizen Richten");
        //OrderNotes.Items.Add("Notizen Prüfen");
        //OrderNotes.Items.Add("Notizen Schleifen");
        //OrderNotes.Items.Add("Notizen Sägen");
        //OrderNotes.Items.Add("Notizen Verpacken");
        //OrderNotes.Items.Add("Notizen Werkszeugnis");
        //OrderNotes.Items.Add("Notizen Lieferschein");
        //OrderNotes.Items.Add("Notizen Berechnung");
    }

    private void chkOrdersDeliveryEXW_Checked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Avalonia.Controls.CheckBox checkBox)
        {
            // Access the IsChecked property
            bool? isChecked = checkBox.IsChecked;

            // Check the state of the CheckBox
            if (isChecked == true)
            {
                StackPanelInquiryDeliveryDetails.IsVisible = false;
                TxtInquiryDeliverySearch.IsVisible = false;
            }
            if (isChecked == false)
            {
                StackPanelInquiryDeliveryDetails.IsVisible = true;
                TxtInquiryDeliverySearch.IsVisible = true;
            }
        }
    }
}