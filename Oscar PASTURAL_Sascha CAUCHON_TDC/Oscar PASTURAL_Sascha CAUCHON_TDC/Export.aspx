<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.Export" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Module Export:</h4>
    <p>
        Export en XML des clients ayant commandé plusieurs fois durant le dernier mois:
    </p>
    <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
    <p>
        <asp:Label ID="lblExport" runat="server" Text=""></asp:Label>
    </p>
</asp:Content>
