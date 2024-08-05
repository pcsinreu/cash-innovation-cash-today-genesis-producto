Namespace GetClientesSubClientesPuntoServicios

    ''' <summary>
    ''' Classe PuntoServicio
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' [vinicius.gama] 22/10/2009 Alterado - Adicionado propriedade Oid, identificador unico
    ''' </history>
    <Serializable()> _
    Public Class PuntoServicio

#Region "[VARIAVEIS]"

        Private _OidPuntoServicio As String
        Private _CodPuntoServicio As String
        Private _DescripcionPuntoServicio As String
        Private _Enviado As Boolean
        
#End Region

#Region "[PROPRIEDADES]"

        Public Property OidPuntoServicio() As String
            Get
                Return _OidPuntoServicio
            End Get
            Set(value As String)
                _OidPuntoServicio = value
            End Set
        End Property

        Public Property CodPuntoServicio() As String
            Get
                Return _CodPuntoServicio
            End Get
            Set(value As String)
                _CodPuntoServicio = value
            End Set
        End Property

        Public Property DescripcionPuntoServicio() As String
            Get
                Return _DescripcionPuntoServicio
            End Get
            Set(value As String)
                _DescripcionPuntoServicio = value
            End Set
        End Property

        Public Property Enviado() As Boolean
            Get
                Return _Enviado
            End Get
            Set(value As Boolean)
                _Enviado = value
            End Set
        End Property

#End Region

    End Class

End Namespace