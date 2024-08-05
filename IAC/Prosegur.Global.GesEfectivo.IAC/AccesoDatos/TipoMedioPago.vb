Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class TipoMedioPago

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Obtém os tipos medio pago através de um código divisa
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Shared Function GetComboTiposMediosPagoByDivisa(objPeticion As ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Peticion) As ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.TipoMedioPagoColeccion

        ' criar objeto tipos medio pago coleccion
        Dim objTiposMedioPago As New ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.TipoMedioPagoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.getComboTiposMediosPagoByDivisa.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoIsoDivisa))

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows

                ' adicionar para coleção
                objTiposMedioPago.Add(PopularComboTiposMedioPagoByDivisa(dr))

            Next

        End If

        ' retornar coleção de tipos medio pago
        Return objTiposMedioPago

    End Function

    ''' <summary>
    ''' Popula o objeto através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Private Shared Function PopularComboTiposMedioPagoByDivisa(dr As DataRow) As ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.TipoMedioPago

        ' criar objeto tipos medio pago
        Dim objTipoMedioPago As New ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.TipoMedioPago

        Util.AtribuirValorObjeto(objTipoMedioPago.Codigo, dr("cod_tipo_medio_pago"), GetType(String))

        If Not String.IsNullOrEmpty(dr("cod_tipo_medio_pago")) Then
            objTipoMedioPago.Descripcion = TipoMedioPago.ObterTipoMedioPagoDescripcion(dr("cod_tipo_medio_pago"))
        End If

        ' retorna objeto preenchido
        Return objTipoMedioPago

    End Function

    ''' <summary>
    ''' Obtém os tipos medio pago 
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' </history>
    Public Shared Function GetComboTiposMediosPago() As ContractoServicio.Utilidad.GetComboTiposMedioPago.TipoMedioPagoColeccion

        ' se encontrou algum registro
        Dim objTiposMedioPago As New ContractoServicio.Utilidad.GetComboTiposMedioPago.TipoMedioPagoColeccion

        ' adicionar para coleção
        objTiposMedioPago.Add(PopularComboTiposMedioPago(ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_CHEQUE))
        objTiposMedioPago.Add(PopularComboTiposMedioPago(ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_OTROSVALORES))
        objTiposMedioPago.Add(PopularComboTiposMedioPago(ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_TICKET))
        objTiposMedioPago.Add(PopularComboTiposMedioPago(ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_TARJETA))

        ' retornar coleção de tipos medio pago
        Return objTiposMedioPago

    End Function

    ''' <summary>
    ''' Popula o objeto através de um datarow
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' [blcosta] 29/06/2010 modificado
    ''' </history>
    Private Shared Function PopularComboTiposMedioPago(codigoTipoMedioPago As String) As ContractoServicio.Utilidad.GetComboTiposMedioPago.TipoMedioPago

        ' criar objeto tipos medio pago
        Dim objTipoMedioPago As New ContractoServicio.Utilidad.GetComboTiposMedioPago.TipoMedioPago

        Util.AtribuirValorObjeto(objTipoMedioPago.Codigo, codigoTipoMedioPago, GetType(String))

        If Not String.IsNullOrEmpty(codigoTipoMedioPago) Then
            objTipoMedioPago.Descripcion = TipoMedioPago.ObterTipoMedioPagoDescripcion(codigoTipoMedioPago)
        End If

        ' retorna objeto preenchido
        Return objTipoMedioPago

    End Function

    ''' <summary>
    ''' retorna a descrição do tipo de meio de pagamento
    ''' </summary>
    ''' <param name="CodigoTipoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta] 30/06/2010 Criado
    ''' </history>
    Public Shared Function ObterTipoMedioPagoDescripcion(CodigoTipoMedioPago As String) As String

        If String.IsNullOrEmpty(CodigoTipoMedioPago) Then
            Return String.Empty
        End If

        Select Case CodigoTipoMedioPago

            Case ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_CHEQUE
                Return Traduzir("014_tipopg_cheque")

            Case ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_OTROSVALORES
                Return Traduzir("014_tipopq_otrosvalores")

            Case ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_TICKET
                Return Traduzir("014_tipopg_ticket")

            Case ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_TARJETA
                Return Traduzir("014_tipopg_tarjeta")

            Case Else

                Return String.Empty

        End Select

    End Function

#End Region

End Class