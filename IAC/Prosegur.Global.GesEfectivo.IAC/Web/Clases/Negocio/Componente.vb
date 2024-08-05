Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    <Serializable()> _
    Public Class Componente
        Inherits BaseEntidade

#Region "[CONSTANTES]"

        Public Const C_TREEVIEW_NODE_EFETIVO As String = "022_lbl_efectivo"

#End Region

#Region "[VARIÁVEIS]"

        Private _orden As String
        Private _desOrden As String
        Private _oidMorfologiaComponente As String
        Private _codTipoContenedor As String
        Private _desTipoContenedor As String
        Private _necFuncionContenedor As String
        Private _codMorfologia As String
        Private _bolVigente As Boolean
        Private _objectos As New List(Of Objecto)

#End Region

#Region "[PROPRIEDADES]"

        Public Property Orden As String
            Get
                Return _orden
            End Get
            Set(value As String)
                _orden = value
            End Set
        End Property

        Public Property DesOrden As String
            Get
                Return _desOrden
            End Get
            Set(value As String)
                _desOrden = value
            End Set
        End Property

        Public ReadOnly Property DesFuncionContenedor() As String
            Get
                Select Case NecFuncionContenedor
                    Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR : Return Traduzir("022_funcion_dispensador")
                    Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_INGRESADOR : Return Traduzir("022_funcion_ingresador")
                    Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DEPOSITO : Return Traduzir("022_funcion_deposito")
                    Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_RECHAZO : Return Traduzir("022_funcion_rechazo")
                    Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_TARJETA : Return Traduzir("022_funcion_tarjeta")
                    Case Else : Return String.Empty
                End Select
            End Get
        End Property

        Public Property OidMorfologiaComponente() As String
            Get
                Return _oidMorfologiaComponente
            End Get
            Set(value As String)
                _oidMorfologiaComponente = value
            End Set
        End Property

        Public Property CodTipoContenedor() As String
            Get
                If String.IsNullOrEmpty(_codTipoContenedor) AndAlso _
                    Not _necFuncionContenedor = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR Then
                    ' Si tipo es null e não é função dispensador: CodTipoContenedor = Bolsa
                    Return ContractoServicio.Constantes.C_COD_TIPO_BOLSA
                Else
                    Return _codTipoContenedor
                End If
            End Get
            Set(value As String)
                If value = ContractoServicio.Constantes.C_COD_TIPO_BOLSA Then
                    ' Si tipo es Bolsa: codTipoContenedor = Null
                    _codTipoContenedor = String.Empty
                Else
                    _codTipoContenedor = value
                End If
            End Set
        End Property

        Public Property DesTipoContenedor() As String
            Get
                If String.IsNullOrEmpty(_desTipoContenedor) AndAlso
                    Not _necFuncionContenedor = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR Then
                    Return ContractoServicio.Constantes.C_COD_TIPO_BOLSA
                Else
                    Return _desTipoContenedor
                End If
            End Get
            Set(value As String)
                If value = ContractoServicio.Constantes.C_COD_TIPO_BOLSA Then
                    _desTipoContenedor = String.Empty
                Else
                    _desTipoContenedor = value
                End If
            End Set
        End Property

        Public Property NecFuncionContenedor() As Integer
            Get
                Return _necFuncionContenedor
            End Get
            Set(value As Integer)
                _necFuncionContenedor = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property Objectos() As List(Of Objecto)
            Get
                Return _objectos
            End Get
            Set(value As List(Of Objecto))
                _objectos = value
            End Set
        End Property

        Public ReadOnly Property DesDivisaMedioPago() As String
            Get
                Return ObtenerDescDivisaMedioPago()
            End Get
        End Property

        Public Property Codigo() As String
            Get
                Return _codMorfologia
            End Get
            Set(value As String)
                _codMorfologia = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()



        End Sub

        Public Sub New(Comp As Integracion.ContractoServicio.GetMorfologiaDetail.Componente)

            _oidMorfologiaComponente = Comp.OidMorfologiaComponente
            _codTipoContenedor = Comp.CodTipoContenedor
            _desTipoContenedor = Comp.DesTipoContenedor
            _necFuncionContenedor = Comp.NecFuncionContenedor
            _bolVigente = Comp.BolVigente
            _codMorfologia = Comp.CodMorfologiaComponente
            _orden = Comp.Orden
            _objectos = New List(Of Objecto)

            For Each item In Comp.Objectos

                _objectos.Add(New Objecto(item))

            Next

        End Sub

