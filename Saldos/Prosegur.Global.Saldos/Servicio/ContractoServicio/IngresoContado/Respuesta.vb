Imports System.Xml.Serialization
Imports System.Xml

Namespace IngresoContado

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 03/08/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:IngresoContado")> _
    <XmlRoot(Namespace:="urn:IngresoContado")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Resultado As Integer

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Resultado() As Integer
            Get
                Return _Resultado
            End Get
            Set(value As Integer)
                _Resultado = value
            End Set
        End Property

#End Region

    End Class

End Namespace