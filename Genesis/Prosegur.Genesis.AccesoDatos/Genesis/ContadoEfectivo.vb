Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class ContadoEfectivo

        ''' <summary>
        ''' Insere um Contado efectivo.
        ''' </summary>
        ''' <param name="identificadorRemesa">Es una referencia a la entidad de REMESA</param>
        ''' <param name="identificadorBulto">Es una referencia a la entidad de BULTO</param>
        ''' <param name="identificadorParcial">Es una referencia a la entidad de PARCIAL</param>
        ''' <param name="identificadorDenominacion">Es una referencia a la entidad de DENOMINACION</param>
        ''' <param name="identificadorUnidadeMedida">Es una referencia a la entidad de UNIDAD MEDIDA</param>
        ''' <param name="tipoContado">Indica el TIPO del CONTADO: 0=Máquina; 1=Manual;</param>
        ''' <param name="importe">Indica el valor del Importe</param>
        ''' <param name="cantidad">Cantidad de Denominación contada</param>
        ''' <param name="identificadorCalidad">Indica la Calidad de la Denominación: E=Excelente; B=Buena; P=Pésimo;</param>
        ''' <param name="usuario">Usuario de creación</param>
        ''' <remarks></remarks>
        Public Shared Sub ContadoEfectivoInserir(identificadorRemesa As String, _
                                                 identificadorBulto As String, _
                                                 identificadorParcial As String, _
                                                 identificadorDenominacion As String, _
                                                 identificadorUnidadeMedida As String, _
                                                 tipoContado As String, _
                                                 importe As Decimal, _
                                                 cantidad As Long, _
                                                 identificadorCalidad As String, _
                                                 usuario As String)


            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ContadoEfectivoInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONTADO_EFECTIVO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, estadoDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, identificadorDenominacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, identificadorUnidadeMedida))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_CONTADO", ProsegurDbType.Inteiro_Curto, tipoContado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, importe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, cantidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CALIDAD", ProsegurDbType.Descricao_Curta, identificadorCalidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub


        ''' <summary>
        ''' Excluir os contados efectivo da remesa.
        ''' </summary>
        ''' <param name="identificadorRemessa"></param>
        ''' <remarks></remarks>
        Public Shared Sub ContadoEfectivoExcluirRemesa(identificadorRemessa As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ContadoEfectivoExcluirRemesa)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemessa))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar os contado efectivo da remessa.
        ' ''' </summary>
        ' ''' <param name="identificadorRemesa">Identificador da Remessa</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub ContadoEfectivoActualizarRemesa(identificadorRemesa As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ContadoEfectivoActualizarRemesa, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

        ''' <summary>
        ''' Exclui os contados efectivos do bulto.
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub ContadoEfectivoExcluirBulto(identificadorBulto As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ContadoEfectivoExcluirBulto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar os contado efectivo do malote.
        ' ''' </summary>
        ' ''' <param name="identificadorBulto">Identificador da Malote</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub ContadoEfectivoActualizarBulto(identificadorBulto As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ContadoEfectivoActualizarBulto, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

        ''' <summary>
        ''' Exclui os contados efectivos do parcial.
        ''' </summary>
        ''' <param name="identificadorParcial">Identificador do Parcial</param>
        ''' <remarks></remarks>
        Public Shared Sub ContadoEfectivoExcluirParcial(identificadorParcial As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ContadoEfectivoExcluirParcial)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar os contado efectivo do parcial.
        ' ''' </summary>
        ' ''' <param name="identificadorParcial">Identificador da Parcial</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub ContadoEfectivoActualizarParcial(identificadorParcial As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ContadoEfectivoActualizarParcial, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

    End Class

End Namespace
