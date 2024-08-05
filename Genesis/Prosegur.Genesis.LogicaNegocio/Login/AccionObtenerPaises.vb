Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global
Imports Prosegur.Genesis.ContractoServicio.Login
Imports System.Xml.Serialization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports Prosegur.Global.Seguridad.ContractoServicio.ObtenerDelegaciones
Imports Prosegur.Genesis.Comunicacion

Public Class AccionObtenerPaises
    Public Shared Function Ejecutar() As ObtenerPaises.Respuesta

        Dim objProxyLogin As New ProxyWS.ProxyLogin()

        'Dim objProxyLogin As New LoginGlobal.Seguridad()

        Dim respuesta As New ObtenerPaises.Respuesta


        respuesta.Paises = LogicaNegocio.Genesis.Pais.ObtenerPaises

        Return respuesta
    End Function
End Class
