Namespace Proceso.SetProceso

    ''' <summary>
    ''' Classe Punto Servicio
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class PuntoServicio

#Region "[VARIÁVEIS]"

        Private _codigo As String

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

#End Region

    End Class

End Namespace