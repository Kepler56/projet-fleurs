<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.AdminLogin" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleAdminLogin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HyperLink ID="hlAdminLogin" runat="server" NavigateUrl="ClientLoginPage.aspx">Client Login</asp:HyperLink>
    <div class="center">
      <div id="login">
        <h3 id="title">Connexion à l'interface d'administration</h3>
        <div id="username">
          <label for="txtUsername" class="form-label">Username:</label>
          <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
          <asp:RequiredFieldValidator CssClass="validators" ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="* L'identifiant est requis"></asp:RequiredFieldValidator>
        </div>
        <div id="password">
          <label for="txtPassword" class="form-label">Password:</label>
          <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
          <asp:RequiredFieldValidator CssClass="validators" ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="* Le mot de passe est requis"></asp:RequiredFieldValidator>
        </div>
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn" />
        <p>
          <asp:Label ID="lblError" runat="server" CssClass="validators"></asp:Label>
        </p>
      </div>
    </div>
</asp:Content>