
Namespace Grupo.GetATMsbyGrupo

    ''' <summary>
    ''' Classe SubCliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 13/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class SubCliente

#Region "[Variáveis]"

        Private _codigoSubcliente As String
        Private _descripcionSubcliente As String
        Private _puntosServicio As List(Of PuntoServicio)

#End Region

#Region "[Propriedades]"

        Public Property CodigoSubcliente() As String
            Get
                Return _codigoSubcliente
            End Get
            Set(value As String)
                _codigoSubcliente = value
            End Set
        End Property

        Public Property DescripcionSubcliente() As String
            Get
                Return _descripcionSubcliente
            End Get
            Set(value As String)
                _descripcionSubcliente = value
            End Set
        End Property

        Public Property PuntosServicio() As List(Of PuntoServicio)
            Get
                Return _puntosServicio
            End Get
            Set(value As List(Of PuntoServicio))
                _puntosServicio = value
            End Set
        End Property

#End Region

    End Class

End Namespace