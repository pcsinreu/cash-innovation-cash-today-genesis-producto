Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoProcedencia.GetTiposProcedencias
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>SS
    ''' [danielnunes] 14/05/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetTiposProcedencia")> _
    <XmlRoot(Namespace:="urn:GetTiposProcedencia")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVEIS]"

        Private _TipoProcedencia As TipoProcedenciaColeccion
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property TipoProcedencia() As TipoProcedenciaColeccion
            Get
                Return _TipoProcedencia
            End Get
            Set(value As TipoProcedenciaColeccion)
                _TipoProcedencia = value
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

