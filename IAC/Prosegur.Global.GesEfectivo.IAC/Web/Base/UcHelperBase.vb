Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper

''' <summary>
''' Define Classe Base Helper.
''' </summary>
<Serializable()>
Public MustInherit Class UcHelperBase
    Inherits UcBase
    Implements IHelper

    ''' <summary>
    ''' Define a Tabela que deverá ser carregada no Controle Helper.
    ''' </summary>
    Public Property Tabela As Tabela Implements IHelper.Tabela

    ''' <summary>
    ''' Determina se será possível selecionar mais de um resultado no Grid.
    ''' </summary>
    ''' <example>True: Exibe controle do tipo checkbox no Grid.</example>
    ''' <example>False: Exibe controle do tipo radiobutton no Grid.</example>
    Public Property MultiSelecao As Boolean Implements IHelper.MultiSelecao

    ''' <summary>
    ''' Valor a ser Exibido no Fieldset/Label do controle.
    ''' </summary>
    Public Property Titulo As String Implements IHelper.Titulo

    ''' <summary>
    ''' Título a ser exibido no Popup de Busca Avançada.
    ''' </summary>
    Public Property Popup_Titulo As String Implements IHelper.Popup_Titulo

    ''' <summary>
    ''' Título a ser exibido no Fieldset que agrupará os campos do formulário de Busca do Popup.
    ''' </summary>
    Public Property Popup_Filtro As String Implements IHelper.Popup_Filtro

    ''' <summary>
    ''' Título a ser exibido no Fieldset que agrupará o Grid de Resultados no Popup.
    ''' </summary>
    Public Property Popup_Resultado As String Implements IHelper.Popup_Resultado

    ''' <summary>
    ''' Define se o controle estará habilitado para seleção.
    ''' </summary>
    Public Property ControleHabilitado As Boolean Implements IHelper.ControleHabilitado

    ''' <summary>
    ''' Define se o controle deverá ser exido preenchido.
    ''' </summary>
    Public Property ExibeControlePreenchido As Boolean Implements IHelper.ExibeControlePreenchido

    ''' <summary>    
    ''' Define o número máximo de registros a serem exibidos no Grid de Resultados da pesquisa por paginação.
    ''' </summary>    
    Public Property MaxRegistroPorPagina As Integer Implements IHelper.MaxRegistroPorPagina

    ''' <summary>
    ''' Define como os dados retornados da consulta deverão ser ordenados.
    ''' </summary>
    ''' <example>
    ''' <code>
    ''' ucCliente.ClienteOrden.Add("CODIGO", New UtilHelper.OrderSQL("COD_CLIENTE"))
    ''' </code>
    ''' </example>
    ''' <remarks>Preenchimento desta propriedade é opcional.</remarks>
    Public Property OrdenacaoConsulta As Dictionary(Of String, UtilHelper.OrderSQL) Implements IHelper.OrderConsulta

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa.
    ''' </summary>
    ''' <example>
    ''' <code>    
    ''' lstFiltros.Add(New UtilHelper.ArgumentosFiltro("BOL_VIGENTE", "1"))
    ''' FiltroConsulta.Add(New UtilHelper.TabelaFiltro_ With {.Tabela = EnumHelper.Tabela_Genesis.Cliente}, lstFiltros)
    ''' </code>
    ''' </example>
    ''' <remarks>Preenchimento desta propriedade é opcional.</remarks>    
    Public Property FiltroConsulta As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)) Implements IHelper.FiltroConsulta

    ''' <summary>
    ''' Define as junções a serem incluídas na Query.
    ''' </summary>
    ''' <example>
    ''' <code>
    ''' New UtilHelper.JoinSQL With 
    ''' {
    ''' .TabelaEsquerda = New UtilHelper.TabelaHelper With {.Tabela = EnumHelper.Tabela_Genesis.Planta},
    ''' .CampoComumTabEsq = "OID_PLANTA",
    ''' .TabelaDireita = New UtilHelper.TabelaHelper With {.Tabela = EnumHelper.Tabela_Genesis.Sector},
    ''' .CampoComumTabDireita = "OID_PLANTA"
    ''' }
    ''' </code>
    ''' </example>    
    ''' <remarks>Preenchimento desta propriedade é opcional.</remarks>
    Public Property JoinConsulta As Dictionary(Of String, UtilHelper.JoinSQL) Implements IHelper.JoinConsulta

    ''' <summary>
    ''' Define Tipo Resposta Helper.
    ''' </summary>
    Protected Property RespostaHelper As RespuestaHelper Implements IHelper.ResultadoConsulta

    ''' <summary>
    ''' Método responsável pela limpeza dos campos do Controle Helper.
    ''' </summary>
    MustOverride Sub LimparCampos() Implements IHelper.LimparCampos

    Public Property Obrigatorio As Boolean Implements IHelper.Obrigatorio
End Class