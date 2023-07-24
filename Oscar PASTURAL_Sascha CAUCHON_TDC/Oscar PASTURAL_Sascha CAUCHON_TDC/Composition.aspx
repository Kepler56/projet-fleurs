<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="Composition.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.Composition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleComposition.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftMenuColumn">
        <asp:DropDownList ID="ddlFiltres" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltres_SelectedIndexChanged">
            <asp:ListItem Text="Compositions" Value="composition" Selected="True"/>
            <asp:ListItem Text="Compositions Standards à CC" Value="standards"/>
            <asp:ListItem Text="Compositions Personnalisés à CC" Value="personnalises"/>
            <asp:ListItem Text="CC à CAL" Value="cal"/>
            <asp:ListItem Text="CAL à CL" Value="cl"/>
        </asp:DropDownList>
        <p>
            <asp:Button ID="btnGoBack" runat="server" Text="Go Back" OnClick="btnGoBack_Click" />
        </p>
        <asp:HyperLink ID="hlDéconnexion" runat="server" NavigateUrl="AdminLogin.aspx">Déconnexion</asp:HyperLink>
    </div>
    <div class="table">
        <asp:GridView ID="dataTableComposition" runat="server" AutoGenerateColumns="True" CssClass="gridview"/>
    </div>
    <div>
        <asp:Panel ID="pnlMAJEtatP" runat="server" Visible="False">
            <p>
                ID Commande:
                <asp:TextBox ID="txtCommande" runat="server" Text="COM"></asp:TextBox>
            </p>
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"/>
            <p>
                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlMAJEtatS" runat="server" Visible="False">
            <p>
                ID Commande:
                <asp:TextBox ID="txtCommandeS" runat="server" Text="COM"></asp:TextBox>
            </p>
            <asp:Button ID="btnUpdateS" runat="server" Text="Update" OnClick="btnUpdateS_Click" />
            <p>
                <asp:Label ID="lblSuccessS" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Label ID="lblErrorS" runat="server"></asp:Label>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlCCaCAL" runat="server" Visible="False">
            <p>
                ID Commande:
                <asp:TextBox ID="txtIDCommande" runat="server" Text="COM"></asp:TextBox>
            </p>
            <asp:Button ID="btnUpdateCCaCAL" runat="server" Text="Update" OnClick="btnUpdateCCaCAL_Click" />
            <p>
                <asp:Label ID="lblSuccessCCaCAL" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Label ID="lblErrorCCaCAL" runat="server"></asp:Label>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlCALaCL" runat="server" Visible="False">
            <asp:Button ID="btnUpdateCALaCL" runat="server" Text="Update" OnClick="btnUpdateCALaCL_Click" />
            <p>
                <asp:Label ID="lblSuccesCALaCL" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Label ID="lblErrorCALaCL" runat="server"></asp:Label>
            </p>
        </asp:Panel>
    </div>
</asp:Content>
