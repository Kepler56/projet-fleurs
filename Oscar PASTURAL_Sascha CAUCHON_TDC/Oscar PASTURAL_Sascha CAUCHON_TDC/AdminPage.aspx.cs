using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace Oscar_PASTURAL_Sascha_CAUCHON_TDC
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///  //On sauve la valeur séléctionné du Radio Button List dans une variable Session["ModuleSelectionner"]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ModuleSelectionner"] = rblModules.SelectedValue;
        }

        /// <summary>
        /// //On récupère la valeur de la variable Session et on navigue dans le WebForm correspondant avec l'aide du module sélectionné
        /// et avec Response.Redirect("ModuleSelectionner.aspx")
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGoToModule_Click(object sender, EventArgs e)
        {
            
            string moduleSelectionner = (string)Session["ModuleSelectionner"];
            switch (moduleSelectionner)
            {
                case "clients":
                    Response.Redirect("Clients.aspx");
                    break;
                case "items":
                    Response.Redirect("Items.aspx");
                    break;
                case "commande":
                    Response.Redirect("Commande.aspx");
                    break;
                case "composition":
                    Response.Redirect("Composition.aspx");
                    break;
                case "statistiques":
                    Response.Redirect("Statistiques.aspx");
                    break;
                case "xml":
                    Response.Redirect("Export.aspx");
                    break;
                default:
                    //Si aucun module n'est sélectionné on affiche un message d'erreur
                    lblErrorMessage.Text = "Veuillez sélectionner un module";
                    break;
            }
        }
    }
}