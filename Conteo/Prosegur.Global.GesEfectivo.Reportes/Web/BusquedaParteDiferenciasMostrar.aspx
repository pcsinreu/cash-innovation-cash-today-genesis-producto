<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaParteDiferenciasMostrar.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.BusquedaParteDiferenciasMostrar" EnableEventValidation="false" MasterPageFile="~/Master/MasterModal.Master" %>
<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register assembly="Prosegur.Web" namespace="Prosegur.Web" tagprefix="pro" %>
<%@ Register src="Controles/Erro.ascx" tagname="Erro" tagprefix="uc1" %>
<%@ Register src="Controles/Cabecalho.ascx" tagname="Cabecalho" tagprefix="uc3" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Reportes - Busqueda Parte Diferencias</title>
        <style type="text/css"> 
                
            .style7
            {
                height: 20px;
            }
                
        </style>   
     <script src="JS/Funcoes.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div class="content-modal">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>           
                  <div class="ui-panel-titlebar">
            <asp:Label ID="lblTituloDocumentos" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
                <table class="tabela_campos">
                    <tr>
                        <td align="left" style="padding-left: 22px; font-size: medium; font-family: Arial;">
                            <asp:Label ID="lblPrecinto" runat="server" CssClass="Lbl2" Font-Bold="true"></asp:Label>:
                            <asp:Label ID="lblPrecintoNumero" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:HiddenField runat="server" ID="hdHayDocumentos" />
                    <dx:ASPxGridView runat="server" ID="gvParteDiferenciasDocumentos" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnHtmlRowCreated="gvParteDiferenciasDocumentos_OnHtmlRowCreated" >
                        <SettingsPager Position="Bottom" Mode="ShowPager">
                            <PageSizeItemSettings Items="10, 20, 50" />
                        </SettingsPager>
                        <Styles>
                            <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                        </Styles>
                        <SettingsBehavior SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="FechaCreacion">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="NumeroDocumento">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="HayDocumentoGeneral">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                 <DataItemTemplate>
                                    <input id="ckSelecionadoG" type="checkbox" runat="server" class="checkbox" />
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="HayDocumentoIncidencia">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                 <DataItemTemplate>
                                    <input id="ckSelecionadoI" type="checkbox" runat="server" class="checkbox" />
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="HayDocumentoJustificacion">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                 <DataItemTemplate>
                                    <input id="ckSelecionadoJ" type="checkbox" runat="server" class="checkbox" />
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </div>
                </ContentTemplate>
     </asp:UpdatePanel>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>
            <div class="content-modal_botoes">
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnVisualizar" CssClass="btn-visualizar" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
 
       


