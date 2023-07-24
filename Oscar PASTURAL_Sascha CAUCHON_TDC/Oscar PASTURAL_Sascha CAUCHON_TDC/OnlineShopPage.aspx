<%@ Page Title="" Language="C#" MasterPageFile="~/Fleurs.Master" AutoEventWireup="true" CodeBehind="OnlineShopPage.aspx.cs" Inherits="Oscar_PASTURAL_Sascha_CAUCHON_TDC.OnlineShopPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleOnlineShop.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftMenuColumn">
        <asp:DropDownList ID="ddlFiltres" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltres_SelectedIndexChanged">
            <asp:ListItem Text="Informations Clients" Value="infos" Selected="True"/>
            <asp:ListItem Text="Produits" Value="produits" />
            <asp:ListItem Text="Boutiques standards" Value="standard" />
            <asp:ListItem Text="Boutiques personnalisés" Value="personnalise" />
            <asp:ListItem Text="Suivre commande" Value="suivreCommande" />
        </asp:DropDownList>
        <p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="ClientLoginPage.aspx">Déconnexion</asp:HyperLink>
        </p>
    </div>
    <p>
        <asp:Label ID="lblBienvenue" runat="server" Text=""></asp:Label>
    </p>
    <asp:Panel ID="pnlProduits" runat="server" Visible="False">
        <p>Voici les produits du magasins:</p>
        <div class="table">
            <asp:GridView ID="dataTableProduits" runat="server" AutoGenerateColumns="True" CssClass="gridview"/>
        </div>
    </asp:Panel>
    <div>
        <asp:Panel ID="pnlInformationsClients" runat="server" Visible="False">
            <p>
                ID Client:
                <asp:Label ID="lblIDClient" runat="server" Text=""></asp:Label>
            </p>
            <p>
                Nom:
                <asp:Label ID="lblNom" runat="server" Text=""></asp:Label>
            </p>
            <p>
                Date de création de compte
                <asp:Label ID="lblCreation" runat="server" Text=""></asp:Label>
            </p>
            <p>
                Carte de crédit
                <asp:Label ID="lblCarteCredit" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:Button ID="btnStatut" runat="server" Text="Vérifier Statut" OnClick="btnStatut_Click" />
            </p>
            <p>
                <asp:Label ID="lblStatut" runat="server" Text=""></asp:Label>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlBoutiquesStandards" runat="server" Visible="False">
            <p>
                Sélectionner votre boutique:
            </p>
            <asp:DropDownList ID="ddlBoutiques" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltres_SelectedIndexChanged">
                <asp:ListItem Text="Boutique n°1" Value="1" Selected="True"/>
                <asp:ListItem Text="Boutique n°2" Value="2" />
                <asp:ListItem Text="Boutique n°3" Value="3" />
                <asp:ListItem Text="Boutique n°4" Value="4" />
                <asp:ListItem Text="Boutique n°5" Value="5" />
            </asp:DropDownList>
            <p>
                Sélectionner votre date de livraison:
            </p>
            <asp:TextBox ID="txtDateLivraison" runat="server" TextMode="Date"></asp:TextBox> <br />
            <asp:RequiredFieldValidator CssClass="validators" ID="rfvDateLivraison" runat="server" ControlToValidate="txtDateLivraison" ErrorMessage="* La date de livraison est requis"></asp:RequiredFieldValidator>
            <p>
                Sélectionner votre adresse de livraison:
            </p>
            <asp:TextBox ID="txtAdresseLivraison" runat="server"></asp:TextBox> <br />
            <asp:RequiredFieldValidator CssClass="validators" ID="rfvAdresseLivraison" runat="server" ControlToValidate="txtAdresseLivraison" ErrorMessage="* L'adresse de livraison est requis"></asp:RequiredFieldValidator>
            <p>
                Quelle message voulez-vous choisir pour accompagner le bouquet?
            </p>
            <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox><br />
            <p>
                <asp:GridView ID="dataTableBoutiquesStandards" runat="server" AutoGenerateColumns="False" CssClass="gridview" OnRowCommand="dataTableBoutiquesStandards_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="num_bouquet" HeaderText="Numéro bouquet" />
                        <asp:BoundField DataField="nom_bouquet" HeaderText="Nom bouquet" />
                        <asp:BoundField DataField="categorie" HeaderText="Catégorie" />
                        <asp:BoundField DataField="prix" HeaderText="Prix" />
                        <asp:TemplateField HeaderText="Sélectionner un bouquet">
                            <ItemTemplate>
                                <asp:Button ID="btnAjouter" runat="server" Text="Sélectionner" CommandName="select" CommandArgument='<%#Eval("num_bouquet") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </p>
            <p>
                <asp:Label ID="lblBouquetSelectionner" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:Button ID="btnConfirmerCommandeStandards" runat="server" Text="Confirmer" OnClick="btnConfirmerCommandeStandards_Click" />
            </p>
            <p>
                <asp:Label ID="lblConfirmationCommande" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:Label ID="lblPenurie" runat="server" Text=""></asp:Label>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlBoutiquesPersonnalises" runat="server" Visible="False">
            <p>
                Sélectionner votre boutique:
            </p>
            <asp:DropDownList ID="ddlBoutiquesP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltres_SelectedIndexChanged">
                <asp:ListItem Text="Boutique n°1" Value="1" Selected="True"/>
                <asp:ListItem Text="Boutique n°2" Value="2" />
                <asp:ListItem Text="Boutique n°3" Value="3" />
                <asp:ListItem Text="Boutique n°4" Value="4" />
                <asp:ListItem Text="Boutique n°5" Value="5" />
            </asp:DropDownList>
            <p>
                Sélectionner votre date de livraison:
            </p>
            <asp:TextBox ID="txtDateLivraisonP" runat="server" TextMode="Date"></asp:TextBox> <br />
            <asp:RequiredFieldValidator CssClass="validators" ID="rfvDateLivraisonP" runat="server" ControlToValidate="txtDateLivraisonP" ErrorMessage="* La date de livraison est requis"></asp:RequiredFieldValidator>
            <p>
                Sélectionner votre adresse de livraison:
            </p>
            <asp:TextBox ID="txtAdresseLivraisonP" runat="server"></asp:TextBox> <br />
            <asp:RequiredFieldValidator CssClass="validators" ID="rfvAdresseLivraisonP" runat="server" ControlToValidate="txtAdresseLivraisonP" ErrorMessage="* L'adresse de livraison est requis"></asp:RequiredFieldValidator>
            <p>
                Quelle message voulez-vous choisir pour accompagner le bouquet?
            </p>
            <asp:TextBox ID="txtMessageP" runat="server"></asp:TextBox><br />
            <!--<p>
                Donner une description de votre bouquet:
            </p>
            <asp:TextBox ID="txtDescriptionClient" runat="server" Height="100px" TextMode="MultiLine" Width="200px"></asp:TextBox><br />
            <p>
                Donner un prix maximum à votre bouquet:
            </p>
            <asp:TextBox ID="txtPrixMax" runat="server"></asp:TextBox>-->
            <p>
                <asp:GridView ID="dataTableBoutiquesPersonnalises" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="id_items" HeaderText="ID" />
                        <asp:BoundField DataField="nom_item" HeaderText="Nom" />
                        <asp:BoundField DataField="prix_item" HeaderText="Prix" />
                        <asp:BoundField DataField="type_item" HeaderText="Type" />
                        <asp:BoundField DataField="disponibilite" HeaderText="Disponibilité" />
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="quantity"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="add-button" 
                                    CommandName="AddToCart" 
                                    CommandArgument='<%# Eval("id_items")  %>' 
                                    OnClick = "btnAdd_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </p>
            <p>
                <asp:Label ID="lblCart" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:Button ID="btnConfirmerCommandePersonnalisee" runat="server" Text="Confirmer" OnClick="btnConfirmerCommandePersonnalisee_Click"/>
            </p>
            <p>
                <asp:Label ID="lblSuccessCommandeP" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:Label ID="lblPenurieP" runat="server" Text=""></asp:Label>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlSuivreCommande" runat="server" Visible="False">
            <div class="table">
                <asp:GridView ID="dataTableSuivreCommande" runat="server" AutoGenerateColumns="True" CssClass="gridview"/>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

