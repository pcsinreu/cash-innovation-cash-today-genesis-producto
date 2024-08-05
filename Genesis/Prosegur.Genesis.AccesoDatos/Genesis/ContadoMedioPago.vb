Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class ContadoMedioPago

        ''' <summary>
        ''' Insere um Contado MedioPago.
        ''' </summary>
        ''' <param name="identificadorRemesa">Es una referencia a la entidad de REMESA</param>
        ''' <param name="identificadorBulto">Es una referencia a la entidad de BULTO</param>
        ''' <param name="identificadorParcial">Es una referencia a la entidad de PARCIAL</param>
        ''' <param name="identificadorMedioPago">Es una referencia a la entidad de MedioPago</param>
        ''' <param name="tipoContado">Indica el TIPO del CONTADO: 0=Máquina; 1=Manual</param>
        ''' <param name="importe">Indica el valor del Importe</param>
        ''' <param name="cantidad">Indica Cantidad informada</param>
        ''' <param name="nivelDetalhe">Indica el NIVEL de DETALLE: D=Detallado; T=total</param>
        ''' <param name="secuencia">Indica la secuencia de lo Contado</param>
        ''' <param name="usuario">Usuario de creación</param>
        ''' <remarks></remarks>
        Public Shared Function ContadoMedioPagoInserir(identificadorRemesa As String, _
                                                       identificadorBulto As String, _
                                                       identificadorParcial As String, _
                                                       identificadorMedioPago As String, _
                                                       tipoContado As String, _
                                                       importe As Decimal, _
                                                       cantidad As Long, _
                                                       nivelDetalhe As String, _
                                                       secuencia As Integer, _
                                                       usuario As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ContadoMedioPagoInserir)
            cmd.CommandType = CommandType.Text

            Dim identificadorContadoMedioPago = Guid.NewGuid.ToString

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONTADO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorContadoMedioPago))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, estadoDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorMedioPago))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_CONTADO", ProsegurDbType.Inteiro_Curto, tipoContado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, importe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, cantidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, nivelDetalhe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEC_SECUENCIA", ProsegurDbType.Inteiro_Curto, secuencia))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            Return identificadorContadoMedioPago
        End Function

        ''' <summary>
        ''' Excluir os contados MedioPago da remesa.
        ''' </summary>
        ''' <param name="identificadorRemessa"></param>
        ''' <remarks></remarks>
        Public Shared Sub ContadoMedioPagoExcluirRemesa(identificadorRemessa As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ContadoMedioPagoExcluirRemesa)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemessa))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar os contados MedioPago da remessa.
        ' ''' </summary>
        ' ''' <param name="identificadorRemesa">Identificador da Remessa</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub ContadoMedioPagoActualizarRemesa(identificadorRemesa As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ContadoMedioPagoActualizarRemesa, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

        ''' <summary>
        ''' Exclui os contados MedioPago do bulto.
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub ContadoMedioPagoExcluirBulto(identificadorBulto As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ContadoMedioPagoExcluirrBulto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar os contados MedioPago do malote.
        ' ''' </summary>
        ' ''' <param name="identificadorBulto">Identificador do Malote</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub ContadoMedioPagoActualizarBulto(identificadorBulto As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ContadoMedioPagoActualizarBulto, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

        ''' <summary>
        ''' Exclui os contados MedioPago do bulto.
        ''' </summary>
        ''' <param name="identificadorParcial"></param>
        ''' <remarks></remarks>
        Public Shared Sub ContadoMedioPagoExcluirParcial(identificadorParcial As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ContadoMedioPagoExcluirrParcial)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar os contados MedioPago da parcial.
        ' ''' </summary>
        ' ''' <param name="identificadorParcial">Identificador do Parcial</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub ContadoMedioPagoActualizarParcial(identificadorParcial As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ContadoMedioPagoActualizarParcial, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

    End Class

End Namespace



