Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresSector

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarContenedoresSector")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresSector")> _
    <Serializable()> _
    Public Class ContenedorRespuesta

#Region "[PROPRIEDADES]"

        Public Property IdentificadorContenedor As String
        Public Property codTipoContenedor As String
        Public Property DesTipoContenedor As String
        Public Property codEstadoContenedor As String
        Public Property fechaHoraArmado As DateTime
        Public Property Precintos As List(Of String)
        Public Property PrecintoAutomatico As String
        Public Property Sector As SectorRespuesta
        Public Property Cuentas As List(Of Cuenta)
        Public Property AceptaPico As Integer
        Public Property CodigoPosicion As String
        Public Property FechaVencimento As Date
        Public Property UsuarioCreacion As String
        Public Property CodPuesto As String

        '  Public Property Canais As List(Of Canal)
        '  Public Property DetalheEfectivo As List(Of DetalleEfectivo)
        ' Public Property DetalheMedioPago As List(Of DetalleMedioPago)

#End Region

    End Class
End Namespace