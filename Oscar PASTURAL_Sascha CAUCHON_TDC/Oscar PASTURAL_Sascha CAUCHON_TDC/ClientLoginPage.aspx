<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="ClientLoginPage.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.ClientLoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleClientLogin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:HyperLink ID="hlAdminLogin" runat="server" NavigateUrl="AdminLogin.aspx">Admin Login</asp:HyperLink>
    <div class="center">
      <div id="login">
        <h3 id="title">Connexion à l'interface client</h3>
        <div id="username">
          <label for="txtUsername" class="form-label">Email:</label>
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
        <p>
            Vous n'avez pas d'identifiant ? Créer un compte 
            <asp:HyperLink ID="hyperlinkRegister" NavigateUrl="ClientRegisterPage.aspx" runat="server">ici.</asp:HyperLink>
        </p>
      </div>
    </div>
</asp:Content>
