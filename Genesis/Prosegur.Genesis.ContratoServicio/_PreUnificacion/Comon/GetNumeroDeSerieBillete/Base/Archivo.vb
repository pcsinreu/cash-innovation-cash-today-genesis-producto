Namespace Comon.GetNumeroDeSerieBillete.Base

    ''' <summary>
    ''' Archivo
    ''' </summary>
    ''' <history>
    ''' [mult.guilherme.silva] 17/07/2013 Criado
    ''' </history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Archivo

#Region "Variáveis"

        Private _NombreArchivo As String
        Private _CodDelegacion As String
        Private _FyhActualizacion As Date
        Private _OidRemesa As String
        Private _OidBulto As String
        Private _Divisas As DivisaColeccion
        Private _CodUsuario As String






#End Region
        
#Region "Propriedades"
        Public Property FyhActualizacion() As Date
            Get
                Return _FyhActualizacion
            End Get
            Set(value As Date)
                _FyhActualizacion = value
            End Set
        End Property



        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

     

        Public Property NombreArchivo() As String
            Get
                Return _NombreArchivo
            End Get
            Set(value As String)
                _NombreArchivo = value
            End Set
        End Property


        Public Property OidBulto() As String
            Get
                Return _OidBulto
            End Get
            Set(value As String)
                _OidBulto = value
            End Set
        End Property


        Public Property OidRemesa() As String
            Get
                Return _OidRemesa
            End Get
            Set(value As String)
                _OidRemesa = value
            End Set
        End Property

        Public Property Divisas() As DivisaColeccion

            Get
                Return _Divisas
            End Get
            Set(value As DivisaColeccion
)
                _Divisas = value
            End Set
        End Property


        Public Property CodUsuario() As String
            Get
                Return _CodUsuario
            End Get
            Set(value As String)
                _CodUsuario = value
            End Set
        End Property

#End Region

    End Class

End Namespace
