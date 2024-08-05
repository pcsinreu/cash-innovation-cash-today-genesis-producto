Imports Prosegur.DbHelper

<Serializable()> _
Public Class Planta

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Descripcion As String
    Private _IdPS As String
    Private _CentrosProceso As CentrosProceso
    Private _IdPSDescripcion As String
    Private _CodDelegacionGenesis As String

#End Region

#Region "[PROPRIEDADES]"

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

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
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

    Public Property IdPS() As String
        Get
            Return _IdPS
        End Get
        Set(Value As String)
            _IdPS = Value
        End Set
    End Property

    Public Property IdPSDescripcion() As String
        Get
            Return _IdPSDescripcion
        End Get
        Set(value As String)
            _IdPSDescripcion = value
        End Set
    End Property

    Public Property CodDelegacionGenesis() As String
        Get
            Return _CodDelegacionGenesis
        End Get
        Set(value As String)
            _CodDelegacionGenesis = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Registrar()

        ' TODO: Existia uma validação que verificava se descrição e idps estavam preenchidas
        ' esta validação devera ser feita na página

        If Me.Id = 0 Then

            Me.Id = Moneda.ObterIdMoneda

            InserirPlanta()

        Else
            AlterarPlanta()
        End If

    End Sub

    Private Function InserirPlanta() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.PlantaRegistrarInsert.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idPlanta", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Observacao_Curta, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Descricao_Curta, Me.IdPS))

        Return AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Function

    Private Sub AlterarPlanta()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.PlantaRegistrarUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idPlanta", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Observacao_Curta, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Descricao_Curta, Me.IdPS))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodDelegacionGenesis", ProsegurDbType.Descricao_Curta, Me.CodDelegacionGenesis))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Function Eliminar() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.PlantaRegistrarEliminar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idPlanta", ProsegurDbType.Inteiro_Longo, Me.Id))

        Return AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Function

    Public Sub Realizar()

        If Me.IdPS = String.Empty Then ' se não informou idps

            RealizarPorId()

        Else ' se informou idps

            RealizarPorIdPs()

        End If

    End Sub

    Private Sub RealizarPorId()

        Dim numRows As Integer = 0
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.PlantaRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idPlanta", ProsegurDbType.Inteiro_Longo, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Id = dt.Rows(0)("IdPlanta")
            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.IdPS = dt.Rows(0)("IdPS")
            Me.IdPSDescripcion = dt.Rows(0)("IdPSDescripcion")

            If dt.Rows(0)("CodDelegacionGenesis") IsNot DBNull.Value Then
                Me.CodDelegacionGenesis = dt.Rows(0)("CodDelegacionGenesis")
            Else
                Me.CodDelegacionGenesis = String.Empty
            End If

        End If

    End Sub

    Private Sub RealizarPorIdPs()

        Dim numRows As Integer = 0
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.PlantaRealizarPorIdps.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Id = dt.Rows(0)("IdPlanta")
            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.IdPS = dt.Rows(0)("IdPS")
            Me.IdPSDescripcion = dt.Rows(0)("IdPSDescripcion")

            If dt.Rows(0)("CodDelegacionGenesis") IsNot DBNull.Value Then
                Me.CodDelegacionGenesis = dt.Rows(0)("CodDelegacionGenesis")
            Else
                Me.CodDelegacionGenesis = String.Empty
            End If

        End If

    End Sub

    Public Sub RealizarByCodigoDelegacion()

        Dim numRows As Integer = 0
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.PlantaRealizarByCodigoDelegacion.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodDelegacion", ProsegurDbType.Descricao_Curta, Me.CodDelegacionGenesis))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Id = dt.Rows(0)("idplanta")
            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.IdPS = dt.Rows(0)("IdPS")
            Me.CodDelegacionGenesis = dt.Rows(0)("CodDelegacionGenesis")

        End If

    End Sub

    ''' <summary>
    ''' Obtêm os centros de processo de uma planta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/06/2009 Criado
    ''' </history>
    Public Sub CentrosProcesoRealizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.PlantaCentrosProcesoRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPlanta", ProsegurDbType.Inteiro_Longo, Me.Id))

        ' executa query
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' verificar se encontrou algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objCentroProcesso As Negocio.CentroProceso = Nothing

            ' para cada registro encontrado
            For Each dr As DataRow In dt.Rows

                objCentroProcesso = New Negocio.CentroProceso

                If dr("Id") IsNot DBNull.Value Then
                    objCentroProcesso.Id = dr("Id")
                Else
                    objCentroProcesso.Id = 0
                End If

                If dr("Descripcion") IsNot DBNull.Value Then
                    objCentroProcesso.Descripcion = dr("Descripcion")
                Else
                    objCentroProcesso.Descripcion = String.Empty
                End If

                ' adicionar para coleção
                Me.CentrosProceso.Add(objCentroProcesso)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Obtêm os centros de processo de uma planta por IDPS da Planta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 18/06/2009 Criado
    ''' </history>
    Public Sub CentrosProcesoRealizarByIdsPlanta()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.PlantaCentrosProcesoRealizarByIDPS.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsPlanta", ProsegurDbType.Descricao_Curta, Me.IdPS))

        ' executa query
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' verificar se encontrou algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objCentroProcesso As Negocio.CentroProceso = Nothing

            ' para cada registro encontrado
            For Each dr As DataRow In dt.Rows

                objCentroProcesso = New Negocio.CentroProceso

                If dr("IDPS") IsNot DBNull.Value Then
                    objCentroProcesso.IdPS = dr("IDPS")
                Else
                    objCentroProcesso.IdPS = 0
                End If

                If dr("Descripcion") IsNot DBNull.Value Then
                    objCentroProcesso.Descripcion = dr("Descripcion")
                Else
                    objCentroProcesso.Descripcion = String.Empty
                End If

                ' adicionar para coleção
                Me.CentrosProceso.Add(objCentroProcesso)

            Next

        End If

    End Sub

#End Region

End Class