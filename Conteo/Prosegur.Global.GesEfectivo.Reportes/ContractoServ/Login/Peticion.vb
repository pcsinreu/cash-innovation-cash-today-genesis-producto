Imports System.Xml.Serialization
Imports System.Xml

Namespace Login

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:Login")> _
    <XmlRoot(Namespace:="urn:Login")> _
    Public Class Peticion

#Region " Variáveis "

        Private _IdentificadorUsuario As String
        Private _Password As String
        Private _Delegacion As String

#End Region

#Region " Propriedades "

        Public Property IdentificadorUsuario() As String
            Get
                Return _IdentificadorUsuario
            End Get
            Set(value As String)
                _IdentificadorUsuario = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(value As String)
                _Password = value
            End Set
        End Property

        Public Property Delegacion() As String
            Get
                Return _Delegacion
            End Get
            Set(value As String)
                _Delegacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace