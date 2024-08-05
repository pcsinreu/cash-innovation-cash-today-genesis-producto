Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarSeguimientoElemento

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <XmlRoot(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <Serializable()> _
    Public Class Documento

#Region "[PROPRIEDADES]"

        Public Property oidDocumento As String
        Public Property esAgrupador As Boolean
        Public Property codFormulario As String
        Public Property desFormulario As String
        Public Property codEstado As String
        Public Property fechaModificacion As DateTime
        Public Property desUsuarioModificacion As String
        Public Property Acciones As System.Collections.ObjectModel.ObservableCollection(Of String)
        Public Property CuentasOrigen As List(Of Cuenta)
        Public Property CuentasDestino As List(Of Cuenta)
        Public Property ContenedorPadre As ContenedorRespuesta
        Public Property ContenedorMayorNivel As ContenedorRespuesta

#End Region

    End Class
End Namespace