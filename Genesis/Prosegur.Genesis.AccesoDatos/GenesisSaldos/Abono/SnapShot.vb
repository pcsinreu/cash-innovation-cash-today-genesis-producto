Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text
Namespace GenesisSaldos.Abono
    Public Class SnapShot

#Region "Anadir"

        Public Shared Sub AnadirAbonoSaldoSnapshot(ByRef objSnapShotSaldo As Clases.Abono.AbonoValor, _
                                                   idAbono As String, transaccion As DataBaseHelper.Transaccion)


            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoSnapshotAnadir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_SNAPSHOT", DbHelper.ProsegurDbType.Objeto_Id, objSnapShotSaldo.AbonoSaldo.IdentificadorSnapshot)
            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, idAbono)
            wrapper.AgregarParam("OID_CLIENTE", DbHelper.ProsegurDbType.Objeto_Id, objSnapShotSaldo.Cliente.Identificador)
            wrapper.AgregarParam("OID_SUBCLIENTE", DbHelper.ProsegurDbType.Objeto_Id, objSnapShotSaldo.SubCliente.Identificador)
            wrapper.AgregarParam("OID_PTO_SERVICIO", DbHelper.ProsegurDbType.Objeto_Id, objSnapShotSaldo.PtoServicio.Identificador)
            wrapper.AgregarParam("OID_SUBCANAL", ProsegurDbType.Objeto_Id, objSnapShotSaldo.AbonoSaldo.SubCanal.Identificador)
            wrapper.AgregarParam("OID_DIVISA", ProsegurDbType.Objeto_Id, objSnapShotSaldo.AbonoSaldo.Divisa.Identificador)
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, objSnapShotSaldo.AbonoSaldo.Importe)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, objSnapShotSaldo.UsuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objSnapShotSaldo.UsuarioModificacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

#End Region

#Region "Borrar"

        Public Shared Sub BorrarAbonoSaldoSnapShot(IdentificadorAbono As String, transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoSnapshotBorrar), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, IdentificadorAbono)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

#End Region

    End Class
End Namespace

