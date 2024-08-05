Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text

Namespace GenesisSaldos.Abono
    Public Class SaldoMedioPago

#Region "[ANADIR]"
        Public Shared Sub AnadirAbonoSaldoMedioPago(ByRef objMedioPagoAbono As Clases.Abono.MedioPagoAbono,
                                                         identificadorSnapShot As String,
                                                         identificadorCuenta As String,
                                                         identificadorDivisa As String,
                                                         ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoSnapshotMedioPagoAnadir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_SNAPSHOT_MEDIOPAGO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_SNAPSHOT", ProsegurDbType.Objeto_Id, identificadorSnapShot)
            wrapper.AgregarParam("OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, identificadorCuenta)
            wrapper.AgregarParam("OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa)
            wrapper.AgregarParam("OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, objMedioPagoAbono.Identificador)
            wrapper.AgregarParam("COD_TIPO_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, objMedioPagoAbono.TipoMedioPago.RecuperarValor())
            wrapper.AgregarParam("OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, objMedioPagoAbono.IdentificadorUnidadeMedida)
            wrapper.AgregarParam("COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, objMedioPagoAbono.TipoNivelDetalle.RecuperarValor())
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, objMedioPagoAbono.Importe)
            wrapper.AgregarParam("NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, objMedioPagoAbono.Cantidad)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, objMedioPagoAbono.UsuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objMedioPagoAbono.UsuarioCreacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

        Public Shared Sub AnadirAbonoSaldoTotalMedioPago(IdentificadorSnapShot As String, identificadorCuenta As String,
                                                         identificadorDivisa As String, tipoMedioPago As Enumeradores.TipoMedioPago, _
                                                         importe As Decimal, usuarioCreacion As String, usuarioModificacion As String,
                                                         ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoSnapshotMedioPagoAnadir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_SNAPSHOT_MEDIOPAGO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_SNAPSHOT", ProsegurDbType.Objeto_Id, IdentificadorSnapShot)
            wrapper.AgregarParam("OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, identificadorCuenta)
            wrapper.AgregarParam("OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa)
            wrapper.AgregarParam("OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, DBNull.Value)
            wrapper.AgregarParam("COD_TIPO_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, tipoMedioPago.RecuperarValor())
            wrapper.AgregarParam("OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, DBNull.Value)
            wrapper.AgregarParam("COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, Enumeradores.TipoNivelDetalhe.Total.RecuperarValor())
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, importe)
            wrapper.AgregarParam("NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, 0)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, usuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuarioModificacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub
#End Region

#Region "[BORRAR]"

        Public Shared Sub BorrarAbonoSaldoMedioPago(IdentificadorAbono As String, ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoSnapshotMedioPagoBorrar), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, IdentificadorAbono)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub
#End Region

    End Class
End Namespace
