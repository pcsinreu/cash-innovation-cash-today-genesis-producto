Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.GrupoDocumento.ConsultaGrupoDocumentos

    <XmlType(Namespace:="urn:ConsultaGrupoDocumentos")> _
    <XmlRoot(Namespace:="urn:ConsultaGrupoDocumentos")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property Usuario As String
        Public Property Contrasena As String
        Public Property Delegacion As String
        Public Property Planta As String
        Public Property Sector As String
        Public Property Puesto As String
        Public Property Cliente As String
        Public Property SubCliente As String
        Public Property PuntoServicio As String
        Public Property Canal As String
        Public Property SubCanal As String
        Public Property EstadosDocumento As ObservableCollection(Of Enumeradores.EstadoDocumento)
        Public Property ConjuntosCaracteristicas As ObservableCollection(Of ObservableCollection(Of Enumeradores.CaracteristicaFormulario))
        Public Property FechaHoraDesde As Nullable(Of DateTime)
        Public Property FechaHoraHasta As Nullable(Of DateTime)
    End Class

End Namespace