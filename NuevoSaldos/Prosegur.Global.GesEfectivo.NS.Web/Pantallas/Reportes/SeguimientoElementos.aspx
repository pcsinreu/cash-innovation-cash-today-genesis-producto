<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="SeguimientoElementos.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.SeguimientoElementos" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>

<%@ Register Src="~/Controles/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div id="dvTituloFiltro" runat="server" class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblFiltros" runat="server" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <div class="dvUsarFloat">
            <div style="width: 98%; height: auto;">
                <div style="width: 180px;">
                    <b>
                        <uc1:ucTextBox ID="ucPrecintoDelElemento" runat="server" Tamano="20" TipoInterno="Alfanumerico" />
                    </b>
                    <br />
                </div>
                <div style="width: 180px;">
                    <b>
                        <asp:Label ID="lblTipoElemento" runat="server"></asp:Label></b><br />
                    <asp:DropDownList ID="ddlTipoElemento" runat="server" Style="width: 160px"></asp:DropDownList>
                </div>
                <div>
                    <b>
                        <uc3:ucRadioButtonList ID="ucFormato" runat="server" />
                    </b>
                    <br />
                </div>
            </div>
        </div>
        <div class="dvclear"></div>
        <div style="margin: 0px 25px 10px 0px; height: auto;">
            <asp:Button ID="btnBuscar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
        </div>
        <div id="dvTituloGrid" runat="server" visible="false" class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblTituloGrid" runat="server" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <div class="dvclear">
        </div>
        <div>
            <asp:GridView ID="gdvContenedor" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataKeyNames="Precintos" AllowSorting="True" Visible="false"
                EnableModelValidation="True" BorderStyle="None" EmptyDataRowStyle-BorderWidth="5" OnPageIndexChanging="gdv_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbSelecionado" ValidationGroup="rbSelecionado" GroupName="rbSelecionado" runat="server" OnCheckedChanged="rbSelecionado_CheckedChanged" AutoPostBack="true"/>
                        </ItemTemplate>
                        <HeaderStyle Width="10px" />
                        <ItemStyle Width="10px" />
                        <FooterStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Precintos" HeaderText="Precintos" ReadOnly="True">
                        <HeaderStyle Width="75px" />
                        <ItemStyle Width="75px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaHoraArmado" HeaderText="Fecha/Hora Armado" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True">
                        <HeaderStyle Width="75px" />
                        <ItemStyle Width="75px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AgrupaElementos" HeaderText="Agrupa Elementos?" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cuentas" HeaderText="Cuentas" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
                <EmptyDataTemplate>
                    <div class="EmptyData">
                        <span>
                            <%# Tradutor.Traduzir("lblSemRegistro")%> 
                        </span>
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:GridView ID="gdvBulto" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataKeyNames="Precinto" AllowSorting="True" Visible="false"
                EnableModelValidation="True" BorderStyle="None" EmptyDataRowStyle-BorderWidth="5" OnPageIndexChanging="gdv_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbSelecionado" ValidationGroup="rbSelecionado" GroupName="rbSelecionado" runat="server" OnCheckedChanged="rbSelecionado_CheckedChanged" AutoPostBack="true"/>
                        </ItemTemplate>
                        <HeaderStyle Width="10px" />
                        <ItemStyle Width="10px" />
                        <FooterStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Precinto" HeaderText="Precinto" ReadOnly="True">
                        <HeaderStyle Width="75px" />
                        <ItemStyle Width="75px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodigoExterno" HeaderText="Codigo Externo" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaHoraCreacion" HeaderText="Fecha/Hora Creacion" ReadOnly="True">
                        <HeaderStyle Width="75px" />
                        <ItemStyle Width="75px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TipoServicio" HeaderText="TipoServicio" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cuenta" HeaderText="Cuentas" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
                <EmptyDataTemplate>
                    <div class="EmptyData">
                        <span>
                            <%# Tradutor.Traduzir("lblSemRegistro")%> 
                        </span>
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:GridView ID="gdvRemesa" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataKeyNames="CodigoExterno" AllowSorting="True" Visible="false"
                EnableModelValidation="True" BorderStyle="None" EmptyDataRowStyle-BorderWidth="5" OnPageIndexChanging="gdv_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbSelecionado" ValidationGroup="rbSelecionado" GroupName="rbSelecionado" runat="server" OnCheckedChanged="rbSelecionado_CheckedChanged" AutoPostBack="true"/>
                        </ItemTemplate>
                        <HeaderStyle Width="10px" />
                        <ItemStyle Width="10px" />
                        <FooterStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CodigoExterno" HeaderText="Codigo Externo" ReadOnly="True">
                        <HeaderStyle Width="75px" />
                        <ItemStyle Width="75px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaHoraCreacion" HeaderText="Fecha/Hora Creacion" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True">
                        <HeaderStyle Width="75px" />
                        <ItemStyle Width="75px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaHoraTransporte" HeaderText="Fecha/Hora Transporte" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
                <EmptyDataTemplate>
                    <div class="EmptyData">
                        <span>
                            <%# Tradutor.Traduzir("lblSemRegistro")%> 
                        </span>
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center">
        <ns:Boton ID="btnGenerarReporte" runat="server" Enabled="true" Text="F4 Grabar" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png"
            TeclaAtalho="F4" />
    </div>
</asp:Content>
