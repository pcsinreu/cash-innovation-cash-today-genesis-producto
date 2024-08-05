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
    Public Class Remesa

#Region "[PROPRIEDADES]"

        Public Property codigoExterno As String
        Public Property fechaHoraTransporte As DateTime
        Public Property codRuta As String
        Public Property Bulto As ConsultarSeguimientoElemento.Bulto
        Public Property fechaHoraCriacion As DateTime


#End Region

    End Class
End Namespace