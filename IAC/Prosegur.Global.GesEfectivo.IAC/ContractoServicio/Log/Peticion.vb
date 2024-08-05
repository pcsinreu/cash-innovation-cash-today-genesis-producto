Imports System.Xml.Serialization
Imports System.Xml

Namespace Log

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 29/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:Log")> _
    <XmlRoot(Namespace:="urn:Log")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _LoginUsuario As String
        Private _Delegacion As String
        Private _DescricionErro As String
        Private _Otros As String
        Private _FYHErro As Date

#End Region

#Region " Propriedades "

        Public Property LoginUsuario() As String
            Get
                Return _LoginUsuario
            End Get
            Set(value As String)
                _LoginUsuario = value
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

        Public Property DescricionErro() As String
            Get
                Return _DescricionErro
            End Get
            Set(value As String)
                _DescricionErro = value
            End Set
        End Property

        Public Property Otros() As String
            Get
                Return _Otros
            End Get
            Set(value As String)
                _Otros = value
            End Set
        End Property

        Public Property FYHErro() As Date
            Get
                Return _FYHErro
            End Get
            Set(value As Date)
                _FYHErro = value
            End Set
        End Property

#End Region

    End Class

End Namespace