Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoCliente.GetTiposClientes

    ''' <summary>
    ''' Classe Peticion de Tipo Clientes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetTiposClientes")> _
    <XmlRoot(Namespace:="urn:GetTiposClientes")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _codTipoCliente As String

        Private _desTipoCliente As String
        
        Private _bolActivo As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property codTipoCliente() As String
            Get
                Return _codTipoCliente
            End Get
            Set(value As String)
                _codTipoCliente = value
            End Set
        End Property

        Public Property desTipoCliente() As String
            Get
                Return _desTipoCliente
            End Get
            Set(value As String)
                _desTipoCliente = value
            End Set
        End Property

        Public Property bolActivo() As Nullable(Of Boolean)
            Get
                Return _bolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _bolActivo = value
            End Set
        End Property

#End Region

    End Class
End Namespace

