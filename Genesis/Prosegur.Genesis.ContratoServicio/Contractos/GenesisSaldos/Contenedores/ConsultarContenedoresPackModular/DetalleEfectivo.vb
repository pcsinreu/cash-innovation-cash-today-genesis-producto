﻿Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresPackModular

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <Serializable()> _
    Public Class DetalleEfectivo

#Region "[PROPRIEDADES]"

        Public Property codDivisa As String
        Public Property desDivisa As String
        Public Property codSimbolo As String
        Public Property codDenominacion As String
        Public Property codCalidad As String
        Public Property disponible As Boolean
        Public Property bloqueado As Boolean
        Public Property cantidad As Integer
        Public Property importe As Decimal

#End Region

    End Class

End Namespace