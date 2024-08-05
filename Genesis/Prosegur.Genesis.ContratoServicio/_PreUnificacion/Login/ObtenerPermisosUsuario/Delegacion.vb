Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
    <DataContract()> _
    Public Class Delegacion

        <DataMember()> _
        Public Property Codigo() As String

        <DataMember()> _
        Public Property Nombre() As String

        <DataMember()> _
        Public Property Zona() As String

        <DataMember()> _
        Public Property GMT() As Short

        <DataMember()> _
        Public Property VeranoFechaHoraIni() As DateTime

        <DataMember()> _
        Public Property VeranoFechaHoraFin() As DateTime

        <DataMember()> _
        Public Property VeranoAjuste() As Short

        <DataMember()> _
        Public Property CantidadMinutosIni() As Short

        <DataMember()> _
        Public Property CantidadMetrosBase() As Short

        <DataMember()> _
        Public Property CantidadMinutosSalida() As Short

        <DataMember()> _
        Public Property Plantas() As PlantaColeccion

    End Class

End Namespace

