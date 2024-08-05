Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSetor.GetTiposSectores

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/03/2013 Criado
    ''' </history>
    <XmlType(Namespace:="GetTiposSectores")> _
    <XmlRoot(Namespace:="GetTiposSectores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVÉIS]"

        Private _tipoSetor As TipoSetorColeccion
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property TipoSetor() As TipoSetorColeccion
            Get
                Return _tipoSetor
            End Get
            Set(value As TipoSetorColeccion)
                _tipoSetor = value
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
