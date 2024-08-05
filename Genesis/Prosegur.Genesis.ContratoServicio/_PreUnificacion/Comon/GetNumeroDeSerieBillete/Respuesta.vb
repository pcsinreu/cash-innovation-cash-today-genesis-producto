Imports System.Xml.Serialization

Namespace Comon.GetNumeroDeSerieBillete

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [mult.guilherme.silva] Criado 17/07/2013
    '''</history>


    <Serializable()> _
    <XmlType(Namespace:="urn:GetNumeroDeSerieBillete")> _
    <XmlRoot(Namespace:="urn:GetNumeroDeSerieBillete")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variaveis"

        Private _Archivos As Base.ArchivoColeccion

#End Region

#Region "Propriedades"

        Public Property Archivos() As Base.ArchivoColeccion
            Get
                Return _Archivos
            End Get
            Set(value As Base.ArchivoColeccion)
                _Archivos = value
            End Set
        End Property

#End Region

    End Class

End Namespace

