Namespace GrupoCliente

    ''' <summary>
    ''' Classe SubCliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 24/10/2012 - Criado
    ''' </history>
    <Serializable()> _
    Public Class SubCliente

#Region "[VARIÁVEIS]"

        Private _CodSubCliente As String
        Private _DesSubCliente As String
        Private _PtosServicio As PuntoServicioColeccion
        Private _OidSubCliente As String
        

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodSubCliente As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

        Public Property DesSubCliente As String
            Get
                Return _DesSubCliente
            End Get
            Set(value As String)
                _DesSubCliente = value
            End Set
        End Property

        Public Property PtosServicio As PuntoServicioColeccion
            Get
                Return _PtosServicio
            End Get
            Set(value As PuntoServicioColeccion)
                _PtosServicio = value
            End Set
        End Property

        Public Property OidSubCliente As String
            Get
                Return _OidSubCliente
            End Get
            Set(value As String)
                _OidSubCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace