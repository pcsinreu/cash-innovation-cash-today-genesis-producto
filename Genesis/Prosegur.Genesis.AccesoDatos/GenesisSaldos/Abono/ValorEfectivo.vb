Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text

Namespace GenesisSaldos.Abono

    Public Class ValorEfectivo

#Region "Inserir"

        Public Shared Sub InserirAbonoValorEfectivo(objAbonoValorEfectivo As Clases.Abono.EfectivoAbono,
                                                         IdentificadorAbonoValor As String,
                                                         IdentificadorDivisa As String,
                                                         ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoValorEfectivoInserir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_VALOR_DET_EFECTIVO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_VALOR", ProsegurDbType.Objeto_Id, IdentificadorAbonoValor)
            wrapper.AgregarParam("OID_DENOMINACION", ProsegurDbType.Objeto_Id, objAbonoValorEfectivo.Identificador)
            wrapper.AgregarParam("COD_NIVEL_DETALLE", ProsegurDbType.Identificador_Alfanumerico, objAbonoValorEfectivo.TipoNivelDetalle.RecuperarValor)
            wrapper.AgregarParam("COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value)
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, objAbonoValorEfectivo.Importe)
            wrapper.AgregarParam("NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, objAbonoValorEfectivo.Cantidad)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, objAbonoValorEfectivo.UsuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objAbonoValorEfectivo.UsuarioModificacion)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

        Public Shared Sub InserirAbonoValorTotalEfectivo(importe As Decimal,
                                                 identificadorAbonoValor As String,
                                                 usuario As String,
                                                 codigoTipoEfectivoTotal As String,
                                                 ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoValorEfectivoInserir), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO_VALOR_DET_EFECTIVO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString)
            wrapper.AgregarParam("OID_ABONO_VALOR", ProsegurDbType.Objeto_Id, identificadorAbonoValor)
            wrapper.AgregarParam("OID_DENOMINACION", ProsegurDbType.Objeto_Id, DBNull.Value)
            wrapper.AgregarParam("COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Objeto_Id, codigoTipoEfectivoTotal)
            wrapper.AgregarParam("COD_NIVEL_DETALLE", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.TipoNivelDetalhe.Total.RecuperarValor())
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, importe)
            wrapper.AgregarParam("NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, 0)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, usuario)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuario)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

#End Region

#Region "Deletar"

        Public Shared Sub DeletarAbonoValorEfectivo(IdentificadorAbono As String, transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoValorEfectivoDeletar), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, IdentificadorAbono)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

#End Region

    End Class

End Namespace