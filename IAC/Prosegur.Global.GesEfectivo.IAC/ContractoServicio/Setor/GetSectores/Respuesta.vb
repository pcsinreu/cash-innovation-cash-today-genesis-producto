Imports System.Xml.Serialization
Imports System.Xml

Namespace Setor.GetSectores

    ''' <sumary>
    ''' Classe Repuesta
    ''' </sumary>
    ''' <remarks></remarks>
    ''' <history>
    ''' pgoncalves 08/03/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetSectores")> _
    <XmlRoot(Namespace:="urn:GetSectores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVEIS]"

        Private _setor As SetorColeccion
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Setor() As SetorColeccion
            Get
                Return _setor
            End Get
            Set(value As SetorColeccion)
                _setor = value
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


