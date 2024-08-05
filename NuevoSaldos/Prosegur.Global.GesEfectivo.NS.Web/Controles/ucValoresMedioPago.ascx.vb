Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon

Public Class ucValoresMedioPago
    Inherits UcBase

#Region "[PROPRIEDADES]"

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
    End Sub

    ''' <summary>
    ''' Método que valida os campos obrigatorios do controle.
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Function ValidarControl() As List(Of String)
        Dim retorno As New List(Of String)
        Return retorno
    End Function

    ''' <summary>
    ''' Adiciona os scripts
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()
    'End Sub

#End Region

#Region "[EVENTOS]"

#End Region

#Region "[METODOS]"

#End Region
End Class