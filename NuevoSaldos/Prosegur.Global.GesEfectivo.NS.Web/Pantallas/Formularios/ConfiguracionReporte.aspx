<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="ConfiguracionReporte.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ConfiguracionReporte" %>

<%@ Import Namespace="Prosegur.Genesis.Comon" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function cklTiposReporte_onchange() {
            var cklTiposReporte = $("#<%= cklTiposReporte.ClientID %> input[type=radio]:checked");
            var txtMascaraNombre = $("#<%= txtMascaraNombre.ClientID%>");
            var tipoCert = "<%= Convert.ToInt32(Enumeradores.TipoReporte.Certificacion) %>"

            if (cklTiposReporte.val() == tipoCert) {
                txtMascaraNombre.val("");
                $("#<%= dvNombreArchivo.ClientID%>").hide();
            } else {
                $("#<%= dvNombreArchivo.ClientID %>").show();
            }
        }
        function ddlRenderizador_onchange() {
            var ddlRenderizadorVal = $("#<%= ddlRenderizador.ClientID%>").val();
            var tipoRenderCSV = "<%= Enumeradores.TipoRenderizador.CSV.ToString() %>"
            var txtExtensaoArquivo = $("#<%= txtExtensaoArquivo.ClientID %>");
            var dvSeparadorArquivo = $("#<%= dvSeparadorArquivo.ClientID%>");
            var txtSeparadorArquivo = $("#<%= txtSeparadorArquivo.ClientID%>");

            if (ddlRenderizadorVal == tipoRenderCSV) {

                txtExtensaoArquivo.val("");
                txtExtensaoArquivo.removeAttr("disabled");
                dvSeparadorArquivo.show();

            } else {

                var extXLS = "<%= Extenciones.EnumExtension.RecuperarValor(Enumeradores.TipoRenderizador.EXCEL) %>"
                var extPDF = "<%= Extenciones.EnumExtension.RecuperarValor(Enumeradores.TipoRenderizador.PDF) %>"
                var extDOC = "<%= Extenciones.EnumExtension.RecuperarValor(Enumeradores.TipoRenderizador.WORD )%>"
                var extXML = "<%= Extenciones.EnumExtension.RecuperarValor(Enumeradores.TipoRenderizador.XML)  %>"
                switch (ddlRenderizadorVal) {
                    case "<%= Enumeradores.TipoRenderizador.EXCEL.ToString() %>":
                        txtExtensaoArquivo.val(extXLS);
                        break;
                    case "<%= Enumeradores.TipoRenderizador.PDF.ToString()%>":
                        txtExtensaoArquivo.val(extPDF);
                        break;
                    case "<%= Enumeradores.TipoRenderizador.WORD.ToString()%>":
                        txtExtensaoArquivo.val(extDOC);
                        break;
                    case "<%= Enumeradores.TipoRenderizador.XML.ToString() %>":
                        txtExtensaoArquivo.val(extXML);
                        break;
                }

                txtExtensaoArquivo.attr("disabled", "disabled");
                dvSeparadorArquivo.hide();
                txtSeparadorArquivo.val("");

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidIdendificador" runat="server" />
    <div class="content">
        <div runat="server" id="dvFiltro">
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblTituloDadosGenerales" runat="server" Text="Filtros" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
            </div>
            <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                <div class="dvUsarFloat">
                    <div id="dvCodigo" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblCodigo" runat="server" Text="Codigo" /><br />
                        <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" Width="100px" />
                    </div>
                    <div id="dvDescricion" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblDescricao" runat="server" Text="Descricion" /><br />
                        <asp:TextBox ID="txtDescricao" runat="server" Width="215px" />
                    </div>

                    <div id="dvCheckBoxListTiposClientes" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblTiposClientes" runat="server" Text="Tipo Cliente"></asp:Label><br />
                        <div style="width: 250px; height: 50px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                            <asp:CheckBoxList ID="cklTiposClientes" runat="server" RepeatColumns="1">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="upTiposReporte" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="dvCheckBoxListTiposReporte" runat="server" style="margin-top: 0px;">
                                <asp:Label ID="lblTiposReporte" runat="server" Text="Tipo Reporte"></asp:Label><br />
                                <div style="width: 250px; height: 50px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                                    <asp:RadioButtonList ID="cklTiposReporte" runat="server" RepeatColumns="1" onchange="javascript:cklTiposReporte_onchange();" AutoPostBack="true">
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div id="dvCheckBoxListParametros" runat="server" style="margin-top: 0px; margin-left: 25px;">
                                <asp:Label ID="lblParametros" runat="server" Text="Parâmetros"></asp:Label><br />
                                <div style="width: 250px; height: 50px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                                    <asp:CheckBoxList ID="cklParametros" runat="server" RepeatColumns="1">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="dvclear"></div>
                    <div id="dvDireccion" runat="server" style="margin-top: 0px;">
                        <asp:Label ID="lblDireccion" runat="server" Text="Direccion del reporte en el servidor Reporting Service" /><br />
                        <asp:TextBox ID="txtDireccion" runat="server" Width="345px" />
                    </div>
                    <div class="dvclear"></div>
                    <div id="dvNombreArchivo" runat="server" style="margin-top: 0px;">
                        <div id="dvMascaraNombre" runat="server" style="margin-top: 0px;">
                            <asp:Label ID="lblMascaraNombre" runat="server" Text="Máscara para Generación del Nombre del Reporte" /><br />
                            <asp:TextBox ID="txtMascaraNombre" runat="server" Width="345px" MaxLength="500" />
                        </div>
                        <div id="dvRenderizador" runat="server" style="margin-top: 0px; margin-left: 25px;">
                            <asp:Label ID="lblRenderizador" runat="server" Text="Renderizador" /><br />
                            <asp:DropDownList ID="ddlRenderizador" runat="server" Width="80px" onchange="javascript:ddlRenderizador_onchange();"></asp:DropDownList>
                        </div>
                        <div id="dvExtensaoArquivo" runat="server" style="margin-top: 0px; margin-left: 10px;">
                            <asp:Label ID="lblExtensaoArquivo" runat="server" Text="Extensão" /><br />
                            <asp:TextBox ID="txtExtensaoArquivo" runat="server" Width="80px" MaxLength="20" Enabled="false" />
                        </div>
                        <div id="dvSeparadorArquivo" runat="server" style="margin-top: 0px; margin-left: 10px; display: none;">
                            <asp:Label ID="lblSeparadorArquivo" runat="server" Text="Separador" /><br />
                            <asp:TextBox ID="txtSeparadorArquivo" runat="server" Width="80px" MaxLength="20" />
                        </div>
                    </div>
                    <div class="dvclear"></div>
                </div>
            </div>
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblClientes" runat="server" Text="Clientes (Totalizadores de Saldos)" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
            </div>
            <div class="dvclear"></div>
            <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                <div class="dvUsarFloat">
                    <div id="dvCheckBoxListClientes" runat="server" style="margin-top: 5px;">
                        <div style="width: 400px; height: 350px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                            <asp:CheckBoxList ID="cklClientesTotalizadorSaldos" runat="server" RepeatColumns="1">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div id="dvCheckBoxListClientes2" runat="server" style="margin-top: 5px;">
                        <div style="width: 400px; height: 350px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                            <asp:CheckBoxList ID="cklClientesTotalizadorSaldos2" runat="server" RepeatColumns="1">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div id="dvCheckBoxListClientes3" runat="server">
                        <div style="width: 400px; height: 350px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                            <asp:CheckBoxList ID="cklClientesTotalizadorSaldos3" runat="server" RepeatColumns="1">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="dvclear"></div>
            </div>
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
