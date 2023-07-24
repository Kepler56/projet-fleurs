using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Oscar_PASTURAL_Sascha_CAUCHON_TDC
{
    public partial class Clients : System.Web.UI.Page
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

                
                command.CommandText = "SELECT id_client,nom_client,prenom_client FROM clients;";
               
                //Nous avons stocké les données dans un objet DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTableClients = new DataTable();
                adapter.Fill(dataTableClients);

                //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
                this.dataTableClients.DataSource = dataTableClients;
                this.dataTableClients.DataBind();
                connexion.Close();
            }
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
        /// Méthode pour passer dans chaque filtres du module "Client" pour vérifier plusieurs types d'informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFiltres_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                case "clients":
                    command.CommandText = "SELECT id_client,nom_client,prenom_client FROM clients;";
                    pnlSupprimerClient.Visible = true;
                    pnlMettreAJour.Visible = false;
                    break;
                case "or":
                    command.CommandText = "SELECT id_client,nom_client,prenom_client, type_fidelite FROM clients WHERE type_fidelite = 'OR';";
                    pnlSupprimerClient.Visible = false;
                    pnlMettreAJour.Visible = false;
                    break;
                case "bronze":
                    pnlSupprimerClient.Visible = false;
                    pnlMettreAJour.Visible = false;
                    command.CommandText = "SELECT id_client,nom_client,prenom_client, type_fidelite FROM clients WHERE type_fidelite = 'BRONZE';";
                    break;
                case "maj":
                    pnlSupprimerClient.Visible = false;
                    pnlMettreAJour.Visible = true;
                    command.CommandText = "SELECT id_client,nom_client,prenom_client, type_fidelite FROM clients WHERE type_fidelite = 'BRONZE' " +
                        "UNION " +
                        "SELECT id_client,nom_client,prenom_client, type_fidelite FROM clients WHERE type_fidelite = 'OR';";
                    break;
                case "contact":
                    pnlSupprimerClient.Visible = false;
                    pnlMettreAJour.Visible = false;
                    command.CommandText = "SELECT id_client,nom_client,prenom_client,num_tel,courrier FROM clients ;";
                    break;
                case "adresse":
                    pnlSupprimerClient.Visible = false;
                    pnlMettreAJour.Visible = false;
                    command.CommandText = "SELECT id_client,nom_client,prenom_client,adresse_facturation FROM clients ;";
                    break;
                default:
                    command.CommandText = "";
                    break;
            }

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTableClients = new DataTable();
            adapter.Fill(dataTableClients);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableClients.DataSource = dataTableClients;
            this.dataTableClients.DataBind();
            connexion.Close();
        }


        /// <summary>
        /// Méthode pour supprimer un client de la BDD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSupprimerClient_Click(object sender, EventArgs e)
        {
            //Entre l'Identifiant du client qu'il veut supprimer
            int id = Convert.ToInt32(txtIdClient.Text);

            //Vérifier que l'identifiant fait partie de la BDD
            string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connexionStr);
            MySqlCommand c = connexion.CreateCommand();

            c.CommandText = "select count(*) from commande where id_client = @id_client;";
            c.Parameters.AddWithValue("@id_client", id);

            connexion.Open();
            object result = c.ExecuteScalar();
            int count = Convert.ToInt32(result);
            connexion.Close();

            if (count > 0)
            {
                lblErreurSupp.Text = "Le client a fait une commande il ne peut pas être supprimé de la BDD";
                lblConfirmationSupp.Text = "";
            }
            else if (count == 0)
            {
                //Supprimer d'abord les commandes du clients     
                MySqlCommand c2 = connexion.CreateCommand();

                c2.CommandText = "delete from clients where id_client = @id_client;";
                c2.Parameters.AddWithValue("@id_client", id);


                connexion.Open();

                c2.ExecuteNonQuery();

                connexion.Close();
                lblConfirmationSupp.Text = "Le client à bien été supprimé de la BDD.";
                lblErreurSupp.Text = "";
            }
            else
            {
                lblConfirmationSupp.Text = "";
                lblErreurSupp.Text = "Le client n'existe pas dans la BDD.";
            }
        }

        /// <summary>
        /// Méthode pour mettre à jour le statut de fidélité des clients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMettreAJour_Click(object sender, EventArgs e)
        {
            string niveau = rblSelect.SelectedValue.ToUpper();

            // Mettre à jour le statut des clients en fonction de leur fidélité
            using (MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=fleurs;Uid=root;Pwd=root;"))
            {
                MySqlCommand commande = connexion.CreateCommand();
                int rowsAffected1 = 0;
                int rowsAffected2 = 0;
                connexion.Open();

                if (niveau == "OR")
                {
                    // Mettre à jour les clients ayant acheté 5 bouquets ou plus ce mois-ci
                    commande.CommandText = $"UPDATE clients SET type_fidelite = '{niveau}' WHERE id_client IN (SELECT id_client FROM commande WHERE MONTH(date_commande) = MONTH(CURRENT_DATE()) GROUP BY id_client HAVING COUNT(*) >= 5)";
                    rowsAffected1 = commande.ExecuteNonQuery();
                }

                if (niveau == "BRONZE")
                {
                    // Mettre à jour les clients ayant acheté au moins un bouquet par mois en moyenne
                    commande.CommandText = $"UPDATE clients SET type_fidelite = 'BRONZE' WHERE id_client IN (SELECT id_client FROM commande GROUP BY id_client HAVING COUNT(*) / NULLIF(TIMESTAMPDIFF(MONTH, MIN(date_commande), MAX(date_commande)), 0) >= 1)";
                    rowsAffected2 = commande.ExecuteNonQuery();
                }

                connexion.Close();

                int rowsAffected = rowsAffected1 + rowsAffected2;

                if (rowsAffected > 0)
                {
                    lblConfirmer.Text = "Les statuts des clients ont été mis à jour.";
                    lblErreur.Text = "";
                }
                else
                {
                    lblConfirmer.Text = "";
                    lblErreur.Text = "Aucun client n'a été mis à jour.";
                }
            }

        }
    }
}
