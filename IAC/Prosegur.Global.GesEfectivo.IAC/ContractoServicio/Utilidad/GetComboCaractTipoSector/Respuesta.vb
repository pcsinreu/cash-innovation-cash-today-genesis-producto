﻿Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboCaractTipoSector

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboCaractTipoSector")> _
    <XmlRoot(Namespace:="urn:GetComboCaractTipoSector")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _caracteristicas As CaracteristicaColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Caracteristicas() As CaracteristicaColeccion
            Get
                Return _caracteristicas
            End Get
            Set(value As CaracteristicaColeccion)
                _caracteristicas = value
            End Set
        End Property

#End Region

    End Class
End Namespace