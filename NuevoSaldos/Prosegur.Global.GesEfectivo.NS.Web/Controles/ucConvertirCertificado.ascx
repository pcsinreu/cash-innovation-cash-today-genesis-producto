<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucConvertirCertificado.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucConvertirCertificado" %>

<div class="ui-panel-titlebar" style="margin-top: 10px;">
    <asp:Label ID="lblCertificadosSelecionados" runat="server" Text="" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
</div>
<div id="ModalBusqueda">
    <asp:UpdatePanel ID="upCertificados" runat="server">
        <ContentTemplate>
            <script>
                function iniciarExecucao(mensagem, lstIndices) {
                    ExibirMensagemModal(mensagem);
                    ConverterCertificado(lstIndices, 0);
                }

                function ConverterCertificado(indexExecucao, posicao) {
                    jQuery.ajax({
                        url: 'ResultadoCertificacion.aspx/ConverterCertificado',
                        type: "POST",
                        dataType: "json",
                        data: "{indexExecucao: '" + indexExecucao[posicao] + "', total: '" + indexExecucao.length + "'}",
                        contentType: "application/json; charset=utf-8",
                        success: function (data, text) {
                            var json_x = JSON.parse(data.d);
                            if (json_x.CodigoError == "0" && json_x.Respuesta != null) {
                                ExibirMensagemModal(json_x.Respuesta.replace("[faltam]", (indexExecucao.length - (posicao + 1))));
                                if ((posicao + 1) == indexExecucao.length) {
                                    FecharMensagemModal();
                                    var upCertificados = '<%=upCertificados.ClientID%>';
                                    __doPostBack(upCertificados, 'Fechar')
                                } else {
                                    ConverterCertificado(indexExecucao, posicao + 1)
                                }
                            } else {
                                genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
                            }
                        },
                        error: function (request, status, error) {
                            genesisAlertError(error, request.responseText);
                        }
                    });
                }
            </script>
            <asp:Panel ID="resultado" runat="server">
                <asp:GridView ID="gridCertificados" runat="server" AutoGenerateColumns="False"
                    BorderStyle="None" Width="97%" EnableModelValidation="True" OnRowDataBound="gridCertificados_RowDataBound">
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
                                <asp:Label ID="Cliente" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Cliente.Descripcion") %>' />
                            </ItemTemplate>
                            <HeaderStyle Font-Size="9px" />
                            <ItemStyle HorizontalAlign="Left" Font-Size="7pt" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="Codigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CodigoCertificado")%>' />
                            </ItemTemplate>
                            <HeaderStyle Font-Size="9px" />
                            <ItemStyle HorizontalAlign="Left" Font-Size="7pt" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="do_tipo" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Font-Size="9px" />
                            <ItemStyle HorizontalAlign="Left" Font-Size="7pt" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="para_tipo" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Font-Size="9px" />
                            <ItemStyle HorizontalAlign="Left" Font-Size="7pt" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
                <div id="dvError" runat="server">
                    <asp:Label ID="lblError" runat="server" Style="color:red; font-size: 9pt; font-style: italic;"></asp:Label>
                </div>
                <ul class="certificados-btns">
                    <li>
                        <asp:Button ID="btnAceptar" runat="server" SkinID="filter-button" Text="Aceitar" OnClick="btnAceptar_Click" /></li>
                    <li>
                        <asp:Button ID="btnCancelar" runat="server" SkinID="filter-button" Text="Cancelar" /></li>
                </ul>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
