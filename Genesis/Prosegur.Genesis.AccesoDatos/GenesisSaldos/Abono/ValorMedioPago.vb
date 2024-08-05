Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text

Namespace GenesisSaldos.Abono
    Public Class ValorMedioPago


#Region "Inserir"

        Public Shared Sub InserirAbonoValorMedioPago(objAbonoValorMedioPago As Clases.Abono.MedioPagoAbono,
                                                     IdentificadorAbonoValor As String,
                                                     IdentificadorDivisa As String,
                                                     ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoValorMedioPagoInserir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_VALOR_DET_MEDPAGO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_VALOR", ProsegurDbType.Objeto_Id, IdentificadorAbonoValor)
            wrapper.AgregarParam("OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, objAbonoValorMedioPago.Identificador)
            wrapper.AgregarParam("COD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, objAbonoValorMedioPago.TipoMedioPago.RecuperarValor)
            wrapper.AgregarParam("COD_NIVEL_DETALLE", ProsegurDbType.Identificador_Alfanumerico,
                                                                        If(String.IsNullOrEmpty(objAbonoValorMedioPago.Identificador),
                                                                           Enumeradores.TipoNivelDetalhe.Total.RecuperarValor(),
                                                                           Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()))
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, objAbonoValorMedioPago.Importe)
            wrapper.AgregarParam("NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, objAbonoValorMedioPago.Cantidad)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, objAbonoValorMedioPago.UsuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objAbonoValorMedioPago.UsuarioModificacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

        Public Shared Sub InserirAbonoValorTotalMedioPago(importe As Decimal,
                                                 identificadorAbonoValor As String,
                                                 codTipoMedioPago As Enumeradores.TipoMedioPago,
                                                 usuario As String,
                                                 ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoValorMedioPagoInserir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_VALOR_DET_MEDPAGO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_VALOR", ProsegurDbType.Objeto_Id, identificadorAbonoValor)
            wrapper.AgregarParam("OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, DBNull.Value)
            wrapper.AgregarParam("OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, DBNull.Value)
            wrapper.AgregarParam("COD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, codTipoMedioPago.RecuperarValor())
            wrapper.AgregarParam("COD_NIVEL_DETALLE", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.TipoNivelDetalhe.Total.RecuperarValor())
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, importe)
            wrapper.AgregarParam("NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, 0)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, usuario)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuario)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

#End Region

#Region "Deletar"

        Public Shared Sub DeletarAbonoValorMedioPago(IdentificadorAbono As String, transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoTerminoBorrar), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, IdentificadorAbono)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

#End Region

    End Class

End Namespace