Namespace GuardarCliente

    ''' <summary>
    ''' Classe PuntoServicioError
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class PuntoServicioError

#Region "[VARIAVEIS]"

        Private _CodPuntoServicio As String
        Private _DescripcionError As String

#End Region

#Region "[METODOS]"

        Public Property CodPuntoServicio() As String
            Get
                Return _CodPuntoServicio
            End Get
            Set(value As String)
                _CodPuntoServicio = value
            End Set
        End Property

        Public Property DescripcionError() As String
            Get
                Return _DescripcionError
            End Get
            Set(value As String)
                _DescripcionError = value
            End Set
        End Property

#End Region

    End Class

End Namespace