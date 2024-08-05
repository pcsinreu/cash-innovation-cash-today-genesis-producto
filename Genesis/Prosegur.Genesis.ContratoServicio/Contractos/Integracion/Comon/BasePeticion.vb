Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class BasePeticion

        ''' <summary>
        ''' Validación de Origen Seguro.
        ''' </summary>
        Public Property tokenAcceso As String

        ''' <summary>
        ''' Informará si se ejecutarán las reglas de validación luego de intentar generar el documento y haya ocurrido algún error. 
        ''' </summary>
        Public Property validarPostError As Boolean

        ''' <summary>
        ''' Identificador Ajeno
        ''' </summary>
        Public Property IdentificadorAjeno As String

    End Class

End Namespace

