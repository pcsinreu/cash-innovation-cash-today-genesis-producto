Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace IngresoRemesasNuevo

    ''' <summary>
    ''' Classe Remesa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama]  27/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:IngresoRemesasNuevo")> _
    <XmlRoot(Namespace:="urn:IngresoRemesasNuevo")> _
    <Serializable()> _
    Public Class Remesa

#Region "[VARIÁVEIS]"

        Private _CodigoDelegacion As String
        Private _CodigoPlanta As String
        Private _CodigoSector As String
        Private _CodigoSectorDestino As String
        Private _CodigoCajero As String
        Private _DescripcionSector As String
        Private _DescripcionSectorDestino As String
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
        Private _DeclaradosMedioPagoRemesa As List(Of DeclaradoMedioPagoRemesa)
        Private _CamposExtra As CamposExtras
        Private _Bultos As Bultos
        Private _ValoresRemesa As ValoresRemesa
        Private _TipoMercancia As String
        Private _TipoMercanciaCodigo As String
        Private _NoEntregue As Boolean
        Private _Identificador As String

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

        Public Property CodigoPlanta() As String
            Get
                Return _CodigoPlanta
            End Get
            Set(value As String)
                _CodigoPlanta = value
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

        Public Property DescripcionSector() As String
            Get
                Return _DescripcionSector
            End Get
            Set(value As String)
                _DescripcionSector = value
            End Set
        End Property

        Public Property DescripcionSectorDestino() As String
            Get
                Return _DescripcionSectorDestino
            End Get
            Set(value As String)
                _DescripcionSectorDestino = value
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

        Public Property DeclaradosMedioPagoRemesa() As List(Of DeclaradoMedioPagoRemesa)
            Get
                Return _DeclaradosMedioPagoRemesa
            End Get
            Set(value As List(Of DeclaradoMedioPagoRemesa))
                _DeclaradosMedioPagoRemesa = value
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

        Public Property TipoMercancia() As String
            Get
                Return _TipoMercancia
            End Get
            Set(value As String)
                _TipoMercancia = value
            End Set
        End Property

        Public Property TipoMercanciaCodigo As String
            Get
                Return _TipoMercanciaCodigo
            End Get
            Set(value As String)
                _TipoMercanciaCodigo = value
            End Set
        End Property

        Public Property NoEntregue As Boolean
            Get
                Return _NoEntregue
            End Get
            Set(value As Boolean)
                _NoEntregue = value
            End Set
        End Property

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property

        Public Property CodigoClienteSaldoSeleccionado As String
        Public Property DescripcionClienteSaldoSeleccionado As String
        Public Property CodigoSubClienteSaldoSeleccionado As String
        Public Property DescripcionSubClienteSaldoSeleccionado As String
        Public Property CodigoPtoServicioSaldoSeleccionado As String
        Public Property DescripcionPtoServicioSaldoSeleccionado As String

        Public Property UltDocRelaccionadoSalidaRecorido As Boolean

        Public Property TuveUnSalidaRecorrido As Boolean
        Public Property IdentificadorOT As String
        'indicia que a remessa ja está vinculada a alguma OT, para posteriormente validar se ela poderá ser excluida da lista em memoria na tela de Recepção de Rota
        Public Property pertenenceOT As Boolean
#End Region

#Region "[METODOS]"

        Public Function RecuperarValorCampoExtra(nombreCampoExtra As String) As Object

            ' Verifica se existe a coleção de campos extras
            If _CamposExtra IsNot Nothing Then

                ' Recupera o campo extra
                Dim objCE As CampoExtra = _CamposExtra.Where(Function(ce) ce.Nombre = nombreCampoExtra).FirstOrDefault()

                ' Se encontou o campo extra
                If objCE IsNot Nothing Then

                    ' Retorna o valor do campo extra
                    Return objCE.Valor

                End If

            End If

            Return Nothing

        End Function

        Public Sub GrabarCampoExtra(nombreCampoExtra As String, valorCampoExtra As String)

            ' Verifica se existe a coleção de campos extras
            If Me._CamposExtra Is Nothing Then
                Me._CamposExtra = New CamposExtras
            End If

            ' Recupera o campo extra
            Dim objCE As CampoExtra = Me._CamposExtra.Where(Function(ce) ce.Nombre = nombreCampoExtra).FirstOrDefault()

            ' Se encontou o campo extra
            If objCE IsNot Nothing Then
                ' Atualiza o valor
                objCE.Valor = valorCampoExtra
            Else
                ' Adiciona o novo campo extra
                Me._CamposExtra.Add(New CampoExtra With {.Nombre = nombreCampoExtra, .Valor = valorCampoExtra})
            End If

        End Sub

#End Region

    End Class

End Namespace