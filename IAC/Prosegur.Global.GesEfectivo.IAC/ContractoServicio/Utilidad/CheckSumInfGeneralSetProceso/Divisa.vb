Namespace Utilidad.CheckSumInfGeneralSetProceso


    <Serializable()> _
Public Class Divisa

#Region "[VARIÁVEIS]"

        Private _codigo As String

#End Region

#Region "PROPRIEDADES"

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