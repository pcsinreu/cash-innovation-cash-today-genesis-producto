Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text

Namespace GenesisSaldos.Abono

    Public Class Elemento


#Region "Inserir"

        Public Shared Sub InserirAbonoElemento(objAbonoElemento As Clases.Abono.AbonoElemento,
                                               IdentificadorAbonoValor As String,
                                               ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoElementoInserir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_ELEMENTO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_VALOR", ProsegurDbType.Objeto_Id, IdentificadorAbonoValor)
            wrapper.AgregarParam("OID_REMESA", ProsegurDbType.Objeto_Id, objAbonoElemento.IdentificadorRemesa)
            wrapper.AgregarParam("OID_BULTO", ProsegurDbType.Identificador_Alfanumerico, Util.RetornaValorOuDbNull(objAbonoElemento.IdentificadorBulto))
            wrapper.AgregarParam("COD_ABONO_ELEMENTO", ProsegurDbType.Identificador_Alfanumerico, objAbonoElemento.Codigo)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, objAbonoElemento.UsuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objAbonoElemento.UsuarioModificacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

#End Region

#Region "Deletar"

        Public Shared Sub DeletarAbonoElemento(IdentificadorAbono As String, ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoElementoDeletar), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, IdentificadorAbono)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

#End Region

#Region "Update"
        Public Shared Sub ActualizarEstadosAbonosRemesas(identificadorAbono As String,
                                                         estado As Enumeradores.EstadoAbonoElemento,
                                                         ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoElementoActualizarEstadoAbonoRemesa), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", ProsegurDbType.Objeto_Id, identificadorAbono)
            wrapper.AgregarParam("COD_ESTADO_ABONO", ProsegurDbType.Objeto_Id, estado.RecuperarValor())

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

            ActualizarEstadosAbonosBultos(identificadorAbono, estado, transaccion)

        End Sub
        Public Shared Sub ActualizarEstadosAbonosBultos(IdentificadorAbono As String,
                                                        estado As Enumeradores.EstadoAbonoElemento,
                                                        ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoElementoActualizarEstadoAbonoBulto), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", ProsegurDbType.Objeto_Id, IdentificadorAbono)
            wrapper.AgregarParam("COD_ESTADO_ABONO", ProsegurDbType.Objeto_Id, estado.RecuperarValor())

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub
#End Region

    End Class

End Namespace