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
using Dapper;
using System.IO;
using System.Net.Mail;
using System.Diagnostics;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using Avalonia.Controls;

namespace PS3000.Controls;

public class SurchargeListItem
{
    public string SurchargeItem  { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
}

public class SurchargeListItemIndex
{
    public int SurchargeItemIndex  { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
}

public partial class InquiriesViewModel : ObservableObject
{
    string ConnectionString = PS3000.Properties.Resources.ConnectionString;
    
    //Following is to search the Company Details
    [ObservableProperty] 
    private string customerNameSearch;
    
    public ObservableCollection<string> CustomersList { get; private set; } = new ObservableCollection<string>();
    
    public ObservableCollection<SurchargeListItem> SurchargeList { get; set; } = new ObservableCollection<SurchargeListItem>();

    [RelayCommand]
    private async Task SearchCustomers(string value)
    {
        
        if (CustomersList == null)
        {
            CustomersList = new ObservableCollection<string>();
        }
        else
        {
            CustomersList.Clear();
        }
        
        if (string.IsNullOrEmpty(value))
        {
            CustomerStreet = "";
            CustomersHouseNo = "";
            CustomersCity = "";
            CustomersPostCode = "";
            CustomersCountry = "";
            CustomersPurchaserName = "";
            
            return;
        }

        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                string query = @"
                SELECT Companyname FROM prostahl.Customers 
                WHERE CONCAT_WS(' ', Customernick, Companyname, InvoiceStreet, InvoiceHouseNo, 
                                 InvoiceCity, InvoicePostcode, InvoiceCountry, PurchaserName, 
                                 PurchaserPhone, PurchaserMail, BookkeeperName, BookkeeperPhone, 
                                 BookkeeperMail, CertificateMail, InvoiceMail, OCMail, Skonto, 
                                 SkontoTerm, NettoTerm, InsuranceLimit) LIKE @SearchTerm 
                ORDER BY Companyname ASC LIMIT 1000";

                var companies = await connection.QueryAsync<string>(query, new { SearchTerm = $"%{value}%" });
            
                foreach (var company in companies)
                {
                    CustomersList.Add(company);
                }
            }
        }
        catch (Exception ex)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error", ex.Message, ButtonEnum.YesNo);

            await box.ShowAsync();
        }

