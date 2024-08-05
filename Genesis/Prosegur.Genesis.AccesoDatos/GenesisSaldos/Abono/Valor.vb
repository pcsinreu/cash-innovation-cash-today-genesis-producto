Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text

Namespace GenesisSaldos.Abono

    Public Class Valor

#Region "Inserir"

        Public Shared Function InserirAbonoValor(objAbonoValor As Clases.Abono.AbonoValor,
                                                 identificadorAbono As String,
                                                 identificadorBanco As String,
                                                 ByRef transaccion As DataBaseHelper.Transaccion) As String

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoValorInserir), True, CommandType.Text)

            Dim IdentificadorAbonoValor As String = Guid.NewGuid.ToString

            wrapper.AgregarParam("OID_ABONO_VALOR", ProsegurDbType.Objeto_Id, IdentificadorAbonoValor)
            wrapper.AgregarParam("OID_ABONO", ProsegurDbType.Objeto_Id, identificadorAbono)
            wrapper.AgregarParam("OID_CLIENTE", ProsegurDbType.Objeto_Id, objAbonoValor.Cliente.Identificador)
            wrapper.AgregarParam("OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, If(objAbonoValor.SubCliente IsNot Nothing, Util.RetornaValorOuDbNull(objAbonoValor.SubCliente.Identificador),
                                                                                DBNull.Value))
            wrapper.AgregarParam("OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, If(objAbonoValor.PtoServicio IsNot Nothing, Util.RetornaValorOuDbNull(objAbonoValor.PtoServicio.Identificador),
                                                                                  DBNull.Value))
            wrapper.AgregarParam("OID_DIVISA", ProsegurDbType.Objeto_Id, objAbonoValor.Divisa.Identificador)
            wrapper.AgregarParam("COD_TIPO_CUENTA_BANCARIA", ProsegurDbType.Identificador_Alfanumerico, objAbonoValor.Cuenta.CodigoTipoCuentaBancaria)
            wrapper.AgregarParam("COD_CUENTA_BANCARIA", ProsegurDbType.Identificador_Alfanumerico, objAbonoValor.Cuenta.CodigoCuentaBancaria)
            wrapper.AgregarParam("COD_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, Util.RetornaValorOuDbNull(objAbonoValor.Cuenta.CodigoDocumento))
            wrapper.AgregarParam("DES_TITULARIDAD", ProsegurDbType.Descricao_Longa, objAbonoValor.Cuenta.DescripcionTitularidad)
            wrapper.AgregarParam("DES_OBSERVACIONES", ProsegurDbType.Observacao_Longa, Util.RetornaValorOuDbNull(objAbonoValor.Observaciones))
            wrapper.AgregarParam("NUM_IMPORTE", ProsegurDbType.Numero_Decimal, objAbonoValor.Importe)
            wrapper.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, objAbonoValor.UsuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objAbonoValor.UsuarioModificacion)
            wrapper.AgregarParam("OID_BANCO", ProsegurDbType.Objeto_Id, identificadorBanco)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

            Return IdentificadorAbonoValor
        End Function

#End Region

#Region "Deletar"

        Public Shared Sub DeletarAbonoValor(IdentificadorAbono As String, transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoSaldoTerminoBorrar), True, CommandType.Text)

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Objeto_Id, IdentificadorAbono)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

#End Region

    End Class

End Namespace