Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text
Namespace GenesisSaldos.Abono
    Public Class SaldoTermino

#Region "Anadir"

        Public Shared Sub AnadirTerminoAbonoSaldo(ByRef objTerminoAbonoSaldo As Clases.TerminoIAC, identificadorAbonoSaldo As String, _
                                                   gmtCreacion As Date, usuacrioCreacion As String, gmtModificacion As Date,
                                                   usuarioModificacion As String,
                                                   ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoTerminoAnadir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_SALDO_TERMINO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_SALDO", ProsegurDbType.Objeto_Id, identificadorAbonoSaldo)
            wrapper.AgregarParam("OID_TERMINO", ProsegurDbType.Objeto_Id, objTerminoAbonoSaldo.Identificador)
            wrapper.AgregarParam("DES_VALOR", ProsegurDbType.Descricao_Longa, objTerminoAbonoSaldo.Valor)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, usuacrioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuarioModificacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

#End Region

#Region "Borrar"

        Public Shared Sub BorrarTerminoAbonoSaldo(IdentificadorAbono As String, transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoTerminoBorrar), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, IdentificadorAbono)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

#End Region

#Region "Obtener"
        Public Shared Function ObtenerTerminosSaldos(identificadorAbonoSaldo As String) As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerTerminosAbonoSaldo)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO_SALDO", ProsegurDbType.Identificador_Alfanumerico, identificadorAbonoSaldo))

                Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

        End Function

#End Region


    End Class
End Namespace

