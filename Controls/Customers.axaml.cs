using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;

namespace PS3000;

public partial class Customers : UserControl
{
    public Customers()
    {
        InitializeComponent();
    }

    //Following is all for Delivery Adress Information on Customer Tab

    private async void btnCustomersSaveDeliveryAdress(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Display a MessageBox with two buttons

        string TEST = PS3000.Properties.Resources.ConnectionString;


        var box = MessageBoxManager
        .GetMessageBoxStandard("Bestätigung", TEST,
        ButtonEnum.YesNo);

        var result = await box.ShowAsync();


        //DialogResult result = MessageBox.Show(
        //$"Firmenname: {txtNewDeliveryAdressCompany.Text}{Environment.NewLine}" +
        //$"Straße: {txtNewDeliveryAdressStreet.Text}{Environment.NewLine}" +
        //$"Hausnummer: {txtNewDeliveryAdressNumber.Text}{Environment.NewLine}" +
        //$"PLZ: {txtNewDeliveryAdressPostCode.Text}{Environment.NewLine}" +
        //$"Stadt: {txtNewDeliveryAdressCity.Text}{Environment.NewLine}" +
        //$"Land: {txtNewDeliveryAdressCountry.Text}{Environment.NewLine}" +
        //$"Ansprechpartner: {txtNewDeliveryAdressContactName.Text}{Environment.NewLine}" +
        //$"Ansprechpartner Telefon: {txtNewDeliveryAdressContactPhone.Text}{Environment.NewLine}" +
        //$"Ansprechpartner Mail: {txtNewDeliveryAdressContactMail.Text}",
        //"Confirm Details", // Title of the message box
        //MessageBoxButtons.YesNo); // Buttons for the message box

        //// Check the Length of the MessageBox
        //if (result == DialogResult.Yes)
        //{
        //    try
        //    {
        //using (MySqlConnection connection = new MySqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            string query = @"INSERT INTO `lieferanschrift` 
        //                    (`Firmenname`, `Straße`, `Hausnummer`, `PLZ`, `Stadt`, `Land`, `Ansprechpartner`, `Ansprechpartner_Telefon`, `Ansprechpartner_Mail`)
        //                    VALUES
        //                    (@Firmenname, @Straße, @Hausnummer, @PLZ, @Stadt, @Land, @Ansprechpartner, @AnsprechpartnerTelefon, @AnsprechpartnerMail);";

        //            MySqlCommand command = new MySqlCommand(query, connection);

        //            // Add parameters
        //            command.Parameters.AddWithValue("@Firmenname", txtNewDeliveryAdressCompany.Text);
        //            command.Parameters.AddWithValue("@Straße", txtNewDeliveryAdressStreet.Text);
        //            command.Parameters.AddWithValue("@Hausnummer", txtNewDeliveryAdressNumber.Text);
        //            command.Parameters.AddWithValue("@PLZ", txtNewDeliveryAdressPostCode.Text);
        //            command.Parameters.AddWithValue("@Stadt", txtNewDeliveryAdressCity.Text);
        //            command.Parameters.AddWithValue("@Land", txtNewDeliveryAdressCountry.Text);
        //            command.Parameters.AddWithValue("@Ansprechpartner", txtNewDeliveryAdressContactName.Text);
        //            command.Parameters.AddWithValue("@AnsprechpartnerTelefon", txtNewDeliveryAdressContactPhone.Text);
        //            command.Parameters.AddWithValue("@AnsprechpartnerMail", txtNewDeliveryAdressContactMail.Text);

        //            // Execute the query
        //            int rowsAffected = command.ExecuteNonQuery();

        //            // Check if the insertion was successful
        //            if (rowsAffected > 0)
        //            {
        //                MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Failed to insert data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    CustomerDeliveryAdressSearchList();
        //    CustomerDeliveryAdressInformationClear();
        //}
    }

    //private void listCustomersDeliveryAdresses_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (listCustomersDeliveryAdresses.SelectedItem != null)
    //    {

