Imports Prosegur.Genesis.Comon.Enumeradores
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Remesa.vb
    '  Descripción: Clase definición Remesa
    ' ***********************************************************************
    <Serializable()>
    Public Class Remesa
        Inherits Elemento

        Friend Clonar As Remesa

#Region "[VARIAVEIS]"

        Private _RemesaOrigen As Remesa
        Private _Estado As Enumeradores.EstadoRemesa
        Private _CantidadBultos As Long = 0
        Private _FechaHoraSalida As DateTime
        Private _FechaServicio As DateTime
        Private _FechaHoraProceso As DateTime
        Private _FechaHoraFinArmado As DateTime
        Private _FechaHoraInicioArmado As DateTime
        Private _FechaHoraFinConteo As DateTime
        Private _FechaHoraInicioConteo As DateTime
        Private _Parada As Int64?
        Private _Bultos As New ObservableCollection(Of Bulto)
        Private _Objetos As New ObservableCollection(Of Objeto)
        Private _CodigoDelegacion As String
        Private _ClienteFacturacion As Cliente
        Private _ClienteSaldo As Cliente
        Private _CodigoEmpresaTransporte As String
        Private _TiposMercancia As ObservableCollection(Of TipoMercancia)
        Private _CodigoCajaCentralizada As String
        Private _DescripcionDireccionEntrega As String
        Private _DescripcionLocalidadEntrega As String
        Private _NumeroPedidoLegado As Integer
        Private _NumeroControleLegado As String
        Private _ReciboTransporte As String
        Private _Ruta As String
        Private _TipoServicio As Nullable(Of TipoServico)
        Private _DescripcionComentario As String
        Private _CodigoReciboSalida As String
        Private _CodigoExterno As String
        Private _FechaHoraTransporte As DateTime
        Private _TrabajaPorBulto As Boolean
        Private _EsRemesaATM As Boolean
        Private _DatosATM As ATM
        Private _Modulos As ObservableCollection(Of Clases.Modulo)
        Private _IdentificadorRemesaPadre As String
        Private _IdentificadorOT As String
        Private _CodigoServicioContratado As String
        Private _CodigoSecuencia As String
        Private _FyhInicio As DateTime
        Private _EstaAnulado As Boolean
        Private _EsMedioPago As Boolean
        Private _NoEntregue As Boolean
        Private _ConfiguracionNivelSaldos As Enumeradores.ConfiguracionNivelSaldos
        Private _ListoTrabajo As Boolean

        Private _descripcionContacto1 As String
        Private _descripcionContacto2 As String
        Private _descripcionContacto3 As String
        Private _descripcionContacto4 As String
        Private _fechaHoraPreparacion As DateTime

        Private _ultimoDocRelaccionadoSalidaRecorido As Boolean
        Private _tuveSalidaRecorido As Boolean
        Private _IdentificadorRemesaModificada As String
        Private _EsRemesaModificada As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property EsRemesaModificada As Boolean
            Get
                Return _EsRemesaModificada
            End Get
            Set(value As Boolean)
                SetProperty(_EsRemesaModificada, value, "EsRemesaModificada")
            End Set
        End Property

        Public Property ListoTrabajo As Boolean
            Get
                Return _ListoTrabajo
            End Get
            Set(value As Boolean)
                SetProperty(_ListoTrabajo, value, "ListoTrabajo")
            End Set
        End Property

        Public Property Modulos As ObservableCollection(Of Clases.Modulo)
            Get
                Return _Modulos
            End Get
            Set(value As ObservableCollection(Of Clases.Modulo))
                SetProperty(_Modulos, value, "Modulos")
            End Set
        End Property

        Public Property DatosATM As ATM
            Get
                Return _DatosATM
            End Get
            Set(value As ATM)
                SetProperty(_DatosATM, value, "DatosATM")
            End Set
        End Property

        Public Property TrabajaPorBulto As Boolean
            Get
                Return _TrabajaPorBulto
            End Get
            Set(value As Boolean)
                SetProperty(_TrabajaPorBulto, value, "TrabajaPorBulto")
            End Set
        End Property

        Public Property EsRemesaATM As Boolean
            Get
                Return _EsRemesaATM
            End Get
            Set(value As Boolean)
                SetProperty(_EsRemesaATM, value, "EsRemesaATM")
            End Set
        End Property

        Public Property TiposMercancia As ObservableCollection(Of TipoMercancia)
            Get
                Return _TiposMercancia
            End Get
            Set(value As ObservableCollection(Of TipoMercancia))
                SetProperty(_TiposMercancia, value, "TipoMercancia")
            End Set
        End Property

        Public Property Estado As Enumeradores.EstadoRemesa
            Get
                Return _Estado
            End Get
            Set(value As Enumeradores.EstadoRemesa)
                SetProperty(_Estado, value, "Estado")
            End Set
        End Property

        Public Property FechaHoraSalida As DateTime
            Get
                Return _FechaHoraSalida
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraSalida, value, "FechaHoraSalida")
            End Set
        End Property
        Public Property FechaServicio As DateTime
            Get
                Return _FechaServicio
            End Get
            Set(value As DateTime)
                SetProperty(_FechaServicio, value, "FechaServicio")
            End Set
        End Property

        Public Property FechaHoraProceso As DateTime
            Get
                Return _FechaHoraProceso
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraProceso, value, "FechaHoraProceso")
            End Set
        End Property
        Public Property FechaHoraFinArmado As DateTime
            Get
                Return _FechaHoraFinArmado
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraFinArmado, value, "FechaHoraFinArmado")
            End Set
        End Property
        Public Property FechaHoraInicioArmado As DateTime
            Get
                Return _FechaHoraInicioArmado
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraInicioArmado, value, "FechaHoraInicioArmado")
            End Set
        End Property

        Public Property FechaHoraFinConteo As DateTime
            Get
                Return _FechaHoraFinConteo
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraFinConteo, value, "FechaHoraFinConteo")
            End Set
        End Property

        Public Property FechaHoraInicioConteo As DateTime
            Get
                Return _FechaHoraInicioConteo
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraInicioConteo, value, "FechaHoraInicioConteo")
            End Set
        End Property

        Public Property Parada As Int64?
            Get
                Return _Parada
            End Get
            Set(value As Int64?)
                SetProperty(_Parada, value, "Parada")
            End Set
        End Property

        Public Property ClienteFacturacion As Cliente
            Get
                Return _ClienteFacturacion
            End Get
            Set(value As Cliente)
                SetProperty(_ClienteFacturacion, value, "ClienteFacturacion")
            End Set
        End Property
        ''' <summary>
        ''' Cliente totalizador de saldo do cliente destino
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ClienteSaldo As Cliente
            Get
                Return _ClienteSaldo
            End Get
            Set(value As Cliente)
                SetProperty(_ClienteSaldo, value, "ClienteSaldo")
            End Set
        End Property

        Public Property CodigoCajaCentralizada As String
            Get
                Return _CodigoCajaCentralizada
            End Get
            Set(value As String)
                SetProperty(_CodigoCajaCentralizada, value, "CodigoCajaCentralizada")
            End Set
        End Property

        Public Property CodigoDelegacion As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                SetProperty(_CodigoDelegacion, value, "IdentificadorLote")
            End Set
        End Property

        Public Property CodigoEmpresaTransporte As String
            Get
                Return _CodigoEmpresaTransporte
            End Get
            Set(value As String)
                SetProperty(_CodigoEmpresaTransporte, value, "CodigoEmpresaTransporte")
            End Set
        End Property

        Public Property CodigoExterno As String
            Get
                Return _CodigoExterno
            End Get
            Set(value As String)
                SetProperty(_CodigoExterno, value, "CodigoExterno")
            End Set
        End Property

        Public Property CodigoReciboSalida As String
            Get
                Return _CodigoReciboSalida
            End Get
            Set(value As String)
                SetProperty(_CodigoReciboSalida, value, "CodigoReciboSalida")
            End Set
        End Property

        Public Property FechaHoraTransporte As DateTime
            Get
                Return _FechaHoraTransporte
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraTransporte, value, "FechaHoraTransporte")
            End Set
        End Property

        Public Property DescripcionDireccionEntrega As String
            Get
                Return _DescripcionDireccionEntrega
            End Get
            Set(value As String)
                SetProperty(_DescripcionDireccionEntrega, value, "DescripcionDireccionEntrega")
            End Set
        End Property

        Public Property DescripcionLocalidadEntrega As String
            Get
                Return _DescripcionLocalidadEntrega
            End Get
            Set(value As String)
                SetProperty(_DescripcionLocalidadEntrega, value, "DescripcionLocalidadEntrega")
            End Set
        End Property

        Public Property DescripcionComentario As String
            Get
                Return _DescripcionComentario
            End Get
            Set(value As String)
                SetProperty(_DescripcionComentario, value, "DescrpcionComentario")
            End Set
        End Property

        Public Property NumeroControleLegado As String
            Get
                Return _NumeroControleLegado
            End Get
            Set(value As String)
                SetProperty(_NumeroControleLegado, value, "NumeroControleLegado")
            End Set
        End Property

        Public Property NumeroPedidoLegado As Integer
            Get
                Return _NumeroPedidoLegado
            End Get
            Set(value As Integer)
                SetProperty(_NumeroPedidoLegado, value, "NumeroPedidoLegado")
            End Set
        End Property

        Public Property ReciboTransporte As String
            Get
                Return _ReciboTransporte
            End Get
            Set(value As String)
                SetProperty(_ReciboTransporte, value, "ReciboTransporte")
            End Set
        End Property

        Public Property CantidadBultos As Long
            Get
                ' Se a quantidade de parciais não foi informada
                If _CantidadBultos = 0 Then
                    ' Verifica se existem malotes
                    If Me.Bultos IsNot Nothing AndAlso Me.Bultos.Count > 0 Then
                        ' Atualiza a quantidade de malotes de acordo com os malotes existentes
                        _CantidadBultos = Me.Bultos.Count
                    End If
                End If
                ' Retorna a quantidade de malotes
                Return _CantidadBultos
            End Get
            Set(value As Long)
                SetProperty(_CantidadBultos, value, "CantidadBultos")
            End Set
        End Property

        Public Property Bultos As ObservableCollection(Of Bulto)
            Get
                Return _Bultos
            End Get
            Set(value As ObservableCollection(Of Bulto))
                SetProperty(_Bultos, value, "Bultos")
            End Set
        End Property

        Public Property Objetos As ObservableCollection(Of Objeto)
            Get
                Return _Objetos
            End Get
            Set(value As ObservableCollection(Of Objeto))
                SetProperty(_Objetos, value, "Objetos")
            End Set
        End Property

        Public Property RemesaOrigen As Remesa
            Get
                Return _RemesaOrigen
            End Get
            Set(value As Remesa)
                SetProperty(_RemesaOrigen, value, "RemesaOrigen")
            End Set
        End Property

        Public Property Ruta As String
            Get
                Return _Ruta
            End Get
            Set(value As String)
                SetProperty(_Ruta, value, "Ruta")
            End Set
        End Property

        Public Property TipoServicio As Nullable(Of TipoServico)
            Get
                Return _TipoServicio
            End Get
            Set(value As Nullable(Of TipoServico))
                SetProperty(_TipoServicio, value, "TipoServicio")
            End Set
        End Property

        Public Property IdentificadorRemesaPadre As String
            Get
                Return _IdentificadorRemesaPadre
            End Get
            Set(value As String)
                SetProperty(_IdentificadorRemesaPadre, value, "IdentificadorRemesaPadre")
            End Set
        End Property

        Public Property IdentificadorOT As String
            Get
                Return _IdentificadorOT
            End Get
            Set(value As String)
                SetProperty(_IdentificadorOT, value, "IdentificadorOT")
            End Set
        End Property

        Public Property CodigoServicioContratado As String
            Get
                Return _CodigoServicioContratado
            End Get
            Set(value As String)
                SetProperty(_CodigoServicioContratado, value, "CodigoServicioContratado")
            End Set
        End Property

        Public Property CodigoSecuencia As String
            Get
                Return _CodigoSecuencia
            End Get
            Set(value As String)
                SetProperty(_CodigoSecuencia, value, "CodigoSecuencia")
            End Set
        End Property

        Public Property FyhInicio As DateTime
            Get
                Return _FyhInicio
            End Get
            Set(value As DateTime)
                SetProperty(_FyhInicio, value, "FyhInicio")
            End Set
        End Property

        Public Property EstaAnulado As Boolean
            Get
                Return _EstaAnulado
            End Get
            Set(value As Boolean)
                SetProperty(_EstaAnulado, value, "EstaAnulado")
            End Set
        End Property

        Public Property EsMedioPago As Boolean
            Get
                Return _EsMedioPago
            End Get
            Set(value As Boolean)
                SetProperty(_EsMedioPago, value, "EsMedioPago")
            End Set
        End Property

        ''' <summary>
        ''' Valida se o importe da remesa está batendo com o importe do bulto.
        ''' Está validação é valida somente para o modulo de salidas.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ImporteRemesaOk As Boolean

            Get

                Dim ImporteRemesa As New ObservableCollection(Of Clases.Divisa)
                Dim ImporteBultos As List(Of Tuple(Of Decimal, Clases.Divisa))

                If Me.EsMedioPago Then
                    If Me.Bultos Is Nothing OrElse Me.Bultos.Count = 0 Then
                        Return False
                    Else
                        Return True
                    End If

                End If

                If Me.Divisas IsNot Nothing AndAlso Me.Divisas.Count > 0 Then

                    ImporteRemesa.AddRange(Me.Divisas.Clonar)

                    For Each impRem In ImporteRemesa.Where(Function(ir) ir.ValoresTotalesEfectivo Is Nothing OrElse ir.ValoresTotalesEfectivo.Count = 0).ToList

                        If impRem.Denominaciones IsNot Nothing AndAlso impRem.Denominaciones.Count > 0 Then

                            Dim valorTotal = (From d In impRem.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Count > 0)
                                              From v In d.ValorDenominacion
                                              Select v.Importe).Sum

                            Dim valores As New ValorEfectivo With
                                {
                                    .TipoValor = Enumeradores.TipoValor.Declarado,
                                    .TipoDetalleEfectivo = TipoDetalleEfectivo.NoDefinido,
                                    .InformadoPor = TipoContado.NoDefinido,
                                    .Importe = valorTotal
                                }
                            impRem.ValoresTotalesEfectivo = New ObservableCollection(Of ValorEfectivo) From {valores}

                        End If

                    Next

                End If

                If Me.Modulos IsNot Nothing AndAlso Me.Modulos.Count > 0 Then

                    For Each objModulo In Me.Modulos

                        If objModulo.Divisas IsNot Nothing AndAlso objModulo.Divisas.Count > 0 Then

                            Dim objImporte As Clases.Divisa

                            For Each objDiv In objModulo.Divisas.FindAll(Function(d) d.ValoresTotalesEfectivo IsNot Nothing AndAlso d.ValoresTotalesEfectivo.Count > 0)

                                objImporte = ImporteRemesa.Find(Function(i) i.CodigoISO = objDiv.CodigoISO)

                                If objImporte IsNot Nothing Then

                                    If objImporte.ValoresTotalesEfectivo IsNot Nothing AndAlso objImporte.ValoresTotalesEfectivo.Count > 0 Then
                                        objImporte.ValoresTotalesEfectivo.First.Importe += objDiv.ValoresTotalesEfectivo.Sum(Function(v) (v.Importe * objModulo.Cantidad))
                                    Else
                                        objImporte.ValoresTotalesEfectivo = New ObservableCollection(Of ValorEfectivo)
                                        objImporte.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With {
                                                                                  .Importe = objDiv.ValoresTotalesEfectivo.Sum(Function(v) (v.Importe * objModulo.Cantidad))})
                                    End If

                                Else
                                    ImporteRemesa.Add(New Clases.Divisa With { _
                                                      .Identificador = objDiv.Identificador, _
                                                      .CodigoISO = objDiv.CodigoISO, _
                                                      .Descripcion = objDiv.Descripcion, _
                                                      .ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)})

                                    ImporteRemesa.Last.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With {
                                                                                  .Importe = objDiv.ValoresTotalesEfectivo.Sum(Function(v) (v.Importe * objModulo.Cantidad))})

                                End If

                            Next

                        End If

                    Next

                End If

                ' se é remessa de ATM e quantidade de bultos vazios = quantidade de bultos existentes não é necessário validar valores. Remesa ficará no grid de remesas com bultos
                If Me.EsRemesaATM AndAlso Me.DatosATM IsNot Nothing AndAlso Me.DatosATM.ModalidadRecogida = ModalidadRecogida.EnBase AndAlso Me.Bultos IsNot Nothing AndAlso Me.Bultos.Count > 0 AndAlso
                   Me.Bultos.Where(Function(b) b.Divisas Is Nothing OrElse b.Divisas.Count = 0).Count = Me.Bultos.Count Then

                    Return True

                End If

                If Me.Bultos IsNot Nothing AndAlso Me.Bultos.Count > 0 AndAlso Me.Bultos.Exists(Function(b) b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0) Then

                    Dim ImpBulto As Clases.Divisa = Nothing
                    Dim objDenBulto As Clases.Denominacion = Nothing
                    Dim ImporteBultosRemesa As New ObservableCollection(Of Clases.Divisa)

                    For Each Bulto In Me.Bultos.FindAll(Function(b) b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0)
                        ImporteBultosRemesa.AddRange(Bulto.Divisas.Clonar)
                    Next

                    ImporteBultos = Comon.Util.RetornaTotalImporteDivisas(ImporteBultosRemesa, Enumeradores.TipoValor.Declarado, TipoNivelDetalhe.Detalhado)

                Else
                    Return False
                End If

                If ImporteRemesa IsNot Nothing AndAlso ImporteRemesa.Count > 0 Then

                    If ImporteBultos.Count = 0 Then Return False

                    Dim ImporteDivisaBulto As Tuple(Of Decimal, Clases.Divisa) = Nothing

                    For Each ImpRem In ImporteRemesa

                        ImporteDivisaBulto = ImporteBultos.Find(Function(ib) ib.Item2.CodigoISO = ImpRem.CodigoISO)

                        If ImporteDivisaBulto IsNot Nothing AndAlso ImpRem.ValoresTotalesEfectivo IsNot Nothing AndAlso ImpRem.ValoresTotalesEfectivo.Count > 0 Then

                            If ImporteDivisaBulto.Item1 <> ImpRem.ValoresTotalesEfectivo.Sum(Function(v) v.Importe) Then
                                Return False
                            End If

                        Else
                            Return False
                        End If

                    Next

                Else
                    Return False
                End If

                Return True
            End Get

        End Property

        Public Property NoEntregue As Boolean
            Get
                Return _NoEntregue
            End Get
            Set(value As Boolean)
                SetProperty(_NoEntregue, value, "NoEntregue")
            End Set
        End Property

        Public Property ConfiguracionNivelSaldos() As Enumeradores.ConfiguracionNivelSaldos
            Get
                Return _ConfiguracionNivelSaldos
            End Get
            Set(value As Enumeradores.ConfiguracionNivelSaldos)
                SetProperty(_ConfiguracionNivelSaldos, value, "ConfiguracionNivelSaldos")
            End Set
        End Property

        Public Property DescripcionContacto1() As String
            Get
                Return _descripcionContacto1
            End Get
            Set(value As String)
                SetProperty(_descripcionContacto1, value, "DescripcionContacto1")
            End Set
        End Property

        Public Property DescripcionContacto2() As String
            Get
                Return _descripcionContacto2
            End Get
            Set(value As String)
                SetProperty(_descripcionContacto2, value, "DescripcionContacto2")
            End Set
        End Property

        Public Property DescripcionContacto3() As String
            Get
                Return _descripcionContacto3
            End Get
            Set(value As String)
                SetProperty(_descripcionContacto3, value, "DescripcionContacto3")
            End Set
        End Property

        Public Property DescripcionContacto4() As String
            Get
                Return _descripcionContacto4
            End Get
            Set(value As String)
                SetProperty(_descripcionContacto4, value, "DescripcionContacto4")
            End Set
        End Property

        Public Property FechaHoraPreparacion As DateTime
            Get
                Return _fechaHoraPreparacion
            End Get
            Set(value As DateTime)
                SetProperty(_fechaHoraPreparacion, value, "FechaHoraPreparacion")
            End Set
        End Property

        Public Property UltimoDocRelaccionadoSalidaRecorido As Boolean
            Get
                Return _ultimoDocRelaccionadoSalidaRecorido
            End Get
            Set(value As Boolean)
                SetProperty(_ultimoDocRelaccionadoSalidaRecorido, value, "UltimoDocRelaccionadoSalidaRecorido")
            End Set
        End Property

        Public Property TuveSalidaRecorido As Boolean
            Get
                Return _tuveSalidaRecorido
            End Get
            Set(value As Boolean)
                SetProperty(_tuveSalidaRecorido, value, "TuveSalidaRecorido")
            End Set
        End Property

        Public Property IdentificadorRemesaModificada As String
            Get
                Return _IdentificadorRemesaModificada
            End Get
            Set(value As String)
                SetProperty(_IdentificadorRemesaModificada, value, "IdentificadorRemesaModificada")
            End Set
        End Property

#End Region

        Sub New()
            Identificador = System.Guid.NewGuid().ToString()
        End Sub

    End Class

End Namespace