Imports System.Collections.Generic
Imports Prosegur.DbHelper
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global.Saldos.Negocio.Enumeradores

<Serializable()> _
Public Class Documentos
    Inherits List(Of Documento)

#Region "[VARIÁVEIS]"

    Private _IdGrupo As Integer
    Private _Detalles As Detalles

#End Region

#Region "[PROPRIEDADES]"

    Public Property Detalles() As Detalles
        Get
            If _Detalles Is Nothing Then
                _Detalles = New Detalles()
            End If
            Detalles = _Detalles
        End Get
        Set(Value As Detalles)
            _Detalles = Value
        End Set
    End Property

    Public Property IdGrupo() As Integer
        Get
            IdGrupo = _IdGrupo
        End Get
        Set(Value As Integer)
            _IdGrupo = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém os documentos através do Id do grupo.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 29/05/2009 Criado
    ''' </history>
    Public Sub Realizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.GrupoDocumentosRealizar.ToString
        comando.CommandType = CommandType.Text

        ' adicionar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGrupo", ProsegurDbType.Inteiro_Longo, Me.IdGrupo))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objDocumento As Documento = Nothing

            ' para cada registro encontrado
            For Each dr As DataRow In dt.Rows

                ' criar nova instancia
                objDocumento = New Documento

                If dr("IdGrupo") IsNot DBNull.Value Then
                    objDocumento.Grupo.Id = dr("IdGrupo")
                Else
                    objDocumento.Grupo.Id = 0
                End If

                If dr("IdOrigen") IsNot DBNull.Value Then
                    objDocumento.Origen.Id = dr("IdOrigen")
                Else
                    objDocumento.Origen.Id = 0
                End If

                If dr("IdEstadoComprobante") IsNot DBNull.Value Then
                    objDocumento.EstadoComprobante.Id = dr("IdEstadoComprobante")
                Else
                    objDocumento.EstadoComprobante.Id = 0
                End If

                If dr("IdUsuario") IsNot DBNull.Value Then
                    objDocumento.Usuario.Id = dr("IdUsuario")
                Else
                    objDocumento.Usuario.Id = 0
                End If

                If dr("IdUsuarioResuelve") IsNot DBNull.Value Then
                    objDocumento.UsuarioResolutor.Id = dr("IdUsuarioResuelve")
                Else
                    objDocumento.UsuarioResolutor.Id = 0
                End If

                If dr("FechaResuelve") IsNot DBNull.Value Then
                    objDocumento.FechaResolucion = Convert.ToDateTime(dr("FechaResuelve"))
                Else
                    objDocumento.FechaResolucion = DateTime.MinValue
                End If

                If dr("FechaGestion") IsNot DBNull.Value Then
                    objDocumento.FechaGestion = Convert.ToDateTime(dr("FechaGestion"))
                Else
                    objDocumento.FechaGestion = DateTime.MinValue
                End If

                If dr("FechaDispone") IsNot DBNull.Value Then
                    objDocumento.FechaDispone = Convert.ToDateTime(dr("FechaDispone"))
                Else
                    objDocumento.FechaDispone = DateTime.MinValue
                End If

                If dr("Fecha") IsNot DBNull.Value Then
                    objDocumento.Fecha = Convert.ToDateTime(dr("Fecha"))
                Else
                    objDocumento.Fecha = DateTime.MinValue
                End If

                If dr("IdUsuarioDispone") IsNot DBNull.Value Then
                    objDocumento.UsuarioDispone.Id = dr("IdUsuarioDispone")
                Else
                    objDocumento.UsuarioDispone.Id = 0
                End If

                If dr("IdFormulario") IsNot DBNull.Value Then
                    objDocumento.Formulario.Id = dr("IdFormulario")
                Else
                    objDocumento.Formulario.Id = 0
                End If

                objDocumento.ReintentosConteo = dr("reintentos_conteo")

                ' este pedaço de código talves poderá ser removido futuramente, 
                ' pois não está executando nada
                ' ------------------------------------------
                If objDocumento.Formulario IsNot Nothing _
                    AndAlso objDocumento.Formulario.Campos IsNot Nothing _
                    AndAlso objDocumento.Formulario.Campos.Count > 0 Then

                    For Each objCampo As Campo In objDocumento.Formulario.Campos
                        Select Case objCampo.Tipo
                            Case "I"
                                If dr("Id" & objCampo.Nombre) IsNot DBNull.Value Then
                                    objCampo.IdValor = dr("Id" & objCampo.Nombre)
                                End If
                                If dr(objCampo.Nombre) IsNot DBNull.Value Then
                                    objCampo.Valor = dr(objCampo.Nombre)
                                End If
                            Case "A"
                                If dr(objCampo.Nombre) IsNot DBNull.Value Then
                                    objCampo.Valor = dr(objCampo.Nombre)
                                End If
                            Case "F"
                                If dr(objCampo.Nombre) IsNot DBNull.Value Then
                                    objCampo.Valor = dr(objCampo.Nombre)
                                Else
                                    objCampo.Valor = DateTime.MinValue
                                End If
                        End Select
                    Next

                End If

                If objDocumento.Formulario.ConValores Then
                    objDocumento.Detalles.Realizar()
                End If
                ' ------------------------------------------

                ' adiciona documento para documentos
                Me.Add(objDocumento)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Obtém os documentos a serem enviados para o Conteo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 05/08/2009 Criado
    ''' </history>
    Public Sub DocumentosEnviarConteoRealizar(CantidadReintentos As Integer, CantidadDias As Integer)

        Dim campo As Campo = Nothing
        Dim Campos As Campos = Nothing
        Dim IdDocDetalles As Long = Nothing
        Dim IdDocBultos As Long = Nothing
        Dim IdDocCamposExtra As Long = Nothing

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.DocumentosEnviarConteoRealizar.ToString
        comando.CommandType = CommandType.Text

        If CantidadDias > 0 Then

            comando.CommandText = String.Format(comando.CommandText, "AND DC.FechaResuelve >= TRUNC(SYSDATE - :CantidadDias) ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CantidadDias", ProsegurDbType.Inteiro_Curto, CantidadDias))

        Else

            comando.CommandText = String.Format(comando.CommandText, String.Empty)

        End If

        ' adicionar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "reintentos_conteo", ProsegurDbType.Inteiro_Longo, CantidadReintentos))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Negocio.Util.LogMensagemEmDisco("INÍCIO MONTAR COLEÇAO REMESAS", "ENVIAR_SALDOS_CONTEO_LOG.txt")

            Dim objDocumento As Documento = Nothing

            ' para cada registro encontrado
            For Each dr As DataRow In dt.Rows

                ' criar nova instancia
                objDocumento = New Documento

                objDocumento.Id = dr("IdDocumento")
                objDocumento.Usuario.Id = dr("IdUsuario")
                objDocumento.Fecha = dr("Fecha")
                objDocumento.IdDocumentoLegado = dr("IdDocumentoLegado")

                If dr("NumComprobante") Is DBNull.Value Then
                    objDocumento.NumComprobante = ""
                Else
                    objDocumento.NumComprobante = dr("NumComprobante")
                End If

                If dr("IdUsuarioResolutor") Is DBNull.Value Then
                    objDocumento.UsuarioResolutor.Id = 0
                Else
                    objDocumento.UsuarioResolutor.Id = dr("IdUsuarioResolutor")
                End If

                If dr("FechaResolucion") Is DBNull.Value Then
                    objDocumento.FechaResolucion = Date.MinValue
                Else
                    objDocumento.FechaResolucion = dr("FechaResolucion")
                End If

                If dr("IdUsuarioDispone") Is DBNull.Value Then
                    objDocumento.UsuarioDispone.Id = 0
                Else
                    objDocumento.UsuarioDispone.Id = dr("IdUsuarioDispone")
                End If

                If dr("FechaDispone") Is DBNull.Value Then
                    objDocumento.FechaDispone = Date.MinValue
                Else
                    objDocumento.FechaDispone = dr("FechaDispone")
                End If

                'realiza formulario
                objDocumento.Formulario.Id = dr("IdFormulario")
                objDocumento.Formulario.Realizar()

                'realiza estadoComprobante
                objDocumento.EstadoComprobante.Id = dr("IdEstadoComprobante")
                objDocumento.EstadoComprobante.Realizar()

                If dr("FechaGestion") Is DBNull.Value Then
                    objDocumento.FechaGestion = Date.MinValue
                Else
                    objDocumento.FechaGestion = dr("FechaGestion")
                End If

                If dr("Agrupado") Is DBNull.Value Then
                    objDocumento.Agrupado = False
                Else
                    objDocumento.Agrupado = CType(dr("Agrupado"), Boolean)
                End If

                If dr("EsGrupo") Is DBNull.Value Then
                    objDocumento.EsGrupo = False
                Else
                    objDocumento.EsGrupo = CType(dr("EsGrupo"), Boolean)
                End If

                If dr("IdGrupo") Is DBNull.Value Then
                    objDocumento.Grupo.Id = 0
                Else
                    objDocumento.Grupo.Id = dr("IdGrupo")
                End If

                If dr("IdOrigen") Is DBNull.Value Then
                    objDocumento.Origen.Id = 0
                Else
                    objDocumento.Origen.Id = dr("IdOrigen")
                End If

                If dr("Reenviado") Is DBNull.Value Then
                    objDocumento.Reenviado = False
                Else
                    objDocumento.Reenviado = CType(dr("Reenviado"), Boolean)
                End If

                If dr("Disponible") Is DBNull.Value Then
                    objDocumento.Disponible = False
                Else
                    objDocumento.Disponible = CType(dr("Disponible"), Boolean)
                End If

                If dr("Sustituido") Is DBNull.Value Then
                    objDocumento.Sustituido = False
                Else
                    objDocumento.Sustituido = CType(dr("Sustituido"), Boolean)
                End If

                If dr("EsSustituto") Is DBNull.Value Then
                    objDocumento.EsSustituto = False
                Else
                    objDocumento.EsSustituto = CType(dr("EsSustituto"), Boolean)
                End If

                If dr("IdSustituto") Is DBNull.Value Then
                    objDocumento.Sustituto.Id = 0
                Else
                    objDocumento.Sustituto.Id = dr("IdSustituto")
                End If

                If dr("IdPrimordial") Is DBNull.Value Then
                    objDocumento.Primordial.Id = 0
                Else
                    objDocumento.Primordial.Id = dr("IdPrimordial")
                End If

                If dr("Importado") Is DBNull.Value Then
                    objDocumento.Importado = False
                Else
                    objDocumento.Importado = CType(dr("Importado"), Boolean)
                End If

                If dr("Exportado") Is DBNull.Value Then
                    objDocumento.Exportado = False
                Else
                    objDocumento.Exportado = CType(dr("Exportado"), Boolean)
                End If

                If dr("ArchivoRemesaLegado") IsNot DBNull.Value Then
                    objDocumento.ArchivoRemesaLegado = dr("ArchivoRemesaLegado")
                End If

                If dr("reintentos_conteo") IsNot DBNull.Value Then
                    objDocumento.ReintentosConteo = dr("reintentos_conteo")
                Else
                    objDocumento.ReintentosConteo = 0
                End If

                Campos = objDocumento.Formulario.Campos

                If Campos IsNot Nothing Then

                    For Each c As Campo In Campos

                        Select Case c.Tipo

                            Case "I"

                                If dr("Id" & c.Nombre) Is DBNull.Value Then
                                    c.IdValor = 0
                                Else
                                    c.IdValor = Convert.ToInt32(dr("Id" & c.Nombre))
                                End If

                                If dr(c.Nombre) IsNot DBNull.Value Then
                                    c.Valor = dr(c.Nombre)
                                Else
                                    c.Valor = String.Empty
                                End If

                            Case "A"

                                If dr(c.Nombre) Is DBNull.Value Then
                                    c.Valor = String.Empty
                                Else
                                    c.Valor = dr(c.Nombre)
                                End If

                            Case "F"

                                If dr(c.Nombre) Is DBNull.Value Then
                                    c.Valor = String.Empty
                                Else
                                    c.Valor = dr(c.Nombre)
                                End If

                        End Select

                    Next

                End If

                objDocumento.Formulario.Campos = Campos

                If objDocumento.Formulario.ConValores Then
                    IdDocDetalles = dr("IdDocDetalles")
                    If IdDocDetalles > 0 Then
                        objDocumento.Detalles.Documento.Id = IdDocDetalles
                    Else
                        objDocumento.Detalles.Documento.Id = objDocumento.Id
                    End If
                End If

                IdDocCamposExtra = dr("IdDocCamposExtra")

                If IdDocCamposExtra > 0 Then
                    objDocumento.Formulario.CamposExtra.Documento.Id = IdDocCamposExtra
                Else
                    objDocumento.Formulario.CamposExtra.Documento.Id = objDocumento.Id
                End If

                IdDocBultos = dr("IdDocBultos")
                If objDocumento.Formulario.ConBultos Then
                    If IdDocBultos > 0 Then
                        objDocumento.Bultos.Documento.Id = IdDocBultos
                    Else
                        objDocumento.Bultos.Documento.Id = objDocumento.Id
                    End If
                End If

                If objDocumento.Formulario.EsActaProceso Then
                    If objDocumento.Origen.Id > 0 Then objDocumento.Origen.Realizar()
                    objDocumento.Sobres.Documento.Id = objDocumento.Id
                    objDocumento.Sobres.Realizar()
                End If

                Me.Add(objDocumento)

            Next

            Negocio.Util.LogMensagemEmDisco("INÍCIO MONTAR COLEÇAO REMESAS", "ENVIAR_SALDOS_CONTEO_LOG.txt")

        End If

    End Sub

    ''' <summary>
    ''' Obtém os documentos a serem enviados para o Conteo por IdDocumento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 10/03/2011 Criado
    ''' </history>
    Public Sub DocumentosEnviarConteoRealizarByIdDocumento(IdsDocumentos As List(Of String))

        Dim campo As Campo = Nothing
        Dim Campos As Campos = Nothing
        Dim IdDocDetalles As Long = Nothing
        Dim IdDocBultos As Long = Nothing
        Dim IdDocCamposExtra As Long = Nothing

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text

        ' É inicializado com o valor '', sem essa inicialização é gerado um erro na execução da query...
        Dim strQueryDocumento As String = "'',"

        If IdsDocumentos IsNot Nothing AndAlso IdsDocumentos.Count > 0 Then

            For Each idDocumento As String In IdsDocumentos
                strQueryDocumento &= "'" & idDocumento + "',"
            Next

            strQueryDocumento = strQueryDocumento.Substring(0, strQueryDocumento.Length - 1)

        End If

        comando.CommandText = String.Format(My.Resources.DocumentosEnviarConteoRealizarByIdDocumento.ToString, strQueryDocumento)

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objDocumento As Documento = Nothing

            ' para cada registro encontrado
            For Each dr As DataRow In dt.Rows

                ' criar nova instancia
                objDocumento = New Documento

                objDocumento.Id = dr("IdDocumento")
                objDocumento.Usuario.Id = dr("IdUsuario")
                objDocumento.Fecha = dr("Fecha")
                objDocumento.IdDocumentoLegado = dr("IdDocumentoLegado")

                If dr("NumComprobante") Is DBNull.Value Then
                    objDocumento.NumComprobante = ""
                Else
                    objDocumento.NumComprobante = dr("NumComprobante")
                End If

                If dr("IdUsuarioResolutor") Is DBNull.Value Then
                    objDocumento.UsuarioResolutor.Id = 0
                Else
                    objDocumento.UsuarioResolutor.Id = dr("IdUsuarioResolutor")
                End If

                If dr("FechaResolucion") Is DBNull.Value Then
                    objDocumento.FechaResolucion = Date.MinValue
                Else
                    objDocumento.FechaResolucion = dr("FechaResolucion")
                End If

                If dr("IdUsuarioDispone") Is DBNull.Value Then
                    objDocumento.UsuarioDispone.Id = 0
                Else
                    objDocumento.UsuarioDispone.Id = dr("IdUsuarioDispone")
                End If

                If dr("FechaDispone") Is DBNull.Value Then
                    objDocumento.FechaDispone = Date.MinValue
                Else
                    objDocumento.FechaDispone = dr("FechaDispone")
                End If

                'realiza formulario
                objDocumento.Formulario.Id = dr("IdFormulario")
                objDocumento.Formulario.Realizar()

                'realiza estadoComprobante
                objDocumento.EstadoComprobante.Id = dr("IdEstadoComprobante")
                objDocumento.EstadoComprobante.Realizar()

                If dr("FechaGestion") Is DBNull.Value Then
                    objDocumento.FechaGestion = Date.MinValue
                Else
                    objDocumento.FechaGestion = dr("FechaGestion")
                End If

                If dr("Agrupado") Is DBNull.Value Then
                    objDocumento.Agrupado = False
                Else
                    objDocumento.Agrupado = CType(dr("Agrupado"), Boolean)
                End If

                If dr("EsGrupo") Is DBNull.Value Then
                    objDocumento.EsGrupo = False
                Else
                    objDocumento.EsGrupo = CType(dr("EsGrupo"), Boolean)
                End If

                If dr("IdGrupo") Is DBNull.Value Then
                    objDocumento.Grupo.Id = 0
                Else
                    objDocumento.Grupo.Id = dr("IdGrupo")
                End If

                If dr("IdOrigen") Is DBNull.Value Then
                    objDocumento.Origen.Id = 0
                Else
                    objDocumento.Origen.Id = dr("IdOrigen")
                End If

                If dr("Reenviado") Is DBNull.Value Then
                    objDocumento.Reenviado = False
                Else
                    objDocumento.Reenviado = CType(dr("Reenviado"), Boolean)
                End If

                If dr("Disponible") Is DBNull.Value Then
                    objDocumento.Disponible = False
                Else
                    objDocumento.Disponible = CType(dr("Disponible"), Boolean)
                End If

                If dr("Sustituido") Is DBNull.Value Then
                    objDocumento.Sustituido = False
                Else
                    objDocumento.Sustituido = CType(dr("Sustituido"), Boolean)
                End If

                If dr("EsSustituto") Is DBNull.Value Then
                    objDocumento.EsSustituto = False
                Else
                    objDocumento.EsSustituto = CType(dr("EsSustituto"), Boolean)
                End If

                If dr("IdSustituto") Is DBNull.Value Then
                    objDocumento.Sustituto.Id = 0
                Else
                    objDocumento.Sustituto.Id = dr("IdSustituto")
                End If

                If dr("IdPrimordial") Is DBNull.Value Then
                    objDocumento.Primordial.Id = 0
                Else
                    objDocumento.Primordial.Id = dr("IdPrimordial")
                End If

                If dr("Importado") Is DBNull.Value Then
                    objDocumento.Importado = False
                Else
                    objDocumento.Importado = CType(dr("Importado"), Boolean)
                End If

                If dr("Exportado") Is DBNull.Value Then
                    objDocumento.Exportado = False
                Else
                    objDocumento.Exportado = CType(dr("Exportado"), Boolean)
                End If

                If dr("ArchivoRemesaLegado") IsNot DBNull.Value Then
                    objDocumento.ArchivoRemesaLegado = dr("ArchivoRemesaLegado")
                End If

                If dr("reintentos_conteo") IsNot DBNull.Value Then
                    objDocumento.ReintentosConteo = dr("reintentos_conteo")
                Else
                    objDocumento.ReintentosConteo = 0
                End If

                Campos = objDocumento.Formulario.Campos

                If Campos IsNot Nothing Then

                    For Each c As Campo In Campos

                        Select Case c.Tipo

                            Case "I"

                                If dr("Id" & c.Nombre) Is DBNull.Value Then
                                    c.IdValor = 0
                                Else
                                    c.IdValor = Convert.ToInt32(dr("Id" & c.Nombre))
                                End If

                                If dr(c.Nombre) IsNot DBNull.Value Then
                                    c.Valor = dr(c.Nombre)
                                Else
                                    c.Valor = String.Empty
                                End If

                            Case "A"

                                If dr(c.Nombre) Is DBNull.Value Then
                                    c.Valor = String.Empty
                                Else
                                    c.Valor = dr(c.Nombre)
                                End If

                            Case "F"

                                If dr(c.Nombre) Is DBNull.Value Then
                                    c.Valor = String.Empty
                                Else
                                    c.Valor = dr(c.Nombre)
                                End If

                        End Select

                    Next

                End If

                objDocumento.Formulario.Campos = Campos

                If objDocumento.Formulario.ConValores Then
                    IdDocDetalles = dr("IdDocDetalles")
                    If IdDocDetalles > 0 Then
                        objDocumento.Detalles.Documento.Id = IdDocDetalles
                    Else
                        objDocumento.Detalles.Documento.Id = objDocumento.Id
                    End If
                End If

                IdDocCamposExtra = dr("IdDocCamposExtra")

                If IdDocCamposExtra > 0 Then
                    objDocumento.Formulario.CamposExtra.Documento.Id = IdDocCamposExtra
                Else
                    objDocumento.Formulario.CamposExtra.Documento.Id = objDocumento.Id
                End If

                IdDocBultos = dr("IdDocBultos")
                If objDocumento.Formulario.ConBultos Then
                    If IdDocBultos > 0 Then
                        objDocumento.Bultos.Documento.Id = IdDocBultos
                    Else
                        objDocumento.Bultos.Documento.Id = objDocumento.Id
                    End If
                End If

                If objDocumento.Formulario.EsActaProceso Then
                    If objDocumento.Origen.Id > 0 Then objDocumento.Origen.Realizar()
                    objDocumento.Sobres.Documento.Id = objDocumento.Id
                    objDocumento.Sobres.Realizar()
                End If

                Me.Add(objDocumento)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Verifica si ya existe un documento con el identificador de la transacción
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RealizarTransacciones(filtro As Hashtable)

        Dim campo As Campo = Nothing
        Dim Campos As Campos = Nothing
        Dim IdDocDetalles As Long = Nothing
        Dim IdDocBultos As Long = Nothing
        Dim IdDocCamposExtra As Long = Nothing
        Dim IdEspecie As Long = 0

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoCabeceraRealizarTransaciones.ToString()

        ' Adiciona os parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHAHORADESDE", ProsegurDbType.Data_Hora, If(String.IsNullOrEmpty(filtro(Constantes.CONST_FECHA_HORA_DESDE)), DBNull.Value, filtro(Constantes.CONST_FECHA_HORA_DESDE))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHAHORAHASTA", ProsegurDbType.Data_Hora, If(String.IsNullOrEmpty(filtro(Constantes.CONST_FECHA_HORA_HASTA)), DBNull.Value, filtro(Constantes.CONST_FECHA_HORA_HASTA))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDPLANTA", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_PLANTA)), DBNull.Value, filtro(Constantes.CONST_COD_PLANTA))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDCENTROPROCESO", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_SECTOR)), DBNull.Value, filtro(Constantes.CONST_COD_SECTOR))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDCANAL", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_CANAL)), DBNull.Value, filtro(Constantes.CONST_COD_CANAL))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDCLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_CLIENTE)), DBNull.Value, filtro(Constantes.CONST_COD_CLIENTE))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDMONEDA", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_MONEDA)), DBNull.Value, Integer.Parse(filtro(Constantes.CONST_COD_MONEDA)))))

        ' Se o filtro de saldo diponível foi informado
        If Not String.IsNullOrEmpty(filtro(Constantes.CONST_SOLO_SALDO_DIPONIBLE)) AndAlso filtro(Constantes.CONST_SOLO_SALDO_DIPONIBLE) IsNot Nothing Then
            comando.CommandText &= String.Format(" AND {0} = {1}", "Saldo_Disponible_###VERSION###(M.ACCION, EC.CODIGO)", If(Boolean.Parse(filtro(Constantes.CONST_SOLO_SALDO_DIPONIBLE)), 1, 0))
        End If

        ' Os registros devem esta ordenados pela FechaGestion
        comando.CommandText &= " order by DC.FECHAGESTION "

        ' Atualiza o comand text
        comando.CommandText = comando.CommandText.Replace("###VERSION###", Prosegur.Genesis.Comon.Util.Version)

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then


            For Each dr In dt.Rows

                If (From d In Me Where d.Id = dr("IdDocumento")).FirstOrDefault Is Nothing Then

                    Dim objDocumento As New Negocio.Documento

                    objDocumento.Usuario.Id = dr("IdUsuario")
                    objDocumento.Fecha = dr("Fecha")

                    If dr("IdDocumento") Is DBNull.Value Then
                        objDocumento.Id = 0
                    Else
                        objDocumento.Id = dr("IdDocumento")
                    End If

                    If dr("NumComprobante") Is DBNull.Value Then
                        objDocumento.NumComprobante = ""
                    Else
                        objDocumento.NumComprobante = dr("NumComprobante")
                    End If

                    If dr("IdUsuarioResolutor") Is DBNull.Value Then
                        objDocumento.UsuarioResolutor.Id = 0
                    Else
                        objDocumento.UsuarioResolutor.Id = dr("IdUsuarioResolutor")
                    End If

                    If dr("FechaResolucion") Is DBNull.Value Then
                        objDocumento.FechaResolucion = Date.MinValue
                    Else
                        objDocumento.FechaResolucion = dr("FechaResolucion")
                    End If

                    If dr("IdUsuarioDispone") Is DBNull.Value Then
                        objDocumento.UsuarioDispone.Id = 0
                    Else
                        objDocumento.UsuarioDispone.Id = dr("IdUsuarioDispone")
                    End If

                    If dr("FechaDispone") Is DBNull.Value Then
                        objDocumento.FechaDispone = Date.MinValue
                    Else
                        objDocumento.FechaDispone = dr("FechaDispone")
                    End If

                    'realiza formulario
                    objDocumento.Formulario.Id = dr("IdFormulario")
                    objDocumento.Formulario.Realizar()

                    'realiza estadoComprobante
                    objDocumento.EstadoComprobante.Id = dr("IdEstadoComprobante")
                    objDocumento.EstadoComprobante.Descripcion = dr("DescripcionEstadoComprobante")

                    If dr("FechaGestion") Is DBNull.Value Then
                        objDocumento.FechaGestion = Date.MinValue
                    Else
                        objDocumento.FechaGestion = dr("FechaGestion")
                    End If

                    If dr("Agrupado") Is DBNull.Value Then
                        objDocumento.Agrupado = False
                    Else
                        objDocumento.Agrupado = CType(dr("Agrupado"), Boolean)
                    End If

                    If dr("EsGrupo") Is DBNull.Value Then
                        objDocumento.EsGrupo = False
                    Else
                        objDocumento.EsGrupo = CType(dr("EsGrupo"), Boolean)
                    End If

                    If dr("IdGrupo") Is DBNull.Value Then
                        objDocumento.Grupo.Id = 0
                    Else
                        objDocumento.Grupo.Id = dr("IdGrupo")
                    End If

                    If dr("IdOrigen") Is DBNull.Value Then
                        objDocumento.Origen.Id = 0
                    Else
                        objDocumento.Origen.Id = dr("IdOrigen")
                    End If

                    If dr("Reenviado") Is DBNull.Value Then
                        objDocumento.Reenviado = False
                    Else
                        objDocumento.Reenviado = CType(dr("Reenviado"), Boolean)
                    End If

                    If dr("Disponible") Is DBNull.Value Then
                        objDocumento.Disponible = False
                    Else
                        objDocumento.Disponible = CType(dr("Disponible"), Boolean)
                    End If

                    If dr("Sustituido") Is DBNull.Value Then
                        objDocumento.Sustituido = False
                    Else
                        objDocumento.Sustituido = CType(dr("Sustituido"), Boolean)
                    End If

                    If dr("EsSustituto") Is DBNull.Value Then
                        objDocumento.EsSustituto = False
                    Else
                        objDocumento.EsSustituto = CType(dr("EsSustituto"), Boolean)
                    End If

                    If dr("IdSustituto") Is DBNull.Value Then
                        objDocumento.Sustituto.Id = 0
                    Else
                        objDocumento.Sustituto.Id = dr("IdSustituto")
                    End If

                    If dr("IdPrimordial") Is DBNull.Value Then
                        objDocumento.Primordial.Id = 0
                    Else
                        objDocumento.Primordial.Id = dr("IdPrimordial")
                    End If

                    If dr("Importado") Is DBNull.Value Then
                        objDocumento.Importado = False
                    Else
                        objDocumento.Importado = CType(dr("Importado"), Boolean)
                    End If

                    If dr("Exportado") Is DBNull.Value Then
                        objDocumento.Exportado = False
                    Else
                        objDocumento.Exportado = CType(dr("Exportado"), Boolean)
                    End If

                    If dr("SaldoDisponible") Is DBNull.Value OrElse dr("SaldoDisponible") = -1 Then
                        objDocumento.SaldoDisponible = Nothing
                    Else
                        objDocumento.SaldoDisponible = CType(dr("SaldoDisponible"), Boolean)
                    End If

                    Campos = objDocumento.Formulario.Campos

                    If Campos IsNot Nothing Then

                        For Each c As Campo In Campos

                            Select Case c.Tipo

                                Case "I"

                                    If dr("Id" & c.Nombre) Is DBNull.Value Then
                                        c.IdValor = 0
                                    Else
                                        c.IdValor = Convert.ToInt32(dr("Id" & c.Nombre))
                                    End If

                                    If dr(c.Nombre) IsNot DBNull.Value Then
                                        c.Valor = dr(c.Nombre)
                                    Else
                                        c.Valor = String.Empty
                                    End If

                                Case "A"

                                    If dr(c.Nombre) Is DBNull.Value Then
                                        c.Valor = String.Empty
                                    Else
                                        c.Valor = dr(c.Nombre)
                                    End If

                                Case "F"

                                    If dr(c.Nombre) Is DBNull.Value Then
                                        c.Valor = String.Empty
                                    Else
                                        c.Valor = dr(c.Nombre)
                                    End If

                            End Select

                        Next

                    End If

                    If objDocumento.Formulario.ConValores Then
                        IdDocDetalles = dr("IdDocDetalles")

                        If IdDocDetalles > 0 Then
                            objDocumento.Detalles.Documento.Id = IdDocDetalles
                        Else
                            objDocumento.Detalles.Documento.Id = objDocumento.Id
                        End If

                        If Not String.IsNullOrEmpty(filtro(Constantes.CONST_COD_MONEDA)) Then
                            objDocumento.Detalles.Moneda.Id = Integer.Parse(filtro(Constantes.CONST_COD_MONEDA))
                        End If

                        objDocumento.Detalles.Realizar()

                    End If

                    IdDocCamposExtra = dr("IdDocCamposExtra")

                    If IdDocCamposExtra > 0 Then
                        objDocumento.Formulario.CamposExtra.Documento.Id = IdDocCamposExtra
                    Else
                        objDocumento.Formulario.CamposExtra.Documento.Id = objDocumento.Id
                        objDocumento.Formulario.CamposExtra.Realizar()
                    End If

                    IdDocBultos = dr("IdDocBultos")
                    If objDocumento.Formulario.ConBultos Then
                        If IdDocBultos > 0 Then
                            objDocumento.Bultos.Documento.Id = IdDocBultos
                        Else
                            objDocumento.Bultos.Documento.Id = objDocumento.Id
                        End If
                    End If

                    If objDocumento.Formulario.EsActaProceso Then
                        If objDocumento.Origen.Id > 0 Then objDocumento.Origen.Realizar()
                        objDocumento.Sobres.Documento.Id = objDocumento.Id
                        objDocumento.Sobres.Realizar()
                    End If

                    objDocumento.ReintentosConteo = dr("reintentos_conteo")
                    objDocumento.Legado = dr("Legado")

                    If dr("IdMovimentacionFondo") IsNot DBNull.Value Then
                        objDocumento.IdMovimentacionFondo = dr("IdMovimentacionFondo")
                    End If

                    ' Adiciona os objetos na lista
                    Me.Add(objDocumento)

                End If


            Next

        End If

    End Sub

    ''' <summary>
    ''' Verifica si ya existe un documento con el identificador de la transacción
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RealizarTransaccionesDT(filtro As Hashtable, objRespuesta As ContractoServicio.RecuperarTransaccionesFechas.Respuesta, MuestraSaldoDesglosado As Boolean)

        Dim campo As Campo = Nothing
        Dim Campos As Campos = Nothing
        Dim IdDocDetalles As Long = Nothing
        Dim IdDocBultos As Long = Nothing
        Dim IdEspecie As Long = 0
        Dim IdDocTransacList = New Dictionary(Of String, ContractoServicio.RecuperarTransaccionesFechas.Transaccion)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoCabeceraRealizarTransacionesCompleto.ToString()

        ' Adiciona os parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHAHORADESDE", ProsegurDbType.Data_Hora, If(String.IsNullOrEmpty(filtro(Constantes.CONST_FECHA_HORA_DESDE)), DBNull.Value, filtro(Constantes.CONST_FECHA_HORA_DESDE))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHAHORAHASTA", ProsegurDbType.Data_Hora, If(String.IsNullOrEmpty(filtro(Constantes.CONST_FECHA_HORA_HASTA)), DBNull.Value, filtro(Constantes.CONST_FECHA_HORA_HASTA))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDPLANTA", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_PLANTA)), DBNull.Value, filtro(Constantes.CONST_COD_PLANTA))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDCENTROPROCESO", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_SECTOR)), DBNull.Value, filtro(Constantes.CONST_COD_SECTOR))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDCANAL", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_CANAL)), DBNull.Value, filtro(Constantes.CONST_COD_CANAL))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDCLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_CLIENTE)), DBNull.Value, filtro(Constantes.CONST_COD_CLIENTE))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDMONEDA", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_MONEDA)), DBNull.Value, Integer.Parse(filtro(Constantes.CONST_COD_MONEDA)))))

        ' Se o filtro de saldo diponível foi informado
        If Not String.IsNullOrEmpty(filtro(Constantes.CONST_SOLO_SALDO_DIPONIBLE)) AndAlso filtro(Constantes.CONST_SOLO_SALDO_DIPONIBLE) IsNot Nothing Then
            comando.CommandText &= String.Format(" AND {0} = {1}", "Saldo_Disponible_###VERSION###(M.ACCION, EC.CODIGO)", If(Boolean.Parse(filtro(Constantes.CONST_SOLO_SALDO_DIPONIBLE)), 1, 0))
        End If

        ' Os registros devem esta ordenados pela FechaGestion
        comando.CommandText &= " order by DC.FECHAGESTION "

        ' Atualiza o comand text
        comando.CommandText = comando.CommandText.Replace("###VERSION###", Prosegur.Genesis.Comon.Util.Version)
        'comando.CommandText = comando.CommandText.Replace("###VERSION###", "13041410")

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' recupera os dados da especie
        comando.CommandText = My.Resources.DocumentoDetalleByJoin.ToString()
        Dim DetalleListDR = ObtenerDetalleListDR(filtro)

        ' Obtem todos os formulários e campos
        Dim FoumularioCampoListDR = ObtenerFoumularioCampoListDR()

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' Instancia o objeto de transacciones
            objRespuesta.Transacciones = New ContractoServicio.RecuperarTransaccionesFechas.Transacciones

            For Each dr In dt.Rows

                Dim objTransacion As ContractoServicio.RecuperarTransaccionesFechas.Transaccion
                If Not IdDocTransacList.ContainsKey(dr("IdDocumento")) Then

                    ' Preenche a transação
                    objTransacion = New ContractoServicio.RecuperarTransaccionesFechas.Transaccion
                    IdDocTransacList.Add(dr("IdDocumento"), objTransacion)

                    ' nome do formulário
                    objTransacion.NombreDocumento = String.Empty
                    If dr("FormularioDescripcion") IsNot DBNull.Value Then
                        objTransacion.NombreDocumento = dr("FormularioDescripcion")
                    End If

                    ' Id movimentação fondo
                    If dr("IdMovimentacionFondo") IsNot DBNull.Value Then
                        objTransacion.OidTransaccion = dr("IdMovimentacionFondo")
                    End If

                    ' fecha gestion
                    If dr("FechaGestion") Is DBNull.Value Then
                        objTransacion.FechaTransaccion = Date.MinValue
                    Else
                        objTransacion.FechaTransaccion = dr("FechaGestion")
                    End If

                    Dim ne = FoumularioCampoListDR.FirstOrDefault(Function(f) f("IdFormulario") = dr("IdFormulario") AndAlso f("Nombre") = ContractoServicio.Enumeradores.eCampos.NumExterno.ToString)
                    If ne IsNot Nothing Then
                        ' numero externo
                        If dr("NumExterno") IsNot DBNull.Value Then
                            objTransacion.NumExterno = dr("NumExterno")
                        End If
                    End If

                    Dim cpo = FoumularioCampoListDR.FirstOrDefault(Function(f) f("IdFormulario") = dr("IdFormulario") AndAlso f("Nombre") = ContractoServicio.Enumeradores.eCampos.CentroProcesoOrigen.ToString)
                    If cpo IsNot Nothing Then
                        ' centro de proceso origem
                        If dr("CentroProcesoOrigenIdps") IsNot DBNull.Value OrElse dr("CentroProcesoOrigen") IsNot DBNull.Value Then
                            objTransacion.SectorOrigen = New ContractoServicio.RecuperarTransaccionesFechas.Sector With { _
                                .Codigo = If(dr("CentroProcesoOrigenIdps") IsNot DBNull.Value, dr("CentroProcesoOrigenIdps"), Nothing), _
                                .Descripcion = If(dr("CentroProcesoOrigen") IsNot DBNull.Value, dr("CentroProcesoOrigen"), Nothing) _
                            }
                        End If
                    End If

                    Dim cpd = FoumularioCampoListDR.FirstOrDefault(Function(f) f("IdFormulario") = dr("IdFormulario") AndAlso f("Nombre") = ContractoServicio.Enumeradores.eCampos.CentroProcesoDestino.ToString)
                    If cpd IsNot Nothing Then
                        ' Centro de proceso detino
                        If dr("CentroProcesoDestinoIdps") IsNot DBNull.Value AndAlso dr("CentroProcesoDestino") IsNot DBNull.Value Then
                            objTransacion.SectorDestino = New ContractoServicio.RecuperarTransaccionesFechas.Sector With { _
                                .Codigo = If(dr("CentroProcesoDestinoIdps") IsNot DBNull.Value, dr("CentroProcesoDestinoIdps"), Nothing), _
                                .Descripcion = If(dr("CentroProcesoDestino") IsNot DBNull.Value, dr("CentroProcesoDestino"), Nothing) _
                            }
                        End If
                    End If

                    ' Saldo Disponible Bool
                    If dr("SaldoDisponible") Is DBNull.Value OrElse dr("SaldoDisponible") = -1 Then
                        objTransacion.Disponible = Nothing
                    Else
                        objTransacion.Disponible = CType(dr("SaldoDisponible"), Boolean)
                    End If

                    ' cliente
                    If dr("ClienteOrigenIdps") IsNot DBNull.Value OrElse dr("ClienteOrigen") IsNot DBNull.Value Then
                        objTransacion.Cliente = New ContractoServicio.RecuperarTransaccionesFechas.Cliente With { _
                            .Codigo = If(dr("ClienteOrigenIdps") IsNot DBNull.Value, dr("ClienteOrigenIdps"), Nothing), _
                            .Descripcion = If(dr("ClienteOrigen") IsNot DBNull.Value, dr("ClienteOrigen"), Nothing) _
                        }
                    End If

                    ' Planta
                    If dr("PlantaOrigemIdps") IsNot DBNull.Value OrElse dr("PlantaOrigem") IsNot DBNull.Value Then
                        objTransacion.Planta = New ContractoServicio.RecuperarTransaccionesFechas.Planta With { _
                            .Codigo = If(dr("PlantaOrigemIdps") IsNot DBNull.Value, dr("PlantaOrigemIdps"), Nothing), _
                            .Descripcion = If(dr("PlantaOrigem") IsNot DBNull.Value, dr("PlantaOrigem"), Nothing) _
                        }
                    End If

                    Dim cno = FoumularioCampoListDR.FirstOrDefault(Function(f) f("IdFormulario") = dr("IdFormulario") AndAlso f("Nombre") = ContractoServicio.Enumeradores.eCampos.Banco.ToString)
                    If cno IsNot Nothing Then
                        ' Canal Origem - Banco
                        If dr("BancoIdps") IsNot DBNull.Value OrElse dr("Banco") IsNot DBNull.Value Then
                            objTransacion.CanalOrigen = New ContractoServicio.RecuperarTransaccionesFechas.Canal With { _
                            .Codigo = If(dr("BancoIdps") IsNot DBNull.Value, dr("BancoIdps"), Nothing), _
                            .Descripcion = If(dr("Banco") IsNot DBNull.Value, dr("Banco"), Nothing) _
                            }
                        End If
                    End If

                    Dim cnd = FoumularioCampoListDR.FirstOrDefault(Function(f) f("IdFormulario") = dr("IdFormulario") AndAlso f("Nombre") = ContractoServicio.Enumeradores.eCampos.BancoDeposito.ToString)
                    If cnd IsNot Nothing Then
                        ' Canal Destino - Banco Deposito
                        If dr("BancoDepositoIdps") IsNot DBNull.Value OrElse dr("BancoDeposito") IsNot DBNull.Value Then
                            objTransacion.CanalDestino = New ContractoServicio.RecuperarTransaccionesFechas.Canal With { _
                            .Codigo = If(dr("BancoDepositoIdps") IsNot DBNull.Value, dr("BancoDepositoIdps"), Nothing), _
                            .Descripcion = If(dr("BancoDeposito") IsNot DBNull.Value, dr("BancoDeposito"), Nothing) _
                            }
                        End If
                    End If

                    ' Adiciona na lista apenas se o objTransacion não foi inserido em um loop anterior
                    objRespuesta.Transacciones.Add(objTransacion)

                    ' -------- Detalles --------
                    Dim monedas As New ContractoServicio.RecuperarTransaccionesFechas.Monedas
                    Dim detalleFiltradoListDR As List(Of DataRow) = Nothing

                    ' se o Formulario.ConValores true
                    If dr("Convalores") = 1 Then
                        IdDocDetalles = dr("IdDocumento")

                        If IdDocDetalles > 0 Then
                            detalleFiltradoListDR = DetalleListDR.Where(Function(f) f("IdDocumento") = IdDocDetalles).ToList()
                        End If

                        If Not String.IsNullOrEmpty(filtro(Constantes.CONST_COD_MONEDA)) Then
                            detalleFiltradoListDR = detalleFiltradoListDR.Where(Function(f) f("IdMoneda") = Integer.Parse(filtro(Constantes.CONST_COD_MONEDA))).ToList()
                        End If

                        ' se existe algum detalle
                        If detalleFiltradoListDR.Count > 0 Then

                            If objTransacion.Monedas Is Nothing Then
                                ' Intancia o objeto de moedas
                                objTransacion.Monedas = New ContractoServicio.RecuperarTransaccionesFechas.Monedas
                            End If


                            ' Para cada detalle que existe
                            For Each detalle In detalleFiltradoListDR.OrderBy(Function(f) f("IdMoneda")).ThenBy(Function(f) f("Orden"))

                                Dim objMoneda As ContractoServicio.RecuperarTransaccionesFechas.Moneda = Nothing
                                objMoneda = objTransacion.Monedas.FirstOrDefault(Function(s) s.Codigo = detalle("Isogenesis"))

                                ' se ainda não foi preenchido nenhuma Moneda na coleçao objTransacion.Monedas
                                If objMoneda Is Nothing Then
                                    objMoneda = New ContractoServicio.RecuperarTransaccionesFechas.Moneda

                                    objMoneda.Codigo = detalle("Isogenesis")
                                    objMoneda.Descripcion = detalle("Moneda")
                                    objMoneda.Importe = detalle("Importe")
                                    objTransacion.Monedas.Add(objMoneda)
                                Else
                                    objMoneda.Importe += detalle("Importe")
                                End If

                                ' Busca saldo desglosado
                                If MuestraSaldoDesglosado Then

                                    If objMoneda.Especies Is Nothing Then
                                        ' Intancia o objeto de especies
                                        objMoneda.Especies = New ContractoServicio.RecuperarTransaccionesFechas.Especies
                                    End If

                                    Dim objEspecie As ContractoServicio.RecuperarTransaccionesFechas.Especie = Nothing
                                    objEspecie = objMoneda.Especies.FirstOrDefault(Function(s) s.Codigo = If(detalle("IdGenesis") IsNot DBNull.Value, detalle("IdGenesis"), String.Empty))

                                    ' se ainda não foi preenchido nenhuma Especie na colelçao objMoneda.Especies
                                    If objEspecie Is Nothing Then
                                        objEspecie = New ContractoServicio.RecuperarTransaccionesFechas.Especie

                                        objEspecie.Codigo = If(detalle("IdGenesis") IsNot DBNull.Value, detalle("IdGenesis"), String.Empty)
                                        objEspecie.Descripcion = If(detalle("Descripcion") IsNot DBNull.Value, detalle("Descripcion"), String.Empty)
                                        objEspecie.Cantidad = detalle("Cantidad")
                                        objEspecie.Importe = detalle("Importe")
                                        objMoneda.Especies.Add(objEspecie)
                                    Else
                                        objEspecie.Cantidad += detalle("Cantidad")
                                        objEspecie.Importe += detalle("Importe")
                                    End If

                                End If

                            Next

                        End If

                    End If

                Else
                    'objTransacion = IdDocTransacList(dr("IdDocumento"))
                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Obtem todos os detalles e retorna em DataTable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ObtenerDetalleListDR(filtro As Hashtable) As List(Of DataRow)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoDetalleByJoin.ToString()

        ' Adiciona os parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHAHORADESDE", ProsegurDbType.Data_Hora, If(String.IsNullOrEmpty(filtro(Constantes.CONST_FECHA_HORA_DESDE)), DBNull.Value, filtro(Constantes.CONST_FECHA_HORA_DESDE))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHAHORAHASTA", ProsegurDbType.Data_Hora, If(String.IsNullOrEmpty(filtro(Constantes.CONST_FECHA_HORA_HASTA)), DBNull.Value, filtro(Constantes.CONST_FECHA_HORA_HASTA))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDPLANTA", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_PLANTA)), DBNull.Value, filtro(Constantes.CONST_COD_PLANTA))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDCENTROPROCESO", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_SECTOR)), DBNull.Value, filtro(Constantes.CONST_COD_SECTOR))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDCANAL", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_CANAL)), DBNull.Value, filtro(Constantes.CONST_COD_CANAL))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDCLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_CLIENTE)), DBNull.Value, filtro(Constantes.CONST_COD_CLIENTE))))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDMONEDA", ProsegurDbType.Inteiro_Longo, If(String.IsNullOrEmpty(filtro(Constantes.CONST_COD_MONEDA)), DBNull.Value, Integer.Parse(filtro(Constantes.CONST_COD_MONEDA)))))


        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        Dim retorno As New List(Of DataRow)
        For Each dr In dt.Rows
            retorno.Add(dr)
        Next
        Return retorno

    End Function

    ''' <summary>
    ''' Obtem todos os formularios e campos, retorna em DataTable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ObtenerFoumularioCampoListDR() As List(Of DataRow)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.FormularioCampoRealizarTodos.ToString()

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        Dim retorno As New List(Of DataRow)
        For Each dr In dt.Rows
            retorno.Add(dr)
        Next
        Return retorno

    End Function

    ''' <summary>
    ''' Recupera algumas informações dos documentos do saldos de acordo com os filtros informados.
    ''' </summary>
    ''' <param name="codigosGrupo"></param>
    ''' <param name="fechaHoraSaldoDesde"></param>
    ''' <param name="fechaHoraSaldoHasta"></param>
    ''' <param name="codigoPlanta"></param>
    ''' <param name="codigoSector"></param>
    ''' <param name="codidoCliente"></param>
    ''' <param name="codigoCanal"></param>
    ''' <param name="codigoMoneda"></param>
    ''' <param name="soloSaldoDisponible"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecuperaRemesasPorGrupo(codigosGrupo As List(Of String), _
                                            fechaHoraSaldoDesde As DateTime, _
                                            fechaHoraSaldoHasta As DateTime, _
                                            Optional codigoPlanta As String = Nothing, _
                                            Optional codigoSector As String = Nothing, _
                                            Optional codidoCliente As String = Nothing, _
                                            Optional codigoCanal As String = Nothing, _
                                            Optional codigoMoneda As String = Nothing, _
                                            Optional soloSaldoDisponible As Nullable(Of Boolean) = Nothing) As DataTable

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoPorGrupo.ToString()

        ' cria os filtros
        Dim filtros As New System.Text.StringBuilder()

        If codigosGrupo IsNot Nothing AndAlso codigosGrupo.Count > 0 Then
            Dim codigos = String.Format("'{0}'", String.Join("','", codigosGrupo.ToArray()))
            filtros.AppendLine("	   AND G.COD_GRUPO IN (" & codigos & ") ")
        End If

        If Not fechaHoraSaldoDesde.Equals(DateTime.MinValue) Then
            filtros.AppendLine("	   AND DC.FECHA >= :FECHA_HORA_SALDO_DESDE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHA_HORA_SALDO_DESDE", ProsegurDbType.Data_Hora, fechaHoraSaldoDesde))
        End If

        If Not fechaHoraSaldoHasta.Equals(DateTime.MinValue) Then
            filtros.AppendLine("	   AND DC.FECHA <= :FECHA_HORA_SALDO_HASTA ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHA_HORA_SALDO_HASTA", ProsegurDbType.Data_Hora, fechaHoraSaldoHasta))
        End If

        If Not String.IsNullOrEmpty(codigoPlanta) Then
            filtros.AppendLine("	   AND P_O.IDPS = :CODIGO_PLANTA ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CODIGO_PLANTA", ProsegurDbType.Descricao_Curta, codigoPlanta))
        End If

        If Not String.IsNullOrEmpty(codigoSector) Then
            filtros.AppendLine("	   AND CP_D.IDPS = :CODIGO_SECTOR ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CODIGO_SECTOR", ProsegurDbType.Descricao_Curta, codigoSector))
        End If

        If Not String.IsNullOrEmpty(codidoCliente) Then
            filtros.AppendLine("	   AND C_O.IDPS = :CODIGO_CLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CODIGO_CLIENTE", ProsegurDbType.Descricao_Curta, codidoCliente))
        End If

        If Not String.IsNullOrEmpty(codigoCanal) Then
            filtros.AppendLine("	   AND CA_O.IDPS = :CODIGO_CANAL ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CODIGO_CANAL", ProsegurDbType.Descricao_Curta, codigoCanal))
        End If

        If Not String.IsNullOrEmpty(codigoMoneda) Then
            filtros.AppendLine("	   AND M.ISOGENESIS = :CODIGO_MONEDA ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CODIGO_MONEDA", ProsegurDbType.Descricao_Curta, codigoMoneda))
        End If

        If soloSaldoDisponible IsNot Nothing Then
            filtros.AppendLine("	   AND SALDO_DISPONIBLE_" & Prosegur.Genesis.Comon.Util.Version & "(M.ACCION, EC.CODIGO) = :SALDO_DISPONIBLE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SALDO_DISPONIBLE", ProsegurDbType.Inteiro_Curto, soloSaldoDisponible))
        End If

        comando.CommandText = comando.CommandText.Replace("###FILTROS###", filtros.ToString()).Replace("###VERSION###", Prosegur.Genesis.Comon.Util.Version)

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

    End Function

#End Region

End Class