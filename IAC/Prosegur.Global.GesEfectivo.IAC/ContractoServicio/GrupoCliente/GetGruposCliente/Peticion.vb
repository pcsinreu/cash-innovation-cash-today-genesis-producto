Imports System.Xml.Serialization
Imports System.Xml

Namespace GrupoCliente.GetGruposCliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 24/10/2012 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetGruposCliente")> _
    <XmlRoot(Namespace:="urn:GetGruposCliente")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIÁVEIS]"

        Private _GrupoCliente As GrupoCliente

#End Region

#Region "[PROPRIEDADES]"

        Public Property GrupoCliente As GrupoCliente
            Get
                Return _GrupoCliente
            End Get
            Set(value As GrupoCliente)
                _GrupoCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace
