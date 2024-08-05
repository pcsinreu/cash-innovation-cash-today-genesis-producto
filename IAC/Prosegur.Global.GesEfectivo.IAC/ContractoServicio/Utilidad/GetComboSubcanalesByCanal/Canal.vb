Namespace Utilidad.GetComboSubcanalesByCanal

    <Serializable()> _
    Public Class Canal

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _subCanales As SubCanalColeccion

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

        Public Property SubCanales() As SubCanalColeccion
            Get
                Return _subCanales
            End Get
            Set(value As SubCanalColeccion)
                _subCanales = value
            End Set
        End Property

#End Region

    End Class
End Namespace