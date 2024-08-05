<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="ConvertirCertificado.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ConvertirCertificado" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<%@ Register Src="~/Controles/PopupCertificados.ascx" TagName="Certificado" TagPrefix="ucPopup" %>
<%@ Register Src="~/Controles/ucLista.ascx" TagName="Lista" TagPrefix="ucLista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content">
        <div runat="server" id="dvEntrada">
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblTituloEntrada" runat="server" Text="Filtros" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
            </div>
            <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                <div class="dvUsarFloat">
                    <div id="dvTipoCertificado" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblTipoCertificado" runat="server" Text="Tipo Certificado"></asp:Label></b><br />
                        <asp:DropDownList ID="ddlTipoCertificado" runat="server"></asp:DropDownList>
                    </div>
                    <div class="dvclear"></div>
                    <div id="dvCliente" runat="server" style="margin-top: 0px;">
                        <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="phCliente" runat="server"></asp:PlaceHolder>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="dvclear"></div>
                </div>
                <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGrid" runat="server" Visible="false">
                            <asp:GridView ID="gdvCertificados" DataKeyNames="IdentificadorCertificado" runat="server" EnableModelValidation="True"
                                AllowPaging="True" AutoGenerateColumns="False" Width="99%">
                                <Columns>
                                    <asp:TemplateField>
                                        <FooterTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rbSelecionado" ValidationGroup="rbSelecionado" GroupName="rbSelecionado"
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codCertificado" HeaderText="CodigoCertificado">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codEstado" HeaderText="TipoCertificado">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fyhCertificado" HeaderText="Data/Hora">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <div id="dvBuscar" runat="server" style="display: block; margin-top: 0px; line-height: 35px;">
                                <asp:Button ID="btnConsulta" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblTitulo2" runat="server" Text="Resultado de la consulta" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
            </div>
            <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div runat="server" id="dvResultado">
                            <div class="dvUsarFloat">
                                <div class="dvclear"></div>
                                <div id="dvIdentificadorCertificado" runat="server" style="margin-top: 0px;">
                                    <asp:Label ID="lblIdentificador" Width="80px" runat="server"
                                        Text="lblIdentificador" /></b><br />
                                    <asp:TextBox ID="txtIdentificador" Width="200px" runat="server" ReadOnly="true" AutoPostBack="True" />
                                </div>
                                <div id="dvFecha" runat="server" style="margin-top: 0px;">
                                    <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label></b><br />
                                    <asp:TextBox ID="txtFecha" runat="server" />
                                </div>
                                <div class="dvclear"></div>
                                <div id="dvListaDelegaciones" runat="server" style="margin-top: 0px; width: auto !important;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ucLista:Lista runat="server" ID="listaDelegaciones" modo="Alta" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="dvListaSectores" runat="server" style="margin-top: 0px; width: auto !important;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ucLista:Lista runat="server" ID="listaSectores" modo="Alta" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div> 
                                <div id="dvListaSubCanales" runat="server" style="margin-top: 0px; width: auto !important;">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ucLista:Lista runat="server" ID="listaSubCanales" modo="Alta" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="dvclear"></div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <ucPopup:Certificado ID="popupCuestion" AutoAbrirPopup="false" Width="650" EsModal="false" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table>
            <tr>
                <td>                   
                    <ns:Boton ID="btnConsultaConfiguracaoSaldo" runat="server" Enabled="false" Text="" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png" TeclaAtalho="F4" />                    
                </td>
                <td>                    
                    <ns:Boton ID="btnValidarCertificado" runat="server" Text="" ImageUrl="~/App_Themes/Padrao/css/img/iconos/icon_active.png" TeclaAtalho="F10" />                    
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
