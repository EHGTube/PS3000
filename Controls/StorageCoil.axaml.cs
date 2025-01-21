using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace PS3000;

public partial class StorageCoil : UserControl
{
    string connectionString = PS3000.Properties.Resources.ConnectionString;

    public StorageCoil()
    {
        InitializeComponent();
    }


    private void txtCoilsNewCoilWidth_TextChanged(object sender, EventArgs e)
    {
        SetCoilLength();
    }

    private void txtCoilsNewCoilWeight_TextChanged(object sender, EventArgs e)
    {
        SetCoilLength();
    }

    private void txtCoilsNewCoilSetWT_TextChanged(object sender, EventArgs e)
    {
        SetCoilLength();
    }

    private void SetCoilLength()
    {
        float.TryParse(txtCoilsNewCoilWeight.Text, out float weight);
        float.TryParse(txtCoilsNewCoilWidth.Text, out float width);
        float.TryParse(txtCoilsNewCoilSetWT.Text, out float WT);
        float formula = (weight / width / WT / 7.97f * 1000);

        // Round the Length to 2 decimal places

        txtCoilsNewCoilLength.Text = Math.Round(formula, 0).ToString();
    }

    private void btnCoilsNewCoilSave_Click(object sender, EventArgs e)
    {
        if (ListCoils.SelectedItems.Count > 0)
        {
            //Update existing Coil
            string laufendeCoilnummer = ListCoils.SelectedItems[0].SubItems[0].Text;

            string tablerow = "Status";
            string txtfield = cmbCoilsNewCoilStatus.SelectedIndex.ToString();

            if (txtfield == "-1")
            {
                txtfield = "0";
            }

            string updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "WSGruppe";
            txtfield = cmbCoilsNewCoilWTGroup.SelectedItem.ToString();
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Wandstarke";
            txtfield = txtCoilsNewCoilSetWT.Text.Replace(',', '.');
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Besaumt";
            if (chkCoilsNewCoilCut.Checked)
            {
                txtfield = "1";
            }
            else
            {
                txtfield = "0";
            }
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Werkstoff";
            txtfield = cmbCoilsNewCoilMaterial.SelectedIndex.ToString();
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Kaufdatum";
            txtfield = dateCoilsNewCoilPurchase.Value.ToString("yyyy-MM-dd");
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Lieferant";
            txtfield = cmbCoilsNewCoilSupplier.SelectedIndex.ToString();
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Ausfuhrung";
            txtfield = cmbCoilsNewCoilVariant.SelectedIndex.ToString();
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Breite";
            txtfield = txtCoilsNewCoilWidth.Text;
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Gewicht";
            txtfield = txtCoilsNewCoilWeight.Text;
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Lange";
            txtfield = txtCoilsNewCoilLength.Text;
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Preis";
            txtfield = txtCoilsNewCoilPrice.Text.Replace(',', '.');
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Notizen";
            txtfield = txtCoilsNewCoilText.Text;
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            tablerow = "Charge";
            txtfield = txtCoilsNewCoilHeat.Text;
            updateQuery = $"UPDATE lagercoils SET {tablerow} = '{txtfield}' WHERE LaufendeCoilnummer = '{laufendeCoilnummer}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        else //Create a new Coil
        {
            string Besäumt;
            string BesäumtValue;
            if (chkCoilsNewCoilCut.Checked)
            {
                Besäumt = "Ja";
                BesäumtValue = "1";
            }
            else
            {
                Besäumt = "Nein";
                BesäumtValue = "0";
            }

            // Display a MessageBox with two buttons
            DialogResult result = MessageBox.Show(
                $"WS Gruppe: {cmbCoilsNewCoilWTGroup.SelectedItem.ToString()} {Environment.NewLine}" +
                $"Wandstärke: {txtCoilsNewCoilSetWT.Text ?? ""} {Environment.NewLine}" +
                $"Besäumt?: {Besäumt} {Environment.NewLine}" +
                $"Werkstoff: {cmbCoilsNewCoilMaterial.SelectedItem.ToString()} {Environment.NewLine}" +
                $"Kaufdatum: {dateCoilsNewCoilPurchase.Value.ToString("yyyy-MM-dd")} {Environment.NewLine}" +
                $"Lieferant: {cmbCoilsNewCoilSupplier.SelectedItem.ToString()} {Environment.NewLine}" +
                $"Ausführung: {cmbCoilsNewCoilVariant.SelectedItem.ToString()} {Environment.NewLine}" +
                $"Breite: {txtCoilsNewCoilWidth.Text} {Environment.NewLine}" +
                $"Gewicht: {txtCoilsNewCoilWeight.Text ?? ""} {Environment.NewLine}" +
                $"Länge: {txtCoilsNewCoilLength.Text ?? ""} {Environment.NewLine}" +
                $"Status: {cmbCoilsNewCoilStatus.SelectedItem.ToString()} {Environment.NewLine}" +
                $"Preis €/kg: {txtCoilsNewCoilPrice.Text ?? ""} {Environment.NewLine}" +
                $"Notizen: {txtCoilsNewCoilText.Text ?? ""}",
                $"Charge: {txtCoilsNewCoilHeat.Text ?? ""} {Environment.NewLine}" +
                "Confirm Details", // Title of the message box
                MessageBoxButtons.YesNo // Buttons for the message box
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = @"INSERT INTO `lagercoils` 
                                (`WSGruppe`, `Wandstarke`,`Besaumt`,`Werkstoff`,`Kaufdatum`,`Lieferant`,`Ausfuhrung`,`Breite`,`Gewicht`,`Lange`,`Preis`,`Notizen`,`Status`,`Charge`)
                                VALUES
                                (@WSGruppe, @Wandstärke, @Besäumt, @Werkstoff, @Kaufdatum, @Lieferant, @Ausführung, @Breite, @Gewicht, @Länge, @Preis, @Notizen, @Status, @Charge);";

                        MySqlCommand command = new MySqlCommand(query, connection);

                        // Add parameters
                        command.Parameters.AddWithValue("@WSGruppe", cmbCoilsNewCoilWTGroup.SelectedItem.ToString().Replace(',', '.'));
                        command.Parameters.AddWithValue("@Wandstärke", txtCoilsNewCoilSetWT.Text.Replace(',', '.'));
                        command.Parameters.AddWithValue("@Besäumt", BesäumtValue);
                        command.Parameters.AddWithValue("@Werkstoff", cmbCoilsNewCoilMaterial.SelectedIndex);
                        command.Parameters.AddWithValue("@Kaufdatum", dateCoilsNewCoilPurchase.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Lieferant", cmbCoilsNewCoilSupplier.SelectedIndex);
                        command.Parameters.AddWithValue("@Ausführung", cmbCoilsNewCoilVariant.SelectedIndex);
                        command.Parameters.AddWithValue("@Status", cmbCoilsNewCoilStatus.SelectedIndex);
                        command.Parameters.AddWithValue("@Breite", txtCoilsNewCoilWidth.Text);
                        command.Parameters.AddWithValue("@Gewicht", txtCoilsNewCoilWeight.Text);
                        command.Parameters.AddWithValue("@Länge", txtCoilsNewCoilLength.Text);
                        command.Parameters.AddWithValue("@Preis", txtCoilsNewCoilPrice.Text.Replace(',', '.'));
                        command.Parameters.AddWithValue("@Notizen", txtCoilsNewCoilText.Text);
                        command.Parameters.AddWithValue("@Charge", txtCoilsNewCoilHeat.Text);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    private async void LoadCoilsInStorage(string Selectors)
    {
        ListCoils.Items.Clear();
        ListCoils.Columns.Clear();

        // Add columns to the ListView
        ListCoils.Columns.Add("Coilnummer", -2);
        ListCoils.Columns.Add("Status", -2);
        ListCoils.Columns.Add("WSGruppe", -2);
        ListCoils.Columns.Add("Wandstärke", -2);
        ListCoils.Columns.Add("Besäumt", -2);
        ListCoils.Columns.Add("Werkstoff", -2);
        ListCoils.Columns.Add("Kaufdatum", -2);
        ListCoils.Columns.Add("Lieferant", -2);
        ListCoils.Columns.Add("Ausführung", -2);
        ListCoils.Columns.Add("Breite", -2);
        ListCoils.Columns.Add("Gewicht", -2);
        ListCoils.Columns.Add("Länge", -2);
        ListCoils.Columns.Add("Preis", -2);
        ListCoils.Columns.Add("Notizen", -2);

        // Fetch total number of rows
        int totalRows = GetTotalRowCount(Selectors);
        const int pageSize = 1000; // Number of rows to fetch at a time
        int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

        // Fetch rows in batches
        for (int currentPage = 0; currentPage < totalPages; currentPage++)
        {
            List<string[]> rows = CoilsGetRows(currentPage, pageSize, Selectors);

            foreach (ColumnHeader column in ListCoils.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); // Resize based on header size
            }

            //await Task.Delay(250);

            // Add each row to the ListView with a delay
            foreach (var row in rows)
            {
                ListViewItem item = new ListViewItem(row[0]);

                for (int i = 1; i < row.Length; i++)
                {
                    item.SubItems.Add(row[i]);
                }

                ListCoils.Items.Add(item);
            }
        }

        foreach (ColumnHeader column in ListCoils.Columns)
        {
            column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); // Resize based on header size
        }
    }

    private int GetTotalRowCount(string Selectors)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string countQuery = $"SELECT COUNT(*) FROM lagercoils {Selectors}";

            using (MySqlCommand cmd = new MySqlCommand(countQuery, conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }

    private List<string[]> CoilsGetRows(int pageNumber, int pageSize, string Selectors)
    {
        var results = new List<string[]>();
        int offset = pageNumber * pageSize;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = $"SELECT LaufendeCoilnummer, Status, WSGruppe, Wandstarke, Besaumt, Werkstoff, Kaufdatum, Lieferant, Ausfuhrung, Breite, Gewicht, Lange, Preis, Notizen FROM lagercoils {Selectors} LIMIT {pageSize} OFFSET {offset}";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string WSGruppe = reader["WSGruppe"].ToString();
                        string Wandstarke = reader["Wandstarke"].ToString();

                        string Besaumt = reader["Besaumt"].ToString() == "1" ? "Ja" : "Nein";
                        string Werkstoff = GetFieldValue("Werkstoff", reader["Werkstoff"].ToString(), "werkstoffe", "LaufendeWerkstoffnummer");

                        DateTime kaufdatum = Convert.ToDateTime(reader["Kaufdatum"]);
                        string Kaufdatum = kaufdatum.ToString("yyyy-MM-dd");
                        string Lieferant = GetFieldValue("Name", reader["Lieferant"].ToString(), "lieferanten", "LaufendeLieferantennummer");
                        string Ausfuhrung = reader["Ausfuhrung"].ToString();
                        string Breite = reader["Breite"].ToString();
                        string Gewicht = reader["Gewicht"].ToString();
                        string Lange = reader["Lange"].ToString();
                        string Preis = reader["Preis"].ToString();
                        string Notizen = reader["Notizen"].ToString();
                        string Status = reader["Status"].ToString();

                        switch (int.Parse(Ausfuhrung))
                        {
                            case 0:
                                Ausfuhrung = "1C";
                                break;
                            case 1:
                                Ausfuhrung = "1E";
                                break;
                            case 2:
                                Ausfuhrung = "1D";
                                break;
                            case 3:
                                Ausfuhrung = "2C";
                                break;
                            case 4:
                                Ausfuhrung = "2E";
                                break;
                            case 5:
                                Ausfuhrung = "2D";
                                break;
                            case 6:
                                Ausfuhrung = "2B";
                                break;
                            case 7:
                                Ausfuhrung = "2R";
                                break;
                        }


                        if (String.IsNullOrEmpty(Status)) { Status = "0"; };
                        switch (int.Parse(Status))
                        {
                            case 0:
                                Status = "Unbekannt";
                                break;
                            case 1:
                                Status = "Zu Lieferant Unterwegs";
                                break;
                            case 2:
                                Status = "Lager Lieferant";
                                break;
                            case 3:
                                Status = "Unterwegs zu EHG";
                                break;
                            case 4:
                                Status = "Lager";
                                break;
                            case 5:
                                Status = "Spaltplan";
                                break;
                            case 6:
                                Status = "Gespalten";
                                break;
                            case 7:
                                Status = "Verarbeitet";
                                break;
                            case 8:
                                Status = "Band";
                                break;
                            case 9:
                                Status = "Restband";
                                break;

                        }

                        string LaufendeCoilnummer = reader["LaufendeCoilnummer"].ToString();

                        results.Add(new string[] { LaufendeCoilnummer, Status, WSGruppe, Wandstarke, Besaumt, Werkstoff, Kaufdatum, Lieferant, Ausfuhrung, Breite, Gewicht, Lange, Preis, Notizen });
                    }
                }
            }
        }
        return results;
    }

