<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="DocumentosPendientes.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.DocumentosPendientes" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ConfirmaFechaCertificacion(msg, idControl, fechaAntigua)
        {
            if(fechaAntigua != ''){
                $("#" + idControl).datetimepicker("hide");
                if (!confirm(msg)) {
                    $("#" + idControl).val(fechaAntigua);
                }
            }
            else {
                if (!confirm(msg)) {                    
                    $("#" + idControl).prop("checked", !$("#" + idControl).is(":checked"));
                }
            }         
        }
        function DefineFechaPlanCert(idControl, fechaAntigua) {
            $("#" + idControl).datetimepicker("hide");
            $("#" + idControl).val(fechaAntigua);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTitulo" runat="server" Text="Filtros" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
        </div>
        <div style="width: 100%; margin-left: 0px; margin-top: 5px;">
            <div id="dvFiltros" style="display: block; margin-left: 0px; margin-top: 5px;">
                <div style="float: left;">
                    <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="float: left;">
                    <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="dvclear"></div>
            <div class="dvUsarFloat">
                <div id="dvFechaCertDesde" runat="server" style="display: block; margin-top: 0px;">
                    <asp:Label ID="lblFechaCertDesde" Text="" runat="server" />
                    <br />
                    <asp:TextBox ID="txtFechaCertDesde" runat="server" />
                </div>
                <div id="dvFechaCertHasta" runat="server" style="display: block; margin-top: 0px;">
                    <asp:Label ID="lblFechaCertHasta" Text="" runat="server" />
                    <br />
                    <asp:TextBox ID="txtFechaCertHasta" runat="server" />
                </div>
                <div id="dvTipoDocumento" runat="server" style="display: block; margin-top: 0px;">
                    <asp:Label ID="lblTipoDocumento" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlTipoDocumento" runat="server"></asp:DropDownList>
                </div>
                <div class="dvclear"></div>
                <div id="dvIncDocSinFechaCert" runat="server" style="display: block; margin-top: 0px;">
                    <asp:CheckBox ID="chkIncDocSinFechaCert" runat="server"></asp:CheckBox>
                </div>
                <div id="dvIncDocNoCert" runat="server" style="display: block; margin-top: 0px;">
                    <asp:CheckBox ID="chkIncDocNoCert" runat="server"></asp:CheckBox>
                </div>
                <div class="dvclear"></div>
                <div id="dvBuscar" runat="server" style="display: block; margin-top: 0px;">
                    <asp:Button ID="btnBuscar" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                </div>
                <div class="dvclear"></div>
            </div>
        </div>
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTitulo1" runat="server" Text="Resultado de la consulta" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
        </div>
        <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
            <asp:UpdatePanel ID="upnSearchResults" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:GridView ID="grvResultadoDocumentos" runat="server" AutoGenerateColumns="False"
                        BorderStyle="None" Width="97%" EnableModelValidation="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Código Externo" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                                <ItemTemplate>
                                    <asp:Label ID="Identificador" runat="server" Font-Size="7pt" Text='<%# DataBinder.Eval(Container.DataItem, "Identificador") %>' Visible="false" />
                                    <asp:Label ID="Numero" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NumeroExterno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="N° Comprovante" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="CodigoComprobante" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CodigoComprobante") %>' />
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Formulário" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Formulario" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DescripcionFormulario") %>' />
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cliente" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Cliente" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CuentaSaldoOrigen.Cliente.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubCliente" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="SubCliente" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CuentaSaldoOrigen.SubCliente.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubCanal" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="SubCanal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CuentaSaldoOrigen.SubCanal.Descripcion")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sector Origen" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="SectorOrigen" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CuentaSaldoOrigen.Sector.Descripcion")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sector Destino" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="SectorDestino" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CuentaSaldoDestino.Sector.Descripcion")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha y Hora Plan Certificación" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>                                                                        
                                    <asp:Label ID="FechaPlanCert" runat="server" Font-Size="7pt" Text='<%# DataBinder.Eval(Container.DataItem, "FechaPlanCertificacion") %>' Visible="false" />
                                    <asp:TextBox ID="txtFechaPlanCert" runat="server" Font-Size="7pt" Text='<%# DataBinder.Eval(Container.DataItem, "FechaPlanCertificacion") %>' AutoPostBack="true" OnTextChanged="txtFechaPlanCert_TextChanged" />
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="NoCertificar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "NoCertificar")%>' Visible="false"></asp:CheckBox>
                                    <asp:CheckBox ID="chkExcluir" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "NoCertificar")%>' OnCheckedChanged="chkExcluir_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="redirecionaDocumento" runat="server" ImageUrl="~/App_Themes/Padrao/css/img/iconos/icon_menu_on.png" />                                                                        
                                </ItemTemplate>
                                <HeaderStyle Font-Size="9px" />
                                <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
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
    <asp:PlaceHolder ID="phAcciones" runat="server"></asp:PlaceHolder>
</asp:Content>
