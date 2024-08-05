Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Xml

Namespace GenesisSaldos.Certificacion.ValidarCertificacion

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 27/05/2013 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:ValidarCertificacion")> _
    <XmlRoot(Namespace:="urn:ValidarCertificacion")> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _CodigoCliente As String
        Private _CodigoSector As List(Of String)
        Private _CodigoSubcanal As List(Of String)
        Private _CodigoDelegacion As List(Of String)
        Private _EstadoCertificado As String
        Private _FechaHoraCertificacion As DateTime

#End Region

#Region "[PROPRIEDADES]"

        Public Property DelegacionLogada As Clases.Delegacion

        Public Property CodigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property

        Public Property CodigoSector() As List(Of String)
            Get
                Return _CodigoSector
            End Get
            Set(value As List(Of String))
                _CodigoSector = value
            End Set
        End Property

        Public Property CodigoSubcanal() As List(Of String)
            Get
                Return _CodigoSubcanal
            End Get
            Set(value As List(Of String))
                _CodigoSubcanal = value
            End Set
        End Property

        Public Property CodigoDelegacion() As List(Of String)
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As List(Of String))
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property EstadoCertificado() As String
            Get
                Return _EstadoCertificado
            End Get
            Set(value As String)
                _EstadoCertificado = value
            End Set
        End Property

        Public Property FechaHoraCertificacion() As DateTime
            Get
                Return _FechaHoraCertificacion
            End Get
            Set(value As DateTime)
                _FechaHoraCertificacion = value
            End Set
        End Property

#End Region


    End Class

End Namespace

