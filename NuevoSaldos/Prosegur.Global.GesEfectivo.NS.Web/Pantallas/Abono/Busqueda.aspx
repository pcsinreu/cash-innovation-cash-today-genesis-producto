<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Busqueda.aspx.vb" MasterPageFile="~/Master/Master.master"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Abono.Busqueda" %>

<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/ucBusquedaAvanzada.ascx" TagPrefix="ns" TagName="ucBusquedaAvanzada" %>
<%@ Register Src="~/Controles/ucBusqueda.ascx" TagPrefix="ns" TagName="ucBusqueda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/js/Bootstrap/css/bootstrap.min.css")%>">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery.paginate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/Busqueda.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Bootstrap/js/bootstrap.min.js")%>"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <style>
            .popover
            {
                max-width: 400px;
                max-height: 200px;
                overflow-y: auto;
            }
            /* Popover Header */
            .popover-title {
                font-size: 12px;
                text-align: center;
            }
            /* Popover Body */
            .popover-content {
                font-size: 8px;
                text-align: left;
            }

        </style>
        <div id="dvFiltro" style="width:99%">
            <div class="ui-panel-titlebar">
                <span id="lblTituloFiltro" class="ui-panel-title" style="color: #767676 !important;"><%= Tradutor.Traduzir("071_Busqueda_Titulo_Filtro")%></span>
            </div>
            <div class="dvForm dvFormNO" style="margin-left: 15px;">
                <div id="dvBancoCliente" style="width: 415px !important; height: 60px !important; margin-bottom: 0px !important;">
                    <ns:ucBusqueda runat="server" ID="UcBanco" Tipo="Banco" Titulo="Banco" EsMulti="False" VisibilidadInicial="True" AtributoDataBinding="" />
                    <ns:ucBusqueda runat="server" ID="UcCliente" Tipo="Cliente" Titulo="Cliente" EsMulti="False" VisibilidadInicial="True" AtributoDataBinding="" />
                    <ns:ucBusquedaAvanzada runat="server" ID="ucBusquedaAvanzada" />
                </div>
                <div id="dvFechasEstado" style="width: 415px !important; height: 60px !important; margin-bottom: 0px !important;">
                    <div>
                        <span id="lblFechaDesde"><%= Tradutor.Traduzir("071_Busqueda_Campo_FechaHoraDesde")%></span><br>
                        <input name="txtFechaDesde" type="text" id="txtFechaDesde" value="" class="controleFecha" style="width: 152px;" onchange="javascript: cargaValorFiltro(this);" />
                        <script>AbrirCalendario('txtFechaDesde', 'True')</script>
                    </div>
                    <div>
                        <span id="lblFechaHasta"><%= Tradutor.Traduzir("071_Busqueda_Campo_FechaHoraHasta")%></span><br>
                        <input name="txtFechaHasta" type="text" id="txtFechaHasta" class="controleFecha" style="width: 152px;" onchange="javascript: cargaValorFiltro(this);" />
                        <script>AbrirCalendario('txtFechaHasta', 'True')</script>
                    </div>
                    <div class="dvclear"></div>
                    <div>
                        <span id="lblEstado"><%= Tradutor.Traduzir("071_Busqueda_Campo_Estado")%></span><br>
                        <select id="txtEstado" class="ui-gn-mandatory ui-widget ui-state-default ui-corner-all" style="width: 157px;" onchange="javascript: cargaValorFiltro(this);">
                        </select>
                    </div>
                </div>
                <div id="dvTipoNumeroCodigo" style="width: 415px !important; height: 60px !important; margin-bottom: 0px !important;">
                    <div>
                        <span id="lblTipoAbono"><%= Tradutor.Traduzir("071_Comon_Campo_TipoAbono")%></span><br>
                        <select id="txtTipoAbono" class="ui-gn-mandatory ui-widget ui-state-default ui-corner-all" style="width: 165px;" onchange="javascript:visibleNumExterno(this);">
                        </select>
                    </div>
                    <div style="margin-left: 15px; display: none;" id="dvNumeroExterno">
                        <span id="lblNumeroExterno"><%= Tradutor.Traduzir("071_Comon_Campo_NumeroExterno")%></span><br>
                        <input name="txtNumeroExterno" type="text" id="txtNumeroExterno" style="width: 169px;" onchange="javascript: cargaValorFiltro(this);" />
                    </div>
                    <div class="dvclear"></div>
                    <div style="margin-bottom: 3px;">
                        <span id="lblCodigo"><%= Tradutor.Traduzir("071_Comon_Campo_Codigo")%></span><br>
                        <input name="txtCodigo" type="text" id="txtCodigo" style="width: 350px;" onchange="javascript: cargaValorFiltro(this);" />
                    </div>
                </div>
                <div id="dvDivisas" style="width: 415px !important; margin-bottom: 0px !important;">
                    <div class="lista">
                        <div id="dvTitulo">
                            <span id="lblDivisas"><%= Tradutor.Traduzir("071_Comon_Campo_Divisa")%></span>
                        </div>
                        <div class="dvclear"></div>
                        <div id="dvValores" class="valores" style="width: 340px; height: 55px;">
                        </div>
                        <div id="dvAcciones" class="acciones" style="width: 12px; margin-left: 3px;">
                            <button type="button" value="" id="btnAgregarTodos" class="imagem" onclick="seleccionarTodos(document.getElementById('dvValores'), true)">
                                <img src="<%= ResolveUrl("~/Imagenes/ckeck_true.png")%>" alt="" />
                            </button>
                            <br />
                            <button type="button" value="" id="btnDesAgregarTodos" class="imagem" onclick="seleccionarTodos(document.getElementById('dvValores'), false)">
                                <img src="<%= ResolveUrl("~/Imagenes/ckeck_false.png")%>" alt="" />
                            </button>
                        </div>
                    </div>
                </div>
                <div class="dvclear">
                </div>
                <div id="dvBoton" style="width: 415px !important; height: 40px !important; margin-top: 10px !important;">
                    <input type="button" value="<%= Tradutor.Traduzir("071_Comon_Boton_Buscar")%>" id="btnBuscar" class="boton" onclick="javascript: obtenerAbonos();">
                </div>
                <div class="dvclear">
                </div>
            </div>
        </div>

        <div id="dvRespuestaFiltro" style="width:99%">
            <div class="ui-panel-titlebar">
                <span id="lblTituloRespuestaFiltro" class="ui-panel-title" style="color: #767676 !important;"><%= Tradutor.Traduzir("071_Busqueda_Titulo_RespuestaConsulta")%></span>
            </div>
            <div id="dvRespuestasAbonos" class="gridAbono gridAbonoNO">
            </div>
        </div>
    </div>

    <asp:Literal ID="litDicionario" runat="server"></asp:Literal>
    <asp:Literal ID="litCargaInicial" runat="server"></asp:Literal>

    <script>
        $(function () {
            cargarValoresIniciales();
        });        
    </script>
</asp:Content>

 <asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div class="dvForm dvFormNO">
        <div style="margin: 0px; width: 100%;">
            <center>
            <input type="button" disabled="disabled" value="<%= Tradutor.Traduzir("071_Busqueda_Boton_VisualizarArchivo")%>" id="btnVisualizarArchivo" class="boton" style="width:150px; color:#232323;" onclick="javascript: accionVisualizarArchivo();" />
            <input type="button" disabled="disabled" value="<%= Tradutor.Traduzir("071_Busqueda_Boton_VisualizarReporte")%>" id="btnVisualizarReporte" class="boton" style="width:150px; color:#232323;" onclick="javascript: accionVisualizarReporte();" />
            <input type="button" value="<%= Tradutor.Traduzir("071_Busqueda_Boton_NuevoAbono")%>" id="btnNuevoAbono" class="boton" style="width:150px; color:#232323;" onclick="javascript: accionNuevoAbono();" />
        </center>
        </div>
        <div class="dvclear"></div>
    </div>
</asp:Content>