Imports System.Xml.Serialization
Imports System.Xml

Namespace CrearMovimiento

    ''' <summary>
    ''' Clase Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 28/06/2011 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:CrearMovimiento")> _
    <XmlRoot(Namespace:="urn:CrearMovimiento")> _
    <Serializable()> _
    Public Class Peticion

#Region "[INICIALIZAÇÃO]"

        Sub New()

            Usuario = New GuardarDatosDocumento.Usuario
            Movimiento = New Movimiento

        End Sub

#End Region

#Region "[VARIAVEIS]"

        Private _Usuario As GuardarDatosDocumento.Usuario
        Private _Movimiento As Movimiento

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Datos del Usuario
        ''' </summary>
        ''' <value>GuardarDatosDocumento.Usuario</value>
        ''' <returns>GuardarDatosDocumento.Usuario</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
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
        ''' Movimiento
        ''' </summary>
        ''' <value>Movimiento</value>
        ''' <returns>Movimiento</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Movimiento() As Movimiento
            Get
                Return _Movimiento
            End Get
            Set(value As Movimiento)
                _Movimiento = value
            End Set
        End Property

#End Region

    End Class

End Namespace