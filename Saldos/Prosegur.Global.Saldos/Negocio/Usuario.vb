Imports Prosegur.DbHelper
Imports Prosegur.CriptoHelper

<Serializable()> _
Public Class Usuario

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Nombre As String
    Private _Funciones As Funciones
    Private _ApellidoNombre As String
    Private _CentroProcesoActual As CentroProceso
    Private _CentrosProceso As CentrosProceso
    Private _Bloqueado As Boolean
    Private _ElijeCP As Boolean
    Private _FormulariosExclusivos As Formularios
    Private _FechaCambioClave As Date
    Private _Caduca As Boolean
    Private _DiasDeValidez As Long
    Private _Caduco As Boolean
    Private _Clave As String
    Private _ClaveNueva As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property ClaveNueva() As String
        Get
            Return _ClaveNueva
        End Get
        Set(Value As String)
            Value = Value & New String(" ", 16)
            _ClaveNueva = Left(Value, 16)
        End Set
    End Property

    Public Property Clave() As String
        Get
            Return _Clave
        End Get
        Set(Value As String)
            Value = Value & New String(" ", 16)
            _Clave = Left(Value, 16)
        End Set
    End Property

    Public Property Caduco() As Boolean
        Get
            Return _Caduco
        End Get
        Set(Value As Boolean)
            _Caduco = Value
        End Set
    End Property

    Public Property DiasDeValidez() As Long
        Get
            Return _DiasDeValidez
        End Get
        Set(Value As Long)
            _DiasDeValidez = Value
        End Set
    End Property

    Public Property Caduca() As Boolean
        Get
            Return _Caduca
        End Get
        Set(Value As Boolean)
            _Caduca = Value
        End Set
    End Property

    Public ReadOnly Property FechaCambioClave() As Date
        Get
            Return _FechaCambioClave
        End Get
    End Property

    Public Property FormulariosExclusivos() As Formularios
        Get
            If _FormulariosExclusivos Is Nothing Then
                _FormulariosExclusivos = New Formularios()
            End If
            Return _FormulariosExclusivos
        End Get
        Set(Value As Formularios)
            _FormulariosExclusivos = Value
        End Set
    End Property

    Public Property ElijeCP() As Boolean
        Get
            Return _ElijeCP
        End Get
        Set(Value As Boolean)
            _ElijeCP = Value
        End Set
    End Property

    Public Property Bloqueado() As Boolean
        Get
            Return _Bloqueado
        End Get
        Set(Value As Boolean)
            _Bloqueado = Value
        End Set
    End Property

    Public Property CentrosProceso() As CentrosProceso
        Get
            If _CentrosProceso Is Nothing Then
                _CentrosProceso = New CentrosProceso()
            End If
            Return _CentrosProceso
        End Get
        Set(Value As CentrosProceso)
            _CentrosProceso = Value
        End Set
    End Property

    Public Property CentroProcesoActual() As CentroProceso
        Get
            If _CentroProcesoActual Is Nothing Then
                _CentroProcesoActual = New CentroProceso()
            End If
            Return _CentroProcesoActual
        End Get
        Set(Value As CentroProceso)
            _CentroProcesoActual = Value
        End Set
    End Property

    Public Property ApellidoNombre() As String
        Get
            Return _ApellidoNombre
        End Get
        Set(Value As String)
            _ApellidoNombre = Value
        End Set
    End Property

    Public Property Funciones() As Funciones
        Get
            If _Funciones Is Nothing Then
                _Funciones = New Funciones()
            End If
            Return _Funciones
        End Get
        Set(Value As Funciones)
            _Funciones = Value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(Value As String)
            _Nombre = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém login do usuário
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/05/2009 Criado
    ''' </history>
    Public Sub Ingresar(Optional CriptografarClave As Boolean = True)

        Dim ClaveCriptografada As String

        ' criptografar chave
        If CriptografarClave Then
            Dim objHashSha1 As New SHA1
            ClaveCriptografada = objHashSha1.GerarHash(Me.Clave)
        Else
            ClaveCriptografada = Me.Clave
        End If

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.UsuarioIngresar.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Nombre", ProsegurDbType.Descricao_Longa, Me.Nombre))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Clave", ProsegurDbType.Descricao_Longa, ClaveCriptografada))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Id = dt.Rows(0)("IdUsuario")
            Me.Bloqueado = Convert.ToBoolean(dt.Rows(0)("Bloqueado"))

        Else

            Me.Id = -1

        End If

    End Sub

    ''' <summary>
    ''' Obtém formularios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/05/2009 Criado
    ''' </history>
    Public Function FormulariosExclusivosRealizar() As Integer

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.UsuarioFormularioRealizar.ToString
        comando.CommandType = CommandType.Text

        If Me.Id <> 0 Then

            comando.CommandText &= " WHERE FU.IdUsuario = :IdUsuario "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))

        End If

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' limpar coleção
            Me.FormulariosExclusivos.Clear()

            ' criar formulario
            Dim objFormulario As Formulario = Nothing

            For Each dr As DataRow In dt.Rows

                objFormulario = New Formulario
                objFormulario.Id = dr("Id")

                If dr("Descripcion") IsNot DBNull.Value Then
                    objFormulario.Descripcion = dr("Descripcion")
                End If

                objFormulario.Motivo = Nothing
                objFormulario.Copias = Nothing
                objFormulario.CamposExtra = Nothing

                If dr("SoloEnGrupo") IsNot DBNull.Value Then
                    objFormulario.SoloEnGrupo = Convert.ToBoolean(dr("SoloEnGrupo"))
                End If

                If dr("ConValores") IsNot DBNull.Value Then
                    objFormulario.ConValores = Convert.ToBoolean(dr("ConValores"))
                End If

                If dr("ConBultos") IsNot DBNull.Value Then
                    objFormulario.ConBultos = Convert.ToBoolean(dr("ConBultos"))
                End If

                If dr("BasadoEnReporte") IsNot DBNull.Value Then
                    objFormulario.BasadoEnReporte = Convert.ToBoolean(dr("BasadoEnReporte"))
                End If

                objFormulario.Reporte = Nothing

                If dr("BasadoEnSaldos") IsNot DBNull.Value Then
                    objFormulario.BasadoEnSaldos = Convert.ToBoolean(dr("BasadoEnSaldos"))
                End If

                If dr("SeImprime") IsNot DBNull.Value Then
                    objFormulario.SeImprime = Convert.ToBoolean(dr("SeImprime"))
                End If

                If dr("Interplantas") IsNot DBNull.Value Then
                    objFormulario.Interplantas = Convert.ToBoolean(dr("Interplantas"))
                End If

                objFormulario.DistinguirPorNivel = False
                objFormulario.Matrices = False
                objFormulario.SoloIndividual = False
                objFormulario.EsActaProceso = False
                objFormulario.Color = String.Empty

                If dr("BasadoEnExtracto") IsNot DBNull.Value Then
                    objFormulario.BasadoEnExtracto = Convert.ToBoolean(dr("BasadoEnExtracto"))
                End If

                Me.FormulariosExclusivos.Add(objFormulario)

            Next

        End If

    End Function

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            If dt.Rows(0)("ApellidoNombre") IsNot DBNull.Value Then
                Me.ApellidoNombre = dt.Rows(0)("ApellidoNombre")
            Else
                Me.ApellidoNombre = String.Empty
            End If

            Me.Nombre = dt.Rows(0)("Nombre")
            Me.Clave = dt.Rows(0)("Clave")
            Me.ElijeCP = Convert.ToBoolean(dt.Rows(0)("ElijeCP"))
            Me.Caduca = dt.Rows(0)("Caduca")
            Me.DiasDeValidez = dt.Rows(0)("DiasdeValidez")
            Me.Caduco = dt.Rows(0)("Caduco")

            Dim fechaCambio As DateTime = Convert.ToDateTime(dt.Rows(0)("FechaCambioClave"))
            If (DateDiff(DateInterval.Day, fechaCambio, DateTime.Now) - Me.DiasDeValidez >= 0) AndAlso (Me.Caduca = 1) Then
                Me.Caduco = 1
            End If

            Me.Funciones.Usuario.Id = Me.Id
            Me.Funciones.Realizar()

            Me.CentrosProceso.Usuario.Id = Me.Id
            Me.CentrosProceso.Realizar()

            Me.FormulariosExclusivosRealizar()

        End If

    End Sub

    Public Sub Registrar()

        Dim transacao As New Transacao(Constantes.CONEXAO_SALDOS)

        If Me.Id = 0 Then
            Me.Id = Usuario.ObterIdUsuario()
            RegistrarInsert(transacao)
        Else
            RegistrarUpdate(transacao)
        End If

        Me.BorrarCentrosProceso(transacao)

        If CentrosProceso IsNot Nothing Then

            For Each cp As CentroProceso In CentrosProceso
                UsuarioCentroProcesoRegistrar(cp.Id, transacao)
            Next

        End If

        '---------------------------------------------------------------------
        Me.BorrarFunciones(transacao)

        If Funciones IsNot Nothing Then

            For Each f As Funcion In Funciones
                UsuarioFuncionRegistrar(f.Id, transacao)
            Next

        End If

        '---------------------------------------------------------------------

        Me.BorrarFormulariosExclusivos(transacao)

        If FormulariosExclusivos IsNot Nothing Then

            For Each f As Formulario In FormulariosExclusivos
                UsuarioFormularioRegistrar(f.Id, transacao)
            Next

        End If

        transacao.RealizarTransacao()

    End Sub

    ''' <summary>
    ''' Recupera o id do usuario.
    ''' </summary>
    ''' <param name="Login"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 26/04/2011 - Criado
    ''' </history>
    Public Shared Function RecuperarIdUsuario(Login As String) As Integer

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        cmd.CommandText = My.Resources.UsuarioRecuperarId.ToString
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NOMBRE", ProsegurDbType.Identificador_Alfanumerico, Login))

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, cmd)
    End Function

    Public Sub RegistrarInsert(ByRef transacao As Transacao)

        ' criptografar chave
        Dim hash As New CriptoHelper.SHA1
        Dim ChaveCriptografada As String = hash.GerarHash(Me.Clave)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioRealisarInsert.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Nombre", ProsegurDbType.Descricao_Longa, Me.Nombre))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Clave", ProsegurDbType.Descricao_Longa, ChaveCriptografada))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ApellidoNombre", ProsegurDbType.Observacao_Curta, Me.ApellidoNombre))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ElijeCP", ProsegurDbType.Logico, Me.ElijeCP))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Caduca", ProsegurDbType.Logico, Me.Caduca))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Caduco", ProsegurDbType.Logico, Me.Caduco))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DiasDeValidez", ProsegurDbType.Inteiro_Longo, Me.DiasDeValidez))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub RegistrarUpdate(ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioRgistrarUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Nombre", ProsegurDbType.Identificador_Alfanumerico, Me.Nombre))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ApellidoNombre", ProsegurDbType.Observacao_Curta, Me.ApellidoNombre))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ElijeCP", ProsegurDbType.Logico, Me.ElijeCP))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Caduca", ProsegurDbType.Logico, Me.Caduca))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Caduco", ProsegurDbType.Logico, Me.Caduco))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DiasDeValidez", ProsegurDbType.Inteiro_Longo, Me.DiasDeValidez))

        If Me.Clave.Trim <> String.Empty Then

            ' criptografar chave
            Dim hash As New CriptoHelper.SHA1
            Dim ChaveCriptografada As String = hash.GerarHash(Me.Clave)

            comando.CommandText = String.Format(comando.CommandText, ",clave = :Clave")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Clave", ProsegurDbType.Descricao_Longa, ChaveCriptografada))

        Else
            comando.CommandText = String.Format(comando.CommandText, String.Empty)
        End If

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarCentrosProceso(ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioBorraCentroProcesso.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarFunciones(ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioFuncionBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub UsuarioCentroProcesoRegistrar(idCentroProceso As Integer, ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioCentroProcessoRegistar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, idCentroProceso))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub UsuarioFormularioRegistrar(idFormulario As Integer, ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioFormularioRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, idFormulario))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub UsuarioFuncionRegistrar(idfuncion As Integer, ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioFuncionRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFuncion", ProsegurDbType.Inteiro_Longo, idfuncion))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarFormulariosExclusivos(ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioFormularioBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Id))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub Bloquear(idUsuario As Integer)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioBloquear.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, idUsuario))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Sub DesBloquear(idUsuario As Integer)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.UsuarioDesbloquear.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, idUsuario))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Shared Function ObterIdUsuario() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.SUsuario.ToString()
        comando.CommandType = CommandType.Text

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    Private Function Encriptar(ByRef StringX16 As String) As String

        'Dim Encriptador As QSEncriptador.Encriptador
        'Set Encriptador = sCtxtCreateObject("QSEncriptador.Encriptador", octxt)
        'Encriptador.LLave = "eldioni "
        'Encriptar = Encriptador.Encriptar(StringX16)
        'Set Encriptador = Nothing
        'Encriptar = StringX16


        Return Nothing
    End Function

    Private Function Desencriptar(ByRef StringX16 As String) As String

        'Dim Encriptador As QSEncriptador.Encriptador
        'Set Encriptador = sCtxtCreateObject("QSEncriptador.Encriptador", octxt)
        'Encriptador.LLave = "eldioni "
        'Desencriptar = Encriptador.Desencriptar(StringX16)
        'Set Encriptador = Nothing
        'Desencriptar = StringX16

        Return Nothing
    End Function

    ''' <summary>
    ''' Altera a chave do usuário
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 12/05/2009 Criado
    ''' </history>
    Public Sub CambiarClave()

        ' criptografar chave
        Dim objHashSha1 As New SHA1
        Dim ClaveCriptografada As String = objHashSha1.GerarHash(Me.ClaveNueva)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.UsuarioCambiarClave.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ClaveNueva", ProsegurDbType.Descricao_Longa, ClaveCriptografada))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Descricao_Longa, Me.Id))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

#End Region

End Class