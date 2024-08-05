Namespace Utilidad.GetComboDelegaciones

    ''' <summary>
    ''' Classe Delegacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/03/2009 Criado
    ''' [pgoncalves] 20/02/2013 - Alterado
    ''' </history>
    <Serializable()> _
    Public Class Delegacion

#Region "[Variáveis]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _OidDelegacion As String
        Private _CodNombreParametro As String

#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property OidDelegacion() As String
            Get
                Return _OidDelegacion
            End Get
            Set(value As String)
                _OidDelegacion = value
            End Set
        End Property

        Public Property CodNombreParametro() As String
            Get
                Return _CodNombreParametro
            End Get
            Set(value As String)
                _CodNombreParametro = value
            End Set
        End Property

#End Region

    End Class

End Namespace
