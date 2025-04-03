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

namespace PS3000.Controls;

public class SurchargeItem
{
    public string SurchargeType { get; set; }
    public string Description { get; set; }
    public string Amount { get; set; }
}

public partial class InquiriesViewModel : ObservableObject
{
    string ConnectionString = PS3000.Properties.Resources.ConnectionString;
    
    //Following is to search the Company Details
    [ObservableProperty] 
    private string customerNameSearch;
    
    public ObservableCollection<string> CustomersList { get; private set; } = new ObservableCollection<string>();
    
    public ObservableCollection<SurchargeItem> SurchargeList { get; set; } = new ObservableCollection<SurchargeItem>();

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
    string customersPurchaserName;
    
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
        if (customer == null)
        {
            return;
        }    
        else if (customer != "")    
        {
            CustomersPurchaserName = customer.PurchaserName;
            CustomerStreet = customer.InvoiceStreet;
            CustomersHouseNo = customer.InvoiceHouseNo;
            CustomersCity = customer.InvoiceCity;
            CustomersPostCode = customer.InvoicePostcode;
            CustomersCountry = customer.InvoiceCountry;
        }
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

        // If delivery address exists, assign all properties
        if (deliveryAddress != null)
        {
            CustomersDeliveryAdressStreet = deliveryAddress.Street;
            CustomersDeliveryAdressHouseNo = deliveryAddress.HouseNo;
            CustomersDeliveryAdressPostCode = deliveryAddress.Postcode;
            CustomersDeliveryAdressCity = deliveryAddress.City;
            CustomersDeliveryAdressCountry = deliveryAddress.Country;
            CustomersDeliveryAdressContactName = deliveryAddress.ContactName;
        }
    }

    private bool _deliveryEXW;
    public bool DeliveryEXW
    {
        get => _deliveryEXW;
        set
        {
            if (_deliveryEXW != value)
            {
                _deliveryEXW = value;
                OnPropertyChanged(nameof(DeliveryEXW));

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
                    DeliveryEXW = true;
                }
                else
                {
                    DeliveryEXW = false;
                }
                
                if (inquiry.English == 0)
                {
                    EnglishCustomer = true;
                }
                else
                {
                    EnglishCustomer = false;
                }
                
                DeliveryAdressSearch = inquiry.DeliveryCompanyName;
                CustomerNameSearch = inquiry.CustomerCompanyName;

                SurchargeList.Clear();
                
                Console.WriteLine(value);

                
                // Query and load surcharges
                var results = await connection.QueryAsync<SurchargeItem>(
                    @"SELECT SurchargeType, Description, Amount FROM inquirysurcharges WHERE Anfragennummer = @InquiryNo",
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

    }
    
    partial void OnInquiryNumberChanged(string value)
    {
        SearchInquiryNumber(value);
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