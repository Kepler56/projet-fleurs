using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Oscar_PASTURAL_Sascha_CAUCHON_TDC
{
    public partial class Commande : System.Web.UI.Page
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


                command.CommandText = "SELECT num_commande, date_commande, date_livraison, adresse_livraison, message FROM commande;";

                //Nous avons stocké les données dans un objet DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTableCommandes = new DataTable();
                adapter.Fill(dataTableCommandes);

                //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
                this.dataTableCommandes.DataSource = dataTableCommandes;
                this.dataTableCommandes.DataBind();
                connexion.Close();

            }
        }

        /// <summary>
        /// Méthode pour passer dans chaque filtres du module "Commandes" pour vérifier plusieurs types d'informations
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

            switch (filtresCommande)
            {
                case "commandes":
                    pnlTypeCommande.Visible = false;
                    pnlEtatCommande.Visible = false;
                    pnlClientCommande.Visible = false;
                    command.CommandText = "SELECT num_commande, date_commande, date_livraison, adresse_livraison, message FROM commande;";
                    break;
                case "etats":
                    pnlTypeCommande.Visible = false;
                    pnlEtatCommande.Visible = true;
                    pnlClientCommande.Visible = false;
                    command.CommandText = "SELECT num_commande, date_commande, date_livraison, adresse_livraison, code_commande FROM commande";
                    break;
                case "boutique":
                    pnlTypeCommande.Visible = false;
                    pnlEtatCommande.Visible = false;
                    pnlClientCommande.Visible = false;
                    command.CommandText = "select num_commande, date_commande, date_livraison, adresse_livraison, boutique.num_boutique " +
                        "from commande join  boutique on commande.num_boutique = boutique.num_boutique;";
                    break;
                case "type":
                    command.CommandText = "SELECT num_commande, date_commande, date_livraison, adresse_livraison, message FROM commande;";
                    pnlTypeCommande.Visible = true;
                    pnlEtatCommande.Visible = false;
                    pnlClientCommande.Visible = false;
                    break;
                case "client":
                    pnlTypeCommande.Visible = false;
                    pnlEtatCommande.Visible = false;
                    pnlClientCommande.Visible = true;
                    command.CommandText = "SELECT commande.num_commande, commande.code_commande, adresse_livraison, clients.id_client FROM commande " +
                        "JOIN clients ON commande.id_client = clients.id_client;";
                    break;
                default:
                    command.CommandText = "";
                    break;
            }

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTableCommandes = new DataTable();
            adapter.Fill(dataTableCommandes);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableCommandes.DataSource = dataTableCommandes;
            this.dataTableCommandes.DataBind();

            connexion.Close();
        }

        /// <summary>
        /// Retourner dans la page avec tous les modules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }

        /// <summary>
        /// Sélectionner s'il s'agit d'une commande standard ou personnalisée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectType_Click(object sender, EventArgs e)
        {
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

            if (txtTypeCommande.Text.ToUpper() == "STANDARD")
            {
                lblTypeComamnde.Text = "Commande Standard";
                command.CommandText = "select id_standard, num_commande, bouquet_standard.nom_bouquet, designer from commande_standard inner join bouquet_standard on commande_standard.num_bouquet = bouquet_standard.num_bouquet;";
            }
            else if(txtTypeCommande.Text.ToUpper() == "PERSONNALISEE")
            {
                lblTypeComamnde.Text = "Commande Personnalisée";
                command.CommandText = "select id_personnalisee, num_commande, description_client, prix_max from commande_personnalisee;";
            }
            else
            {
                lblTypeComamnde.Text = "Sélectionner entre 'Standard' et 'Personnalisee'.";
            }

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTableTypeCommande = new DataTable();
            adapter.Fill(dataTableTypeCommande);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableTypeCommande.DataSource = dataTableTypeCommande;
            this.dataTableTypeCommande.DataBind();

            connexion.Close();

        }

        /// <summary>
        /// Sélectionner l'état de commande pour voir plus précisément de quel type de commande il s'agit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectEtat_Click(object sender, EventArgs e)
        {
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

            if (txtEtatCommande.Text.ToUpper() == "VINV")
            {
                lblEtatCommande.Text = "Employé doit vérifier l'inventaire";
                command.CommandText = "SELECT num_commande, date_commande, date_livraison, adresse_livraison, code_commande FROM commande WHERE code_commande = \"VINV\";";
            }
            else if (txtEtatCommande.Text.ToUpper() == "CC")
            {
                lblEtatCommande.Text = "Commande complète";
                command.CommandText = "SELECT num_commande, date_commande, date_livraison, adresse_livraison, code_commande FROM commande WHERE code_commande = \"CC\";";
            }
            else if (txtEtatCommande.Text.ToUpper() == "CPAV")
            {
                lblEtatCommande.Text = "Commande personnalisée à vérifier";
                command.CommandText = "SELECT num_commande, date_commande, date_livraison, adresse_livraison, code_commande FROM commande WHERE code_commande = \"CPAV\";";
            }
            else if (txtEtatCommande.Text.ToUpper() == "CAL")
            {
                lblEtatCommande.Text = "Commande à livrer";
                command.CommandText = "SELECT num_commande, date_commande, date_livraison, adresse_livraison, code_commande FROM commande WHERE code_commande = \"CAL\";";
            }
            else if (txtEtatCommande.Text.ToUpper() == "CL")
            {
                lblEtatCommande.Text = "Commande livrée";
                command.CommandText = "SELECT num_commande, date_commande, date_livraison, adresse_livraison, code_commande FROM commande WHERE code_commande = \"CL\";";
            }
            else
            {
                lblEtatCommande.Text = "Sélectionner entre 'VINV', 'CC', 'CPAV', 'CAL' et 'CL'.";
            }

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTableEtatCommande = new DataTable();
            adapter.Fill(dataTableEtatCommande);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableEtatCommande.DataSource = dataTableEtatCommande;
            this.dataTableEtatCommande.DataBind();

            connexion.Close();
        }

        /// <summary>
        /// Sélectionner la comamnde éffectuer par client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClientCommande_Click(object sender, EventArgs e)
        {
            //Entre l'identifiant qu'il veut voir
            int id = Convert.ToInt32(txtClientCommande.Text);

            //vérifie si l'identifiant fait partie de la table
            string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connexionStr);
            MySqlCommand c = connexion.CreateCommand();

            MySqlParameter id_client = new MySqlParameter("@id_client", MySqlDbType.Int32);

            id_client.Value = id;

            c.CommandText = "select count(*) from clients where id_client = @id_client;";
            c.Parameters.AddWithValue("@id_client", id);

            connexion.Open();
            object result = c.ExecuteScalar();
            int count = Convert.ToInt32(result);
            connexion.Close();

            if (count > 0)
            {
                MySqlCommand c2 = connexion.CreateCommand();

                c2.CommandText = "SELECT commande.num_commande, commande.code_commande, adresse_livraison, clients.id_client " +
                    "FROM commande JOIN clients ON commande.id_client = clients.id_client WHERE clients.id_client=@id_client;";
                c2.Parameters.AddWithValue("@id_client", id);


                connexion.Open();
               
                //Nous avons stocké les données dans un objet DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(c2);
                DataTable dataTableClientCommande = new DataTable();
                adapter.Fill(dataTableClientCommande);

                //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
                this.dataTableClientCommande.DataSource = dataTableClientCommande;
                this.dataTableClientCommande.DataBind();

                connexion.Close();
            }
            else
            {
                lblClientCommande.Text = "Le client n'existe pas ou n'as pas encore fait de commande.";
            }
        }
    }
}