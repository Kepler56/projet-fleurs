<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="Statistiques.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.Statistiques" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleStatistiques.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftMenuColumn">
        <asp:DropDownList ID="ddlFiltres" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltres_SelectedIndexChanged">
            <asp:ListItem Text="Clients" Value="client" Selected="True"/>
            <asp:ListItem Text="Boutique" Value="boutique" />
            <asp:ListItem Text="Commandes" Value="commandes"/>
            <asp:ListItem Text="Bouquets" Value="bouquets"/>
            <asp:ListItem Text="Fleurs" Value="fleurs"/>
        </asp:DropDownList>
        <p>
            <asp:Button ID="btnGoBack" runat="server" Text="Go Back" OnClick="btnGoBack_Click" />
        </p>
        <p>
            <asp:HyperLink ID="hlDéconnexion" runat="server" NavigateUrl="AdminLogin.aspx">Déconnexion</asp:HyperLink>
        </p>
    </div>

    <div>
        <asp:Panel ID="pnlCommandeStats" runat="server" Visible="False">
                <h4>Commandes:</h4>
                <p>
                    Le nombre de commande passées est: <br />
                    <asp:GridView ID="dataTableCommandesPassees" runat="server" AutoGenerateColumns="True" CssClass="statistiques-gridview "/>
                </p>
                <p>
                    Le nombre de commande passées en 2023 est: 
                    <asp:Label ID="lblCommandePasses" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    Le chiffre d'affaire total par mois est: <br />
                    <asp:GridView ID="dataTableCA" runat="server" AutoGenerateColumns="True" CssClass="statistiques-gridview "/>
                </p>
                <p>
                    Le nombre de commande standards est: 
                    <asp:Label ID="lblCommandeStandards" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    Le nombre de commande personnalises est: 
                    <asp:Label ID="lblCommandePersonnalises" runat="server" Text=""></asp:Label>
                </p>
            </asp:Panel>
        <asp:Panel ID="pnlBouquets" runat="server" Visible="False">
                <h4>Bouquets:</h4>
                <p>
                    Le prix moyen d'un bouquet standard dans les commandes est:
                    <asp:Label ID="lblPrixMoyenStandard" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    Le prix moyen d'un bouquet personnalisé dans les commandes est:
                    <asp:Label ID="lblPrixMoyenPersonnalise" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    Le bouquets standard avec le plus de succés est:
                    <asp:Label ID="lblSuccesBouquetStandard" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    Le nombre de bouquet vendu par mois est: <br />
                    <asp:GridView ID="dataTableNombreBouquets" runat="server" AutoGenerateColumns="True" CssClass="statistiques-gridview "/>
                </p>
                <p>
                    Le nombre de bouquet vendu par an est: 
                    <asp:Label ID="lblNombreBouquets" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    Le nombre de bouquet vendu par catégorie est: 
                    <asp:GridView ID="dataTableNombreBouquetsCategorie" runat="server" AutoGenerateColumns="True" CssClass="statistiques-gridview "/>
                </p>
                <p>
                    Le bouquet standard le plus chers est: 
                    <asp:Label ID="lblBouquetStandardPlusChers" runat="server" Text=""></asp:Label>. 
                    Et le bouquet standard le moins cher est:
                    <asp:Label ID="lblBouquetStandardMoinsChers" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    Le bouquet personnalisé le plus chers est: 
                    <asp:Label ID="lblBouquetPersonnalisePlusChers" runat="server" Text=""></asp:Label>. 
                    Et le bouquet personnalisé le moins cher est:
                    <asp:Label ID="lblBouquetPersonnaliseMoinsChers" runat="server" Text=""></asp:Label>
                </p>
            </asp:Panel>
        <asp:Panel ID="pnlFleurs" runat="server" Visible="False">
                <h4>Fleurs:</h4>
                <p>
                    La fleur exotique la moins vendue est:
                    <asp:Label ID="lblFleurExotique" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    La nombre de confection vendue en 2023 est:
                    <asp:Label ID="lblNombreConfection" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    La confection la moins vendue est:
                    <asp:Label ID="lblConfectionMoinsVendue" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    La confection la plus vendue est:
                    <asp:Label ID="lblConfectionPlusVendue" runat="server" Text=""></asp:Label>
                </p>
            </asp:Panel>
        <asp:Panel ID="pnlClient" runat="server" Visible="False">
            <h4>Clients</h4>
            <p>
                Nombre de nouveaux clients:
                <asp:GridView ID="dataTableNouveauxClients" runat="server" AutoGenerateColumns="True" CssClass="statistiques-gridview "/>
            </p>
            <p>
                Le meilleur client du mois est:
                <asp:Label ID="lblClientMois" runat="server" Text=""></asp:Label>
            </p>
            <p>
                Le meilleur client de l'année est:
                <asp:Label ID="lblClientAnnee" runat="server" Text=""></asp:Label>
            </p>
            <p>
                Client(s) ayant commandé le plus de fois dans le dernier mois:
                <asp:GridView ID="dataTableMeilleursClients" runat="server" AutoGenerateColumns="True" CssClass="statistiques-gridview "/>
            </p>
            <p>
                Nombre de commandes passées par clients:
                <asp:GridView ID="dataTableCommandesClients" runat="server" AutoGenerateColumns="True" CssClass="statistiques-gridview "/>
            </p>
            <p>
                Nombre de clients fidèles (ayant passé plus de 3 commandes):
                <asp:GridView ID="dataTableClientsFideles" runat="server" AutoGenerateColumns="True" CssClass="statistiques-gridview "/>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlBoutique" runat="server" Visible="False">
                <h4>Boutique</h4>
                <p>
                    Le magasin qui a généré le plus de chiffre d'affaire est:
                    <asp:Label ID="lblMeilleurMagasin" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    Les magasins avec leurs nombre de commandes:
                    <asp:GridView ID="dataTableBoutiquesCommandes" runat="server" AutoGenerateColumns="True" CssClass="statistiques-gridview "/>
                </p>
        </asp:Panel>
    </div>
</asp:Content>
