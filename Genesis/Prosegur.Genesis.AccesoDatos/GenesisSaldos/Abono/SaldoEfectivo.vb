Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text

Namespace GenesisSaldos.Abono
    Public Class SaldoEfectivo

#Region "[ANADIR]"

        Public Shared Sub AnadirAbonoSaldoEfectivo(ByRef objEfectivoAbono As Clases.Abono.EfectivoAbono,
                                                         identificadorSnapShot As String,
                                                         identificadorCuenta As String,
                                                         identificadorDivisa As String,
                                                         ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoSnapshotEfectivoAnadir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_SNAPSHOT_EFECTIVO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_SNAPSHOT", ProsegurDbType.Objeto_Id, identificadorSnapShot)
            wrapper.AgregarParam("OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, identificadorCuenta)
            wrapper.AgregarParam("OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa)
            wrapper.AgregarParam("OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, objEfectivoAbono.IdentificadorUnidadeMedida)
            wrapper.AgregarParam("OID_CALIDAD", ProsegurDbType.Objeto_Id, objEfectivoAbono.IdentificadorCalidad)
            wrapper.AgregarParam("OID_DENOMINACION", ProsegurDbType.Objeto_Id, objEfectivoAbono.Identificador)
            wrapper.AgregarParam("COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, objEfectivoAbono.TipoNivelDetalle.RecuperarValor())
            wrapper.AgregarParam("COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Descricao_Curta, DBNull.Value)
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, objEfectivoAbono.Importe)
            wrapper.AgregarParam("NEL_CANTIDAD", ProsegurDbType.Inteiro_Curto, objEfectivoAbono.Cantidad)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, objEfectivoAbono.UsuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objEfectivoAbono.UsuarioCreacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

        Public Shared Sub AnadirAbonoSaldoTotalEfectivo(IdentificadorSnapShot As String, identificadorCuenta As String,
                                                         identificadorDivisa As String, codTipoEfectivoTotal As String,
                                                         importe As Decimal, usuarioCreacion As String,
                                                         usuarioModificacion As String, ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoSnapshotEfectivoAnadir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_SNAPSHOT_EFECTIVO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_SNAPSHOT", ProsegurDbType.Objeto_Id, IdentificadorSnapShot)
            wrapper.AgregarParam("OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, identificadorCuenta)
            wrapper.AgregarParam("OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa)
            wrapper.AgregarParam("OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, DBNull.Value)
            wrapper.AgregarParam("OID_CALIDAD", ProsegurDbType.Objeto_Id, DBNull.Value)
            wrapper.AgregarParam("OID_DENOMINACION", ProsegurDbType.Objeto_Id, DBNull.Value)
            wrapper.AgregarParam("COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, Enumeradores.TipoNivelDetalhe.Total.RecuperarValor())
            wrapper.AgregarParam("COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Descricao_Curta, codTipoEfectivoTotal)
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, importe)
            wrapper.AgregarParam("NEL_CANTIDAD", ProsegurDbType.Inteiro_Curto, DBNull.Value)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, usuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuarioModificacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)


        End Sub

#End Region

#Region "[BORRAR]"
        Public Shared Sub BorrarAbonoSaldoEfectivo(IdentificadorAbono As String, transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoSnapshotEfectivoBorrar), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, IdentificadorAbono)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub
#End Region

    End Class
End Namespace
