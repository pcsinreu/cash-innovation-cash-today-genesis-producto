Imports System.Collections.Generic
Imports Prosegur.DbHelper

<Serializable()> _
Public Class CamposExtra
    Inherits List(Of CampoExtra)

#Region "[VARIÁVEIS]"

    Private _Formulario As Formulario
    Private _Documento As Documento

#End Region

#Region "PROPRIEDADES"

    Public Property Formulario() As Formulario
        Get
            If _Formulario Is Nothing Then
                _Formulario = New Formulario()
            End If
            Formulario = _Formulario
        End Get
        Set(Value As Formulario)
            _Formulario = Value
        End Set
    End Property

    Public Property Documento() As Documento
        Get
            If _Documento Is Nothing Then
                _Documento = New Documento()
            End If
            Documento = _Documento
        End Get
        Set(Value As Documento)
            _Documento = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        If Documento.Id = 0 AndAlso Formulario.Id = 0 Then
            comando.CommandText = My.Resources.CamposExtrasRealizar.ToString()
        End If

        If Documento.Id = 0 AndAlso Formulario.Id <> 0 Then
            comando.CommandText = My.Resources.CamposExtrasRealizarPorIdFormulario.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Identificador_Numerico, Me.Formulario.Id))
        End If

        If Documento.Id <> 0 AndAlso Formulario.Id <> 0 Then
            comando.CommandText = My.Resources.CamposExtrasRealizarPorIdDocumentoIdFormulario.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Documento.Id))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Identificador_Numerico, Me.Formulario.Id))
        End If

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto motivo
            Dim objCampoExtra As CampoExtra = Nothing

            ' Limpa a lista
            Me.Clear()

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' nova instancia...
                objCampoExtra = New Negocio.CampoExtra

                objCampoExtra.Id = dr("Id")

                If dr("Nombre") IsNot DBNull.Value Then
                    objCampoExtra.Nombre = dr("Nombre")
                Else
                    objCampoExtra.Nombre = String.Empty
                End If

                If dr("SeValida") IsNot DBNull.Value Then
                    objCampoExtra.SeValida = Convert.ToBoolean(dr("SeValida"))
                Else
                    objCampoExtra.SeValida = False
                End If

                objCampoExtra.TipoCampoExtra.Id = dr("IdTipoCampoExtra")

                If dr("TCEDescripcion") IsNot DBNull.Value Then
                    objCampoExtra.TipoCampoExtra.Descripcion = dr("TCEDescripcion")
                Else
                    objCampoExtra.TipoCampoExtra.Descripcion = String.Empty
                End If

                If dr("TCECodigo") IsNot DBNull.Value Then
                    objCampoExtra.TipoCampoExtra.Codigo = dr("TCECodigo")
                Else
                    objCampoExtra.TipoCampoExtra.Codigo = String.Empty
                End If

                If dr("Valor") IsNot DBNull.Value Then
                    objCampoExtra.Valor = dr("Valor")
                Else
                    objCampoExtra.Valor = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objCampoExtra)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Apaga todos os campos extras de um documento
    ''' </summary>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Sub BorrarCamposExtra(IdDocumento As Integer, objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.CamposExtraValorBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Inteiro_Longo, IdDocumento))
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

#End Region

End Class