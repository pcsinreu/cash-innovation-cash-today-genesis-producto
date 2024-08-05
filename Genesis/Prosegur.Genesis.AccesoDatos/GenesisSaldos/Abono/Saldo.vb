Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text

Namespace GenesisSaldos.Abono

    Public Class Saldo


#Region "Anadir"

        Public Shared Sub AnadirAbonoSaldo(objAbonoSaldo As Clases.Abono.AbonoSaldo,
                                               IdentificadorAbonoValor As String,
                                               IdentificadorAbono As String,
                                               ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoAnadir), True, CommandType.Text)

            objAbonoSaldo.Identificador = Guid.NewGuid.ToString

            wrapper.AgregarParam("OID_ABONO_SALDO", ProsegurDbType.Objeto_Id, objAbonoSaldo.Identificador)
            wrapper.AgregarParam("OID_ABONO_VALOR", ProsegurDbType.Objeto_Id, IdentificadorAbonoValor)
            wrapper.AgregarParam("OID_ABONO_SNAPSHOT", ProsegurDbType.Objeto_Id, objAbonoSaldo.IdentificadorSnapshot)
            wrapper.AgregarParam("OID_DOCUMENTO", ProsegurDbType.Descricao_Curta, objAbonoSaldo.IdentificadorDocumento)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, objAbonoSaldo.UsuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objAbonoSaldo.UsuarioModificacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

#End Region

#Region "Actualizar"
        Public Shared Sub ActualizarIdentificadorDocumento(identificadoresDocumento As Dictionary(Of String, String))

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandType = CommandType.Text
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ActualizarIdentificadorDocumento)

                For Each idAbonoSaldo In identificadoresDocumento.Keys
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", _
                                            ProsegurDbType.Objeto_Id, identificadoresDocumento(idAbonoSaldo)))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO_SALDO", _
                        ProsegurDbType.Objeto_Id, idAbonoSaldo))

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
                    cmd.Parameters.Clear()
                Next
            End Using
        End Sub

        Public Shared Sub ActualizarIdentificadorDocumento(identificadoresDocumento As Dictionary(Of String, String), ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ActualizarIdentificadorDocumento), True, CommandType.Text)

            For Each idAbonoSaldo In identificadoresDocumento.Keys
                wrapper.AgregarParam("OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadoresDocumento(idAbonoSaldo))
                wrapper.AgregarParam("OID_ABONO_SALDO", ProsegurDbType.Objeto_Id, idAbonoSaldo)

                DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

                wrapper.Params.Clear()
            Next

        End Sub
#End Region


#Region "Borrar"

        Public Shared Sub BorrarAbonoSaldo(IdentificadorAbono As String, transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoBorrar), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, IdentificadorAbono)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

#End Region

    End Class

End Namespace
