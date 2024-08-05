Namespace Utilidad.getComboAplicaciones


    <Serializable()> _
    Public Class Aplicacion

#Region "[VARIÁVEIS]"

        Private _CodigoAplicacion As String
        Private _DescripcionAplicacion As String
        Private _CodigoPermisso As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        Public Property DescripcionAplicacion() As String
            Get
                Return _DescripcionAplicacion
            End Get
            Set(value As String)
                _DescripcionAplicacion = value
            End Set
        End Property

        Public Property CodigoPermiso() As String
            Get
                Return _CodigoPermisso
            End Get
            Set(value As String)
                _CodigoPermisso = value
            End Set
        End Property


#End Region

    End Class
End Namespace