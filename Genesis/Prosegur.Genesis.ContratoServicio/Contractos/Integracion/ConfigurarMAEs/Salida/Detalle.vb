﻿Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarMAEs.Salida

    <XmlType(Namespace:="urn:ConfigurarMAEs.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarMAEs.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace