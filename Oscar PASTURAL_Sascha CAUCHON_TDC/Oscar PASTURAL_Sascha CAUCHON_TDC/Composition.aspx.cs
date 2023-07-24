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
    public partial class Composition : System.Web.UI.Page
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


                command.CommandText = "SELECT 'standard' AS type_commande, id_standard, num_commande, items.nom_item " +
                    "FROM commande_standard INNER JOIN composition ON commande_standard.num_bouquet = composition.num_bouquet " +
                    "INNER JOIN items ON composition.id_items = items.id_items " +
                    "UNION " +
                    "SELECT 'personnalisée' AS type_commande, commande_personnalisee.id_personnalisee, num_commande, items.nom_item " +
                    "FROM commande_personnalisee INNER JOIN confection ON commande_personnalisee.id_personnalisee = confection.id_personnalisee " +
                    "INNER JOIN items ON confection.id_items = items.id_items;";

                //Nous avons stocké les données dans un objet DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTableComposition = new DataTable();
                adapter.Fill(dataTableComposition);

                //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
                this.dataTableComposition.DataSource = dataTableComposition;
                this.dataTableComposition.DataBind();
                connexion.Close();
            }
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }

        /// <summary>
        ///  Méthode pour passer dans chaque filtres du module "Composition" pour vérifier plusieurs types d'informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFiltres_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Ouverture de la connexion
            MySqlConnection connexion = null;
            string filtresCompositions = ddlFiltres.Text;

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

            switch (filtresCompositions)
            {
                case "composition":
                    pnlMAJEtatP.Visible = false;
                    pnlMAJEtatS.Visible = false;
                    pnlCCaCAL.Visible = false;
                    pnlCALaCL.Visible = false;
                    command.CommandText = "SELECT 'standard' AS type_commande, id_standard, num_commande, items.nom_item " +
                    "FROM commande_standard INNER JOIN composition ON commande_standard.num_bouquet = composition.num_bouquet " +
                    "INNER JOIN items ON composition.id_items = items.id_items " +
                    "UNION " +
                    "SELECT 'personnalisée' AS type_commande, commande_personnalisee.id_personnalisee, num_commande, items.nom_item " +
                    "FROM commande_personnalisee INNER JOIN confection ON commande_personnalisee.id_personnalisee = confection.id_personnalisee " +
                    "INNER JOIN items ON confection.id_items = items.id_items;";
                    break;
                case "standards":
                    pnlMAJEtatP.Visible = false;
                    pnlMAJEtatS.Visible = true;
                    pnlCCaCAL.Visible = false;
                    pnlCALaCL.Visible = false;
                    command.CommandText = "SELECT 'standard' AS type_commande, code_commande, commande.num_commande, commande_standard.num_bouquet, items.nom_item, items.disponibilite " +
                        "FROM commande INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande " +
                        "INNER JOIN composition ON commande_standard.num_bouquet = composition.num_bouquet " +
                        "INNER JOIN items ON composition.id_items = items.id_items " +
                        "WHERE code_commande = \"VINV\";";
                    break;
                case "personnalises":
                    pnlMAJEtatP.Visible = true;
                    pnlMAJEtatS.Visible = false;
                    pnlCCaCAL.Visible = false;
                    pnlCALaCL.Visible = false;
                    command.CommandText = "SELECT 'personnalisée' AS type_commande, code_commande, commande.num_commande, items.nom_item, items.disponibilite " +
                        "FROM commande INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande " +
                        "INNER JOIN confection ON commande_personnalisee.id_personnalisee = confection.id_personnalisee " +
                        "INNER JOIN items ON confection.id_items = items.id_items " +
                        "WHERE code_commande = \"CPAV\";";
                    break;
                case "cal":
                    pnlMAJEtatP.Visible = false;
                    pnlMAJEtatS.Visible = false;
                    pnlCCaCAL.Visible = true;
                    pnlCALaCL.Visible = false;
                    command.CommandText = "select num_commande, date_commande, date_livraison, code_commande, prix from commande where code_commande = \"CC\";";
                    break;
                case "cl":
                    pnlMAJEtatP.Visible = false;
                    pnlMAJEtatS.Visible = false;
                    pnlCCaCAL.Visible = false;
                    pnlCALaCL.Visible = true;
                    command.CommandText = "select num_commande, date_commande, date_livraison, code_commande, prix from commande where code_commande = \"CAL\";";
                    break;
                default:
                    command.CommandText = "";
                    break;
            }

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTableComposition = new DataTable();
            adapter.Fill(dataTableComposition);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableComposition.DataSource = dataTableComposition;
            this.dataTableComposition.DataBind();
            connexion.Close();
        }

        /// <summary>
        /// Méthode pour mettre à jour le montant des items s'ils sont en dessous d'une limite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Entre l'identifiant d'un item
            //Ajouter le nouveau montant qu'il veut à l'item
            string id_commande = txtCommande.Text;

            string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connexionStr);
            MySqlCommand c1 = connexion.CreateCommand();


            c1.CommandText = "select count(*) from commande where code_commande = \"CPAV\" and num_commande=@id_commande";
            c1.Parameters.AddWithValue("@id_commande", id_commande);

            connexion.Open();
            object result = c1.ExecuteScalar();
            int count = Convert.ToInt32(result);
            connexion.Close();

            if (count > 0)
            {
                //Mise à jour de l'état de la commande
                MySqlCommand c2 = connexion.CreateCommand();
                c2.CommandText = "SELECT COUNT(*) FROM commande_personnalisee " +
                    "INNER JOIN confection ON commande_personnalisee.id_personnalisee = confection.id_personnalisee " +
                    "INNER JOIN items ON confection.id_items = items.id_items " +
                    "WHERE commande_personnalisee.num_commande = (SELECT num_commande FROM commande WHERE code_commande = \"CPAV\") " +
                    "AND items.disponibilite = 0;";

                connexion.Open();
                int itemCount = Convert.ToInt32(c2.ExecuteScalar());
                connexion.Close();

                if (itemCount == 0)
                {
                    MySqlCommand c3 = connexion.CreateCommand();
                    c3.CommandText = "UPDATE commande SET code_commande = 'CC' " +
                                      "WHERE num_commande = @id_commande";
                    c3.Parameters.AddWithValue("@id_commande", id_commande);

                    connexion.Open();
                    c3.ExecuteNonQuery();
                    connexion.Close();

                    lblSuccess.Text = "L'état de la commande a été mis à jour avec succès.";
                }
                else
                {
                    lblError.Text = "Certains des articles du bouquet ne sont pas disponibles.";
                }

            }
            else
            {
                lblError.Text = "Le nouveau code ne peut pas être inscrit.";
            }
        }

        /// <summary>
        /// Mettre à jour le code de commande pour les commandes standards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateS_Click(object sender, EventArgs e)
        {
            string id_commande = txtCommandeS.Text;

            string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connexionStr);
            MySqlCommand c1 = connexion.CreateCommand();


            c1.CommandText = "select count(*) from commande where code_commande = \"VINV\" and num_commande=@id_commande";
            c1.Parameters.AddWithValue("@id_commande", id_commande);

            connexion.Open();
            object result = c1.ExecuteScalar();
            int count = Convert.ToInt32(result);
            connexion.Close();

            if (count > 0)
            {
                //Mise à jour de l'état de la commande
                MySqlCommand c2 = connexion.CreateCommand();
                c2.CommandText = "SELECT COUNT(*) FROM commande_standard  " +
                    "INNER JOIN composition ON commande_standard.num_bouquet = composition.num_bouquet " +
                    "INNER JOIN items ON composition.id_items = items.id_items " +
                    "WHERE commande_standard.num_commande IN  (SELECT num_commande FROM commande WHERE code_commande = \"VINV\") AND items.disponibilite = 0;";

                connexion.Open();
                int itemCount = Convert.ToInt32(c2.ExecuteScalar());
                connexion.Close();

                if (itemCount == 0)
                {
                    MySqlCommand c3 = connexion.CreateCommand();
                    c3.CommandText = "UPDATE commande SET code_commande = 'CC' " +
                                      "WHERE num_commande = @id_commande";
                    c3.Parameters.AddWithValue("@id_commande", id_commande);

                    connexion.Open();
                    c3.ExecuteNonQuery();
                    connexion.Close();

                    lblSuccessS.Text = "L'état de la commande a été mis à jour avec succès.";
                }
                else
                {
                    lblErrorS.Text = "Certains des articles du bouquet ne sont pas disponibles.";
                }

            }
            else
            {
                lblErrorS.Text = "Le nouveau code ne peut pas être inscrit.";
            }
        }

        /// <summary>
        /// Mettre à jour les commandes qui sont confirmés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateCCaCAL_Click(object sender, EventArgs e)
        {
            //Enters the Id of the items he wants to update
            //Enters the Amount he wants to add to the items
            string id_commande = txtIDCommande.Text;

            string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connexionStr);
            MySqlCommand c1 = connexion.CreateCommand();


            c1.CommandText = "select count(*) from commande where code_commande = \"CC\" and num_commande=@id_commande";
            c1.Parameters.AddWithValue("@id_commande", id_commande);

            connexion.Open();
            object result = c1.ExecuteScalar();
            int count = Convert.ToInt32(result);
            connexion.Close();

            if (count > 0)
            {

                MySqlCommand c3 = connexion.CreateCommand();
                c3.CommandText = "UPDATE commande SET code_commande = 'CAL' " +
                                    "WHERE num_commande = @id_commande";
                c3.Parameters.AddWithValue("@id_commande", id_commande);

                connexion.Open();
                c3.ExecuteNonQuery();
                connexion.Close();

                lblSuccessCCaCAL.Text = "L'état de la commande a été mis à jour avec succès.";

            }
            else
            {
                lblErrorCCaCAL.Text = "Le nouveau code ne peut pas être inscrit.";
            }
        }

        /// <summary>
        /// Mettre à jour les commandes qui sont livrés 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateCALaCL_Click(object sender, EventArgs e)
        {
            string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connexionStr);
            MySqlCommand c1 = connexion.CreateCommand();


            c1.CommandText = "select count(*) from commande where code_commande = \"CAL\"";

            connexion.Open();
            object result = c1.ExecuteScalar();
            int count = Convert.ToInt32(result);
            connexion.Close();

            if (count > 0)
            {

                MySqlCommand c3 = connexion.CreateCommand();
                c3.CommandText = "UPDATE commande SET code_commande = 'CL' WHERE date_livraison < CURDATE() AND code_commande = 'CAL'";

                connexion.Open();
                c3.ExecuteNonQuery();
                connexion.Close();

                lblSuccesCALaCL.Text = "L'état de la commande a été mis à jour avec succès.";

            }
            else
            {
                lblErrorCALaCL.Text = "Les dates de livraisons ne sont pas encore passés";
            }
        }
    }
}
