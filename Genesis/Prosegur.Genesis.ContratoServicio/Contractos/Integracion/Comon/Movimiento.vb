Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class Movimiento

        ''' <summary>
        ''' Estado en el que se debe crear el documento ( “ENCURSO”, “CONFIRMADO”, ACEPTADO”).
        ''' </summary>
        Public Property accion As Enumeradores.Accion

        ''' <summary>
        ''' Código de Formulario a crear.
        ''' </summary>
        Public Property codigoFormulario As String

        ''' <summary>
        ''' Estructura donde vendrá informados los datos de la cuenta origen
        ''' </summary>
        Public Property origen As DatosCuenta

        ''' <summary>
        ''' Estructura donde vendrá informados los datos de la cuenta destino - En caso de no estar informada, la cuenta destino será la misma que la cuenta origen.
        ''' </summary>
        Public Property destino As DatosCuenta

        ''' <summary>
        ''' Identificador del movimiento. Debe ser único.
        ''' </summary>
        Public Property codigoExterno As String

        ''' <summary>
        ''' Colecciones donde vendrán informados los datos de las divisas y los Medios de Pago. Deberá venir informado una colección o ambas.
        ''' </summary>
        Public Property valores As Valores

        ''' <summary>
        ''' Colección de Campos adicionales  
        ''' </summary>
        Public Property camposAdicionales As List(Of CampoAdicional)

        ''' <summary>
        ''' Fecha y hora del movimiento de gestión de fondos.
        ''' </summary>
        Public Property fechaHoraGestionFondos As DateTime
        Public Property FechaContable As DateTime?
        Public Property ActualId As String
        Public Property CollectionId As String

        ''' <summary>
        ''' Usuario que está llamando el servicio
        ''' </summary>
        Public Property usuario As String

    End Class

End Namespace