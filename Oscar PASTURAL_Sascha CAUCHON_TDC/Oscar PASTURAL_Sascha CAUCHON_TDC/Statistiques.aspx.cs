using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Oscar_PASTURAL_Sascha_CAUCHON_TDC
{
    public partial class Statistiques : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            MySqlConnection connexion = null;
            string filtres = ddlFiltres.Text;

            try
            {
                string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
                connexion = new MySqlConnection(connexionStr);
            }
            catch (MySqlException)
            {
                Console.WriteLine("ErreurConnexion :" + e.ToString());
                return;
            }


            switch (filtres)
            {
                case "client":
                    pnlClient.Visible = true;
                    pnlBoutique.Visible = false;
                    pnlFleurs.Visible = false;
                    pnlBouquets.Visible = false;
                    pnlCommandeStats.Visible = false;
                    MeilleursClientsAnnee(connexion);
                    MeilleursClientsMois(connexion);
                    MeilleursClients(connexion);
                    NombreCommandesClients(connexion);
                    ClientsFideles(connexion);
                    NouveauClients(connexion);
                    break;
                case "boutique":
                    pnlBoutique.Visible = true;
                    pnlClient.Visible = false;
                    pnlFleurs.Visible = false;
                    pnlBouquets.Visible = false;
                    pnlCommandeStats.Visible = false;
                    MeilleurMagasin(connexion);
                    CommandesBoutiques(connexion);
                    break;
                case "commandes":
                    pnlCommandeStats.Visible = true;
                    pnlBouquets.Visible = false;
                    pnlFleurs.Visible = false;
                    pnlBoutique.Visible = false;
                    pnlClient.Visible = false;
                    NombreCommandes(connexion);
                    NombreCommandesAnnee(connexion);
                    ChiffreAffaireTotal(connexion);
                    NombreCommandesPersonnalises(connexion);
                    NombreCommandesStandards(connexion);
                    break;
                case "bouquets":
                    pnlBouquets.Visible = true;
                    pnlFleurs.Visible = false;
                    pnlCommandeStats.Visible = false;
                    pnlBoutique.Visible = false;
                    pnlClient.Visible = false;
                    SuccesBouquetStandard(connexion);
                    PrixMoyenBouquetStandard(connexion);
                    PrixMoyenBouquetPersonnalise(connexion);
                    NombreBouquetsVendus(connexion);
                    NombreBouquetsVendusAns(connexion);
                    NombreBouquetsVendusCategorie(connexion);
                    BouquetsChersStandards(connexion);
                    BouquetsChersPersonnalises(connexion);
                    break;
                case "fleurs":
                    pnlFleurs.Visible = true;
                    pnlBouquets.Visible = false;
                    pnlCommandeStats.Visible = false;
                    pnlBoutique.Visible = false;
                    pnlClient.Visible = false;
                    FleursExotique(connexion);
                    ConfectionMoinsVendue(connexion);
                    NombreConfection(connexion);
                    ConfectionPlusVendue(connexion);
                    break;
                default:
                    break;
            }
        }

        //Methods containing the SQL querys that calculate the statistics for a bunch of flowers
        //Bouquets
        public void SuccesBouquetStandard(MySqlConnection connexion) 
        {
            /*
                La requête va joindre les deux tables bouquet_standard avec commande_standard. Ensuite avec COUNT(),
                on va compter le nombre de commandes par bouquet. Le tableau va être trié par ordre décroissant et 
                ensuite on va prendre le premier élément qui apparaît avec avec la méthode LIMIT 1. 
             */

            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT bouquet_standard.nom_bouquet, COUNT(commande_standard.num_bouquet) AS nb_commandes " +
                "FROM commande_standard " +
                "INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet " +
                "GROUP BY commande_standard.num_bouquet " +
                "ORDER BY nb_commandes DESC " +
                "LIMIT 1;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblSuccesBouquetStandard.Text = values[0] + 
                " et il a été commandé " + values[1] + " fois.";
            connexion.Close();
        }
        public void PrixMoyenBouquetStandard(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT AVG(prix) FROM bouquet_standard INNER JOIN commande_standard ON bouquet_standard.num_bouquet = commande_standard.num_bouquet;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblPrixMoyenStandard.Text = values[0] + " £";
            connexion.Close();
        }

        public void PrixMoyenBouquetPersonnalise(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT AVG(prix_max) AS prix_moyen FROM commande_personnalisee;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblPrixMoyenPersonnalise.Text = values[0] + " £";
            connexion.Close();
        }

        public void NombreBouquetsVendus(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT (t1.nombre_bouquets + t2.nombre_bouquets) AS nombre_bouquets, t1.mois, t1.annee " +
                "FROM (SELECT COUNT(*) AS nombre_bouquets, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee " +
                "FROM commande " +
                "INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande " +
                "INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet " +
                "GROUP BY mois, annee) AS t1 " +
                "JOIN (SELECT COUNT(*) AS nombre_bouquets, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee " +
                "FROM commande " +
                "INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande " +
                "GROUP BY mois, annee) AS t2 " +
                "ON t1.mois = t2.mois AND t1.annee = t2.annee;";

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(c);
            DataTable dataTableNombreBouquets = new DataTable();
            adapter.Fill(dataTableNombreBouquets);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableNombreBouquets.DataSource = dataTableNombreBouquets;
            this.dataTableNombreBouquets.DataBind();
            connexion.Close();
        }
        public void NombreBouquetsVendusAns(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT COUNT(*) AS nombre_bouquets, YEAR(date_commande) AS annee " +
                "FROM commande " +
                "INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande " +
                "GROUP BY annee;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblNombreBouquets.Text = values[0];
            connexion.Close();
        }
        public void NombreBouquetsVendusCategorie(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT COUNT(*) AS nombre_bouquets, categorie FROM bouquet_standard INNER JOIN commande_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet GROUP BY categorie;";

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(c);
            DataTable dataTableNombreBouquetsCategorie = new DataTable();
            adapter.Fill(dataTableNombreBouquetsCategorie);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableNombreBouquetsCategorie.DataSource = dataTableNombreBouquetsCategorie;
            this.dataTableNombreBouquetsCategorie.DataBind();
            connexion.Close();
        }
        public void BouquetsChersStandards(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT nom_bouquet, prix FROM bouquet_standard WHERE prix = (SELECT MAX(prix) FROM bouquet_standard);";
            MySqlCommand c2 = connexion.CreateCommand();
            c2.CommandText = "SELECT nom_bouquet, prix FROM bouquet_standard WHERE prix = (SELECT MIN(prix) FROM bouquet_standard);";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();

            MySqlDataReader reader2 = c2.ExecuteReader();

            List<string> values2 = new List<string>();

            while (reader2.Read())
            {
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    values2.Add(reader2.GetString(i));
                }
            }
            reader.Close();



            lblBouquetStandardPlusChers.Text = values[0] + " avec un prix de " + values[1];
            lblBouquetStandardMoinsChers.Text = values2[0] + " avec un prix de " + values2[1];
            connexion.Close();
        }
        public void BouquetsChersPersonnalises(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT MAX(prix_max) AS prix_max, id_personnalisee " +
                "FROM commande_personnalisee " +
                "GROUP BY id_personnalisee " +
                "ORDER BY prix_max DESC " +
                "LIMIT 1;";
            MySqlCommand c2 = connexion.CreateCommand();
            c2.CommandText = "SELECT MIN(prix_max) AS prix_min, id_personnalisee " +
                "FROM commande_personnalisee " +
                "GROUP BY id_personnalisee " +
                "ORDER BY prix_max " +
                "LIMIT 1;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();

            MySqlDataReader reader2 = c2.ExecuteReader();

            List<string> values2 = new List<string>();

            while (reader2.Read())
            {
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    values2.Add(reader2.GetString(i));
                }
            }
            reader.Close();



            lblBouquetPersonnalisePlusChers.Text = values[1] + " avec un prix de " + values[0];
            lblBouquetPersonnaliseMoinsChers.Text = values2[1] + " avec un prix de " + values2[0];
            connexion.Close();
        }

        //Methods containing the SQL querys that calculate the statistics for flowers
        //Fleurs
        public void FleursExotique(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT nom_item, SUM(quantite) AS quantite_vendue FROM items " +
                "\r\nINNER JOIN composition " +
                "\r\nON items.id_items = composition.id_items " +
                "\r\nWHERE nom_item IN ('Ginger','Gerbera')" +
                "\r\nGROUP BY nom_item " +
                "\r\nORDER BY quantite_vendue ASC LIMIT 1;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblFleurExotique.Text = values[0] + " avec une quantité de " + values[1];
            connexion.Close();
        }
        public void NombreConfection(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT SUM(quantite) AS nombre_confections, YEAR(date_commande) AS annee" +
                " FROM commande" +
                " INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande" +
                " INNER JOIN confection ON commande_personnalisee.id_personnalisee=confection.id_personnalisee" +
                " GROUP BY annee;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblNombreConfection.Text = values[0];
            connexion.Close();
        }
        public void ConfectionPlusVendue (MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT id_items, COUNT(*) AS nombre_ventes " +
                "FROM commande_personnalisee " +
                "INNER JOIN confection " +
                "ON commande_personnalisee.id_personnalisee = confection.id_personnalisee " +
                "GROUP BY id_items " +
                "ORDER BY nombre_ventes DESC " +
                "LIMIT 1;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblConfectionPlusVendue.Text = values[0] + " avec une quantité de " + values[1];
            connexion.Close();
        }
        public void ConfectionMoinsVendue(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT id_items, COUNT(*) AS nombre_ventes " +
                "FROM commande_personnalisee " +
                "INNER JOIN confection " +
                "ON commande_personnalisee.id_personnalisee = confection.id_personnalisee " +
                "GROUP BY id_items " +
                "ORDER BY nombre_ventes " +
                "LIMIT 1;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblConfectionMoinsVendue.Text = values[0] + " avec une quantité de " + values[1];
            connexion.Close();
        }

        //Methods containing the SQL querys that calculate the statistics for orders
        //Commandes
        public void NombreCommandes(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT COUNT(*) AS nombre_commandes, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee" +
                "\r\nFROM commande" +
                "\r\nGROUP BY mois, annee;";

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(c);
            DataTable dataTableCommandesPassees = new DataTable();
            adapter.Fill(dataTableCommandesPassees);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableCommandesPassees.DataSource = dataTableCommandesPassees;
            this.dataTableCommandesPassees.DataBind();
            connexion.Close();
        }
        public void NombreCommandesAnnee(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT COUNT(*) AS nombre_commandes, YEAR(date_commande) AS annee" +
                "\r\nFROM commande" +
                "\r\nGROUP BY annee;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblCommandePasses.Text = values[0];
            connexion.Close();
        }
        public void ChiffreAffaireTotal(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT (t1.chiffre_affaires + t2.chiffre_affaires) AS chiffre_affaires, t1.mois, t1.annee " +
                "FROM (SELECT SUM(bouquet_standard.prix) AS chiffre_affaires, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee " +
                "FROM commande " +
                "INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande " +
                "INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet " +
                "GROUP BY mois, annee) AS t1 " +
                "JOIN (SELECT SUM(prix_max) AS chiffre_affaires, MONTH(date_commande) AS mois, YEAR(date_commande) AS annee " +
                "FROM commande " +
                "INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande " +
                "GROUP BY mois, annee) AS t2 " +
                "ON t1.mois = t2.mois;";

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(c);
            DataTable dataTableCA = new DataTable();
            adapter.Fill(dataTableCA);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableCA.DataSource = dataTableCA;
            this.dataTableCA.DataBind();
            connexion.Close();
        }
        public void NombreCommandesPersonnalises(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT COUNT(*) AS nombre_commandes FROM commande_personnalisee;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblCommandePersonnalises.Text = values[0];

            connexion.Close();
        }
        public void NombreCommandesStandards(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT COUNT(*) AS nombre_commandes FROM commande_standard;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblCommandeStandards.Text = values[0];

            connexion.Close();
        }

        //Methods containing the SQL querys that calculate the statistics for the clients
        public void MeilleursClientsMois(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT t1.id_client, (t1.total_achat + t2.total_achat) AS total_achat " +
                "FROM (SELECT id_client, SUM(bouquet_standard.prix) AS total_achat " +
                "FROM commande " +
                "INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande " +
                "INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet " +
                "WHERE MONTH(date_commande) = MONTH('2023-04-01') AND YEAR(date_commande) = YEAR(CURRENT_DATE()) " +
                "GROUP BY id_client) AS t1 " +
                "JOIN (SELECT id_client, SUM(prix_max) AS total_achat " +
                "FROM commande " +
                "INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande " +
                "WHERE MONTH(date_commande) = MONTH('2023-04-01') AND YEAR(date_commande) = YEAR(CURRENT_DATE()) " +
                "GROUP BY id_client) AS t2 " +
                "ON t1.id_client = t2.id_client " +
                "ORDER BY total_achat DESC LIMIT 1;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblClientMois.Text = values[0] + " avec un achat total de " + values[1];
            connexion.Close();
        }
        public void MeilleursClientsAnnee(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT t1.id_client, (t1.total_achat + t2.total_achat) AS total_achat " +
                "FROM (SELECT id_client, SUM(bouquet_standard.prix) AS total_achat " +
                "FROM commande " +
                "INNER JOIN commande_standard ON commande.num_commande = commande_standard.num_commande " +
                "INNER JOIN bouquet_standard ON commande_standard.num_bouquet = bouquet_standard.num_bouquet " +
                "WHERE YEAR(date_commande) = YEAR(CURRENT_DATE()) " +
                "GROUP BY id_client) AS t1 " +
                "JOIN (SELECT id_client, SUM(prix_max) AS total_achat " +
                "FROM commande " +
                "INNER JOIN commande_personnalisee ON commande.num_commande = commande_personnalisee.num_commande " +
                "WHERE YEAR(date_commande) = YEAR(CURRENT_DATE()) " +
                "GROUP BY id_client) AS t2 " +
                "ON t1.id_client = t2.id_client " +
                "ORDER BY total_achat DESC LIMIT 1;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblClientAnnee.Text = values[0] + " avec un achat total de " + values[1];
            connexion.Close();
        }
        public void MeilleursClients(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT id_client, COUNT(*) AS nombre_commandes" +
                "\r\nFROM commande" +
                "\r\nWHERE date_commande >= DATE_SUB(CURDATE(), INTERVAL 1 MONTH)" +
                "\r\nGROUP BY id_client" +
                "\r\nHAVING COUNT(*) > 1;";

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(c);
            DataTable dataTableMeilleursClients = new DataTable();
            adapter.Fill(dataTableMeilleursClients);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableMeilleursClients.DataSource = dataTableMeilleursClients;
            this.dataTableMeilleursClients.DataBind();
            connexion.Close();
        }
        public void NombreCommandesClients(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT id_client, COUNT(DISTINCT num_commande) AS nombre_commandes FROM commande GROUP BY id_client;";

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(c);
            DataTable dataTableCommandesClients = new DataTable();
            adapter.Fill(dataTableCommandesClients);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableCommandesClients.DataSource = dataTableCommandesClients;
            this.dataTableCommandesClients.DataBind();
            connexion.Close();
        }
        public void ClientsFideles(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT id_client FROM commande GROUP BY id_client HAVING COUNT(DISTINCT num_commande) > 3;";

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(c);
            DataTable dataTableClientsFideles = new DataTable();
            adapter.Fill(dataTableClientsFideles);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableClientsFideles.DataSource = dataTableClientsFideles;
            this.dataTableClientsFideles.DataBind();
            connexion.Close();
        }
        public void NouveauClients(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT id_client " +
                "FROM clients " +
                "WHERE MONTH(date_inscription)=MONTH(CURRENT_DATE()) AND  YEAR(date_inscription)=YEAR(CURRENT_DATE());";

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(c);
            DataTable dataTableNouveauxClients = new DataTable();
            adapter.Fill(dataTableNouveauxClients);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableNouveauxClients.DataSource = dataTableNouveauxClients;
            this.dataTableNouveauxClients.DataBind();
            connexion.Close();
        }



        //Methods containing the SQL querys to calculate the statistics for the shops
        public void MeilleurMagasin(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT num_boutique, SUM(bouquet_standard.prix) AS chiffre_affaires FROM commande_standard " +
                "INNER JOIN commande " +
                "ON commande_standard.num_commande = commande.num_commande " +
                "INNER JOIN bouquet_standard " +
                "ON commande_standard.num_bouquet=bouquet_standard.num_bouquet " +
                "WHERE YEAR(date_commande) = YEAR(CURRENT_DATE())  " +
                "GROUP BY num_boutique " +
                "ORDER BY chiffre_affaires DESC LIMIT 1;";

            MySqlDataReader reader = c.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetString(i));
                }
            }
            reader.Close();



            lblMeilleurMagasin.Text = "le magasin n°" + values[0] + " avec un chiffre d'affaire de " + values[1];
            connexion.Close();
        }
        public void CommandesBoutiques(MySqlConnection connexion)
        {
            connexion.Open();
            MySqlCommand c = connexion.CreateCommand();
            c.CommandText = "SELECT COUNT(*) as nombre_commande, boutique.num_boutique, boutique.adresse_boutique " +
                "FROM commande INNER JOIN boutique ON commande.num_boutique = boutique.num_boutique " +
                "GROUP BY boutique.num_boutique " +
                "ORDER BY nombre_commande DESC;";

            //Nous avons stocké les données dans un objet DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(c);
            DataTable dataTableBoutiquesCommandes = new DataTable();
            adapter.Fill(dataTableBoutiquesCommandes);

            //nous avons défini la source de données du GridView (this.dataTable.DataSource) sur cet objet DataTable avant de l'afficher (this.dataTable.DataBind()).
            this.dataTableBoutiquesCommandes.DataSource = dataTableBoutiquesCommandes;
            this.dataTableBoutiquesCommandes.DataBind();
            connexion.Close();
        }
        
    }
}