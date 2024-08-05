Imports Prosegur.Genesis.ContractoServicio
Imports System.Xml.Serialization

Namespace Contractos.Pruebas.crearDocumentoFondos

    <Serializable()>
    Public Class Movimiento

        ''' <summary>
        ''' Estructura donde vendrá informados los datos de la cuenta origen
        ''' </summary>
        Public Property origen As Contractos.Integracion.Comon.DatosCuenta

        ''' <summary>
        ''' Estructura donde vendrá informados los datos de la cuenta destino - En caso de no estar informada, la cuenta destino será la misma que la cuenta origen.
        ''' </summary>
        Public Property destino As Contractos.Integracion.Comon.DatosCuenta

        ''' <summary>
        ''' Colecciones donde vendrán informados los datos de las divisas y los Medios de Pago. Deberá venir informado una colección o ambas.
        ''' </summary>
        Public Property valores As Contractos.Integracion.Comon.Valores

        ''' <summary>
        ''' Colección de Campos adicionales  
        ''' </summary>
        Public Property camposAdicionales As List(Of Contractos.Integracion.Comon.CampoAdicional)

    End Class

End Namespace