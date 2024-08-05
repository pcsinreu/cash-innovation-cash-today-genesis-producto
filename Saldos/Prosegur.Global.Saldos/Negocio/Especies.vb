Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class Especies
    Inherits List(Of Especie)

#Region "VARIÁVEIS"

    Private _Moneda As Moneda
    Private _Uniforme As Boolean
    Private _DistinguirPorUniformidad As Boolean
    Private _DistinguirPorActaProceso As Boolean
    Private _EnActaProceso As Boolean
    Private _Calidad As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property EnActaProceso() As Boolean
        Get
            Return _EnActaProceso
        End Get
        Set(Value As Boolean)
            _EnActaProceso = Value
        End Set
    End Property

    Public Property DistinguirPorActaProceso() As Boolean
        Get
            Return _DistinguirPorActaProceso
        End Get
        Set(Value As Boolean)
            _DistinguirPorActaProceso = Value
        End Set
    End Property

    Public Property DistinguirPorUniformidad() As Boolean
        Get
            Return _DistinguirPorUniformidad
        End Get
        Set(Value As Boolean)
            _DistinguirPorUniformidad = Value
        End Set
    End Property

    Public Property Uniforme() As Boolean
        Get
            Return _Uniforme
        End Get
        Set(Value As Boolean)
            _Uniforme = Value
        End Set
    End Property

    Public Property Moneda() As Moneda
        Get
            If _Moneda Is Nothing Then
                _Moneda = New Moneda
            End If
            Return _Moneda
        End Get
        Set(Value As Moneda)
            _Moneda = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar(Optional idRboIdSigIInotNull As Boolean = False)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.EspeciesRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Inteiro_Longo, Me.Moneda.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorUniformidad", ProsegurDbType.Logico, Me.DistinguirPorUniformidad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Uniforme", ProsegurDbType.Logico, Me.Uniforme))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorActaProceso", ProsegurDbType.Logico, Me.DistinguirPorActaProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EnActaProceso", ProsegurDbType.Logico, Me.EnActaProceso))
        comando.CommandText = String.Format(comando.CommandText, If(idRboIdSigIInotNull, "AND(IdRBO IS NOT NULL AND IdSIGII IS NOT NULL)", String.Empty))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objEspecie As Especie = Nothing

            For Each dr As DataRow In dt.Rows

                objEspecie = New Especie

                objEspecie.Id = dr("Id")
                objEspecie.Descripcion = dr("Descripcion")

                If dr("Calidad") IsNot DBNull.Value Then
                    objEspecie.Calidad = dr("Calidad")
                Else
                    objEspecie.Calidad = String.Empty
                End If

                objEspecie.Moneda.Id = dr("IdMoneda")
                objEspecie.Moneda.Descripcion = dr("DescMoneda")
                objEspecie.Moneda.Simbolo = dr("SimboloMoneda")

                If dr("Uniforme") IsNot DBNull.Value Then
                    objEspecie.Uniforme = Convert.ToBoolean(dr("Uniforme"))
                Else
                    objEspecie.Uniforme = dr("Uniforme")
                End If

                If dr("Orden") IsNot DBNull.Value Then
                    objEspecie.Orden = Convert.ToInt32(dr("Orden"))
                Else
                    objEspecie.Orden = String.Empty
                End If

                If dr("EnActaProceso") IsNot DBNull.Value Then
                    objEspecie.EnActaProceso = Convert.ToBoolean(dr("EnActaProceso"))
                Else
                    objEspecie.EnActaProceso = False
                End If

                If dr("IdRBO") IsNot DBNull.Value Then
                    objEspecie.IdRBO = dr("IdRBO")
                Else
                    objEspecie.IdRBO = String.Empty
                End If

                If dr("IdSIGII") IsNot DBNull.Value Then
                    objEspecie.IdSIGII = dr("IdSIGII")
                Else
                    objEspecie.IdSIGII = String.Empty
                End If

                Me.Add(objEspecie)

            Next

        End If

    End Sub

#End Region

End Class