#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Obtém a descrição das divisas/meios de pagamento
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ObtenerDescDivisaMedioPago() As String

            Dim strDesc As String = String.Empty
            Dim desc As New StringBuilder

            Select Case _necFuncionContenedor

                Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR

                    If _objectos IsNot Nothing AndAlso _objectos.Count > 0 Then

                        ' descripción de la divisa + “-” + código denominación.
                        desc.Append(String.Format("{0} - {1}", _objectos(0).DesDivisa, _objectos(0).CodDenominacion))

                    End If

                    strDesc = desc.ToString()

                Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_INGRESADOR, _
                ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_TARJETA, _
                ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DEPOSITO

                    For Each obj In Objectos

                        ' verifica se é efetivo
                        If obj.BolEfectivo Then

                            ' adiciona descrição do efetivo
                            desc.Append(String.Format("{0} - {1} / ", obj.DesDivisa, Traduzir(Negocio.Componente.C_TREEVIEW_NODE_EFETIVO)))

                        End If

                        For Each tipoMP In obj.TiposMedioPago

                            For Each mp In tipoMP.MediosPago
                                ' descripción de la divisa + “-” + descripción del medio de pago.
                                desc.Append(String.Format("{0} - {1} / ", obj.DesDivisa, mp.DesMedioPago))
                            Next

                        Next

                    Next

                    strDesc = desc.ToString()

                    If strDesc.Length > 0 Then
                        ' remove a última "/"
                        strDesc = strDesc.Substring(0, strDesc.Length - 2)
                    End If

            End Select

            Return strDesc

        End Function

        Public Function ConvertToSetMorfologia() As ContractoServicio.Morfologia.SetMorfologia.Componente

            Dim componenteRetorno As New ContractoServicio.Morfologia.SetMorfologia.Componente
            Dim obj As ContractoServicio.Morfologia.SetMorfologia.Objecto

            With componenteRetorno
                .BolVigente = _bolVigente
                .NecFuncionContenedor = _necFuncionContenedor
                .CodTipoContenedor = _codTipoContenedor
                .DesTipoContenedor = _desTipoContenedor
                .CodMorfologiaComponente = _codMorfologia
                .Orden = _orden
                .Objectos = New List(Of ContractoServicio.Morfologia.SetMorfologia.Objecto)
            End With

            For Each objeto In Objectos

                If objeto.BolEfectivo OrElse _necFuncionContenedor = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR Then

                    obj = New ContractoServicio.Morfologia.SetMorfologia.Objecto
                    obj.CodDenominacion = objeto.CodDenominacion
                    obj.CodIsoDivisa = objeto.CodIsoDivisa
                    obj.NecOrdenDivisa = objeto.OrdenDivisa
                    obj.NecOrdenTipoMedPago = objeto.OrdenEfectivo
                    componenteRetorno.Objectos.Add(obj)

                End If

                For Each tipoMp In objeto.TiposMedioPago

                    For Each mp In tipoMp.MediosPago

                        obj = New ContractoServicio.Morfologia.SetMorfologia.Objecto

                        obj.CodDenominacion = objeto.CodDenominacion
                        obj.CodIsoDivisa = objeto.CodIsoDivisa
                        obj.CodMedioPago = mp.CodMedioPago
                        obj.CodTipoMedioPago = tipoMp.CodTipoMedioPago
                        obj.NecOrdenDivisa = objeto.OrdenDivisa
                        obj.NecOrdenTipoMedPago = tipoMp.OrdenTipoMedioPago

                        componenteRetorno.Objectos.Add(obj)

                    Next

                Next

            Next

            Return componenteRetorno

        End Function


        Public Shared Function ObtenerListaComponenteXMedioPago(Componentes As List(Of Componente), CodigoFunction As Integer) As List(Of String)

            Dim lista As New List(Of String)
            Dim desEfectivo As String = Traduzir(Negocio.Componente.C_TREEVIEW_NODE_EFETIVO)

            For Each comp In (From c In Componentes Where c.NecFuncionContenedor = CodigoFunction)

                For Each obj In comp.Objectos

                    If obj.BolEfectivo Then

                        ' código = codIsoDivisa + codigo tipo medio pago 
                        lista.Add(obj.CodIsoDivisa & desEfectivo)

                    Else

                        For Each tmp In obj.TiposMedioPago

                            ' código = codIsoDivisa + codigo tipo medio pago 
                            lista.Add(obj.CodIsoDivisa & tmp.CodTipoMedioPago)

                        Next

                    End If

                Next

            Next

            Return lista

        End Function

#End Region

    End Class

End Namespace
