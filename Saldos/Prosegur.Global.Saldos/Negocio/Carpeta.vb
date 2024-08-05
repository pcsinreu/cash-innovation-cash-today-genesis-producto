Imports Prosegur.DbHelper
Imports System.Text

<Serializable()> _
Public Class Carpeta

#Region "[VARIÁVEIS]"

    Private _Documentos As Documentos
    Private _Titulo As String
    Private _EstadoComprobante As EstadoComprobante
    Private _VistaDestinatario As Boolean
    Private _FechaDesde As Date
    Private _FechaHasta As Date
    Private _NumComprobanteDesde As String
    Private _NumComprobanteHasta As String
    Private _CentroProceso As CentroProceso
    Private _UsuarioActual As Usuario
    Private _NumPagina As Integer
    Private _Paginado As Boolean
    Private _NumPaginas As Integer
    Private _NumDocsPorPagina As Integer
    Private _NumDocumentos As Integer
    Private _NumDocIni As Integer
    Private _NumDocFin As Integer
    Private _DistinguirPorDisponibilidad As Boolean
    Private _Disponible As Boolean
    Private _TipoCodigo As String
    Private _ListaCodigos As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property TipoCodigo() As String
        Get
            TipoCodigo = _TipoCodigo
        End Get
        Set(Value As String)
            _TipoCodigo = Value
        End Set
    End Property

    Public Property ListaCodigos() As String
        Get
            ListaCodigos = _ListaCodigos
        End Get
        Set(Value As String)
            _ListaCodigos = Value
        End Set
    End Property

    Public Property Disponible() As Boolean
        Get
            Disponible = _Disponible
        End Get
        Set(Value As Boolean)
            _Disponible = Value
        End Set
    End Property

    Public Property DistinguirPorDisponibilidad() As Boolean
        Get
            DistinguirPorDisponibilidad = _DistinguirPorDisponibilidad
        End Get
        Set(Value As Boolean)
            _DistinguirPorDisponibilidad = Value
        End Set
    End Property

    Public Property NumDocFin() As Integer
        Get
            NumDocFin = _NumDocFin
        End Get
        Set(Value As Integer)
            _NumDocFin = Value
        End Set
    End Property

    Public Property NumDocsPorPagina() As Integer
        Get
            NumDocsPorPagina = _NumDocsPorPagina
        End Get
        Set(Value As Integer)
            _NumDocsPorPagina = Value
        End Set
    End Property

    Public Property NumPaginas() As Integer
        Get
            NumPaginas = _NumPaginas
        End Get
        Set(Value As Integer)
            _NumPaginas = Value
        End Set
    End Property

    Public Property Paginado() As Boolean
        Get
            Paginado = _Paginado
        End Get
        Set(Value As Boolean)
            _Paginado = Value
        End Set
    End Property

    Public Property NumPagina() As Integer
        Get
            NumPagina = _NumPagina
        End Get
        Set(Value As Integer)
            _NumPagina = Value
        End Set
    End Property

    Public Property UsuarioActual() As Usuario
        Get
            If _UsuarioActual Is Nothing Then
                _UsuarioActual = New Usuario()
            End If
            UsuarioActual = _UsuarioActual
        End Get
        Set(Value As Usuario)
            _UsuarioActual = Value
        End Set
    End Property

    Public Property CentroProceso() As CentroProceso
        Get
            If _CentroProceso Is Nothing Then
                _CentroProceso = New CentroProceso()
            End If
            CentroProceso = _CentroProceso
        End Get
        Set(Value As CentroProceso)
            _CentroProceso = Value
        End Set
    End Property

    Public Property NumComprobanteHasta() As String
        Get
            NumComprobanteHasta = _NumComprobanteHasta
        End Get
        Set(Value As String)
            _NumComprobanteHasta = Value
        End Set
    End Property

    Public Property NumComprobanteDesde() As String
        Get
            NumComprobanteDesde = _NumComprobanteDesde
        End Get
        Set(Value As String)
            _NumComprobanteDesde = Value
        End Set
    End Property

    Public Property FechaHasta() As Date
        Get
            FechaHasta = _FechaHasta
        End Get
        Set(Value As Date)
            _FechaHasta = Value
        End Set
    End Property

    Public Property FechaDesde() As Date
        Get
            FechaDesde = _FechaDesde
        End Get
        Set(Value As Date)
            _FechaDesde = Value
        End Set
    End Property

    Public Property VistaDestinatario() As Boolean
        Get
            VistaDestinatario = _VistaDestinatario
        End Get
        Set(Value As Boolean)
            _VistaDestinatario = Value
        End Set
    End Property

    Public Property EstadoComprobante() As EstadoComprobante
        Get
            If _EstadoComprobante Is Nothing Then
                _EstadoComprobante = New EstadoComprobante()
            End If
            EstadoComprobante = _EstadoComprobante
        End Get
        Set(Value As EstadoComprobante)
            _EstadoComprobante = Value
        End Set
    End Property

    Public Property Titulo() As String
        Get
            Titulo = _Titulo
        End Get
        Set(Value As String)
            _Titulo = Value
        End Set
    End Property

    Public Property Documentos() As Documentos
        Get
            If _Documentos Is Nothing Then
                _Documentos = New Documentos()
            End If
            Documentos = _Documentos
        End Get
        Set(Value As Documentos)
            _Documentos = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Realiza consulta com os dados informados nos filtros
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/06/2009 Criado
    ''' </history>
    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.CarpetaDocumentosRealizar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Identificador_Numerico, Me.CentroProceso.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoComprobante", ProsegurDbType.Identificador_Numerico, Me.EstadoComprobante.Id))

        If Me.FechaDesde <> DateTime.MinValue Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesde", ProsegurDbType.Data_Hora, Me.FechaDesde))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesde", ProsegurDbType.Data_Hora, DBNull.Value))
        End If

        If Me.FechaHasta <> DateTime.MinValue Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHasta", ProsegurDbType.Data_Hora, Me.FechaHasta))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHasta", ProsegurDbType.Data_Hora, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteDesde", ProsegurDbType.Identificador_Alfanumerico, Me.NumComprobanteDesde))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteHasta", ProsegurDbType.Identificador_Alfanumerico, Me.NumComprobanteHasta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "TipoCodigo", ProsegurDbType.Descricao_Curta, Me.TipoCodigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ListaCodigos", ProsegurDbType.Descricao_Longa, Me.ListaCodigos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, Me.VistaDestinatario))

        If Me.DistinguirPorDisponibilidad Then
            comando.CommandText &= " AND DC.DISPONIBLE = :DISPONIBLE "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DISPONIBLE", ProsegurDbType.Logico, Me.Disponible))
        End If

        comando.CommandText &= " ORDER BY DC.Fecha DESC, DC.NumExterno DESC, DC.NumComprobante DESC "

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso tenha encontrado registros
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objDocumento As Documento = Nothing

            ' para cada registro, preencher o objeto documento
            For Each dr As DataRow In dt.Rows

                objDocumento = New Documento

                If dr("IdDocumento") IsNot DBNull.Value Then
                    objDocumento.Id = dr("IdDocumento")
                Else
                    objDocumento.Id = 0
                End If

                If dr("IdGrupo") IsNot DBNull.Value Then
                    objDocumento.Grupo.Id = Convert.ToInt32(dr("IdGrupo"))
                Else
                    objDocumento.Grupo.Id = 0
                End If

                If dr("EsGrupo") Is DBNull.Value Then
                    objDocumento.EsGrupo = False
                Else
                    objDocumento.EsGrupo = CType(dr("EsGrupo"), Boolean)
                    objDocumento.GrupoDocumentosRealizar()
                End If

                If dr("IdOrigen") IsNot DBNull.Value Then
                    objDocumento.Origen.Id = Convert.ToInt32(dr("IdOrigen"))
                Else
                    objDocumento.Origen.Id = 0
                End If

                If dr("IdFormulario") IsNot DBNull.Value Then
                    objDocumento.Formulario.Id = Convert.ToInt32(dr("IdFormulario"))
                Else
                    objDocumento.Formulario.Id = 0
                End If
                objDocumento.Formulario.Realizar()

                ' caso tenha campos
                If objDocumento.Formulario.Campos IsNot Nothing _
                    AndAlso objDocumento.Formulario.Campos.Count > 0 Then

                    ' para cada campo
                    For Each objCampo As Campo In objDocumento.Formulario.Campos

                        Select Case objCampo.Tipo
                            Case "I"
                                If dr("Id" & objCampo.Nombre) IsNot DBNull.Value Then
                                    objCampo.IdValor = dr("Id" & objCampo.Nombre)
                                Else
                                    objCampo.IdValor = 0
                                End If

                                If dr(objCampo.Nombre & "Desc") IsNot DBNull.Value Then
                                    objCampo.Valor = dr(objCampo.Nombre & "Desc")
                                Else
                                    objCampo.Valor = String.Empty
                                End If

                            Case "A"

                                If dr(objCampo.Nombre) IsNot DBNull.Value Then
                                    objCampo.Valor = dr(objCampo.Nombre)
                                Else
                                    objCampo.Valor = String.Empty
                                End If

                        End Select

                    Next

                End If

                If dr("Sustituido") IsNot DBNull.Value Then
                    objDocumento.Sustituido = Convert.ToBoolean(dr("Sustituido"))
                Else
                    objDocumento.Sustituido = False
                End If

                If dr("IdEstadoComprobante") IsNot DBNull.Value Then
                    objDocumento.EstadoComprobante.Id = Convert.ToInt32(dr("IdEstadoComprobante"))
                Else
                    objDocumento.EstadoComprobante.Id = 0
                End If

                If dr("IdUsuario") IsNot DBNull.Value Then
                    objDocumento.Usuario.Id = Convert.ToInt32(dr("IdUsuario"))
                Else
                    objDocumento.Usuario.Id = 0
                End If
                objDocumento.Usuario.Realizar()

                If dr("IdUsuarioResuelve") IsNot DBNull.Value Then
                    objDocumento.UsuarioResolutor.Id = Convert.ToInt32(dr("IdUsuarioResuelve"))
                    objDocumento.UsuarioResolutor.Realizar()
                Else
                    objDocumento.UsuarioResolutor.Id = 0
                End If

                If dr("IdUsuariodispone") IsNot DBNull.Value Then
                    objDocumento.UsuarioDispone.Id = Convert.ToInt32(dr("IdUsuariodispone"))
                    objDocumento.UsuarioDispone.Realizar()
                Else
                    objDocumento.UsuarioDispone.Id = 0
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

                If dr("NumComprobante") IsNot DBNull.Value Then
                    objDocumento.NumComprobante = dr("NumComprobante")
                Else
                    objDocumento.NumComprobante = String.Empty
                End If

                If dr("Fecha") IsNot DBNull.Value Then
                    objDocumento.Fecha = Convert.ToDateTime(dr("Fecha"))
                Else
                    objDocumento.Fecha = DateTime.MinValue
                End If

                ' adicionar para coleção
                Me.Documentos.Add(objDocumento)

            Next

        End If

    End Sub

    Public Sub RealizarSimplificado()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        Dim sql As String = String.Empty
        Dim filtro As New StringBuilder

        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, Me.VistaDestinatario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Identificador_Numerico, Me.CentroProceso.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoComprobante", ProsegurDbType.Identificador_Numerico, Me.EstadoComprobante.Id))

        If Not String.IsNullOrEmpty(Me.NumComprobanteDesde) Then
            filtro.Append(" AND ")
            filtro.Append("(DC.NumComprobante >= :NumComprobanteDesde)")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteDesde", ProsegurDbType.Identificador_Alfanumerico, Me.NumComprobanteDesde))
        End If

        If Not String.IsNullOrEmpty(Me.NumComprobanteHasta) Then
            filtro.Append(" AND ")
            filtro.Append("(DC.NumComprobante <= :NumComprobanteHasta)")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteHasta", ProsegurDbType.Identificador_Alfanumerico, Me.NumComprobanteHasta))
        End If

        If Not String.IsNullOrEmpty(Me.TipoCodigo) AndAlso Not String.IsNullOrEmpty(Me.ListaCodigos) Then

            filtro.Append(" AND ")

            Select Case Me.TipoCodigo

                Case "NC"

                    filtro.Append("(DC.NumComprobante IN (" & Me.ListaCodigos & ") OR ")
                    filtro.Append("(0 < (SELECT COUNT(*) FROM PD_DOCUMENTOCABECERA SQDC WHERE(SQDC.IdGrupo = DC.IdDocumento) " & _
                                  "AND SQDC.NumComprobante IN (" & Me.ListaCodigos & "))))")

                Case "NE"

                    filtro.Append("(DC.NumExterno IN (" & Me.ListaCodigos & ") OR ")
                    filtro.Append("(0 < (SELECT COUNT(*) FROM PD_DOCUMENTOCABECERA SQDC WHERE(SQDC.IdGrupo = DC.IdDocumento) " & _
                                  "AND SQDC.NumExterno IN (" & Me.ListaCodigos & ")))) ")

            End Select

        End If

        If Me.FechaDesde <> DateTime.MinValue Then
            filtro.Append(" AND " & String.Format("DC.Fecha >= To_Date('{0}', 'dd/mm/YYYY HH24:MI:SS')", FechaDesde.ToString("dd/MM/yyyy HH:mm:ss")))
            ' filtro.Append(" AND DC.Fecha >= :FechaDesde")
            'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesde", ProsegurDbType.Data, Me.FechaDesde))
        End If

        If Me.FechaHasta <> DateTime.MinValue Then
            filtro.Append(" AND " & String.Format("DC.Fecha <= To_Date('{0}', 'dd/mm/YYYY HH24:MI:SS')", FechaHasta.ToString("dd/MM/yyyy HH:mm:ss")))
            'filtro.Append(" AND DC.Fecha <= :FechaHasta")
            'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHasta", ProsegurDbType.Data, Me.FechaHasta))
        End If

        If Me.DistinguirPorDisponibilidad Then
            filtro.Append(" AND DC.DISPONIBLE = :DISPONIBLE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DISPONIBLE", ProsegurDbType.Logico, Me.Disponible))
        End If

        filtro.Append(" ORDER BY DC.Fecha DESC, DC.NumExterno DESC, DC.NumComprobante DESC ")

        sql = My.Resources.CarpetaDocumentosRealizarSimplificado & filtro.ToString()

        comando.CommandText = sql

        PopularColecao(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando))

    End Sub

    Private Sub PopularColecao(dt As DataTable)

        ' caso tenha encontrado registros
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objDocumento As Documento = Nothing

            ' para cada registro, preencher o objeto documento
            For Each dr As DataRow In dt.Rows

                objDocumento = New Documento

                If dr("IdDocumento") IsNot DBNull.Value Then
                    objDocumento.Id = dr("IdDocumento")
                Else
                    objDocumento.Id = 0
                End If

                If dr("IdFormulario") IsNot DBNull.Value Then
                    objDocumento.Formulario.Id = Convert.ToInt32(dr("IdFormulario"))
                    If dr("Descripcion") IsNot DBNull.Value Then
                        objDocumento.Formulario.Descripcion = dr("Descripcion")
                    End If

                    If dr("Color") IsNot DBNull.Value Then
                        objDocumento.Formulario.Color = dr("Color")
                    End If
                Else
                    objDocumento.Formulario.Id = 0
                    objDocumento.Formulario.Campos.Formulario.Id = 0
                End If

                objDocumento.Campos = New Hashtable

                If (dr("idcentroprocesoorigen") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("IdCentroProcesoOrigen", dr("idcentroprocesoorigen"))
                End If

                If (dr("centroprocesoorigendesc") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("CentroProcesoOrigenDesc", dr("centroprocesoorigendesc"))
                End If

                If (dr("idcentroprocesodestino") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("IdCentroProcesoDestino", dr("idcentroprocesodestino"))
                End If

                If (dr("centroprocesodestinodesc") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("CentroProcesoDestinoDesc", dr("centroprocesodestinodesc"))
                End If

                If (dr("idclienteorigen") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("IdClienteOrigen", dr("idclienteorigen"))
                End If

                If (dr("clienteorigendesc") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("ClienteOrigenDesc", dr("clienteorigendesc"))
                End If

                If (dr("idclientedestino") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("IdClienteDestino", dr("idclientedestino"))
                End If

                If (dr("clientedestinodesc") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("ClienteDestinoDesc", dr("clientedestinodesc"))
                End If

                If (dr("idbanco") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("IdBanco", dr("idbanco"))
                End If

                If (dr("bancodesc") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("BancoDesc", dr("bancodesc"))
                End If

                If (dr("idbancodeposito") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("IdBancoDeposito", dr("idbancodeposito"))
                End If

                If (dr("bancodepositodesc") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("BancoDepositoDesc", dr("bancodepositodesc"))
                End If

                If (dr("numexterno") IsNot DBNull.Value) Then
                    objDocumento.Campos.Add("NumExterno", dr("numexterno"))
                End If

                If dr("IdUsuario") IsNot DBNull.Value Then
                    objDocumento.Usuario.Id = Convert.ToInt32(dr("IdUsuario"))
                    objDocumento.Usuario.ApellidoNombre = dr("Nombre")
                Else
                    objDocumento.Usuario.Id = 0
                End If

                If dr("IdUsuarioResuelve") IsNot DBNull.Value Then
                    objDocumento.UsuarioResolutor.Id = Convert.ToInt32(dr("IdUsuarioResuelve"))
                    objDocumento.UsuarioResolutor.ApellidoNombre = dr("NombreResuelve")
                Else
                    objDocumento.UsuarioResolutor.Id = 0
                End If

                If dr("IdUsuarioDispone") IsNot DBNull.Value Then
                    objDocumento.UsuarioDispone.Id = Convert.ToInt32(dr("IdUsuarioDispone"))
                    objDocumento.UsuarioDispone.ApellidoNombre = dr("NombreDispone")
                Else
                    objDocumento.UsuarioResolutor.Id = 0
                End If

                If dr("FechaResuelve") IsNot DBNull.Value Then
                    objDocumento.FechaResolucion = Convert.ToDateTime(dr("FechaResuelve"))
                Else
                    objDocumento.FechaResolucion = DateTime.MinValue
                End If

                If dr("NumComprobante") IsNot DBNull.Value Then
                    objDocumento.NumComprobante = dr("NumComprobante")
                Else
                    objDocumento.NumComprobante = String.Empty
                End If

                If dr("Fecha") IsNot DBNull.Value Then
                    objDocumento.Fecha = Convert.ToDateTime(dr("Fecha"))
                Else
                    objDocumento.Fecha = DateTime.MinValue
                End If

                If dr("Sustituido") IsNot DBNull.Value Then
                    objDocumento.Sustituido = Convert.ToBoolean(dr("Sustituido"))
                End If

                ' adicionar para coleção
                Me.Documentos.Add(objDocumento)

            Next

        End If

    End Sub

    Public Sub RealizarPorPrecintos()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.CarpetaDocumentosRealizarPorPrecinto.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Identificador_Numerico, Me.CentroProceso.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoComprobante", ProsegurDbType.Identificador_Numerico, Me.EstadoComprobante.Id))

        If Me.FechaDesde <> DateTime.MinValue Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesde", ProsegurDbType.Data_Hora, Me.FechaDesde))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesde", ProsegurDbType.Data_Hora, DBNull.Value))
        End If

        If Me.FechaHasta <> DateTime.MinValue Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHasta", ProsegurDbType.Data_Hora, Me.FechaHasta))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHasta", ProsegurDbType.Data_Hora, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteDesde", ProsegurDbType.Identificador_Alfanumerico, Me.NumComprobanteDesde))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteHasta", ProsegurDbType.Identificador_Alfanumerico, Me.NumComprobanteHasta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ListaCodigos", ProsegurDbType.Descricao_Longa, Me.ListaCodigos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, Me.VistaDestinatario))

        If Me.DistinguirPorDisponibilidad Then
            comando.CommandText &= " AND DC.DISPONIBLE = :DISPONIBLE "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DISPONIBLE", ProsegurDbType.Logico, Me.Disponible))
        End If

        comando.CommandText &= " ORDER BY DC.Fecha DESC, DC.NumExterno DESC, DC.NumComprobante DESC "

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso tenha encontrado registros
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objDocumento As Documento = Nothing

            ' para cada registro, preencher o objeto documento
            For Each dr As DataRow In dt.Rows

                objDocumento = New Documento

                If dr("IdDocumento") IsNot DBNull.Value Then
                    objDocumento.Id = dr("IdDocumento")
                Else
                    objDocumento.Id = 0
                End If

                If dr("IdGrupo") IsNot DBNull.Value Then
                    objDocumento.Grupo.Id = Convert.ToInt32(dr("IdGrupo"))
                Else
                    objDocumento.Grupo.Id = 0
                End If

                If dr("IdOrigen") IsNot DBNull.Value Then
                    objDocumento.Origen.Id = Convert.ToInt32(dr("IdOrigen"))
                Else
                    objDocumento.Origen.Id = 0
                End If

                If dr("IdFormulario") IsNot DBNull.Value Then
                    objDocumento.Formulario.Id = Convert.ToInt32(dr("IdFormulario"))
                Else
                    objDocumento.Formulario.Id = 0
                End If
                objDocumento.Formulario.Realizar()

                ' caso tenha campos
                If objDocumento.Formulario.Campos IsNot Nothing _
                    AndAlso objDocumento.Formulario.Campos.Count > 0 Then

                    ' para cada campo
                    For Each objCampo As Campo In objDocumento.Formulario.Campos

                        Select Case objCampo.Tipo
                            Case "I"
                                If dr("Id" & objCampo.Nombre) IsNot DBNull.Value Then
                                    objCampo.IdValor = dr("Id" & objCampo.Nombre)
                                Else
                                    objCampo.IdValor = 0
                                End If

                                If dr(objCampo.Nombre & "Desc") IsNot DBNull.Value Then
                                    objCampo.Valor = dr(objCampo.Nombre & "Desc")
                                Else
                                    objCampo.Valor = String.Empty
                                End If

                            Case "A"

                                If dr(objCampo.Nombre) IsNot DBNull.Value Then
                                    objCampo.Valor = dr(objCampo.Nombre)
                                Else
                                    objCampo.Valor = String.Empty
                                End If

                        End Select

                    Next

                End If

                If dr("Sustituido") IsNot DBNull.Value Then
                    objDocumento.Sustituido = Convert.ToBoolean(dr("Sustituido"))
                Else
                    objDocumento.Sustituido = False
                End If

                If dr("IdEstadoComprobante") IsNot DBNull.Value Then
                    objDocumento.EstadoComprobante.Id = Convert.ToInt32(dr("IdEstadoComprobante"))
                Else
                    objDocumento.EstadoComprobante.Id = 0
                End If

                If dr("IdUsuario") IsNot DBNull.Value Then
                    objDocumento.Usuario.Id = Convert.ToInt32(dr("IdUsuario"))
                Else
                    objDocumento.Usuario.Id = 0
                End If
                objDocumento.Usuario.Realizar()

                If dr("IdUsuarioResuelve") IsNot DBNull.Value Then
                    objDocumento.UsuarioResolutor.Id = Convert.ToInt32(dr("IdUsuarioResuelve"))
                    objDocumento.UsuarioResolutor.Realizar()
                Else
                    objDocumento.UsuarioResolutor.Id = 0
                End If

                If dr("IdUsuariodispone") IsNot DBNull.Value Then
                    objDocumento.UsuarioDispone.Id = Convert.ToInt32(dr("IdUsuariodispone"))
                    objDocumento.UsuarioDispone.Realizar()
                Else
                    objDocumento.UsuarioDispone.Id = 0
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

                If dr("NumComprobante") IsNot DBNull.Value Then
                    objDocumento.NumComprobante = dr("NumComprobante")
                Else
                    objDocumento.NumComprobante = String.Empty
                End If

                If dr("Fecha") IsNot DBNull.Value Then
                    objDocumento.Fecha = Convert.ToDateTime(dr("Fecha"))
                Else
                    objDocumento.Fecha = DateTime.MinValue
                End If

                ' adicionar para coleção
                Me.Documentos.Add(objDocumento)

            Next

        End If

    End Sub

    Private Sub GenerarTitulo(ByRef conexion As Object)




    End Sub

#End Region

End Class