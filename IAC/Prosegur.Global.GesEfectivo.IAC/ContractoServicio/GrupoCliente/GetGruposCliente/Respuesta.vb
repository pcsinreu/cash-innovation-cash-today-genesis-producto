Imports System.Xml.Serialization
Imports System.Xml

Namespace GrupoCliente.GetGruposCliente


    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 24/10/2012 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetGruposCliente")> _
    <XmlRoot(Namespace:="urn:GetGruposCliente")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIÁVEIS]"

        Private _GruposCliente As GrupoClienteColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property GruposCliente As GrupoClienteColeccion
            Get
                Return _GruposCliente
            End Get
            Set(value As GrupoClienteColeccion)
                _GruposCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace
