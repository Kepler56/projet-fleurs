<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="Commande.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.Commande" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleCommande.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftMenuColumn">
        <asp:DropDownList ID="ddlFiltres" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltres_SelectedIndexChanged">
            <asp:ListItem Text="Commandes" Value="commandes" Selected="True"/>
            <asp:ListItem Text="Etats commandes" Value="etats" />
            <asp:ListItem Text="Boutique commande" Value="boutique" />
            <asp:ListItem Text="Type commande" Value="type" />
            <asp:ListItem Text="Client commande" Value="client" />
        </asp:DropDownList>
        <p>
            <asp:Button ID="btnGoBack" runat="server" Text="Go Back" OnClick="btnGoBack_Click" />
        </p>
        <p>
            <asp:HyperLink ID="hlDéconnexion" runat="server" NavigateUrl="AdminLogin.aspx">Déconnexion</asp:HyperLink>
        </p>
    </div>
    <div class="table">
        <asp:GridView ID="dataTableCommandes" runat="server" AutoGenerateColumns="True" CssClass="commande-gridview"/>
    </div>
    <div>
        <asp:Panel ID="pnlTypeCommande" runat="server" Visible="False">
            <p>
                Type de commande:
                <asp:TextBox ID="txtTypeCommande" runat="server" TextMode="Search"></asp:TextBox>
                <asp:Button ID="btnSelectType" runat="server" Text="Select" OnClick="btnSelectType_Click" />
            </p>
            <p>
                <asp:Label ID="lblTypeComamnde" runat="server"></asp:Label>
            </p>
            <p>
                <asp:GridView ID="dataTableTypeCommande" runat="server" AutoGenerateColumns="True" CssClass="commande-gridview"/>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlEtatCommande" runat="server" Visible="False">
            <p>
                Etat de commande:
                <asp:TextBox ID="txtEtatCommande" runat="server" TextMode="Search"></asp:TextBox>
                <asp:Button ID="btnSelectEtat" runat="server" Text="Select" OnClick="btnSelectEtat_Click" />
            </p>
            <p>
                <asp:Label ID="lblEtatCommande" runat="server"></asp:Label>
            </p>
            <p>
                <asp:GridView ID="dataTableEtatCommande" runat="server" AutoGenerateColumns="True" CssClass="commande-gridview"/>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlClientCommande" runat="server" Visible="False">
            <p>
                Clients:
                <asp:TextBox ID="txtClientCommande" runat="server" TextMode="Number"></asp:TextBox>
                <asp:Button ID="btnClientCommande" runat="server" Text="Select" OnClick="btnClientCommande_Click" />
            </p>
            <p>
                <asp:Label ID="lblClientCommande" runat="server"></asp:Label>
            </p>
            <p>
                <asp:GridView ID="dataTableClientCommande" runat="server" AutoGenerateColumns="True" CssClass="commande-gridview"/>
            </p>
        </asp:Panel>
    </div>
</asp:Content>