    private void btnCoilsSearch_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string GradeQuery = "";
        string SupplierQuery = "";
        string WTQuery = "";
        string query = "";
        string StatusQuery = "";

        if (SearchComboWT.SelectedIndex != -1 || SearchComboGrade.SelectedIndex != -1 || SearchComboSupplier.SelectedIndex != -1 || SearchComboStatus.SelectedIndex != -1)
        {

            if (SearchComboWT.SelectedIndex != -1)
            {
                WTQuery = $@"WSGruppe = '{SearchComboWT.SelectedItem}'";
            }

            if (SearchComboGrade.SelectedIndex != -1)
            {
                if (SearchComboWT.SelectedIndex != -1)
                {
                    GradeQuery = $@" AND Werkstoff = '{SearchComboGrade.SelectedIndex + 1}'";
                }
                else
                {
                    GradeQuery = $@"Werkstoff = '{SearchComboGrade.SelectedIndex + 1}'";
                }
            }

            if (SearchComboSupplier.SelectedIndex != -1)
            {
                if (SearchComboWT.SelectedIndex != -1 || SearchComboGrade.SelectedIndex != -1)
                {
                    SupplierQuery = $@" AND Lieferant = '{SearchComboSupplier.SelectedIndex}'";
                }
                else
                {
                    SupplierQuery = $@"Lieferant = '{SearchComboSupplier.SelectedIndex}'";
                }
            }

            if (SearchComboStatus.SelectedIndex != -1)
            {
                if (SearchComboSupplier.SelectedIndex != -1 || SearchComboGrade.SelectedIndex != -1 || SearchComboWT.SelectedIndex != -1)
                {
                    StatusQuery = $@" AND Status = '{SearchComboStatus.SelectedIndex}' OR Status IS NULL";
                }
                else
                {
                    StatusQuery = $@"Status = '{SearchComboStatus.SelectedIndex}'";
                }
            }

            // Build and execute the query
            query = $@"WHERE {WTQuery}{GradeQuery}{SupplierQuery}{StatusQuery}";
        }

        LoadCoilsInStorage(query);
    }

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
}