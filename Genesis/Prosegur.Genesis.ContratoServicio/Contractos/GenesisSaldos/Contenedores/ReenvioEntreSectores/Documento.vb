Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ReenvioEntreSectores

    <Serializable()> _
    Public Class Documento

        Public Property CodigoFormulario As String
        Public Property FechaHoraPlanCertificado As DateTime
        Public Property FechaHoraGestion As DateTime
        Public Property SectorOrigen As Comon.Sector
        Public Property SectorDestino As Comon.Sector
        Public Property Contenedores As List(Of Contenedor)
        Public Property CodigoUsuario As String

    End Class

End Namespace