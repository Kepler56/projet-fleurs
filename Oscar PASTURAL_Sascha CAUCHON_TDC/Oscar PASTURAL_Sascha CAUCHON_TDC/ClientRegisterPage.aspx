<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="ClientRegisterPage.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.ClientRegisterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleRegisterPage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HyperLink ID="hlAdminLogin" runat="server" NavigateUrl="AdminLogin.aspx">Admin Login</asp:HyperLink> &nbsp; | &nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="ClientLoginPage.aspx">Client Login</asp:HyperLink>
    <div class="center">
      <div id="login">
        <h3 id="title">Connexion</h3>
        <div id="nom">
             <label for="txtNom" class="form-label">Nom:</label>
             <asp:TextBox ID="txtNom" runat="server" CssClass="form-control"></asp:TextBox>
             <asp:RequiredFieldValidator CssClass="validators" ID="rfvNom" runat="server" ControlToValidate="txtNom" ErrorMessage="* Le nom est requis"></asp:RequiredFieldValidator>
        </div>
        <div id="prenom">
            <label for="txtPrenom" class="form-label">Prénom:</label>
            <asp:TextBox ID="txtPrenom" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validators" ID="rfvPrénom" runat="server" ControlToValidate="txtPrenom" ErrorMessage="* Le prénom est requis"></asp:RequiredFieldValidator>
        </div>
        <div id="telephone">
            <label for="txtTel" class="form-label">Numéro Téléphone:</label>
            <asp:TextBox ID="txtTel" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validators" ID="rfvTel" runat="server" ControlToValidate="txtTel" ErrorMessage="* Le numéro de téléphone est requis"></asp:RequiredFieldValidator>
        </div>
         <div id="courrier">
            <label for="txtEmail" class="form-label">Courrier:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validators" ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="* Le courrier est requis"></asp:RequiredFieldValidator>
        </div>
        <div id="password">
          <label for="txtPassword" class="form-label">Password:</label>
          <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
          <asp:RequiredFieldValidator CssClass="validators" ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="* Le mot de passe est requis"></asp:RequiredFieldValidator>
        </div>
        <div id="adresse">
            <label for="txtAdresse" class="form-label">Adresse de facturation:</label>
            <asp:TextBox ID="txtAdresse" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validators" ID="rfvAdresse" runat="server" ControlToValidate="txtAdresse" ErrorMessage="* L'adresse de facturation est requis"></asp:RequiredFieldValidator>
        </div>
        <div id="carteCredit">
            <label for="txtCarteCredit" class="form-label">Carte crédit:</label>
            <asp:TextBox ID="txtCarteCredit" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validators" ID="rfvCarteCredit" runat="server" ControlToValidate="txtCarteCredit" ErrorMessage="* L'adresse de facturation est requis"></asp:RequiredFieldValidator>
        </div>

        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn" OnClick="btnRegister_Click" />
        <p>
          <asp:Label ID="lblError" runat="server" CssClass="validators"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
        </p>
        <asp:Panel ID="pnlRegister" runat="server" Visible="False">
            <p>
                Le compte avec le même courrier existe déja!
            </p>
            <p>
                Si vous avez déjà un compte? Connectez-vous 
                <asp:HyperLink ID="hyperlinkRegister" NavigateUrl="ClientLoginPage.aspx" runat="server">ici.</asp:HyperLink>
            </p>
        </asp:Panel>
      </div>
     </div>
</asp:Content>
