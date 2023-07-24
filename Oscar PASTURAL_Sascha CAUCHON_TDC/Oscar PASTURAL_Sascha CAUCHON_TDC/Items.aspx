<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.Items" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleItems.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftMenuColumn">
        <asp:DropDownList ID="ddlFiltres" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltres_SelectedIndexChanged">
            <asp:ListItem Text="Produits" Value="produits" Selected="True"/>
            <asp:ListItem Text="Bouquets" Value="bouquets_standards" />
            <asp:ListItem Text="Fleurs" Value="fleurs" />
            <asp:ListItem Text="Accessoires" Value="accessoires" />
            <asp:ListItem Text="Quantité" Value="quantite" />
            <asp:ListItem Text="Prix" Value="prix" />
            <asp:ListItem Text="Disponibilité" Value="disponibilite" />
            <asp:ListItem Text="Vérification Stock" Value="verifieStock" />
        </asp:DropDownList>
        <p>
            <asp:Button ID="btnGoBack" runat="server" Text="Go Back" OnClick="btnGoBack_Click"/>
        </p>
        <p>
            <asp:HyperLink ID="hlDéconnexion" runat="server" NavigateUrl="AdminLogin.aspx">Déconnexion</asp:HyperLink>
        </p>
    </div>
    <div class="table">
        <asp:GridView ID="dataTableItems" runat="server" AutoGenerateColumns="True" CssClass="items-gridview"/>
    </div>
    <div>
        <asp:Panel ID="pnlVerifieStock" runat="server" Visible="False">
            <p>
                <asp:Label ID="lblStockCount" runat="server" CssClass ="alert"></asp:Label>
            </p>
            <p>
                ID:
                <asp:TextBox ID="txtItemId" runat="server" TextMode="Number" CssClass="amountBox" Text="1"></asp:TextBox>
                Amount:
                <asp:TextBox ID="txtAmount" runat="server" TextMode="Number" CssClass="amountBox" Text ="1"></asp:TextBox>
            </p>
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            <p>
                <asp:Label ID="lblSuccessful" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Label ID="lblWrongId" runat="server"></asp:Label>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlMettreAJourStock" runat="server" Visible="False">
            <p>
                Mettre à jour le stock: 
            </p>
            <asp:Button ID="btnMAJStock" runat="server" Text="Mettre à Jour" OnClick="btnMAJStock_Click" />
            <p>
                <asp:Label ID="lblSuccessMAJStock" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Label ID="lblErreur" runat="server"></asp:Label>
            </p>
        </asp:Panel>
    </div>
</asp:Content>
