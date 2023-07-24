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
    public partial class ClientLoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Méthode pour que le client se connecte à son compte et rentrer dans la boutique en ligne. On vérifie d'abord si le client existe bien 
        /// dans la BDD et si c'est le cas il est redirigé vers le catalogue numérique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string connectionString = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand command = connexion.CreateCommand();
            command.Connection = connexion;

            command.CommandText = "SELECT COUNT(*) FROM clients WHERE courrier = @username AND mot_de_passe = @password";

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connexion.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());

            if (count > 0)
            {
                // Récupérer l'ID du client à partir de la base de données
                MySqlCommand command2 = connexion.CreateCommand();
                command2.CommandText = "SELECT id_client FROM clients WHERE courrier = @username2;";
                command2.Parameters.AddWithValue("@username2", username);

                MySqlDataReader reader = command2.ExecuteReader();
                int idClient = 0;
                if (reader.Read())
                {
                    idClient = reader.GetInt32(0);
                }
                reader.Close();

                // Stocker l'ID du client et son adresse dans des variables de session
                Session["id_client"] = idClient;

                // L'identifiant et le mot de passe sont valides, donc on le redirige vers la boutique en ligne
                Response.Redirect("OnlineShopPage.aspx");
            }
            else
            {
                // L'identifiant et le mot de passe ne sont pas valides, on affiche un message d'erreur
                lblError.Text = "Mot de passe ou identifiant incorrecte.";
            }
            connexion.Close();
        }
    }
}