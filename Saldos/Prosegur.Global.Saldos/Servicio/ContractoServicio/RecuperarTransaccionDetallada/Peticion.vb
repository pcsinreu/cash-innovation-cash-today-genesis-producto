Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarTransaccionDetallada

    ''' <summary>
    ''' Clase Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 12/07/2011 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarTransaccionDetallada")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionDetallada")> _
    <Serializable()> _
    Public Class Peticion

#Region "[INICIALIZAÇÃO]"

        Sub New()

            Usuario = New GuardarDatosDocumento.Usuario

        End Sub

#End Region

#Region "[VARIAVEIS]"

        Private _Usuario As GuardarDatosDocumento.Usuario
        Private _OidTransaccion As String

#End Region

#Region "[PROPRIEDADES]"


        ''' <summary>
        ''' Datos del Usuario
        ''' </summary>
        ''' <value>GuardarDatosDocumento.Usuario</value>
        ''' <returns>GuardarDatosDocumento.Usuario</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Usuario() As GuardarDatosDocumento.Usuario
            Get
                Return _Usuario
            End Get
            Set(value As GuardarDatosDocumento.Usuario)
                _Usuario = value
            End Set
        End Property

        ''' <summary>
        ''' Identificador de la Transaccion
        ''' </summary>
        ''' <value>Boolean</value>
        ''' <returns>Boolean</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property OidTransaccion() As String
            Get
                Return _OidTransaccion
            End Get
            Set(value As String)
                _OidTransaccion = value
            End Set
        End Property

#End Region

    End Class

End Namespace