using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MySql.Data.MySqlClient;


namespace PS3000.Controls;

public partial class CustomersViewModel : ObservableObject
{
    public string? Name { get; init; }
    
    private string _selectedCustomer;
    public string SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            if (SetProperty(ref _selectedCustomer, value)) // This returns true if the value actually changed
            {
                // This code runs whenever SelectedCustomer changes
                OnSelectedCustomerChanged();
            }
        }
    }

    [ObservableProperty]
    string customersDeliveryAdressCompany;
    [ObservableProperty]
    string customersDeliveryAdressStreet;
    [ObservableProperty]
    string customersDeliveryAdressNumber;
    [ObservableProperty]
    string customersDeliveryAdressHouseNo;
    [ObservableProperty]
    string customersDeliveryAdressPostCode;
    [ObservableProperty]
    string customersDeliveryAdressCity;
    [ObservableProperty]
    string customersDeliveryAdressCountry;
    [ObservableProperty]
    string customersDeliveryAdressContactName;
    [ObservableProperty]
    string customersDeliveryAdressContactPhone;
    [ObservableProperty]
    string customersDeliveryAdressContactMail;
    
    [ObservableProperty]
    string customerNameSearch;
    [ObservableProperty]
    string customersPurchaserName;
    [ObservableProperty]
    string customersPurchaserPhone;
    [ObservableProperty]
    string customersPurchaserMail;
    [ObservableProperty]
    string customersBookkeeperName;
    [ObservableProperty]
    string customersBookkeeperPhone;
    [ObservableProperty]
    string customersBookkeeperMail;
    [ObservableProperty]
    string customersCertificateMail;
    [ObservableProperty]
    string customersInvoiceMail;
    [ObservableProperty]
    string customersNumber;
    [ObservableProperty]
    string customersName;
    [ObservableProperty]
    string customershortName;
    [ObservableProperty]
    string customerstreet;
    [ObservableProperty]
    string customersHouseNo;
    [ObservableProperty]
    string customersCity;
    [ObservableProperty]
    string customersPostCode;
    [ObservableProperty]
    string customersCountry;
    [ObservableProperty]
    string customerSkonto;
    [ObservableProperty]
    string customerSkontoTerm;
    [ObservableProperty]
    string customersNettoTerm;
    [ObservableProperty]
    string customersNotes;
    
    
    private async void OnSelectedCustomerChanged()
    {
        CustomersDeliveryAdressCompany = SelectedCustomer;
        CustomersDeliveryAdressStreet = await GetFieldValueAsync("Straße", SelectedCustomer, "lieferanschrift", "Firmenname");
        CustomersDeliveryAdressHouseNo = await GetFieldValueAsync("Hausnummer", SelectedCustomer, "lieferanschrift", "Firmenname");
        CustomersDeliveryAdressPostCode = await GetFieldValueAsync("PLZ", SelectedCustomer, "lieferanschrift", "Firmenname");
        CustomersDeliveryAdressCity = await GetFieldValueAsync("Stadt", SelectedCustomer, "lieferanschrift", "Firmenname");
        CustomersDeliveryAdressCountry = await GetFieldValueAsync("Land", SelectedCustomer, "lieferanschrift", "Firmenname");
        CustomersDeliveryAdressContactName = await GetFieldValueAsync("Ansprechpartner", SelectedCustomer, "lieferanschrift", "Firmenname");
        CustomersDeliveryAdressContactPhone = await GetFieldValueAsync("Ansprechpartner_Telefon", SelectedCustomer, "lieferanschrift", "Firmenname");
        CustomersDeliveryAdressContactMail = await GetFieldValueAsync("Ansprechpartner_Mail", SelectedCustomer, "lieferanschrift", "Firmenname");
        CustomersDeliveryAdressNumber = await GetFieldValueAsync("Lieferanschrift_Nummer", SelectedCustomer, "lieferanschrift", "Firmenname");
    }
    
    [ObservableProperty] 
    private string deliveryAdressSearch;
    
    public ObservableCollection<string> DeliveryAdressList { get; private set; } = new ObservableCollection<string>();
    
    [RelayCommand]
    private async Task SearchDeliveryAdress(string value)
    {
        
        if (value != null)
        {
            DeliveryAdressList.Clear();
        
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                {
                    connection.Open();
        
                    string query = $"SELECT * FROM `prostahl`.`lieferanschrift` WHERE CONCAT_WS(' ', `Firmenname`, `Straße`, `Hausnummer`, `PLZ`, `Stadt`, `Land`, `Ansprechpartner`, `Ansprechpartner_Telefon`, `Ansprechpartner_Mail`) LIKE '%{value}%' ORDER BY `Firmenname` ASC LIMIT 1000;\r\n";
                    MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection);
        
                    using (MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Read values from columns
                            string readout = reader.GetString(1); // Assuming the searched value is at index 1
        
                            // Do something with the values...
                            DeliveryAdressList.Add(readout);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager
                  .GetMessageBoxStandard("Error", ex.Message,
                      ButtonEnum.YesNo);
        
                var result = await box.ShowAsync();
            }
        }
    }
    
    
    partial void OnDeliveryAdressSearchChanged(string value)
    {
        SearchDeliveryAdress(value);
    }

    [RelayCommand]
    private void DeleteDeliveryAdressSearch()
    {
        CustomersDeliveryAdressCompany = "";
        CustomersDeliveryAdressStreet = "";
        CustomersDeliveryAdressHouseNo = "";
        CustomersDeliveryAdressPostCode = "";
        CustomersDeliveryAdressCity = "";
        CustomersDeliveryAdressCountry = "";
        CustomersDeliveryAdressContactName = "";
        CustomersDeliveryAdressContactPhone = "";
        CustomersDeliveryAdressContactMail = "";
        CustomersDeliveryAdressNumber = "";
        DeliveryAdressSearch = "";
        DeliveryAdressList.Clear();
    }

    [RelayCommand]
    private async void AddCustomer()
    {
        var box = MessageBoxManager
        .GetMessageBoxStandard("Best�tigung",
            "Lieferanschri",
        ButtonEnum.YesNo);
        
        var result = await box.ShowAsync();
    }

    string ConnectionString = PS3000.Properties.Resources.ConnectionString;
    
    
    private async Task<string> GetFieldValueAsync(string returnColumn, string keyphrase, string table, string searchColumn)
    {
        string query = $"SELECT `{returnColumn}` FROM `prostahl`.`{table}` WHERE `{searchColumn}` LIKE @keyphrase ORDER BY `{searchColumn}` ASC LIMIT 1";

        try
        {
            using var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@keyphrase", keyphrase);
            string formattedQuery = query.Replace("@keyphrase", $"'{keyphrase}'");
            Console.WriteLine($"Query: {formattedQuery}");
                
            object result = await command.ExecuteScalarAsync();
            return result?.ToString() ?? string.Empty;
        }
        catch (Exception ex)
        {
            // Log the error (use a logging framework or similar, depending on your environment)
            Console.WriteLine($"Error: {ex.Message}");
            return string.Empty;
        }
    }
    //
    //
    // //Following is all for Delivery Adress Information on Customer Tab
    //
    // private async void btnCustomersSaveDeliveryAdress_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    // {
    //     if (ListCustomersDeliveryAdressList.SelectedItem != null)
    //     {
    //         var box = MessageBoxManager
    //     .GetMessageBoxStandard("Best�tigung",
    //         $"Lieferanschrift Nr.: {textCustomersDeliveryAdressNumber.Text}{Environment.NewLine}" + 
    //         $"Firmenname: {textCustomersDeliveryAdressCompany.Text}{Environment.NewLine}" +
    //         $"Stra�e: {textCustomersDeliveryAdressStreet.Text}{Environment.NewLine}" +
    //         $"Hausnummer: {textCustomersDeliveryAdressHouseNo.Text}{Environment.NewLine}" +
    //         $"PLZ: {textCustomersDeliveryAdressPostCode.Text}{Environment.NewLine}" +
    //         $"Stadt: {textCustomersDeliveryAdressCity.Text}{Environment.NewLine}" +
    //         $"Land: {textCustomersDeliveryAdressCountry.Text}{Environment.NewLine}" +
    //         $"Ansprechpartner: {textCustomersDeliveryAdressContactName.Text}{Environment.NewLine}" +
    //         $"Ansprechpartner Telefon: {textCustomersDeliveryAdressContactPhone.Text}{Environment.NewLine}" +
    //         $"Ansprechpartner Mail: {textCustomersDeliveryAdressContactMail.Text}",
    //     ButtonEnum.YesNo);
    //
    //     var result = await box.ShowAsync();
    //
    //         if (result == ButtonResult.Yes)
    //         {
    //
    //             string tablerow = "Firmenname";
    //             string txtfield = textCustomersDeliveryAdressCompany.Text;
    //             string updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{textCustomersDeliveryAdressNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             tablerow = "Stra�e";
    //             txtfield = textCustomersDeliveryAdressStreet.Text;
    //             updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{textCustomersDeliveryAdressNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();                }
    //
    //             tablerow = "Hausnummer";
    //             txtfield = textCustomersDeliveryAdressHouseNo.Text;
    //             updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{textCustomersDeliveryAdressNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             tablerow = "PLZ";
    //             txtfield = textCustomersDeliveryAdressPostCode.Text;
    //             updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{textCustomersDeliveryAdressNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             tablerow = "Stadt";
    //             txtfield = textCustomersDeliveryAdressCity.Text;
    //             updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{textCustomersDeliveryAdressNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             tablerow = "Land";
    //             txtfield = textCustomersDeliveryAdressCountry.Text;
    //             updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{textCustomersDeliveryAdressNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             tablerow = "Ansprechpartner";
    //             txtfield = textCustomersDeliveryAdressContactName.Text;
    //             updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{textCustomersDeliveryAdressNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             tablerow = "Ansprechpartner_Telefon";
    //             txtfield = textCustomersDeliveryAdressContactPhone.Text;
    //             updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{textCustomersDeliveryAdressNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             tablerow = "Ansprechpartner_Mail";
    //             txtfield = textCustomersDeliveryAdressContactMail.Text;
    //             updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{textCustomersDeliveryAdressNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             CustomerDeliveryAdressInformationClear();
    //             CustomerDeliveryAdressSearchList();
    //         }
    //     }
    //     else
    //     {
    //         var box = MessageBoxManager
    //             .GetMessageBoxStandard("Best�tigung",
    //             $"Lieferanschrift Nr.: {textCustomersDeliveryAdressNumber.Text}{Environment.NewLine}" +
    //             $"Firmenname: {textCustomersDeliveryAdressCompany.Text}{Environment.NewLine}" +
    //             $"Stra�e: {textCustomersDeliveryAdressStreet.Text}{Environment.NewLine}" +
    //             $"Hausnummer: {textCustomersDeliveryAdressHouseNo.Text}{Environment.NewLine}" +
    //             $"PLZ: {textCustomersDeliveryAdressPostCode.Text}{Environment.NewLine}" +
    //             $"Stadt: {textCustomersDeliveryAdressCity.Text}{Environment.NewLine}" +
    //             $"Land: {textCustomersDeliveryAdressCountry.Text}{Environment.NewLine}" +
    //             $"Ansprechpartner: {textCustomersDeliveryAdressContactName.Text}{Environment.NewLine}" +
    //             $"Ansprechpartner Telefon: {textCustomersDeliveryAdressContactPhone.Text}{Environment.NewLine}" +
    //             $"Ansprechpartner Mail: {textCustomersDeliveryAdressContactMail.Text}",
    //             ButtonEnum.YesNo);
    //
    //         var result = await box.ShowAsync();
    //
    //         if (result == ButtonResult.Yes)
    //         {
    //             string query = @"INSERT INTO lieferanschrift 
    //             (`Firmenname`, `Stra�e`, `Hausnummer`, `PLZ`, `Stadt`, `Land`, `Ansprechpartner`, `Ansprechpartner_Telefon`, `Ansprechpartner_Mail`) 
    //             VALUES 
    //             (@Firmenname, @Stra�e, @Hausnummer, @PLZ, @Stadt, @Land, @Ansprechpartner, @Ansprechpartner_Telefon, @Ansprechpartner_Mail)";
    //
    //             // Create MySqlConnection object
    //             using (MySqlConnection connection = new MySqlConnection(connectionString))
    //             {
    //                 // Open the connection
    //                 connection.Open();
    //
    //                 // Create MySqlCommand object
    //                 using (MySqlCommand command = new MySqlCommand(query, connection))
    //                 {
    //                     // Add parameters
    //                     command.Parameters.AddWithValue("@Firmenname", textCustomersDeliveryAdressCompany.Text);
    //                     command.Parameters.AddWithValue("@Stra�e", textCustomersDeliveryAdressStreet.Text);
    //                     command.Parameters.AddWithValue("@Hausnummer", textCustomersDeliveryAdressHouseNo.Text);
    //                     command.Parameters.AddWithValue("@PLZ", textCustomersDeliveryAdressPostCode.Text);
    //                     command.Parameters.AddWithValue("@Stadt", textCustomersDeliveryAdressCity.Text);
    //                     command.Parameters.AddWithValue("@Land", textCustomersDeliveryAdressCountry.Text);
    //                     command.Parameters.AddWithValue("@Ansprechpartner", textCustomersDeliveryAdressContactName.Text);
    //                     command.Parameters.AddWithValue("@Ansprechpartner_Telefon", textCustomersDeliveryAdressContactPhone.Text);
    //                     command.Parameters.AddWithValue("@Ansprechpartner_Mail", textCustomersDeliveryAdressContactMail.Text);
    //
    //                     // Execute the query
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //         }
    //
    //      }
    // }
    
    
    //
    // private void DeliveryAdressSearchBox_TextChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    // {
    //     if (ListCustomersDeliveryAdressList.SelectedItem != null)
    //     {
    //         string ID = ListCustomersDeliveryAdressList.SelectedItem.ToString();
    //         textCustomersDeliveryAdressCompany.Text = GetFieldValue("Firmenname", ID, "lieferanschrift", "Firmenname");
    //         textCustomersDeliveryAdressStreet.Text = GetFieldValue("Stra�e", ID, "lieferanschrift", "Firmenname");
    //         textCustomersDeliveryAdressHouseNo.Text = GetFieldValue("Hausnummer", ID, "lieferanschrift", "Firmenname");
    //         textCustomersDeliveryAdressPostCode.Text = GetFieldValue("PLZ", ID, "lieferanschrift", "Firmenname");
    //         textCustomersDeliveryAdressCity.Text = GetFieldValue("Stadt", ID, "lieferanschrift", "Firmenname");
    //         textCustomersDeliveryAdressCountry.Text = GetFieldValue("Land", ID, "lieferanschrift", "Firmenname");
    //         textCustomersDeliveryAdressContactName.Text = GetFieldValue("Ansprechpartner", ID, "lieferanschrift", "Firmenname");
    //         textCustomersDeliveryAdressContactPhone.Text = GetFieldValue("Ansprechpartner_Telefon", ID, "lieferanschrift", "Firmenname");
    //         textCustomersDeliveryAdressContactMail.Text = GetFieldValue("Ansprechpartner_Mail", ID, "lieferanschrift", "Firmenname");
    //         textCustomersDeliveryAdressNumber.Text = GetFieldValue("Lieferanschrift_Nummer", ID, "lieferanschrift", "Firmenname");
    //     }
    // }
    //

    //
    // private void CustomerDeliveryAdressInformationClear()
    // {
    //     textCustomersDeliveryAdressCompany.Text = "";
    //     textCustomersDeliveryAdressStreet.Text = "";
    //     textCustomersDeliveryAdressHouseNo.Text = "";
    //     textCustomersDeliveryAdressPostCode.Text = "";
    //     textCustomersDeliveryAdressCity.Text = "";
    //     textCustomersDeliveryAdressCountry.Text = "";
    //     textCustomersDeliveryAdressContactName.Text = "";
    //     textCustomersDeliveryAdressContactPhone.Text = "";
    //     textCustomersDeliveryAdressContactMail.Text = "";
    //     textCustomersDeliveryAdressNumber.Text = "";
    // }
    //
    
    //
    // private void btnDeliveryInfoClear_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    // {
    //     CustomerDeliveryAdressInformationClear();
    //     textCustomersDeliveryAdressSearch.Text = "";
    //     ListCustomersDeliveryAdressList.Items.Clear();
    // }
    //
    //
    //
    // //Following is all for Customer Information on Customer Tab
    //
    // private async void btnNewCustomer_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    // {
    //     if (ListCustomersCustomerList.SelectedItem != null)
    //     {
    //         var box = MessageBoxManager
    //             .GetMessageBoxStandard("Best�tigung",
    //                 $"Kundennummer: {textCustomerNumber.Text}{Environment.NewLine}" +
    //                 $"Firmenname: {textCustomerName.Text}{Environment.NewLine}" +
    //                 $"K�rzel: {textCustomerShortName.Text}{Environment.NewLine}" +
    //                 $"Stra�e: {textCustomerStreet.Text}{Environment.NewLine}" +
    //                 $"Hausnummer: {textCustomerHouseNo.Text}{Environment.NewLine}" +
    //                 $"PLZ: {textCustomerPostCode.Text}{Environment.NewLine}" +
    //                 $"Stadt: {textCustomerCity.Text}{Environment.NewLine}" +
    //                 $"Land: {textCustomerCountry.Text}{Environment.NewLine}" +
    //                 $"Eink�ufer: {textCustomersPurchaserName.Text}{Environment.NewLine}" +
    //                 $"Eink�ufer Telefon: {textCustomersPurchaserPhone.Text}{Environment.NewLine}" +
    //                 $"Eink�ufer E-Mail: {textCustomersPurchaserMail.Text}{Environment.NewLine}" +
    //                 $"Buchhaltung Name: {textCustomersBookkeeperName.Text}{Environment.NewLine}" +
    //                 $"Buchhalung Telefon: {textCustomersBookkeeperPhone.Text}{Environment.NewLine}" +
    //                 $"Buchhalung E-Mail: {textCustomersBookkeeperMail.Text}{Environment.NewLine}" +
    //                 $"Werkszeugnis E-Mail: {textCustomersCertificateMail.Text}{Environment.NewLine}" +
    //                 $"Rechung E-Mail: {textCustomersInvoiceMail.Text}{Environment.NewLine}" +
    //                 $"Skonto: {textCustomerSkonto.Text}{Environment.NewLine}" +
    //                 $"Skontofrist: {textCustomerSkontoTerm.Text}{Environment.NewLine}" +
    //                 $"Nettofrist: {textCustomerNettoTerm.Text}",
    //             ButtonEnum.YesNo);
    //
    //         var result = await box.ShowAsync();
    //
    //         if (result == ButtonResult.Yes)
    //         {
    //             string table = "kunden";
    //
    //             string row = "K�rzel";
    //             string textbox = textCustomerShortName.Text;
    //             string updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Firmenname";
    //             textbox = textCustomerName.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Rechnung_Stra�e";
    //             textbox = textCustomerStreet.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Rechnung_Hausnummer";
    //             textbox = textCustomerHouseNo.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Rechnung_Stadt";
    //             textbox = textCustomerCity.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Rechnung_PLZ";
    //             textbox = textCustomerPostCode.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Rechnung_Land";
    //             textbox = textCustomerCountry.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Eink�ufer_Name";
    //             textbox = textCustomersPurchaserName.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Eink�ufer_Telefonnummer";
    //             textbox = textCustomersPurchaserPhone.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Eink�ufer_Email";
    //             textbox = textCustomersPurchaserMail.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Buchhaltung_Name";
    //             textbox = textCustomersBookkeeperName.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Buchhaltung_Telefonnummer";
    //             textbox = textCustomersBookkeeperPhone.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Buchhaltung_Email";
    //             textbox = textCustomersBookkeeperMail.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Werkszeugnis_Email";
    //             textbox = textCustomersCertificateMail.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Rechnung_Email";
    //             textbox = textCustomersInvoiceMail.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Skonto";
    //             textbox = textCustomerSkonto.Text.Replace(',', '.');
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Skontofrist";
    //             textbox = textCustomerSkontoTerm.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             row = "Nettofrist";
    //             textbox = textCustomerNettoTerm.Text;
    //             updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Kundennummer = '{textCustomerNumber.Text}'";
    //
    //             try
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //                 {
    //                     connection.Open();
    //                     MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             catch (Exception ex)
    //             {
    //                 var error = MessageBoxManager
    //                     .GetMessageBoxStandard("Fehler",
    //                         $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                         ButtonEnum.Ok,
    //                         Icon.Error);
    //
    //                 await error.ShowAsync();
    //             }
    //
    //             CustomerCustomerInformationClear();
    //             CustomerCustomerCreationSearchList();
    //         }
    //     }
    //     else
    //     {
    //         var box = MessageBoxManager
    //             .GetMessageBoxStandard("Best�tigung",
    //                 $"Kundennummer: {textCustomerNumber.Text}{Environment.NewLine}" +
    //                 $"Firmenname: {textCustomerName.Text}{Environment.NewLine}" +
    //                 $"K�rzel: {textCustomerShortName.Text}{Environment.NewLine}" +
    //                 $"Stra�e: {textCustomerStreet.Text}{Environment.NewLine}" +
    //                 $"Hausnummer: {textCustomerHouseNo.Text}{Environment.NewLine}" +
    //                 $"PLZ: {textCustomerPostCode.Text}{Environment.NewLine}" +
    //                 $"Stadt: {textCustomerCity.Text}{Environment.NewLine}" +
    //                 $"Land: {textCustomerCountry.Text}{Environment.NewLine}" +
    //                 $"Eink�ufer: {textCustomersPurchaserName.Text}{Environment.NewLine}" +
    //                 $"Eink�ufer Telefon: {textCustomersPurchaserPhone.Text}{Environment.NewLine}" +
    //                 $"Eink�ufer E-Mail: {textCustomersPurchaserMail.Text}{Environment.NewLine}" +
    //                 $"Buchhaltung Name: {textCustomersBookkeeperName.Text}{Environment.NewLine}" +
    //                 $"Buchhalung Telefon: {textCustomersBookkeeperPhone.Text}{Environment.NewLine}" +
    //                 $"Buchhalung E-Mail: {textCustomersBookkeeperMail.Text}{Environment.NewLine}" +
    //                 $"Werkszeugnis E-Mail: {textCustomersCertificateMail.Text}{Environment.NewLine}" +
    //                 $"Rechung E-Mail: {textCustomersInvoiceMail.Text}{Environment.NewLine}" +
    //                 $"Skonto: {textCustomerSkonto.Text}{Environment.NewLine}" +
    //                 $"Skontofrist: {textCustomerSkontoTerm.Text}{Environment.NewLine}" +
    //                 $"Nettofrist: {textCustomerNettoTerm.Text}",
    //             ButtonEnum.YesNo);
    //
    //         var result = await box.ShowAsync();
    //
    //         if (result == ButtonResult.Yes)
    //         {
    //
    //             // SQL query
    //             string query = @"INSERT INTO kunden 
    //                 (K�rzel, Firmenname, `Rechnung_Stra�e`, `Rechnung_Hausnummer`, `Rechnung_Stadt`, `Rechnung_PLZ`, `Rechnung_Land`, 
    //                 `Eink�ufer_Name`, `Eink�ufer_Telefonnummer`, `Eink�ufer_EMail`, `Buchhaltung_Name`, `Buchhaltung_Telefonnummer`, 
    //                 `Buchhaltung_EMail`, `Werkszeugnis_EMail`, `Rechnung_EMail`, `Skonto`, `Skontofrist`, `Nettofrist`) 
    //                 VALUES 
    //                 (@K�rzel, @Firmenname, @Rechnung_Stra�e, @Rechnung_Hausnummer, @Rechnung_Stadt, @Rechnung_PLZ, @Rechnung_Land, 
    //                 @Eink�ufer_Name, @Eink�ufer_Telefon, @Eink�ufer_Email, @Buchhaltung_Name, @Buchhaltung_Telefon, 
    //                 @Buchhaltung_Email, @Werkszeugnis_Email, @Rechnung_Email, @Skonto, @Skontofrist, @Nettofrist)";
    //
    //             // Create MySql.Data.MySqlClient.MySqlConnection object
    //             using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //             {
    //                 // Open the connection
    //                 connection.Open();
    //
    //                 // Create MySql.Data.MySqlClient.MySqlCommand object
    //                 using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
    //                 {
    //                     // Add parameters
    //                     command.Parameters.AddWithValue("@Firmenname", textCustomerName.Text);
    //                     command.Parameters.AddWithValue("@K�rzel", textCustomerShortName.Text);
    //                     command.Parameters.AddWithValue("@Rechnung_Stra�e", textCustomerStreet.Text);
    //                     command.Parameters.AddWithValue("@Rechnung_Hausnummer", textCustomerHouseNo.Text);
    //                     command.Parameters.AddWithValue("@Rechnung_PLZ", textCustomerPostCode.Text);
    //                     command.Parameters.AddWithValue("@Rechnung_Stadt", textCustomerCity.Text);
    //                     command.Parameters.AddWithValue("@Rechnung_Land", textCustomerCountry.Text);
    //                     command.Parameters.AddWithValue("@Eink�ufer_Name", textCustomersPurchaserName.Text);
    //                     command.Parameters.AddWithValue("@Eink�ufer_Telefon", textCustomersPurchaserPhone.Text);
    //                     command.Parameters.AddWithValue("@Eink�ufer_Email", textCustomersPurchaserMail.Text);
    //                     command.Parameters.AddWithValue("@Buchhaltung_Name", textCustomersBookkeeperName.Text);
    //                     command.Parameters.AddWithValue("@Buchhaltung_Telefon", textCustomersBookkeeperPhone.Text);
    //                     command.Parameters.AddWithValue("@Buchhaltung_Email", textCustomersBookkeeperMail.Text);
    //                     command.Parameters.AddWithValue("@Werkszeugnis_Email", textCustomersCertificateMail.Text);
    //                     command.Parameters.AddWithValue("@Rechnung_Email", textCustomersInvoiceMail.Text);
    //                     command.Parameters.AddWithValue("@Skonto", textCustomerSkonto.Text);
    //                     command.Parameters.AddWithValue("@Skontofrist", textCustomerSkontoTerm.Text);
    //                     command.Parameters.AddWithValue("@Nettofrist", textCustomerNettoTerm.Text);
    //
    //
    //                     // Execute the query
    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //             CustomerCustomerInformationClear();
    //             CustomerCustomerCreationSearchList();
    //         }
    //     }
    // }
    //
    // private void textCustomerNameSearch_TextChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    // {
    //     CustomerCustomerCreationSearchList();
    // }
    //
    // private void ListCustomersCustomerList_SelectedIndexChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    // {
    //     if (ListCustomersCustomerList.SelectedItem != null)
    //     {
    //         string firmenname = ListCustomersCustomerList.SelectedItem.ToString();
    //
    //         textCustomerNumber.Text = GetFieldValue("Kundennummer", firmenname, "kunden", "Firmenname");
    //         textCustomerStreet.Text = GetFieldValue("Rechnung_Stra�e", firmenname, "kunden", "Firmenname");
    //         textCustomerHouseNo.Text = GetFieldValue("Rechnung_Hausnummer", firmenname, "kunden", "Firmenname");
    //         textCustomerShortName.Text = GetFieldValue("K�rzel", firmenname, "kunden", "Firmenname");
    //         textCustomerCity.Text = GetFieldValue("Rechnung_Stadt", firmenname, "kunden", "Firmenname");
    //         textCustomerPostCode.Text = GetFieldValue("Rechnung_PLZ", firmenname, "kunden", "Firmenname");
    //         textCustomerCountry.Text = GetFieldValue("Rechnung_Land", firmenname, "kunden", "Firmenname");
    //         textCustomersPurchaserName.Text = GetFieldValue("Eink�ufer_Name", firmenname, "kunden", "Firmenname");
    //         textCustomersPurchaserPhone.Text = GetFieldValue("Eink�ufer_Telefonnummer", firmenname, "kunden", "Firmenname");
    //         textCustomersPurchaserMail.Text = GetFieldValue("Eink�ufer_EMail", firmenname, "kunden", "Firmenname");
    //         textCustomersBookkeeperName.Text = GetFieldValue("Buchhaltung_Name", firmenname, "kunden", "Firmenname");
    //         textCustomersBookkeeperPhone.Text = GetFieldValue("Buchhaltung_Telefonnummer", firmenname, "kunden", "Firmenname");
    //         textCustomersBookkeeperMail.Text = GetFieldValue("Buchhaltung_EMail", firmenname, "kunden", "Firmenname");
    //         textCustomersCertificateMail.Text = GetFieldValue("Werkszeugnis_EMail", firmenname, "kunden", "Firmenname");
    //         textCustomersInvoiceMail.Text = GetFieldValue("Rechnung_EMail", firmenname, "kunden", "Firmenname");
    //         textCustomerName.Text = GetFieldValue("Firmenname", firmenname, "kunden", "Firmenname");
    //         textCustomerNotes.Text = GetFieldValue("Notizen", firmenname, "kunden", "Firmenname");
    //         textCustomerSkonto.Text = GetFieldValue("Skonto", firmenname, "kunden", "Firmenname");
    //         textCustomerSkontoTerm.Text = GetFieldValue("Skontofrist", firmenname, "kunden", "Firmenname");
    //         textCustomerNettoTerm.Text = GetFieldValue("Nettofrist", firmenname, "kunden", "Firmenname");
    //     }
    // }
    //
    // private async void textCustomerNotes_LostFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    // {
    //     if (ListCustomersCustomerList.SelectedItem != null)
    //     {
    //         // Customer Selected -> offer Change
    //         string table = "kunden";
    //
    //         string row = "Notizen";
    //         string textbox = textCustomerNotes.Text;
    //         string updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{ListCustomersCustomerList.SelectedItem.ToString()}'";
    //
    //         try
    //         {
    //             using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //             {
    //                 connection.Open();
    //                 MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
    //
    //                 command.ExecuteNonQuery();
    //             }
    //         }
    //         catch (Exception ex)
    //         {
    //             var error = MessageBoxManager
    //                 .GetMessageBoxStandard("Fehler",
    //                     $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                     ButtonEnum.Ok,
    //                     Icon.Error);
    //
    //             await error.ShowAsync();
    //         }
    //     }
    //     CustomerCustomerInformationClear();
    //     CustomerCustomerCreationSearchList();
    // }
    //
    // private void CustomerCustomerInformationClear()
    // {
    //     textCustomerNumber.Text = "";
    //     textCustomerStreet.Text = "";
    //     textCustomerHouseNo.Text = "";
    //     textCustomerShortName.Text = "";
    //     textCustomerCity.Text = "";
    //     textCustomerPostCode.Text = "";
    //     textCustomerCountry.Text = "";
    //     textCustomersPurchaserName.Text = "";
    //     textCustomersPurchaserPhone.Text = "";
    //     textCustomersPurchaserMail.Text = "";
    //     textCustomersBookkeeperName.Text = "";
    //     textCustomersBookkeeperPhone.Text = "";
    //     textCustomersBookkeeperMail.Text = "";
    //     textCustomersCertificateMail.Text = "";
    //     textCustomersInvoiceMail.Text = "";
    //     textCustomerName.Text = "";
    //     textCustomerNotes.Text = "";
    //     textCustomerSkonto.Text = "";
    //     textCustomerSkontoTerm.Text = "";
    //     textCustomerNettoTerm.Text = "";
    // }
    //
    // private async void CustomerCustomerCreationSearchList()
    // {
    //     if (textCustomerNameSearch.Text != null)
    //     {
    //         ListCustomersCustomerList.Items.Clear();
    //
    //         try
    //         {
    //             string query = $"SELECT * FROM prostahl.kunden WHERE CONCAT_WS(' ', K�rzel, Firmenname, Rechnung_Stra�e, Rechnung_Hausnummer, Rechnung_Stadt, Rechnung_PLZ, Rechnung_Land, Eink�ufer_Name, Eink�ufer_Telefonnummer, Eink�ufer_EMail, Buchhaltung_Name, Buchhaltung_Telefonnummer, Buchhaltung_EMail, Werkszeugnis_EMail, Rechnung_EMail, Skonto, Skontofrist, Nettofrist) LIKE @SearchTerm ORDER BY Firmenname ASC LIMIT 1000";
    //
    //             using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
    //             {
    //                 using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
    //                 {
    //                     command.Parameters.AddWithValue("@SearchTerm", $"%{textCustomerNameSearch.Text}%");
    //
    //                     try
    //                     {
    //                         connection.Open();
    //                         using (MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader())
    //                         {
    //                             while (reader.Read())
    //                             {
    //                                 // Read values from columns
    //                                 string firmenname = reader.GetString(2); // Assuming `Firmenname` is at index 2
    //
    //                                 // Do something with the values...
    //                                 ListCustomersCustomerList.Items.Add(firmenname);
    //                             }
    //                         }
    //                     }
    //                     catch (Exception ex)
    //                     {
    //                         var error = MessageBoxManager
    //                         .GetMessageBoxStandard("Fehler",
    //                             $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                             ButtonEnum.Ok,
    //                             Icon.Error);
    //
    //                         await error.ShowAsync();
    //                     }
    //                 }
    //             }
    //         }
    //         catch (Exception ex)
    //         {
    //             var error = MessageBoxManager
    //             .GetMessageBoxStandard("Fehler",
    //                 $"Ein Fehler ist aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}",
    //                 ButtonEnum.Ok,
    //                 Icon.Error);
    //
    //             await error.ShowAsync();
    //         }
    //     }
    // }
    //
    // private void btnCustomerInfoClear_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    // {
    //     CustomerCustomerInformationClear();
    //     textCustomerNameSearch.Text = "";
    //     ListCustomersCustomerList.Items.Clear();
    // }
}

