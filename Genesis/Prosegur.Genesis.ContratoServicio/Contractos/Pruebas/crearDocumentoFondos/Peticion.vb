Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Pruebas.crearDocumentoFondos

    <XmlType(Namespace:="urn:pruebasCrearDocumentoFondos")> _
    <XmlRoot(Namespace:="urn:pruebasCrearDocumentoFondos")> _
    <Serializable()>
    Public Class Peticion
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BasePeticion

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>
        <XmlElement(IsNullable:=True)>
        Public Property movimiento As Movimiento

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>
        <XmlElement(IsNullable:=True)>
        Public Property movimientoAjeno As Movimiento

        ''' <summary>
        ''' Código de Formulario a crear.
        ''' </summary>
        Public Property codigoFormulario As List(Of String)

        ''' <summary>
        ''' Fecha y hora del movimiento de gestión de fondos.
        ''' </summary>
        Public Property fechaHoraGestionFondosInicio As DateTime

        ''' <summary>
        ''' Fecha y hora del movimiento de gestión de fondos.
        ''' </summary>
        Public Property fechaHoraGestionFondosFin As DateTime

        ''' <summary>
        ''' Usuario que está llamando el servicio
        ''' </summary>
        Public Property usuario As String

    End Class

End Namespace
