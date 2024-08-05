﻿Imports System.Xml.Serialization
Imports System.Xml

Namespace Planta.VerificaExistencia
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 19/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificaExistencia")> _
    <XmlRoot(Namespace:="urn:VerificaExistencia")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Existe As Boolean

#End Region

#Region "[Propriedades]"

        Public Property Existe() As Boolean
            Get
                Return _Existe
            End Get
            Set(value As Boolean)
                _Existe = value
            End Set
        End Property

#End Region
    End Class
End Namespace
