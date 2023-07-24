using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Xml;

namespace Oscar_PASTURAL_Sascha_CAUCHON_TDC
{
    public partial class Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Méthode pour créer un XML des clients ayant commandé plusieurs fois durant le dernier mois
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string connexionStr = "Server=localhost;Database=fleurs;Uid=root;Pwd=root;";
            MySqlConnection connexion = new MySqlConnection(connexionStr);
            connexion.Open();
            string query = "SELECT id_client, COUNT(DISTINCT num_commande) AS nombre_commandes FROM commande WHERE MONTH(date_commande)=MONTH(CURRENT_DATE()) GROUP BY id_client HAVING COUNT(DISTINCT num_commande)>1;";

            MySqlCommand command = new MySqlCommand(query, connexion);

            // Exécution de la commande et récupération des données dans un DataReader
            MySqlDataReader dataReader = command.ExecuteReader();
            XmlDocument xmlDoc = new XmlDocument();

            // Création de la racine du document XML
            XmlElement root = xmlDoc.CreateElement("ListeClients");
            xmlDoc.AppendChild(root);
            while (dataReader.Read())
            {
                // Création de l'élément XML pour chaque enregistrement dans le DataReader
                XmlElement element = xmlDoc.CreateElement("Client");
                element.SetAttribute("id_client", dataReader["id_client"].ToString());
                element.SetAttribute("nombre_commandes", dataReader["nombre_commandes"].ToString());
                root.AppendChild(element);
            }
            dataReader.Close();
            connexion.Close();
            xmlDoc.Save("C:\\Users\\Sascha\\OneDrive\\Desktop\\Projet Oscar PASTURAL_Sascha Cauchon TDC\\ListeClients.xml.txt");
            lblExport.Text = "L'export des clients ayant commandé plusieurs fois durant le dernier mois est réalisé.";
        }
    }
}