Imports System.Xml.Serialization
Imports System.Xml

Namespace CreacionMifIntersector

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [abueno] 23/07/2010 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:CreacionMifIntersector")> _
    <XmlRoot(Namespace:="urn:CreacionMifIntersector")> _
    <Serializable()> _
    Public Class Peticion

#Region "[INICIALIZAÇÃO]"

        Sub New()
            Usuario = New GuardarDatosDocumento.Usuario
            Movimentacion = New Movimentacion

        End Sub

#End Region

#Region "[VARIAVEIS]"

        Private _Usuario As GuardarDatosDocumento.Usuario
        Private _Movimentacion As Movimentacion

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Propriedad Usuario
        ''' </summary>
        ''' <value>GuardarDatosDocumento.Usuario</value>
        ''' <returns>GuardarDatosDocumento.Usuario</returns>
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
        ''' Propriedad Movimentacion
        ''' </summary>
        ''' <value>Movimentacion</value>
        ''' <returns>Movimentacion</returns>
        ''' <remarks></remarks>
        Public Property Movimentacion() As Movimentacion
            Get
                Return _Movimentacion
            End Get
            Set(value As Movimentacion)
                _Movimentacion = value
            End Set
        End Property


#End Region

    End Class

End Namespace