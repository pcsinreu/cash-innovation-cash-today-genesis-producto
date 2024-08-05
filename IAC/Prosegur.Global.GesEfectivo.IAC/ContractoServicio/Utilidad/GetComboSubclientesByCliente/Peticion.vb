Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboSubclientesByCliente


    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboSubclientesByCliente")> _
    <XmlRoot(Namespace:="urn:GetComboSubclientesByCliente")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _codigosClientes As List(Of String)
        Private _codigoSubcliente As String
        Private _descripcionSubcliente As String
        Private _TotalizadorSaldo As Boolean
        Private _Vigente As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigosClientes() As List(Of String)
            Get
                Return _codigosClientes
            End Get
            Set(value As List(Of String))
                _codigosClientes = value
            End Set
        End Property

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

        Public Property TotalizadorSaldo() As Boolean
            Get
                Return _TotalizadorSaldo
            End Get
            Set(value As Boolean)
                _TotalizadorSaldo = value
            End Set
        End Property

        Public Property vigente() As Nullable(Of Boolean)
            Get
                Return _Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _Vigente = value
            End Set
        End Property

#End Region

    End Class
End Namespace