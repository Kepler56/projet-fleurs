using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Oscar_PASTURAL_Sascha_CAUCHON_TDC
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vérifier si l'utilisateur est déjà connecté
            /*if (Session["loggedIn"] != null && (bool)Session["loggedIn"])
            {
                Response.Redirect("AdminPage.aspx");
            }*/
        }

        /// <summary>
        /// Méthode pour connecter le propriétaire du magasin dans le système d'information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Vérifier les identifiants de connexion
            if (txtUsername.Text == "bellefleur" && txtPassword.Text == "root")
            {
                // Définir la session comme connectée et rediriger vers la page de bienvenue
                Session["loggedIn"] = true;
                Response.Redirect("AdminPage.aspx");
            }
            else
            {
                // Afficher un message d'erreur si les identifiants sont incorrects
                lblError.Text = "Identifiant ou mot de passe incorrect.";
            }
        }
    }
}