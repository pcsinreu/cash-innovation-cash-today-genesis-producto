<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="ResultadoCertificacion.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ResultadoCertificacion" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/ucContainerDocumentos.ascx" TagPrefix="ns" TagName="ucContainerDocumentos" %>
<%@ Register Src="~/Controles/PopupBusquedaCliente.ascx" TagPrefix="ns" TagName="PopupBusquedaCliente" %>
<%@ Register Src="~/Controles/ucConvertirCertificado.ascx" TagPrefix="ns" TagName="ucConvertirCertificado" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function DownloadReporte(CodigoCertificado, CodigoEstado, CodigoConfigReporte, CodigoSubCanal) {
            jQuery.ajax({
                url: 'ResultadoCertificacion.aspx/DownloadArchivoReporte',
                type: "POST",
                dataType: "json",
                data: "{CodigoCertificado: '" + CodigoCertificado + "', CodigoEstado: '" + CodigoEstado + "', CodigoConfigReporte: '" + CodigoConfigReporte + "', CodigoSubCanal: '" + CodigoSubCanal + "'}",
                contentType: "application/json; charset=utf-8",
                success: function (data, text) {
                    window.open("ResultadoCertificacion.aspx?download=true");
                },
                error: function (request, status, error) {
                    genesisAlertError(request.responseJSON.Message, request.responseText);
                }
            });


        }

    </script>
    <style>
        .no-close .ui-dialog-titlebar-close {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTitulo" runat="server" Text="" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
        </div>
        <div style="width: 100%; margin-left: 0px; margin-top: 5px;">
            <div class="dvUsarFloat">
                <div id="dvCodigo" runat="server" style="display: block; margin-top: 0px;">
                    <asp:Label ID="lblCodigoCertificado" runat="server" SkinID="filter-label"
                        Text="" />
                    <br />
                    <asp:TextBox ID="txtCodigoCertificado" runat="server" SkinID="form-textbox-mandatory"
                        Width="330px" Enabled="false" />
                </div>
                <div id="dvFiltros" runat="server" style="display: block; margin-top: 0px; height: 130px;">
                    <asp:UpdatePanel ID="upFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div style="display: block; margin-left: 0px; margin-top: 5px;">
                                <div style="display: block; margin-top: 0px;">
                                    <asp:UpdatePanel ID="upDelegacion" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:PlaceHolder runat="server" ID="phDelegacion"></asp:PlaceHolder>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="dvclear"></div>
                                <div style="display: block; margin-top: 0px;">
                                    <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="dvclear"></div>
                            <div class="dvUsarFloat">
                                <div id="dvFechaCertificacion" runat="server" style="display: block; margin-top: 0px;">
                                    <asp:Label ID="lblFechaCertificacion" runat="server" SkinID="filter-label"
                                        Text="" />
                                    <br />
                                    <asp:TextBox ID="txtFechaCertificacion" runat="server" SkinID="form-textbox-mandatory"
                                        Width="150px" />
                                </div>
                                <div id="dvEstado" runat="server" style="display: block; margin-left: 30px;">
                                    <asp:Label ID="lblEstado" runat="server" SkinID="filter-label"
                                        Text="" />
                                    <br />
                                    <asp:RadioButtonList ID="rblEstado" runat="server" SkinID="form-textbox-mandatory"
                                        RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>

                                </div>
                                <div class="dvclear"></div>
                                <div id="dvBuscar" runat="server" style="display: block; margin-top: 10px;">
                                    <asp:Button ID="btnBuscar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                                </div>
                                <div class="dvclear"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="dvclear"></div>
        <div class="ui-panel-titlebar" style="margin-top: 10px;">
            <asp:Label ID="lblTitulo1" runat="server" Text="" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
        </div>
        <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
            <asp:UpdatePanel ID="upnSearchResults" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:GridView ID="grvResultado" runat="server" AutoGenerateColumns="False"
                        BorderStyle="None" Width="97%" EnableModelValidation="True" OnRowDataBound="grvResultado_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chbSelecionarTodos" runat="server" onclick="checkAll(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="IdCertificado" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IdentificadorCertificado") %>' Visible="false" />
                                    <asp:CheckBox ID="chbSelecionar" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Left" Font-Size="7pt" Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="FyhCertificado" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FyhCertificado") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Tipo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DescripcionEstado") %>' />
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Left" Font-Size="7pt" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Cliente" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Cliente.Descripcion") %>' />
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Left" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="IdSubCanal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubCanales(0).Identificador") %>' Visible="false" />
                                    <asp:Label ID="SubCanal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubCanales(0).Descripcion")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Left" Font-Size="7pt" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="IdReporte" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ConfigReporte.Identificador")%>' Visible="false" />
                                    <asp:Label ID="Reporte" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ConfigReporte.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Left" Font-Size="7pt" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Situacion" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <div style="width: 200px; float: left">
                                        <asp:Image ID="imgSituacionReporte" runat="server" />
                                        &nbsp;
                                     <asp:Label ID="lblSituacionReporte" Style="vertical-align: middle" runat="server"></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="7pt" Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgDownload" runat="server" Visible="false" ImageUrl="~/Imagenes/move_waiting_down.png" />
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Left" Font-Size="7pt" Width="20px" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" />
                    </asp:GridView>
                    <div id="dvEmptyData" runat="server" style="display: block;">
                        <asp:Label ID="lblSemRegistro" runat="server" Style="color: #767676; font-size: 9pt; font-style: italic;"></asp:Label>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <div style="width: 50%;">
        <div id="dvResultadoRelatorio" style="display: none;">
            <div class="ui-widget-overlay ui-front"></div>
            <div class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-front ui-draggable" style="height: auto; width: 250px; top: 21.5px; left: 290.5px; display: block;" tabindex="-1" role="dialog">
                <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                    <span id="ui-id-53" class="ui-dialog-title" style="color: #5e5e5e">
                        <asp:Label ID="lblTituloResultado" runat="server" Text="aaaaaa" SkinID="filtro-label_titulo" Style="color: #767676 !important; margin-right: 5px !important;" />
                    </span>
                </div>
                <div id="dvMensagemExecucao" style="width: auto; min-height: 250px; max-height: none; height: auto;" class="ui-dialog-content ui-widget-content">
                    <span id="mensagemExecucao"></span>
                </div>
            </div>
        </div>
    </div>
    <div>
        <asp:UpdatePanel ID="uppConvertirCertificado" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dvConvertirCertificadoPopup" runat="server" style="display: none;">
                    <div class="ui-widget-overlay ui-front"></div>
                    <div class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-front ui-draggable" style="height: auto; width: 778px; top: 21.5px; left: 290.5px; display: block;" tabindex="-1" role="dialog">
                        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                            <span id="ui-id-53" class="ui-dialog-title" style="color: #5e5e5e">
                                <asp:Literal ID="lblTituloConvertirCertificado" runat="server"></asp:Literal>
                            </span>
                        </div>
                        <div id="dvConvertirCertificadoPopupConteudo" style="width: auto; min-height: 250px; max-height: none; height: auto;" class="ui-dialog-content ui-widget-content">
                            <asp:PlaceHolder ID="phConvertirCertificado" runat="server">
                                <ns:ucConvertirCertificado runat="server" ID="ucConvertirCertificado" />
                            </asp:PlaceHolder>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
          <table>
                    <tr>
                        <td>
                            <ns:Boton ID="btnVolver" runat="server" Text="" ImageUrl="~/App_Themes/Padrao/css/img/button/back.png" />
                        </td>
                        <td>
                            <ns:Boton ID="btnEjecutarInforme" runat="server" Text="" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png" />
                        </td>
                        <td>
                            <ns:Boton ID="btnConvertirProvisionalSinCierre" runat="server" Text="" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png" Visible="false" />
                        </td>
                        <td>
                            <ns:Boton ID="btnConvertirProvisionalConCierre" runat="server" Text="" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png" Visible="false" />
                        </td>
                        <td>
                            <ns:Boton ID="btnConvertirDefinitivo" runat="server" Text="" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png" Visible="false" />
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upBotonesRodapie" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <ns:Boton ID="btnConvertirRelacionados" runat="server" Text="" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png" Visible="false" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                
            </table>
    </center>
</asp:Content>
