﻿Imports System.Xml.Serialization

Namespace Contractos.Infraestructura.RecuperarDatosEntradaMovimientos.Salida

    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace