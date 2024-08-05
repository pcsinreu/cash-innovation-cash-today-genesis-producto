Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora

    <XmlType(Namespace:="urn:RetornaValoresProcesadosPorHora")> _
    <XmlRoot(Namespace:="urn:RetornaValoresProcesadosPorHora")> _
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

    <XmlType(Namespace:="urn:RetornaValoresProcesadosPorHora")> _
    <XmlRoot(Namespace:="urn:RetornaValoresProcesadosPorHora")> _
    <Serializable()>
    Public Class Dados
        'Operador
        Public CodigoUsuario As String
        Public DescricaoUsuario As String
        'Delegacion
        Public CodigoDelegacion As String
        Public DescricaoDelegacion As String
        'Sector
        Public CodigoSetor As String
        Public DescricaoSetor As String
        'Producto
        Public CodigoProducto As String
        Public DescricaoProducto As String
        'Cliente
        Public CodigoCliente As String
        Public DescricaoCliente As String
        'Productividad
        Public HorasTrabajadas As Double
        Public BilletesYMonedasProcesadas As Double
        Public ProductividadHora As Double
        ''Divisa???
        'Public CodigoIsoDivisa As String
        'Public DescricaoDivisa As String
        ''Denominacion???
        'Public CodigoDenominacion As String
        'Public DescricaoDenominacion As String
    End Class

End Namespace