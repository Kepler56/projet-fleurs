<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleAdminPage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftMenuColumn">
        <asp:RadioButtonList ID="rblModules" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblModules_SelectedIndexChanged">
            <asp:ListItem Text="Clients" Value="clients"/>
            <asp:ListItem Text="Produits" Value="items" />
            <asp:ListItem Text="Commandes" Value="commande" />
            <asp:ListItem Text="Composition" Value="composition" />
            <asp:ListItem Text="Statistiques" Value="statistiques" />
            <asp:ListItem Text="Export" Value="xml" />
        </asp:RadioButtonList>
        <p>
            <asp:Button ID="btnGoToModule" runat="server" Text="Go To Module" OnClick="btnGoToModule_Click" />
        </p>
        <p>
            <asp:HyperLink ID="hlDéconnexion" runat="server" NavigateUrl="AdminLogin.aspx">Déconnexion</asp:HyperLink>
        </p>
        <p>
            <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
        </p>
    </div>
</asp:Content>
