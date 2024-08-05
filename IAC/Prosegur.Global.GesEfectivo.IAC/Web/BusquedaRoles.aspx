<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="BusquedaRoles.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaRoles" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario.Tradutor" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-collapse legend-expand");
            $(".accordion").slideToggle("fast");
        };
        function ManterFiltroAberto() {
            $("#DivFiltros").addClass("legend-expand");
            $(".accordion").show();
        };
    </script>
    <style type="text/css">
        .styleLabel {
            float: left;
            width: 120px;
            padding-top: 5px;
        }

        .styleLabel2 {
            float: left;
            width: 70px;
            padding-top: 5px;
        }

        .styleLabel3 {
            float: left;
            padding-top: 5px;
        }

        .styleColuna1 {
            float: left;
            width: 231px;
            margin-right: 5px;
        }

        .styleColuna2 {
            float: left;
            width: 250px;
            margin-right: 5px;
        }

        .auto-style1 {
            height: 72px;
        }

        .margen-left-cero > input[type="checkbox"] {
            margin-left: 0 !important;
        }

        .botones {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:HiddenField runat="server" ID="hiddenCodigo" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <div id="Filtros" style="display: block;">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server"></asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block;">
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <div>
                                            <div>
                                                <div class="styleLabel">
                                                    <asp:Label ID="lblNombreRol" runat="server" CssClass="label2"></asp:Label>
                                                </div>
                                                <div class="styleColuna1">
                                                    <asp:TextBox ID="txtNombreRol" Width="200" runat="server" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="styleLabel">
                                                    <asp:Label ID="lblModulo" runat="server" CssClass="label2"></asp:Label>
                                                </div>
                                                <div class="styleColuna2">
                                                    <asp:DropDownList ID="ddlModulo" Width="200" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="styleLabel2">
                                                    <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                                </div>
                                                <div class="styleLabel2">
                                                    <asp:CheckBox Style="margin-left: 0px !important;" ID="chkVigente" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table style="margin: 0px !Important;">
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="100px" />
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </fieldset>
                </div>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblTituloRoles" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabelaGrid">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnGrid" Visible="False">
                                        <dx:ASPxGridView runat="server" ID="grid" ClientInstanceName="grid"
                                            AutoGenerateColumns="False" KeyFieldName="Codigo" Width="99%">
                                            <Styles>
                                                <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                                                <RowHotTrack CssClass="GridLinhaDestaque2"></RowHotTrack>
                                            </Styles>
                                            <Columns>
                                                <dx:GridViewDataColumn Width="140" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:ImageButton
                                                            runat="server"
                                                            ImageUrl="~/App_Themes/Padrao/css/img/button/edit.png"
                                                            OnClientClick='<%# BuscaPostbackGrid("EDITAR", Container.KeyValue) %>' 
                                                            ID="imgEditar"
                                                            CssClass="imgButton"
                                                            ToolTip='<%# Traduzir("btnModificacion") %>' />
                                                        <asp:ImageButton
                                                            runat="server"
                                                            ImageUrl="~/App_Themes/Padrao/css/img/button/buscar.png"
                                                            OnClientClick='<%# BuscaPostbackGrid("CONSULTAR", Container.KeyValue) %>' 
                                                            ID="imgConsultar"
                                                            CssClass="imgButton"
                                                            ToolTip='<%# Traduzir("btnConsulta") %>' />
                                                        <asp:ImageButton
                                                            runat="server"
                                                            ImageUrl="~/App_Themes/Padrao/css/img/button/borrar.png"
                                                            OnClientClick='<%# BuscaPostbackGrid("ELIMINAR", Container.KeyValue) %>' 
                                                            ID="imgBaja"
                                                            CssClass="imgButton"
                                                            ToolTip='<%# Traduzir("btnBaja") %>' />
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataTextColumn FieldName="Aplicacion.Descripcion">
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Codigo">
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataColumn FieldName="Activo">
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                                                    <DataItemTemplate>
                                                        <asp:Image runat="server"
                                                            ImageUrl="~/Imagenes/contain01.png"
                                                            ID="imgBolvigenteTrue"
                                                            CssClass="imgButton"
                                                            Visible='<%# Eval("Activo") %>' />
                                                        <asp:Image runat="server"
                                                            ImageUrl="~/Imagenes/nocontain01.png"
                                                            ID="Image1"
                                                            CssClass="imgButton"
                                                            Visible='<%# Not Eval("Activo") %>' />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <SettingsPager PageSize="10">
                                                <PageSizeItemSettings Visible="True" />
                                            </SettingsPager>
                                            <SettingsBehavior AllowSort="True" />
                                        </dx:ASPxGridView>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGridSemRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                            <tr>
                                                <td style="border-width: 0;"></td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="label2">Não existem dados a serem exibidos.</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="margin-top: 20px;">
            <asp:UpdatePanel runat="server" ID="updForm">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnForm" Visible="True">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloMantenimientoRoles" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos" style="width: 60%;">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblNombreForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtNombreRoleForm" runat="server" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="200px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtNombreRoleForm" runat="server" />--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescripcionForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td rowspan="2">
                                    <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescRolForm" Style="resize: none; box-shadow: none" runat="server" TextMode="MultiLine" Height="50px" Width="200px" MaxLength="255" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtDescRolForm" runat="server" />--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <div class="styleLabel3">
                                        <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                    </div>
                                </td>
                                <td>
                                    <div class="styleLabel2">
                                        <asp:CheckBox CssClass="margen-left-cero" ID="chkVigenteForm" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloPermisos" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanelPermisosForm" runat="server">
                            <ContentTemplate>
                                <div style="margin-left: 7px">
                                    <div style="margin-bottom: 20px;">
                                        <div class="styleLabel">
                                            <asp:Label ID="lblModuloForm" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <div class="">
                                            <asp:DropDownList ID="ddlModuloForm" runat="server" Width="210px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div style="float: left;">
                                        <div>
                                            <asp:Label ID="lblPermisosDisponibles" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:ListBox ID="lsbPermisosDisponibles" Height="120px" Width="400px" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div style="float: left; height:124px; width: 25px; margin: 12px 10px 0 10px" >
                                        <div id="divBotones" runat="server">
                                            <asp:ImageButton runat="server" ID="btnPermisoAgregarTodos" CssClass="botones" ImageUrl="~/Imagenes/pag007.jpg" />
                                            <asp:ImageButton runat="server" ID="btnPermisoAgregar" CssClass="botones" ImageUrl="~/Imagenes/pag005.jpg" />
                                            <asp:ImageButton runat="server" ID="btnPermisoQuitar" CssClass="botones" ImageUrl="~/Imagenes/pag006.jpg" />
                                            <asp:ImageButton runat="server" ID="btnPermisoQuitarTodos" CssClass="botones" ImageUrl="~/Imagenes/pag008.jpg" />
                                        </div>
                                    </div>
                                    <div>
                                        <div>
                                            <asp:Label ID="lblPermisosAsignados" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:ListBox ID="lsbPermisosAsignados" Height="120px" Width="400px" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table>
            <tr align="center">
                <td>
                    <asp:Button runat="server" ID="btnNovo" CssClass="btn-novo"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnGrabar" CssClass="btn-salvar"/>
                </td>
                 <td >
                    <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir"/>
                    <div class="botaoOcultar">
                        <asp:Button runat="server" ID="btnHabilitaEdicao"  CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnHabilitaConsulta"  CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnHabilitarExclusao" CssClass="ui-button"/>
                        <asp:Button runat="server" ID="btnAlertaSi" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnAlertaNo" CssClass="btn-excluir" />
                        <dx:ASPxButton ID="btnGrid" ClientInstanceName="btnGrid" AutoPostBack="true" runat="server" Visible="false" Text="btnGrid" ClientIDMode="Static" UseSubmitBehavior="False" />
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
