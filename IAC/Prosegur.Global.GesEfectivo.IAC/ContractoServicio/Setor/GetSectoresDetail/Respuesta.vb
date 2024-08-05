Imports System.Xml.Serialization
Imports System.Xml

Namespace Setor.GetSectoresDetail

    ''' <sumary>
    ''' Classe Repuesta
    ''' </sumary>
    ''' <remarks></remarks>
    ''' <history>
    ''' pgoncalves 08/03/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetSectoresDetail")> _
    <XmlRoot(Namespace:="urn:GetSectoresDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVEIS]"

        Private _sector As Sector
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Sector() As Sector
            Get
                Return _sector
            End Get
            Set(value As Sector)
                _sector = value
            End Set
        End Property

        Public Property Resultado() As String
            Get
                Return _Resultado
            End Get
            Set(value As String)
                _Resultado = value
            End Set
        End Property

#End Region

    End Class
End Namespace


