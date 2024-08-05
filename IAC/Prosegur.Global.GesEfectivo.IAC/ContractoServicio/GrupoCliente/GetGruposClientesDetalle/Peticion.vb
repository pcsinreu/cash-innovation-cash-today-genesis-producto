Imports System.Xml.Serialization
Imports System.Xml

Namespace GrupoCliente.GetGruposClientesDetalle

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 24/10/2012 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetGruposClientesDetalle")> _
    <XmlRoot(Namespace:="urn:GetGruposClientesDetalle")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIÁVEIS]"

        Private _Codigo As List(Of String)

#End Region


#Region "[PROPRIEDADES]"

        Public Property Codigo As List(Of String)
            Get
                Return _Codigo
            End Get
            Set(value As List(Of String))
                _Codigo = value
            End Set
        End Property

#End Region

    End Class

End Namespace

