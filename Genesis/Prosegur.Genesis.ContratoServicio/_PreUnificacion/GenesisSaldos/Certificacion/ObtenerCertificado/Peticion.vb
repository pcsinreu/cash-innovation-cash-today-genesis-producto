Imports System.Xml.Serialization
Imports System.Xml

Namespace GenesisSaldos.Certificacion.ObtenerCertificado

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/06/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:ObtenerCertificado")> _
    <XmlRoot(Namespace:="urn:ObtenerCertificado")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _CodigoCliente As String
        Private _EstadoCertificado As List(Of String)
        Private _IdentificadorDelegacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property codigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property

        Public Property estadoCertificado() As List(Of String)
            Get
                Return _EstadoCertificado
            End Get
            Set(value As List(Of String))
                _EstadoCertificado = value
            End Set
        End Property

        Public Property IdentificadorDelegacion() As String
            Get
                Return _IdentificadorDelegacion
            End Get
            Set(value As String)
                _IdentificadorDelegacion = value
            End Set
        End Property

#End Region
    End Class

End Namespace

