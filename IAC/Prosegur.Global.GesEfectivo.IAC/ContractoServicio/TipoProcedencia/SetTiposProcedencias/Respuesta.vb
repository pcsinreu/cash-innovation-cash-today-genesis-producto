Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoProcedencia.SetTiposProcedencias

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 14/05/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetTiposProcedencia")> _
    <XmlRoot(Namespace:="urn:SetTiposProcedencia")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _codTipoProcedencia As String
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property codTipoProcedencia() As String
            Get
                Return _codTipoProcedencia
            End Get
            Set(value As String)
                _codTipoProcedencia = value
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
