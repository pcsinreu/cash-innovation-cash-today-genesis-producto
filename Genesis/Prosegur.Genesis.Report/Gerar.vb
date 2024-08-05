Imports System.Net
Imports Prosegur.Genesis.Report.RSE
Imports System.Globalization
Imports System.IO
Imports System.Xml
Imports Prosegur.Genesis.Report.RS2010
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC

Public Class Gerar

#Region "[VARIÁVEIS]"

    Protected Shared objRs2010 As RS2010.ReportingService2010
    Protected Shared objRs2005 As RS2005.ReportingService2005
    Protected Shared objRSG As RSE.ReportExecutionService
    Protected Shared strReportServicesURL As String

#End Region

#Region "[REPORTING SERVICE]"

    Public Sub Gerar(defaultCredential As Boolean, reportServicesURL As String)
        Me.ConnectRS(defaultCredential, reportServicesURL)
    End Sub

    Public Sub Autenticar(reportServicesURL As String, usuario As String, senha As String, dominio As String)
        Dim credencial As NetworkCredential = New NetworkCredential
        credencial.UserName = usuario
        credencial.Password = senha
        credencial.Domain = dominio

        objRs2010 = New RS2010.ReportingService2010
        objRs2005 = New RS2005.ReportingService2005
        strReportServicesURL = reportServicesURL
        objRs2005.Credentials = credencial
        objRs2010.Credentials = credencial
        objRs2010.Url = strReportServicesURL + Constantes.wsdl2010
        objRs2005.Url = strReportServicesURL + Constantes.wsdl2005
    End Sub

    Public Sub Autenticar(defaultCredential As Boolean, reportServicesURL As String)
        Dim credencial = GetCredencial(defaultCredential)

        objRs2010 = New RS2010.ReportingService2010
        objRs2005 = New RS2005.ReportingService2005

        strReportServicesURL = reportServicesURL
        objRs2005.Credentials = credencial
        objRs2010.Credentials = credencial
        objRs2010.Url = strReportServicesURL + Constantes.wsdl2010
        objRs2005.Url = strReportServicesURL + Constantes.wsdl2005
    End Sub

    Public Sub Autenticar(defaultCredential As Boolean)
        Dim credencial = GetCredencial(defaultCredential)

        objRs2010 = New RS2010.ReportingService2010
        objRs2005 = New RS2005.ReportingService2005

        strReportServicesURL = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("ReportServicesURL")
        objRs2005.Credentials = credencial
        objRs2010.Credentials = credencial
        objRs2010.Url = strReportServicesURL + Constantes.wsdl2010
        objRs2005.Url = strReportServicesURL + Constantes.wsdl2005
    End Sub

    ''' <summary>
    ''' Recupera as credenciais.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCredencial(defaultCredential As Boolean) As NetworkCredential

        Dim credencial As NetworkCredential = New NetworkCredential
        If (defaultCredential) Then
            credencial = System.Net.CredentialCache.DefaultCredentials
        Else
            credencial.UserName = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("RS_A_USER")
            credencial.Password = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("RS_a_PASS")
            credencial.Domain = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("RS_A_DOMAIN")
        End If

        Return credencial

    End Function

    ''' <summary>
    ''' Credencial do servidor de relatório.
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="senha"></param>
    ''' <param name="dominio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCredencial(usuario As String, senha As String, dominio As String) As NetworkCredential

        Dim credencial As NetworkCredential = New NetworkCredential

        credencial.UserName = usuario
        credencial.Password = senha
        credencial.Domain = dominio
        Return credencial

    End Function


    ''' <summary>
    ''' Conecta ao report services
    ''' </summary>
    ''' <param name="reportServicesURL">Url do servidor de relatório.</param>
    ''' <remarks></remarks>
    Private Sub ConnectRS(defaultCredential As Boolean, reportServicesURL As String)
        Dim credential = Me.GetCredencial(defaultCredential)

        objRs2010 = New RS2010.ReportingService2010
        objRs2005 = New RS2005.ReportingService2005
        strReportServicesURL = reportServicesURL

        If defaultCredential Then
            objRs2005.Credentials = System.Net.CredentialCache.DefaultCredentials
            objRs2010.Credentials = System.Net.CredentialCache.DefaultCredentials
        Else
            objRs2005.Credentials = credential
            objRs2010.Credentials = credential
            objRs2010.Url = strReportServicesURL + Constantes.wsdl2010
            objRs2005.Url = strReportServicesURL + Constantes.wsdl2005
        End If

    End Sub

    Private Function ConnectExecutionRS(ReportServicesURL As String) As RSE.ReportExecutionService
        'Create an instance of the Web service proxy and set the Url property
        Dim _objReporExecSev As New RSE.ReportExecutionService()

        _objReporExecSev.Credentials = objRs2010.Credentials
        If String.IsNullOrEmpty(strReportServicesURL) Then
            _objReporExecSev.Url = ReportServicesURL & Constantes.wsdlreportExecution
        Else
            _objReporExecSev.Url = strReportServicesURL & Constantes.wsdlreportExecution
        End If

        Return _objReporExecSev
    End Function


    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Public Function DisplayItems(Path As String) As RS2010.CatalogItem()

        Dim catalogItems As Prosegur.Genesis.Report.RS2010.CatalogItem() = Nothing

        ''Call RS ListChildren
        catalogItems = objRs2010.ListChildren(Path, False)

        If catalogItems IsNot Nothing AndAlso catalogItems.Count > 0 Then

            For Each catalog In catalogItems
                catalog.Name = catalog.Name.ToUpper
            Next

        End If

        Return catalogItems

    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Public Function ListarParametros(Path As String, objValores As RS2005.ParameterValue()) As List(Of RS2005.ReportParameter)

        Dim _historyID As String = Nothing
        Dim _forRendering As Boolean = False
        Dim _parametros As RS2005.ReportParameter() = Nothing
        Dim _credentials As RS2005.DataSourceCredentials() = Nothing
        Dim objParametros As List(Of RS2005.ReportParameter) = Nothing

        Try
            _parametros = objRs2005.GetReportParameters(Path, _historyID, _forRendering, objValores, _credentials)

            'Main part of method 
            If _parametros IsNot Nothing AndAlso _parametros.Count > 0 Then

                objParametros = New List(Of RS2005.ReportParameter)

                For Each parametro In _parametros
                    objParametros.Add(parametro)
                Next

            End If

        Catch ex As Exception
            Throw
        End Try

        Return objParametros

    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Public Function ListarParametros(Path As String, objValores As RS2010.ParameterValue()) As List(Of RS2010.ItemParameter)

        Dim _historyID As String = Nothing
        Dim _forRendering As Boolean = False
        Dim _parametros As RS2010.ItemParameter() = Nothing
        Dim _credentials As RS2010.DataSourceCredentials() = Nothing
        Dim objParametros As List(Of RS2010.ItemParameter) = Nothing

        Try

            _parametros = objRs2010.GetItemParameters(Path, _historyID, _forRendering, objValores, _credentials)

            'Main part of method 
            If _parametros IsNot Nothing AndAlso _parametros.Count > 0 Then

                objParametros = New List(Of RS2010.ItemParameter)

                For Each parametro In _parametros
                    parametro.Name = parametro.Name
                    objParametros.Add(parametro)
                Next

            End If

        Catch ex As Exception
            Throw
        End Try

        Return objParametros

    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Public Function ListarParametrosMaiusculo(Path As String, objValores As RS2010.ParameterValue()) As List(Of RS2010.ItemParameter)

        Dim _historyID As String = Nothing
        Dim _forRendering As Boolean = False
        Dim _parametros As RS2010.ItemParameter() = Nothing
        Dim _credentials As RS2010.DataSourceCredentials() = Nothing
        Dim objParametros As List(Of RS2010.ItemParameter) = Nothing

        Try

            _parametros = objRs2010.GetItemParameters(Path, _historyID, _forRendering, objValores, _credentials)

            'Main part of method 
            If _parametros IsNot Nothing AndAlso _parametros.Count > 0 Then

                objParametros = New List(Of RS2010.ItemParameter)

                For Each parametro In _parametros
                    parametro.Name = parametro.Name.ToUpper
                    objParametros.Add(parametro)
                Next

            End If

        Catch ex As Exception
            Throw
        End Try

        Return objParametros

    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Public Function RenderReport(reportPath As String, renderformat As String, listaParametros As List(Of RSE.ParameterValue),
                                 ByRef Extension As String, Optional Separador As String = "", Optional ReportServicesURL As String = "") As Byte()

        Dim log As New System.Text.StringBuilder
        Dim deviceInfo As String = String.Empty
        Dim Resultados As Byte() = Nothing
        Dim Codificacion As String = String.Empty
        Dim mimeType As String = String.Empty
        Dim warnings As RSE.Warning() = Nothing
        Dim history As String = Nothing
        Dim listaParametrosReport As New List(Of RSE.ParameterValue)
        Try
            log.AppendFormat("reportPath:{0} ", reportPath)
            log.AppendFormat("strReportServicesURL:{0} ", strReportServicesURL)
            If Not String.IsNullOrEmpty(ReportServicesURL) Then
                log.AppendFormat("ReportServicesURL:{0} ", ReportServicesURL)
            End If

            'Se existir separador então a variavel deviceInfo deverá receber as tags
            If Not String.IsNullOrEmpty(Separador) AndAlso renderformat.ToUpper = "CSV" Then
                deviceInfo = "<DeviceInfo>"
                deviceInfo += "<ExcelMode>True</ExcelMode>"

                If Separador = "T" Then
                    'T, indica que el archivo tendrá campos delimitados por tabulación;
                    Separador = "&#9;"

                ElseIf Separador = "F" Then
                    'F, indica que el archivo tendrá campos de tamaño fijos;
                    Separador = String.Empty
                End If

                deviceInfo += "<FieldDelimiter>" & Separador & "</FieldDelimiter>"
                deviceInfo += "</DeviceInfo>"
            Else
                If renderformat.ToUpper = "CSV" Then
                    deviceInfo = "<DeviceInfo><ExcelMode>True</ExcelMode></DeviceInfo>"
                End If
            End If

            If listaParametros IsNot Nothing AndAlso listaParametros.Count > 0 Then
                listaParametrosReport = listaParametros
            End If

            objRSG = ConnectExecutionRS(ReportServicesURL)
            Dim execInfo As New RSE.ExecutionInfo
            Dim execHeader As New RSE.ExecutionHeader
            objRSG.ExecutionHeaderValue = execHeader
            'execHeader.ExecutionID = System.Guid.NewGuid.ToString
            execInfo = objRSG.LoadReport(reportPath, history)

            Dim streamsIDs As String() = Nothing

            Try
                objRSG.SetExecutionParameters(listaParametrosReport.ToArray(), CultureInfo.CurrentCulture.Name)
                'Verifica se o time out foi informado
                Dim strTimeout = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("Timeout")
                Dim timeOut As Int32 = 0
                'Se não conseguiu encontrar o time out, 
                If Not Int32.TryParse(strTimeout, timeOut) Then
                    'set o valor para 10 minutos. valor em millisegundos
                    timeOut = 900000
                End If

                objRSG.Timeout = timeOut
                Resultados = objRSG.Render(renderformat, deviceInfo, Extension, mimeType, Codificacion, warnings, streamsIDs)

                'Se for CSV, então retira as asps duplas
                If renderformat.ToUpper = "CSV" Then
                    Dim encoding As New System.Text.UTF8Encoding
                    Dim strArquivo As String = encoding.GetString(Resultados)

                    If strArquivo.Contains(vbNewLine) Then

                        Dim lineas As String() = strArquivo.Split(vbNewLine + vbNewLine)

                        strArquivo = String.Empty

                        For Each linea In lineas

                            linea = Replace(linea, Chr(10), "")

                            If linea IsNot Nothing Then
                                'Verifica se o primeiro caracter é aspas duplas, se for remove o primeiro e o próximo
                                If linea.StartsWith("""") Then
                                    'Remoe a primeira
                                    linea = linea.Substring(1)

                                    If linea.StartsWith("""") Then
                                        linea = linea.Substring(1)
                                    End If
                                End If

                                If linea.Length > 0 Then
                                    If Not String.IsNullOrEmpty(strArquivo) Then strArquivo &= vbNewLine
                                    strArquivo &= linea
                                End If

                            End If
                        Next
                    End If
                    Resultados = encoding.GetBytes(strArquivo)
                End If

            Catch ex As Exception
                Throw
            End Try
        Catch ex As Exception
            Throw New Exception(log.ToString + " " + ex.Message, ex)
        End Try

        Return Resultados

    End Function

    ''' <summary>
    ''' obtem os parametros do relatório
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/05/2013 - Criado
    ''' </history>
    Public Function ObtenerParametrosReporte(CodigoReporte As String, objReportes As RS2010.CatalogItem()) As List(Of RS2005.ReportParameter)

        Dim objParametrosReporte As List(Of RS2005.ReportParameter) = Nothing

        If Not String.IsNullOrEmpty(CodigoReporte) AndAlso objReportes IsNot Nothing AndAlso objReportes.Count > 0 Then

            Dim objReporte As RS2010.CatalogItem = (From objR In objReportes Where objR.Name = CodigoReporte).FirstOrDefault

            If objReporte IsNot Nothing Then

                Dim objValores As RS2005.ParameterValue() = Nothing
                Dim objValores2010 As RS2010.ParameterValue() = Nothing

                'Lista os parametros do relatório
                objParametrosReporte = ListarParametros(objReporte.Path, objValores)

            End If

        End If

        Return objParametrosReporte

    End Function

    ''' <summary>
    ''' Gera o relatório
    ''' </summary>
    ''' <param name="CodigoReporte"></param>
    ''' <param name="objReportes"></param>
    ''' <param name="FormatoSalida"></param>
    ''' <param name="Ruta"></param>
    ''' <param name="DesConfiguracion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 07/05/2013 - Criado
    ''' </history>
    Public Sub GerarInforme(CodConfiguracion As String, CodigoReporte As String, objReportes As RS2010.CatalogItem(), FormatoSalida As String,
                             Ruta As String, DesConfiguracion As String,
                             objColParametros As List(Of ParametroReporte), IdReporte As String,
                             FormulaNombre As String, FormatoSalidaSalvar As String,
                             Optional Separador As String = "")

        Dim ParametrosReporte = ObtenerParametrosReporte(CodigoReporte, objReportes)

        Dim execInfo As New RSE.ExecutionInfo()
        Dim execHeader As New RSE.ExecutionHeader()

        Dim _parametros As List(Of RSE.ParameterValue) = PreencherParametros(objColParametros, ParametrosReporte)

        Dim Erros As New List(Of String)

        If FormatoSalidaSalvar.ToLower.Equals(Constantes.extensao_pdf) OrElse FormatoSalidaSalvar.ToLower.Equals(Constantes.extensao_xls) OrElse
                FormatoSalidaSalvar.ToLower.Equals(Constantes.extensao_txt) OrElse FormatoSalidaSalvar.ToLower.Equals(Constantes.extensao_mhtml) OrElse
                FormatoSalidaSalvar.ToLower.Equals(Constantes.extensao_csv) OrElse FormatoSalidaSalvar.ToLower.Equals(Constantes.extensao_xlsx) Then
            FormatoSalidaSalvar = String.Format("[{0}]", FormatoSalidaSalvar)
        End If

        'Gera o nome do arquivo
        Dim NombreArchivo As String = GerarNomeExtensao(Ruta, CodigoReporte, IdReporte, FormulaNombre.ToUpper, _parametros, Erros)
        Dim ExtencionArchivo As String = GerarNomeExtensao(Ruta, CodigoReporte, IdReporte, FormatoSalidaSalvar.ToUpper, _parametros, Erros)

        'Se ocorreu erro no momento de gerar o nome.
        If Erros.Count > 0 Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                 Join(CType((From e In Erros Where Not String.IsNullOrEmpty(e)
                                                             Select e.Trim()), IEnumerable(Of String)).ToArray(), vbCrLf))
        End If

        'Utilizado para preencher o parâmetro quando for P_NOMBRE_ARCHIVO
        'não é preenchido no PreencherParametros() pois nesse momento não temos o Nome e neim a Extensão
        'e para obtelas precisamos dos parâmetros preenchidos
        If ParametrosReporte IsNot Nothing Then
            If (From param In ParametrosReporte Where param.Name.Equals(Constantes.CONST_P_NOMBRE_ARCHIVO)).Count > 0 Then

                Dim pNombreArchivo As New RSE.ParameterValue
                pNombreArchivo = (From pNombre In _parametros Where pNombre.Name.Equals(Constantes.CONST_P_NOMBRE_ARCHIVO)).FirstOrDefault

                _parametros.Remove(pNombreArchivo)

                pNombreArchivo = New RSE.ParameterValue
                pNombreArchivo.Name = Constantes.CONST_P_NOMBRE_ARCHIVO
                pNombreArchivo.Label = NombreArchivo & "." & ExtencionArchivo
                pNombreArchivo.Value = NombreArchivo & "." & ExtencionArchivo

                _parametros.Add(pNombreArchivo)
            End If
        End If

        Dim objRelatorio = (From objR In objReportes Where objR.Name = CodigoReporte).FirstOrDefault
        Dim Extension As String = Nothing

        'Recupera o relatório
        Dim Resultados As Byte() = RenderReport(objRelatorio.Path, FormatoSalida, _parametros, Extension, Separador)

        'Grava o relatório na base
        If Resultados IsNot Nothing Then


            Dim DiretorioSalvar As String = String.Format("{0}\{1}.{2}", Ruta, NombreArchivo, ExtencionArchivo)

            'Se o arquivo ja existir, deleta o arquivo
            If System.IO.File.Exists(DiretorioSalvar) Then

                Try
                    System.IO.File.Delete(DiretorioSalvar)

                Catch ex As Exception
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                        String.Format(Traduzir("err_msg_error_apagar_arquivo"), DiretorioSalvar))
                End Try

            End If

            'Salva arquivo no disco
            System.IO.File.WriteAllBytes(DiretorioSalvar, Resultados)

        End If

    End Sub

    ''' <summary>
    ''' Retorna o nome do arquivo
    ''' </summary>
    ''' <param name="Extension"></param>
    ''' <param name="Ruta"></param>
    ''' <param name="CodigoReporte"></param>
    ''' <param name="Desconfiguracion"></param>
    ''' <param name="Cliente"></param>
    ''' <param name="SubCliente"></param>
    ''' <param name="PuntoServicio"></param>
    ''' <param name="GrupoCliente"></param>
    ''' <param name="AgrupaPorcliente"></param>
    ''' <param name="AgrupaPorSubCliente"></param>
    ''' <param name="AgrupaPorPuntoServicio"></param>
    ''' <param name="AgrupaPorGrupoCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' Os parâmetros fantasma foram deixados no método segundo solicitação da Joyce, Anselmo e Leandro.
    Private Function RetornarNombreArchivo(Extension As String, Ruta As String, CodigoReporte As String, Desconfiguracion As String,
                                           Cliente As String, SubCliente As String, PuntoServicio As String, GrupoCliente As String,
                                           AgrupaPorcliente As Boolean, AgrupaPorSubCliente As Boolean, AgrupaPorPuntoServicio As Boolean,
                                           AgrupaPorGrupoCliente As Boolean, CodConfiguracion As String) As String

        Dim NombreArchivo As String = String.Empty

        If AgrupaPorcliente OrElse AgrupaPorGrupoCliente Then
            NombreArchivo = String.Format("{0}\{1}_{2}." & Extension, Ruta, Desconfiguracion, CodConfiguracion)
        ElseIf AgrupaPorSubCliente Then
            NombreArchivo = String.Format("{0}\{1}_{2}." & Extension, Ruta, Desconfiguracion, CodConfiguracion)
        ElseIf AgrupaPorPuntoServicio Then
            NombreArchivo = String.Format("{0}\{1}_{2}." & Extension, Ruta, Desconfiguracion, CodConfiguracion)
        Else

            If Not String.IsNullOrEmpty(Cliente) Then

                If Not String.IsNullOrEmpty(SubCliente) Then

                    If Not String.IsNullOrEmpty(PuntoServicio) Then
                        NombreArchivo = String.Format("{0}\{1}_{2}." & Extension, Ruta, Desconfiguracion, CodConfiguracion)
                    Else
                        NombreArchivo = String.Format("{0}\{1}_{2}." & Extension, Ruta, Desconfiguracion, CodConfiguracion)
                    End If
                Else
                    NombreArchivo = String.Format("{0}\{1}_{2}." & Extension, Ruta, Desconfiguracion, CodConfiguracion)
                End If

            ElseIf Not String.IsNullOrEmpty(AgrupaPorGrupoCliente) Then
                NombreArchivo = String.Format("{0}\{1}_{2}_{3}." & Extension, Ruta, Desconfiguracion, CodConfiguracion, GrupoCliente)
            Else
                NombreArchivo = String.Format("{0}\{1}_{2}." & Extension, Ruta, Desconfiguracion, CodConfiguracion)
            End If

        End If

        Return NombreArchivo

    End Function

    ''' <summary>
    ''' Preenche os parametros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PreencherParametros(objColParametros As List(Of ParametroReporte), objParametrosReporte As List(Of RS2005.ReportParameter)) As List(Of RSE.ParameterValue)

        Dim objParametros As New List(Of RSE.ParameterValue)
        Dim paramValue As RSE.ParameterValue = Nothing
        Dim adicionouParametro As Boolean = False

        If objParametrosReporte IsNot Nothing AndAlso objParametrosReporte.Count > 0 Then

            For Each param In objParametrosReporte
                adicionouParametro = False

                If objColParametros IsNot Nothing AndAlso objColParametros.Count > 0 Then
                    For Each paramReporte In objColParametros.Where(Function(p) p.CodParametro.ToUpper = param.Name.ToUpper)
                        paramValue = New RSE.ParameterValue
                        paramValue.Name = param.Name

                        If Not String.IsNullOrEmpty(paramReporte.DesValorParametro) Then
                            paramValue.Value = paramReporte.DesValorParametro
                            paramValue.Label = paramReporte.DesParametro
                        Else
                            'Verifica se é um parametro de multivalue que não é obrigatório mas e permite valor branco.
                            If param.AllowBlank AndAlso param.MultiValue AndAlso Not param.ValidValuesQueryBased AndAlso Not param.Nullable Then
                                paramValue.Value = ""
                            ElseIf param.AllowBlank AndAlso Not param.Nullable Then
                                paramValue.Value = ""
                            End If
                        End If

                        objParametros.Add(paramValue)
                        adicionouParametro = True
                    Next
                End If

                'se o parametro não foi adicionado
                If Not adicionouParametro Then
                    paramValue = New RSE.ParameterValue
                    paramValue.Name = param.Name

                    'Verifica se o parametro possuie valor padrão
                    If param.DefaultValues IsNot Nothing AndAlso param.DefaultValues.Count(Function(v) Not String.IsNullOrEmpty(v)) > 0 Then

                    ElseIf param.AllowBlank AndAlso param.MultiValue AndAlso Not param.ValidValuesQueryBased AndAlso Not param.Nullable Then
                        paramValue.Value = ""
                        objParametros.Add(paramValue)
                    ElseIf param.AllowBlank AndAlso Not param.Nullable Then
                        paramValue.Value = ""
                        objParametros.Add(paramValue)
                    End If
                End If
            Next

        End If

        Return objParametros

    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Public Function ListaCatalogItem(Path As String) As RS2010.CatalogItem()

        Dim catalogItems As Prosegur.Genesis.Report.RS2010.CatalogItem() = Nothing

        ''Call RS ListChildren
        catalogItems = objRs2010.ListChildren(Path, False)

        If catalogItems IsNot Nothing AndAlso catalogItems.Count > 0 Then

            For Each catalog In catalogItems
                catalog.Name = catalog.Name.ToUpper
            Next

        End If

        Return catalogItems

    End Function

    Public Function ParametrosRelatorio(PathRelatorio As String) As List(Of Parametro)
        Dim listaParametros As New List(Of Parametro)
        Dim reportDefinition() As Byte = Nothing
        Dim nomeRelatorio As String = String.Empty
        Try
            If Not String.IsNullOrEmpty(PathRelatorio) Then
                nomeRelatorio = PathRelatorio.Substring(PathRelatorio.LastIndexOf("/") + 1)
            End If

            Dim objValores As RS2010.ParameterValue() = Nothing
            Dim listaParameter = Me.ListarParametros(PathRelatorio, objValores)
            Dim sb As New System.Text.StringBuilder

            'Verifica se algum parametro é de consulta através de select ou dataset.
            Dim nomeParametros As New List(Of String)
            If listaParameter IsNot Nothing AndAlso listaParameter.Count > 0 Then
                For Each item In listaParameter.Where(Function(p) p.ValidValuesQueryBased)
                    nomeParametros.Add(item.Name)
                Next
            End If

            If (nomeParametros.Count > 0) Then
                listaParametros = Me.ParametrosDeConsulta(PathRelatorio, nomeParametros)
                For Each param In listaParametros
                    param.NameOriginal = param.Name
                    param.Name = param.Name.ToUpper
                    Dim nomeParametro = param.Name
                    Dim itemParam = listaParameter.Where(Function(p) p.Name.ToUpper = nomeParametro).FirstOrDefault

                    If Not itemParam Is Nothing Then
                        param.NomeRelatorio = nomeRelatorio
                        param.PathRelatorio = PathRelatorio
                        param.MultiValue = itemParam.MultiValue
                        param.Required = Not itemParam.Nullable AndAlso Not itemParam.AllowBlank
                        param.Visible = IIf(itemParam.Prompt = String.Empty, False, True)
                        If Not itemParam.Dependencies Is Nothing Then
                            param.Dependencies = New List(Of Dependencie)
                            For Each depend In itemParam.Dependencies
                                Dim nomeParam As String = depend
                                ' verifica se o parametro pai é multi value
                                Dim multivalue As Boolean = listaParameter.Exists(Function(p) p.Name.ToUpper = nomeParam.ToUpper AndAlso p.MultiValue)
                                param.Dependencies.Add(New Dependencie() With {.Codigo = nomeParam.ToUpper, .CodigoOriginal = nomeParam, .MultiValue = multivalue})
                            Next
                        End If
                    End If
                Next
            Else

                If listaParameter IsNot Nothing AndAlso listaParameter.Count > 0 Then

                    For Each itemParam In listaParameter
                        Dim param As New Parametro
                        param.NomeRelatorio = nomeRelatorio
                        param.PathRelatorio = PathRelatorio
                        param.MultiValue = itemParam.MultiValue
                        param.Required = Not itemParam.Nullable AndAlso Not itemParam.AllowBlank
                        param.Visible = IIf(itemParam.Prompt = String.Empty, False, True)
                        param.NameOriginal = itemParam.Name
                        param.Name = itemParam.Name.ToUpper
                        param.DataType = itemParam.ParameterTypeName
                        param.Prompt = itemParam.Prompt

                        If itemParam.ValidValues IsNot Nothing AndAlso itemParam.ValidValues.Count > 0 Then

                            param.ValidValue = New Prosegur.Genesis.Report.ValidValue
                            param.ValidValue.ParameterValues = New List(Of Prosegur.Genesis.Report.ParameterValue)

                            For Each Valor In itemParam.ValidValues

                                param.ValidValue.ParameterValues.Add(New Prosegur.Genesis.Report.ParameterValue With {
                                                                     .Label = Valor.Label,
                                                                     .Value = Valor.Value})
                            Next

                        End If

                        If Not itemParam.Dependencies Is Nothing Then
                            param.Dependencies = New List(Of Dependencie)
                            For Each depend In itemParam.Dependencies
                                Dim nomeParam As String = depend
                                ' verifica se o parametro pai é multi value
                                Dim multivalue As Boolean = listaParameter.Exists(Function(p) p.Name.ToUpper = nomeParam.ToUpper AndAlso p.MultiValue)
                                param.Dependencies.Add(New Dependencie() With {.Codigo = nomeParam.ToUpper, .CodigoOriginal = nomeParam, .MultiValue = multivalue})
                            Next
                        End If

                        listaParametros.Add(param)
                    Next

                End If

            End If

        Catch ex As Exception
            Throw
        End Try

        Return listaParametros

    End Function

    Private Function ParametrosDeConsulta(PathRelatorio As String, parametros As List(Of String)) As List(Of Parametro)
        Dim listaParametros As New List(Of Parametro)
        Dim reportDefinition() As Byte = Nothing

        Try
            reportDefinition = objRs2010.GetItemDefinition(PathRelatorio)
            Dim mstream As MemoryStream = Nothing
            Dim doc As System.Xml.XmlDocument = New System.Xml.XmlDocument
            mstream = New MemoryStream(reportDefinition)

            doc.Load(mstream)
            mstream.Position = 0
            Dim xreport As XElement = XElement.Load(New XmlNodeReader(doc))
            Dim dns As String = "{" & xreport.GetDefaultNamespace.ToString() & "}"
            Dim seconddns As String = ""
            If (Not (xreport.GetNamespaceOfPrefix("rd")) Is Nothing) Then
                seconddns = ("{" & (xreport.GetNamespaceOfPrefix("rd").ToString() & "}"))
            End If

            Dim valor As String = String.Empty
            Dim strDataType As String = String.Empty
            Dim nome As String = String.Empty

            'recupera os dataset com select no relatório.
            Dim x_ds = xreport.Element(dns + "DataSets").Elements(dns + "DataSet")
            Dim listaDataSet As New List(Of DataSet)

            For Each xe_DataSet As XElement In x_ds

                Dim item = xe_DataSet.Element(dns + "Query")
                If Not item Is Nothing Then
                    Dim objDataset As New DataSet()
                    objDataset.CommandText = item.Element(dns + "CommandText").Value
                    objDataset.DataSourceName = item.Element(dns + "DataSourceName").Value
                    objDataset.Name = xe_DataSet.Attribute("Name").Value
                    listaDataSet.Add(objDataset)
                End If

            Next

            'Get the DataSources
            Dim parameters = xreport.Element((dns + "ReportParameters")).Elements((dns + "ReportParameter"))
            For Each xe_ds As XElement In parameters
                Dim parametro As New Parametro

                parametro.Name = xe_ds.Attribute("Name").Value
                Dim item = xe_ds.Element((dns + "DataType"))
                If (Not (item) Is Nothing) Then
                    parametro.DataType = item.Value
                End If

                item = xe_ds.Element((dns + "Prompt"))
                If (Not (item) Is Nothing) Then
                    parametro.Prompt = item.Value
                End If

                item = xe_ds.Element((dns + "DefaultValue"))
                If (Not (item) Is Nothing) Then
                    parametro.DefaultValue = New DefaultValue
                    Dim values = xe_ds.Element((dns + "DefaultValue")).Elements((dns + "Values"))

                    For Each xe As XElement In values
                        item = xe.Element((dns + "Value"))
                        If (Not (item) Is Nothing) Then
                            parametro.DefaultValue.Values.Add(item.Value)
                        End If
                    Next

                    Dim dsReference = xe_ds.Element((dns + "DefaultValue")).Elements((dns + "DataSetReference"))

                    parametro.DefaultValue.DataSetReference = New DataSetReference
                    For Each xe As XElement In dsReference
                        item = xe.Element((dns + "DataSetName"))
                        If (Not (item) Is Nothing) Then
                            parametro.DefaultValue.DataSetReference.DataSetName = item.Value
                            Dim ds = listaDataSet.Where(Function(d) d.Name = item.Value).FirstOrDefault
                            If Not ds Is Nothing Then
                                parametro.DefaultValue.DataSetReference.CommandText = ds.CommandText
                                parametro.DefaultValue.DataSetReference.Conexao = ds.DataSourceName
                            End If
                        End If
                        item = xe.Element((dns + "ValueField"))
                        If (Not (item) Is Nothing) Then
                            parametro.DefaultValue.DataSetReference.ValueField = item.Value
                        End If
                    Next
                End If

                item = xe_ds.Element((dns + "ValidValues"))
                If (Not (item) Is Nothing) Then
                    parametro.ValidValue = New ValidValue
                    parametro.ValidValue.DataSetReference = New DataSetReference
                    Dim dsReference = xe_ds.Element((dns + "ValidValues")).Elements((dns + "DataSetReference"))
                    For Each xe As XElement In dsReference
                        parametro.ValidValue.DataSetReference = New DataSetReference
                        item = xe.Element((dns + "DataSetName"))
                        If (Not (item) Is Nothing) Then
                            parametro.ValidValue.DataSetReference.DataSetName = item.Value
                            parametro.DataSet = True
                            Dim ds = listaDataSet.Where(Function(d) d.Name = item.Value).FirstOrDefault
                            If Not ds Is Nothing Then
                                parametro.ValidValue.DataSetReference.CommandText = ds.CommandText
                                parametro.ValidValue.DataSetReference.Conexao = ds.DataSourceName
                            End If
                        End If
                        item = xe.Element((dns + "ValueField"))
                        If (Not (item) Is Nothing) Then
                            parametro.ValidValue.DataSetReference.ValueField = item.Value
                        End If
                        item = xe.Element((dns + "LabelField"))
                        If (Not (item) Is Nothing) Then
                            parametro.ValidValue.DataSetReference.LabelField = item.Value
                        End If
                    Next
                    Dim validValues = xe_ds.Element((dns + "ValidValues")).Elements((dns + "ParameterValues"))
                    parametro.ValidValue.ParameterValues = New List(Of ParameterValue)
                    For Each xe As XElement In validValues
                        For Each paraValue In xe.Elements((dns + "ParameterValue"))
                            Dim param As ParameterValue = New ParameterValue
                            item = paraValue.Element((dns + "Value"))
                            If (Not (item) Is Nothing) Then
                                param.Value = item.Value
                            End If
                            item = paraValue.Element((dns + "Label"))
                            If (Not (item) Is Nothing) Then
                                param.Label = item.Value
                            End If
                            parametro.ValidValue.ParameterValues.Add(param)
                        Next
                    Next
                End If

                listaParametros.Add(parametro)
            Next

            'Recupera os dadaSets compartilhados
            Dim dataSets() As ItemReferenceData = objRs2010.GetItemReferences(PathRelatorio, "DataSet")
            For Each dataset In dataSets
                ' verifica se esse dataSet está vinculado a algum parametro
                Dim nomeDataset As String = dataset.Name
                For Each param In listaParametros.Where(Function(p) Not p.DefaultValue Is Nothing AndAlso Not p.DefaultValue.DataSetReference Is Nothing AndAlso p.DefaultValue.DataSetReference.DataSetName = nomeDataset)
                    Me.ParametroSQL(dataset.Reference, param.DefaultValue.DataSetReference)
                Next

                For Each param In listaParametros.Where(Function(p) Not p.ValidValue Is Nothing AndAlso Not p.ValidValue.DataSetReference Is Nothing AndAlso p.ValidValue.DataSetReference.DataSetName = nomeDataset)
                    If Not String.IsNullOrEmpty(dataset.Reference) Then
                        Me.ParametroSQL(dataset.Reference, param.ValidValue.DataSetReference)
                    End If
                Next

            Next
        Catch ex As Exception
            Throw
        End Try

        Return listaParametros

    End Function

    Private Sub ParametroSQL(PathdDataSet As String, ByRef dsReference As DataSetReference)
        Dim SQL As String = String.Empty
        Dim reportDefinition() As Byte = Nothing
        Dim mstream As MemoryStream = Nothing
        Try
            reportDefinition = objRs2010.GetItemDefinition(PathdDataSet)
            mstream = New MemoryStream(reportDefinition)
            Dim doc As System.Xml.XmlDocument = New System.Xml.XmlDocument
            doc.Load(mstream)
            mstream.Position = 0

            Dim xreport As XElement = XElement.Load(New XmlNodeReader(doc))
            Dim dns As String = "{" & xreport.GetDefaultNamespace.ToString() & "}"
            Dim seconddns As String = ""
            If (Not (xreport.GetNamespaceOfPrefix("rd")) Is Nothing) Then
                seconddns = ("{" & (xreport.GetNamespaceOfPrefix("rd").ToString() & "}"))
            End If

            Dim xe_Query = xreport.Element((dns + "DataSet")).Elements((dns + "Query"))
            dsReference.CommandText = xe_Query.Elements(dns + "CommandText").Value

            Dim CommandType = xe_Query.Elements(dns + "CommandType").Value
            If CommandType = "StoredProcedure" Then
                dsReference.StoredProcedure = True
            End If

            dsReference.Conexao = xe_Query.Elements(dns + "DataSourceReference").Value
            mstream.Close()
        Catch ex As Exception
            Throw
        End Try

    End Sub
    ''' <summary>
    ''' Valida o nome/extensão do arquivo.
    ''' </summary>
    ''' <param name="PathRelatorio">Caminho do relatóiro a ser validado.</param>
    ''' <param name="nomeExtensao">Nome/Extensão a ser validada.</param>
    ''' <param name="campo">Descrição do campo a ser validado.</param>
    ''' <returns>Retorna  uma lista de mensanges de erros quando ocorrer.</returns>
    ''' <remarks></remarks>
    Public Function ValidarNomeExtensao(PathRelatorio As String, nomeExtensao As String, campo As String) As List(Of String)
        Dim listaParametros As New List(Of Parametro)
        Dim reportDefinition() As Byte = Nothing
        Dim listaErros As New List(Of String)
        Dim nome As String = String.Empty
        Try
            Dim objValores As RS2010.ParameterValue() = Nothing
            'Recupera todos os parametros do relatório.
            Dim listaParameter = Me.ListarParametros(PathRelatorio, objValores)
            Dim sb As New System.Text.StringBuilder

            listaErros = Me.ValidarNomeExtensao(nomeExtensao, listaParameter, campo)
        Catch ex As Exception
            Throw
        End Try

        Return listaErros

    End Function

    ''' <summary>
    ''' Valida o nome/extensão do arquivo.
    ''' </summary>
    ''' <param name="nomeExtensao">Nome/Extensão a ser validada.</param>
    ''' <param name="parametros">Parametros do relatório.</param>
    ''' <param name="campo">Descrição do campo que está sendo validado.</param>
    ''' <returns>Retorna  uma lista de mensanges de erros quando ocorrer.</returns>
    ''' <remarks></remarks>
    Private Function ValidarNomeExtensao(nomeExtensao As String, parametros As List(Of RS2010.ItemParameter), campo As String) As List(Of String)
        Dim limitador As String = String.Empty
        Dim limitadorAbertura As String
        Dim indexFechamento As Integer = 0
        Dim espacos As String = "                             "
        Dim limitadorAnterior As String = String.Empty
        limitadorAbertura = nomeExtensao.Substring(0, 1)
        Dim ListaErros As New List(Of String)
        Dim resposta As String = String.Empty
        Do While nomeExtensao.Length > 0
            limitadorAbertura = nomeExtensao.Substring(0, 1)

            resposta = Me.ValidarLimitador(limitadorAnterior, limitador, nomeExtensao, limitadorAbertura, campo)
            limitadorAnterior = limitador
            If String.IsNullOrEmpty(resposta) Then
                If (limitadorAbertura = Constantes.limitador_depara_abertura) Then
                    Dim nomeExtensaoCombinacao = limitador
                    Do While nomeExtensaoCombinacao.Length > 0
                        limitadorAbertura = nomeExtensaoCombinacao.Substring(0, 1)
                        resposta = Me.ValidarLimitador(limitadorAnterior, limitador, nomeExtensaoCombinacao, limitadorAbertura, campo)
                        If String.IsNullOrEmpty(resposta) Then
                            resposta = Me.ValidarLimitadorParametro(limitador, limitadorAbertura, parametros)
                            If Not String.IsNullOrEmpty(resposta) Then
                                ListaErros.Add(String.Format(Traduzir("027_msg_campo"), campo) & resposta)
                            End If
                        Else
                            ListaErros.Add(resposta)
                        End If
                    Loop
                Else
                    resposta = Me.ValidarLimitadorParametro(limitador, limitadorAbertura, parametros)
                    If Not String.IsNullOrEmpty(resposta) Then
                        ListaErros.Add(String.Format(Traduzir("027_msg_campo"), campo) & resposta)
                    End If
                End If
            Else
                ListaErros.Add(resposta)
            End If
        Loop

        Return ListaErros

    End Function

    ''' <summary>
    ''' Valida os grupo de parametros no nome/extensão.
    ''' </summary>
    ''' <param name="limitadorAnterior">Limitador anterior.</param>
    ''' <param name="limitador">Limitador sem as chaves</param>
    ''' <param name="nomeExtensao">Nome/Extensão a ser validada.</param>
    ''' <param name="limitadorAbertura">Chave de abertura.</param>
    ''' <param name="campo">Descrição do campo que está sendo validado.</param>
    ''' <returns>Retorna uma mensagem de erro quando ocorrer.</returns>
    ''' <remarks></remarks>
    Private Function ValidarLimitador(limitadorAnterior As String, ByRef limitador As String, ByRef nomeExtensao As String, limitadorAbertura As String, campo As String) As String
        Dim limitadorFechamento As String
        Dim indexFechamento As Integer = 0
        Dim retorno As String = String.Empty
        Select Case limitadorAbertura
            Case Constantes.limitador_data_abertura
                limitadorFechamento = Constantes.limitador_data_fechamento
                ' procura pelo parametro de fechamento
                indexFechamento = nomeExtensao.IndexOf(limitadorFechamento)
                If indexFechamento = -1 Then
                    retorno = String.Format(Traduzir("027_msg_tag_fechamento"), campo, nomeExtensao.Trim)
                    nomeExtensao = String.Empty
                Else
                    limitador = nomeExtensao.Substring(1, indexFechamento - 1).Trim

                    If String.IsNullOrEmpty(limitador) Then
                        retorno = String.Format(Traduzir("027_msg_campo_vazio"), campo, nomeExtensao.Substring(0, indexFechamento + 1).Trim)
                        nomeExtensao = String.Empty
                    Else
                        nomeExtensao = nomeExtensao.Substring(indexFechamento + 1).Trim
                    End If
                End If

            Case Constantes.limitador_parametro_abertura
                limitadorFechamento = Constantes.limitador_parametro_fechamento
                ' procura pelo parametro de fechamento
                indexFechamento = nomeExtensao.IndexOf(limitadorFechamento, 1)
                If indexFechamento = -1 Then
                    retorno = String.Format(Traduzir("027_msg_tag_fechamento"), campo, nomeExtensao.Trim)
                    nomeExtensao = String.Empty
                Else
                    limitador = nomeExtensao.Substring(1, indexFechamento - 1).Trim
                    If String.IsNullOrEmpty(limitador) Then
                        retorno = String.Format(Traduzir("027_msg_campo_vazio"), campo, nomeExtensao.Substring(0, indexFechamento + 1).Trim)
                        nomeExtensao = String.Empty
                    Else
                        nomeExtensao = nomeExtensao.Substring(indexFechamento + 1).Trim
                    End If
                End If

            Case Constantes.limitador_quantidade_abertura
                limitadorFechamento = Constantes.limitador_quantidade_fechamento
                ' procura pelo parametro de fechamento
                indexFechamento = nomeExtensao.IndexOf(limitadorFechamento, 1)
                If indexFechamento = -1 Then

                    retorno = String.Format(Traduzir("027_msg_tag_fechamento"), campo, nomeExtensao.Trim)
                    nomeExtensao = String.Empty
                Else
                    If String.IsNullOrEmpty(limitadorAnterior) Then
                        retorno = String.Format(Traduzir("027_msg_falta_parametro_anterior"), campo, nomeExtensao.Trim)
                        nomeExtensao = String.Empty
                    Else
                        limitador = nomeExtensao.Substring(1, indexFechamento - 1).Trim
                        If String.IsNullOrEmpty(limitador) Then
                            retorno = String.Format(Traduzir("027_msg_campo_vazio"), campo, nomeExtensao.Substring(0, indexFechamento + 1).Trim)
                            nomeExtensao = String.Empty
                        Else
                            nomeExtensao = nomeExtensao.Substring(indexFechamento + 1).Trim
                        End If
                    End If
                End If

            Case Constantes.limitador_fixo_abertura
                limitadorFechamento = Constantes.limitador_fixo_fechamento
                ' procura pelo parametro de fechamento
                indexFechamento = nomeExtensao.IndexOf(limitadorFechamento)
                If indexFechamento = -1 Then
                    retorno = String.Format(Traduzir("027_msg_tag_fechamento"), campo, nomeExtensao.Trim)
                    nomeExtensao = String.Empty
                Else
                    limitador = nomeExtensao.Substring(1, indexFechamento - 1).Trim
                    If String.IsNullOrEmpty(limitador) Then
                        retorno = String.Format(Traduzir("027_msg_campo_vazio"), campo, nomeExtensao.Substring(0, indexFechamento + 1).Trim)
                        nomeExtensao = String.Empty
                    Else
                        nomeExtensao = nomeExtensao.Substring(indexFechamento + 1).Trim
                    End If
                End If

            Case Constantes.limitador_depara_abertura
                limitadorFechamento = Constantes.limitador_depara_fechamento
                ' procura pelo parametro de fechamento
                indexFechamento = nomeExtensao.IndexOf(limitadorFechamento)
                If indexFechamento = -1 Then
                    retorno = String.Format(Traduzir("027_msg_tag_fechamento"), campo, nomeExtensao.Trim)
                    nomeExtensao = String.Empty
                Else
                    limitador = nomeExtensao.Substring(1, indexFechamento - 1).Trim
                    If String.IsNullOrEmpty(limitador) Then
                        retorno = String.Format(Traduzir("027_msg_campo_vazio"), campo, nomeExtensao.Substring(0, indexFechamento + 1).Trim)
                        nomeExtensao = String.Empty
                    Else
                        If limitador.IndexOf("=") <= 0 Then
                            retorno = String.Format(Traduzir("027_msg_falta_igual"), campo, nomeExtensao.Trim)
                            nomeExtensao = String.Empty
                        Else
                            nomeExtensao = nomeExtensao.Substring(indexFechamento + 1).Trim
                            limitador = limitador.Replace("=", String.Empty)
                        End If
                    End If
                End If

            Case Constantes.limitador_substituicao_abertura
                'layout de substitutição parametro_anterior^[c]R^    
                limitadorFechamento = Constantes.limitador_substituicao_fechamento
                ' procura pelo parametro de fechamento
                indexFechamento = nomeExtensao.IndexOf(limitadorFechamento, 1)

                If indexFechamento = -1 Then
                    retorno = String.Format(Traduzir("027_msg_tag_fechamento"), campo, nomeExtensao.Trim)
                    nomeExtensao = String.Empty
                ElseIf String.IsNullOrEmpty(limitadorAnterior) Then
                    retorno = String.Format(Traduzir("027_msg_falta_parametro_anterior"), campo, nomeExtensao.Trim)
                    nomeExtensao = String.Empty
                Else
                    limitador = nomeExtensao.Substring(1, indexFechamento - 1).Trim
                    If String.IsNullOrEmpty(limitador) Then
                        retorno = String.Format(Traduzir("027_msg_campo_vazio"), campo, nomeExtensao.Substring(0, indexFechamento + 1).Trim)
                        nomeExtensao = String.Empty
                    Else
                        retorno = Me.validarParametroSubstituicao(limitador, campo)

                        ' se não deu erro
                        If String.IsNullOrEmpty(retorno) Then
                            nomeExtensao = nomeExtensao.Substring(indexFechamento + 1).Trim
                        Else
                            nomeExtensao = String.Empty
                        End If
                    End If
                End If
            Case Else
                retorno = String.Format(Traduzir("027_msg_tag_invalida"), campo, limitadorAbertura)
                nomeExtensao = String.Empty
        End Select

        Return retorno

    End Function

    ''' <summary>
    ''' Validar o limitador com os parametros do relatório.
    ''' </summary>
    ''' <param name="limitador">Limitador sem chaves.</param>
    ''' <param name="limitadorAbertura">Chave de abertura do limitador.</param>
    ''' <param name="parametros">Parametros do relatório.</param>
    ''' <returns>Retorma uma mensagem de erro quando ocorrer.</returns>
    ''' <remarks></remarks>
    Private Function ValidarLimitadorParametro(limitador As String, limitadorAbertura As String, parametros As List(Of RS2010.ItemParameter)) As String
        Dim retorno As String = String.Empty
        ' para não dar erro de index.
        limitador = limitador.ToUpper & "                             "

        Select Case limitadorAbertura
            Case Constantes.limitador_data_abertura
                Dim prefixoData As String = limitador.Substring(0, 3).Trim
                Select Case prefixoData
                    Case Constantes.prefixo_data_atual, Constantes.prefixo_data_conteo, Constantes.prefixo_data_transporte
                        'verifica se o valor do formato está correto.
                        Dim formato As String = limitador.Substring(3).Trim
                        If Not (Constantes.prefixo_data_aaaa = formato OrElse Constantes.prefixo_data_aa = formato _
                                OrElse Constantes.prefixo_data_dd = formato OrElse Constantes.prefixo_data_mm = formato _
                                OrElse Constantes.prefixo_hora_minuto = formato OrElse Constantes.prefixo_hora_minuto1 = formato OrElse Constantes.prefixo_hora_minuto2 = formato _
                                OrElse Constantes.prefixo_data_ddmmaa = formato OrElse Constantes.prefixo_data_ddmmaaaa = formato OrElse Constantes.prefixo_data_l = formato) Then
                            retorno = String.Format(Traduzir("029_msg_formato_data_invalido"), formato.Trim)
                        Else
                            'verifica se o parametro 
                            If Constantes.prefixo_data_conteo = prefixoData Then
                                Dim arraDatas(3) As String
                                arraDatas(0) = "P_FYH_CONTEO_DESDE"
                                arraDatas(1) = "P_FEC_CONTEO_DESDE"
                                arraDatas(2) = "P_FYH_CONTEO_HASTA"
                                arraDatas(3) = "P_FEC_CONTEO_HASTA"
                                If Not parametros.Exists(Function(p) arraDatas.Contains(p.Name)) Then
                                    retorno = Traduzir("029_msg_parametro_fecha_no_existe")
                                End If
                            ElseIf Constantes.prefixo_data_transporte = prefixoData Then
                                Dim arraDatas(3) As String
                                arraDatas(0) = "P_FYH_TRANSPORTE_DESDE"
                                arraDatas(1) = "P_FEC_TRANSPORTE_DESDE"
                                arraDatas(2) = "P_FYH_TRANSPORTE_HASTA"
                                arraDatas(3) = "P_FEC_TRANSPORTE_HASTA"
                                If Not parametros.Exists(Function(p) arraDatas.Contains(p.Name)) Then
                                    retorno = Traduzir("029_msg_parametro_fecha_no_existe")
                                End If
                            End If
                        End If
                    Case Else
                        retorno = String.Format(Traduzir("029_msg_prefixo_fecha_invalido"), prefixoData.Trim)
                End Select

            Case Constantes.limitador_parametro_abertura
                Dim prefixo As String = limitador.Trim
                Select Case prefixo
                    Case Constantes.prefixo_codigo_ajeno_cliente,
                        Constantes.prefixo_codigo_cliente
                        'Verifica se o parametro de cliente existe no relatório.
                        If Not parametros.Exists(Function(p) p.Name = "P_COM_CLIENTE") Then
                            retorno = String.Format(Traduzir("029_msg_parametro_no_existe"), Constantes.CONST_P_CODIGO_CLIENTE)
                        End If

                    Case Constantes.prefixo_codigo_ajeno_subcliente,
                        Constantes.prefixo_codigo_subcliente
                        'Verifica se o parametro de subcliente existe no relatório.
                        If Not parametros.Exists(Function(p) p.Name = "P_COM_SUBCLIENTE") Then
                            retorno = String.Format(Traduzir("029_msg_parametro_no_existe"), Constantes.CONST_P_CODIGO_SUBCLIENTE)
                        End If

                    Case Constantes.prefixo_codigo_ajeno_pontoServico,
                        Constantes.prefixo_codigo_pontoServico
                        'Verifica se o parametro de pontoServico existe no relatório.
                        If Not parametros.Exists(Function(p) p.Name = "P_COM_PUNTOSERVICIO") Then
                            retorno = String.Format(Traduzir("029_msg_parametro_no_existe"), Constantes.CONST_P_CODIGO_PUNTO_SERVICIO)
                        End If

                    Case Constantes.prefixo_codigo_delegacao

                        'Verifica se o parametro de pontoServico existe no relatório.
                        If Not (parametros.Exists(Function(p) p.Name = "P_COM_DELEGACION") OrElse parametros.Exists(Function(p) p.Name = "P_COM_DELEGACION_USUARIO")) Then
                            retorno = String.Format(Traduzir("029_msg_parametro_no_existe"), Constantes.CONST_P_CODIGO_DELEGACION)
                        End If

                    Case Constantes.prefixo_id_reporte,
                        Constantes.prefixo_nome_reporte
                        'OK, não precisa validar.

                    Case Else
                        If parametros Is Nothing OrElse parametros.Count = 0 OrElse
                           Not (parametros.Exists(Function(p) p.Name = prefixo.ToUpper)) Then
                            retorno = String.Format(Traduzir("029_msg_parametro_no_existe"), prefixo.Trim)
                        End If
                End Select

            Case Constantes.limitador_quantidade_abertura
                If Not (limitador.Substring(0, 1).Trim = Constantes.prefixo_quantidade_direita OrElse limitador.Substring(0, 1).Trim = Constantes.prefixo_quantidade_esquerda) Then
                    retorno = String.Format(Traduzir("029_msg_valor_parametro_cantidad"), limitador.Substring(0, 1).Trim)
                Else
                    ' verfica se o valor é númerico
                    If Not IsNumeric(limitador.Substring(1).Trim) Then
                        retorno = String.Format(Traduzir("029_msg_valor_parametro_cantidad_numerico"), limitador.Trim)
                    Else
                        'verifica se não digitou um valor muito grande
                        Try
                            Convert.ToInt32(limitador.Substring(1).Trim)
                        Catch ex As Exception
                            retorno = Traduzir("029_msg_valor_parametro_cantidad_max")
                        End Try

                    End If
                End If

            Case Constantes.limitador_fixo_abertura
            Case Constantes.limitador_depara_abertura
        End Select

        Return retorno

    End Function

    Private Function validarParametroSubstituicao(nomeExtensao As String, campo As String) As String
        Dim retorno As String = String.Empty
        Dim limitadorAbertura As String
        Dim limitadorFechamento As String
        Dim indexFechamento As Integer

        If nomeExtensao.Length > 0 Then
            'Verifica se o valor fixo foi informado
            limitadorAbertura = nomeExtensao.Substring(0, 1)
            If limitadorAbertura <> Constantes.limitador_fixo_abertura Then
                retorno = String.Format(Traduzir("027_msg_tag_invalida"), campo, limitadorAbertura)
            Else
                'Verifica se a tag de fechamento foi informada
                limitadorFechamento = Constantes.limitador_fixo_fechamento
                indexFechamento = nomeExtensao.IndexOf(limitadorFechamento)
                If indexFechamento = -1 Then
                    retorno = String.Format(Traduzir("027_msg_tag_fechamento"), campo, nomeExtensao.Trim)
                Else
                    Dim valorProcura = nomeExtensao.Substring(1, indexFechamento - 1).Trim
                    If String.IsNullOrEmpty(valorProcura) Then
                        retorno = String.Format(Traduzir("027_msg_campo_vazio"), campo, nomeExtensao.Substring(0, indexFechamento + 1).Trim)
                    Else
                        'Verifica se o parametro para substuir na esquerda ou direita foi informado
                        Dim direita_esquerda = nomeExtensao.Substring(indexFechamento + 1).Trim
                        If direita_esquerda <> Constantes.prefixo_quantidade_direita AndAlso direita_esquerda <> Constantes.prefixo_quantidade_esquerda Then
                            retorno = String.Format(Traduzir("027_msg_tag_substituicao"), campo, limitadorAbertura)
                        End If
                    End If
                End If
            End If
        End If

        Return retorno
    End Function

    ''' <summary>
    ''' Gera o nome do arquivo ou a extensão.
    ''' </summary>
    ''' <param name="PathRelatorio">Caminho do relatório.</param>
    ''' <param name="NomeRelatorio">Nome do relatório.</param>
    ''' <param name="IDRelatorio">ID do relatório.</param>
    ''' <param name="nomeExtensao">Nome/Extensão a ser gerada.</param>
    ''' <param name="parametros">Paramtros do relatório com os valores preenchidos pelo usuário.</param>
    ''' <param name="erros">Lista de erros quando ocorrer.</param>
    ''' <returns>Retorna o nome ou a extensão gerada.</returns>
    ''' <remarks></remarks>
    Public Function GerarNomeExtensao(PathRelatorio As String, NomeRelatorio As String, IDRelatorio As String, nomeExtensao As String,
                                      parametros As List(Of RSE.ParameterValue), ByRef erros As List(Of String)) As String
        Dim limitador As String = String.Empty
        Dim limitadorAbertura As String
        Dim limitadorFechamento As String
        Dim indexFechamento As Integer = 0
        Dim espacos As String = "                             "
        limitadorAbertura = nomeExtensao.Substring(0, 1)
        Dim ListaErros As New List(Of String)
        Dim nomeGerado As String = String.Empty
        Dim retorno As String = String.Empty
        Dim UltimoNombreGenerado As String = String.Empty

        Do While nomeExtensao.Length > 0
            limitadorAbertura = nomeExtensao.Substring(0, 1)
            Select Case limitadorAbertura
                Case Constantes.limitador_data_abertura
                    limitadorFechamento = Constantes.limitador_data_fechamento
                    ' procura pelo parametro de fechamento
                    indexFechamento = nomeExtensao.IndexOf(limitadorFechamento)
                    If indexFechamento = -1 Then
                        ListaErros.Add(String.Format(Traduzir("029_msg_parametro_sin_cerrar"), nomeExtensao.Trim))
                        Exit Do
                    End If

                Case Constantes.limitador_parametro_abertura
                    limitadorFechamento = Constantes.limitador_parametro_fechamento
                    ' procura pelo parametro de fechamento
                    indexFechamento = nomeExtensao.IndexOf(limitadorFechamento, 1)
                    If indexFechamento = -1 Then
                        ListaErros.Add(String.Format(Traduzir("029_msg_parametro_sin_cerrar"), nomeExtensao.Trim))
                        Exit Do
                    End If

                Case Constantes.limitador_quantidade_abertura
                    limitadorFechamento = Constantes.limitador_quantidade_fechamento
                    ' procura pelo parametro de fechamento
                    indexFechamento = nomeExtensao.IndexOf(limitadorFechamento, 1)
                    If indexFechamento = -1 Then
                        ListaErros.Add(String.Format(Traduzir("029_msg_parametro_sin_cerrar"), nomeExtensao.Trim))
                        Exit Do
                    End If

                Case Constantes.limitador_fixo_abertura
                    limitadorFechamento = Constantes.limitador_fixo_fechamento
                    ' procura pelo parametro de fechamento
                    indexFechamento = nomeExtensao.IndexOf(limitadorFechamento)
                    If indexFechamento = -1 Then
                        ListaErros.Add(String.Format(Traduzir("029_msg_parametro_sin_cerrar"), nomeExtensao.Trim))
                        Exit Do
                    End If

                Case Constantes.limitador_depara_abertura
                    limitadorFechamento = Constantes.limitador_depara_fechamento
                    ' procura pelo parametro de fechamento
                    indexFechamento = nomeExtensao.IndexOf(limitadorFechamento)
                    If indexFechamento = -1 Then
                        ListaErros.Add(String.Format(Traduzir("029_msg_parametro_sin_cerrar"), nomeExtensao.Trim))
                        Exit Do
                    End If

                Case Constantes.limitador_substituicao_abertura
                    limitadorFechamento = Constantes.limitador_substituicao_fechamento
                    ' procura pelo parametro de fechamento
                    indexFechamento = nomeExtensao.IndexOf(limitadorFechamento, 1)
                    If indexFechamento = -1 Then
                        ListaErros.Add(String.Format(Traduzir("029_msg_parametro_sin_cerrar"), nomeExtensao.Trim))
                        Exit Do
                    End If
                Case Else
                    ListaErros.Add(Traduzir("029_msg_formato_invalido"))
                    Exit Do
            End Select

            limitador = nomeExtensao.Substring(1, indexFechamento - 1)
            Dim HayLimitadorTamanho As Boolean = False

            retorno = Me.GerarNomeExtensao(parametros, NomeRelatorio, IDRelatorio, limitador, limitadorAbertura, ListaErros, UltimoNombreGenerado, , HayLimitadorTamanho)

            If HayLimitadorTamanho Then
                nomeGerado = nomeGerado.Replace(UltimoNombreGenerado, String.Empty)
            End If

            UltimoNombreGenerado = retorno

            nomeGerado = nomeGerado & retorno

            nomeExtensao = nomeExtensao.Substring(indexFechamento + 1).Trim
        Loop

        erros.AddRange(ListaErros)

        Return nomeGerado

    End Function

    ''' <summary>
    ''' Gera o nome/extensão do arquivo.
    ''' </summary>
    ''' <param name="parametros">Parametros do relatório com os valores informados pelo usuário.</param>
    ''' <param name="NomeRelatorio">Nome do relatório.</param>
    ''' <param name="IDRelatorio">ID do relatório.</param>
    ''' <param name="limitador">Limitador sem chaves.</param>
    ''' <param name="limitadorAbertura">Chave de abertura.</param>
    ''' <param name="erros">Lista de erros quando ocorrer.</param>
    ''' <returns>Retorna o nome/extensão gerado.</returns>
    ''' <remarks></remarks>
    Private Function GerarNomeExtensao(parametros As List(Of RSE.ParameterValue), NomeRelatorio As String, IDRelatorio As String, limitador As String, limitadorAbertura As String, ByRef erros As List(Of String),
                                       NombreGenerado As String, Optional EsDePara As Boolean = False, Optional ByRef HayLimitadorTamano As Boolean = False) As String

        Dim valorParametro As String = String.Empty
        Dim data As New DateTime
        Dim valorAnterior As String = String.Empty
        Select Case limitadorAbertura
            Case Constantes.limitador_data_abertura
                ' procura pelo parametro de fechamento
                Dim prefixoData As String = limitador.Substring(0, 3).Trim
                Select Case prefixoData
                    Case Constantes.prefixo_data_atual, Constantes.prefixo_data_conteo, Constantes.prefixo_data_transporte

                        ' Verifica se o valor do formato está correto.
                        Dim formato As String = limitador.Substring(3).Trim.ToUpper
                        If Not (Constantes.prefixo_data_aaaa = formato OrElse Constantes.prefixo_data_aa = formato _
                                OrElse Constantes.prefixo_data_dd = formato OrElse Constantes.prefixo_data_mm = formato _
                                OrElse Constantes.prefixo_hora_minuto = formato OrElse Constantes.prefixo_hora_minuto1 = formato OrElse Constantes.prefixo_hora_minuto2 = formato _
                                OrElse Constantes.prefixo_data_ddmmaa = formato OrElse Constantes.prefixo_data_ddmmaaaa = formato OrElse Constantes.prefixo_data_l = formato) Then
                            erros.Add(String.Format(Traduzir("029_msg_formato_data_invalido"), formato.Trim))
                        Else
                            formato = formato.ToLower

                            ' Verifica se o formato possui HH hora,
                            If formato.Contains("hh") Then
                                formato = formato.Replace("hh", "HH")
                            Else
                                formato = formato.Replace("a", "y")
                                formato = formato.Replace("mm", "MM")
                            End If

                            '' Verifica se o parametro 
                            If Constantes.prefixo_data_conteo = prefixoData Then
                                '' Verifica se o parametro P_FYH_CONTEO_HASTA foi preenchido
                                valorParametro = Me.GetValorParametro(parametros, Constantes.CONST_P_FECHA_CONTEO_HASTA)
                                If (String.IsNullOrEmpty(valorParametro)) Then
                                    '' Verifica se o parametro P_FYH_CONTEO_DESDE foi preenchido
                                    valorParametro = Me.GetValorParametro(parametros, Constantes.CONST_P_FECHA_CONTEO_DESDE)
                                    If (String.IsNullOrEmpty(valorParametro)) Then
                                        erros.Add(Traduzir("029_msg_fecha_conteo_sin_valor"))
                                    Else
                                        '' Sendo o parametro preenchido tratar o mesmo
                                        If DateTime.TryParse(valorParametro, data) Then

                                            If formato.ToUpper.Equals(Constantes.prefixo_data_l) Then
                                                valorParametro = RetornaLetraDoMes(data.ToString(Constantes.prefixo_data_mm))
                                            Else
                                                valorParametro = data.ToString(formato)
                                            End If

                                        Else
                                            erros.Add(String.Format(Traduzir("029_msg_fecha_conteo_invalida"), data))
                                        End If
                                    End If
                                Else
                                    '' Sendo o parametro preenchido tratar o mesmo
                                    If DateTime.TryParse(valorParametro, data) Then

                                        If formato.ToUpper.Equals(Constantes.prefixo_data_l) Then
                                            valorParametro = RetornaLetraDoMes(data.ToString(Constantes.prefixo_data_mm))
                                        Else
                                            valorParametro = data.ToString(formato)
                                        End If

                                    Else
                                        erros.Add(String.Format(Traduzir("029_msg_fecha_conteo_invalida"), data))
                                    End If
                                End If

                            ElseIf Constantes.prefixo_data_transporte = prefixoData Then
                                '' Verifica se o parametro P_FYH_TRANSPORTE_HASTA foi preenchido
                                valorParametro = Me.GetValorParametro(parametros, Constantes.CONST_P_FECHA_TRANSPORTE_HASTA)
                                If (String.IsNullOrEmpty(valorParametro)) Then
                                    '' Verifica se o parametro P_FYH_TRANSPORTE_DESDE foi preenchido
                                    valorParametro = Me.GetValorParametro(parametros, Constantes.CONST_P_FECHA_TRANSPORTE_DESDE)
                                    If (String.IsNullOrEmpty(valorParametro)) Then
                                        erros.Add(Traduzir("029_msg_fecha_transporte_sin_valor"))
                                    Else
                                        '' Sendo o parametro preenchido tratar o mesmo
                                        If DateTime.TryParse(valorParametro, data) Then

                                            If formato.ToUpper.Equals(Constantes.prefixo_data_l) Then
                                                valorParametro = RetornaLetraDoMes(data.ToString(Constantes.prefixo_data_mm))
                                            Else
                                                valorParametro = data.ToString(formato)
                                            End If

                                        Else
                                            erros.Add(String.Format(Traduzir("029_msg_fecha_transporte_invalida"), data))
                                        End If
                                    End If
                                Else
                                    '' Sendo o parametro preenchido tratar o mesmo
                                    If DateTime.TryParse(valorParametro, data) Then

                                        If formato.ToUpper.Equals(Constantes.prefixo_data_l) Then
                                            valorParametro = RetornaLetraDoMes(data.ToString(Constantes.prefixo_data_mm))
                                        Else
                                            valorParametro = data.ToString(formato)
                                        End If

                                    Else
                                        erros.Add(String.Format(Traduzir("029_msg_fecha_transporte_invalida"), data))
                                    End If
                                End If

                            ElseIf Constantes.prefixo_data_atual = prefixoData Then

                                If formato.ToUpper.Equals(Constantes.prefixo_data_l) Then
                                    valorParametro = RetornaLetraDoMes(DateTime.Now.ToString(Constantes.prefixo_data_mm))
                                Else
                                    valorParametro = DateTime.Now.ToString(formato)
                                End If

                            End If
                        End If
                    Case Else
                        erros.Add(String.Format(Traduzir("029_msg_prefixo_fecha_invalido"), prefixoData.Trim))
                End Select

            Case Constantes.limitador_parametro_abertura
                Dim prefixo As String = limitador.Trim
                Dim CodigoParametro As String = String.Empty

                Select Case prefixo
                    Case Constantes.prefixo_codigo_ajeno_cliente,
                        Constantes.prefixo_codigo_cliente

                        'Verifica se o parametro de cliente existe no relatório.
                        If Not parametros.Exists(Function(p) p.Name = Constantes.CONST_P_CODIGO_CLIENTE) Then
                            erros.Add(String.Format(Traduzir("029_msg_parametro_no_existe"), Constantes.CONST_P_CODIGO_CLIENTE))
                        Else

                            If prefixo = Constantes.prefixo_codigo_ajeno_cliente Then
                                valorParametro = RetornarCodigoAjeno(Constantes.prefixo_codigo_ajeno_cliente, parametros, erros)
                            Else
                                valorParametro = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_CLIENTE)
                            End If

                            If (String.IsNullOrEmpty(valorParametro)) Then
                                erros.Add(String.Format(Traduzir("029_msg_valor_no_informado"), Constantes.CONST_P_CODIGO_CLIENTE))
                            End If
                            CodigoParametro = Constantes.CONST_P_CODIGO_CLIENTE
                        End If

                    Case Constantes.prefixo_codigo_ajeno_subcliente,
                            Constantes.prefixo_codigo_subcliente

                        'Verifica se o parametro de subcliente existe no relatório.
                        If Not parametros.Exists(Function(p) p.Name = Constantes.CONST_P_CODIGO_SUBCLIENTE) Then
                            erros.Add(String.Format(Traduzir("029_msg_parametro_no_existe"), Constantes.CONST_P_CODIGO_SUBCLIENTE))
                        Else

                            If prefixo = Constantes.prefixo_codigo_ajeno_subcliente Then
                                valorParametro = RetornarCodigoAjeno(Constantes.prefixo_codigo_ajeno_subcliente, parametros, erros)
                            Else
                                valorParametro = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_SUBCLIENTE)
                            End If

                            If (String.IsNullOrEmpty(valorParametro)) Then
                                erros.Add(String.Format(Traduzir("029_msg_valor_no_informado"), Constantes.CONST_P_CODIGO_SUBCLIENTE))
                            End If
                            CodigoParametro = Constantes.CONST_P_CODIGO_SUBCLIENTE
                        End If

                    Case Constantes.prefixo_codigo_ajeno_pontoServico,
                            Constantes.prefixo_codigo_pontoServico
                        'Verifica se o parametro de pontoServico existe no relatório.
                        If Not parametros.Exists(Function(p) p.Name = Constantes.CONST_P_CODIGO_PUNTO_SERVICIO) Then
                            erros.Add(String.Format(Traduzir("029_msg_parametro_no_existe"), Constantes.CONST_P_CODIGO_PUNTO_SERVICIO))
                        Else

                            If prefixo = Constantes.prefixo_codigo_ajeno_pontoServico Then
                                valorParametro = RetornarCodigoAjeno(Constantes.prefixo_codigo_ajeno_pontoServico, parametros, erros)
                            Else
                                valorParametro = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_PUNTO_SERVICIO)
                            End If

                            If (String.IsNullOrEmpty(valorParametro)) Then
                                erros.Add(String.Format(Traduzir("029_msg_valor_no_informado"), Constantes.CONST_P_CODIGO_PUNTO_SERVICIO))
                            End If
                            CodigoParametro = Constantes.CONST_P_CODIGO_PUNTO_SERVICIO
                        End If

                    Case Constantes.prefixo_codigo_delegacao

                        'Verifica se o parametro de pontoServico existe no relatório.
                        If Not (parametros.Exists(Function(p) p.Name = Constantes.CONST_P_CODIGO_DELEGACION) OrElse parametros.Exists(Function(p) p.Name = Constantes.CONST_P_CODIGO_DELEGACION_USUARIO)) Then
                            erros.Add(String.Format(Traduzir("029_msg_parametro_no_existe"), Constantes.CONST_P_CODIGO_DELEGACION))
                        Else
                            valorParametro = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_DELEGACION)
                            CodigoParametro = Constantes.CONST_P_CODIGO_DELEGACION

                            If (String.IsNullOrEmpty(valorParametro)) Then
                                valorParametro = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_DELEGACION_USUARIO)
                                CodigoParametro = Constantes.CONST_P_CODIGO_DELEGACION_USUARIO
                                If (String.IsNullOrEmpty(valorParametro)) Then
                                    erros.Add(String.Format(Traduzir("029_msg_valor_no_informado"), Constantes.CONST_P_CODIGO_DELEGACION & "/" & Constantes.CONST_P_CODIGO_DELEGACION_USUARIO))
                                End If
                            End If

                        End If

                    Case Constantes.prefixo_id_reporte
                        valorParametro = IDRelatorio

                    Case Constantes.prefixo_nome_reporte
                        valorParametro = NomeRelatorio

                    Case Else

                        Dim objParametro As RSE.ParameterValue = (From p In parametros Where p.Name = prefixo.Trim.ToUpper).FirstOrDefault

                        If objParametro IsNot Nothing Then
                            CodigoParametro = prefixo.Trim.ToUpper

                            'Verifica se a descrição foi informada,
                            If Not String.IsNullOrEmpty(objParametro.Label) Then
                                valorParametro = objParametro.Label
                            ElseIf Not String.IsNullOrEmpty(objParametro.Value) Then
                                valorParametro = objParametro.Value
                            Else
                                erros.Add(String.Format(Traduzir("029_msg_valor_no_informado"), prefixo.Trim))
                            End If
                        Else
                            erros.Add(String.Format(Traduzir("029_msg_parametro_no_existe"), prefixo.Trim))
                        End If

                End Select

                If EsDePara AndAlso Not String.IsNullOrEmpty(valorParametro) Then
                    valorParametro = RetornarValorDepara(parametros, CodigoParametro, valorParametro)
                End If

            Case Constantes.limitador_quantidade_abertura

                Dim PrefixoQuantidade As String = limitador.Substring(0, 1)

                limitador = limitador.Replace(PrefixoQuantidade, String.Empty)

                Dim QuantidadeCaracteresRemove As Int32 = 0

                If IsNumeric(limitador) Then

                    QuantidadeCaracteresRemove = limitador

                    If QuantidadeCaracteresRemove > NombreGenerado.Length Then
                        QuantidadeCaracteresRemove = NombreGenerado.Length
                    End If

                    If PrefixoQuantidade = Constantes.prefixo_quantidade_direita Then
                        valorParametro = NombreGenerado.Substring(0, NombreGenerado.Length - QuantidadeCaracteresRemove)
                    Else
                        valorParametro = NombreGenerado.Substring(QuantidadeCaracteresRemove)
                    End If

                    HayLimitadorTamano = True

                Else
                    erros.Add(String.Format(Traduzir("029_msg_parametro_no_existe"), limitador.Trim))
                End If

            Case Constantes.limitador_fixo_abertura
                ' procura pelo parametro de fechamento
                valorParametro = limitador

            Case Constantes.limitador_depara_abertura

                limitador = Replace(limitador, "@DE_PARA@=", String.Empty)

                Dim LimitadorAberturaDePara As String = limitador.Substring(0, 1)

                limitador = Replace(limitador, LimitadorAberturaDePara, String.Empty)

                valorParametro = Me.GerarNomeExtensao(parametros, NomeRelatorio, IDRelatorio, limitador, LimitadorAberturaDePara, erros, NombreGenerado, True)

            Case Constantes.limitador_substituicao_abertura
                'Verifica se existe um valor anterior para retirar o valor desse campo
                If String.IsNullOrEmpty(NombreGenerado) Then
                    erros.Add(String.Format(Traduzir("027_msg_falta_parametro_anterior"), Traduzir("027_lbl_NombreArchivo") & "/" & Traduzir("027_lbl_ExtensionArchivo"), limitador.Trim))
                Else
                    valorParametro = RetornarValorSubstituicao(NombreGenerado, limitador.Replace(Constantes.limitador_substituicao_abertura, String.Empty))
                    HayLimitadorTamano = True
                End If
        End Select

        Return valorParametro

    End Function

    ''' <summary>
    ''' Retornar valor de para
    ''' </summary>
    ''' <param name="ObjParametros"></param>
    ''' <param name="CodigoValor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornarValorDepara(ObjParametros As List(Of RSE.ParameterValue), CodParametroVariavel As String, CodigoValor As String) As String

        Dim ValorRetorno As String = String.Empty

        If ObjParametros IsNot Nothing AndAlso ObjParametros.Count > 0 Then

            Dim LabelParametro = (From param In ObjParametros Where param.Name = CodParametroVariavel AndAlso param.Value = CodigoValor Select param.Label).FirstOrDefault

            ValorRetorno = (From param In ObjParametros Where param.Name = Constantes.CONST_P_DE_PARA AndAlso param.Label = LabelParametro Select param.Value).FirstOrDefault

        End If

        Return ValorRetorno

    End Function

    ''' <summary>
    ''' Retornar valor substituicao
    ''' </summary>
    ''' <param name="valorParametroAnterior"></param>
    ''' <param name="limitador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornarValorSubstituicao(valorParametroAnterior As String, limitador As String) As String

        Dim retorno As String = valorParametroAnterior
        Dim limitadorAbertura As String
        Dim limitadorFechamento As String
        Dim indexFechamento As Integer
        Dim indexProcura As Integer

        If limitador.Length > 0 Then
            'Verifica se o valor fixo foi informado
            limitadorAbertura = limitador.Substring(0, 1)
            If limitadorAbertura = Constantes.limitador_fixo_abertura Then
                'Verifica se a tag de fechamento foi informada
                limitadorFechamento = Constantes.limitador_fixo_fechamento
                indexFechamento = limitador.IndexOf(limitadorFechamento)

                If indexFechamento > -1 Then
                    Dim valorProcura = limitador.Substring(1, indexFechamento - 1).Trim
                    If Not String.IsNullOrEmpty(valorProcura) Then
                        'Verifica se o parametro para substuir na esquerda ou direita foi informado
                        Dim direita_esquerda = limitador.Substring(indexFechamento + 1).Trim
                        If direita_esquerda = Constantes.prefixo_quantidade_direita Then
                            indexProcura = valorParametroAnterior.IndexOf(valorProcura)
                            If indexProcura > -1 Then
                                retorno = valorParametroAnterior.Substring(indexProcura + valorProcura.Length)
                            End If
                        ElseIf direita_esquerda = Constantes.prefixo_quantidade_esquerda Then
                            indexProcura = valorParametroAnterior.IndexOf(valorProcura)
                            If indexProcura > -1 Then
                                retorno = valorParametroAnterior.Substring(0, indexProcura)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        Return retorno
    End Function

    ''' <summary>
    ''' Retorna o codigo ajeno
    ''' </summary>
    ''' <param name="CodigoParametro"></param>
    ''' <param name="parametros"></param>
    ''' <param name="erros"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornarCodigoAjeno(CodigoParametro As String, parametros As List(Of RSE.ParameterValue), ByRef erros As List(Of String)) As String

        Dim objPeticion As New ContractoServicio.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.Peticion
        Dim objRespuesta As ContractoServicio.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.Respuesta = Nothing
        Dim codigo As String = String.Empty
        objPeticion.ValorPadron = True

        Select Case CodigoParametro

            Case Constantes.prefixo_codigo_ajeno_cliente

                objPeticion.CodigoCliente = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_CLIENTE)

                If String.IsNullOrEmpty(objPeticion.CodigoCliente) Then
                    Return String.Empty
                End If

            Case Constantes.prefixo_codigo_ajeno_subcliente

                objPeticion.CodigoCliente = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_CLIENTE)
                objPeticion.CodigoSubCliente = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_SUBCLIENTE)

                If String.IsNullOrEmpty(objPeticion.CodigoCliente) AndAlso String.IsNullOrEmpty(objPeticion.CodigoSubCliente) Then
                    Return String.Empty
                End If

            Case Constantes.prefixo_codigo_ajeno_pontoServico

                objPeticion.CodigoCliente = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_CLIENTE)
                objPeticion.CodigoSubCliente = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_SUBCLIENTE)
                objPeticion.CodigoPuntoServicio = Me.GetValorParametro(parametros, Constantes.CONST_P_CODIGO_PUNTO_SERVICIO)

                If String.IsNullOrEmpty(objPeticion.CodigoCliente) AndAlso String.IsNullOrEmpty(objPeticion.CodigoSubCliente) AndAlso String.IsNullOrEmpty(objPeticion.CodigoPuntoServicio) Then
                    Return String.Empty
                End If
        End Select

        Dim objProxy As New Comunicacion.ProxyCodigoAjeno

        objRespuesta = objProxy.GetAjenoByClienteSubClientePuntoServicio(objPeticion)

        If objRespuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            erros.Add(objRespuesta.MensajeError)
        Else
            codigo = If(objRespuesta.Ajenos IsNot Nothing AndAlso objRespuesta.Ajenos.Count > 0, objRespuesta.Ajenos.First.CodAjeno, String.Empty)
        End If

        Return codigo

    End Function

    Private Function GetValorParametro(parametros As List(Of RSE.ParameterValue), nomeParametro As String) As String
        Dim valor As String = String.Empty
        Dim param = parametros.FirstOrDefault(Function(p) p.Name = nomeParametro)
        If Not param Is Nothing Then
            If Not param.Value Is Nothing Then
                valor = param.Value
            End If
        End If

        Return valor

    End Function

    Private Function RetornaLetraDoMes(mes As String) As String

        Dim letraMes As String = String.Empty

        Select Case mes

            Case Constantes.numero_mes_janeiro
                letraMes = Constantes.letra_mes_janeiro
            Case Constantes.numero_mes_fevereiro
                letraMes = Constantes.letra_mes_fevereiro
            Case Constantes.numero_mes_marco
                letraMes = Constantes.letra_mes_marco
            Case Constantes.numero_mes_abril
                letraMes = Constantes.letra_mes_abril
            Case Constantes.numero_mes_maio
                letraMes = Constantes.letra_mes_maio
            Case Constantes.numero_mes_junho
                letraMes = Constantes.letra_mes_junho
            Case Constantes.numero_mes_julho
                letraMes = Constantes.letra_mes_julho
            Case Constantes.numero_mes_agosto
                letraMes = Constantes.letra_mes_agosto
            Case Constantes.numero_mes_setembro
                letraMes = Constantes.letra_mes_setembro
            Case Constantes.numero_mes_outubro
                letraMes = Constantes.letra_mes_outubro
            Case Constantes.numero_mes_novembro
                letraMes = Constantes.letra_mes_novembro
            Case Constantes.numero_mes_dezembro
                letraMes = Constantes.letra_mes_dezembro

        End Select

        Return letraMes

    End Function

#End Region

End Class
