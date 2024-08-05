Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarCentroProceso

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 29/04/2011 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarCentroProceso")> _
    <XmlRoot(Namespace:="urn:RecuperarCentroProceso")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Resultado As Integer
        Private _CodigoDelegacion As String
        Private _DescripcionPlanta As String
        Private _IDPSPlanta As String
        Private _CentroProcesoCol As CentroProcesoColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Resultado() As Integer
            Get
                Return _Resultado
            End Get
            Set(value As Integer)
                _Resultado = value
            End Set
        End Property


        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property DescripcionPlanta() As String
            Get
                Return _DescripcionPlanta
            End Get
            Set(value As String)
                _DescripcionPlanta = value
            End Set
        End Property

        Public Property IDPSPlanta() As String
            Get
                Return _IDPSPlanta
            End Get
            Set(value As String)
                _IDPSPlanta = value
            End Set
        End Property

        Public Property CentroProcesoColeccion() As CentroProcesoColeccion
            Get
                Return _CentroProcesoCol
            End Get
            Set(value As CentroProcesoColeccion)
                _CentroProcesoCol = value
            End Set
        End Property

#End Region

    End Class
End Namespace