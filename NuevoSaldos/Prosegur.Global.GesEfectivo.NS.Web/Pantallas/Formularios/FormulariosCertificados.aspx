<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="FormulariosCertificados.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.FormulariosCertificados" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div runat="server" id="dvFiltro">
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblTituloFiltro" runat="server" Text="Filtros" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
            </div>
            <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                <div class="dvUsarFloat">
                    <div id="dvCliente" runat="server" style="margin-top: 0px;">
                        <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="dvCheckBoxListTiposClientes" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblTiposClientes" runat="server" Text="Tipo Cliente"></asp:Label><br />
                        <div style="width: 300px; height: 50px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                            <asp:CheckBoxList ID="cklTiposClientes" runat="server"></asp:CheckBoxList>
                        </div>
                    </div>
                    <div id="dvCheckBoxListTiposReporte" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblTiposReporte" runat="server" Text="Tipo Reporte"></asp:Label><br />
                        <div style="width: 300px; height: 50px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                            <asp:CheckBoxList ID="cklTiposReporte" runat="server" RepeatColumns="1"></asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="dvclear"></div>
                    <div id="dvCodigo" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblCodigo" runat="server" Text="Codigo" /><br />
                        <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" Width="97px" />
                    </div>
                    <div id="dvDescricion" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblDescricao" runat="server" Text="Descricion" /><br />
                        <asp:TextBox ID="txtDescricao" runat="server" Width="213px" />
                    </div>
                    <div class="dvclear"></div>
                    <div style="height: auto;">
                        <asp:Button ID="btnBuscar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 0px;" />
                    </div>
                    <div class="dvclear"></div>
                </div>
            </div>
        </div>
        <div runat="server" id="dvResultadoFiltro">
            <div id="dvTituloResultado" runat="server" class="ui-panel-titlebar" style="display: block; margin-bottom: 2px; padding-left: 20px;">
                <asp:Label ID="lblResultados" runat="server" Text="Resultado de la Consulta" Style="color: #767676 !important; font-size: 9pt;" />
            </div>
            <style>
                .ColunaSemMargin {
                    margin: 0px !important;
                    padding: 0px !important;
                }
            </style>
            <asp:UpdatePanel ID="upResultado" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="dvResultado" runat="server" style="margin-left: 10px; margin-top: 5px;">
                        <asp:GridView ID="grvResultadoConfigReporte" runat="server" AutoGenerateColumns="False"
                            EnableModelValidation="True" BorderStyle="None" Width="98%">
                            <Columns>
                                <asp:TemplateField HeaderText="Codigo">
                                    <ItemTemplate>
                                        <asp:Label ID="Codigo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="11px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descricion">
                                    <ItemTemplate>
                                        <asp:Label ID="Descricion" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="11px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Direccion">
                                    <ItemTemplate>
                                        <asp:Label ID="Direccion" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="11px" />
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modificar">
                                    <ItemTemplate>
                                        <asp:Literal ID="litImgModificar" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                    <HeaderStyle Width="90px" Font-Size="11px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgEliminar" ImageUrl="~/Imagenes/Quitar2.png" OnCommand="ImgEliminar_Command" Style="background: none !important;" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="90px" Font-Size="11px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="height: auto; text-align: left; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                    <%# Tradutor.Traduzir("lblSemRegistro")%>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <asp:UpdatePanel ID="upBotones" runat="server">
        <ContentTemplate>
            <center>
            <table style="border-collapse: collapse;">
                <tr>
                    <td>
                        <asp:PlaceHolder ID="phAcciones" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

