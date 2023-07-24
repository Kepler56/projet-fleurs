using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace Oscar_PASTURAL_Sascha_CAUCHON_TDC
{
    public partial class OnlineShopPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblBienvenue.Text = "Bienvenue dans le catalogue numérique";
            }
        }


        /// <summary>
        /// Méthode pour passer dans chaque filtres du module "OnlineShopPage" pour vérifier plusieurs types d'informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFiltres_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection connexion = null;
            string filtresCommande = ddlFiltres.Text;

            try
            {
                string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
                connexion = new MySqlConnection(connexionStr);
                connexion.Open();
            }
            catch (MySqlException)
            {
                Console.WriteLine("ErreurConnexion :" + e.ToString());
                return;
            }

            MySqlCommand command = new MySqlCommand();
            command.Connection = connexion;
            MySqlDataAdapter adapter = null;


            switch (filtresCommande)
            {
                case "infos":
                    pnlInformationsClients.Visible = true;
                    pnlBoutiquesStandards.Visible = false;
                    pnlBoutiquesPersonnalises.Visible = false;
                    pnlProduits.Visible = false;
                    pnlSuivreCommande.Visible = false;
                    GetInformationClient();                  
                    break;
                case "produits":
                    pnlBoutiquesStandards.Visible = false;
                    pnlBoutiquesPersonnalises.Visible = false;
                    pnlProduits.Visible = true;
                    pnlSuivreCommande.Visible = false;
                    pnlInformationsClients.Visible = false;
                    command.CommandText = "SELECT * FROM items;";

                    //Nous avons stocké les données dans un objet DataTable
                    adapter = new MySqlDataAdapter(command);
                    DataTable dataTableProduits = new DataTable();
                    adapter.Fill(dataTableProduits);

                    //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
                    this.dataTableProduits.DataSource = dataTableProduits;
                    this.dataTableProduits.DataBind();
                    connexion.Close();
                    break;
                case "standard":
                    pnlBoutiquesStandards.Visible = true;
                    pnlBoutiquesPersonnalises.Visible = false;
                    pnlProduits.Visible = false;
                    pnlSuivreCommande.Visible = false;
                    pnlInformationsClients.Visible = false;
                    command.CommandText = "select * from bouquet_standard;";

                    //Nous avons stocké les données dans un objet DataTable
                    adapter = new MySqlDataAdapter(command);
                    DataTable dataTableBoutiquesStandards = new DataTable();
                    adapter.Fill(dataTableBoutiquesStandards);

                    //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
                    this.dataTableBoutiquesStandards.DataSource = dataTableBoutiquesStandards;
                    this.dataTableBoutiquesStandards.DataBind();
                    break;
                case "personnalise":
                    pnlBoutiquesStandards.Visible = false;
                    pnlBoutiquesPersonnalises.Visible = true;
                    pnlProduits.Visible = false;
                    pnlSuivreCommande.Visible = false;
                    pnlInformationsClients.Visible = false;
                    command.CommandText = "select id_items, nom_item, prix_item, type_item, disponibilite from items;";

                    //Nous avons stocké les données dans un objet DataTable
                    adapter = new MySqlDataAdapter(command);
                    DataTable dataTableBoutiquesPersonnalises = new DataTable();
                    adapter.Fill(dataTableBoutiquesPersonnalises);

                    //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
                    this.dataTableBoutiquesPersonnalises.DataSource = dataTableBoutiquesPersonnalises;
                    this.dataTableBoutiquesPersonnalises.DataBind();
                    break;
                case "suivreCommande":
                    pnlBoutiquesStandards.Visible = false;
                    pnlBoutiquesPersonnalises.Visible = false;
                    pnlProduits.Visible = false;
                    pnlSuivreCommande.Visible = true;
                    pnlInformationsClients.Visible = false;
                    command.CommandText = "select num_commande, prix, date_livraison, code_commande " +
                        "from commande join clients on commande.id_client = clients.id_client " +
                        "where clients.id_client = @id_client;";
                    command.Parameters.AddWithValue("@id_client", Session["id_client"]);

                    //Nous avons stocké les données dans un objet DataTable
                    adapter = new MySqlDataAdapter(command);
                    DataTable dataTableSuivreCommande = new DataTable();
                    adapter.Fill(dataTableSuivreCommande);

                    //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
                    this.dataTableSuivreCommande.DataSource = dataTableSuivreCommande;
                    this.dataTableSuivreCommande.DataBind();

                    break;
                default:
                    command.CommandText = "";
                    break;
            }

            connexion.Close();
        }

        protected void dataTableBoutiquesStandards_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "select")
            {
                if (int.TryParse(e.CommandArgument.ToString(), out int numBouquet) && numBouquet > 0)
                {
                    Session["bouquetSelectionne"] = numBouquet;
                    lblBouquetSelectionner.Text = $"Bouquet numéro {numBouquet} est sélectionné, confirmer votre commande.";
                }
            }

        }

        protected void btnConfirmerCommandeStandards_Click(object sender, EventArgs e)
        {
           // Générer les variables
            string numCommande = GetNextCommandNumber();
            DateTime dateCommande = DateTime.Today;
            string dateCommandeString = dateCommande.ToString("yyyy-MM-dd");
            int numBoutique = GetBoutiqueNumber();
            int prixBouquet = GetPrixBouquet();
            string dateLivraison = txtDateLivraison.Text;
            string adresseLivraison = txtAdresseLivraison.Text;
            string message = txtMessage.Text;
            string codeCommande = "";
                
            int reduction = GetReduction();
            int prix_bouquet_reduction = prixBouquet - (prixBouquet * reduction / 100);

            // Calculer la différence entre les deux dates
            TimeSpan difference = DateTime.Parse(dateLivraison) - dateCommande;

            // Récupérer le nombre de jours de la différence
            int differenceEnJours = difference.Days;

            // Vérifier si la commande a été passée au moins 3 jours avant la date de livraison
            if (differenceEnJours >= 3)
            {
                codeCommande = "CC";   
            }
            else
            {
                codeCommande = "VINV";
                lblPenurie.Text = "Attention! Comme la commande est effectuée moins de 3 jours avant la date de livraison, il peut y avoir une éventuelle pénurie";
            }

            // Insérer la commande dans la table "commande"
            MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=fleurs;Uid=root;Pwd=root;");
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "INSERT INTO commande VALUES (@numCommande, @dateCommande, @dateLivraison, @adresseLivraison, @message, @codeCommande, @prix, @idClient, @numBoutique)";
            command.Parameters.AddWithValue("@numCommande", numCommande);
            command.Parameters.AddWithValue("@dateCommande", dateCommandeString);
            command.Parameters.AddWithValue("@dateLivraison", dateLivraison);
            command.Parameters.AddWithValue("@adresseLivraison", adresseLivraison);
            command.Parameters.AddWithValue("@message", message);
            command.Parameters.AddWithValue("@codeCommande", codeCommande);
            command.Parameters.AddWithValue("@prix", prix_bouquet_reduction);
            command.Parameters.AddWithValue("@idClient", Session["id_client"]);
            command.Parameters.AddWithValue("@numBoutique", numBoutique);
            connexion.Open();
            command.ExecuteNonQuery();
            connexion.Close();

            // Insérer le bouquet sélectionné dans la table "commande_standard"
            int numCommandeStandard = GetNextCommandeStandardNumber();

            MySqlCommand command2 = connexion.CreateCommand();
            command2.CommandText = "INSERT INTO commande_standard VALUES (@id_standard, @numCommande, @numBouquet, @designer)";
            command2.Parameters.AddWithValue("@id_standard", numCommandeStandard);
            command2.Parameters.AddWithValue("@numCommande", numCommande);
            command2.Parameters.AddWithValue("@numBouquet", Convert.ToInt32(Session["bouquetSelectionne"]));
            string designer = "";
            if (Convert.ToInt32(Session["bouquetSelectionne"]) == 1)
            {
                designer = "Fabien";
            }
            else if (Convert.ToInt32(Session["bouquetSelectionne"]) == 2)
            {
                designer = "Yves";
            }
            else if (Convert.ToInt32(Session["bouquetSelectionne"]) == 3)
            {
                designer = "Jean";
            }
            else if (Convert.ToInt32(Session["bouquetSelectionne"]) == 4)
            {
                designer = "Laura";
            }
            else if (Convert.ToInt32(Session["bouquetSelectionne"]) == 5)
            {
                designer = "Amélie";
            }
            else
            {
                designer = "Anonymas";
            }
            command2.Parameters.AddWithValue("@designer", designer);
            connexion.Open();
            command2.ExecuteNonQuery();
            connexion.Close();

            // Afficher un message de confirmation et réinitialiser la variable du bouquet sélectionné
            lblConfirmationCommande.Text = $"Votre commande a été enregistrée. Le prix final de votre bouquet est {prix_bouquet_reduction}";            
            MiseAJoursStocksItems();

            Session["bouquetSelectionne"] = null; // Réinitialiser la variable de session
        }

        public int GetNextCommandeStandardNumber()
        {
            MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=fleurs;Uid=root;Pwd=root;");
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM commande_standard";

            connexion.Open();
            int nombreDeCommande = Convert.ToInt32(command.ExecuteScalar());
            connexion.Close();

            int nextCommandNumber = nombreDeCommande + 1;

            return nextCommandNumber;
        }

        public int GetNextCommandePersonnaliseeNumber()
        {
            MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=fleurs;Uid=root;Pwd=root;");
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "select COUNT(*) from commande_personnalisee;";

            connexion.Open();
            int nombreDeCommande = Convert.ToInt32(command.ExecuteScalar());
            connexion.Close();

            int nextCommandNumber = nombreDeCommande + 1;

            return nextCommandNumber;
        }

        public string GetNextCommandNumber()
        {
            MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=fleurs;Uid=root;Pwd=root;");
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM commande";

            connexion.Open();
            int nombreDeCommande = Convert.ToInt32(command.ExecuteScalar());
            connexion.Close();

            string nextCommandNumber = "COM" + Convert.ToString(nombreDeCommande + 1);
            return nextCommandNumber;
        }

        public int GetBoutiqueNumber()
        {
            int numBoutique = Convert.ToInt32(ddlBoutiques.Text);

            return numBoutique;
        }

        public void MiseAJoursStocksItems()
        {
            // Récupérer la composition du bouquet depuis la base de données
            string connectionString = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT id_items, quantite FROM composition WHERE num_bouquet = @numBouquet;";

            connexion.Open();
            command.Parameters.AddWithValue("@numBouquet", Convert.ToInt32(Session["bouquetSelectionne"]));
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idItem = reader.GetInt32(0);
                int quantite = reader.GetInt32(1);

                // Créer une nouvelle connexion pour l'updateCommand
                using (MySqlConnection updateConnexion = new MySqlConnection(connectionString))
                {
                    updateConnexion.Open();

                    // Mettre à jour le stock de l'item dans la base de données
                    MySqlCommand updateCommand = updateConnexion.CreateCommand();
                    updateCommand.CommandText = "UPDATE items SET stock = stock - @quantite WHERE id_items = @idItem;";

                    updateCommand.Parameters.AddWithValue("@quantite", quantite);
                    updateCommand.Parameters.AddWithValue("@idItem", idItem);
                    updateCommand.ExecuteNonQuery();
                }
            }
            reader.Close();
            connexion.Close();
        }

        public int GetPrixBouquet()
        {
            MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=fleurs;Uid=root;Pwd=root;");
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT prix FROM bouquet_standard WHERE num_bouquet = @bouquetSelectionne";
            command.Parameters.AddWithValue("@bouquetSelectionne", Convert.ToInt32(Session["bouquetSelectionne"]));

            connexion.Open();
            int prixCommande = Convert.ToInt32(command.ExecuteScalar());
            connexion.Close();

            return prixCommande;
        }

        public void GetInformationClient()
        {
            // Récupérer les informations d'un client depuis la base de données
            string connectionString = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "select nom_client, date_inscription, carte_credit from clients where id_client = @id_client;";

            connexion.Open();
            command.Parameters.AddWithValue("@id_client", Session["id_client"]);
            MySqlDataReader reader = command.ExecuteReader();
            string nom_client = "";
            string date_inscrption = "";
            string carte = "";
            while (reader.Read())
            {
                 nom_client = reader.GetString(0);
                 date_inscrption = reader.GetString(1);
                 carte = reader.GetString(2);
            }
            reader.Close();

            lblIDClient.Text = Session["id_client"].ToString();
            lblNom.Text = nom_client;
            lblCreation.Text = date_inscrption;
            lblCarteCredit.Text = carte;

            connexion.Close();
        }

        public int GetReduction()
        {
            // Récupérer la réduction d'un client depuis la base de données
            string connectionString = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            using (MySqlConnection connexion = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connexion.CreateCommand();
                command.CommandText = "SELECT statut.reduction FROM statut JOIN clients ON statut.type_fidelite = clients.type_fidelite WHERE id_client = @id_client;";
                command.Parameters.AddWithValue("@id_client", Session["id_client"]);
                int reduction_final = 0;

                connexion.Open();
                object reduction = command.ExecuteScalar();
                if (reduction != null && reduction != DBNull.Value)
                {
                    reduction_final = Convert.ToInt32(reduction);
                }
                connexion.Close();
                return reduction_final;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Button btnAdd = (Button)sender;
            GridViewRow row = (GridViewRow)btnAdd.NamingContainer;

            int itemId = Convert.ToInt32(btnAdd.CommandArgument);
            string message = "Vous avez sélectionner: ";

            int itemQuantity = Convert.ToInt32(((TextBox)row.FindControl("txtQuantity")).Text);

            Dictionary<int, int> cart = (Session["Cart"] as Dictionary<int, int>) ?? new Dictionary<int, int>();

            if (cart.ContainsKey(itemId))
            {
                cart[itemId] += itemQuantity;
            }
            else
            {
                cart.Add(itemId, itemQuantity);
            }

            message += itemId + " avec une quantité " + itemQuantity + "\n";
            Session["Cart"] = cart;
            lblCart.Text = message;
        }

        public decimal GetPrixBouquetP()
        {
            decimal totalPrice = 0;

            foreach (KeyValuePair<int, int> cart in (Dictionary<int, int>)Session["Cart"])
            {
                int Id = cart.Key;
                int Quantity = cart.Value;

                decimal itemPrice = GetItemPrice(Id);

                decimal itemTotalPrice = Quantity * itemPrice;

                totalPrice += itemTotalPrice;
            }
            return totalPrice;
        }

        public int GetItemPrice(int itemId)
        {
            int itemPrice = 0;
            // Insérer la commande dans la table "commande"
            MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=fleurs;Uid=root;Pwd=root;");
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "select prix_item from items where id_items = @id_item;";
            command.Parameters.AddWithValue("@id_item", itemId);

            connexion.Open();
            object result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                itemPrice = Convert.ToInt32(result);
            }

            connexion.Close();

            return itemPrice;
        }

        public string GetDescription()
        {
            string description_client = "Arrangement floral avec: ";

            foreach (KeyValuePair<int, int> cart in (Dictionary<int, int>)Session["Cart"])
            {
                description_client += cart.Key + " de quantité " + cart.Value + ", ";
            }

            return description_client;
        }

        protected void btnConfirmerCommandePersonnalisee_Click(object sender, EventArgs e)
        {
            // Générer les variables
            string numCommande = GetNextCommandNumber();
            DateTime dateCommande = DateTime.Today;
            string dateCommandeString = dateCommande.ToString("yyyy-MM-dd");
            int numBoutique = GetBoutiqueNumber();
            int prixBouquet = Convert.ToInt32(GetPrixBouquetP());
            string dateLivraison = txtDateLivraisonP.Text;
            string adresseLivraison = txtAdresseLivraisonP.Text;
            string message = txtMessageP.Text;
            string codeCommande = "CPAV";

            int reduction = GetReduction();

            int prix_bouquet_reduction = prixBouquet - (prixBouquet * reduction / 100);


            // Insérer la commande dans la table "commande"
            MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=fleurs;Uid=root;Pwd=root;");
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "INSERT INTO commande VALUES (@numCommande, @dateCommande, @dateLivraison, @adresseLivraison, @message, @codeCommande, @prix, @idClient, @numBoutique)";
            command.Parameters.AddWithValue("@numCommande", numCommande);
            command.Parameters.AddWithValue("@dateCommande", dateCommandeString);
            command.Parameters.AddWithValue("@dateLivraison", dateLivraison);
            command.Parameters.AddWithValue("@adresseLivraison", adresseLivraison);
            command.Parameters.AddWithValue("@message", message);
            command.Parameters.AddWithValue("@codeCommande", codeCommande);
            command.Parameters.AddWithValue("@prix", prix_bouquet_reduction);
            command.Parameters.AddWithValue("@idClient", Session["id_client"]);
            command.Parameters.AddWithValue("@numBoutique", numBoutique);
            connexion.Open();
            command.ExecuteNonQuery();
            connexion.Close();

            // Insérer dans la table "commande_personnnalisee"
            int numCommandePersonnalisee = GetNextCommandePersonnaliseeNumber();
            string description_Client = GetDescription();

            MySqlCommand command2 = connexion.CreateCommand();
            command2.CommandText = "INSERT INTO commande_personnalisee VALUES (@id_personnalisee, @numCommande, @description_client, @prix_max)";
            command2.Parameters.AddWithValue("@id_personnalisee", numCommandePersonnalisee);
            command2.Parameters.AddWithValue("@numCommande", numCommande);
            command2.Parameters.AddWithValue("@description_client", description_Client);
            command2.Parameters.AddWithValue("@prix_max", prixBouquet);
            connexion.Open();
            command2.ExecuteNonQuery();
            connexion.Close();

            // Insérer les items sélectionnés dans la table "confection"
            MySqlCommand command3 = connexion.CreateCommand();
            command3.CommandText = "INSERT INTO confection VALUES (@id_perso,@id_items,@quantite);";
            command3.Parameters.AddWithValue("@id_perso", numCommandePersonnalisee);
            command3.Parameters.AddWithValue("@id_items", 0);
            command3.Parameters.AddWithValue("@quantite", 0);

            foreach (KeyValuePair<int, int> cart in (Dictionary<int, int>)Session["Cart"])
            {
                command3.Parameters["@id_perso"].Value = numCommandePersonnalisee;
                command3.Parameters["@id_items"].Value = cart.Key;
                command3.Parameters["@quantite"].Value = cart.Value;
                connexion.Open();
                command3.ExecuteNonQuery();
                connexion.Close();
            }

            // Afficher un message de confirmation et réinitialiser la variable du bouquet sélectionné
            lblSuccessCommandeP.Text = "Votre commande a été enregistrée. Voici votre description de bouquet " + description_Client +
                $", le prix final de votre bouquet est {prix_bouquet_reduction}";


            //Vider les Sessions:
            Session["Cart"] = null;
            MiseAJoursStocksItemsP(numCommandePersonnalisee);

        }

        public void MiseAJoursStocksItemsP(int numCommandePersonnalisee)
        {
            // Récupérer la composition du bouquet depuis la base de données
            string connectionString = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT id_items, quantite FROM confection WHERE id_personnalisee=@numCommandeP;";

            connexion.Open();
            command.Parameters.AddWithValue("@numCommandeP", numCommandePersonnalisee);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idItem = reader.GetInt32(0);
                int quantite = reader.GetInt32(1);

                // Créer une nouvelle connexion pour l'updateCommand
                using (MySqlConnection updateConnexion = new MySqlConnection(connectionString))
                {
                    updateConnexion.Open();

                    // Mettre à jour le stock de l'item dans la base de données
                    MySqlCommand updateCommand = updateConnexion.CreateCommand();
                    updateCommand.CommandText = "UPDATE items SET stock = stock - @quantite WHERE id_items = @idItem;";

                    updateCommand.Parameters.AddWithValue("@quantite", quantite);
                    updateCommand.Parameters.AddWithValue("@idItem", idItem);
                    updateCommand.ExecuteNonQuery();
                }
            }
            reader.Close();
            connexion.Close();
        }

        protected void btnStatut_Click(object sender, EventArgs e)
        {
            int idClient = Convert.ToInt32(Session["id_client"]);

            string connectionString = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            using (MySqlConnection connexion = new MySqlConnection(connectionString))
            {
                connexion.Open();

                // Vérifier si le client est éligible au statut OR
                MySqlCommand command = connexion.CreateCommand();
                command.CommandText = "SELECT id_client, COUNT(*) AS nb_bouquets_achetes FROM commande WHERE id_client = @idClient AND MONTH(date_commande) = MONTH(CURDATE()) GROUP BY id_client HAVING nb_bouquets_achetes >= 5;";
                command.Parameters.AddWithValue("@idClient", idClient);
                MySqlDataReader reader = command.ExecuteReader();

                bool eligibiliteOR = reader.HasRows;
                reader.Close();

                // Vérifier si le client est éligible au statut Bronze
                command = connexion.CreateCommand();
                command.CommandText = "SELECT id_client, COUNT(*) AS nb_bouquets_achetes, DATEDIFF(MAX(date_commande), MIN(date_commande))/30 AS nb_mois FROM commande WHERE id_client = @idClient GROUP BY id_client HAVING (nb_bouquets_achetes / nb_mois) >= 1;";
                command.Parameters.AddWithValue("@idClient", idClient);
                reader = command.ExecuteReader();

                bool eligibiliteBronze = reader.HasRows;
                reader.Close();

                // Mettre à jour le statut de fidélité du client si nécessaire
                string nouveauStatut = "";
                if (eligibiliteOR)
                {
                    nouveauStatut = "OR";
                }
                else if (eligibiliteBronze)
                {
                    nouveauStatut = "BRONZE";
                }

                if (!string.IsNullOrEmpty(nouveauStatut))
                {
                    command = connexion.CreateCommand();
                    command.CommandText = "UPDATE clients SET type_fidelite = @nouveauStatut WHERE id_client = @idClient;";
                    command.Parameters.AddWithValue("@nouveauStatut", nouveauStatut);
                    command.Parameters.AddWithValue("@idClient", idClient);
                    command.ExecuteNonQuery();
                }

                // Afficher le statut de fidélité et réduction du client
                command = connexion.CreateCommand();
                command.CommandText = "SELECT type_fidelite FROM clients WHERE id_client = @idClient;";
                command.Parameters.AddWithValue("@idClient", idClient);
                object statutObj = command.ExecuteScalar();

                int reduction = GetReduction();

                string statut = statutObj != null ? statutObj.ToString() : "Aucun";
                lblStatut.Text = "Statut de fidélité: " + statut  + " avec une réduction de " + reduction + "%";

                connexion.Close();
            }
        }
    }
}