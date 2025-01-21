using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;

namespace PS3000;

public partial class MasterData : UserControl
{
    public MasterData()
    {
        InitializeComponent();
    }

    //private void btnStorageCoilsMaterialCreationAlloyComponentsCreate_Click(object sender, EventArgs e)
    //{
    //    if (!String.IsNullOrEmpty(txtStorageCoilsMaterialCreationDINName.Text))
    //    {
    //        DialogResult result = MessageBox.Show(
    //        $"Name: {txtStorageCoilsMaterialCreationDINName.Text}{Environment.NewLine}" +
    //        $"C-Min: {txtStorageCoilsMaterialCreationAlloyComponentsCMin.Text} {Environment.NewLine}" +
    //        $"C-Max: {txtStorageCoilsMaterialCreationAlloyComponentsCMax.Text} {Environment.NewLine}" +
    //        $"Mn-Min: {txtStorageCoilsMaterialCreationAlloyComponentsMnMin.Text} {Environment.NewLine}" +
    //        $"Mn-Max: {txtStorageCoilsMaterialCreationAlloyComponentsMnMax.Text} {Environment.NewLine}" +
    //        $"Si-Min: {txtStorageCoilsMaterialCreationAlloyComponentsSiMin.Text} {Environment.NewLine}" +
    //        $"Si-Max: {txtStorageCoilsMaterialCreationAlloyComponentsSiMax.Text} {Environment.NewLine}" +
    //        $"P-Min: {txtStorageCoilsMaterialCreationAlloyComponentsPMin.Text} {Environment.NewLine}" +
    //        $"P-Max: {txtStorageCoilsMaterialCreationAlloyComponentsPMax.Text} {Environment.NewLine}" +
    //        $"S-Min: {txtStorageCoilsMaterialCreationAlloyComponentsSMin.Text} {Environment.NewLine}" +
    //        $"S-Max: {txtStorageCoilsMaterialCreationAlloyComponentsSMax.Text} {Environment.NewLine}" +
    //        $"Cr-Min: {txtStorageCoilsMaterialCreationAlloyComponentsCrMin.Text} {Environment.NewLine}" +
    //        $"Cr-Max: {txtStorageCoilsMaterialCreationAlloyComponentsCrMax.Text} {Environment.NewLine}" +
    //        $"Ni-Min: {txtStorageCoilsMaterialCreationAlloyComponentsNiMin.Text} {Environment.NewLine}" +
    //        $"Ni-Max: {txtStorageCoilsMaterialCreationAlloyComponentsNiMax.Text} {Environment.NewLine}" +
    //        $"N-Min: {txtStorageCoilsMaterialCreationAlloyComponentsNMin.Text} {Environment.NewLine}" +
    //        $"N-Max: {txtStorageCoilsMaterialCreationAlloyComponentsNMax.Text} {Environment.NewLine}" +
    //        $"Mo-Min: {txtStorageCoilsMaterialCreationAlloyComponentsMoMin.Text} {Environment.NewLine}" +
    //        $"Mo-Max: {txtStorageCoilsMaterialCreationAlloyComponentsMoMax.Text} {Environment.NewLine}" +
    //        $"Ti-Min: {txtStorageCoilsMaterialCreationAlloyComponentsTiMin.Text} {Environment.NewLine}" +
    //        $"Ti-Max: {txtStorageCoilsMaterialCreationAlloyComponentsTiMax.Text} {Environment.NewLine}" +
    //        $"Nb-Min: {txtStorageCoilsMaterialCreationAlloyComponentsNbMin.Text} {Environment.NewLine}" +
    //        $"Nb-Max: {txtStorageCoilsMaterialCreationAlloyComponentsNbMax.Text} {Environment.NewLine}" +
    //        $"Cu-Min: {txtStorageCoilsMaterialCreationAlloyComponentsCuMin.Text} {Environment.NewLine}" +
    //        $"Cu-Max: {txtStorageCoilsMaterialCreationAlloyComponentsCuMax.Text} {Environment.NewLine}" +
    //        $"Ce-Min: {txtStorageCoilsMaterialCreationAlloyComponentsCeMin.Text} {Environment.NewLine}" +
    //        $"Ce-Max: {txtStorageCoilsMaterialCreationAlloyComponentsCeMax.Text} {Environment.NewLine}",
    //        "Confirm Details", // Title of the message box
    //        MessageBoxButtons.YesNo); // Buttons for the message box

