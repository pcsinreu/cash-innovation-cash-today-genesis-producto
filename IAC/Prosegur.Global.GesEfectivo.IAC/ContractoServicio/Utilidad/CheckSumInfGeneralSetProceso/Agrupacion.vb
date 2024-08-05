Namespace Utilidad.CheckSumInfGeneralSetProceso

    <Serializable()> _
    Public Class Agrupacion

#Region "[VARIÁVEIS]"

        Private _descripcion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

#End Region

    End Class
End Namespace
