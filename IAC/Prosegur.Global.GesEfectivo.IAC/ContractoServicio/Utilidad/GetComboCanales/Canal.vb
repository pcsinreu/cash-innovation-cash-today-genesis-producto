Namespace Utilidad.GetComboCanales

    <Serializable()> _
    Public Class Canal

#Region "[VARIÁVEIS]"

        Private _identificador As String
        Private _codigo As String
        Private _descripcion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Identificador() As String
            Get
                Return _identificador
            End Get
            Set(value As String)
                _identificador = value
            End Set
        End Property

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

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