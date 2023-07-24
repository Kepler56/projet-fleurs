<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.Clients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleClients.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftMenuColumn">
        <asp:DropDownList ID="ddlFiltres" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltres_SelectedIndexChanged">
            <asp:ListItem Text="Clients" Value="clients"/>
            <asp:ListItem Text="Statut Or" Value="or" />
            <asp:ListItem Text="Statut Bronze" Value="bronze" />
            <asp:ListItem Text="Mettre à jour statut" Value="maj" />
            <asp:ListItem Text="Contact" Value="contact" />
            <asp:ListItem Text="Adresse" Value="adresse" />
        </asp:DropDownList>
        <p>
            <asp:Button ID="btnGoBack" runat="server" Text="Go Back" OnClick="btnGoBack_Click" />
        </p>
        <p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="AdminLogin.aspx">Déconnexion</asp:HyperLink>
        </p>
    </div>
    <div class="table">
        <asp:GridView ID="dataTableClients" runat="server" AutoGenerateColumns="True" CssClass="gridview"/>
    </div>
    <asp:Panel ID="pnlSupprimerClient" runat="server" Visible="False">
            <p>
                Supprimer un client:
                <asp:TextBox ID="txtIdClient" runat="server" TextMode="Search"></asp:TextBox>
                <asp:Button ID="btnSupprimerClient" runat="server" Text="Supprimer" OnClick="btnSupprimerClient_Click" />
                <p>
                    <asp:Label ID="lblConfirmationSupp" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblErreurSupp" runat="server" Text=""></asp:Label>
                </p>
            </p>
    </asp:Panel>
    <asp:Panel ID="pnlMettreAJour" runat="server" Visible="False">
            <p>
                Mettre à jour le statut des clients:
             </p>
            <div class="form-group">
                <asp:RadioButtonList ID="rblSelect" runat="server">
                    <asp:ListItem Text="Or" Value="or"/>
                    <asp:ListItem Text="Bronze" Value="bronze" />
                </asp:RadioButtonList>
            </div>
            <asp:Button ID="btnMettreAJour" runat="server" Text="Mettre à jour" OnClick="btnMettreAJour_Click"  />
            <p>
                <asp:Label ID="lblConfirmer" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblErreur" runat="server" Text=""></asp:Label>
            </p>
    </asp:Panel>
</asp:Content>
