Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas.Remesa
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace NuevoSalidas

    Public Class Bulto

        ''' <summary>
        ''' Retorna os efetivos para o Bulto informado como parâmetro.
        ''' </summary>
        ''' <param name="IdentificadorBulto">String</param>
        ''' <returns>DataTable</returns> 
        ''' <remarks></remarks>
        ''' <history>
        ''' [abueno] 29/07/2010 Criado
        ''' [jagsilva] 14/09/2010 Alterado
        ''' </history>
        Public Shared Function ObterEfetivosBulto(IdentificadorBulto As String) As DataTable

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = My.Resources.Salidas_Bulto_ObterEfectivoBulto

                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))

            End With

            'Obtem o estado do bulto
            Return DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        ''' <summary>
        ''' Retorna os bultos não Arqueados
        ''' </summary>
        ''' <param name="CodigoDelegacion">String</param>
        ''' <param name="CodigoPuesto">String</param>
        ''' <returns>String</returns> 
        ''' <remarks></remarks>
        ''' <history>
        ''' [abueno] 29/07/2010 Criado
        ''' </history>
        Public Shared Function ObterBultoNoArqueados(CodigoDelegacion As String, CodigoPuesto As String, BolControlaNumerarioPorBulto As Boolean) As DataTable

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = My.Resources.Salidas_Bulto_RecuperarBultosNoArqueados

                'Adiciona parâmetro ao comando
                If (Not String.IsNullOrEmpty(CodigoDelegacion)) Then

                    'Adiciona Filtro de codigo de delegação
                    .CommandText &= " AND PUE.COD_DELEGACION = :COD_DELEGACION "
                    .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
                End If

                If (Not String.IsNullOrEmpty(CodigoPuesto)) Then
                    'Adiciona Filtro de codigo de CodigoPuesto
                    .CommandText &= " AND PUE.COD_PUESTO = :COD_PUESTO "
                    .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, CodigoPuesto))
                End If

                'Adiciona ordenação da consulta
                .CommandText &= "ORDER BY  BUL.COD_PRECINTO_BULTO "

            End With

            'Obtem o estado do bulto
            Return DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        ''' <summary>
        ''' Verifica se todos os bultos foram preparados.
        ''' </summary>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 21/06/2012 - Criado
        ''' </history>
        Public Shared Function HayBultosObjetosParaPreparar(IdentificadorRemesa As String) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_VerificarTodosBultosFueronPreparados.ToString)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        ''' <summary>
        ''' Verifica se é o ultimo bulto ou objeto da remesa
        ''' </summary>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 20/06/2012 - Criado
        ''' </history>
        Public Shared Function RetornarCantidadBultosObjetosParaProcesar(IdentificadorRemesa As String) As Integer

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_VerificarEsUltimoBultoObjeto.ToString)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_BULTO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(Enumeradores.EstadoBulto.Procesado)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        Public Shared Function ValidarPrecintoDuplicadoPorBulto(IdentificadorBulto As String, CodigoPrecinto As String) As Boolean

            Dim PrecintosExistentes As New System.Text.StringBuilder
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandText = My.Resources.Salidas_Bulto_ValidarCantidadPrecintos
            comando.CommandType = CommandType.Text
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Objeto_Id, CodigoPrecinto))
            Dim CantidadGeneral As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, comando)

            ' SE EXISTIR PRECINTO NA TABELA DE BULTO
            If CantidadGeneral > 0 Then
                Dim comandoPorBulto As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                comandoPorBulto.CommandType = CommandType.Text
                comandoPorBulto.CommandText = My.Resources.Salidas_Bulto_ValidarPrecintoDuplicadoPorBulto
                comandoPorBulto.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))
                comandoPorBulto.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Objeto_Id, CodigoPrecinto))
                Dim dtCantidadPorBulto As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comandoPorBulto)

                ' SE EXISTIR PARA O BULTO CURRENTE, ATUALIZARÁ O BULTO NORMALMENTE, SE NAO. GUARDA PRECINTO DUPLICADO E EXIBE O ERRO.
                If dtCantidadPorBulto.Rows.Count = 1 AndAlso CantidadGeneral = 1 Then
                    'pode atualizar o bulto
                    Return True
                Else
                    'guarda precinto duplicado
                    Return False
                End If
            End If

            'pode atualizar o bulto
            Return True

        End Function
        Public Shared Function ValidarCodigoBolsaDuplicadoPorBulto(IdentificadorBulto As String, CodigoBolsa As String) As Boolean

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandText = My.Resources.Salidas_Bulto_ValidarCantidadCodigoBolsa
            comando.CommandType = CommandType.Text
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_BOLSA", ProsegurDbType.Objeto_Id, CodigoBolsa))
            Dim CantidadGeneral As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, comando)

            ' SE EXISTIR PRECINTO NA TABELA DE BULTO
            If CantidadGeneral > 0 Then
                Dim comandoPorBulto As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                comandoPorBulto.CommandType = CommandType.Text
                comandoPorBulto.CommandText = My.Resources.Salidas_Bulto_ValidarCodigoBolsaDuplicadoPorBulto
                comandoPorBulto.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))
                comandoPorBulto.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_BOLSA", ProsegurDbType.Objeto_Id, CodigoBolsa))
                Dim dtCantidadPorBulto As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comandoPorBulto)

                ' SE EXISTIR PARA O BULTO CURRENTE, ATUALIZARÁ O BULTO NORMALMENTE, SE NAO. GUARDA PRECINTO DUPLICADO E EXIBE O ERRO.
                If dtCantidadPorBulto.Rows.Count = 1 AndAlso CantidadGeneral = 1 Then
                    'pode atualizar o bulto
                    Return True
                Else
                    'guarda precinto duplicado
                    Return False
                End If
            End If

            'pode atualizar o bulto
            Return True

        End Function

        Public Shared Sub ActualizarBolPreparadoPorBulto(IdentificadorBulto As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ActualizarBolPreparadoPorBulto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub ActualizarBolPreparadoPorRemesa(IdentificadorRemesa As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ActualizarBolPreparadoPorRemesa)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Recupera os bultos
        ''' </summary>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks> 
        Public Shared Function RecuperarBultosRemesa(IdentificadorRemesa As String, _
                                                     IdentificadoresBulto As List(Of String)) As ObservableCollection(Of Clases.Bulto)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Bulto_RecuperarDatos
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            If IdentificadoresBulto IsNot Nothing AndAlso IdentificadoresBulto.Count > 0 Then

                cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresBulto, "OID_BULTO", cmd, "AND", "BUL")

            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Return PreencherBultos(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd))
        End Function

        ''' <summary>
        ''' Preenche o bulto
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PreencherBultos(dt As DataTable) As ObservableCollection(Of Clases.Bulto)

            Dim objBultos As ObservableCollection(Of Clases.Bulto) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objBultos = New ObservableCollection(Of Clases.Bulto)()
                Dim objBulto As Clases.Bulto = Nothing
                Dim objPrecintos As ObservableCollection(Of String)
                Dim CodigoPrecinto As String = String.Empty

                For Each dr In dt.Rows

                    objBulto = New Clases.Bulto()

                    CodigoPrecinto = Util.AtribuirValorObj(dr("COD_PRECINTO_BULTO"), GetType(String))

                    If Not String.IsNullOrEmpty(CodigoPrecinto) Then
                        objPrecintos = New ObservableCollection(Of String)
                        objPrecintos.Add(CodigoPrecinto)
                    Else
                        objPrecintos = Nothing
                    End If

                    With objBulto
                        .Identificador = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))
                        .Precintos = objPrecintos
                        .CodigoBolsa = Util.AtribuirValorObj(dr("COD_BOLSA"), GetType(String))
                        .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoBulto)(Util.AtribuirValorObj(dr("COD_ESTADO_BULTO"), GetType(String)))
                        .Cuadrado = Util.AtribuirValorObj(dr("BOL_CUADRADO"), GetType(Boolean))
                        .UsuarioCreacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime))
                        .TipoBulto = TipoBulto.ObtenerTipoBulto(Util.AtribuirValorObj(dr("OID_TIPO_BULTO"), GetType(String)))
                        .AceptaPicos = Util.AtribuirValorObj(dr("BOL_PICOS"), GetType(Boolean))
                        .Preparado = Util.AtribuirValorObj(dr("BOL_PREPARADO"), GetType(Boolean))
                        .PuestoResponsable = Util.AtribuirValorObj(dr("COD_PUESTO"), GetType(String))
                        .TiposMercancia = TipoMercancia.RecuperarTiposMercanciaBulto(.Identificador)
                        .Divisas = Divisa.RecuperarDivisasBultoOModulo(.Identificador, False)
                        .Modulo = Modulo.RecuperarModuloBulto(.Identificador)
                        .UsuarioBloqueo = Util.AtribuirValorObj(dr("COD_USUARIO_BLOQUEO"), GetType(String))
                    End With

                    objBultos.Add(objBulto)
                Next

            End If

            Return objBultos
        End Function

        Public Shared Function RecuperarIdentificadorBultoPorCodigoPrecinto(CodigoPrecinto As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Bulto_RecuperarIdentificadorBultoPorCodigoPrecinto
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, CodigoPrecinto))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        Public Shared Function RecuperarIdentificadoresBultosPorCodigosPrecintos(CodigosPrecintos As List(Of String)) As List(Of Clases.Bulto)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Bulto_RecuperarIdentificadorBultosPorCodigosPrecintos
            cmd.CommandType = CommandType.Text

            cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, CodigosPrecintos, "COD_PRECINTO_BULTO", cmd, "WHERE", "BULT"))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim listaBultos As New List(Of Clases.Bulto)

                For Each row In dt.Rows

                    If Not String.IsNullOrEmpty(Util.AtribuirValorObj(row("OID_BULTO"), GetType(String))) Then
                        listaBultos.Add(New Clases.Bulto With {.Identificador = Util.AtribuirValorObj(row("OID_BULTO"), GetType(String)), .Precintos = New ObservableCollection(Of String) From {Util.AtribuirValorObj(row("COD_PRECINTO_BULTO"), GetType(String))}})
                    End If
                Next

                Return listaBultos

            End If

            Return Nothing

        End Function

        Public Shared Function ObtenerIdentificadoresBultoPorCodigoPrecinto(CodigoPrecinto As String) As List(Of String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Bulto_RecuperarIdentificadorBultoPorCodigoPrecinto
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, CodigoPrecinto))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim Identificadores As New List(Of String)
                For Each row In dt.Rows
                    Identificadores.Add(Util.AtribuirValorObj(row("OID_BULTO"), GetType(String)))
                Next row
            End If
            Return Nothing
        End Function

        Public Shared Sub GuardarPrecintos(Bultos As ObservableCollection(Of Clases.Bulto), Optional ByRef Transaccion As Transacao = Nothing)

            For Each Bulto As Clases.Bulto In Bultos

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ActualizarPrecinto)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, Bulto.Precintos(0)))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, Bulto.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, Bulto.FechaHoraModificacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Objeto_Id, Bulto.UsuarioModificacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_BOLSA", ProsegurDbType.Objeto_Id, Bulto.CodigoBolsa))

                If Transaccion IsNot Nothing Then
                    Transaccion.AdicionarItemTransacao(cmd)
                Else
                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)
                End If

            Next

        End Sub

        Public Shared Function RecuperarIdentificadorBultoPorIdentificador(Identificador As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Bulto_RecuperarIdentificadorBultoPorIdentificador
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, Identificador))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        ''' <summary>
        ''' Validar serviços malote.
        ''' </summary>
        ''' <param name="codigoDelegacion">Código da Delegação</param>
        ''' <param name="OidsBultos">Identificadores do Malote</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ValidarServicios(codigoDelegacion As String, OidsBultos As List(Of String)) As ObservableCollection(Of Clases.Bulto)

            Dim objBultos As New ObservableCollection(Of Clases.Bulto)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ValidarServicioBulto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

            Dim sbComando As New System.Text.StringBuilder
            sbComando.AppendLine(Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, OidsBultos, "OID_BULTO", cmd, " AND ", "B")))

            cmd.CommandText &= sbComando.ToString

            ' Para cada linha retornada
            For Each dr As DataRow In AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd).Rows

                objBultos.Add(New Clases.Bulto With
                               {
                                   .Identificador = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String)),
                                   .Estado = Extenciones.RecuperarEnum(Of EstadoRemesa)(Util.AtribuirValorObj(dr("COD_ESTADO_BULTO"), GetType(String))),
                                   .FechaHoraModificacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime)),
                                   .UsuarioBloqueo = Util.AtribuirValorObj(dr("COD_USUARIO_BLOQUEO"), GetType(String))
                               })

            Next

            Return objBultos

        End Function

        Shared Sub ActualizarEstadoPorRemesa(identificadorRemesa As String, estadoBulto As String, usuarioActualizacion As String, fechaActualizacion As Date)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ActualizarEstadoPorRemesa)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_BULTO", ProsegurDbType.Identificador_Alfanumerico, estadoBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, usuarioActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Shared Sub AsignarPuesto(identificadorRemesa As String, identificadorBulto As String, codigoPuesto As String, usuarioActualizacion As String, fechaActualizacion As DateTime, codDelegacion As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_AsignarPuesto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_LISTA_TRABAJO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, usuarioActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto.ToUpper()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion.ToUpper()))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Shared Sub ActualizarPuestoAsignado(identificadorRemesa As String, identificadorBulto As String, codigoPuesto As String, usuarioActualizacion As String, fechaActualizacion As DateTime)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ActualizarPuestoAsignado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto.ToUpper()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, usuarioActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub ActualizarEstado(identificadorBulto As String, _
                                           estadoBulto As String, _
                                           usuarioActualizacion As String, _
                                           fechaActualizacion As DateTime,
                                           EsActualizarUsuarioBloqueio As Boolean, _
                                           CodigoUsuarioBloqueo As String,
                                           Optional codigoprecinto As String = "",
                                           Optional codigoBolsa As String = "")

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Bulto_ActualizarEstado
            cmd.CommandType = CommandType.Text
            Dim Query As String = String.Empty

            If Not String.IsNullOrEmpty(codigoprecinto) Then
                Query = ", COD_PRECINTO_BULTO = []COD_PRECINTO_BULTO"
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, codigoprecinto))
            End If

            If Not String.IsNullOrEmpty(codigoBolsa) Then
                Query &= ", COD_BOLSA = []COD_BOLSA "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_BOLSA", ProsegurDbType.Identificador_Alfanumerico, codigoBolsa))
            End If

            If EsActualizarUsuarioBloqueio Then
                Query &= " ,COD_USUARIO_BLOQUEO = []COD_USUARIO_BLOQUEO "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, If(String.IsNullOrEmpty(CodigoUsuarioBloqueo), DBNull.Value, CodigoUsuarioBloqueo)))
            End If

            cmd.CommandText = String.Format(cmd.CommandText, Query)

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_BULTO", ProsegurDbType.Identificador_Alfanumerico, estadoBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, usuarioActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub ActualizarCuadrado(precintosBultos As List(Of String), _
                                           usuarioActualizacion As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Bulto_ActualizarBolCuadrado
            cmd.CommandType = CommandType.Text

            cmd.CommandText += Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, precintosBultos, "COD_PRECINTO_BULTO", cmd, "WHERE", "B")

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, usuarioActualizacion))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Recupera precintos que existem na base de dados
        ''' </summary>
        ''' <param name="Precintos">Lista de precintos. OBS: Obrigatório</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarPrecintosDuplicados(Precintos As ObservableCollection(Of String)) As System.Text.StringBuilder

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandText = My.Resources.Salidas_Bulto_RecuperarPrecintos
            comando.CommandType = CommandType.Text
            comando.CommandText = String.Format(comando.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, Precintos, "COD_PRECINTO_BULTO", comando, "WHERE", "B"))

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim PrecintosExistentes As New System.Text.StringBuilder

                For Each row In dt.Rows
                    PrecintosExistentes.AppendLine(row("COD_PRECINTO_BULTO"))
                Next row
                Return PrecintosExistentes
            End If

            Return New System.Text.StringBuilder

        End Function

        Public Shared Function RecuperarPrecintosDuplicadosBultosYObjetos(IdentificadorBulto As String,
                                                                          IdentificadorObjeto As String,
                                                                          Precintos As ObservableCollection(Of String)) As System.Text.StringBuilder

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandText = My.Resources.Salidas_Bulto_RecuperarPrecintos
            comando.CommandType = CommandType.Text
            comando.CommandText = String.Format(comando.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, Precintos, "COD_PRECINTO_BULTO", comando, "WHERE", "B"))

            If Not String.IsNullOrEmpty(IdentificadorBulto) Then
                comando.CommandText &= " AND B.OID_BULTO <> []OID_BULTO"
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))
            End If

            comando.CommandText &= " UNION SELECT COD_IDENTIFICADOR FROM GEPR_TOBJETO O "
            comando.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, Precintos, "COD_IDENTIFICADOR", comando, "WHERE", "O")

            If Not String.IsNullOrEmpty(IdentificadorObjeto) Then
                comando.CommandText &= " AND O.OID_OBJETO <> []OID_OBJETO"
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_OBJETO", ProsegurDbType.Objeto_Id, IdentificadorObjeto))
            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim PrecintosExistentes As New System.Text.StringBuilder

                For Each row In dt.Rows
                    PrecintosExistentes.AppendLine(row("COD_PRECINTO_BULTO"))
                Next row
                Return PrecintosExistentes
            End If

            Return New System.Text.StringBuilder

        End Function
        
        Public Shared Function HayCodigoCajetinRemesa(identificadorRemesa As String, identificadorBulto As String, codigoCajetin As String) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_HayCodigoCajetinRemesa.ToString)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_BOLSA", ProsegurDbType.Objeto_Id, codigoCajetin))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        ''' <summary>
        ''' Verifica si se puede modificar el estado del bulto. Sólo si puede poner En Curso (EC) un bulto con estado ‘PE’ o ‘AS’. 
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ValidarModificarEstadoBulto(identificadorBulto As String) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ValidarModificarEstadoBulto)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        ''' <summary>
        ''' 4.	Si el estado del bulto fue actualizado para ‘AS’ entonces verifica si se debe volver el estado de la remesa para “AS”.
        ''' 'PR', 'SA', 'EN'
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ValidarModificarEstadoRemesa(identificadorRemesa As String) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ValidarModificarEstadoRemesa)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        ''' <summary>
        ''' Método que deleta todos os efectivos de um bulto
        ''' </summary>
        ''' <param name="oidBulto"></param>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub BorrarEfectivosBulto(oidBulto As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_BorrarEfectivosBulto)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, oidBulto))
            End With

            'Apaga a remesa do banco
            DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Método que atualizar somente um bulto
        ''' </summary>
        ''' <param name="oidBulto"></param>
        ''' <param name="CodPrecintoBulto"></param>
        ''' <param name="OidTipoBulto"></param>
        ''' <param name="CodUsuario"></param>
        ''' <param name="fechaActualizacion"></param>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarUnBulto(oidBulto As String, _
                                            CodPrecintoBulto As String, _
                                            OidTipoBulto As String, _
                                            CodUsuario As String, _
                                            fechaActualizacion As DateTime,
                                            Optional ByRef Transaccion As Transacao = Nothing)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ActualizarBulto)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, oidBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, CodPrecintoBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_TIPO_BULTO", ProsegurDbType.Objeto_Id, OidTipoBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaActualizacion))
            End With

            'Apaga a remesa do banco
            If Transaccion IsNot Nothing Then
                Transaccion.AdicionarItemTransacao(cmd)
            Else
                DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)
            End If

        End Sub

        ''' <summary>
        ''' Função que verifica se um bulto foi atualizado
        ''' </summary>
        ''' <param name="oidBulto"></param>
        ''' <param name="FyhActualizacion"></param>
        ''' <returns></returns>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function VerificarActualizacionBulto(oidBulto As String, _
                                                           FyhActualizacion As Date) As Integer

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_VerificarActualizacionBulto)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, oidBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, FyhActualizacion))
            End With

            Return DbHelper.AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        ''' <summary>
        ''' Método que insere um efectivo bulto no banco de dados
        ''' </summary>
        ''' <param name="OidBulto"></param>
        ''' <param name="OidDenominacion"></param>
        ''' <param name="OidModuloBulto"></param>
        ''' <param name="NelCantidad"></param>
        ''' <param name="NecAgrupacion"></param>
        ''' <param name="CodUsuario"></param>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub InsertarEfectivoBulto(OidBulto As String, _
                                                OidDenominacion As String, _
                                                OidModuloBulto As String, _
                                                NelCantidad As Int64, _
                                                NecAgrupacion As Nullable(Of Integer), _
                                                CodUsuario As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_InsertarEfectivoBulto)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_EFECTIVO_BULTO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString()))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, OidBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, OidDenominacion))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_MODULO_BULTO", ProsegurDbType.Objeto_Id, OidModuloBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, NelCantidad))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEC_AGRUPACION", ProsegurDbType.Inteiro_Longo, NecAgrupacion))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
            End With

            'Apaga a remesa do banco
            DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Método que insere um bulto
        ''' </summary>
        ''' <param name="IdentificadorBulto"></param>
        ''' <param name="IdentificadorTipoBulto"></param>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <param name="CodigoPrecintoBulto"></param>
        ''' <param name="CodigoEstadoBulto"></param>
        ''' <param name="EsPreparado"></param>
        ''' <param name="CodigoUsuario"></param>
        ''' <param name="FechaActualizacion"></param>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub InsertarBulto(IdentificadorBulto As String,
                                        IdentificadorTipoBulto As String,
                                        IdentificadorRemesa As String,
                                        CodigoPrecintoBulto As String,
                                        CodigoEstadoBulto As String,
                                        EsPreparado As Boolean,
                                        CodigoUsuario As String,
                                        CodigoUsuarioBloqueo As String,
                                        FechaActualizacion As DateTime,
                                        Optional ByRef Transaccion As Transacao = Nothing)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_InsertarBulto)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_TIPO_BULTO", ProsegurDbType.Objeto_Id, IdentificadorTipoBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, CodigoPrecintoBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_BULTO", ProsegurDbType.Identificador_Alfanumerico, CodigoEstadoBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "BOL_PREPARADO", ProsegurDbType.Logico, EsPreparado))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuarioBloqueo))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, FechaActualizacion))

            End With

            'Apaga a remesa do banco
            If Transaccion IsNot Nothing Then
                Transaccion.AdicionarItemTransacao(cmd)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)
            End If

        End Sub

        ''' <summary>
        ''' Método que deleta todos os bultos de uma remesa que não
        ''' foram informados na atualização de bultos
        ''' </summary>
        ''' <param name="remesa"></param>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub BorrarBultosDeUnaRemesa(remesa As Clases.Remesa)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_BorrarBultosDeUnaRemesa)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, remesa.CodigoDelegacion))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA_LEGADO", ProsegurDbType.Objeto_Id, remesa.IdentificadorExterno))
            End With

            'Apaga a remesa do banco
            DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Function ActualizarEstadoBultoConcorrencia(IdentificadorBulto As String, _
                                                CodigoEstadoBulto As String, _
                                                CodigoUsuario As String, _
                                                FyhActualizacion As DateTime, _
                                                CodigoUsuarioBloqueo As String) As Int16

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            'cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, "UPDATE GEPR_TBULTO SET COD_ESTADO_BULTO = []COD_ESTADO_BULTO, COD_USUARIO = []COD_USUARIO, FYH_ACTUALIZACION = []FYH_ACTUALIZACION, COD_USUARIO_BLOQUEO = []COD_USUARIO_BLOQUEO  WHERE OID_BULTO = []OID_BULTO AND COD_ESTADO_BULTO <> []COD_ESTADO_BULTO")
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_ActualizarEstadoBultoConcorrencia)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_BULTO", ProsegurDbType.Objeto_Id, CodigoEstadoBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, CodigoUsuario))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Descricao_Curta, If(String.IsNullOrEmpty(CodigoUsuarioBloqueo), DBNull.Value, CodigoUsuarioBloqueo)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, FyhActualizacion))

            Return AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        Public Shared Function BultosBloqueadosPorOutroUsuario(Bultos As ObservableCollection(Of Clases.Bulto),
                                                               CodigoUsuarioBloqueio As String) As ObservableCollection(Of Clases.Bulto)

            Dim objBultos As New ObservableCollection(Of Clases.Bulto)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            Dim identificadoresBultos As List(Of String) = (From r In Bultos Select r.Identificador).ToList

            cmd.CommandText = My.Resources.Salidas_Bulto_RecuperarBloqueadosPorOutroUsuario
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuarioBloqueio))
            cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresBultos, "OID_BULTO", cmd, "AND"))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            ' Para cada linha retornada
            For Each dr As DataRow In AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd).Rows
                objBultos.Add(New Clases.Bulto With
                               {
                                  .Identificador = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String)),
                                  .Estado = Extenciones.RecuperarEnum(Of EstadoBulto)(Util.AtribuirValorObj(dr("COD_ESTADO_BULTO"), GetType(String))),
                                  .UsuarioBloqueo = Util.AtribuirValorObj(dr("COD_USUARIO_BLOQUEO"), GetType(String))
                               })

            Next

            Return objBultos

        End Function

        Public Shared Sub BloquearBulto(identificadorBulto As String, CodigoUsuarioBloqueio As String)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_BloquearBulto)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuarioBloqueio))
            End With

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub BloquearBultoRemesa(identificadoresBulto As List(Of String), CodigoUsuarioBloqueio As String)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(My.Resources.Salidas_Bulto_BloquearBultoRemesa,
                                                                                            Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS,
                                                                                                                  identificadoresBulto,
                                                                                                                  "OID_BULTO",
                                                                                                                  cmd,
                                                                                                                  "AND", "B")))
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuarioBloqueio))

            End With

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub DesBloquearBulto(IdentificadorBulto As String, CodigoUsuarioBloqueio As String)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_DesBloquearBulto)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuarioBloqueio))
            End With

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub ActualizarBultoTipoMercancia(identificadorBultoBorrar As String, identificadorBultoActualizar As String)

            ' recupera os tipos de mercancia do malote que será excluído
            Dim tiposmercanciabultoborrado As ObservableCollection(Of Clases.TipoMercancia) = TipoMercancia.RecuperarTiposMercanciaBulto(identificadorBultoBorrar)

            If tiposmercanciabultoborrado IsNot Nothing AndAlso tiposmercanciabultoborrado.Count > 0 Then

                ' recupera os tipos de mercancia do malote que será actualizado
                Dim tiposmercanciabultoactualizado As ObservableCollection(Of Clases.TipoMercancia) = TipoMercancia.RecuperarTiposMercanciaBulto(identificadorBultoActualizar)

                Dim tiposmercanciadiferencia As New ObservableCollection(Of Clases.TipoMercancia)

                If tiposmercanciabultoactualizado IsNot Nothing AndAlso tiposmercanciabultoactualizado.Count > 0 Then

                    ' recupera os tipos de mercancia que tem no bulto a ser excluido e não tem no bulto a ser atualizado
                    tiposmercanciadiferencia = (From tb In tiposmercanciabultoborrado
                                                From ta In tiposmercanciabultoactualizado
                                                Where tb.Identificador <> ta.Identificador
                                                Select tb).ToObservableCollection

                    If tiposmercanciadiferencia IsNot Nothing AndAlso tiposmercanciadiferencia.Count > 0 Then

                        For Each tp In tiposmercanciadiferencia
                            TipoMercancia.InsertarTipoMercanciaBulto(tp.Identificador, identificadorBultoActualizar)
                        Next tp

                    End If

                Else

                    For Each tp In tiposmercanciabultoborrado
                        TipoMercancia.InsertarTipoMercanciaBulto(tp.Identificador, identificadorBultoActualizar)
                    Next tp

                End If

            End If

        End Sub

        ''' <summary>
        ''' Atualiza o valor da coluna OID_BULTO na tabela GEPR_TEFECTIVO_BULTO, para que o bulto possa ser excluido
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBultoBorrar"></param>
        ''' <param name="identificadorBultoActualizar"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarBultoEfectivo(identificadorRemesa As String,
                                                  identificadorBultoBorrar As String,
                                                  ByRef identificadorBultoActualizar As String)


            Dim identificadoresEfectivosBorrar As List(Of String) = Nothing
            ' busca quantidade de efetivos que o bulto possui
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = My.Resources.Salidas_Bulto_RecuperarCantidadEfectivoBulto
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBultoBorrar))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
            Dim dtEfectivosBultoBorrar As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            ' se existirem efetivos vinculados ao bulto que será excluido, 
            ' busca outro bulto que tenha efetivo vinculado
            If dtEfectivosBultoBorrar.Rows.Count > 0 Then
                identificadoresEfectivosBorrar = New List(Of String)
                For Each row In dtEfectivosBultoBorrar.Rows
                    identificadoresEfectivosBorrar.Add(row("OID_EFECTIVO_BULTO"))
                Next row

                cmd = Nothing
                cmd = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                cmd.CommandText = My.Resources.Salidas_Bulto_RecuperarBultoConEfectivo
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBultoBorrar))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

                Dim dtBultoConEfectivo As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

                ' se existir outros bultos que tenham efetivos vinculados
                If dtBultoConEfectivo IsNot Nothing AndAlso dtBultoConEfectivo.Rows.Count > 0 Then
                    identificadorBultoActualizar = dtBultoConEfectivo.Rows(0)("OID_BULTO")

                    ' efetivos do bulto que será excluído
                    For Each rowefectivobultoborrar In dtEfectivosBultoBorrar.Rows

                        Dim cantidadFinalEfectivo As Integer = 0
                        Dim identificadorEfectivoBulto As String = String.Empty

                        ' efetivos do bulto que será excluído
                        For Each rowefectivoconbulto In dtBultoConEfectivo.Rows

                            If rowefectivoconbulto("COD_DENOMINACION") = rowefectivobultoborrar("COD_DENOMINACION") Then
                                identificadorEfectivoBulto = rowefectivoconbulto("OID_EFECTIVO_BULTO")
                                cantidadFinalEfectivo = rowefectivoconbulto("NEL_CANTIDAD") + rowefectivobultoborrar("NEL_CANTIDAD")
                                Exit For

                            End If

                        Next

                        cmd = Nothing
                        cmd = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                        cmd.CommandText = My.Resources.Salidas_Bulto_ActualizarBultoEfectivoBultoPorDenominacion
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBultoActualizar))

                        If cantidadFinalEfectivo > 0 Then
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_EFECTIVO_BULTO", ProsegurDbType.Objeto_Id, identificadorEfectivoBulto))
                            cmd.CommandText = String.Format(cmd.CommandText, ", EB.NEL_CANTIDAD = []NEL_CANTIDAD ")
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, cantidadFinalEfectivo))

                        Else
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_EFECTIVO_BULTO", ProsegurDbType.Objeto_Id, rowefectivobultoborrar("OID_EFECTIVO_BULTO")))
                            cmd.CommandText = String.Format(cmd.CommandText, "")
                        End If

                        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

                    Next

                Else

                    For Each identificadorefectivobulto In identificadoresEfectivosBorrar

                        cmd = Nothing
                        cmd = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                        cmd.CommandText = String.Format(My.Resources.Salidas_Bulto_ActualizarBultoEfectivoBultoPorDenominacion, "")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBultoActualizar))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_EFECTIVO_BULTO", ProsegurDbType.Objeto_Id, identificadorefectivobulto))
                        'cmd.CommandText = My.Resources.Salidas_Bulto_ActualizarBultoEfectivoBulto
                        'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBultoActualizar))
                        'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO_BORRADO", ProsegurDbType.Objeto_Id, identificadorBultoBorrar))
                        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

                    Next identificadorefectivobulto

                End If

            End If

        End Sub

        Public Shared Sub BorrarBulto(identificadorBulto As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = My.Resources.Salidas_Bulto_BorrarBulto
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub BorrarBultoListaTrabajo(identificadorBulto As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = My.Resources.Salidas_Bulto_BorrarListaTrabajo
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub ActualizarCodigoBolsa(identificadorBulto As String, codigoBolsa As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = My.Resources.Salidas_Bulto_ActualizarCodigoBolsa
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_BOLSA", ProsegurDbType.Objeto_Id, codigoBolsa))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)
        End Sub

        Public Shared Function ObtenerDivisasBultosProcesados(codigoDelegacion As String, codigoSectorPuesto As String, identificadoresDenominacionBulto As List(Of String), gestionaSaldoPorPuesto As Boolean) As ObservableCollection(Of Clases.Divisa)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Bulto_ObtenerDivisasBultosProcesados
            cmd.CommandType = CommandType.Text

            Dim sqlInner As String = String.Empty
            Dim where As String = String.Empty

            If gestionaSaldoPorPuesto Then
                sqlInner = " INNER JOIN GEPR_TLISTA_TRABAJO LT ON LT.OID_BULTO = B.OID_BULTO "
                sqlInner += " INNER JOIN GEPR_TPUESTO P ON P.OID_PUESTO = LT.OID_PUESTO "

                where = " AND P.COD_PUESTO = []COD_PUESTO "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoSectorPuesto))
            Else
                where = " AND R.COD_SECTOR = []COD_SECTOR "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, codigoSectorPuesto))
            End If

            where += Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresDenominacionBulto, "OID_DENOMINACION", cmd, "AND", "DEN")

            cmd.CommandText = String.Format(cmd.CommandText, sqlInner, where)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            divisas = PreencherObtenerDivisasBultosProcesados(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd))

            Return divisas

        End Function

        Private Shared Function PreencherObtenerDivisasBultosProcesados(dt As DataTable) As ObservableCollection(Of Clases.Divisa)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each row In dt.Rows

                    Dim divisa As Clases.Divisa = divisas.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(row("OID_DIVISA"), GetType(String)))
                    Dim denominacion As Clases.Denominacion

                    If divisa Is Nothing Then
                        divisa = New Clases.Divisa
                        divisa.Identificador = Util.AtribuirValorObj(row("OID_DIVISA"), GetType(String))
                        divisa.CodigoISO = Util.AtribuirValorObj(row("COD_ISO_DIVISA"), GetType(String))

                        divisas.Add(divisa)
                    End If

                    If divisa.Denominaciones Is Nothing Then
                        divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)
                    End If

                    denominacion = divisa.Denominaciones.FirstOrDefault(Function(den) den.Identificador = Util.AtribuirValorObj(row("OID_DENOMINACION"), GetType(String)))

                    If denominacion Is Nothing Then

                        denominacion = New Clases.Denominacion With
                                       {.Identificador = Util.AtribuirValorObj(row("OID_DENOMINACION"), GetType(String)),
                                        .Codigo = Util.AtribuirValorObj(row("COD_DENOMINACION"), GetType(String))}

                        denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)

                        Dim valor As New Clases.ValorDenominacion With
                            {.Cantidad = Util.AtribuirValorObj(row("CANTIDAD"), GetType(Long)),
                             .Importe = Util.AtribuirValorObj(row("CANTIDAD"), GetType(Long)) * Util.AtribuirValorObj(row("NUM_VALOR_FACIAL"), GetType(Double))}

                        denominacion.ValorDenominacion.Add(valor)

                        divisa.Denominaciones.Add(denominacion)

                    Else

                        If denominacion.ValorDenominacion Is Nothing Then

                            denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)

                            Dim valor As New Clases.ValorDenominacion With
                            {.Cantidad = Util.AtribuirValorObj(row("CANTIDAD"), GetType(Long)),
                             .Importe = Util.AtribuirValorObj(row("CANTIDAD"), GetType(Long)) * Util.AtribuirValorObj(row("NUM_VALOR_FACIAL"), GetType(Double))}

                            denominacion.ValorDenominacion.Add(valor)
                        Else
                            denominacion.ValorDenominacion(0).Cantidad += Util.AtribuirValorObj(row("CANTIDAD"), GetType(Long))
                            denominacion.ValorDenominacion(0).Importe += (Util.AtribuirValorObj(row("CANTIDAD"), GetType(Long)) * Util.AtribuirValorObj(row("NUM_VALOR_FACIAL"), GetType(Double)))
                        End If

                    End If

                Next

            End If

            Return divisas
        End Function

        Public Shared Sub ActualizarPrecintoSalidas(bulto As Clases.Bulto)

            If bulto IsNot Nothing AndAlso bulto.Precintos IsNot Nothing AndAlso bulto.Precintos.Count > 0 Then

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                cmd.CommandType = CommandType.Text
                cmd.CommandText = My.Resources.Salidas_Bulto_ActualizarPrecintos

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Identificador_Alfanumerico, bulto.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, bulto.Precintos.FirstOrDefault))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, bulto.UsuarioModificacion))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

            End If

        End Sub

        Public Shared Sub AnularBultosRemesa(remesa As Clases.Remesa, codigoUsuario As String, fechahoraActualizacion As DateTime)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandType = CommandType.Text
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Bulto_AnularBultosRemesa)

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_BULTO", ProsegurDbType.Descricao_Curta, remesa.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Longa, codigoUsuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechahoraActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, remesa.Identificador))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)
        End Sub
    End Class

End Namespace