Imports System.Web
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports System.Reflection


''' <summary>
''' Classe de parametros para utilização dentro do contexto Genesis Web
''' </summary>
Public Class Parametros

    Public Shared Property Permisos As ContractoServicio.Login.CrearTokenAcceso.Permisos
        Get
            If HttpContext.Current.Session("Permisos") Is Nothing Then
                HttpContext.Current.Session("Permisos") = New ContractoServicio.Login.CrearTokenAcceso.Permisos
            End If

            Return HttpContext.Current.Session("Permisos")

        End Get
        Set(value As ContractoServicio.Login.CrearTokenAcceso.Permisos)
            HttpContext.Current.Session("Permisos") = value
        End Set
    End Property

    Public Shared Property URLServicio As String = String.Empty
    Public Shared Property URLServicioTokenLogin As String = String.Empty
    Public Shared Property URLServicioTokenSenha As String = String.Empty
    Public Shared Property CodigoAplicacion As String = String.Empty
    Public Shared Property CodigoPuesto As String = String.Empty
    Public Shared Property PathXmlError As String = String.Empty

    Public Shared Property IsBusinessNotificationEnabled As Boolean
        Get
            Return Prosegur.Genesis.Comon.BugsnagHelper.IsBusinessNotificationEnabled

        End Get
        Set(value As Boolean)
            Prosegur.Genesis.Comon.BugsnagHelper.IsBusinessNotificationEnabled = value
        End Set
    End Property



    Public Shared ReadOnly Property AgenteComunicacion() As Comunicacion.Agente
        Get
            Return Comunicacion.Agente.Instancia(URLServicio, True)
        End Get
    End Property

    Public Shared Property Parametro As Parametro

    ''' <summary>
    ''' Recupera os parametros
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira]  09/06/2013  criado
    ''' </history>
    Public Shared Sub RecuperarParametros(codigoDelegacion As String, codigoAplicacion As String)

        ' Recupera os parâmetros da aplicação
        Dim objPeticion As New GetParametrosDelegacionPais.Peticion
        Dim objRespuesta As New GetParametrosDelegacionPais.Respuesta
        Dim ProxyIac As New Comunicacion.ProxyIacIntegracion()

        objPeticion.CodigoAplicacion = codigoAplicacion
        objPeticion.CodigoDelegacion = codigoDelegacion
        objRespuesta = ProxyIac.GetParametrosDelegacionPais(objPeticion)

        _Parametro = New Parametro

        If objRespuesta.Parametros IsNot Nothing AndAlso objRespuesta.Parametros.Count > 0 Then
            RelacionaValores(objRespuesta.Parametros)
        End If

    End Sub

    ''' <summary>
    ''' Preenche o objeto de parametros com os valores recuperados do iac.
    ''' </summary>
    ''' <param name="ParametrosValor"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira]  09/06/2013  criado
    ''' </history>
    Private Shared Sub RelacionaValores(ParametrosValor As GetParametrosDelegacionPais.ParametroRespuestaColeccion)

        Try

            For Each Valor As GetParametrosDelegacionPais.ParametroRespuesta In ParametrosValor

                Dim propriedadeCodigo As String = Valor.CodigoParametro
                Dim propriedadeValor As Object
                Dim propriedade As PropertyInfo = _Parametro.GetType().GetProperties().FirstOrDefault(Function(p) p.Name.ToUpper().Equals(propriedadeCodigo.ToUpper()))

                If propriedade IsNot Nothing Then

                    If Not String.IsNullOrEmpty(Valor.ValorParametro) Then

                        propriedadeValor = Convert.ChangeType(If(TypeOf propriedade.GetValue(_Parametro, Nothing) Is Boolean AndAlso IsNumeric(Valor.ValorParametro), Convert.ToInt32(Valor.ValorParametro), Valor.ValorParametro), propriedade.PropertyType)

                        propriedade.SetValue(_Parametro, propriedadeValor, Nothing)

                    End If

                End If

            Next

        Catch ex As Exception

            ' TODO: adicionar mensagem sobre atribuição de valores na classe de parametros
            Throw

        End Try

    End Sub

End Class
