Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Dashboard.Productividad.RetornaValoresProcesados

    <XmlType(Namespace:="urn:RetornaValoresProcesados")> _
    <XmlRoot(Namespace:="urn:RetornaValoresProcesados")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Sub New()
        End Sub
        Public Sub New(mensaje As String)
            MyBase.New(mensaje)
        End Sub

        Public Sub New(excepcion As Exception)
            MyBase.New(excepcion)
        End Sub

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaValoresProcesados")> _
    <XmlRoot(Namespace:="urn:RetornaValoresProcesados")> _
    <Serializable()>
    Public Class Dados
        Public CodigoDelegacion As String
        Public DescricaoDelegacion As String
        Public FechaHora As DateTime
        Public CodigoSetor As String
        Public DescricaoSetor As String
        Public CodigoCliente As String
        Public DescricaoCliente As String
        Public CodigoProducto As String
        Public DescricaoProducto As String
        Public CodigoUsuario As String
        Public DescricaoUsuario As String
        Public CodigoIsoDivisa As String
        Public DescricaoDivisa As String
        Public CodigoDenominacion As String
        Public DescricaoDenominacion As String
        Public BolMecanizado As Boolean
        Public NelCantidadMecanizado As Long
        Public NelCantidadManual As Long
        'Public NelCantidadMinutosMecanizado As Long
        'Public NelCantidadMinutosManual As Long
        Public Tipo As String
    End Class

End Namespace