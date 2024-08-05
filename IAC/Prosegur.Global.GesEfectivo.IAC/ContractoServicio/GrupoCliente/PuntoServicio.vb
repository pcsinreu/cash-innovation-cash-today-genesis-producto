Namespace GrupoCliente

    ''' <summary>
    ''' Classe PuntoServicio
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 24/10/2012 - Criado
    ''' </history>
    <Serializable()> _
    Public Class PuntoServicio

#Region "[VARIÁVEIS]"

        Private _CodPtoServicio As String
        Private _DesPtoServivico As String
        Private _OidPtoServivico As String


#End Region

#Region "[PROPRIEDADES]"

        Public Property CodPtoServicio As String
            Get
                Return _CodPtoServicio
            End Get
            Set(value As String)
                _CodPtoServicio = value
            End Set
        End Property

        Public Property DesPtoServicio As String
            Get
                Return _DesPtoServivico
            End Get
            Set(value As String)
                _DesPtoServivico = value
            End Set
        End Property


        Public Property OidPtoServivico As String
            Get
                Return _OidPtoServivico
            End Get
            Set(value As String)
                _OidPtoServivico = value
            End Set
        End Property

#End Region

    End Class

End Namespace


