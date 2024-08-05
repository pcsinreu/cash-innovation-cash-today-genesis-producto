Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente

    <XmlType(Namespace:="urn:RetornaSomaValoresProcesadosCliente")> _
    <XmlRoot(Namespace:="urn:RetornaSomaValoresProcesadosCliente")> _
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

    <XmlType(Namespace:="urn:RetornaSomaValoresProcesadosCliente")> _
    <XmlRoot(Namespace:="urn:RetornaSomaValoresProcesadosCliente")> _
    <Serializable()>
    Public Class Dados
        Public CodigoCliente As String
        Public DescricaoCliente As String
        Public Total As Integer
    End Class

End Namespace