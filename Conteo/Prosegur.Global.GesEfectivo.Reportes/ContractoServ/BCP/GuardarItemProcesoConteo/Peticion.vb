Imports System.Xml.Serialization
Imports System.Xml

Namespace bcp.GuardarItemProcesoConteo

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/07/2012 - Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:GuardarItemProcesoConteo")> _
    <XmlRoot(Namespace:="urn:GuardarItemProcesoConteo")> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _CodProceso As String

        '_Parametros tem a seguinte forma:
        'Especies & "|" & Depositos & "|" & FechaConteoDesde & "|" & FechaConteoHasta
        '0|1;2|07/07/2012 00:00|09/07/2012 23:59
        Private _Parametros As String
        Private _FechaCreacion As Date
        Private _CodigoUsuario As String
        Private _CodDelegacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodProceso() As String
            Get
                Return _CodProceso
            End Get
            Set(value As String)
                _CodProceso = value
            End Set
        End Property

        Public Property Parametros() As String
            Get
                Return _Parametros
            End Get
            Set(value As String)
                _Parametros = value
            End Set
        End Property

        Public Property FechaCreacion() As Date
            Get
                Return _FechaCreacion
            End Get
            Set(value As Date)
                _FechaCreacion = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

#End Region

    End Class
End Namespace