<%@ Page Title="" Language="vb"  EnableEventValidation="True" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="Reportes.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.Reportes" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="Controles/ucDelegacion.ascx" TagName="ucDelegacion" TagPrefix="uc1" %>
<%@ Register Src="Controles/ucData.ascx" TagName="ucData" TagPrefix="uc2" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="head">
        <title>Reportes - Parte de Diferencias</title>
    <style type="text/css">
        .botaoOcultar {
            display: none;
        }
    </style>
    <script src="JS/Master/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="botaoOcultar">
                <asp:Button runat="server" ID="btnConfEscogido" />
                   <asp:TextBox ID="txtBuscar" runat="server" Width="350px"></asp:TextBox>
            </div>
            <div class="content">
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTituloCriteriosBusqueda" CssClass="ui-panel-title" runat="server"></asp:Label>

                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="updGrid" ChildrenAsTriggers="True" UpdateMode="Conditional">
                        <ContentTemplate>

                    <asp:HiddenField runat="server" ID="hdReportes" />
                    <dx:ASPxGridView runat="server" ID="gvReportes" AutoGenerateColumns="False" KeyFieldName="IdentificadorConfiguracion" Width="100%" OnHtmlRowCreated="gvReportes_OnHtmlRowCreated" OnPageIndexChanged="gvReportes_OnPageIndexChanged" OnProcessOnClickRowFilter="gvReportes_OnProcessOnClickRowFilter">
                        <SettingsPager Position="Bottom" Mode="ShowPager">
                            <PageSizeItemSettings Items="10, 20, 50" />
                        </SettingsPager>
                         <Settings ShowFilterRow="True" EnableFilterControlPopupMenuScrolling="True"></Settings>
                           <SettingsBehavior FilterRowMode="OnClick" SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                        <Styles>
                            <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                        </Styles>
                        <Columns>
                            <dx:GridViewDataColumn Width="10px">
                                <DataItemTemplate>
                                    <input id="ckSelecionado" type="checkbox" runat="server" class="check_selecao" />
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="DesConfiguracion">
                                  <Settings AutoFilterCondition="Contains" FilterMode="DisplayText"></Settings>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
           
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div>
                    <table>
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="UpdatePanelGridSemRegistro" runat="server" ChildrenAsTriggers="False"
                                    UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                            <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                                <tr>
                                                    <td style="border-width: 0;">
                                                        <asp:Image ID="imgErro" runat="server" src="Imagenes/info.jpg" />
                                                    </td>
                                                    <td style="border-width: 0;">
                                                        <asp:Label ID="lblSemRegistro" runat="server" CssClass="Lbl2" Text="Label">[lblSemRegistro]</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <table class="tabela_campos" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <uc1:ucDelegacion ID="ucDelegacion" runat="server" Visible="false" />
                                    <uc1:ucDelegacion ID="ucDelegacionUsuario" runat="server" Visible="false" />
                                </td>
                            </tr>
                        </table>
                        <table class="tabela_campos" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <asp:Panel ID="panelFechaConteo" runat="server">
                                        <uc2:ucData ID="ucDataConteo" runat="server" Visible="False" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <table class="tabela_campos" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <asp:Panel ID="panelFechaTransporte" runat="server">
                                        <uc2:ucData ID="ucDataTransporte" runat="server" Visible="false" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </table>
                    
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <center>
                <table>
                    <tr align="center">
                        <td>
                           <asp:Button runat="server" ID="btnGerarRelatorio" CssClass="btn-visualizar"/>
                        </td>
                        <td>
                             <asp:Button runat="server" ID="btnAdicionar" CssClass="btn-novo"/>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnEditar" CssClass="btn-edit"/>  
                            </td>
                        <td>
                              <input type="button" id="valBtnExcluir" class="btn-excluir" runat="server"/>
                        </td>
                        <td>
                             <asp:Button runat="server" ID="btnConfiguracionGeneral" CssClass="btn-config"/>
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div class="botaoOcultar">
         <asp:Button runat="server" ID="btnExcluir" CssClass="btn-excluir"/>
    </div>
</asp:Content>