    //        // Check the Length of the MessageBox
    //        if (result == DialogResult.Yes)
    //        {
    //            string query = "INSERT INTO werkstoffe (Werkstoff, `C-Min`, `C-Max`, `Mn-Min`, `Mn-Max`, `Si-Min`, `Si-Max`, `P-Min`, `P-Max`, `S-Min`, `S-Max`, `Cr-Min`, `Cr-Max`, `Ni-Min`, `Ni-Max`, `N-Min`, `N-Max`, `Mo-Min`, `Mo-Max`, `Ti-Min`, `Ti-Max`, `Nb-Min`, `Nb-Max`, `Cu-Min`, `Cu-Max`, `Ce-Min`, `Ce-Max`) " +
    //                           "VALUES (@Werkstoff, @CMin, @CMax, @MnMin, @MnMax, @SiMin, @SiMax, @PMin, @PMax, @SMin, @SMax, @CrMin, @CrMax, @NiMin, @NiMax, @NMin, @NMax, @MoMin, @MoMax, @TiMin, @TiMax, @NbMin, @NbMax, @CuMin, @CuMax, @CeMin, @CeMax)";

    //            using (MySqlConnection conn = new MySqlConnection(connectionString))
    //            {
    //                MySqlCommand cmd = new MySqlCommand(query, conn);

    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsCMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsCMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsCMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsCMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsMnMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsMnMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsMnMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsMnMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsSiMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsSiMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsSiMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsSiMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsPMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsPMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsPMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsPMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsSMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsSMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsSMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsSMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsCrMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsCrMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsCrMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsCrMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsNiMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsNiMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsNiMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsNiMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsNMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsNMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsNMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsNMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsMoMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsMoMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsMoMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsMoMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsTiMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsTiMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsTiMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsTiMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsNbMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsNbMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsNbMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsNbMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsCuMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsCuMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsCuMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsCuMax);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsCeMin.Text, out float StorageCoilsMaterialCreationAlloyComponentsCeMin);
    //                float.TryParse(txtStorageCoilsMaterialCreationAlloyComponentsCeMax.Text, out float StorageCoilsMaterialCreationAlloyComponentsCeMax);

    //                cmd.Parameters.AddWithValue("@Werkstoff", txtStorageCoilsMaterialCreationDINName.Text);
    //                cmd.Parameters.AddWithValue("@CMin", StorageCoilsMaterialCreationAlloyComponentsCMin);
    //                cmd.Parameters.AddWithValue("@CMax", StorageCoilsMaterialCreationAlloyComponentsCMax);
    //                cmd.Parameters.AddWithValue("@MnMin", StorageCoilsMaterialCreationAlloyComponentsMnMin);
    //                cmd.Parameters.AddWithValue("@MnMax", StorageCoilsMaterialCreationAlloyComponentsMnMax);
    //                cmd.Parameters.AddWithValue("@SiMin", StorageCoilsMaterialCreationAlloyComponentsSiMin);
    //                cmd.Parameters.AddWithValue("@SiMax", StorageCoilsMaterialCreationAlloyComponentsSiMax);
    //                cmd.Parameters.AddWithValue("@PMin", StorageCoilsMaterialCreationAlloyComponentsPMin);
    //                cmd.Parameters.AddWithValue("@PMax", StorageCoilsMaterialCreationAlloyComponentsPMax);
    //                cmd.Parameters.AddWithValue("@SMin", StorageCoilsMaterialCreationAlloyComponentsSMin);
    //                cmd.Parameters.AddWithValue("@SMax", StorageCoilsMaterialCreationAlloyComponentsSMax);
    //                cmd.Parameters.AddWithValue("@CrMin", StorageCoilsMaterialCreationAlloyComponentsCrMin);
    //                cmd.Parameters.AddWithValue("@CrMax", StorageCoilsMaterialCreationAlloyComponentsCrMax);
    //                cmd.Parameters.AddWithValue("@NiMin", StorageCoilsMaterialCreationAlloyComponentsNiMin);
    //                cmd.Parameters.AddWithValue("@NiMax", StorageCoilsMaterialCreationAlloyComponentsNiMax);
    //                cmd.Parameters.AddWithValue("@NMin", StorageCoilsMaterialCreationAlloyComponentsNMin);
    //                cmd.Parameters.AddWithValue("@NMax", StorageCoilsMaterialCreationAlloyComponentsNMax);
    //                cmd.Parameters.AddWithValue("@MoMin", StorageCoilsMaterialCreationAlloyComponentsMoMin);
    //                cmd.Parameters.AddWithValue("@MoMax", StorageCoilsMaterialCreationAlloyComponentsMoMax);
    //                cmd.Parameters.AddWithValue("@TiMin", StorageCoilsMaterialCreationAlloyComponentsTiMin);
    //                cmd.Parameters.AddWithValue("@TiMax", StorageCoilsMaterialCreationAlloyComponentsTiMax);
    //                cmd.Parameters.AddWithValue("@NbMin", StorageCoilsMaterialCreationAlloyComponentsNbMin);
    //                cmd.Parameters.AddWithValue("@NbMax", StorageCoilsMaterialCreationAlloyComponentsNbMax);
    //                cmd.Parameters.AddWithValue("@CuMin", StorageCoilsMaterialCreationAlloyComponentsCuMin);
    //                cmd.Parameters.AddWithValue("@CuMax", StorageCoilsMaterialCreationAlloyComponentsCuMax);
    //                cmd.Parameters.AddWithValue("@CeMin", StorageCoilsMaterialCreationAlloyComponentsCeMin);
    //                cmd.Parameters.AddWithValue("@CeMax", StorageCoilsMaterialCreationAlloyComponentsCeMax);



    //                conn.Open();
    //                cmd.ExecuteNonQuery();
    //            }
    //        }

    //        LoadMaterialList();
    //    }
    //}

    //private void listStorageCoilsMaterialCreationMaterials_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (listStorageCoilsMaterialCreationMaterials.SelectedItem != null)
    //    {

    //        string ID = listStorageCoilsMaterialCreationMaterials.SelectedItem.ToString();

    //        txtStorageCoilsMaterialCreationDINName.Text = ID;
    //        txtStorageCoilsMaterialCreationAlloyComponentsCMin.Text = GetFieldValue("C-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsCMax.Text = GetFieldValue("C-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsMnMin.Text = GetFieldValue("Mn-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsMnMax.Text = GetFieldValue("Mn-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsSiMin.Text = GetFieldValue("Si-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsSiMax.Text = GetFieldValue("Si-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsPMin.Text = GetFieldValue("P-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsPMax.Text = GetFieldValue("P-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsSMin.Text = GetFieldValue("S-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsSMax.Text = GetFieldValue("S-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsCrMin.Text = GetFieldValue("Cr-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsCrMax.Text = GetFieldValue("Cr-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsNiMin.Text = GetFieldValue("Ni-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsNiMax.Text = GetFieldValue("Ni-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsNMin.Text = GetFieldValue("N-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsNMax.Text = GetFieldValue("N-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsMoMin.Text = GetFieldValue("Mo-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsMoMax.Text = GetFieldValue("Mo-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsTiMin.Text = GetFieldValue("Ti-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsTiMax.Text = GetFieldValue("Ti-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsNbMin.Text = GetFieldValue("Nb-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsNbMax.Text = GetFieldValue("Nb-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsCuMin.Text = GetFieldValue("Cu-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsCuMax.Text = GetFieldValue("Cu-Max", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsCeMin.Text = GetFieldValue("Ce-Min", ID, "werkstoffe", "Werkstoff");
    //        txtStorageCoilsMaterialCreationAlloyComponentsCeMax.Text = GetFieldValue("Ce-Max", ID, "werkstoffe", "Werkstoff");

    //        StorageCoilsMaterialCreationMaterialsSearch();
    //    }
    //}

    //private void txtStorageCoilsMaterialCreationMaterialsSearch_TextChanged(object sender, EventArgs e)
    //{
    //    StorageCoilsMaterialCreationMaterialsSearch();
    //}

    //private void StorageCoilsMaterialCreationMaterialsSearch()
    //{
    //    if (txtStorageCoilsMaterialCreationMaterialsSearch.Text != null)
    //    {
    //        listStorageCoilsMaterialCreationMaterials.Items.Clear();

    //        try
    //        {
    //            using (MySqlConnection connection = new MySqlConnection(connectionString))
    //            {
    //                connection.Open();

    //                string query = $"SELECT * FROM `prostahl`.`werkstoffe` WHERE CONCAT_WS(' ', `Werkstoff`) LIKE '%{txtStorageCoilsMaterialCreationMaterialsSearch.Text}%' ORDER BY `Werkstoff` ASC LIMIT 1000;\r\n";
    //                MySqlCommand command = new MySqlCommand(query, connection);

    //                using (MySqlDataReader reader = command.ExecuteReader())
    //                {
    //                    while (reader.Read())
    //                    {
    //                        // Read values from columns
    //                        string readout = reader.GetString(1); // Assuming the searched value is at index 1

    //                        // Do something with the values...
    //                        listStorageCoilsMaterialCreationMaterials.Items.Add(readout);
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




    //Following is to create a new Article (OD, WT, Material) 

    //private void btnStorageTubesArticleCreation_Click(object sender, EventArgs e)
    //{
    //    if (!String.IsNullOrEmpty(txtStorageTubesArticleCreationWT.Text) && !String.IsNullOrEmpty(txtStorageTubesArticleCreationOD.Text) && listStorageTubesArticleCreationMaterials.SelectedItem != null)
    //    {
    //        DialogResult result = MessageBox.Show(
    //        $"Auﬂendurchmesser: {txtStorageTubesArticleCreationOD.Text}{Environment.NewLine}" +
    //        $"Wandst‰rke: {txtStorageTubesArticleCreationWT.Text} {Environment.NewLine}" +
    //        $"Werkstoff: {listStorageTubesArticleCreationMaterials.SelectedItem} {Environment.NewLine}",
    //        "Confirm Details", // Title of the message box
    //        MessageBoxButtons.YesNo); // Buttons for the message box

    //        // Check the Length of the MessageBox
    //        if (result == DialogResult.Yes)
    //        {
    //            string query = "INSERT INTO artikelrohre (Werkstoff, `Auﬂendurchmesser`, `Wandst‰rke`) " +
    //                           "VALUES (@Werkstoff, @Auﬂendurchmesser, @Wandst‰rke)";

    //            using (MySqlConnection conn = new MySqlConnection(connectionString))
    //            {
    //                MySqlCommand cmd = new MySqlCommand(query, conn);

    //                float.TryParse(txtStorageTubesArticleCreationOD.Text, out float StorageTubesArticleCreationOD);
    //                float.TryParse(txtStorageTubesArticleCreationWT.Text, out float StorageTubesArticleCreationWT);

    //                cmd.Parameters.AddWithValue("@Werkstoff", listStorageTubesArticleCreationMaterials.SelectedItem);
    //                cmd.Parameters.AddWithValue("@Auﬂendurchmesser", StorageTubesArticleCreationOD);
    //                cmd.Parameters.AddWithValue("@Wandst‰rke", StorageTubesArticleCreationWT);

    //                conn.Open();
    //                cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //}

    //private void txtStorageTubesArticleCreationWT_TextChanged(object sender, EventArgs e)
    //{
    //    PopulateStorageTubesArticleMaterialsList();
    //}

    //private void txtStorageTubesArticleCreationOD_TextChanged(object sender, EventArgs e)
    //{
    //    PopulateStorageTubesArticleMaterialsList();
    //}

    //private void PopulateStorageTubesArticleMaterialsList()
    //{
    //    listStorageTubesArticleCreationMaterials.Items.Clear();

    //    try
    //    {
    //        using (MySqlConnection connection = new MySqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            string query = $"SELECT * FROM `prostahl`.`werkstoffe` ORDER BY `Werkstoff` ASC LIMIT 1000;\r\n";
    //            MySqlCommand command = new MySqlCommand(query, connection);

    //            using (MySqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    // Read values from columns
    //                    string readout = reader.GetString(0); // Assuming the searched value is at index 1

    //                    // Do something with the values...
    //                    listStorageTubesArticleCreationMaterials.Items.Add(readout);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show("Error: " + ex.Message);
    //    }
    //}


    //Admin Page

    //private void btnAdminSaveDocLocation_Click(object sender, EventArgs e)
    //{
    //    using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
    //    {
    //        folderDialog.Description = "Select a folder";
    //        folderDialog.ShowNewFolderButton = false; // Optional: Allows or disallows creating a new folder

    //        // Display the dialog and check if user selected a folder
    //        if (folderDialog.ShowDialog() == DialogResult.OK)
    //        {
    //            // Save the selected folder path to a variable or display it
    //            string selectedFolderPath = folderDialog.SelectedPath;

    //            PPcN.Properties.Settings.Default.DocumentSaveLocation = selectedFolderPath;
    //            PPcN.Properties.Settings.Default.Save();
    //        }
    //    }
    //}

    //Add Suppliers

    //private void PopulateComboLists()
    //{
    //    cmbCoilsNewCoilMaterial.Items.Clear();
    //    try
    //    {
    //        using (MySqlConnection connection = new MySqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            string query = $"SELECT * FROM `prostahl`.`werkstoffe` WHERE CONCAT_WS(' ', `Werkstoff`) ORDER BY `Werkstoff` ASC LIMIT 1000;\r\n";
    //            MySqlCommand command = new MySqlCommand(query, connection);

    //            using (MySqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    // Read values from columns
    //                    string readout = reader.GetString(1); // Assuming the searched value is at index 1
    //                                                          // Do something with the values...
    //                    cmbCoilsNewCoilMaterial.Items.Add(readout);
    //                    cmbCoilsSearchGrade.Items.Add(readout);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show("Error: " + ex.Message);
    //    }


    //    cmbCoilsNewCoilSupplier.Items.Clear();

    //    try
    //    {
    //        using (MySqlConnection connection = new MySqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            string query = $"SELECT * FROM `prostahl`.`lieferanten` ORDER BY `Name` ASC LIMIT 1000;\r\n";
    //            MySqlCommand command = new MySqlCommand(query, connection);

    //            using (MySqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    // Read values from columns
    //                    string readout = reader.GetString(1); // Assuming the searched value is at index 1
    //                                                          // Do something with the values...
    //                    cmbCoilsNewCoilSupplier.Items.Add(readout);
    //                    cmbCoilsSearchSupplier.Items.Add(readout);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show("Error: " + ex.Message);
    //    }
    //}

    //private void btnNewSupplier_Click(object sender, EventArgs e)
    //{

    //    // Display a MessageBox with two buttons
    //    DialogResult result = MessageBox.Show(
    //        $"Lieferant: {txtNewSupplierName.Text ?? ""}",
    //        "Confirm Details", // Title of the message box
    //        MessageBoxButtons.YesNo // Buttons for the message box
    //    );



    //    if (!String.IsNullOrEmpty(txtNewSupplierName.Text) && result == DialogResult.Yes)
    //    {
    //        try
    //        {
    //            using (MySqlConnection connection = new MySqlConnection(connectionString))
    //            {
    //                connection.Open();

    //                string query = @"INSERT INTO `lieferanten` 
    //                            (`Name`)
    //                            VALUES
    //                            (@Name);";

    //                MySqlCommand command = new MySqlCommand(query, connection);

    //                // Add parameters
    //                command.Parameters.AddWithValue("@Name", txtNewSupplierName.Text);

    //                // Execute the query
    //                int rowsAffected = command.ExecuteNonQuery();

    //                // Check if the insertion was successful
    //                if (rowsAffected > 0)
    //                {
    //                    MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //                }
    //                else
    //                {
    //                    MessageBox.Show("Failed to insert data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //        }
    //    }
    //}














    // CHECK
    // FOLLOWING
    // NOT 
    // SURE
    // IF
    // BS
    // !!!




    //Load Articles

    //private void btnStorageTubesArticleCreationArticlesLoad_Click(object sender, EventArgs e)
    //{
    //    listStorageTubesArticleCreationArticlesOD.Items.Clear();
    //    listStorageTubesArticleCreationArticlesWT.Items.Clear();
    //    listStorageTubesArticleCreationArticlesMaterial.Items.Clear();
    //    listStorageTubesArticleCreationArticlesArtNo.Items.Clear();

    //    try
    //    {
    //        using (MySqlConnection connection = new MySqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            string query = "SELECT DISTINCT `Auﬂendurchmesser` FROM `artikelrohre` ORDER BY `Auﬂendurchmesser` ASC;";
    //            MySqlCommand command = new MySqlCommand(query, connection);
    //            Debug.WriteLine(query);

    //            using (MySqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    // Read values from columns
    //                    decimal readout = reader.GetDecimal(0); // Column index should be 0 for the first and only column
    //                    string readoutString = readout.ToString();

    //                    // Do something with the values...
    //                    listStorageTubesArticleCreationArticlesOD.Items.Add(readoutString);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        Debug.WriteLine(ex.Message);
    //    }



    //    try
    //    {
    //        using (MySqlConnection connection = new MySqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            string query = "SELECT DISTINCT `Wandst‰rke` FROM `artikelrohre` ORDER BY `Wandst‰rke` ASC;";
    //            MySqlCommand command = new MySqlCommand(query, connection);
    //            Debug.WriteLine(query);

    //            using (MySqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    // Read values from columns
    //                    decimal readout = reader.GetDecimal(0); // Column index should be 0 for the first and only column
    //                    string readoutString = readout.ToString();

    //                    // Do something with the values...
    //                    listStorageTubesArticleCreationArticlesWT.Items.Add(readoutString);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        Debug.WriteLine(ex.Message);
    //    }



    //    try
    //    {
    //        using (MySqlConnection connection = new MySqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            string query = "SELECT DISTINCT `Werkstoff` FROM `artikelrohre` ORDER BY `Werkstoff` ASC;";
    //            MySqlCommand command = new MySqlCommand(query, connection);
    //            Debug.WriteLine(query);

    //            using (MySqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    // Read values from columns
    //                    string readout = reader.GetString(0); // Column index should be 0 for the first and only column

    //                    // Do something with the values...
    //                    listStorageTubesArticleCreationArticlesMaterial.Items.Add(readout);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        Debug.WriteLine(ex.Message);
    //    }



    //    try
    //    {
    //        using (MySqlConnection connection = new MySqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            string query = "SELECT DISTINCT `Artikelnummer` FROM `artikelrohre` ORDER BY `Artikelnummer` ASC;";
    //            MySqlCommand command = new MySqlCommand(query, connection);
    //            Debug.WriteLine(query);

    //            using (MySqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    // Read values from columns
    //                    int readout = reader.GetInt16(0); // Column index should be 0 for the first and only column
    //                    string readoutString = readout.ToString();

    //                    // Do something with the values...
    //                    listStorageTubesArticleCreationArticlesArtNo.Items.Add(readoutString);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        Debug.WriteLine(ex.Message);
    //    }
    //}

    //private void listStorageTubesArticleCreationArticlesOD_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string OD = null;
    //    string WT = null;
    //    string Material = null;

    //    if (listStorageTubesArticleCreationArticlesOD.SelectedItem != null)
    //    {
    //        OD = listStorageTubesArticleCreationArticlesOD.SelectedItem.ToString().Replace(',', '.');
    //    }

    //    if (listStorageTubesArticleCreationArticlesWT.SelectedItem != null)
    //    {
    //        WT = listStorageTubesArticleCreationArticlesWT.SelectedItem.ToString().Replace(',', '.');
    //    }

    //    if (listStorageTubesArticleCreationArticlesMaterial.SelectedItem != null)
    //    {
    //        Material = listStorageTubesArticleCreationArticlesMaterial.SelectedItem.ToString();
    //    }

    //    listStorageTubesArticleFilterNext(OD, WT, Material);
    //}

    //private void listStorageTubesArticleCreationArticlesWT_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    MessageBox.Show("4");
    //    string OD = null;
    //    string WT = null;
    //    string Material = null;

    //    if (listStorageTubesArticleCreationArticlesOD.SelectedItem != null)
    //    {
    //        OD = listStorageTubesArticleCreationArticlesOD.SelectedItem.ToString().Replace(',', '.');
    //    }

    //    if (listStorageTubesArticleCreationArticlesWT.SelectedItem != null)
    //    {
    //        WT = listStorageTubesArticleCreationArticlesWT.SelectedItem.ToString().Replace(',', '.');
    //    }

    //    if (listStorageTubesArticleCreationArticlesMaterial.SelectedItem != null)
    //    {
    //        Material = listStorageTubesArticleCreationArticlesMaterial.SelectedItem.ToString();
    //    }

    //    listStorageTubesArticleFilterNext(OD, WT, Material);
    //}

    //private void listStorageTubesArticleCreationArticlesMaterial_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string OD = null;
    //    string WT = null;
    //    string Material = null;

    //    if (listStorageTubesArticleCreationArticlesOD.SelectedItem != null)
    //    {
    //        OD = listStorageTubesArticleCreationArticlesOD.SelectedItem.ToString().Replace(',', '.');
    //    }

    //    if (listStorageTubesArticleCreationArticlesWT.SelectedItem != null)
    //    {
    //        WT = listStorageTubesArticleCreationArticlesWT.SelectedItem.ToString().Replace(',', '.');
    //    }

    //    if (listStorageTubesArticleCreationArticlesMaterial.SelectedItem != null)
    //    {
    //        Material = listStorageTubesArticleCreationArticlesMaterial.SelectedItem.ToString();
    //    }

    //    listStorageTubesArticleFilterNext(OD, WT, Material);
    //}

    //private void listStorageTubesArticleFilterNext(string OD, string WT, string Material)
    //{
    //    listStorageTubesArticleCreationArticlesArtNo.Items.Clear();

    //    string query = "SELECT `Artikelnummer` FROM `artikelrohre` ";
    //    bool firstvalue = true;

    //    if (OD != null)
    //    {
    //        if (firstvalue)
    //        {
    //            query += $"WHERE ";
    //            firstvalue = false;
    //        }
    //        query += $"`Auﬂendurchmesser` = {OD} ";
    //    }

    //    if (WT != null)
    //    {
    //        if (firstvalue)
    //        {
    //            query += $"WHERE ";
    //            firstvalue = false;
    //        }
    //        else
    //        {
    //            query += $" AND ";
    //        }

    //        query += $"`Wandst‰rke` = {WT} ";
    //    }

    //    if (Material != null)
    //    {
    //        if (firstvalue)
    //        {
    //            query += $"WHERE ";
    //            firstvalue = false;
    //        }
    //        else
    //        {
    //            query += $" AND ";
    //        }
    //        query += $"`Werkstoff` = {Material} ";
    //    }

    //    query += $"ORDER BY `Artikelnummer` ASC;";


    //    try
    //    {
    //        using (MySqlConnection connection = new MySqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            MySqlCommand command = new MySqlCommand(query, connection);
    //            Debug.WriteLine(query);

    //            using (MySqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    // Read values from columns
    //                    int readout = reader.GetInt16(0); // Column index should be 0 for the first and only column
    //                    string readoutString = readout.ToString();

    //                    // Do something with the values...
    //                    listStorageTubesArticleCreationArticlesArtNo.Items.Add(readoutString);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        Debug.WriteLine(ex.Message);
    //    }
    //}
}