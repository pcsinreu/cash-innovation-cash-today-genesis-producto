Namespace Proceso.SetProceso

    ''' <summary>
    ''' Classe SubCliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>    
    <Serializable()> _
    Public Class SubCliente

#Region "[VARIÁVEIS]"

        Private _codigo As String
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