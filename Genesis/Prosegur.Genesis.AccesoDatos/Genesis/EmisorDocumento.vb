Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe Emissor Documento
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EmisorDocumento

#Region "Consulta"

        ''' <summary>
        ''' Retorna os dados do emissor do documento
        ''' </summary>
        ''' <returns>List(Of Clases.EmisorDocumento)</returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarEmisoresDocumento(objEmisor As Clases.EmisorDocumento) As ObservableCollection(Of Clases.EmisorDocumento)

            Dim objEmisoresDocumento As New ObservableCollection(Of Clases.EmisorDocumento)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EmisorDocumentoRecuperar)
            cmd.CommandType = CommandType.Text

            ' Verifica se o emissor foi passado
            If objEmisor IsNot Nothing Then

                ' Verifica se o código foi informado
                If Not String.IsNullOrEmpty(objEmisor.Codigo) Then
                    cmd.CommandText = cmd.CommandText & Util.ImplementarClausulaWhere(cmd.CommandText, "UPPER(ED.COD_EMISOR_DOCUMENTO) LIKE '%" & objEmisor.Codigo.ToUpper() & "%'")
                End If

                ' Verifica se a descrição foi informada
                If Not String.IsNullOrEmpty(objEmisor.Descripcion) Then
                    cmd.CommandText = cmd.CommandText & Util.ImplementarClausulaWhere(cmd.CommandText, "UPPER(ED.DES_EMISOR_DOCUMENTO) LIKE '%" & objEmisor.Descripcion.ToUpper() & "%'")
                End If

            End If

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            ' Verifica se os emissores foram recuperados
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                ' Cria a lista de emissores
                objEmisoresDocumento = New ObservableCollection(Of Clases.EmisorDocumento)

                ' Para cada emissor existente
                For Each dr As DataRow In dt.Rows

                    objEmisoresDocumento.Add(New Clases.EmisorDocumento With
                                             {
                                                .Codigo = Util.AtribuirValorObj(dr("COD_EMISOR_DOCUMENTO"), GetType(String)),
                                                .Descripcion = Util.AtribuirValorObj(dr("DES_EMISOR_DOCUMENTO"), GetType(String)),
                                                .CodigoValidacionDocumento = Util.AtribuirValorObj(dr("COD_VALIDACION_DOCUMENTO"), GetType(String)),
                                                .TipoOrigen = Util.AtribuirValorObj(dr("COD_TIPO_ORIGEN"), GetType(Integer)),
                                                .EstaActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO"), GetType(Boolean)),
                                                .Identificador = Util.AtribuirValorObj(dr("OID_EMISOR_DOCUMENTO"), GetType(String)),
                                                .IdentificadorIAC = Util.AtribuirValorObj(dr("OID_IAC"), GetType(String))
                                             })

                Next

            End If

            ' Retorna a lista de emissores
            Return objEmisoresDocumento

        End Function

#End Region

    End Class

End Namespace