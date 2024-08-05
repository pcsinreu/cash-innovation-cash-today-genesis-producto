Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis

Namespace TipoPlanificacion.GetTiposPlanificaciones

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetTiposPlanificaciones")> _
    <XmlRoot(Namespace:="urn:GetTiposPlanificaciones")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _Identificador As String
        Private _codTipoPlanificacion As String
        Private _desTipoPlanificacion As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property

        Public Property DesTipoPlanificacion() As String
            Get
                Return _desTipoPlanificacion
            End Get
            Set(value As String)
                _desTipoPlanificacion = value
            End Set
        End Property

        Public Property CodTipoPlanificacion() As String
            Get
                Return _codTipoPlanificacion
            End Get
            Set(value As String)
                _codTipoPlanificacion = value
            End Set
        End Property

#End Region
    End Class
End Namespace
