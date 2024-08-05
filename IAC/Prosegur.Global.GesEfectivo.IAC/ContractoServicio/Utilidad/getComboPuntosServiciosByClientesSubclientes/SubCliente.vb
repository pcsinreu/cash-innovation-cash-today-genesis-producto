Namespace Utilidad.getComboPuntosServiciosByClientesSubclientes

    <Serializable()> _
    Public Class SubCliente

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _puntosServicio As PuntoServicioColeccion

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

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property PuntosServicio() As PuntoServicioColeccion
            Get
                Return _puntosServicio
            End Get
            Set(value As PuntoServicioColeccion)
                _puntosServicio = value
            End Set
        End Property

#End Region
    End Class
End Namespace