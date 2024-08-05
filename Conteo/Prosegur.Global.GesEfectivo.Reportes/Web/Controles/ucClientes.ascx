<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucClientes.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucClientes" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px; width: 730px;">
                <asp:Label ID="lblTitulo" runat="server" Text="" Style="color: #767676 !important; font-size: 9pt;" />
            </div>
            <div class="botaoOcultar">
                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15"></asp:TextBox>
                <asp:TextBox ID="txtDescricao" runat="server" Width="260px" MaxLength="36"></asp:TextBox>
                <pro:Botao ID="btnBuscar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                    Tipo="Consultar" Titulo="btnBuscar">
                </pro:Botao>
                <pro:Botao ID="btnLimpar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                    Tipo="Cancelar" Titulo="btnLimpiar">
                </pro:Botao>
            </div>
            <div>
                 <asp:HiddenField runat="server" ID="hdnSelecionado" />
                <dx:ASPxGridView runat="server" ID="gvDatos" AutoGenerateColumns="False" KeyFieldName="Codigo" Width="730" OnHtmlRowCreated="gvDatos_OnHtmlRowCreated" DataSourceForceStandardPaging="False" OnPageIndexChanged="gvDatos_OnPageIndexChanged" OnProcessOnClickRowFilter="gvDatos_OnProcessOnClickRowFilter">
                    <SettingsPager Position="Bottom" Mode="ShowPager">
                        <PageSizeItemSettings Items="10, 20, 50" />
                    </SettingsPager>
                     <Styles>
                            <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                     </Styles>
                    <Settings ShowFilterRow="True" EnableFilterControlPopupMenuScrolling="True"></Settings>
                    <SettingsBehavior FilterRowMode="OnClick" SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                    <Columns>
                        <dx:GridViewDataColumn Width="10px">
                            <DataItemTemplate>
                                <input id="rbSelecionado" type="radio" runat="server" class="radio_selecao" />
                                <input type="checkbox" id="chkSelecionado"  class="check_selecao" runat="server" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Width="150px" FieldName="Codigo">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText"></Settings>
                            <DataItemTemplate>
                                <asp:Label ID="lblItemCodigo" CssClass="limitText" Width="150px" runat="server" />
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Font-Size="10px"></HeaderStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Descripcion">
                             <Settings AutoFilterCondition="Contains" FilterMode="DisplayText"></Settings>
                            <DataItemTemplate>
                                <asp:Label ID="lblItemDescricao" CssClass="limitText" runat="server" />
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Font-Size="10px"></HeaderStyle>
                        </dx:GridViewDataColumn>
                          <dx:GridViewDataColumn  FieldName="CodigoAjeno">
                              <Settings ShowInFilterControl="False" ShowFilterRowMenu="False" AllowHeaderFilter="False" AllowAutoFilter="False"></Settings>
                            <DataItemTemplate>
                                <asp:Label ID="lblCodigoAjeno" CssClass="limitText" runat="server" />
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Font-Size="10px"></HeaderStyle>
                        </dx:GridViewDataColumn>
                          <dx:GridViewDataColumn  FieldName="DescripcionAjeno">
                               <Settings ShowInFilterControl="False" ShowFilterRowMenu="False" AllowHeaderFilter="False" AllowAutoFilter="False"></Settings>
                            <DataItemTemplate>
                                <asp:Label ID="lblDescripcionAjeno" CssClass="limitText" runat="server" />
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Font-Size="10px"></HeaderStyle>
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
               
            </div>
            <div id="divBotoes" runat="server" style="display: none">
                <table cellspacing="10">
                    <tr>
                        <td>
                            <asp:Button ID="btnAceptar" runat="server" class="btn-visualizar" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelar" runat="server" class="btn_cancelar" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
