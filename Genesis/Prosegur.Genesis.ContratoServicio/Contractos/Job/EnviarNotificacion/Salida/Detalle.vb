﻿Imports System.Xml.Serialization

Namespace Contractos.Job.EnviarNotificacion.Salida
    Public Class Detalle
        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String
    End Class
End Namespace