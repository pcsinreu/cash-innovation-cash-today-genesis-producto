Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon
Imports System.Web
Imports System.Web.Services
Imports Ionic.Zip
Imports System.IO

Public Class ObtenerReportes1
    Implements System.Web.IHttpHandler, IRequiresSessionState

    Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim peticion As New Parametro.obtenerParametros.Peticion
        Dim respuesta As Parametro.obtenerParametros.Respuesta
        Dim nombresArchivos As List(Of String) = Nothing
        Dim codigoDelegacion As String = ""
        Dim esReportes As Boolean = False

        If Not String.IsNullOrEmpty(context.Request.QueryString("nombresArchivos")) Then
            nombresArchivos = New List(Of String)
            nombresArchivos = context.Request.QueryString("nombresArchivos").ToString().Split(";").ToList
        End If

        If Not String.IsNullOrEmpty(context.Request.QueryString("codigoDelegacion")) Then
            codigoDelegacion = context.Request.QueryString("codigoDelegacion")
        End If

        If Not String.IsNullOrEmpty(context.Request.QueryString("esReportes")) Then
            esReportes = context.Request.QueryString("esReportes")
        End If
        
        If nombresArchivos IsNot Nothing AndAlso nombresArchivos.Count > 0 AndAlso Not String.IsNullOrEmpty(codigoDelegacion) Then
            peticion.codigoAplicacion = Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS
            peticion.codigoDelegacion = codigoDelegacion
            peticion.codigosParametro = New List(Of String)
            peticion.codigosParametro.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_DELEGACION_DIRECCION_REPORTES_GENERADOS)

            'recupera o parâmetro DireccionReportesGenerados
            respuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Parametros.obtenerParametros(peticion)

            If respuesta IsNot Nothing AndAlso respuesta.parametros IsNot Nothing AndAlso respuesta.parametros.Count > 0 Then

                Dim zip As New ZipFile()
                context.Response.ContentType = "application/zip"
                context.Response.AddHeader("Content-Disposition", "filename=" & If(esReportes, "Reportes.zip", "Archivos.zip"))

                For Each nombreArchivo In nombresArchivos
                    If System.IO.File.Exists(respuesta.parametros.First.valorParametro & "\" & nombreArchivo) AndAlso Not zip.EntryFileNames.Contains(nombreArchivo) Then
                        zip.AddFile(respuesta.parametros.First.valorParametro & "\" & nombreArchivo, String.Empty)
                    End If
                Next

                zip.Save(context.Response.OutputStream)
        End If

        Else
        context.Response.ContentType = "text/plain"
        context.Response.Write("Informaciones invalidas!")
        End If

    End Sub

    Private Function ReadByteArryFromFile(destPath As String) As Byte()
        Dim buff As Byte() = Nothing
        Dim fs As FileStream = New FileStream(destPath, FileMode.Open, FileAccess.Read)
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim numBytes As Long = New FileInfo(destPath).Length
        buff = br.ReadBytes(Integer.Parse(numBytes))
        Return buff
    End Function

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class