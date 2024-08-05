Imports System.Xml.Serialization
Imports System.Xml

Namespace GrupoCliente.SetGruposCliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 24/10/2012 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetGruposCliente")> _
    <XmlRoot(Namespace:="urn:SetGruposCliente")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _GrupoCliente As GrupoClienteDetalle

#End Region

#Region "[PROPRIEDADES]"

        Public Property GrupoCliente As GrupoClienteDetalle
            Get
                Return _GrupoCliente
            End Get
            Set(value As GrupoClienteDetalle)
                _GrupoCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace
