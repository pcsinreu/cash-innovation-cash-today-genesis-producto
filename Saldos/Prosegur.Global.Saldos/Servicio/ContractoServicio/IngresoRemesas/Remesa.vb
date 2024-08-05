Imports System.Xml.Serialization

Namespace IngresoRemesas

    ''' <summary>
    ''' Classe Remesa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama]  27/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:IngresoRemesas")> _
    <XmlRoot(Namespace:="urn:IngresoRemesas")> _
    <Serializable()> _
    Public Class Remesa

#Region "[VARIÁVEIS]"

        Private _CodigoDelegacion As String
        Private _CodigoSector As String
        Private _CodigoSectorDestino As String
        Private _CodigoCajero As String
        Private _NumeroExterno As String
        Private _IdRemesaOrigen As String
        Private _CodigoEstado As String
        Private _CodigoPrecinto As String
        Private _EsInterno As Boolean
        Private _NumBultos As Integer = 0
        Private _UtilizarReglaAutomata As Nullable(Of Boolean)
        Private _DeclaradosTotalRemesa As DeclaradosTotalRemesa
        Private _DeclaradosDetalleRemesa As DeclaradosDetalleRemesa
        Private _DeclaradosAgrupacionRemesa As DeclaradosAgrupacionRemesa
        Private _CamposExtra As CamposExtras
        Private _Bultos As Bultos
        Private _ValoresRemesa As ValoresRemesa

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property CodigoSector() As String
            Get
                Return _CodigoSector
            End Get
            Set(value As String)
                _CodigoSector = value
            End Set
        End Property

        Public Property CodigoSectorDestino() As String
            Get
                Return _CodigoSectorDestino
            End Get
            Set(value As String)
                _CodigoSectorDestino = value
            End Set
        End Property

        Public Property CodigoCajero As String
            Get
                Return _CodigoCajero
            End Get
            Set(value As String)
                _CodigoCajero = value
            End Set
        End Property

        Public Property NumeroExterno() As String
            Get
                Return _NumeroExterno
            End Get
            Set(value As String)
                _NumeroExterno = value
            End Set
        End Property

        Public Property IdRemesaOrigen() As String
            Get
                Return _IdRemesaOrigen
            End Get
            Set(value As String)
                _IdRemesaOrigen = value
            End Set
        End Property

        Public Property CodigoEstado() As String
            Get
                Return _CodigoEstado
            End Get
            Set(value As String)
                _CodigoEstado = value
            End Set
        End Property

        Public Property CodigoPrecinto() As String
            Get
                Return _CodigoPrecinto
            End Get
            Set(value As String)
                _CodigoPrecinto = value
            End Set
        End Property

        Public Property UtilizarReglaAutomata() As Nullable(Of Boolean)
            Get
                Return _UtilizarReglaAutomata
            End Get
            Set(value As Nullable(Of Boolean))
                _UtilizarReglaAutomata = value
            End Set
        End Property

        Public Property EsInterno() As Boolean
            Get
                Return _EsInterno
            End Get
            Set(value As Boolean)
                _EsInterno = value
            End Set
        End Property

        Public Property DeclaradosTotalRemesa() As DeclaradosTotalRemesa
            Get
                Return _DeclaradosTotalRemesa
            End Get
            Set(value As DeclaradosTotalRemesa)
                _DeclaradosTotalRemesa = value
            End Set
        End Property

        Public Property DeclaradosDetalleRemesa() As DeclaradosDetalleRemesa
            Get
                Return _DeclaradosDetalleRemesa
            End Get
            Set(value As DeclaradosDetalleRemesa)
                _DeclaradosDetalleRemesa = value
            End Set
        End Property

        Public Property DeclaradosAgrupacionRemesa() As DeclaradosAgrupacionRemesa
            Get
                Return _DeclaradosAgrupacionRemesa
            End Get
            Set(value As DeclaradosAgrupacionRemesa)
                _DeclaradosAgrupacionRemesa = value
            End Set
        End Property

        Public Property Bultos() As Bultos
            Get
                Return _Bultos
            End Get
            Set(value As Bultos)
                _Bultos = value
            End Set
        End Property

        Public Property CamposExtra() As CamposExtras
            Get
                Return _CamposExtra
            End Get
            Set(value As CamposExtras)
                _CamposExtra = value
            End Set
        End Property


        Public Property NumBultos() As Integer
            Get
                Return _NumBultos
            End Get
            Set(value As Integer)
                _NumBultos = value
            End Set
        End Property

        Public Property ValoresRemesa() As ValoresRemesa
            Get
                Return _ValoresRemesa
            End Get
            Set(value As ValoresRemesa)
                _ValoresRemesa = value
            End Set
        End Property

#End Region

    End Class

End Namespace