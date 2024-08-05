﻿Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos

    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarTotalizadoresSaldos")> _
    <XmlRoot(Namespace:="urn:RecuperarTotalizadoresSaldos")> _
    Public Class Respuesta
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BaseRespuesta

        Public Property TotalizadoresSaldos As List(Of Clases.TotalizadorSaldo)

    End Class

End Namespace

