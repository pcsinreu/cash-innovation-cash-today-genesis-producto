Imports System.Xml.Serialization
Imports System.Xml

Namespace Cliente.GetClientes

    <Serializable()>
    <XmlType(Namespace:="urn:GetClientes")>
    <XmlRoot(Namespace:="urn:GetClientes")>
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property CodTipoCliente As String
        Public Property BolTotalizadorSaldo As Boolean?
        Public Property BolVigente As Boolean?
        Public Property BolAbonaPorSaldo As Boolean?
        Public Property TipoBanco As TipoBanco?
    End Class

    Public Enum TipoBanco

        Todos = 1
        Capital = 2
        Comision = 3
    End Enum

End Namespace
