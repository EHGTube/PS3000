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


namespace PS3000.Controls;

public partial class CustomersViewModel : ObservableObject
{
    
    //Following is to search the Delivery Adresses
    
    [ObservableProperty] 
    private string deliveryAdressSearch;
    
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
            DeliveryAdressList.Clear();        }
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

        // If delivery address exists, assign all properties
        if (deliveryAddress != null)
        {
            CustomersDeliveryAdressCompany = SelectedDeliveryAdress;
            CustomersDeliveryAdressStreet = deliveryAddress.Street;
            CustomersDeliveryAdressHouseNo = deliveryAddress.HouseNo;
            CustomersDeliveryAdressPostCode = deliveryAddress.Postcode;
            CustomersDeliveryAdressCity = deliveryAddress.City;
            CustomersDeliveryAdressCountry = deliveryAddress.Country;
            CustomersDeliveryAdressContactName = deliveryAddress.ContactName;
            CustomersDeliveryAdressContactPhone = deliveryAddress.ContactPhone;
            CustomersDeliveryAdressContactMail = deliveryAddress.ContactMail;
            CustomersDeliveryAdressNumber = deliveryAddress.deliveryadressNo?.ToString();
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
    
    
    //Following will update an existing Delivery Adress or add a new one: 
    [RelayCommand]
    private async void AddDeliveryAdress()
    {
        if (String.IsNullOrEmpty(deliveryAdressSearch))
        {
            {
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Neue Lieferadresse Anlegen?",
                    $"Firmenname: {customersDeliveryAdressCompany}{Environment.NewLine}" +
                        $"Straße: {customersDeliveryAdressStreet}{Environment.NewLine}" +
                        $"Hausnummer: {customersDeliveryAdressHouseNo}{Environment.NewLine}" +
                        $"PLZ: {customersDeliveryAdressPostCode}{Environment.NewLine}" +
                        $"Stadt: {customersDeliveryAdressCity}{Environment.NewLine}" +
                        $"Land: {customersDeliveryAdressCountry}{Environment.NewLine}" +
                        $"Kontakt Name: {customersDeliveryAdressContactName}{Environment.NewLine}" +
                        $"Kontakt Telefon: {customersDeliveryAdressContactPhone}{Environment.NewLine}" +
                        $"Kontakt E-Mail: {customersDeliveryAdressContactMail}{Environment.NewLine}",
                    ButtonEnum.YesNo);
            
                var result = await box.ShowAsync();
            
                if (result == ButtonResult.Yes)
                {
                    const string sql = @"
                    INSERT INTO deliveryadress (CompanyName, Street, HouseNo, Postcode, City, Country, ContactName, ContactPhone, ContactMail)
                    VALUES (@CompanyName, @Street, @HouseNo, @Postcode, @City, @Country, @ContactName, @ContactPhone, @ContactMail);
                    SELECT LAST_INSERT_ID();";
            
                    using var connection = new MySqlConnection(ConnectionString);
                    await connection.OpenAsync();
            
                    // Using DynamicParameters for more flexibility
                    var parameters = new DynamicParameters();
                    parameters.Add("@CompanyName", customersDeliveryAdressCompany);
                    parameters.Add("@Street", customersDeliveryAdressStreet);
                    parameters.Add("@HouseNo", customersDeliveryAdressHouseNo);
                    parameters.Add("@Postcode", customersDeliveryAdressPostCode);
                    parameters.Add("@City", customersDeliveryAdressCity);
                    parameters.Add("@Country", customersDeliveryAdressCountry);
                    parameters.Add("@ContactName", customersDeliveryAdressContactName);
                    parameters.Add("@ContactPhone", customersDeliveryAdressContactPhone);
                    parameters.Add("@ContactMail", customersDeliveryAdressContactMail);
                    
                    var deliveryAddressId = await connection.ExecuteScalarAsync<int>(sql, parameters);
                    
                    var donebox = MessageBoxManager
                        .GetMessageBoxStandard("Erfolgreich!", $"Lieferadresse-ID: {deliveryAddressId}", ButtonEnum.Ok);

                    var tast = await donebox.ShowAsync();
                }
            }
        }
        else
        {
            var box = MessageBoxManager
                    .GetMessageBoxStandard("Änderungen Bestätigen",
                    $"Firmenname: {customersDeliveryAdressCompany}{Environment.NewLine}" +
                        $"Adress-ID: {customersDeliveryAdressNumber}{Environment.NewLine}" +
                        $"Straße: {customersDeliveryAdressStreet}{Environment.NewLine}" +
                        $"Hausnummer: {customersDeliveryAdressHouseNo}{Environment.NewLine}" +
                        $"PLZ: {customersDeliveryAdressPostCode}{Environment.NewLine}" +
                        $"Stadt: {customersDeliveryAdressCity}{Environment.NewLine}" +
                        $"Land: {customersDeliveryAdressCountry}{Environment.NewLine}" +
                        $"Kontakt Name: {customersDeliveryAdressContactName}{Environment.NewLine}" +
                        $"Kontakt Telefon: {customersDeliveryAdressContactPhone}{Environment.NewLine}" +
                        $"Kontakt E-Mail: {customersDeliveryAdressContactMail}{Environment.NewLine}",
                    ButtonEnum.YesNo);
            
                var result = await box.ShowAsync();
                
            if (result == ButtonResult.Yes)
            {
                // First, fetch the current delivery address data
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                
                var currentDeliveryAddress = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "SELECT * FROM deliveryadress WHERE deliveryadressNo = @deliveryadressNo", 
                    new { deliveryadressNo = customersDeliveryAdressNumber }
                );
                
                if (currentDeliveryAddress == null)
                {
                    var notFoundBox = MessageBoxManager.GetMessageBoxStandard(
                        "Fehler!", 
                        $"Lieferadresse mit ID {customersDeliveryAdressNumber} wurde nicht gefunden.", 
                        ButtonEnum.Ok
                    );
                    await notFoundBox.ShowAsync();
                    return;
                }
                
                // Create a dictionary to store field-value pairs for fields that have changed
                var updates = new Dictionary<string, object>();
                var fieldMappings = new Dictionary<string, (object currentValue, object newValue)>
                {
                    { "CompanyName", (currentDeliveryAddress.CompanyName, customersDeliveryAdressCompany) },
                    { "Street", (currentDeliveryAddress.Street, customersDeliveryAdressStreet) },
                    { "HouseNo", (currentDeliveryAddress.HouseNo, customersDeliveryAdressHouseNo) },
                    { "Postcode", (currentDeliveryAddress.Postcode, customersDeliveryAdressPostCode) },
                    { "City", (currentDeliveryAddress.City, customersDeliveryAdressCity) },
                    { "Country", (currentDeliveryAddress.Country, customersDeliveryAdressCountry) },
                    { "ContactName", (currentDeliveryAddress.ContactName, customersDeliveryAdressContactName) },
                    { "ContactPhone", (currentDeliveryAddress.ContactPhone, customersDeliveryAdressContactPhone) },
                    { "ContactMail", (currentDeliveryAddress.ContactMail, customersDeliveryAdressContactMail) }
                };
                
                // Add changed fields to the updates dictionary
                foreach (var field in fieldMappings)
                {
                    var currentVal = field.Value.currentValue;
                    var newVal = field.Value.newValue;

                    // Convert both values to string, replace commas with dots, and trim spaces
                    string currentValStr = currentVal?.ToString().Replace(",", ".").Trim() ?? "";
                    string newValStr = newVal?.ToString().Replace(",", ".").Trim() ?? "";

                    // Compare the processed strings
                    if (!currentValStr.Equals(newValStr))
                    {
                        // Add the original newVal to updates (not the processed string)
                        updates.Add(field.Key, field.Value.newValue);
                    }
                }
                
                // If no changes were detected, inform the user and exit
                if (updates.Count == 0)
                {
                    var noChangesBox = MessageBoxManager.GetMessageBoxStandard(
                        "Information", 
                        "Es wurden keine Änderungen festgestellt.", 
                        ButtonEnum.Ok
                    );
                    await noChangesBox.ShowAsync();
                    return;
                }
                
                // Build the SQL query with only the changed fields
                var setClause = string.Join(", ", updates.Keys.Select(key => $"{key} = @{key}"));
                string sql = $"UPDATE deliveryadress SET {setClause} WHERE deliveryadressNo = @deliveryadressNo";
                
                // Create parameters with only the changed fields plus the deliveryadressNo
                var parameters = new DynamicParameters(updates);
                parameters.Add("@deliveryadressNo", customersDeliveryAdressNumber);
                
                // Create a copy of the SQL for logging purposes
                string logSql = sql;

                // Replace each parameter with its value for logging
                foreach (var param in updates)
                {
                    logSql = logSql.Replace($"@{param.Key}", param.Value?.ToString() ?? "NULL");
                }

                // Replace the deliveryadressNo parameter
                logSql = logSql.Replace("@deliveryadressNo", customersDeliveryAdressNumber);

                Console.WriteLine(logSql);                
                // Execute the update
                int rowsAffected = await connection.ExecuteAsync(sql, parameters);
                
                // Show result message
                if (rowsAffected > 0)
                {
                    var ResultQuery = MessageBoxManager.GetMessageBoxStandard(
                        "Erfolgreich",
                        $"Lieferadresse: {customersDeliveryAdressCompany} wurde bearbeitet! ({updates.Count} Felder aktualisiert)",
                        ButtonEnum.Ok
                    );
                    await ResultQuery.ShowAsync();
                }
                else
                {
                    var ResultQuery = MessageBoxManager.GetMessageBoxStandard(
                        "Fehler!",
                        $"Lieferadresse: {customersDeliveryAdressCompany} wurde nicht bearbeitet!",
                        ButtonEnum.Ok
                    );
                    await ResultQuery.ShowAsync();
                }
            }
        }

    }
    
    
    
    //Following is to search the Company Details
    [ObservableProperty] 
    private string customerNameSearch;
    
    public ObservableCollection<string> CustomersList { get; private set; } = new ObservableCollection<string>();

    [RelayCommand]
    private async Task SearchCustomers(string value)
    {
        CustomersList.Clear();

        if (string.IsNullOrEmpty(value))
        {
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
    }    
    
    partial void OnCustomerNameSearchChanged(string value)
    {
        SearchCustomers(value);
    }
    

    
    
    
    
    
    //Following is to load Details once Customer Company has been selected
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
    string customersOCMail;
    [ObservableProperty]
    string customersNumber;
    [ObservableProperty]
    string customersName;
    [ObservableProperty]
    string customershortName;
    [ObservableProperty]
    string customerStreet;
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
    [ObservableProperty]
    string customersInsurance;

    
    //Following is to load Details once Customer company has been selected
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

        // If customer exists, assign all properties
        if (customer != null)
        {
            CustomersName = SelectedCustomer;
            CustomershortName = customer.Customernick;
            CustomersPurchaserName = customer.PurchaserName;
            CustomersPurchaserPhone = customer.PurchaserPhone;
            CustomersPurchaserMail = customer.PurchaserMail;
            CustomersBookkeeperName = customer.BookkeeperName;
            CustomersBookkeeperPhone = customer.BookkeeperPhone;
            CustomersBookkeeperMail = customer.BookkeeperMail;
            CustomersCertificateMail = customer.CertificateMail;
            CustomersInvoiceMail = customer.InvoiceMail;
            CustomersOCMail = customer.OCMail;
            CustomersNumber = customer.Customernumber?.ToString();
            CustomerStreet = customer.InvoiceStreet;
            CustomersHouseNo = customer.InvoiceHouseNo;
            CustomersCity = customer.InvoiceCity;
            CustomersPostCode = customer.InvoicePostcode;
            CustomersCountry = customer.InvoiceCountry;
            CustomerSkonto = customer.Skonto?.ToString();
            CustomerSkontoTerm = customer.SkontoTerm?.ToString();
            CustomersNettoTerm = customer.NettoTerm?.ToString();
            CustomersNotes = customer.Notes;
            CustomersInsurance = customer.InsuranceLimit?.ToString();
        }
        else
        {
            // Handle case where customer was not found
            var notFoundBox = MessageBoxManager.GetMessageBoxStandard(
                "Fehler!", 
                $"Kunde '{SelectedCustomer}' wurde nicht gefunden.", 
                ButtonEnum.Ok
            );
            await notFoundBox.ShowAsync();
        }
    }
    
    [RelayCommand]
    private async void AddCustomer()
    {
        if (String.IsNullOrEmpty(SelectedCustomer))
        {
            {
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Neuen Kunden Anlegen?",
                    $"Firmenname: {customersName}{Environment.NewLine}" +
                        $"Kürzel: {customershortName}{Environment.NewLine}" +
                        $"Straße: {customerStreet}{Environment.NewLine}" +
                        $"Hausnummer: {customersHouseNo}{Environment.NewLine}" +
                        $"PLZ: {customersPostCode}{Environment.NewLine}" +
                        $"Stadt: {customersCity}{Environment.NewLine}" +
                        $"Land: {customersCountry}{Environment.NewLine}" +
                        $"Einkäufer Name: {customersPurchaserName}{Environment.NewLine}" +
                        $"Einkäufer Telefon: {customersPurchaserPhone}{Environment.NewLine}" +
                        $"Einkäufer E-Mail: {customersPurchaserMail}{Environment.NewLine}" +
                        $"Buchhaltung Name: {customersBookkeeperName}{Environment.NewLine}" +
                        $"Buchhalung Telefon: {customersBookkeeperPhone}{Environment.NewLine}" +
                        $"Buchhalung E-Mail: {customersBookkeeperMail}{Environment.NewLine}" +
                        $"Werkszeugnis E-Mail: {customersCertificateMail}{Environment.NewLine}" +
                        $"Rechung E-Mail: {customersInvoiceMail}{Environment.NewLine}" +
                        $"AB E-Mail: {customersOCMail}{Environment.NewLine}" +
                        $"Notizen: {customersNotes}{Environment.NewLine}" +
                        $"Skonto: {customerSkonto}{Environment.NewLine}" +
                        $"Skontofrist: {customerSkontoTerm}{Environment.NewLine}" +
                        $"Nettofrist: {customersNettoTerm}{Environment.NewLine}" +
                        $"Versicherungslimit: {customersInsurance}{Environment.NewLine}",
                    ButtonEnum.YesNo);
            
                var result = await box.ShowAsync();
            
                if (result == ButtonResult.Yes)
                {
                    const string sql = @"
                    INSERT INTO Customers (Customernick, Companyname, InvoiceStreet, InvoiceHouseNo, InvoiceCity, InvoicePostcode, InvoiceCountry, PurchaserName, PurchaserPhone, PurchaserMail, BookkeeperName, BookkeeperPhone, BookkeeperMail, CertificateMail, InvoiceMail, OCMail, Notes, Skonto, SkontoTerm, NettoTerm, InsuranceLimit)
                    VALUES (@Customernick, @Companyname, @InvoiceStreet, @InvoiceHouseNo, @InvoiceCity, @InvoicePostcode, @InvoiceCountry, @PurchaserName, @PurchaserPhone, @PurchaserMail, @BookkeeperName, @BookkeeperPhone, @BookkeeperMail, @CertificateMail, @InvoiceMail, @OCMail, @Notes, @Skonto, @SkontoTerm, @NettoTerm, @InsuranceLimit);
                    SELECT LAST_INSERT_ID();";
            
                    using var connection = new MySqlConnection(ConnectionString);
                    await connection.OpenAsync();
            
                    // Using DynamicParameters for more flexibility
                    var parameters = new DynamicParameters();
                    parameters.Add("@Customernick", customershortName);
                    parameters.Add("@Companyname", customersName);
                    parameters.Add("@InvoiceStreet", customerStreet);
                    parameters.Add("@InvoiceHouseNo", customersHouseNo);
                    parameters.Add("@InvoiceCity", customersCity);
                    parameters.Add("@InvoicePostcode", customersPostCode);
                    parameters.Add("@InvoiceCountry", customersCountry);
                    parameters.Add("@PurchaserName", customersPurchaserName);
                    parameters.Add("@PurchaserPhone", customersPurchaserPhone);
                    parameters.Add("@PurchaserMail", customersPurchaserMail);
                    parameters.Add("@BookkeeperName", customersBookkeeperName);
                    parameters.Add("@BookkeeperPhone", customersBookkeeperPhone);
                    parameters.Add("@BookkeeperMail", customersBookkeeperMail);
                    parameters.Add("@CertificateMail", customersCertificateMail);
                    parameters.Add("@InvoiceMail", customersInvoiceMail);
                    parameters.Add("@OCMail", customersOCMail);
                    parameters.Add("@Notes", customersNotes);
                    parameters.Add("@Skonto", customerSkonto);
                    parameters.Add("@SkontoTerm", customerSkontoTerm);
                    parameters.Add("@NettoTerm", customersNettoTerm);
                    parameters.Add("@InsuranceLimit", customersInsurance);
                    
                    var customerId = await connection.ExecuteScalarAsync<int>(sql, parameters);
                    
                    var donebox = MessageBoxManager
                        .GetMessageBoxStandard("Erfolgreich!", $"Kundennummer: {customerId}",ButtonEnum.Ok);

                    var tast = await donebox.ShowAsync();
                }
            }
        }
        else
        {
            var box = MessageBoxManager
                    .GetMessageBoxStandard("Änderungen Bestätigen",
                    $"Firmenname: {customersName}{Environment.NewLine}" +
                        $"Kürzel: {customershortName}{Environment.NewLine}" +
                        $"Kundennummer: {customersNumber}{Environment.NewLine}" +
                        $"Straße: {customerStreet}{Environment.NewLine}" +
                        $"Hausnummer: {customersHouseNo}{Environment.NewLine}" +
                        $"PLZ: {customersPostCode}{Environment.NewLine}" +
                        $"Stadt: {customersCity}{Environment.NewLine}" +
                        $"Land: {customersCountry}{Environment.NewLine}" +
                        $"Einkäufer Name: {customersPurchaserName}{Environment.NewLine}" +
                        $"Einkäufer Telefon: {customersPurchaserPhone}{Environment.NewLine}" +
                        $"Einkäufer E-Mail: {customersPurchaserMail}{Environment.NewLine}" +
                        $"Buchhaltung Name: {customersBookkeeperName}{Environment.NewLine}" +
                        $"Buchhalung Telefon: {customersBookkeeperPhone}{Environment.NewLine}" +
                        $"Buchhalung E-Mail: {customersBookkeeperMail}{Environment.NewLine}" +
                        $"Werkszeugnis E-Mail: {customersCertificateMail}{Environment.NewLine}" +
                        $"Rechung E-Mail: {customersInvoiceMail}{Environment.NewLine}" +
                        $"AB E-Mail: {customersOCMail}{Environment.NewLine}" +
                        $"Notizen: {customersNotes}{Environment.NewLine}" +
                        $"Skonto: {customerSkonto}{Environment.NewLine}" +
                        $"Skontofrist: {customerSkontoTerm}{Environment.NewLine}" +
                        $"Nettofrist: {customersNettoTerm}{Environment.NewLine}" +
                        $"Versicherungslimit: {customersInsurance}{Environment.NewLine}",
                    ButtonEnum.YesNo);
            
                var result = await box.ShowAsync();
                
            if (result == ButtonResult.Yes)
            {
                // First, fetch the current customer data
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                
                var currentCustomer = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "SELECT * FROM Customers WHERE Customernumber = @Customernumber", 
                    new { Customernumber = customersNumber }
                );
                
                if (currentCustomer == null)
                {
                    var notFoundBox = MessageBoxManager.GetMessageBoxStandard(
                        "Fehler!", 
                        $"Kunde mit Nummer {customersNumber} wurde nicht gefunden.", 
                        ButtonEnum.Ok
                    );
                    await notFoundBox.ShowAsync();
                    return;
                }
                
                // Create a dictionary to store field-value pairs for fields that have changed
                var updates = new Dictionary<string, object>();
                var fieldMappings = new Dictionary<string, (object currentValue, object newValue)>
                {
                    { "Companyname", (currentCustomer.Companyname, customersName) },
                    { "Customernick", (currentCustomer.Customernick, customershortName) },
                    { "InvoiceStreet", (currentCustomer.InvoiceStreet, customerStreet) },
                    { "InvoiceHouseNo", (currentCustomer.InvoiceHouseNo, customersHouseNo) },
                    { "InvoiceCity", (currentCustomer.InvoiceCity, customersCity) },
                    { "InvoicePostcode", (currentCustomer.InvoicePostcode, customersPostCode) },
                    { "InvoiceCountry", (currentCustomer.InvoiceCountry, customersCountry) },
                    { "PurchaserName", (currentCustomer.PurchaserName, customersPurchaserName) },
                    { "PurchaserPhone", (currentCustomer.PurchaserPhone, customersPurchaserPhone) },
                    { "PurchaserMail", (currentCustomer.PurchaserMail, customersPurchaserMail) },
                    { "BookkeeperName", (currentCustomer.BookkeeperName, customersBookkeeperName) },
                    { "BookkeeperPhone", (currentCustomer.BookkeeperPhone, customersBookkeeperPhone) },
                    { "BookkeeperMail", (currentCustomer.BookkeeperMail, customersBookkeeperMail) },
                    { "CertificateMail", (currentCustomer.CertificateMail, customersCertificateMail) },
                    { "InvoiceMail", (currentCustomer.InvoiceMail, customersInvoiceMail) },
                    { "OCMail", (currentCustomer.OCMail, customersOCMail) },
                    { "Notes", (currentCustomer.Notes, customersNotes) },
                    { "Skonto", (currentCustomer.Skonto, customerSkonto) },
                    { "SkontoTerm", (currentCustomer.SkontoTerm, customerSkontoTerm) },
                    { "NettoTerm", (currentCustomer.NettoTerm, customersNettoTerm) },
                    { "InsuranceLimit", (currentCustomer.InsuranceLimit, customersInsurance) }
                };
                
                // Add changed fields to the updates dictionary
                foreach (var field in fieldMappings)
                {
                    var currentVal = field.Value.currentValue;
                    var newVal = field.Value.newValue;
    
                    // Convert both values to string, replace commas with dots, and trim spaces
                    string currentValStr = currentVal?.ToString().Replace(",", ".").Trim() ?? "";
                    string newValStr = newVal?.ToString().Replace(",", ".").Trim() ?? "";
    
                    // Compare the processed strings
                    if (!currentValStr.Equals(newValStr))
                    {
                        // Add the original newVal to updates (not the processed string)
                        updates.Add(field.Key, field.Value.newValue);
                    }
                }
                
                // If no changes were detected, inform the user and exit
                if (updates.Count == 0)
                {
                    var noChangesBox = MessageBoxManager.GetMessageBoxStandard(
                        "Information", 
                        "Es wurden keine Änderungen festgestellt.", 
                        ButtonEnum.Ok
                    );
                    await noChangesBox.ShowAsync();
                    return;
                }
                
                // Build the SQL query with only the changed fields
                var setClause = string.Join(", ", updates.Keys.Select(key => $"{key} = @{key}"));
                string sql = $"UPDATE Customers SET {setClause} WHERE Customernumber = @Customernumber";
                
                // Create parameters with only the changed fields plus the customernumber
                var parameters = new DynamicParameters(updates);
                parameters.Add("@Customernumber", customersNumber);
                
                // Create a copy of the SQL for logging purposes
                string logSql = sql;

                // Replace each parameter with its value for logging
                foreach (var param in updates)
                {
                    logSql = logSql.Replace($"@{param.Key}", param.Value?.ToString() ?? "NULL");
                }

                // Replace the Customernumber parameter
                logSql = logSql.Replace("@Customernumber", customersNumber);

                Console.WriteLine(logSql);                
                // Execute the update
                int rowsAffected = await connection.ExecuteAsync(sql, parameters);
                
                // Show result message
                if (rowsAffected > 0)
                {
                    var ResultQuery = MessageBoxManager.GetMessageBoxStandard(
                        "Erfolgreich",
                        $"Kunde: {customersName} wurde bearbeitet! ({updates.Count} Felder aktualisiert)",
                        ButtonEnum.Ok
                    );
                    await ResultQuery.ShowAsync();
                }
                else
                {
                    var ResultQuery = MessageBoxManager.GetMessageBoxStandard(
                        "Fehler!",
                        $"Kunde: {customersName} wurde nicht bearbeitet!",
                        ButtonEnum.Ok
                    );
                    await ResultQuery.ShowAsync();
                }
            }
        }
    }
    
    string ConnectionString = PS3000.Properties.Resources.ConnectionString;
}