        if (CustomersList.Count == 1)
        {
            SelectedCustomer = CustomersList[0];
        }
    }    
    
    partial void OnCustomerNameSearchChanged(string value)
    {
        SearchCustomers(value);
    }

    
    //Following is to load Details once Customer company has been selected
    
    [ObservableProperty] string customerStreet;
    [ObservableProperty] string customersHouseNo;
    [ObservableProperty] string customersCity;
    [ObservableProperty] string customersPostCode;
    [ObservableProperty] string customersCountry;
    [ObservableProperty] string customersPurchaserName;
    [ObservableProperty] private string customerNumber;
    
    private bool _englishCustomer;
    public bool EnglishCustomer
    {
        get => _englishCustomer;
        set
        {
            if (_englishCustomer != value)
            {
                _englishCustomer = value;
                OnPropertyChanged(nameof(EnglishCustomer));
            }
        }
    }

        
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
    
    private async void OnSelectedCustomerChanged()
    {
        // Create a connection
        using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        // Query to get all customer data at once
        var customer = await connection.QueryFirstOrDefaultAsync<dynamic>(
            @"SELECT 
                Customernumber,
                Customernick, 
                PurchaserName, 
                PurchaserPhone, 
                PurchaserMail, 
                BookkeeperName, 
                BookkeeperPhone, 
                BookkeeperMail, 
                CertificateMail, 
                InvoiceMail, 
                OCMail, 
                Customernumber, 
                InvoiceStreet, 
                InvoiceHouseNo, 
                InvoiceCity, 
                InvoicePostcode, 
                InvoiceCountry, 
                Skonto, 
                SkontoTerm, 
                NettoTerm, 
                Notes, 
                InsuranceLimit 
            FROM Customers 
            WHERE Companyname = @CompanyName",
            new { CompanyName = SelectedCustomer }
        );

        if (customer == null)
        {
            return;
        }

        CustomersPurchaserName = customer.PurchaserName;
        CustomerStreet = customer.InvoiceStreet;
        CustomersHouseNo = customer.InvoiceHouseNo;
        CustomersCity = customer.InvoiceCity;
        CustomersPostCode = customer.InvoicePostcode;
        CustomersCountry = customer.InvoiceCountry;
        CustomerNumber = Convert.ToString(customer.Customernumber);
    }

    
    //Following is to search the Delivery Adresses
    
    [ObservableProperty] 
    private string deliveryAdressSearch;
    [ObservableProperty]
    string customersDeliveryAdressStreet;
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
    string customersDeliveryAdressNo;
    [ObservableProperty] 
    private string inquiryNotes;
    
    public ObservableCollection<string> DeliveryAdressList { get; private set; } = new ObservableCollection<string>();

    [RelayCommand]
    private async Task SearchDeliveryAdress(string value)
    {
        
        if (!string.IsNullOrEmpty(value))
        {
            try
            {
                DeliveryAdressList.Clear();
        
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
        
                // Use parameterized query to prevent SQL injection
                string query = @"
                    SELECT CompanyName 
                    FROM `prostahl`.`deliveryadress` 
                    WHERE CONCAT_WS(' ', 
                        `CompanyName`,
                        `Street`,
                        `HouseNo`,
                        `Postcode`,
                        `City`,
                        `Country`,
                        `ContactName`,
                        `ContactPhone`,
                        `ContactMail`
                    ) LIKE @SearchTerm 
                    ORDER BY `CompanyName` ASC
                    LIMIT 1000";
        
                // Add % wildcards for LIKE clause
                string searchTerm = $"%{value}%";
        
                // Execute query and get results directly as strings
                var results = await connection.QueryAsync<string>(query, new { SearchTerm = searchTerm });
        
                // Add results to the list
                foreach (var result in results)
                {
                    DeliveryAdressList.Add(result);
                }
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard(
                    "Error", 
                    ex.Message,
                    ButtonEnum.YesNo
                );
        
                await box.ShowAsync();
            }
        }
        else
        {
            CustomersDeliveryAdressStreet = "";
            CustomersDeliveryAdressHouseNo = "";
            CustomersDeliveryAdressPostCode = "";
            CustomersDeliveryAdressCity = "";
            CustomersDeliveryAdressCountry = "";
            CustomersDeliveryAdressContactName = "";
            DeliveryAdressSearch = "";
            DeliveryAdressList.Clear();        
        }
        
        if (DeliveryAdressList.Count == 1)
        {
            SelectedDeliveryAdress = DeliveryAdressList[0];
        }
    }
    
    partial void OnDeliveryAdressSearchChanged(string value)
    {
        SearchDeliveryAdress(value);
    }
    
        //Following is to load Details once Delivery Adress company has been selected
    private string _selectedDeliveryAdress;
    public string SelectedDeliveryAdress
    {
        get => _selectedDeliveryAdress;
        set
        {
            if (SetProperty(ref _selectedDeliveryAdress, value)) // This returns true if the value actually changed
            {
                // This code runs whenever SelectedCustomer changes
                OnSelectedDeliveryAdressChanged();
            }
        }
    }
    
    private async void OnSelectedDeliveryAdressChanged()
    {
        // Create a connection
        using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        // Query to get all delivery address data at once
        var deliveryAddress = await connection.QueryFirstOrDefaultAsync<dynamic>(
            @"SELECT 
        Street, 
        HouseNo, 
        Postcode, 
        City, 
        Country, 
        ContactName, 
        ContactPhone, 
        ContactMail, 
        deliveryadressNo 
        FROM deliveryadress 
        WHERE CompanyName = @CompanyName",
            new { CompanyName = SelectedDeliveryAdress }
        );
        
        if (deliveryAddress == null)
        {
            return;
        }
        
        CustomersDeliveryAdressStreet = deliveryAddress.Street;
        CustomersDeliveryAdressHouseNo = deliveryAddress.HouseNo;
        CustomersDeliveryAdressPostCode = deliveryAddress.Postcode;
        CustomersDeliveryAdressCity = deliveryAddress.City;
        CustomersDeliveryAdressCountry = deliveryAddress.Country;
        CustomersDeliveryAdressContactName = deliveryAddress.ContactName;
        CustomersDeliveryAdressNo = Convert.ToString(deliveryAddress.deliveryadressNo);

    }

    private bool _deliveryEXW;
    public bool CheckEXW
    {
        get => _deliveryEXW;
        set
        {
            if (_deliveryEXW != value)
            {
                _deliveryEXW = value;
                OnPropertyChanged(nameof(CheckEXW));

                IsDeliveryEnabled = !_deliveryEXW;
            }
        }
    }
    
    private bool _isDeliveryEnabled = true;
    public bool IsDeliveryEnabled
    {
        get => _isDeliveryEnabled;
        set
        {
            if (_isDeliveryEnabled != value)
            {
                _isDeliveryEnabled = value;
                OnPropertyChanged(nameof(IsDeliveryEnabled));
                CustomersDeliveryAdressContactName = "";
                DeliveryAdressSearch = "";
                CustomersDeliveryAdressStreet = "";
                CustomersDeliveryAdressHouseNo = "";
                CustomersDeliveryAdressPostCode = "";
                CustomersDeliveryAdressCity = "";
                CustomersDeliveryAdressCountry = "";
            }
        }
    }
    
    
    [ObservableProperty] 
    private string inquiryNumber;
    
    [RelayCommand]
    private async Task SearchInquiryNumber(string value)
    {
        DeleteInquiryPositionDetails();

        if (string.IsNullOrWhiteSpace(value))
        {
            return; // Exit early if no inquiry number is provided
        }

        using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        try
        {
            // Query to get all customer data at once
            var inquiry = await connection.QueryFirstOrDefaultAsync<dynamic>(
                @"SELECT 
                        i.Inquirynumber,
                        i.Customernumber, 
                        i.Deliveryadressnumber, 
                        i.EXW, 
                        i.English, 
                        i.Notes,
                        c.Companyname AS CustomerCompanyName,
                        d.CompanyName AS DeliveryCompanyName
                    FROM inquiries i
                    JOIN customers c ON i.Customernumber = c.Customernumber
                    LEFT JOIN deliveryadress d ON i.Deliveryadressnumber = d.deliveryadressNo
                    WHERE i.Inquirynumber = @InquiryNo",
                new { InquiryNo = value }
            );
            
            // If inquiry exists, assign all properties
            if (inquiry != null)    
            {
                InquiryNumber = value;
                InquiryNotes = inquiry.Notes;
                
                if (inquiry.EXW == 0)
                {
                    CheckEXW = false;
                }
                else
                {
                    CheckEXW = true;
                }
                
                if (inquiry.English == 0)
                {
                    EnglishCustomer = false;
                }
                else
                {
                    EnglishCustomer = true;
                }
                
                DeliveryAdressSearch = inquiry.DeliveryCompanyName;
                CustomerNameSearch = inquiry.CustomerCompanyName;

                SurchargeList.Clear();
                
                // Query and load surcharges
                var results = await connection.QueryAsync<SurchargeListItem>(
                    @"SELECT SurchargeItem, Description, Amount FROM inquirysurcharges WHERE Inquirynumber = @InquiryNo",
                    new { InquiryNo = value }
                );

                // Add results to observable collection
                foreach (var item in results)
                {
                    SurchargeList.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            var box = MessageBoxManager.GetMessageBoxStandard("Error", ex.Message, ButtonEnum.Ok);
            await box.ShowAsync();
        }
    }

    private void DeleteInquiryPositionDetails()
    {
        CustomerStreet = "";
        CustomersHouseNo = "";
        CustomersCity = "";
        CustomersPostCode = "";
        CustomersCountry = "";
        CustomersPurchaserName = "";


        CustomersDeliveryAdressStreet = "";
        CustomersDeliveryAdressHouseNo = "";
        CustomersDeliveryAdressPostCode = "";
        CustomersDeliveryAdressCity = "";
        CustomersDeliveryAdressCountry = "";
        CustomersDeliveryAdressContactName = "";

        
        CustomerNameSearch = "";
        CustomersList.Clear();
        
        DeliveryAdressSearch = "";
        DeliveryAdressList.Clear();    
        
        SurchargeList.Clear();
        
        CheckEXW = false;
        EnglishCustomer = false;
    }
    
    partial void OnInquiryNumberChanged(string value)
    {
        SearchInquiryNumber(value);
    }
    
    [RelayCommand]
    private async void InquirySave()
    {
        if (SelectedCustomer != null && SelectedDeliveryAdress != null && String.IsNullOrEmpty(InquiryNumber))
        {
            string AbWerk;
            int AbWerkWert;
            if (CheckEXW) { AbWerk = "Ja"; AbWerkWert = 1; } else { AbWerk = "Nein"; AbWerkWert = 0; }
            string Englisch;
            int EnglischWert;
            if (EnglishCustomer) { Englisch = "Ja"; EnglischWert = 1; } else { Englisch = "Nein"; EnglischWert = 0; }
            
            var box = MessageBoxManager.GetMessageBoxStandard(
                "Kunde Bestätigen",
                $"Kundennummer?: {CustomerNumber}{Environment.NewLine}" +
                $"Ab Werk?: {AbWerk}{Environment.NewLine}" +
                $"Kunde: {SelectedCustomer}{Environment.NewLine}" +
                $"Lieferanschrift: {SelectedDeliveryAdress}{Environment.NewLine}" +
                $"Englisch?: {Englisch}{Environment.NewLine}" +
                $"Bemerkungen: {InquiryNotes}{Environment.NewLine}",
                ButtonEnum.YesNo
            );
            
            var result = await box.ShowAsync();

            if (result == ButtonResult.Yes)
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Insert new inquiry
                    string insertQuery = @"INSERT INTO inquiries 
                           (Customernumber, Deliveryadressnumber, EXW, English, Notes) 
                           VALUES 
                           (@InqCustomernumber, @InqDeliveryadressnumber, @InqEXW, @InqEnglish, @InqNotes)";

                    connection.Execute(insertQuery, new
                    {
                        InqCustomernumber = CustomerNumber,
                        InqDeliveryadressnumber = CustomersDeliveryAdressNo,
                        InqEXW = AbWerkWert,
                        InqEnglish = EnglischWert,
                        InqNotes = InquiryNotes
                    });

                    // Retrieve the last inserted Inquirynumber
                    string selectQuery = "SELECT Inquirynumber FROM inquiries ORDER BY Inquirynumber DESC LIMIT 1";
                    InquiryNumber = connection.QuerySingleOrDefault<int>(selectQuery).ToString();
                }
            }
        }
        else if (!String.IsNullOrEmpty(InquiryNumber))
        {
            string UserConfirmationNotice = "";
            string UserQuery;

            string InquiryNotesCompare = "";
            int DeliveryEXWCompareVal = 0;
            string DeliveryEXWCompare = "";
            string DeliveryEXWText = "";
            string EnglishCustomerCompare = "";
            string EnglishCustomerText = "";
            int EnglishCustomerCompareVal = 0;
            string DeliveryAdressSearchCompare = "";
            string CustomerNameSearchCompare = "";

            if (CheckEXW)
            {
                DeliveryEXWText = "Pickup";
            }
            else
            {
                DeliveryEXWText = "Delivery";
            }

            if (EnglishCustomer)
            {
                EnglishCustomerText = "English";
            }
            else
            {
                EnglishCustomerText = "German"; 
            }
            
            if (string.IsNullOrWhiteSpace(InquiryNumber))
            {
                return; // Exit early if no inquiry number is provided
            }

            using var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();

            try
            {
                // Query to get all customer data at once
                var inquiry = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    @"SELECT 
                            i.Inquirynumber,
                            i.Customernumber, 
                            i.Deliveryadressnumber, 
                            i.EXW, 
                            i.English, 
                            i.Notes,
                            c.Companyname AS CustomerCompanyName,
                            d.CompanyName AS DeliveryCompanyName
                        FROM inquiries i
                        JOIN customers c ON i.Customernumber = c.Customernumber
                        LEFT JOIN deliveryadress d ON i.Deliveryadressnumber = d.deliveryadressNo
                        WHERE i.Inquirynumber = @InquiryNo",
                    new { InquiryNo = InquiryNumber }
                );
                
                // If inquiry exists, assign all properties
                if (inquiry != null)    
                {
                    DeliveryAdressSearchCompare = inquiry.DeliveryCompanyName;
                    CustomerNameSearchCompare = inquiry.CustomerCompanyName;
                    
                    InquiryNotesCompare = inquiry.Notes;
                    
                    if (inquiry.EXW == 0)
                    {
                        DeliveryEXWCompare = "Delivery";
                        DeliveryEXWCompareVal = 0;
                    }
                    else
                    {
                        DeliveryEXWCompare = "Pickup";
                        DeliveryEXWCompareVal = 1;
                    }
                    
                    if (inquiry.English == 0)
                    {
                        EnglishCustomerCompare = "German";
                        EnglishCustomerCompareVal = 0;
                    }
                    else
                    {
                        EnglishCustomerCompare = "English";
                        EnglishCustomerCompareVal = 1;
                    }
                    

                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                var box = MessageBoxManager.GetMessageBoxStandard("Error", ex.Message, ButtonEnum.Ok);
                await box.ShowAsync();
            }

            // Start building query
            var updates = new List<string>();
            var parameters = new DynamicParameters();

            // Compare and build updates
            if (!String.Equals(CustomerNameSearchCompare, SelectedCustomer))
            {
                UserConfirmationNotice = "Customer changed from " + CustomerNameSearchCompare + " to " + SelectedCustomer + $"{Environment.NewLine}";
                updates.Add("Customernumber = @CustomerNumber");
                parameters.Add("CustomerNumber", CustomerNumber); // Assuming you already have CustomerNumber as int
            }

            if (!String.Equals(DeliveryAdressSearchCompare, SelectedDeliveryAdress))
            {
                UserConfirmationNotice += "Delivery Address changed from " + DeliveryAdressSearchCompare + " to " + SelectedDeliveryAdress + $"{Environment.NewLine}";
                updates.Add("Deliveryadressnumber = @DeliveryAddressNumber");
                parameters.Add("DeliveryAddressNumber", CustomersDeliveryAdressNo); // Assuming you have this value
            }

            if (DeliveryEXWCompareVal != (CheckEXW ? 1 : 0))
            {
                UserConfirmationNotice += "Shipping changed from " + DeliveryEXWCompare + " to " + DeliveryEXWText + $"{Environment.NewLine}";
                updates.Add("EXW = @EXW");
                parameters.Add("EXW", CheckEXW ? 1 : 0);

                if (CheckEXW)
                {
                    updates.Add("Deliveryadressnumber = NULL");
                }
                else if (!CheckEXW && !String.IsNullOrEmpty(SelectedDeliveryAdress))
                {
                    updates.Add("Deliveryadressnumber = @DeliveryAddressNumber");
                    
                    string CustomerIDQuery = "SELECT deliveryadressNo FROM deliveryadress WHERE CompanyName = @QueryDeliveryAdressSearch";
                    
                    Console.WriteLine("Query: " + CustomerIDQuery);
                    
                    Console.WriteLine("SearchKey: " + SelectedDeliveryAdress);


            
                    // Since you expect a single result, use QuerySingleOrDefault to get just one result or null if not found
                    var deliveryAdressNo = connection.QuerySingleOrDefault<int?>(CustomerIDQuery, new { QueryDeliveryAdressSearch = SelectedDeliveryAdress });
                    
                    Console.WriteLine("Search Result: " + deliveryAdressNo);
                    parameters.Add("DeliveryAddressNumber", deliveryAdressNo);
                }
                else if (!CheckEXW && String.IsNullOrEmpty(SelectedDeliveryAdress))
                {
                    var BrokenMessage = MessageBoxManager.GetMessageBoxStandard(
                        "Fehler", "Lieferung ausgewählt aber keine Lieferadresse gewählt!",
                        ButtonEnum.Ok
                    );

                    var Display = await BrokenMessage.ShowAsync();
                    
                    return;
                }
            }

            if (EnglishCustomerCompareVal != (EnglishCustomer ? 1 : 0))
            {
                UserConfirmationNotice += "Language changed from " + EnglishCustomerCompare + " to " + EnglishCustomerText + $"{Environment.NewLine}";
                updates.Add("English = @English");
                parameters.Add("English", EnglishCustomer ? 1 : 0);
            }
            
            if (!String.Equals(InquiryNotesCompare, InquiryNotes))
            {
                UserConfirmationNotice += "Notes changed from " + InquiryNotesCompare + " to " + InquiryNotes + $"{Environment.NewLine}";
                updates.Add("Notes = @Notes");
                parameters.Add("Notes", InquiryNotes);
            }

            // Show confirmation message
            var ChangeQuery = MessageBoxManager.GetMessageBoxStandard(
                "Änderungen Bestätigen", UserConfirmationNotice,
                ButtonEnum.YesNo
            );

            var result = await ChangeQuery.ShowAsync();

            if (result == ButtonResult.Yes && updates.Count > 0)
            {
                // Build final query
                string updateQuery = $"UPDATE inquiries SET {string.Join(", ", updates)} WHERE Inquirynumber = @InquiryNumber";
                parameters.Add("InquiryNumber", InquiryNumber);
                
                await connection.ExecuteAsync(updateQuery, parameters);
            }
        }
    }
    
    [RelayCommand]
    private void InquirySavetoPDF()
    {

    }
    
    [ObservableProperty] 
    private int surchargeItemSelector;    
    [ObservableProperty] 
    private string surchargeDescription;  
    [ObservableProperty] 
    private string surchargeAmount;  
    [ObservableProperty]
    private object surchargeItemSelectorObject;

    public string SurchargeItemSelectorText => 
        (SurchargeItemSelectorObject as ComboBoxItem)?.Content?.ToString();    
    
    private SurchargeListItem _selectedSurcharge;
    public SurchargeListItem SelectedSurcharge
    {
        get => _selectedSurcharge;
        set
        {
            if (_selectedSurcharge != value)
            {
                _selectedSurcharge = value;
                OnPropertyChanged();
                OnSelectedSurchargeChanged();
            }
        }
    }

    private async void OnSelectedSurchargeChanged()
    {
        if (SelectedSurcharge == null)
        {
            return;
        }
        
        using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        var sql = @"SELECT SurchargeNumber 
            FROM inquirysurcharges 
            WHERE SurchargeItem = @SurchargeType 
              AND Description = @Description 
              AND Amount = @Amount 
              AND Inquirynumber = @Inqnumber";

        var parameters = new { 
            SurchargeType = SelectedSurcharge.SurchargeItem, 
            Description = SelectedSurcharge.Description, 
            Amount = SelectedSurcharge.Amount,
            Inqnumber = InquiryNumber 
        };

        var surchargeNumber = await connection.QuerySingleOrDefaultAsync<int>(sql, parameters);
        
        var result = await connection.QuerySingleOrDefaultAsync<SurchargeListItemIndex>(
            @"SELECT 
                    DESCRIPTION, 
                    Amount, 
                    SurchargeIndex AS SurchargeItemIndex 
                  FROM inquirysurcharges 
                  WHERE SurchargeNumber = @SurchargeNumber",
            new { SurchargeNumber = surchargeNumber }
        );
        
        SurchargeAmount = result.Amount.ToString();
        SurchargeDescription = result.Description;
        SurchargeItemSelector = result.SurchargeItemIndex;
        
    }
    
    [RelayCommand]
    private async void SurchargeSave()
    {
        if (SelectedSurcharge == null)
        {
            if (!String.IsNullOrEmpty(SurchargeItemSelectorText) && !String.IsNullOrEmpty(SurchargeDescription) &&
                !String.IsNullOrEmpty(SurchargeAmount) && !String.IsNullOrEmpty(InquiryNumber))
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Insert new inquiry
                    string insertQuery = @"INSERT INTO inquirysurcharges 
                           (SurchargeItem, SurchargeIndex, Description, Amount, Inquirynumber) 
                           VALUES 
                           (@SurcSurchargeItem, @SurcSurchargeIndex, @SurcSurchargeDescription, @SurcSurchargeAmount, @SurcSurchargeInquirynumber)";

                    connection.Execute(insertQuery, new
                    {
                        SurcSurchargeItem = SurchargeItemSelectorText,
                        SurcSurchargeIndex = SurchargeItemSelector,
                        SurcSurchargeDescription = SurchargeDescription,
                        SurcSurchargeAmount = SurchargeAmount,
                        SurcSurchargeInquirynumber = InquiryNumber,
                    });
                }

                string CurrentInqNumber = InquiryNumber;

                InquiryNumber = "";

                InquiryNumber = CurrentInqNumber;
            }
            else
            {
                var ChangeQuery = MessageBoxManager.GetMessageBoxStandard(
                    "Änderungen Bestätigen", "Missing Data!",
                    ButtonEnum.Ok
                );

                var result = await ChangeQuery.ShowAsync();
                
                return;
            }
        }
        else
        {
            using var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();

            var sql = @"SELECT SurchargeNumber 
                FROM inquirysurcharges 
                WHERE SurchargeItem = @SurchargeType 
                  AND Description = @Description 
                  AND Amount = @Amount 
                  AND Inquirynumber = @Inqnumber";

            var parameters = new { 
                SurchargeType = SelectedSurcharge.SurchargeItem, 
                Description = SelectedSurcharge.Description, 
                Amount = SelectedSurcharge.Amount,
                Inqnumber = InquiryNumber 
            };

            var surchargeNumber = await connection.QuerySingleOrDefaultAsync<int>(sql, parameters);


            
            if (SelectedSurcharge.SurchargeItem != SurchargeItemSelectorText)
            {
                sql = @"UPDATE inquirysurcharges SET SurchargeItem = @SurchargeItem WHERE SurchargeNumber = @SurchargeNumber";

                var UpdateSurchargeItem = new 
                {
                    SurchargeItem = SurchargeItemSelectorText.ToString(),
                    SurchargeNumber = surchargeNumber
                };

                var affectedRows = await connection.ExecuteAsync(sql, UpdateSurchargeItem);

                Console.WriteLine($"Updated SurchargeItem from {SelectedSurcharge.SurchargeItem} to {SurchargeItemSelectorText} in {surchargeNumber}");
            }
            
            if (SelectedSurcharge.Description != SurchargeDescription)
            {
                sql = @"UPDATE inquirysurcharges SET Description = @Description WHERE SurchargeNumber = @SurchargeNumber";

                var UpdateSurchargeDescription = new 
                {
                    Description = SurchargeDescription,
                    SurchargeNumber = surchargeNumber
                };

                var affectedRows = await connection.ExecuteAsync(sql, UpdateSurchargeDescription);

                Console.WriteLine($"Updated SurchargeItem from {SelectedSurcharge.Description} to {SurchargeDescription} in {surchargeNumber}");
                
            }
            
            if (SelectedSurcharge.Amount.ToString() != SurchargeAmount)
            {
                sql = @"UPDATE inquirysurcharges SET Amount = @Amount WHERE SurchargeNumber = @SurchargeNumber";

                var UpdateSurchargeAmount = new 
                {
                    Amount = SurchargeAmount,
                    SurchargeNumber = surchargeNumber
                };

                var affectedRows = await connection.ExecuteAsync(sql, UpdateSurchargeAmount);

                Console.WriteLine($"Updated SurchargeItem from {SelectedSurcharge.Amount} to {SurchargeAmount} in {surchargeNumber}");
            }
            
            string CurrentInqNumber = InquiryNumber;
            InquiryNumber = "";
            InquiryNumber = CurrentInqNumber;
        }
    }

    [RelayCommand]
    private async void SurchargeDelete()
    {
        if (SelectedSurcharge == null)
        {
            return;
        }

        var box = MessageBoxManager.GetMessageBoxStandard(
            "Do you really want to delete?",
            $"Anfragenummer?: {InquiryNumber}{Environment.NewLine}" +
            $"Surcharge Type: {SelectedSurcharge.SurchargeItem}{Environment.NewLine}" +
            $"Description: {SelectedSurcharge.Description}{Environment.NewLine}" +
            $"Amount: {SelectedSurcharge.Amount}{Environment.NewLine}",
            ButtonEnum.YesNo
        );

        var result = await box.ShowAsync();

        if (result == ButtonResult.Yes)
        {

            using var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();

            var sql = @"SELECT SurchargeNumber 
            FROM inquirysurcharges 
            WHERE SurchargeItem = @SurchargeType 
              AND Description = @Description 
              AND Amount = @Amount 
              AND Inquirynumber = @Inqnumber";

            var parameters = new
            {
                SurchargeType = SelectedSurcharge.SurchargeItem,
                Description = SelectedSurcharge.Description,
                Amount = SelectedSurcharge.Amount,
                Inqnumber = InquiryNumber
            };

            var surchargeNumber = await connection.QuerySingleOrDefaultAsync<int>(sql, parameters);

            sql = "DELETE FROM inquirysurcharges WHERE SurchargeNumber = @SurchargeNumber";

            var DeleteParameters = new { SurchargeNumber = surchargeNumber };

            var affectedRows = await connection.ExecuteAsync(sql, DeleteParameters);

            Console.WriteLine($"{affectedRows} row(s) deleted.");

            string CurrentInqNumber = InquiryNumber;
            InquiryNumber = "";
            InquiryNumber = CurrentInqNumber;
        }
    }





    [RelayCommand]
    private async void Test()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        CreateEmailFile("recipient@example.com", "Your Document");
    }
    
    public static void CreateEmailFile(string recipientEmail, string subject = "Document")
    {
        try
        {
            // Create PDF
            string pdfPath = CreatePdf();

            // Create EML file
            string emlPath = CreateEmlFile(recipientEmail, subject, pdfPath);

            // Open the .eml file with default application
            Process.Start(new ProcessStartInfo(emlPath) { UseShellExecute = true });

            Console.WriteLine("Email file created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a PDF document with sample content
    /// </summary>
    /// <returns>Path to the created PDF file</returns>
    private static string CreatePdf()
    {
        // Generate a unique filename for the PDF
        string pdfPath = Path.Combine(Path.GetTempPath(), $"Document_{Guid.NewGuid()}.pdf");

        // Create PDF using QuestPDF
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.Header().Text("Document Title", TextStyle.Default.FontSize(18).SemiBold());

                page.Content().Column(column =>
                {
                    column.Item().Text($"This is a sample document generated automatically.");
                    column.Item().Text($"Generated on: {DateTime.Now}");
                });

                page.Footer().Text(x =>
                {
                    x.DefaultTextStyle(TextStyle.Default.FontSize(10));
                    x.Span("Page ");
                    x.Span(" of ");
                });
            });
        }).GeneratePdf(pdfPath);

        return pdfPath;
    }

    /// <summary>
    /// Creates an .eml file with the email content and attachment
    /// </summary>
    /// <param name="recipientEmail">Email address of the recipient</param>
    /// <param name="subject">Subject of the email</param>
    /// <param name="pdfPath">Path to the PDF file to attach</param>
    /// <returns>Path to the created .eml file</returns>
    private static string CreateEmlFile(string recipientEmail, string subject, string pdfPath)
    {
        // Generate a unique filename for the EML file
        string emlPath = Path.Combine(Path.GetTempPath(), $"Email_{Guid.NewGuid()}.eml");

        // Create the email message
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("your-email@example.com", "Your Name");
            mailMessage.To.Add(recipientEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = "Please find the attached document.";

            // Attach the PDF
            Attachment attachment = new Attachment(pdfPath);
            mailMessage.Attachments.Add(attachment);

            // Save the email as .eml file
            using (StreamWriter writer = new StreamWriter(emlPath))
            {
                writer.Write("To: " + mailMessage.To + "\r\n");
                writer.Write("From: " + mailMessage.From + "\r\n");
                writer.Write("Subject: " + mailMessage.Subject + "\r\n");
                writer.Write("Content-Type: multipart/mixed; boundary=\"boundary\"\r\n\r\n");
                writer.Write("--boundary\r\n");
                writer.Write("Content-Type: text/plain; charset=utf-8\r\n\r\n");
                writer.Write(mailMessage.Body + "\r\n\r\n");

                // Add attachment
                writer.Write("--boundary\r\n");
                writer.Write($"Content-Type: application/pdf; name=\"{Path.GetFileName(pdfPath)}\"\r\n");
                writer.Write("Content-Transfer-Encoding: base64\r\n");
                writer.Write($"Content-Disposition: attachment; filename=\"{Path.GetFileName(pdfPath)}\"\r\n\r\n");

                // Convert PDF to Base64
                byte[] pdfBytes = File.ReadAllBytes(pdfPath);
                writer.Write(Convert.ToBase64String(pdfBytes));

                writer.Write("\r\n--boundary--");
            }
        }

        return emlPath;
    }
}