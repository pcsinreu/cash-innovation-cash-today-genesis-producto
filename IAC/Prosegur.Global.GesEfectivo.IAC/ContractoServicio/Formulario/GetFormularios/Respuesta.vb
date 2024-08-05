Imports System.Xml.Serialization
Imports System.Xml

Namespace Formulario.GetFormularios

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetFormularios")>
    <XmlRoot(Namespace:="urn:GetFormularios")>
    <Serializable()>
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Formularios As FormularioColeccion

#End Region

#Region "[Propriedades]"

        Public Property Formularios() As FormularioColeccion
            Get
                Return _Formularios
            End Get
            Set(value As FormularioColeccion)
                _Formularios = value
            End Set
        End Property

#End Region

    End Class

End Namespace