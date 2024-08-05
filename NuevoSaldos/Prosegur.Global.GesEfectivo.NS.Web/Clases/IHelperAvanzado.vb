Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper

''' <summary>
''' Interface Controle Helper.
''' </summary>
''' <history>
''' PGP-212
'''</history>
Public Interface IHelperAvanzado

    Property Tabela As Tabela

    Property MultiSelecao As Boolean
    Property Obrigatorio As Boolean
    Property Titulo As String

    Property Popup_Titulo As String

    Property Popup_Filtro As String

    Property Popup_Resultado As String

    Property ControleHabilitado As Boolean

    Property ExibeControlePreenchido As Boolean

    Property MaxRegistroPorPagina As Integer

    Property OrderConsulta As Dictionary(Of String, UtilHelper.OrderSQL)

    Property FiltroConsulta As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Property FiltroAvanzado As Dictionary(Of String, String)

    Property JoinConsulta As Dictionary(Of String, UtilHelper.JoinSQL)

    Property ResultadoConsulta As RespuestaHelper

    Sub LimparCampos()

End Interface
