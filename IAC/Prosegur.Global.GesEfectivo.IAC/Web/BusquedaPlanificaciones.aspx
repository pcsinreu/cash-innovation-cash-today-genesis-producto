<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaPlanificaciones.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaPlanificaciones"
    EnableEventValidation="false" MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<%@ Register Src="~/Controles/ucReloj.ascx" TagName="ucReloj" TagPrefix="uc2" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda de Planificaciones</title>
    <style type="text/css">
        .txtDiaSemana {
        }

        .auto-style4 {
            width: 237px;
        }

        .auto-style8 {
            width: 40px;
        }

        .ui-gn-mandatory {
            margin-right: 0px;
        }

        .auto-style9 {
            width: 53px;
        }

        .auto-style10 {
            width: 175px;
        }

        .auto-style11 {
            width: 243px;
        }

        .auto-style12 {
            width: 52px;
        }
    </style>
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script src="JS/Planificaciones.js" type="text/javascript"></script>
    <script src="JS/jquery.mask.min.js"></script>
    <link href="Css/popover.css" rel="stylesheet" />
    <script src="JS/Scripts/bootstrap.min.js"></script>

    <script type="text/javascript">

        function AbrirModal(AcaoCodigo, OidMaquina) {

            $('#<%=ifMae.ClientID %>').prop('src', 'BusquedaMAE.aspx?modo=modal&AcaoCodigo=' + AcaoCodigo.toString() + '&oidMaquina=' + OidMaquina);
            $('#fundo_modal').fadeIn()
        }
        function FecharModalMae() {
            $('#fundo_modal').hide();

            __doPostBack('<%=btnMae.UniqueID %>');
        }






        var modal_estilos = 'display: block;'
            + 'width: 85%; max-width: 600px;'
            + 'background: #fff; padding: 15px;'
            + 'border-radius: 5px;'
            + '-webkit-box-shadow: 0px 6px 14px -2px rgba(0,0,0,0.75);'
            + '-moz-box-shadow: 0px 6px 14px -2px rgba(0,0,0,0.75);'
            + 'box-shadow: 0px 6px 14px -2px rgba(0,0,0,0.75);'
            + 'position: fixed;'
            + 'top: 50%; left: 50%;'
            + 'transform: translate(-50%,-50%);'
            + 'z-index: 99999999; text-align: center';

        var fundo_modal_estilos = 'top: 0; right: 0;'
            + 'bottom: 0; left: 0; position: fixed;'
            + 'background-color: rgba(0, 0, 0, 0.6); z-index: 99999999;'
            + 'display: none;';

        var meu_modal = '<div id="fundo_modal" style="' + fundo_modal_estilos + '">'
            + '<div id="meu_modal" style="' + modal_estilos + '">'
            + '<h5>Esqueceu sua senha?</h5><br />'
            + '<form>'
            + '<div class="row">'
            + '<div class="col-sm-6">'
            + '<div class="form-group">'
            + '<input name="cpf_cnpj" class="form-control" type="tel" placeholder="CPF/CNPJ" />'
            + '</div>'
            + '<div class="form-group">'
            + '<input name="email" style="max-width: 55%; float: left;" class="form-control" type="email" placeholder="Email" />'
            + '<button style="float: left; margin-left: 15px;" type="submit" class="btn btn-secondary">Enviar</button>'
            + '</div>'
            + '</div>'
            + '<div class="col-sm-6" style="text-align: left;">'
            + 'Qualquer coisa aqui nesta coluna'
            + '</div>'
            + '</div>'
            + '</form>'
            + '<button type="button" class="close" style="top: 5px; right: 10px; position: absolute; cursor: pointer;"><span>&times;</span></button>'
            + '</div></div>';

        $("body").append(meu_modal);

        $("#fundo_modal, .close").click(
            function () {
                $("#fundo_modal").hide();
            }
        );
        $("#meu_modal").click(
            function (e) {
                e.stopPropagation();
            });







        var _fechaGMTDelegacion = null;
        var timeOut = null;

        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-expand");
            $(".accordion").slideToggle("fast");
        };
        function ManterFiltroAberto() {
            $("#DivFiltros").addClass("legend-expand");
            $(".accordion").show();
        };


        function moveRelogio() {

            if (_fechaGMTDelegacion != null) {
                _fechaGMTDelegacion.setSeconds(_fechaGMTDelegacion.getSeconds() + 1);
                var lblRelogio = document.getElementById('lblReloj');
                if (lblRelogio)
                    lblRelogio.innerHTML = _fechaGMTDelegacion.toLocaleString();

                timeOut = setTimeout("moveRelogio()", 1000);
            }
        }

        function ActualizarData(data, info) {
            if (timeOut) {
                clearTimeout(timeOut);
            }
            _fechaGMTDelegacion = new Date(data);
            createPopover('#divTooltip', info);
            moveRelogio();
        }

        function createPopover(item, content) {

            var $pop = $(item);
            $pop.popover({
                placement: 'bottom',
                trigger: 'hover',
                html: true,
                content: content
            });
            return $pop;
        };

    </script>
    <asp:Literal ID="litReloj" runat="server"></asp:Literal>
    <asp:Literal ID="litDicionario" runat="server"></asp:Literal>
    <asp:Literal ID="litHorarios" runat="server"></asp:Literal>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:HiddenField runat="server" ID="hiddenCodigo" />


        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdfAcao" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdfCambioHorario" runat="server" ClientIDMode="Static" />
                <div id="Filtros" style="display: block;">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">Criterios de busqueda</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block;">
                            <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                    <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <table class="tabela_campos">
                                <tr>

                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblNombrePlanificacion" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNombrePlanificacion" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="293px"
                                            MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTipoPlanificacion" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoPlanificacion" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="303px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblEstado" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="303px">
                                            <asp:ListItem Value="" Selected="True">Todos</asp:ListItem>
                                            <asp:ListItem Value="1">Vigente</asp:ListItem>
                                            <asp:ListItem Value="0">No Vigente</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px" ClientIDMode="Static" />
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
                    <asp:Label ID="lblSubTituloPlanificaciones" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="OidPlanificacion" EstiloDestaque="GridLinhaDestaque"
                                        GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        ExibirCabecalhoQuandoVazio="False" OrdenacaoAutomatica="True" PageSize="6"
                                        PaginaAtual="0" PaginacaoAutomatica="True" Width="99%" AgruparRadioButtonsPeloName="False"
                                        ConfigurarNumeroRegistrosManualmente="false" EnableModelValidation="True"
                                        HeaderSpanStyle="">
                                        <Pager ID="objPager_GdvPlanificacion">
                                            <FirstPageButton Visible="True">
                                                <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                </Image>
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Página {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                        <TextBox ID="objTextoGdvPlanificacion" AutoPostBack="True" MaxLength="10" Width="30px">            
                                        </TextBox>
                                        <Columns>

                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="100"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DesBanco" HeaderText="Banco" SortExpression="DesBanco" ControlStyle-Width="400" />
                                            <asp:BoundField DataField="DesPlanificacion" HeaderText="Nombre" SortExpression="desPlanificacion" />
                                            <asp:BoundField DataField="DesTipoPlanificacion" HeaderText="Tipo Planificacion" SortExpression="DesTipoPlanificacion" />
                                            <asp:TemplateField HeaderText="Vigente">
                                                <ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgVigente" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FyhLunes" HeaderText="L" SortExpression="FyhLunes" />
                                            <asp:BoundField DataField="FyhMartes" HeaderText="M" SortExpression="FyhMartes" />
                                            <asp:BoundField DataField="FyhMiercoles" HeaderText="X" SortExpression="FyhMiercoles" />
                                            <asp:BoundField DataField="FyhJueves" HeaderText="J" SortExpression="FyhJueves" />
                                            <asp:BoundField DataField="FyhViernes" HeaderText="V" SortExpression="FyhViernes" />
                                            <asp:BoundField DataField="FyhSabado" HeaderText="S" SortExpression="FyhSabado" />
                                            <asp:BoundField DataField="FyhDomingo" HeaderText="D" SortExpression="FyhDomingo" />
                                            <asp:BoundField HeaderText="oidPlanificacion" Visible="False" />

                                        </Columns>
                                    </pro:ProsegurGridView>
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
                    <asp:Panel runat="server" ID="pnForm" Visible="False">

                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="updUcClienteForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td colspan="2" class="tamanho_celula">
                                            <asp:Panel runat="server" ID="pnUcClienteform" Enabled="True" Width="228px" HorizontalAlign="Left">
                                                <asp:PlaceHolder runat="server" ID="phClientesForm"></asp:PlaceHolder>

                                            </asp:Panel>
                                        </td>

                                        <td class="tamanho_celula"></td>
                                        <td class="auto-style12" style="vertical-align: top">
                                            <asp:ImageButton ID="btnCodigoAjenoBanco" runat="server" CssClass="imgButton" ImageUrl="~/Imagenes/contain_disabled.png" OnClick="imgCodigoAjeno_OnClick" />
                                            <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/iconos/institution.png" ID="imgBancoCapital" CssClass="imgButton" OnCommand="imgCapitalDBancariosForm_OnClick" />
                                        </td>
                                        <td class="tamanho_celula" style="vertical-align: top">
                                            <asp:Label ID="lblCodigoForm" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="auto-style4" style="vertical-align: top">
                                            <asp:TextBox ID="txtCodigoForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="215px"
                                                MaxLength="15"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCodigoForm" runat="server"
                                                ControlToValidate="txtCodigoForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                        </td>
                                        <td class="auto-style10" style="vertical-align: top;">
                                            <asp:Label ID="lblNombreForm" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="auto-style11" style="vertical-align: top">
                                            <asp:TextBox ID="txtNombreForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="215px"
                                                MaxLength="100"></asp:TextBox>
                                            <asp:CustomValidator ID="csvNombreForm" runat="server"
                                                ControlToValidate="txtNombreForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="tamanho_celula" style="padding-left: 10px">
                                            <asp:Label ID="lblTipoForm" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                        </td>
                                        <td class="tamanho_celula" style="padding-left: 6px">
                                            <asp:DropDownList ID="ddlTipoPlanificacionForm" AutoPostBack="true" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                Width="229px">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvTipoForm" runat="server"
                                                ControlToValidate="ddlTipoPlanificacionForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                        </td>
                                        <td class="tamanho_celula"></td>

                                        <td class="auto-style12"></td>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblPatronConfirmacion" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                        </td>
                                        <td class="auto-style4">
                                            <asp:DropDownList ID="ddlPatronConfirmacion" AutoPostBack="true" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                Width="150px">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvPatronConfirmacion" runat="server"
                                                ControlToValidate="ddlPatronConfirmacion" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                        </td>
                                        <td class="auto-style10">
                                            <asp:Button runat="server" ID="btnMensajes" CssClass="ui-button" Width="130px" OnClick="btnMensajes_Click" />
                                        </td>
                                        <td class="tamanho_celula" style="padding-left: 10px;">
                                            <asp:Label ID="lblProcesos" Visible="false" runat="server" CssClass="label2"></asp:Label>
                                            <asp:CheckBoxList ID="chkbxlstProcesos"
                                                AutoPostBack="True"
                                                Visible="False"
                                                CellPadding="5"
                                                CellSpacing="5"
                                                RepeatColumns="1"
                                                RepeatDirection="Vertical"
                                                RepeatLayout="Flow"
                                                TextAlign="Left"
                                                runat="server">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>


                                        <td class="tamanho_celula" style="padding-left: 10px">
                                            <asp:Label ID="lblFechaInicio" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanho_celula" style="padding-left: 6px">
                                            <div style="position: relative">
                                                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>

                                                <asp:CustomValidator ID="csvFechaInicio" runat="server"
                                                    ControlToValidate="txtFechaInicio" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                            </div>
                                        </td>

                                        <td class="tamanho_celula"></td>
                                        <td class="auto-style12"></td>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblFechaFin" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="auto-style4">
                                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>

                                        </td>
                                        <td class="auto-style10"></td>
                                        <td class="auto-style11"></td>

                                    </tr>

                                    <tr>
                                        <td class="tamanho_celula" style="padding-left: 10px">
                                            <asp:Label ID="lblDelegacionForm" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanho_celula" colspan="2" style="padding-left: 6px">
                                            <asp:DropDownList ID="ddlDelegacionForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                Style="float: left" AutoPostBack="true" />

                                            <div class="div_relogio">
                                                <div class="div_relog_label">
                                                    <span id="lblReloj"></span>
                                                </div>
                                            </div>
                                            <div style="padding-top: 4px;">
                                                <a id="divTooltip" data-toggle="popover">
                                                    <img src="Imagenes/help_parametros.png" />
                                                </a>
                                            </div>
                                        </td>
                                        <td class="tamanho_celula"></td>

                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblMinutosAcreditacionForm" runat="server" CssClass="label2" Text="Cantidad de minutos antes de acreditación"></asp:Label>
                                        </td>
                                        <td class="auto-style4">
                                            <asp:TextBox ID="txtMinutosAcreditacionForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                MaxLength="6" ClientIDMode="Static"></asp:TextBox>
                                            <asp:CustomValidator ID="csvMinutosAcreditacionForm" runat="server"
                                                ControlToValidate="txtMinutosAcreditacionForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>

                                        </td>
                                        <td class="auto-style10" style="text-align: left;">
                                            <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                            <asp:CheckBox ID="chkVigenteForm" runat="server" ClientIDMode="Static" />
                                        </td>
                                        <td class="auto-style11"></td>
                                    </tr>
                                </table>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblFiltroAsociacionMovimientos" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="upCanales" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional" HorizontalAlign="Left">
                                        <ContentTemplate>

                                            <asp:Panel runat="server" ID="pnUcCanales" Enabled="True">
                                                <asp:PlaceHolder runat="server" ID="phCanal"></asp:PlaceHolder>
                                            </asp:Panel>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <div id="divDivisasPlanificacion" runat="server">
                            <table>
                                <tr>
                                    <td width="540">
                                        <asp:UpdatePanel ID="upDivisaPlanificacion" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:PlaceHolder ID="phDivisaPlanificacion" runat="server"></asp:PlaceHolder>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divDivisionPeriodos" runat="server">
                            <div class="ui-panel-titlebar">
                                <asp:Label ID="lblDivisionPeriodos" CssClass="ui-panel-title" runat="server"></asp:Label>
                            </div>
                            <asp:UpdatePanel ID="upDivision" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td width="540">
                                                <asp:UpdatePanel ID="upMovimientoPlanificacion" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:PlaceHolder ID="phMovimientoPlanificacion" runat="server"></asp:PlaceHolder>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                            </td>
                                            <td style="vertical-align: top">
                                                <div style="margin-left: 10px">
                                                    <asp:Label ID="lblDivisionSubcanal" runat="server" CssClass="label2" />
                                                    <asp:CheckBox ID="chkDivisionSubcanal" runat="server" ClientIDMode="Static" AutoPostBack="True"/>
                                                </div>
                                            </td>

                                            <td style="vertical-align: top">
                                                <div style="margin-left: 10px">
                                                    <asp:Label Style="margin-left: 5px" ID="lblDivisionDivisa" runat="server" CssClass="label2" />
                                                    <asp:CheckBox ID="chkDivisionDivisa" runat="server" ClientIDMode="Static" AutoPostBack="True"/>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblAgrupValores" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="upAgrupaciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <table style="padding: 10px">
                                    <tr>

                                        <td>
                                            <div>
                                                <asp:Label ID="lblPorSubCanal" runat="server" CssClass="label2" />
                                                <asp:CheckBox ID="chkPorSubCanal" runat="server" ClientIDMode="Static" />
                                            </div>
                                        </td>

                                        <td>
                                            <div style="margin-left: 10px">
                                                <asp:Label ID="lblPorPuntoServicio" runat="server" CssClass="label2" />
                                                <asp:CheckBox ID="chkPorPuntoServicio" runat="server" ClientIDMode="Static" />
                                            </div>
                                        </td>

                                        <td>
                                            <div style="margin-left: 10px">
                                                <asp:Label Style="margin-left: 5px" ID="lblPorFechaContable" runat="server" CssClass="label2" />
                                                <asp:CheckBox ID="chkPorFechaContable" runat="server" ClientIDMode="Static" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lbl_horarios" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_interna" style="margin-top: 10px; text-align: center;">
                            <tr>
                                <td align="center">
                                    <div style="text-align: center;">
                                        <asp:UpdatePanel ID="UpdateHorarios" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                            <ContentTemplate>
                                                <div style="text-align: left; width: inherit;">
                                                    <div style="display: inline-block;">
                                                        <div style="float: left;">
                                                            <pro:ProsegurGridView ID="GdvHorarios" runat="server" AllowPaging="False" Visible="true" ClientIDMode="Static"
                                                                AllowSorting="False" ColunasSelecao="OidProgramacion"
                                                                GridPadrao="False" AutoGenerateColumns="false" Ajax="True" GerenciarControleManualmente="True"
                                                                NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                                                Height="100%" ExibirCabecalhoQuandoVazio="true"
                                                                AgruparRadioButtonsPeloName="False"
                                                                ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True"
                                                                HeaderSpanStyle="">
                                                                <HeaderStyle Font-Bold="True" />
                                                                <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                                                <RowStyle CssClass="GridLinhaPadraoImpar" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="L" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtLunes" runat="server" Text='<%#Bind("FyhLunes")%>' Enabled='<%#Eval("LunesHabilitado")%>' CssClass="txtDiaSemana" ClientIDMode="Static"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtMartes" runat="server" Text='<%#Bind("FyhMartes")%>' Enabled='<%#Eval("MartesHabilitado")%>' CssClass="txtDiaSemana" ClientIDMode="Static"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="X" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">

                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtMiercoles" runat="server" Text='<%#Eval("FyhMiercoles")%>' Enabled='<%#Eval("MiercolesHabilitado")%>' CssClass="txtDiaSemana" ClientIDMode="Static"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="J" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtJueves" runat="server" Text='<%#Eval("FyhJueves")%>' Enabled='<%#Eval("JuevesHabilitado")%>' CssClass="txtDiaSemana" ClientIDMode="Static"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="V" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtViernes" runat="server" Text='<%#Eval("FyhViernes")%>' Enabled='<%#Eval("ViernesHabilitado")%>' CssClass="txtDiaSemana" ClientIDMode="Static"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtSabado" runat="server" Text='<%#Eval("FyhSabado")%>' Enabled='<%#Eval("SabadoHabilitado")%>' CssClass="txtDiaSemana" ClientIDMode="Static"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="D" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDomingo" runat="server" Text='<%#Eval("FyhDomingo")%>' Enabled='<%#Eval("DomingoHabilitado")%>' CssClass="txtDiaSemana" ClientIDMode="Static"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </pro:ProsegurGridView>
                                                        </div>
                                                        <div style="float: left;">
                                                            <div>
                                                                <asp:ImageButton runat="server" ImageUrl="~/Imagenes/plus-square.png" ID="btnAdcLinha" CssClass="imgButton" />
                                                            </div>
                                                            <div>
                                                                <asp:ImageButton runat="server" ImageUrl="~/Imagenes/minus_square.png" ID="btnRemLinha" CssClass="imgButton" />
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </td>


                            </tr>
                        </table>
                        <br />
                        <%-- Inicio User control de limites --%>
                        <div class="ui-panel-titlebar" style="margin-top: 20px;">
                            <asp:Label ID="lblTituloLimite" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="upLimitePlanificacion" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="phLimitePlanificacion" runat="server"></asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%-- Fin User control de limites --%>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblFaturacion" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div style="padding: 10px;">
                                    <asp:Label ID="lblControlaFacturacionForm" runat="server" CssClass="label2"></asp:Label>
                                    <asp:CheckBox ID="chkControlaFacturacionForm" runat="server" ClientIDMode="Static" AutoPostBack="True" />
                                </div>
                                <table>

                                    <tr>
                                        <asp:Panel runat="server" ID="pnlFacturacion">
                                            <td style="width: 450px;">
                                                <asp:Panel runat="server" ID="pnUcBancoComisionform" Enabled="True" Width="200px">
                                                    <asp:PlaceHolder runat="server" ID="phBancoComisionform"></asp:PlaceHolder>
                                                </asp:Panel>
                                            </td>
                                            <td style="vertical-align: top">
                                                <asp:ImageButton ID="btnCodigoAjenoBancoBancoComision" runat="server" CssClass="imgButton" ImageUrl="~/Imagenes/contain_disabled.png" OnClick="imgCodigoAjenoBancoComision_OnClick" />
                                            </td>
                                            <td style="vertical-align: top">
                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/iconos/institution.png" ID="imgBancoComisionDatosBancariosForm" CssClass="imgButton" OnCommand="imgDBancariosForm_OnClick" />
                                            </td>

                                            <td class="tamanho_celula" style="vertical-align: top; text-align: right; padding-right: 5px">
                                                <asp:Label ID="lblComisionPlanificacionForm" runat="server" CssClass="label2"></asp:Label>
                                            </td>

                                            <td style="vertical-align: top">
                                                <asp:TextBox ID="txtComisionClienteForm" runat="server" Width="120px" onkeypress="return bloqueialetrasAceitaVirgulaPunto(event,this);" MaxLength="25" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" />

                                            </td>

                                            <td class="tamanho_celula" style="vertical-align: top; text-align: right; padding-right: 5px; padding-left: 30px;">
                                                <asp:Label ID="lblDiaCierreForm" runat="server" CssClass="label2"></asp:Label>
                                            </td>

                                            <td style="vertical-align: top">
                                                <asp:TextBox ID="txtDiaCierreForm" onkeypress="return ValorNumerico(event);" runat="server" Width="100px" MaxLength="2" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" />

                                            </td>
                                        </asp:Panel>
                                    </tr>
                                </table>


                                <div class="ui-panel-titlebar">
                                    <asp:Label ID="lblSubTituloMAES" CssClass="ui-panel-title" runat="server"></asp:Label>
                                </div>
                                <table class="tabela_interna" style="margin-top: 10px;">
                                    <tr>

                                        <td align="center">

                                            <div style="text-align: center;">
                                                <dx:ASPxGridView ID="grid" runat="server"
                                                    KeyFieldName="OidMaquina" Width="99%" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="DeviceID" VisibleIndex="0">
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Left">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Descripcion" VisibleIndex="1">
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Left">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="NumPorcentComisionTxt" VisibleIndex="2">
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Left">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="BancoTesoreria" VisibleIndex="3">
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Left">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataColumn VisibleIndex="4" CellStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/iconos/institution.png" ID="imgDatosBancariosForm" CssClass="imgButton"
                                                                    OnClientClick='<%# BuscaPostbackGrid("DATOSBANCARIOS", Container.KeyValue) %>'
                                                                    Visible='<%#Not String.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "BancoTesoreria")) %>' CommandArgument='<%# Container.KeyValue %>' />
                                                            </DataItemTemplate>
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn VisibleIndex="5" CellStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <asp:Image runat="server" ImageUrl="~/Imagenes/contain01.png" ID="imgVigenteForm" CssClass="imgButton" Visible='<%# DataBinder.Eval(Container.DataItem, "BolActivo") %>' />
                                                                <asp:Image runat="server" ImageUrl="~/Imagenes/nocontain01.png" ID="imgNoVigenteForm" CssClass="imgButton" Visible='<%# Not DataBinder.Eval(Container.DataItem, "BolActivo") %>' />
                                                            </DataItemTemplate>
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataColumn>

                                                        <dx:GridViewDataColumn VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/iconos/atajo3.png" ID="imgMAE" CssClass="imgButton"
                                                                    OnClientClick='<%# BuscaPostbackGrid("MAE", Container.KeyValue) %>'
                                                                    Visible='<%# VisibleBtnMae( Container.KeyValue)  %>' CommandArgument='<%# Container.KeyValue %>' />
                                                            </DataItemTemplate>
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataColumn>

                                                        <dx:GridViewDataColumn VisibleIndex="7" CellStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluirForm" CssClass="imgButton"
                                                                    OnClientClick='<%# BuscaPostbackGrid("BORRAR", Container.KeyValue) %>'
                                                                    CommandArgument='<%# Container.KeyValue %>' />
                                                            </DataItemTemplate>
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataColumn>


                                                    </Columns>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
                                                    <SettingsPager PageSize="20">

                                                        <PageSizeItemSettings Visible="true" />
                                                    </SettingsPager>
                                                </dx:ASPxGridView>
                                            </div>



                                        </td>
                                    </tr>
                                </table>
                                <asp:UpdatePanel ID="upDatosBanc" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:PlaceHolder ID="phDatosBanc" runat="server"></asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </asp:Panel>


                    <div id="fundo_modal" style="top: 0; right: 0; bottom: 0; left: 0; position: fixed; background-color: rgba(0, 0, 0, 0.6); z-index: 99999999; display: none;">
                        <div id="meu_modal" style="display: block; width: 95%; height: 600px; max-width: 1300px; background: #fff; padding: 0px; border-radius: 5px; -webkit-box-shadow: 0px 6px 14px -2px rgba(0,0,0,0.75); -moz-box-shadow: 0px 6px 14px -2px rgba(0,0,0,0.75); box-shadow: 0px 6px 14px -2px rgba(0,0,0,0.75); position: fixed; top: 50%; left: 50%; transform: translate(-50%,-50%); z-index: 99999999; text-align: center">

                            <form>
                                <iframe runat="server" id="ifMae" style="width: 100%; height: 600px; max-width: 1300px;"></iframe>


                            </form>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        //Script necessário para evitar que dê erro ao clicar duas vezes em algum controle que esteja dentro do updatepanel.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(initializeRequest);

        function initializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack()) {
                args.set_cancel(true);
            }
        }
    </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <center>
        <table>
            <tr align="center">
                <td>
                    <asp:Button runat="server" ID="btnNovo" CssClass="btn-novo" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCamposExtrasModal" CssClass="btn-novo" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnAddMAE" CssClass="btn-novo" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnGrabar" CssClass="btn-salvar" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnBajaConfirm" CssClass="btn-excluir" />
                    <div class="botaoOcultar">
                        <asp:Button runat="server" ID="btnConsomeCodigoAjeno" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnConsomeCodigoAjenoBancoComision" CssClass="btn-excluir" />


                        <asp:Button runat="server" ID="btnConsomeBaja" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnCamposExtras" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnCamposExtrasCliente" CssClass="btn-excluir" />

                        <asp:Button runat="server" ID="btnConsomeTotalizador" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnGrabarConfirm" CssClass="btn-salvar" />
                        <asp:Button runat="server" ID="btnGrabarOnline" CssClass="btn-salvar" />
                        <asp:Button runat="server" ID="btnCambioCanalSim" CssClass="btn-salvar" />
                        <asp:Button runat="server" ID="btnCambioCanalNao" CssClass="btn-salvar" />
                        <asp:Button runat="server" ID="btnConsomePlanificacion" CssClass="btn-excluir" />
                        <%--<asp:Button runat="server" ID="btnConsumeCamposExtras" CssClass="btn-excluir"/>--%>
                        <asp:Button runat="server" ID="btnConsomeCancelar" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnMae" CssClass="btn-excluir" />
                        <dx:ASPxButton ID="btnGrid" ClientInstanceName="btnGrid" AutoPostBack="true" runat="server" Visible="false" Text="btnGrid" ClientIDMode="Static" UseSubmitBehavior="False" />


                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
