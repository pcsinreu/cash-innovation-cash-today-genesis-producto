Imports System.Xml.Serialization
Imports System.Xml

Namespace GrupoCliente.GetGruposClientesDetalle

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 24/10/2012 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetGruposClientesDetalle")> _
    <XmlRoot(Namespace:="urn:GetGruposClientesDetalle")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _GrupoCliente As GrupoClienteDetalleColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property GrupoCliente As GrupoClienteDetalleColeccion
            Get
                Return _GrupoCliente
            End Get
            Set(value As GrupoClienteDetalleColeccion)
                _GrupoCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace
