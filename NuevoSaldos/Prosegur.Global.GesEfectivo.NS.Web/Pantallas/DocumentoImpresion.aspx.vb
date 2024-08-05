Imports Prosegur.Genesis.Report.RSE
Imports Prosegur.Genesis.Report
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Public Class DocumentoImpresion
    Inherits Base

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim objReport As New Prosegur.Genesis.Report.Gerar()
            Dim nomeRelatorio As String = String.Empty
            Dim codigoComprobante As String = String.Empty
            Dim objRespuesta As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta = Nothing
            Dim codigoDelegacion As String = String.Empty
            Dim URL As String = String.Empty

            'quando é chamado de dentro do proprio saldo
            If Request("COD_COMPROBANTE") IsNot Nothing Then
                codigoComprobante = Request("COD_COMPROBANTE")
                nomeRelatorio = Request("IDReporte")

                'quando é chamado do recepcion envio
            ElseIf Request("COD_COMPROBANTE_DOC") IsNot Nothing Then
                Dim objDocumento As Prosegur.Genesis.Comon.Clases.Documento = Nothing
                codigoComprobante = Request("COD_COMPROBANTE_DOC")

                Dim _peticion As New Clases.Transferencias.FiltroDocumentos_v2
                _peticion.CodigoComprobante = codigoComprobante
                Dim _documentos As ObservableCollection(Of Clases.Documento) = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Documento.obtenerDocumentosPorFiltro(_peticion)

                If _documentos IsNot Nothing AndAlso _documentos.Count > 0 Then
                    objDocumento = _documentos.FirstOrDefault
                End If

                If objDocumento IsNot Nothing Then
                    nomeRelatorio = Prosegur.Genesis.Utilidad.Util.RecuperarNomeRelatorio(objDocumento)
                    codigoDelegacion = objDocumento.SectorOrigen.Delegacion.Codigo
                End If

                'quando é chamado do recepcion envio
            ElseIf Request("COD_COMPROBANTE_GRUPO") IsNot Nothing Then
                codigoComprobante = Request("COD_COMPROBANTE_GRUPO")
                Dim identificadorGrupoDocumento = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.MaestroGrupoDocumentos.RecuperarIdentificadorPorCodigoComprobante(codigoComprobante)

                Dim objGrupoDocumentos As Prosegur.Genesis.Comon.Clases.GrupoDocumentos = Nothing
                objGrupoDocumentos = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.MaestroGrupoDocumentos.ObtenerGrupoDocumentosPorIdentificador(identificadorGrupoDocumento)
                If objGrupoDocumentos IsNot Nothing Then
                    nomeRelatorio = Prosegur.Genesis.Utilidad.Util.RecuperarNomeRelatorio(objGrupoDocumentos)
                    codigoDelegacion = objGrupoDocumentos.SectorOrigen.Delegacion.Codigo
                End If
            End If

            If String.IsNullOrEmpty(nomeRelatorio) Then
                '* Não existe implementação no sistema para documentos do tipo “Gestão de Contenedores” 
                'e “Outros Movimentos”. Por este motivo, a impressão não estará preparada para trabalhar com os mesmos neste momento. 
                Return
            End If

            'se preencheu a delegação, então deverá recuperar os parametros do servidor de relatório.
            If Not String.IsNullOrEmpty(codigoDelegacion) Then
                Try
                    objRespuesta = Aplicacao.Util.Utilidad.RecuperarParametrosReporte(codigoDelegacion)
                Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
                    MyBase.MostraMensagemErro(ex.Descricao.ToString())
                Catch ex As Exception
                    MyBase.MostraMensagemErro(ex.ToString())
                End Try
            End If

            Dim Carpeta As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes")
            If objRespuesta IsNot Nothing Then
                URL = objRespuesta.Parametros.Where(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_URL).First.ValorParametro
                Dim Usuario As String = objRespuesta.Parametros.Where(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_USER).First.ValorParametro
                Dim Senha As String = objRespuesta.Parametros.Where(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_PASS).First.ValorParametro
                Dim Dominio As String = objRespuesta.Parametros.Where(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_DOMAIN).First.ValorParametro
                Carpeta = objRespuesta.Parametros.Where(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_CARPETA_REPORTES).First.ValorParametro
                objReport.Autenticar(URL, Usuario, Senha, Dominio)
            Else
                objReport.Autenticar(False)
            End If

            Dim Parametros As New List(Of RSE.ParameterValue)
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COD_COMPROBANTE", .Value = codigoComprobante})
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_DES_USUARIO_SOLICITANTE", .Value = String.Format("{0} {1} ({2})", InformacionUsuario.Nombre, InformacionUsuario.Apelido, Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Login)})
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_ReporteDocumentoNegocio", .Value = Prosegur.Genesis.Web.Login.Parametros.Parametro.ReporteDocumentoNegocio})
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_ReporteDocumentoDomicilioTelefono", .Value = Prosegur.Genesis.Web.Login.Parametros.Parametro.ReporteDocumentoDomicilioTelefono})

            Dim Buffer = objReport.RenderReport(String.Format("{0}/DOCUMENTO/{1}", Carpeta, nomeRelatorio), "PDF", Parametros, "PDF", String.Empty, URL)

            Try
                'Depois de gerar o relatório então será atualizado o bol_impresso para true
                If nomeRelatorio.Contains("Individual") Then
                    Prosegur.Genesis.LogicaNegocio.GenesisSaldos.MaestroDocumentos.ActualizaBolImpreso(String.Empty, codigoComprobante, True)
                Else
                    Prosegur.Genesis.LogicaNegocio.GenesisSaldos.MaestroGrupoDocumentos.ActualizaBolImpreso(String.Empty, codigoComprobante, True)
                End If
            Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
                MyBase.MostraMensagemErro(ex.Descricao.ToString())
            Catch ex As Exception
                MyBase.MostraMensagemErro(ex.ToString())
            End Try

            Response.ClearHeaders()
            Response.ClearContent()
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Length", Buffer.Length)
            Response.BinaryWrite(Buffer)
            Response.Buffer = True
            Response.Flush()
            Response.Clear()
            Response.End()

        End If

    End Sub

End Class
