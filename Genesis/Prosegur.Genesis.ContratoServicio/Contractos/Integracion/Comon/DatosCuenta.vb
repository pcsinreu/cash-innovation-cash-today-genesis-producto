Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
        Public Class DatosCuenta

        ''' <summary>
        ''' Código de Cliente
        ''' </summary>
        Public Property codigoCliente As String

        ''' <summary>
        ''' Código de Sub Cliente.
        ''' </summary>
        Public Property codigoSubCliente As String

        ''' <summary>
        ''' Código de Punto de Servicio
        ''' </summary>
        Public Property codigoPuntoServicio As String

        ''' <summary>
        ''' Código de la Delegación
        ''' </summary>
        Public Property codigoDelegacion As String

        ''' <summary>
        ''' Código de la Planta
        ''' </summary>
        Public Property codigoPlanta As String

        ''' <summary>
        ''' Código de Sector
        ''' </summary>
        Public Property codigoSector As String

        ''' <summary>
        ''' Código de Canal
        ''' </summary>
        Public Property codigoCanal As String

        ''' <summary>
        ''' Código Sub Canal
        ''' </summary>
        Public Property codigoSubCanal As String

    End Class

End Namespace