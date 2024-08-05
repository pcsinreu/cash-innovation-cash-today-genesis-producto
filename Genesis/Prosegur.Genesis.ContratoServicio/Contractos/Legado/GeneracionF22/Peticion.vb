Imports System.Xml.Serialization
Imports System.Xml

Namespace Legado.GeneracionF22

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [abueno] 13/07/2010 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GeneracionF22")> _
    <XmlRoot(Namespace:="urn:GeneracionF22")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _Usuario As Usuario
        Private _Remesas As RemesaColeccion


#End Region


#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Propriedad Usuario
        ''' </summary>
        ''' <value>GuardarDatosDocumento.Usuario</value>
        ''' <returns>GuardarDatosDocumento.Usuario</returns>
        ''' <remarks></remarks>
        Public Property Usuario() As Usuario
            Get
                Return _Usuario
            End Get
            Set(value As Usuario)
                _Usuario = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad Remesa
        ''' </summary>
        ''' <value>RemesaColeccion</value>
        ''' <returns>RemesaColeccion</returns>
        ''' <remarks></remarks>
        Public Property Remesas() As RemesaColeccion
            Get
                Return _Remesas
            End Get
            Set(value As RemesaColeccion)
                _Remesas = value
            End Set
        End Property


#End Region

    End Class

End Namespace