Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresSector

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <Serializable()> _
    Public Class Cuenta

        Public Property IdentificadorCuenta As String
        Public Property CodigoCliente As String
        Public Property DescricaoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property CodigoCanal As String
        Public Property CodigoSubCanal As String
        Public Property DescripcionCanal As String
        Public Property DescripcionSubCanal As String
        Public Property DescripcionSubCliente As String
        Public Property DescripcionPuntoServicio As String

        Public Property DetallesEfectivo As List(Of DetalleEfectivo)

        Public Property DetallesMedioPago As List(Of DetalleMedioPago)

    End Class

End Namespace