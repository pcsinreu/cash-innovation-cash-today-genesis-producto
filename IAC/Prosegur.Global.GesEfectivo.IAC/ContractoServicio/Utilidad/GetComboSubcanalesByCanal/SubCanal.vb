Namespace Utilidad.GetComboSubcanalesByCanal

    <Serializable()> _
    Public Class SubCanal

#Region "[VARIÁVEIS]"

        Private _oidSubCanal As String
        Private _codigo As String
        Private _descripcion As String
#End Region

#Region "[PROPRIEDADES]"

        Public Property OidSubCanal() As String
            Get
                Return _oidSubCanal
            End Get
            Set(value As String)
                _oidSubCanal = value
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