    //        string ID = listCustomersDeliveryAdresses.SelectedItem.ToString();
    //        txtNewDeliveryAdressCompany.Text = GetFieldValue("Firmenname", ID, "lieferanschrift", "Firmenname");
    //        txtNewDeliveryAdressStreet.Text = GetFieldValue("Straße", ID, "lieferanschrift", "Firmenname");
    //        txtNewDeliveryAdressNumber.Text = GetFieldValue("Hausnummer", ID, "lieferanschrift", "Firmenname");
    //        txtNewDeliveryAdressPostCode.Text = GetFieldValue("PLZ", ID, "lieferanschrift", "Firmenname");
    //        txtNewDeliveryAdressCity.Text = GetFieldValue("Stadt", ID, "lieferanschrift", "Firmenname");
    //        txtNewDeliveryAdressCountry.Text = GetFieldValue("Land", ID, "lieferanschrift", "Firmenname");
    //        txtNewDeliveryAdressContactName.Text = GetFieldValue("Ansprechpartner", ID, "lieferanschrift", "Firmenname");
    //        txtNewDeliveryAdressContactPhone.Text = GetFieldValue("Ansprechpartner_Telefon", ID, "lieferanschrift", "Firmenname");
    //        txtNewDeliveryAdressContactMail.Text = GetFieldValue("Ansprechpartner_Mail", ID, "lieferanschrift", "Firmenname");
    //        txtLieferanschriftID.Text = GetFieldValue("Lieferanschrift_Nummer", ID, "lieferanschrift", "Firmenname");
    //    }
    //}

    //private void textCustomersDeliveryAdressSearch_TextChanged(object sender, EventArgs e)
    //{
    //    CustomerDeliveryAdressSearchList();
    //}

    //private void btnCustomerDeliveryAdressChange_Click(object sender, EventArgs e)
    //{
    //    if (listCustomersDeliveryAdresses.SelectedItem != null)
    //    {
    //        DialogResult result = MessageBox.Show(
    //        $"Firmenname: {txtNewDeliveryAdressCompany.Text}{Environment.NewLine}" +
    //        $"Straße: {txtNewDeliveryAdressStreet.Text}{Environment.NewLine}" +
    //        $"Hausnummer: {txtNewDeliveryAdressNumber.Text}{Environment.NewLine}" +
    //        $"PLZ: {txtNewDeliveryAdressPostCode.Text}{Environment.NewLine}" +
    //        $"Stadt: {txtNewDeliveryAdressCity.Text}{Environment.NewLine}" +
    //        $"Land: {txtNewDeliveryAdressCountry.Text}{Environment.NewLine}" +
    //        $"Ansprechpartner: {txtNewDeliveryAdressContactName.Text}{Environment.NewLine}" +
    //        $"Ansprechpartner Telefon: {txtNewDeliveryAdressContactPhone.Text}{Environment.NewLine}" +
    //        $"Ansprechpartner Mail: {txtNewDeliveryAdressContactMail.Text}",
    //        "Confirm Details", // Title of the message box
    //        MessageBoxButtons.YesNo); // Buttons for the message box

    //        // Check the Length of the MessageBox
    //        if (result == DialogResult.Yes)
    //        {

    //            string tablerow = "Firmenname";
    //            string txtfield = txtNewDeliveryAdressCompany.Text;
    //            string updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{txtLieferanschriftID.Text}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            updateQuery = $"UPDATE lieferanschrift SET Straße = '{txtNewDeliveryAdressStreet.Text}' WHERE Lieferanschrift_Nummer = '{txtLieferanschriftID.Text}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }


    //            tablerow = "Hausnummer";
    //            txtfield = txtNewDeliveryAdressNumber.Text;
    //            updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{txtLieferanschriftID.Text}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            tablerow = "PLZ";
    //            txtfield = txtNewDeliveryAdressPostCode.Text;
    //            updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{txtLieferanschriftID.Text}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            tablerow = "Stadt";
    //            txtfield = txtNewDeliveryAdressCity.Text;
    //            updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{txtLieferanschriftID.Text}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }


    //            tablerow = "Land";
    //            txtfield = txtNewDeliveryAdressCountry.Text;
    //            updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{txtLieferanschriftID.Text}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            tablerow = "Ansprechpartner";
    //            txtfield = txtNewDeliveryAdressContactName.Text;
    //            updateQuery = $"UPDATE lieferanschrift SET {tablerow} = '{txtfield}' WHERE Lieferanschrift_Nummer = '{txtLieferanschriftID.Text}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            tablerow = "Ansprechpartner_Telefon";
    //            txtfield = txtNewDeliveryAdressContactPhone.Text;
    //            string firmenname = listCustomersDeliveryAdresses.SelectedItem.ToString();

