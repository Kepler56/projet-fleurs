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
    public partial class ClientRegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Méthode pour qu'un nouveau client puisse se créer un nouveau compte et rentrer les informations saisies dans la BDD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Prendre les valeurs des TextBox
            string nom = txtNom.Text;
            string prenom = txtPrenom.Text;
            string num_tel = txtTel.Text;
            string courrier = txtEmail.Text;
            string mot_de_passe = txtPassword.Text;
            string adresse_facturation = txtAdresse.Text;
            string carte_credit = txtCarteCredit.Text;
            string type_fidelite = null; // valeur par défaut
            DateTime dateCommande = DateTime.Today;
            string dateCommandeString = dateCommande.ToString("yyyy-MM-dd");

            // Créer une connexion à la BDD
            string connectionString = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand command = connexion.CreateCommand();
            command.Connection = connexion;

            // Vérifie si un client avec les mêmes informations existes
            command.CommandText = "SELECT COUNT(*) FROM clients WHERE courrier = @courrier";

            // Add parameters to the command
            command.Parameters.AddWithValue("@courrier", courrier);

            // Ouvre la connexion et exécute la commande
            connexion.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());
            connexion.Close();

            // Si un client avec les mêmes informations existes alors on affiche un message d'erreurs
            if (count > 0)
            {
                pnlRegister.Visible = true;
                return;
            }


            // Créer une commande SQL pour entrer les informations du clients dans la BDD
            command.CommandText = "INSERT INTO clients (nom_client, prenom_client, num_tel, courrier, mot_de_passe, adresse_facturation, carte_credit, date_inscription, type_fidelite) " +
                "VALUES (@nom, @prenom, @num_tel, @courrier, @mot_de_passe, @adresse_facturation, @carte_credit, @date_inscription, @type_fidelite)";

            command.Parameters.Clear(); // clear all the parameters before adding new ones

            // Ajouter les paramètres à la commande
            command.Parameters.AddWithValue("@nom", nom);
            command.Parameters.AddWithValue("@prenom", prenom);
            command.Parameters.AddWithValue("@num_tel", num_tel);
            command.Parameters.AddWithValue("@courrier", courrier);
            command.Parameters.AddWithValue("@mot_de_passe", mot_de_passe);
            command.Parameters.AddWithValue("@adresse_facturation", adresse_facturation);
            command.Parameters.AddWithValue("@carte_credit", carte_credit);
            command.Parameters.AddWithValue("@date_inscription", dateCommandeString);
            command.Parameters.AddWithValue("@type_fidelite", type_fidelite);

            // Ouvrir la commande et exécuter 
            connexion.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connexion.Close();

            // Vérifie si le nombre de lignes a changé pour afficher que l'ajout des informations est terminées 
            if (rowsAffected == 1)
            {
                // MEssage de succés
                lblSuccess.Text = "Le client est rajouté. Vous pouvez vous connecter sur la page Login!";
            }
            else
            {
                // MEssage d'erreurs
                lblSuccess.Text = "Erreur dans l'enregistrement du client.";
            }
               
        }
    }
}