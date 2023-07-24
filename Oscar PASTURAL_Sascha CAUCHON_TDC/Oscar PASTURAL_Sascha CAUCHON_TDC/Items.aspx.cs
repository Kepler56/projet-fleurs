using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Oscar_PASTURAL_Sascha_CAUCHON_TDC
{
    public partial class Items : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Ouverture de la connexion
                MySqlConnection connexion = null;

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


                command.CommandText = "SELECT * FROM items;";

                //Nous avons stocké les données dans un objet DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTableItems = new DataTable();
                adapter.Fill(dataTableItems);

                //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
                this.dataTableItems.DataSource = dataTableItems;
                this.dataTableItems.DataBind();
                connexion.Close();
                
            }
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }

        /// <summary>
        /// Méthode pour passer dans chaque filtres du module "Client" pour vérifier plusieurs types d'informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFiltres_SelectedIndexChanged(object sender, EventArgs e)
        {
            //command.CommandText = "SELECT * FROM items;";

            //Ouverture de la connexion
            MySqlConnection connexion = null;
            string filtresClients = ddlFiltres.Text;

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

            switch (filtresClients)
            {
                case "produits":
                    pnlVerifieStock.Visible = false;
                    pnlMettreAJourStock.Visible = false;
                    command.CommandText = "select * from items;";
                    break;
                case "bouquets_standards":
                    pnlVerifieStock.Visible = false;
                    pnlMettreAJourStock.Visible = false;
                    command.CommandText = "SELECT * FROM bouquet_standard;";
                    break;
                case "fleurs":
                    pnlVerifieStock.Visible = false;
                    pnlMettreAJourStock.Visible = false;
                    command.CommandText = "select * from items where type_item = 'Fleur';";
                    break;
                case "accessoires":
                    pnlVerifieStock.Visible = false;
                    pnlMettreAJourStock.Visible = false;
                    command.CommandText = "select * from items where type_item = 'Accessoire';";
                    break;
                case "quantite":
                    //Quantité par ordre croissant
                    pnlVerifieStock.Visible = false;
                    pnlMettreAJourStock.Visible = false;
                    command.CommandText = "select * from items order by stock;";
                    break;
                case "prix":
                    //Prix par ordre croissant
                    pnlVerifieStock.Visible = false;
                    pnlMettreAJourStock.Visible = false;
                    command.CommandText = "select * from items order by prix_item;";
                    break;
                case "disponibilite":
                    //Afficher les produits indisponibles
                    pnlVerifieStock.Visible = false;
                    pnlMettreAJourStock.Visible = true;
                    command.CommandText = "select id_items, nom_item, disponibilite from items";
                    break;
                case "verifieStock":
                    pnlMettreAJourStock.Visible = false;
                    pnlVerifieStock.Visible = true;
                    GetStockCount();
                    command.CommandText = "select id_items, nom_item, stock, type_item from items where stock <= 3;";
                    break;
                default:
                    command.CommandText = "";
                    break;
            }

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTableItems = new DataTable();
            adapter.Fill(dataTableItems);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableItems.DataSource = dataTableItems;
            this.dataTableItems.DataBind();
            connexion.Close();
        }

        /// <summary>
        /// Vérifier le stock d'un items
        /// </summary>
        public void GetStockCount()
        {
            string message = "";

            string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connexionStr);
            connexion.Open();


            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "select id_items,stock from items;";

            string id_items = "";
            string stock = "";

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id_items = reader.GetString(0);
                stock = reader.GetString(1);
                if (Convert.ToInt32(stock) <= 3)
                {
                    message += "Stock insuffisant pour le produit avec un identifiant : " + id_items + "<br>";
                }
            }
            reader.Close();

            lblStockCount.Text = message;
            connexion.Close();
        }

        /// <summary>
        /// Mettre à jour la quantité d'un produit s'il est en dessous d'une limite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Entre l'identifiant du produit qu'il veut mettre à jour et la quantité
            int id = Convert.ToInt32(txtItemId.Text);
            int amount_clavier = Convert.ToInt32(txtAmount.Text);

            //Check if the id entered is part of the table
            string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connexionStr);
            MySqlCommand c1 = connexion.CreateCommand();

            MySqlParameter id_item = new MySqlParameter("@id_item", MySqlDbType.Int32);

            id_item.Value = id;

            c1.CommandText = "select count(*) from items where id_items = @id_item and stock <= 3;";
            c1.Parameters.AddWithValue("@id_item", id);

            connexion.Open();
            object result = c1.ExecuteScalar();
            int count = Convert.ToInt32(result);
            connexion.Close();

            if (count > 0)
            {
                //Add the new amount to the product 
                MySqlCommand c2 = connexion.CreateCommand();

                MySqlParameter amount = new MySqlParameter("@amount", MySqlDbType.Int32);

                amount.Value = amount_clavier;

                c2.CommandText = "update items SET stock = stock + @amount where id_items = @id_item;";
                c2.Parameters.AddWithValue("@id_item", id);
                c2.Parameters.AddWithValue("@amount", amount_clavier);

                connexion.Open();
                c2.ExecuteNonQuery();
                connexion.Close();

                lblSuccessful.Text = "Le montant a bien été inscrit.";
            }
            else
            {
                lblWrongId.Text = "Le produit a déjà un stock suffisant.";
            }

        }

        /// <summary>
        /// Mettre à jour la disponibilité d'un produit
        /// S'il est en dessous de 3 alors on change la disponibilité à FALSE
        /// S'il est au dessus de 3 alors on change la disponibilité à TRUE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMAJStock_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            using (MySqlConnection connexion = new MySqlConnection(connectionString))
            {
                try
                {
                    connexion.Open();
                    MySqlCommand commandDisponibilite = connexion.CreateCommand();
                    commandDisponibilite.CommandText = "UPDATE items SET disponibilite = CASE WHEN stock <= 3 THEN FALSE ELSE TRUE END;";
                    int rowsAffected = commandDisponibilite.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblSuccessMAJStock.Text = "La disponibilité des articles a été mise à jour.";
                        lblErreur.Text = "";
                    }
                    else
                    {
                        lblSuccessMAJStock.Text = "";
                        lblErreur.Text = "Aucun article n'a été mis à jour.";
                    }
                }
                catch (Exception ex)
                {
                    lblSuccessMAJStock.Text = "";
                    lblErreur.Text = $"Une erreur s'est produite lors de la mise à jour de la disponibilité des articles : {ex.Message}";
                }
            }
        }
    }
}
