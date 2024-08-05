Imports System.Xml.Serialization
Imports System.Xml

Namespace GrupoCliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' </history>
    <Serializable()> _
    Public Class ClienteDetalle


#Region "[VARIÁVEIS]"

        Private _OidGrupoClienteDetalle As String
        Private _CodCliente As String
        Private _DesCliente As String
        Private _OidCliente As String
        Private _CodSubCliente As String
        Private _DesSubCliente As String
        Private _OidSubCliente As String
        Private _CodPtoServicio As String
        Private _DesPtoServivico As String
        Private _OidPtoServivico As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodSubCliente() As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

        Public Property DesSubCliente() As String
            Get
                Return _DesSubCliente
            End Get
            Set(value As String)
                _DesSubCliente = value
            End Set
        End Property

        Public Property OidSubCliente() As String
            Get
                Return _OidSubCliente
            End Get
            Set(value As String)
                _OidSubCliente = value
            End Set
        End Property

        Public Property CodPtoServicio() As String
            Get
                Return _CodPtoServicio
            End Get
            Set(value As String)
                _CodPtoServicio = value
            End Set
        End Property

        Public Property DesPtoServivico() As String
            Get
                Return _DesPtoServivico
            End Get
            Set(value As String)
                _DesPtoServivico = value
            End Set
        End Property

        Public Property OidPtoServivico() As String
            Get
                Return _OidPtoServivico
            End Get
            Set(value As String)
                _OidPtoServivico = value
            End Set
        End Property

        Public Property CodCliente As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

        Public Property DesCliente As String
            Get
                Return _DesCliente
            End Get
            Set(value As String)
                _DesCliente = value
            End Set
        End Property

        Public Property OidGrupoClienteDetalle As String
            Get
                Return _OidGrupoClienteDetalle
            End Get
            Set(value As String)
                _OidGrupoClienteDetalle = value
            End Set
        End Property

        Public Property OidCliente As String
            Get
                Return _OidCliente
            End Get
            Set(value As String)
                _OidCliente = value
            End Set
        End Property

#End Region


    End Class

End Namespace