﻿Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.VerificarDescripcionAgrupacion

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarDescripcionAgrupacion")> _
    <XmlRoot(Namespace:="urn:VerificarDescripcionAgrupacion")> _
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