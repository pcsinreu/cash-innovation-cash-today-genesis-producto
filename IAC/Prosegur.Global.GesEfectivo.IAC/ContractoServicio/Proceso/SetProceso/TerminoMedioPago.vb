Namespace Proceso.SetProceso

    ''' <summary>
    ''' Classe TerminoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class TerminoMedioPago

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _esObligatorioTerminoMedioPago As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property EsObligatorioTerminoMedioPago() As Boolean
            Get
                Return _esObligatorioTerminoMedioPago
            End Get
            Set(value As Boolean)
                _esObligatorioTerminoMedioPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace