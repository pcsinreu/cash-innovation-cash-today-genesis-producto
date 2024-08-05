Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Maquina.GetMaquinaDetalle

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetMaquinaDetalle")> _
    <XmlRoot(Namespace:="urn:GetMaquinaDetalle")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Maquina As Maquina

#End Region

#Region "Propriedades"

        Public Property Maquina() As Maquina
            Get
                Return _Maquina
            End Get
            Set(value As Maquina)
                _Maquina = value
            End Set
        End Property

#End Region

    End Class
End Namespace

