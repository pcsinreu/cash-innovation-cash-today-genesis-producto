Imports System.Reflection

Public Class Downloads
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ACCIONESLOTE
        MyBase.ValidarAcao = True
        MyBase.CodFuncionalidad = "ABM_ACCIONES_EN_LOTE"
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try

            If (Request.QueryString("archivo") IsNot Nothing) Then
                DescarregarCSV(Session(Request.QueryString("archivo")), Request.QueryString("archivo"))
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        Finally
            If (Request.QueryString("archivo") IsNot Nothing) Then
                Session(Request.QueryString("archivo")) = Nothing
            End If
        End Try

    End Sub

#End Region

#Region "[METODOS]"

    Public Shared Sub DescarregarCSV(sCSV As String, pNombreCSV As String)

        'Obtiene la respuesta actual
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

        'Borra la respuesta
        response.Clear()
        response.ClearContent()
        response.ClearHeaders()

        'Tipo de contenido para forzar la descarga
        response.ContentType = "application/octet-stream"
        response.AddHeader("Content-Disposition", "attachment; filename=" & pNombreCSV)

        'Convierte el string a array de bytes
        Dim buffer(sCSV.Length) As Byte
        Dim mContador As Long = 0
        While mContador < sCSV.Length
            buffer(mContador) = Asc(Mid(sCSV, mContador + 1, 1))
            mContador = mContador + 1
        End While

        'Envia los bytes
        response.BinaryWrite(buffer)
        response.End()

    End Sub

#End Region

End Class