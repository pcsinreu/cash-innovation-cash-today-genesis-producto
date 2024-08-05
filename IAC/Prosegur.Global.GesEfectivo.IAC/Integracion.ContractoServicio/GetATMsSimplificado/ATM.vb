﻿Imports System.Xml.Serialization

Namespace GetATMsSimplificado


    <XmlType(Namespace:="urn:GetATMsSimplificado")> _
    <XmlRoot(Namespace:="urn:GetATMsSimplificado")> _
    <Serializable()> _
    Public Class ATM

        Public Property OidCajero As String
        Public Property CodigoCajero As String
        Public Property CodigoCliente As String
        Public Property DescripcionCliente As String
        Public Property CodigoSubcliente As String
        Public Property DescripcionSubcliente As String
        Public Property CodigoPuntoServicio As String
        Public Property DescripcionPuntoServicio As String
        Public Property BolVigente As Boolean

    End Class

End Namespace