Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboNivelesParametros

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboNivelesParametros")> _
    <XmlRoot(Namespace:="urn:GetComboNivelesParametros")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _NivelesParametros As NivelParametroColeccion

#End Region

#Region "[PROPRIEDADE]"

        Public Property NivelesParametros() As NivelParametroColeccion
            Get
                Return _NivelesParametros
            End Get
            Set(value As NivelParametroColeccion)
                _NivelesParametros = value
            End Set
        End Property
#End Region

    End Class
End Namespace