    //            // Ensure the connection is disposed of properly
    //            using (MySqlConnection connection = new MySqlConnection(connectionString))
    //            {
    //                // Open the connection
    //                connection.Open();

    //                // Create the SQL command with parameters
    //                updateQuery = $"UPDATE lieferanschrift SET `{tablerow}` = @TxtField WHERE Lieferanschrift_Nummer = '{txtLieferanschriftID.Text}'";

    //                // Create and configure the MySqlCommand
    //                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
    //                {
    //                    // Add parameters to the command
    //                    command.Parameters.AddWithValue("@TxtField", txtfield);
    //                    command.Parameters.AddWithValue("@Firmenname", firmenname);

    //                    try
    //                    {
    //                        command.ExecuteNonQuery();
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        MessageBox.Show("Error executing SQL query: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //                    }
    //                }
    //            }

    //            tablerow = "Ansprechpartner_Mail";
    //            txtfield = txtNewDeliveryAdressContactMail.Text;
    //            firmenname = listCustomersDeliveryAdresses.SelectedItem.ToString();

    //            // Ensure the connection is disposed of properly
    //            using (MySqlConnection connection = new MySqlConnection(connectionString))
    //            {
    //                // Open the connection
    //                connection.Open();

    //                // Create the SQL command with parameters
    //                updateQuery = $"UPDATE lieferanschrift SET `{tablerow}` = @TxtField WHERE Lieferanschrift_Nummer = '{txtLieferanschriftID.Text}'";

    //                // Create and configure the MySqlCommand
    //                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
    //                {
    //                    // Add parameters to the command
    //                    command.Parameters.AddWithValue("@TxtField", txtfield);
    //                    command.Parameters.AddWithValue("@Firmenname", firmenname);

    //                    try
    //                    {
    //                        // Execute the query
    //                        int rowsAffected = command.ExecuteNonQuery();

    //                        // Check if any rows were affected
    //                        if (rowsAffected > 0)
    //                        {
    //                            MessageBox.Show("Update successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //                        }
    //                        else
    //                        {
    //                            MessageBox.Show("No matching record found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //                        }
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        MessageBox.Show("Error executing SQL query: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //                    }
    //                }
    //            }
    //            CustomerDeliveryAdressInformationClear();
    //            CustomerDeliveryAdressSearchList();
    //        }
    //    }
    //}

    //private void CustomerDeliveryAdressInformationClear()
    //{
    //    txtNewDeliveryAdressCompany.Text = "";
    //    txtNewDeliveryAdressStreet.Text = "";
    //    txtNewDeliveryAdressNumber.Text = "";
    //    txtNewDeliveryAdressPostCode.Text = "";
    //    txtNewDeliveryAdressCity.Text = "";
    //    txtNewDeliveryAdressCountry.Text = "";
    //    txtNewDeliveryAdressContactName.Text = "";
    //    txtNewDeliveryAdressContactPhone.Text = "";
    //    txtNewDeliveryAdressContactMail.Text = "";
    //    txtLieferanschriftID.Text = "";
    //}

    //private void CustomerDeliveryAdressSearchList()
    //{
    //    //TO ADAPT MORE DB Searches:
    //    //Change the "lieferanschrift to which table it should look through
    //    // Change Query String to match all Rows of your DB it should search for.
    //    //Set the Textbox for Search Code Source
    //    //Set the supposed readout index value (maybe its earlier or later)

    //    if (textCustomersDeliveryAdressSearch.Text != null)
    //    {
    //        listCustomersDeliveryAdresses.Items.Clear();

    //        try
    //        {
    //            using (MySqlConnection connection = new MySqlConnection(connectionString))
    //            {
    //                connection.Open();

    //                string query = $"SELECT * FROM `prostahl`.`lieferanschrift` WHERE CONCAT_WS(' ', `Firmenname`, `Straße`, `Hausnummer`, `PLZ`, `Stadt`, `Land`, `Ansprechpartner`, `Ansprechpartner_Telefon`, `Ansprechpartner_Mail`) LIKE '%{textCustomersDeliveryAdressSearch.Text}%' ORDER BY `Firmenname` ASC LIMIT 1000;\r\n";
    //                MySqlCommand command = new MySqlCommand(query, connection);

    //                using (MySqlDataReader reader = command.ExecuteReader())
    //                {
    //                    while (reader.Read())
    //                    {
    //                        // Read values from columns
    //                        string readout = reader.GetString(1); // Assuming the searched value is at index 1

    //                        // Do something with the values...
    //                        listCustomersDeliveryAdresses.Items.Add(readout);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show("Error: " + ex.Message);
    //        }
    //    }
    //}

    ////Following is all for Customer Information on Customer Tab

    //private void btnNewCustomer_Click(object sender, EventArgs e)
    //{
    //    if (listKunden.SelectedItem != null)
    //    {
    //        DialogResult result = MessageBox.Show("Kürzel: " + txtCustomerShortName.Text + Environment.NewLine + "Firmenname: " + txtCustomersNewCustomerCompanyName.Text + Environment.NewLine + "Rechnung Straße: " + txtCustomerAdressStreet.Text + Environment.NewLine + "Rechnung Hausnummer: " + txtCustomerAdressHouseNr.Text + Environment.NewLine + "Rechnung_Stadt: " + txtCustomerAdressCity.Text + Environment.NewLine + "Rechnung_PLZ: " + txtCustomerAdressPostcode.Text + Environment.NewLine + "Rechnung_Land: " + txtCustomerAdressCountry.Text + Environment.NewLine + "Einkäufer_Name: " + txtCustomerPurchaser.Text + Environment.NewLine + "Einkäufer_Telefonnummer: " + txtCustomerPurchaserTelephone.Text + Environment.NewLine + "Einkäufer_EMail: " + txtCustomerPurchaserMail.Text + Environment.NewLine + "Buchhaltung_Name: " + txtCustomerBookkeeperName.Text + Environment.NewLine + "Buchhaltung_Telefonnummer: " + txtCustomerBookkeeperTelephone.Text + Environment.NewLine + "Buchhaltung_EMail: " + txtCustomerBookkeeperMail.Text + Environment.NewLine + "Werkszeugnis_EMail: " + txtCustomerCertificationMail.Text + Environment.NewLine + "Rechnung_EMail: " + txtCustomerInvoiceMail.Text + Environment.NewLine + "Skonto: " + txtCustomerSkonto.Text + Environment.NewLine + "Skontofrist: " + txtCustomerSkontofrist.Text + Environment.NewLine + "Nettofrist: " + txtCustomerNettofrist.Text + Environment.NewLine, "Kunde Ändern", MessageBoxButtons.YesNo);

    //        // Check the Length of the MessageBox
    //        if (result == DialogResult.Yes)
    //        {
    //            // Customer Selected -> offer Change
    //            string table = "kunden";

    //            string row = "Kürzel";
    //            string textbox = txtCustomerShortName.Text;
    //            string updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Firmenname";
    //            textbox = txtCustomersNewCustomerCompanyName.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Rechnung_Straße";
    //            textbox = txtCustomerAdressStreet.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";
    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Rechnung_Hausnummer";
    //            textbox = txtCustomerAdressHouseNr.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Rechnung_Stadt";
    //            textbox = txtCustomerAdressCity.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Rechnung_PLZ";
    //            textbox = txtCustomerAdressPostcode.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Rechnung_Land";
    //            textbox = txtCustomerAdressCountry.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Einkäufer_Name";
    //            textbox = txtCustomerPurchaser.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Einkäufer_Telefonnummer";
    //            textbox = txtCustomerPurchaserTelephone.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Einkäufer_Email";
    //            textbox = txtCustomerPurchaserMail.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Buchhaltung_Name";
    //            textbox = txtCustomerBookkeeperName.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Buchhaltung_Telefonnummer";
    //            textbox = txtCustomerBookkeeperTelephone.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Buchhaltung_Email";
    //            textbox = txtCustomerBookkeeperMail.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Werkszeugnis_Email";
    //            textbox = txtCustomerCertificationMail.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Rechnung_Email";
    //            textbox = txtCustomerInvoiceMail.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }





    //            row = "Skonto";
    //            textbox = txtCustomerSkonto.Text.Replace(',', '.');
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Skontofrist";
    //            textbox = txtCustomerSkontofrist.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            row = "Nettofrist";
    //            textbox = txtCustomerNettofrist.Text;
    //            updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

    //            try
    //            {
    //                using (MySqlConnection connection = new MySqlConnection(connectionString))
    //                {
    //                    connection.Open();
    //                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show("Error: " + ex.Message);
    //            }

    //            CustomerCustomerInformationClear();
    //            CustomerCustomerCreationSearchList();
    //        }
    //    }
    //    else
    //    {

    //        // Display a MessageBox with two buttons
    //        DialogResult result = MessageBox.Show("Kürzel: " + txtCustomerShortName.Text + Environment.NewLine + "Firmenname: " + txtCustomersNewCustomerCompanyName.Text + Environment.NewLine + "Rechnung Straße: " + txtCustomerAdressStreet.Text + Environment.NewLine + "Rechnung Hausnummer: " + txtCustomerAdressHouseNr.Text + Environment.NewLine + "Rechnung_Stadt: " + txtCustomerAdressCity.Text + Environment.NewLine + "Rechnung_PLZ: " + txtCustomerAdressPostcode.Text + Environment.NewLine + "Rechnung_Land: " + txtCustomerAdressCountry.Text + Environment.NewLine + "Einkäufer_Name: " + txtCustomerPurchaser.Text + Environment.NewLine + "Einkäufer_Telefonnummer: " + txtCustomerPurchaserTelephone.Text + Environment.NewLine + "Einkäufer_EMail: " + txtCustomerPurchaserMail.Text + Environment.NewLine + "Buchhaltung_Name: " + txtCustomerBookkeeperName.Text + Environment.NewLine + "Buchhaltung_Telefonnummer: " + txtCustomerBookkeeperTelephone.Text + Environment.NewLine + "Buchhaltung_EMail: " + txtCustomerBookkeeperMail.Text + Environment.NewLine + "Werkszeugnis_EMail: " + txtCustomerCertificationMail.Text + Environment.NewLine + "Rechnung_EMail: " + txtCustomerInvoiceMail.Text + Environment.NewLine + "Skonto: " + txtCustomerSkonto.Text + Environment.NewLine + "Skontofrist: " + txtCustomerSkontofrist.Text + Environment.NewLine + "Nettofrist: " + txtCustomerNettofrist.Text + Environment.NewLine, "Kunde Anlegen", MessageBoxButtons.YesNo);

    //        // Check the Length of the MessageBox
    //        if (result == DialogResult.Yes)
    //        {

    //            // SQL query
    //            string query = @"INSERT INTO kunden 
    //                (Kürzel, Firmenname, `Rechnung_Straße`, `Rechnung_Hausnummer`, `Rechnung_Stadt`, `Rechnung_PLZ`, `Rechnung_Land`, 
    //                `Einkäufer_Name`, `Einkäufer_Telefonnummer`, `Einkäufer_EMail`, `Buchhaltung_Name`, `Buchhaltung_Telefonnummer`, 
    //                `Buchhaltung_EMail`, `Werkszeugnis_EMail`, `Rechnung_EMail`, `Skonto`, `Skontofrist`, `Nettofrist`) 
    //                VALUES 
    //                (@Kürzel, @Firmenname, @Rechnung_Straße, @Rechnung_Hausnummer, @Rechnung_Stadt, @Rechnung_PLZ, @Rechnung_Land, 
    //                @Einkäufer_Name, @Einkäufer_Telefonnummer, @Einkäufer_Email, @Buchhaltung_Name, @Buchhaltung_Telefonnummer, 
    //                @Buchhaltung_Email, @Werkszeugnis_Email, @Rechnung_Email, @Skonto, @Skontofrist, @Nettofrist)";

    //            // Create MySqlConnection object
    //            using (MySqlConnection connection = new MySqlConnection(connectionString))
    //            {
    //                // Open the connection
    //                connection.Open();

    //                // Create MySqlCommand object
    //                using (MySqlCommand command = new MySqlCommand(query, connection))
    //                {
    //                    // Add parameters
    //                    command.Parameters.AddWithValue("@Kürzel", txtCustomerShortName.Text);
    //                    command.Parameters.AddWithValue("@Firmenname", txtCustomersNewCustomerCompanyName.Text);
    //                    command.Parameters.AddWithValue("@Rechnung_Straße", txtCustomerAdressStreet.Text);
    //                    command.Parameters.AddWithValue("@Rechnung_Hausnummer", txtCustomerAdressHouseNr.Text);
    //                    command.Parameters.AddWithValue("@Rechnung_Stadt", txtCustomerAdressCity.Text);
    //                    command.Parameters.AddWithValue("@Rechnung_PLZ", txtCustomerAdressPostcode.Text);
    //                    command.Parameters.AddWithValue("@Rechnung_Land", txtCustomerAdressCountry.Text);
    //                    command.Parameters.AddWithValue("@Einkäufer_Name", txtCustomerPurchaser.Text);
    //                    command.Parameters.AddWithValue("@Einkäufer_Telefonnummer", txtCustomerPurchaserTelephone.Text);
    //                    command.Parameters.AddWithValue("@Einkäufer_Email", txtCustomerPurchaserMail.Text);
    //                    command.Parameters.AddWithValue("@Buchhaltung_Name", txtCustomerBookkeeperName.Text);
    //                    command.Parameters.AddWithValue("@Buchhaltung_Telefonnummer", txtCustomerBookkeeperTelephone.Text);
    //                    command.Parameters.AddWithValue("@Buchhaltung_Email", txtCustomerBookkeeperMail.Text);
    //                    command.Parameters.AddWithValue("@Werkszeugnis_Email", txtCustomerCertificationMail.Text);
    //                    command.Parameters.AddWithValue("@Rechnung_Email", txtCustomerInvoiceMail.Text);
    //                    command.Parameters.AddWithValue("@Skonto", txtCustomerSkonto.Text);
    //                    command.Parameters.AddWithValue("@Skontofrist", txtCustomerSkontofrist.Text);
    //                    command.Parameters.AddWithValue("@Nettofrist", txtCustomerNettofrist.Text);


    //                    // Execute the query
    //                    command.ExecuteNonQuery();
    //                }
    //            }
    //            CustomerCustomerInformationClear();
    //            CustomerCustomerCreationSearchList();
    //        }
    //    }
    //}

    //private void txtKundenname_TextChanged(object sender, EventArgs e)
    //{
    //    CustomerCustomerCreationSearchList();
    //}

    //private void listKunden_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (listKunden.SelectedItem != null)
    //    {
    //        string firmenname = listKunden.SelectedItem.ToString();

    //        textBox102.Text = GetFieldValue("Kundennummer", firmenname, "kunden", "Firmenname");
    //        txtCustomerAdressStreet.Text = GetFieldValue("Rechnung_Straße", firmenname, "kunden", "Firmenname");
    //        txtCustomerAdressHouseNr.Text = GetFieldValue("Rechnung_Hausnummer", firmenname, "kunden", "Firmenname");
    //        txtCustomerShortName.Text = GetFieldValue("Kürzel", firmenname, "kunden", "Firmenname");
    //        txtCustomerAdressCity.Text = GetFieldValue("Rechnung_Stadt", firmenname, "kunden", "Firmenname");
    //        txtCustomerAdressPostcode.Text = GetFieldValue("Rechnung_PLZ", firmenname, "kunden", "Firmenname");
    //        txtCustomerAdressCountry.Text = GetFieldValue("Rechnung_Land", firmenname, "kunden", "Firmenname");
    //        txtCustomerPurchaser.Text = GetFieldValue("Einkäufer_Name", firmenname, "kunden", "Firmenname");
    //        txtCustomerPurchaserTelephone.Text = GetFieldValue("Einkäufer_Telefonnummer", firmenname, "kunden", "Firmenname");
    //        txtCustomerPurchaserMail.Text = GetFieldValue("Einkäufer_EMail", firmenname, "kunden", "Firmenname");
    //        txtCustomerBookkeeperName.Text = GetFieldValue("Buchhaltung_Name", firmenname, "kunden", "Firmenname");
    //        txtCustomerBookkeeperTelephone.Text = GetFieldValue("Buchhaltung_Telefonnummer", firmenname, "kunden", "Firmenname");
    //        txtCustomerBookkeeperMail.Text = GetFieldValue("Buchhaltung_EMail", firmenname, "kunden", "Firmenname");
    //        txtCustomerCertificationMail.Text = GetFieldValue("Werkszeugnis_EMail", firmenname, "kunden", "Firmenname");
    //        txtCustomerInvoiceMail.Text = GetFieldValue("Rechnung_EMail", firmenname, "kunden", "Firmenname");
    //        txtCustomersNewCustomerCompanyName.Text = GetFieldValue("Firmenname", firmenname, "kunden", "Firmenname");
    //        txtCustomerNotes.Text = GetFieldValue("Notizen", firmenname, "kunden", "Firmenname");
    //        txtCustomerSkonto.Text = GetFieldValue("Skonto", firmenname, "kunden", "Firmenname");
    //        txtCustomerSkontofrist.Text = GetFieldValue("Skontofrist", firmenname, "kunden", "Firmenname");
    //        txtCustomerNettofrist.Text = GetFieldValue("Nettofrist", firmenname, "kunden", "Firmenname");
    //    }
    //}

    //private void txtCustomerNotes_LostFocus(object sender, EventArgs e)
    //{
    //    if (listKunden.SelectedItem != null)
    //    {
    //        // Customer Selected -> offer Change
    //        string table = "kunden";

    //        string row = "Notizen";
    //        string textbox = txtCustomerNotes.Text;
    //        string updateQuery = $"UPDATE {table} SET {row} = '{textbox}' WHERE Firmenname = '{listKunden.SelectedItem.ToString()}'";

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
    //    CustomerCustomerInformationClear();
    //    CustomerCustomerCreationSearchList();
    //}

    //private void CustomerCustomerInformationClear()
    //{
    //    txtCustomerPurchaser.Text = "";
    //    txtCustomerPurchaserTelephone.Text = "";
    //    txtCustomerPurchaserMail.Text = "";
    //    txtCustomerBookkeeperName.Text = "";
    //    txtCustomerBookkeeperTelephone.Text = "";
    //    txtCustomerBookkeeperMail.Text = "";
    //    txtCustomerCertificationMail.Text = "";
    //    txtCustomerInvoiceMail.Text = "";
    //    txtCustomerShortName.Text = "";
    //    txtCustomerAdressStreet.Text = "";
    //    txtCustomerAdressHouseNr.Text = "";
    //    txtCustomerAdressCity.Text = "";
    //    txtCustomerAdressPostcode.Text = "";
    //    txtCustomerAdressCountry.Text = "";
    //    txtCustomersNewCustomerCompanyName.Text = "";
    //    txtCustomerNotes.Text = "";
    //    txtCustomerSkonto.Text = "";
    //    txtCustomerSkontofrist.Text = "";
    //    txtCustomerNettofrist.Text = "";
    //}

    //private void CustomerCustomerCreationSearchList()
    //{
    //    if (txtKundenname.Text != null)
    //    {
    //        listKunden.Items.Clear();

    //        try
    //        {
    //            string query = $"SELECT * FROM prostahl.kunden WHERE CONCAT_WS(' ', Kürzel, Firmenname, Rechnung_Straße, Rechnung_Hausnummer, Rechnung_Stadt, Rechnung_PLZ, Rechnung_Land, Einkäufer_Name, Einkäufer_Telefonnummer, Einkäufer_EMail, Buchhaltung_Name, Buchhaltung_Telefonnummer, Buchhaltung_EMail, Werkszeugnis_EMail, Rechnung_EMail, Skonto, Skontofrist, Nettofrist) LIKE @SearchTerm ORDER BY Firmenname ASC LIMIT 1000";

    //            using (MySqlConnection connection = new MySqlConnection(connectionString))
    //            {
    //                using (MySqlCommand command = new MySqlCommand(query, connection))
    //                {
    //                    command.Parameters.AddWithValue("@SearchTerm", $"%{txtKundenname.Text}%");

    //                    try
    //                    {
    //                        connection.Open();
    //                        using (MySqlDataReader reader = command.ExecuteReader())
    //                        {
    //                            while (reader.Read())
    //                            {
    //                                // Read values from columns
    //                                string firmenname = reader.GetString(2); // Assuming `Firmenname` is at index 2

    //                                // Do something with the values...
    //                                listKunden.Items.Add(firmenname);
    //                            }
    //                        }
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        MessageBox.Show("Error executing SQL query: " + ex.Message);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show("Error: " + ex.Message);
    //        }
    //    }
    //}
}