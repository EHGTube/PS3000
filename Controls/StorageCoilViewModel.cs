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

namespace PS3000.Controls
{
    public partial class StorageCoilViewModel : ObservableObject
    {
        [ObservableProperty] 
        private string message = "Click a button!";
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Toggle2))]
        [NotifyCanExecuteChangedFor(nameof(MethodTwoCommand))]
        [NotifyCanExecuteChangedFor(nameof(MethodFourCommand))]
        private bool toggle;
        
        public bool Toggle2 => !Toggle;
        
        [RelayCommand]
        
        private void MethodOne()
        {
            this.Message = "MethodOne Called";
            this.Toggle = !this.Toggle;
        }
        
        [RelayCommand(CanExecute = nameof(Toggle))]
        
        private void MethodTwo(string parameter)
        {
            this.Message = $"MethodTwo Called: {parameter}";
            this.toggle = !this.toggle;
        }
        
        [RelayCommand]
        
        private async Task MethodThree()
        {
            this.Message = "MethodThree Called";
            this.toggle = !this.toggle;
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
        
        [RelayCommand(CanExecute = nameof(Toggle2))]
        
        private async Task MethodFour(string parameter)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Bestätigung",
                    $"MethodFour Called: {parameter}",
                    ButtonEnum.YesNo);
            
            var result = await box.ShowAsync();
            
            this.toggle = !this.toggle;
        }
        
        
                
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CoilLength))]
        public float coilWeight;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CoilLength))]
        public float coilWidth;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CoilLength))]
        public float coilWT;
        
        public int CoilLength => (int)Math.Round(coilWeight / coilWidth / coilWT / 7.97f * 1000, 0);
        
        
        
        string ConnectionString = PS3000.Properties.Resources.ConnectionString;
        
        [ObservableProperty]
        private ObservableCollection<string> coilSuppliers = new();
        [ObservableProperty]
        private ObservableCollection<string> coilGrades = new();
        [ObservableProperty]
        private ObservableCollection<string> coilWTGroups = new();
        [ObservableProperty]
        private ObservableCollection<string> coilWTStatus = new();
        [ObservableProperty]
        private ObservableCollection<string> coilExec = new();

        public StorageCoilViewModel()
        {
            LoadDataAsync();
        }

        [RelayCommand]
        private async Task LoadDataAsync()
        {
            try
            {
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();

                string query = "SELECT * FROM lieferanten;";
                using var command = new MySqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

                CoilSuppliers.Clear();
                while (await reader.ReadAsync())
                {
                    CoilSuppliers.Add(reader["Name"].ToString());

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            
            try
            {
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();

                string query = "SELECT * FROM werkstoffe;";
                using var command = new MySqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

                CoilGrades.Clear();
                while (await reader.ReadAsync())
                {
                    CoilGrades.Add(reader["Werkstoff"].ToString());

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            
            try
            {
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();

                string query = "SELECT * FROM coilswsgruppen;";
                using var command = new MySqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

                CoilWTGroups.Clear();
                while (await reader.ReadAsync())
                {
                    CoilWTGroups.Add(reader["WSGruppe"].ToString());

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            
            try
            {
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();

                string query = "SELECT * FROM coilstatus;";
                using var command = new MySqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

                coilWTStatus.Clear();
                while (await reader.ReadAsync())
                {
                    coilWTStatus.Add(reader["Beschreibung"].ToString());

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            
            
            try
            {
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();

                string query = "SELECT * FROM coilausfuhrung;";
                using var command = new MySqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

                coilExec.Clear();
                while (await reader.ReadAsync())
                {
                    coilExec.Add(reader["Beschreibung"].ToString());

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }        
        
        
        [ObservableProperty]
        private ObservableCollection<CoilAttribute> coilSearchAttributes = new();
        
        [ObservableProperty]
        private string selectedWallThickness;
        [ObservableProperty]
        private string selectedGrade;
        [ObservableProperty]
        private string selectedSupplier;
        [ObservableProperty]
        private string selectedStatus;

                
        [ObservableProperty]
        int coilWTGroup;
        [ObservableProperty]
        string coilSetWT;
        [ObservableProperty]
        int coilGrade;
        [ObservableProperty]
        DateTime? coilPurchaseDate = DateTime.Today;
        [ObservableProperty]
        string coilCharge;
        [ObservableProperty]
        int coilSuppliersSelected;
        [ObservableProperty]
        int coilExecSelected;
        [ObservableProperty]
        int coilWTStatusSelected;
        [ObservableProperty]
        string coilPrice;
        
        [RelayCommand]
        private async Task LoadCoilsInStorageAsync()        
        {
            CoilSearchAttributes.Clear();
            
            string GradeQuery = "";
            string SupplierQuery = "";
            string WTQuery = ""; 
            string Addon = "";
            string StatusQuery = "";
            
                
            if (!String.IsNullOrEmpty(SelectedWallThickness) || !String.IsNullOrEmpty(selectedGrade) || !String.IsNullOrEmpty(selectedSupplier) || !String.IsNullOrEmpty(selectedStatus))
            {
                if (!String.IsNullOrEmpty(SelectedWallThickness))
                {
                    WTQuery = $@"WSGruppe = '{SelectedWallThickness.Replace(',','.')}'";
                }
            
                if (!String.IsNullOrEmpty(selectedGrade))
                {
                    if (!String.IsNullOrEmpty(SelectedWallThickness))
                    {
                        GradeQuery = $@" AND Werkstoff = '{selectedGrade}'";
                    }
                    else
                    {
                        GradeQuery = $@"Werkstoff = '{selectedGrade}'";
                    }
                }
            
                if (!String.IsNullOrEmpty(selectedSupplier))
                {
                    string SupplierCode = await GetFieldValueAsync("LaufendeLieferantennummer", selectedSupplier, "lieferanten",
                        "Name");
                    
                    if (!String.IsNullOrEmpty(SelectedWallThickness) || !String.IsNullOrEmpty(selectedGrade))
                    {
                        SupplierQuery = $@" AND Lieferant = '{SupplierCode}'";
                    }
                    else
                    {
                        SupplierQuery = $@"Lieferant = '{SupplierCode}'";
                    }
                }
            
                if (!String.IsNullOrEmpty(selectedStatus))
                {
                    string StatusCode = await GetFieldValueAsync("Position", selectedStatus, "coilstatus",
                        "Beschreibung");
                    if (!String.IsNullOrEmpty(selectedSupplier) || !String.IsNullOrEmpty(selectedGrade) || !String.IsNullOrEmpty(SelectedWallThickness))
                    {
                        StatusQuery = $@" AND Status = '{StatusCode}' OR Status IS NULL";
                    }
                    else
                    {
                        StatusQuery = $@"Status = '{StatusCode}'";
                    }
                }
            
                // Build and execute the query
                Addon = $@"WHERE {WTQuery}{GradeQuery}{SupplierQuery}{StatusQuery}";
            }

            string query = "SELECT * FROM lagercoils " + Addon;
            
            Console.WriteLine(query);

            try
            {
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();

                using var command = new MySqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var attribute = new CoilAttribute
                    {
                        LaufendeCoilnummer = int.Parse(reader[0]?.ToString() ?? "0"),
                        Status = await GetFieldValueAsync("Beschreibung", reader[1].ToString(), "coilstatus", "Position"),
                        WSGruppe = float.Parse(reader[2]?.ToString() ?? "0"),
                        Charge = reader[3]?.ToString() ?? string.Empty,
                        Wandstärke = float.Parse(reader[4]?.ToString() ?? "0"),
                        Besäumt = reader[5].ToString() == "1" ? "Ja" : "Nein",
                        Werkstoff = await GetFieldValueAsync("Werkstoff", reader[6].ToString(), "werkstoffe", "LaufendeWerkstoffnummer"),
                        Kaufdatum = DateTime.TryParse(reader[7]?.ToString(), out var kaufDatum) 
                            ? kaufDatum.ToString("yyyy-MM-dd") 
                            : string.Empty,
                        Lieferant = await GetFieldValueAsync("Name", reader[8].ToString(), "lieferanten", "LaufendeLieferantennummer"),
                        Ausführung = await GetFieldValueAsync("Beschreibung", reader[9].ToString(), "coilausfuhrung", "Position"),
                        Breite = int.Parse(reader[10]?.ToString() ?? "0"),
                        Gewicht = int.Parse(reader[11]?.ToString() ?? "0"),
                        Länge = int.Parse(reader[12]?.ToString() ?? "0"),
                        Preis = float.Parse(reader[13]?.ToString() ?? "0"),
                        Notizen = reader[14]?.ToString() ?? string.Empty,
                    };

                    CoilSearchAttributes.Add(attribute);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading coils: {ex.Message}");
            }
         }

        
        [ObservableProperty]
        private CoilAttribute coilSearchSelectedItem;
        
        [RelayCommand]        
        private async Task SaveCoil()
        {
            if (CoilSearchSelectedItem != null)
            {
                
                Console.WriteLine("Start");
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Bestätigung",
                        $"Selection: \n" +
                        $"LaufendeCoilnummer: {coilSearchSelectedItem.LaufendeCoilnummer.ToString()}\n" +
                        $"Soll WS: {coilSetWT}\n"+
                        $"Ist WS: {CoilWT}\n"+
                        $"Werkstoff: {await GetFieldValueAsync("Werkstoff",
                            CoilGrade.ToString(),
                            "werkstoffe", "LaufendeWerkstoffnummer")}\n"+
                        $"Kaufdatum: {CoilPurchaseDate}\n"+
                        $"Charge: {CoilCharge}\n"+
                        $"Lieferant: {await GetFieldValueAsync("Name",
                            CoilSuppliersSelected.ToString(),
                            "lieferanten", "LaufendeLieferantennummer")}\n"+
                        $"Ausführung: {await GetFieldValueAsync("Beschreibung",
                            CoilExecSelected.ToString(),
                            "coilausfuhrung", "Position")}\n"+
                        $"Status: {await GetFieldValueAsync("Beschreibung",
                            CoilWTStatusSelected.ToString(),
                            "coilstatus", "Position")}\n"+
                        $"Breite: {CoilWidth}\n"+
                        $"Gewicht: {CoilWeight}\n" +
                        $"Länge: {CoilLength}\n"+
                        $"Preis €/kg: {CoilPrice}\n",
                        ButtonEnum.YesNo);
            
                var result = await box.ShowAsync();
                Console.WriteLine("END");

                
            }
            else
            {
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Bestätigung",
                        "Nothing Selected!",
                        ButtonEnum.Ok);
            
                var result = await box.ShowAsync();
            }
        }
        
        [RelayCommand]
        private async Task UpdateCoilDetails()
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Bestätigung",
                    "Selection Changed!",
                    ButtonEnum.Ok);
            
            var result = await box.ShowAsync();
        }
        
        
        [RelayCommand]
        private async Task LoadCoilDetails()
        {
            var connectionString = "Server=127.0.0.1;Port=3306;Database=prostahl;Uid=root;Pwd=1234;Initial Catalog=DapperDB;Integrated Security=true;TrustServerCertificate=True";
            
            
            var connection = new SqlConnection(connectionString);

            CoilWTGroup = int.Parse(await GetFieldValueAsync("Position", (await GetFieldValueAsync("WSGruppe",
                        coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                        "lagercoils", "LaufendeCoilnummer")).Replace(',', '.').TrimEnd('0').TrimEnd('.'),
                    "coilswsgruppen", "WSGruppe"));
            
            CoilGrade = int.Parse(await GetFieldValueAsync("Werkstoff",
                coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                "lagercoils", "LaufendeCoilnummer"))-1;
            
            var sql = $"SELECT `WSGruppe` FROM `prostahl`.`lagercoils` WHERE `LaufendeCoilnummer` LIKE {coilSearchSelectedItem.LaufendeCoilnummer.ToString()} ORDER BY `LaufendeCoilnummer` ASC LIMIT 1";
            var product = connection.QuerySingle(sql).ToList();

            CoilSetWT = product.WSGruppe;

            // CoilSetWT = await GetFieldValueAsync("WSGruppe",
            //     coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
            //     "lagercoils", "LaufendeCoilnummer");

            CoilWT = float.Parse(await GetFieldValueAsync("Wandstarke",
                coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                "lagercoils", "LaufendeCoilnummer"));

             CoilPurchaseDate = DateTime.Parse(await GetFieldValueAsync("Kaufdatum",
                 coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                 "lagercoils", "LaufendeCoilnummer"));

            CoilCharge = await GetFieldValueAsync("Charge",
                coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                "lagercoils", "LaufendeCoilnummer");

            CoilSuppliersSelected = int.Parse(await GetFieldValueAsync("Lieferant",
                coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                "lagercoils", "LaufendeCoilnummer"))-1;

            CoilExecSelected = int.Parse(await GetFieldValueAsync("Ausfuhrung",
                coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                "lagercoils", "LaufendeCoilnummer"));
            
            CoilWTStatusSelected = int.Parse(await GetFieldValueAsync("Status",
                coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                "lagercoils", "LaufendeCoilnummer"));
            
            CoilWidth = float.Parse(await GetFieldValueAsync("Breite",
                coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                "lagercoils", "LaufendeCoilnummer"));
            
            CoilWeight = float.Parse(await GetFieldValueAsync("Gewicht",
                coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                "lagercoils", "LaufendeCoilnummer"));

             CoilPrice = await GetFieldValueAsync("Preis",
                 coilSearchSelectedItem.LaufendeCoilnummer.ToString(),
                 "lagercoils", "LaufendeCoilnummer");
        }
        
        
        
        //private void btnCoilsNewCoilSave_Click(object sender, EventArgs e)
        //{
        //    if (ListCoils.SelectedItems.Count > 0)
        //    {
        //        //Update existing Coil
        //        string laufendeCoilnummer = ListCoils.SelectedItems[0].SubItems[0].Text;

        //        string tablerow = "Status";
        //        string txtfield = cmbCoilsNewCoilStatus.SelectedIndex.ToString();

        //        if (txtfield == "-1")
        //        {
        //            txtfield = "0";
        //        }

        //        string updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "WSGruppe";
        //        txtfield = cmbCoilsNewCoilWTGroup.SelectedItem.ToString();
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Wandstarke";
        //        txtfield = txtCoilsNewCoilSetWT.Text.Replace(',', '.');
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Besaumt";
        //        if (chkCoilsNewCoilCut.Checked)
        //        {
        //            txtfield = "1";
        //        }
        //        else
        //        {
        //            txtfield = "0";
        //        }
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Werkstoff";
        //        txtfield = cmbCoilsNewCoilMaterial.SelectedIndex.ToString();
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Kaufdatum";
        //        txtfield = dateCoilsNewCoilPurchase.Value.ToString("yyyy-MM-dd");
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Lieferant";
        //        txtfield = cmbCoilsNewCoilSupplier.SelectedIndex.ToString();
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Ausfuhrung";
        //        txtfield = cmbCoilsNewCoilVariant.SelectedIndex.ToString();
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Breite";
        //        txtfield = txtCoilsNewCoilWidth.Text;
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Gewicht";
        //        txtfield = txtCoilsNewCoilWeight.Text;
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Lange";
        //        txtfield = txtCoilsNewCoilLength.Text;
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Preis";
        //        txtfield = txtCoilsNewCoilPrice.Text.Replace(',', '.');
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Notizen";
        //        txtfield = txtCoilsNewCoilText.Text;
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        tablerow = "Charge";
        //        txtfield = txtCoilsNewCoilHeat.Text;
        //        updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //    }
        //    else //Create a new Coil
        //    {
        //        string Besäumt;
        //        string BesäumtValue;
        //        if (chkCoilsNewCoilCut.Checked)
        //        {
        //            Besäumt = "Ja";
        //            BesäumtValue = "1";
        //        }
        //        else
        //        {
        //            Besäumt = "Nein";
        //            BesäumtValue = "0";
        //        }

        //        // Display a MessageBox with two buttons
        //        DialogResult result = MessageBox.Show(
        //            $"WS Gruppe: {cmbCoilsNewCoilWTGroup.SelectedItem.ToString()} {Environment.NewLine}" +
        //            $"Wandstärke: {txtCoilsNewCoilSetWT.Text ?? ""} {Environment.NewLine}" +
        //            $"Besäumt?: {Besäumt} {Environment.NewLine}" +
        //            $"Werkstoff: {cmbCoilsNewCoilMaterial.SelectedItem.ToString()} {Environment.NewLine}" +
        //            $"Kaufdatum: {dateCoilsNewCoilPurchase.Value.ToString("yyyy-MM-dd")} {Environment.NewLine}" +
        //            $"Lieferant: {cmbCoilsNewCoilSupplier.SelectedItem.ToString()} {Environment.NewLine}" +
        //            $"Ausführung: {cmbCoilsNewCoilVariant.SelectedItem.ToString()} {Environment.NewLine}" +
        //            $"Breite: {txtCoilsNewCoilWidth.Text} {Environment.NewLine}" +
        //            $"Gewicht: {txtCoilsNewCoilWeight.Text ?? ""} {Environment.NewLine}" +
        //            $"Länge: {txtCoilsNewCoilLength.Text ?? ""} {Environment.NewLine}" +
        //            $"Status: {cmbCoilsNewCoilStatus.SelectedItem.ToString()} {Environment.NewLine}" +
        //            $"Preis €/kg: {txtCoilsNewCoilPrice.Text ?? ""} {Environment.NewLine}" +
        //            $"Notizen: {txtCoilsNewCoilText.Text ?? ""}",
        //            $"Charge: {txtCoilsNewCoilHeat.Text ?? ""} {Environment.NewLine}" +
        //            "Confirm Details", // Title of the message box
        //            MessageBoxButtons.YesNo // Buttons for the message box
        //        );

        //        if (result == DialogResult.Yes)
        //        {
        //            try
        //            {
        //                using (MySqlConnection connection = new MySqlConnection(connectionString))
        //                {
        //                    connection.Open();

        //                    string query = @"INSERT INTO `lagercoils` 
        //                            (`WSGruppe`, `Wandstarke`,`Besaumt`,`Werkstoff`,`Kaufdatum`,`Lieferant`,`Ausfuhrung`,`Breite`,`Gewicht`,`Lange`,`Preis`,`Notizen`,`Status`,`Charge`)
        //                            VALUES
        //                            (@WSGruppe, @Wandstärke, @Besäumt, @Werkstoff, @Kaufdatum, @Lieferant, @Ausführung, @Breite, @Gewicht, @Länge, @Preis, @Notizen, @Status, @Charge);";

        //                    MySqlCommand command = new MySqlCommand(query, connection);

        //                    // Add parameters
        //                    command.Parameters.AddWithValue("@WSGruppe", cmbCoilsNewCoilWTGroup.SelectedItem.ToString().Replace(',', '.'));
        //                    command.Parameters.AddWithValue("@Wandstärke", txtCoilsNewCoilSetWT.Text.Replace(',', '.'));
        //                    command.Parameters.AddWithValue("@Besäumt", BesäumtValue);
        //                    command.Parameters.AddWithValue("@Werkstoff", cmbCoilsNewCoilMaterial.SelectedIndex);
        //                    command.Parameters.AddWithValue("@Kaufdatum", dateCoilsNewCoilPurchase.Value.ToString("yyyy-MM-dd"));
        //                    command.Parameters.AddWithValue("@Lieferant", cmbCoilsNewCoilSupplier.SelectedIndex);
        //                    command.Parameters.AddWithValue("@Ausführung", cmbCoilsNewCoilVariant.SelectedIndex);
        //                    command.Parameters.AddWithValue("@Status", cmbCoilsNewCoilStatus.SelectedIndex);
        //                    command.Parameters.AddWithValue("@Breite", txtCoilsNewCoilWidth.Text);
        //                    command.Parameters.AddWithValue("@Gewicht", txtCoilsNewCoilWeight.Text);
        //                    command.Parameters.AddWithValue("@Länge", txtCoilsNewCoilLength.Text);
        //                    command.Parameters.AddWithValue("@Preis", txtCoilsNewCoilPrice.Text.Replace(',', '.'));
        //                    command.Parameters.AddWithValue("@Notizen", txtCoilsNewCoilText.Text);
        //                    command.Parameters.AddWithValue("@Charge", txtCoilsNewCoilHeat.Text);

        //                    // Execute the query
        //                    int rowsAffected = command.ExecuteNonQuery();

        //                    // Check if the insertion was successful
        //                    if (rowsAffected > 0)
        //                    {
        //                        MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Failed to insert data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //}

        //private void ListCoils_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ListCoils.SelectedItems.Count > 0)
        //    {
        //        string laufendeCoilnummer = ListCoils.SelectedItems[0].SubItems[0].Text;

        //        string WSGruppe = "";

        //        switch (ListCoils.SelectedItems[0].SubItems[2].Text)
        //        {
        //            case "0,80":
        //                WSGruppe = "0";
        //                break;
        //            case "1,00":
        //                WSGruppe = "1";
        //                break;
        //            case "1,20":
        //                WSGruppe = "2";
        //                break;
        //            case "1,50":
        //                WSGruppe = "3";
        //                break;
        //            case "2,00":
        //                WSGruppe = "4";
        //                break;
        //            case "2,50":
        //                WSGruppe = "5";
        //                break;
        //            case "3,00":
        //                WSGruppe = "6";
        //                break;
        //            case "3,50":
        //                WSGruppe = "7";
        //                break;
        //            case "4,00":
        //                WSGruppe = "8";
        //                break;
        //            case "4,50":
        //                WSGruppe = "9";
        //                break;
        //            case "5,00":
        //                WSGruppe = "10";
        //                break;
        //            case "6,00":
        //                WSGruppe = "11";
        //                break;
        //            case "6,30":
        //                WSGruppe = "12";
        //                break;
        //            case "7,11":
        //                WSGruppe = "13";
        //                break;
        //            default:
        //                WSGruppe = "Fehler";
        //                break;
        //        }
        //        cmbCoilsNewCoilWTGroup.SelectedIndex = int.Parse(WSGruppe);

        //        txtCoilsNewCoilSetWT.Text = GetFieldValue("Wandstarke", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer");

        //        if (int.Parse(GetFieldValue("Besaumt", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer")) == 1)
        //        {
        //            chkCoilsNewCoilCut.Checked = true;
        //        }

        //        cmbCoilsNewCoilMaterial.SelectedIndex = int.Parse(GetFieldValue("Werkstoff", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer")) - 1;
        //        string kaufdatumString = GetFieldValue("Kaufdatum", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer");

        //        // Parse the string to a DateTime object
        //        if (DateTime.TryParse(kaufdatumString, out DateTime kaufdatum))
        //        {
        //            dateCoilsNewCoilPurchase.Value = kaufdatum;
        //        }

        //        cmbCoilsNewCoilSupplier.SelectedIndex = int.Parse(GetFieldValue("Lieferant", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer"));
        //        cmbCoilsNewCoilVariant.SelectedIndex = int.Parse(GetFieldValue("Ausfuhrung", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer"));

        //        int.TryParse(GetFieldValue("Status", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer"), out int Status);


        //        cmbCoilsNewCoilStatus.SelectedIndex = Status;
        //        txtCoilsNewCoilWidth.Text = GetFieldValue("Breite", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer");
        //        txtCoilsNewCoilWeight.Text = GetFieldValue("Gewicht", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer");
        //        txtCoilsNewCoilLength.Text = GetFieldValue("Lange", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer");
        //        txtCoilsNewCoilPrice.Text = GetFieldValue("Preis", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer");
        //        txtCoilsNewCoilText.Text = GetFieldValue("Notizen", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer");
        //        txtCoilsNewCoilHeat.Text = GetFieldValue("Charge", laufendeCoilnummer, "Lagercoils", "LaufendeCoilnummer");

        //        //string status = ListCoils.SelectedItems[0].SubItems[1].Text;
        //        //string WSGruppe = ListCoils.SelectedItems[0].SubItems[2].Text;
        //        //string Wandstärke = ListCoils.SelectedItems[0].SubItems[3].Text;
        //        //string Besaumt = "";
        //        //if (ListCoils.SelectedItems[0].SubItems[4].Text == "Ja")
        //        //{
        //        //    Besaumt = "1";
        //        //}
        //        //else if (ListCoils.SelectedItems[0].SubItems[4].Text == "Nein")
        //        //{
        //        //    Besaumt = "0";
        //        //}
        //        //string Werkstoff = GetFieldValue("LaufendeWerkstoffnummer", ListCoils.SelectedItems[0].SubItems[5].Text, "werkstoffe", "Werkstoff");
        //        //string Kaufdatum = ListCoils.SelectedItems[0].SubItems[6].Text;
        //        //string Lieferant = GetFieldValue("LaufendeLieferantennummer", ListCoils.SelectedItems[0].SubItems[7].Text, "lieferanten", "Name");
        //        //string Ausführung = "";
        //        //switch (ListCoils.SelectedItems[0].SubItems[8].Text)
        //        //{
        //        //    case "1C":
        //        //        Ausführung = "0";
        //        //        break;
        //        //    case "1E":
        //        //        Ausführung = "1";
        //        //        break;
        //        //    case "1D":
        //        //        Ausführung = "2";
        //        //        break;
        //        //    case "2C":
        //        //        Ausführung = "3";
        //        //        break;
        //        //    case "2E":
        //        //        Ausführung = "4";
        //        //        break;
        //        //    case "2D":
        //        //        Ausführung = "5";
        //        //        break;
        //        //    case "2B":
        //        //        Ausführung = "6";
        //        //        break;
        //        //    case "2R":
        //        //        Ausführung = "7";
        //        //        break;
        //        //}
        //        //string breite = ListCoils.SelectedItems[0].SubItems[9].Text;
        //        //string gewicht = ListCoils.SelectedItems[0].SubItems[10].Text;
        //        //string länge = ListCoils.SelectedItems[0].SubItems[11].Text;
        //        //string preis = ListCoils.SelectedItems[0].SubItems[12].Text;
        //        //string notizen = ListCoils.SelectedItems[0].SubItems[13].Text;
        //    }
        //}


        // Following is to create Cutting Plans

        //private void btnSPPlanCreate_Click(object sender, EventArgs e)
        //{
        //    if (String.IsNullOrEmpty(txtProductionSlittingPlanNo.Text))
        //    {
        //        string query = "SELECT MAX(Priorität) AS highest_value FROM spaltplan";

        //        string priorität = "";

        //        using (MySqlConnection conn = new MySqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            using (MySqlCommand cmd = new MySqlCommand(query, conn))
        //            {
        //                object result = cmd.ExecuteScalar();
        //                // Check for null and handle conversion safely
        //                int maxValue = result != DBNull.Value ? Convert.ToInt32(result) : 0;
        //                priorität = (maxValue + 1).ToString();
        //            }
        //        }

        //        query = "INSERT INTO spaltplan (Coil, Status, AbschneidenNach, priorität, Notizen) VALUES (@Coil, @Status, @AbschneidenNach, @priorität, @Notizen); SELECT LAST_INSERT_ID();";

        //        string cutofflength = null;

        //        if (!String.IsNullOrEmpty(txtProductionSlittingPlanCoilCutOffLength.Text))
        //        {
        //            cutofflength = txtProductionSlittingPlanCoilCutOffLength.Text;
        //        }

        //        string SpaltPlanNo = null;

        //        // Create a connection to the database
        //        using (MySqlConnection connection = new MySqlConnection(connectionString))
        //        {
        //            try
        //            {
        //                // Open the connection
        //                connection.Open();

        //                // Create a MySqlCommand object
        //                using (MySqlCommand command = new MySqlCommand(query, connection))
        //                {
        //                    // Define the parameters and their values
        //                    command.Parameters.AddWithValue("@Coil", txtProductionSlittingPlanCoilNo.Text);
        //                    command.Parameters.AddWithValue("@Status", "1");
        //                    command.Parameters.AddWithValue("@AbschneidenNach", cutofflength);
        //                    command.Parameters.AddWithValue("@Priorität", priorität);
        //                    command.Parameters.AddWithValue("@Notizen", txtProductionSlittingPlanCoilNotes.Text);

        //                    // Execute the query and retrieve the ID of the inserted row
        //                    object insertedId = command.ExecuteScalar();
        //                    SpaltPlanNo = insertedId.ToString();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show($"Error: {ex.Message}");
        //            }
        //        }

        //        foreach (DataGridViewRow row in dataGridProductionSlittingPlanPositions.Rows)
        //        {
        //            string AnzahlBänder = row.Cells[0].Value.ToString();
        //            string BandBreite = row.Cells[1].Value.ToString();
        //            string Abmessung = row.Cells[2].Value.ToString();
        //            string KG = row.Cells[3].Value.ToString();
        //            string Meter = row.Cells[4].Value.ToString();
        //            string Lagerort = row.Cells[5].Value.ToString();

        //            query = "INSERT INTO spaltplanpositionen (Spaltplan, Position, Bänderanzahl, Breite, ADxWS, TheoretischeKG, SollLänge, Lagerort) VALUES (@Spaltplan, @Position, @Bänderanzahl, @Breite, @ADxWS, @TheoretischeKG, @SollLänge, @Lagerort);";

        //            // Create a connection to the database
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                try
        //                {
        //                    // Open the connection
        //                    connection.Open();

        //                    // Create a MySqlCommand object
        //                    using (MySqlCommand command = new MySqlCommand(query, connection))
        //                    {
        //                        // Define the parameters and their values
        //                        command.Parameters.AddWithValue("@Spaltplan", SpaltPlanNo);
        //                        command.Parameters.AddWithValue("@Position", row.Index.ToString());
        //                        command.Parameters.AddWithValue("@Bänderanzahl", AnzahlBänder);
        //                        command.Parameters.AddWithValue("@Breite", BandBreite);
        //                        command.Parameters.AddWithValue("@ADxWS", Abmessung);
        //                        command.Parameters.AddWithValue("@TheoretischeKG", KG);
        //                        command.Parameters.AddWithValue("@SollLänge", Meter);
        //                        command.Parameters.AddWithValue("@Lagerort", Lagerort);

        //                        command.ExecuteScalar();
        //                        // Execute the query and retrieve the ID of the inserted row
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show($"Error: {ex.Message}");
        //                }
        //            }
        //        }
        //    }
        //    else if (!String.IsNullOrEmpty(GetFieldValue("Coil", txtProductionSlittingPlanNo.Text, "spaltplan", "Nummer")))
        //    {
        //        string row = "ADTolMin";
        //        string textbox = txtOrderPositionODTolMin.Text.Replace(',', '.');
        //        string updateQuery = $"UPDATE aufträgepositionen SET {row} = '{textbox}' WHERE LaufendePositionsnummer = '{txtProductionSlittingPlanNo.Text}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }

        //        row = "ADTolMin";
        //        textbox = txtOrderPositionODTolMin.Text.Replace(',', '.');
        //        updateQuery = $"UPDATE aufträgepositionen SET {row} = '{textbox}' WHERE LaufendePositionsnummer = '{txtProductionSlittingPlanNo.Text}'";

        //        try
        //        {
        //            using (MySqlConnection connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                MySqlCommand command = new MySqlCommand(updateQuery, connection);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }
        //    }
        //}

        //private void txtProductionSlittingPlanCoilNo_TextChanged(object sender, EventArgs e)
        //{
        //    string Coilnummer = txtProductionSlittingPlanCoilNo.Text;

        //    txtProductionSlittingPlanCoilWeight.Text = GetFieldValue("Gewicht", Coilnummer, "Lagercoils", "LaufendeCoilnummer");
        //    txtProductionSlittingPlanCoilHeat.Text = GetFieldValue("Charge", Coilnummer, "Lagercoils", "LaufendeCoilnummer");
        //    txtProductionSlittingPlanCoilExec.Text = GetFieldValue("Ausfuhrung", Coilnummer, "Lagercoils", "LaufendeCoilnummer");
        //    txtProductionSlittingPlanCoilGrade.Text = GetFieldValue("Werkstoff", Coilnummer, "Lagercoils", "LaufendeCoilnummer");
        //    txtProductionSlittingPlanCoilThickness.Text = GetFieldValue("Wandstarke", Coilnummer, "Lagercoils", "LaufendeCoilnummer");
        //    txtProductionSlittingPlanCoilWidth.Text = GetFieldValue("Breite", Coilnummer, "Lagercoils", "LaufendeCoilnummer");
        //    txtProductionSlittingPlanCoilLength.Text = GetFieldValue("Lange", Coilnummer, "Lagercoils", "LaufendeCoilnummer");
        //}

        //private void dataGridProductionSlittingPlan_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    //MessageBox.Show(dataGridProductionSlittingPlan.Rows.Count.ToString());
        //    //MessageBox.Show(dataGridProductionSlittingPlan.Rows[0].Cells[0].Value.ToString());

        //    double TotalWidth = 0;

        //    for (int i = 0; i < dataGridProductionSlittingPlanPositions.Rows.Count; i++)
        //    {
        //        if (dataGridProductionSlittingPlanPositions.Rows[i].Cells[0].Value != null &&
        //            dataGridProductionSlittingPlanPositions.Rows[i].Cells[1].Value != null &&
        //            !string.IsNullOrEmpty(dataGridProductionSlittingPlanPositions.Rows[i].Cells[0].Value.ToString()) &&
        //            !string.IsNullOrEmpty(dataGridProductionSlittingPlanPositions.Rows[i].Cells[1].Value.ToString()))
        //        {
        //            if (double.TryParse(txtProductionSlittingPlanCoilWeight.Text, out double B11) &&
        //                double.TryParse(txtProductionSlittingPlanCoilWidth.Text, out double I6) &&
        //                double.TryParse(dataGridProductionSlittingPlanPositions.Rows[i].Cells[1].Value?.ToString(), out double B15) &&
        //                double.TryParse(dataGridProductionSlittingPlanPositions.Rows[i].Cells[0].Value?.ToString(), out double A15) &&
        //                double.TryParse(txtProductionSlittingPlanCoilLength.Text, out double K11) &&
        //                double.TryParse(txtProductionSlittingPlanCoilThickness.Text, out double K6))
        //            {
        //                // Pre-compute repeated expressions to avoid redundancy
        //                double divisionResult = B11 / I6;
        //                double innerDivisionResult = divisionResult / K6 / 7.97;

        //                // Calculation
        //                double Length = divisionResult * B15 * A15 * K11 / (innerDivisionResult * 1000);

        //                if (!string.IsNullOrEmpty(txtProductionSlittingPlanCoilCutOffLength.Text) && Length > double.Parse(txtProductionSlittingPlanCoilCutOffLength.Text))
        //                {
        //                    Length = double.Parse(txtProductionSlittingPlanCoilCutOffLength.Text);
        //                }

        //                // Output the Length
        //                dataGridProductionSlittingPlanPositions.Rows[i].Cells[3].Value = Length.ToString("F0");
        //                dataGridProductionSlittingPlanPositions.Rows[i].Cells[4].Value = (K11 * A15).ToString("F0");

        //                TotalWidth += A15 * B15;
        //            }
        //        }

        //    }

        //    txtProductionSlittingPlanCoilRest.Text = (double.Parse(txtProductionSlittingPlanCoilWidth.Text) - TotalWidth).ToString();
        //}

        //private void btnSPPlanAddRow_Click(object sender, EventArgs e)
        //{
        //    dataGridProductionSlittingPlanPositions.Rows.Add();
        //}

        //private void txtProductionSlittingPlanCoilRest_TextChanged(object sender, EventArgs e)
        //{
        //    if (double.Parse(txtProductionSlittingPlanCoilRest.Text) < 0)
        //    {
        //        txtProductionSlittingPlanCoilRest.BackColor = System.Drawing.Color.Red;
        //    }
        //}

        //private void txtProductionSlittingPlanNo_TextChanged(object sender, EventArgs e)
        //{
        //    txtProductionSlittingPlanCoilNo.Text = GetFieldValue("Coil", txtProductionSlittingPlanNo.Text, "spaltplan", "Nummer");
        //    txtProductionSlittingPlanCoilCutOffLength.Text = GetFieldValue("AbschneidenNach", txtProductionSlittingPlanNo.Text, "spaltplan", "Nummer");
        //    txtProductionSlittingPlanCoilNotes.Text = GetFieldValue("Notizen", txtProductionSlittingPlanNo.Text, "spaltplan", "Nummer");


        //    dataGridProductionSlittingPlanPositions.Rows.Clear();

        //    // Fetch rows from the database
        //    List<string[]> rows = SlittingPlanPosGetRows(txtProductionSlittingPlanNo.Text);

        //    // Add each row to the ListView
        //    foreach (var row in rows)
        //    {
        //        dataGridProductionSlittingPlanPositions.Rows.Add(row);
        //    }
        //}

        //private List<string[]> SlittingPlanPosGetRows(string SpaltplanNo)
        //{
        //    var results = new List<string[]>();

        //    using (MySqlConnection conn = new MySqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = "SELECT Position, Bänderanzahl, Breite, ADxWS, TheoretischeKG, SollLänge, Lagerort FROM spaltplanpositionen WHERE Spaltplan = @SpaltplanNo ORDER BY Position ASC;";

        //        using (MySqlCommand cmd = new MySqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@SpaltplanNo", SpaltplanNo);

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    string Bandanzahl = reader[1].ToString();
        //                    string Breite = reader[2].ToString();
        //                    string ADxWS = reader[3].ToString();
        //                    string TheoretischeKG = reader[4].ToString();
        //                    string SollLänge = reader[5].ToString();
        //                    string Lagerort = reader[6].ToString();
        //                    results.Add(new string[] { Bandanzahl, Breite, ADxWS, TheoretischeKG, SollLänge, Lagerort });
        //                }
        //            }
        //        }
        //    }
        //    return results;
        //}

        ////listProductionSlittingPlanSchedule

        //private void listProductionSlittingPlanScheduleRefresh()
        //{
        //    listProductionSlittingPlanSchedule.Clear();
        //    listProductionSlittingPlanSchedule.Columns.Add("Spaltplannummer");
        //    listProductionSlittingPlanSchedule.Columns[0].Width = 110;
        //    try
        //    {
        //        using (MySqlConnection connection = new MySqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            string query = "SELECT `Nummer` FROM `spaltplan` WHERE `Status` = 1 ORDER BY `Priorität` ASC;";
        //            MySqlCommand command = new MySqlCommand(query, connection);

        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    listProductionSlittingPlanSchedule.Items.Add(reader[0].ToString());
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("An error occurred: " + ex.Message);
        //    }
        //}

        //private void listProductionSlittingPlanSchedule_ItemDrag(object sender, ItemDragEventArgs e)
        //{
        //    DoDragDrop(e.Item, DragDropEffects.Move);

        //    int Position = 1;

        //    try
        //    {
        //        using (MySqlConnection connection = new MySqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            for (var i = 0; i < listProductionSlittingPlanSchedule.Items.Count; i++)
        //            {
        //                string query = "UPDATE spaltplan SET Priorität = @Priorität WHERE Nummer = @primaryKey;";
        //                MySqlCommand command = new MySqlCommand(query, connection);
        //                command.Parameters.AddWithValue("@Priorität", i);
        //                command.Parameters.AddWithValue("@primaryKey", listProductionSlittingPlanSchedule.Items[i].Text); // Assuming primary key is stored in Tag
        //                command.ExecuteNonQuery();

        //                Position += 1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("An error occurred while saving the order: " + ex.Message);
        //    }

        //    listProductionSlittingPlanScheduleRefresh();
        //}

        //private void listProductionSlittingPlanSchedule_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(typeof(ListViewItem)))
        //    {
        //        e.Effect = DragDropEffects.Move;
        //    }
        //}

        //private void listProductionSlittingPlanSchedule_DragDrop(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(typeof(ListViewItem)))
        //    {
        //        Point point = listProductionSlittingPlanSchedule.PointToClient(new Point(e.X, e.Y));
        //        ListViewItem targetItem = listProductionSlittingPlanSchedule.GetItemAt(point.X, point.Y);
        //        ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

        //        if (targetItem != null && draggedItem != targetItem)
        //        {
        //            int targetIndex = targetItem.Index;
        //            listProductionSlittingPlanSchedule.Items.Remove(draggedItem);
        //            listProductionSlittingPlanSchedule.Items.Insert(targetIndex, draggedItem);
        //        }
        //    }
        //}

        //private void listProductionSlittingPlanSchedule_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (listProductionSlittingPlanSchedule.SelectedItems.Count > 0) // Ensure there is a selected item
        //    {
        //        txtProductionSlittingPlanNo.Text = listProductionSlittingPlanSchedule.SelectedItems[0].Text;
        //    }
        //    else
        //    {
        //        txtProductionSlittingPlanNo.Text = string.Empty; // Optional: Clear the text box if nothing is selected
        //    }
        //}

        //private void btnSPPlanRestAddRow_Click(object sender, EventArgs e)
        //{
        //    dataGridProductionSlittingPlanRest.Rows.Add();
        //}

        //private void btnSPPlanFinished_Click(object sender, EventArgs e)
        //{
        //    // Collect Position and Bänderanzahl data from the DataGridView
        //    string positionData = string.Join(Environment.NewLine,
        //        dataGridProductionSlittingPlanPositions.Rows
        //            .Cast<DataGridViewRow>()
        //            .Where(row => !row.IsNewRow) // Exclude the new row placeholder
        //            .Select(row =>
        //                $"{Environment.NewLine}Bänderanzahl Position {row.Cells[0].Value}: {row.Cells[0].Value}{Environment.NewLine}" +
        //                $"Bandbreite Position {row.Cells[0].Value}: {row.Cells[1].Value}{Environment.NewLine}" +
        //                $"ADxWS Position {row.Cells[0].Value}: {row.Cells[2].Value}{Environment.NewLine}" +
        //                $"Gewicht {row.Cells[0].Value}: {row.Cells[3].Value}{Environment.NewLine}" +
        //                $"Th. Meter {row.Cells[0].Value}: {row.Cells[4].Value}{Environment.NewLine}" +
        //                $"Lagerort {row.Cells[0].Value}: {row.Cells[5].Value}{Environment.NewLine}" +
        //                $"Länge {row.Cells[0].Value}: {row.Cells[5].Value}"));


        //    // Show the confirmation dialog
        //    DialogResult result = MessageBox.Show(
        //        $"Stimmen folgende Werte{Environment.NewLine}" +
        //        $"Spaltplannummer: {txtProductionSlittingPlanNo.Text}{Environment.NewLine}" +
        //        $"Coilnummer: {txtProductionSlittingPlanCoilNo.Text}{Environment.NewLine}" +
        //        $"{positionData}{Environment.NewLine}",
        //        "Confirm Delete",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Question);
        //}
        
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

    }
}
    
    public class CoilAttribute
    {
        public int LaufendeCoilnummer { get; set; }
        public string Status { get; set; }
        public float WSGruppe { get; set; }
        public string Charge { get; set; }
        public float Wandstärke { get; set; }
        public string Besäumt { get; set; }
        public string Werkstoff { get; set; }
        public string Kaufdatum { get; set; }
        public string Lieferant { get; set; }
        public string Ausführung { get; set; }
        public int Breite { get; set; }
        public int Gewicht { get; set; }
        public int Länge { get; set; }
        public float Preis { get; set; }
        public string Notizen { get; set; }
    }
