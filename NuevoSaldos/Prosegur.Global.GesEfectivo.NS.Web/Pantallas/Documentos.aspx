<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.master"
    CodeBehind="Documentos.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Documentos" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function controleBuscarPor(a) {

            document.getElementById('<%= Me.dvInfNumeroComprovante.ClientID%>').style.display = 'none';
            document.getElementById('<%= Me.dvInfCodigoExterno.ClientID%>').style.display = 'none';
            document.getElementById('<%= Me.dvInfValores.ClientID%>').style.display = 'none';
            document.getElementById('<%= Me.txtInfNumeroComprovante.ClientID%>').value = '';
            document.getElementById('<%= Me.txtInfCodigoExterno.ClientID%>').value = '';
            document.getElementById('<%= Me.lstItensAdicionados.ClientID%>').innerHTML = '';
            document.getElementById('<%= Me.hidItensAdicionados.ClientID%>').value = '';

            if (a.selectedIndex == 1) {
                document.getElementById('<%= Me.dvInfNumeroComprovante.ClientID%>').style.display = 'block';
                document.getElementById('<%= Me.dvInfValores.ClientID%>').style.display = 'block';
                document.getElementById('<%= Me.txtInfNumeroComprovante.ClientID%>').focus();
            }
            if (a.selectedIndex == 2) {
                document.getElementById('<%= Me.dvInfCodigoExterno.ClientID%>').style.display = 'block';
                document.getElementById('<%= Me.dvInfValores.ClientID%>').style.display = 'block';
                document.getElementById('<%= Me.txtInfCodigoExterno.ClientID%>').focus();
            }


        }

        function adicionarComprovante() {
            var valor = document.getElementById('<%= Me.txtInfNumeroComprovante.ClientID%>').value.replace(';', '');
            if (validarValor(valor)) {
                adicionarValor(valor, 'eliminarComprovante');
            }
            document.getElementById('<%= Me.txtInfNumeroComprovante.ClientID%>').value = '';
            document.getElementById('<%= Me.txtInfNumeroComprovante.ClientID%>').focus();
        }

        function adicionarCodigoExterno() {
            var valor = document.getElementById('<%= Me.txtInfCodigoExterno.ClientID%>').value.replace(';', '');
            if (validarValor(valor)) {
                adicionarValor(valor, 'eliminarCodigoExterno');
            }
            document.getElementById('<%= Me.txtInfCodigoExterno.ClientID%>').value = '';
            document.getElementById('<%= Me.txtInfCodigoExterno.ClientID%>').focus();
        }

        function eliminarComprovante(a) {
            eliminarValor(a);
            document.getElementById('<%= Me.hidItensAdicionados.ClientID%>').value = document.getElementById('<%= Me.hidItensAdicionados.ClientID%>').value.replace(a + ';', '');
            document.getElementById('<%= Me.txtInfNumeroComprovante.ClientID%>').value = '';
            document.getElementById('<%= Me.txtInfNumeroComprovante.ClientID%>').focus();
        }

        function eliminarCodigoExterno(a) {
            eliminarValor(a);
            document.getElementById('<%= Me.hidItensAdicionados.ClientID%>').value = document.getElementById('<%= Me.hidItensAdicionados.ClientID%>').value.replace(a + ';', '');
            document.getElementById('<%= Me.txtInfCodigoExterno.ClientID%>').value = '';
            document.getElementById('<%= Me.txtInfCodigoExterno.ClientID%>').focus();
        }

        function adicionarValor(a, b) {
            document.getElementById('<%= Me.lstItensAdicionados.ClientID%>').innerHTML += '<div id="item_' + a + '">' + a + '<button type="button" value="X" onclick="javascript:' + b + '(' + "'" + a + "'" + ');" /></div>';
            document.getElementById('<%= Me.hidItensAdicionados.ClientID%>').value += a + ';';
        }

        function eliminarValor(a) {
            var d = document.getElementById('<%= Me.lstItensAdicionados.ClientID%>');
            var olddiv = document.getElementById('item_' + a);
            d.removeChild(olddiv);
        }

        function validarValor(a) {
            if (document.getElementById('<%= Me.hidItensAdicionados.ClientID%>').value.indexOf(a + ';') > -1) {
                return false
            }
            return true
        }
    </script>
    <style type="text/css">
        .BuscarPorvalores {
            width: 700px !important;
            position: relative;
            overflow: auto;
            height: 45px !important;
        }

            .BuscarPorvalores div {
                float: left;
                border: 1px solid #2996e2;
                color: #2996e2;
                border-radius: 5px;
                padding: 2px;
                padding-left: 4px;
                padding-right: 4px;
                margin: 1px !important;
                border: 1px solid #898989;
                color: #898989;
                background-color: #f7f7f7;
            }

                .BuscarPorvalores div button {
                    background-image: url(images/ui-icons_888888_256x240.png);
                    background-position: -83px -132px;
                    background-color: transparent;
                    padding: 0px;
                    margin-left: 4px;
                    width: 10px;
                    height: 10px;
                    border-style: none;
                    border-radius: 0px;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTitulo" runat="server" Text="Filtros" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
        </div>
        <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
            <div class="dvUsarFloat">
                 <div style="margin-right:0px;">
                    <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="Div1" runat="server" style="display: block; ">
                    <b>
                        <asp:Label ID="lblTipoSitioDocumentos" runat="server" Text="Tipo_Sitio_Documentos"></asp:Label></b><br />
                    <asp:DropDownList ID="ddlTipoSitioDocumentos" runat="server"></asp:DropDownList>
                </div>
                <div id="Div2" runat="server" style="display: block; ">
                    <b>
                        <asp:Label ID="lblCodigoEstadoDocumento" runat="server" Text="CodigoEstadoDocumento"></asp:Label></b><br />
                    <asp:DropDownList ID="ddlCodigoEstadoDocumento" runat="server"></asp:DropDownList>
                </div>
                
                <div class="dvclear"></div>

                <div id="dvFechaCreacion" runat="server" style="display: block; margin-top: 0px;">
                    <b>
                        <asp:Label ID="lblFechaCreacion" runat="server" Text=""></asp:Label></b><br />
                    <asp:Label ID="lblFechaCreacionDesde" Text="" runat="server" />
                    <asp:TextBox ID="txtFechaCreacionDesde" runat="server" />
                    <asp:Label ID="lblFechaCreacionHasta" Text="" runat="server" />
                    <asp:TextBox ID="txtFechaCreacionHasta" runat="server" />
                </div>
                <div id="dvNumeroComprovante" runat="server" style="display: block; margin-top: 0px;">
                    <b>
                        <asp:Label ID="lblNumeroComprovante" runat="server" Text=""></asp:Label></b><br />
                    <asp:Label ID="lblNumeroComprovanteDesde" Text="" runat="server" />
                    <asp:TextBox ID="txtNumeroComprovanteDesde" runat="server" />
                    <asp:Label ID="lblNumeroComprovanteHasta" Text="" runat="server" />
                    <asp:TextBox ID="txtNumeroComprovanteHasta" runat="server" />
                </div>
                <div id="dvDiponibilidad" runat="server" style="display: block; margin-top: 0px;">
                    <b>
                        <asp:Label ID="lblDiponibilidad" runat="server" Text=""></asp:Label></b><br />
                    <asp:DropDownList ID="ddlDisponibilidad" runat="server"></asp:DropDownList>
                </div>
                <div id="dvBuscarPor" runat="server" style="display: block; margin-top: 0px;">
                    <b>
                        <asp:Label ID="lblBuscarPor" runat="server" Text=""></asp:Label></b><br />
                    <asp:DropDownList ID="ddlBuscarPor" runat="server" onchange="javascript:controleBuscarPor(this);"></asp:DropDownList>
                </div>
                <div style="display: block; margin-top: 0px; line-height: 35px;">
                </div>

                <div class="dvclear"></div>
                <div id="dvInfNumeroComprovante" runat="server" style="display: none; margin-top: 0px;">
                    <asp:Label ID="lblInfNumeroComprovante" runat="server" Text="Número Comprovante"></asp:Label><br />
                    <asp:TextBox ID="txtInfNumeroComprovante" runat="server" />
                    <button type="button" onclick="javascript:adicionarComprovante();" class="ui-datepicker-trigger" style="width: auto; height: auto; padding: 0px; margin: 0px;">
                        <img src="../Imagenes/Agregar.png" alt="Añadir" /></button>
                </div>
                <div id="dvInfCodigoExterno" runat="server" style="display: none; margin-top: 0px;">
                    <asp:Label ID="lblInfCodigoExterno" runat="server" Text="Código Externo"></asp:Label><br />
                    <asp:TextBox ID="txtInfCodigoExterno" runat="server" />
                    <button type="button" onclick="javascript:adicionarCodigoExterno();" class="ui-datepicker-trigger" style="width: auto; height: auto; padding: 0px; margin: 0px;">
                        <img src="../Imagenes/Agregar.png" alt="Añadir" /></button>
                </div>
                <div id="dvInfValores" runat="server" style="display: none; margin-top: 0px; height: auto;">
                    <asp:Label ID="lblInfValores" runat="server" Text="Valores"></asp:Label><br />
                    <div id="lstItensAdicionados" runat="server" style="height: auto !important;" class="BuscarPorvalores"></div>
                    <asp:HiddenField ID="hidItensAdicionados" runat="server" />
                </div>


                <div id="dvBuscar" runat="server" style="display: block; margin-top: 0px; line-height: 35px;">
                    <asp:Button ID="btnBuscar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                </div>
                <div class="dvclear"></div>
            </div>
    </div>
    <div class="ui-panel-titlebar">
        <asp:Label ID="lblTitulo2" runat="server" Text="Resultado de la consulta" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
    </div>
    <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
        <asp:UpdatePanel ID="upnSearchResults" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:GridView ID="grvResultadoDocumentos" runat="server" AutoGenerateColumns="False"
                    BorderStyle="None" Width="97%" EnableModelValidation="True">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Literal ID="litImgIcono" runat="server"></asp:Literal>
                            </ItemTemplate>
                            <HeaderStyle Width="11px" Font-Size="10px" />
                            <ItemStyle HorizontalAlign="Center" BackColor="#767676" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Código Externo" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="Numero" runat="server" ForeColor="Goldenrod" Text='<%# DataBinder.Eval(Container.DataItem, "NumeroExterno") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="N° Comprovante" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="CodigoComprobante" runat="server" ForeColor="Goldenrod" Text='<%# DataBinder.Eval(Container.DataItem, "CodigoComprobante") %>' />
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nome" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="Nome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DescripcionFormulario")%>' />
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Canal Cliente" Visible="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="CanalCliente" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector Origen" Visible="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="SectorOrigen" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DescripcionSector")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ao centro proc." Visible="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="AoCentroProc" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Do centro proc." Visible="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="DoCentroProc" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector Destino" Visible="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="SectorDestino" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DescripcionSector")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Criado" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="Criado" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Criado por" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="UsuarioCriacao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UsuarioCreacion")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolvido" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Resolvido" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolvido por" ItemStyle-HorizontalAlign="Center"
                            Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="UsuarioResolucao" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="redirecionaDocumento" runat="server" ImageUrl="~/App_Themes/Padrao/css/img/iconos/icon_menu_on.png"
                                    OnCommand="redirecionaDocumento_Command" />
                            </ItemTemplate>
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle ForeColor="#767676" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
                <div id="dvEmptyData" runat="server" style="display: block;">
                    <asp:Label ID="lblSemRegistro" runat="server" Style="color: #767676; font-size: 9pt; font-style: italic;"></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>
