Imports System.Xml.Serialization
Imports System.Xml

Namespace Planta.SetPlanta
    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetPlanta")> _
    <XmlRoot(Namespace:="urn:SetPlanta")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _CodigoPlanta As String
        Private _codigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta
        Private _ImporteMaximo As ContractoServicio.ImporteMaximo.SetImporteMaximo.Respuesta

#End Region

#Region "[PROPRIEDADE]"

        Public Property CodigoPlanta() As String
            Get
                Return _CodigoPlanta
            End Get
            Set(value As String)
                _CodigoPlanta = value
            End Set
        End Property

        Public Property CodigosAjenos() As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta
            Get
                Return _codigoAjeno
            End Get
            Set(value As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta)
                _codigoAjeno = value
            End Set
        End Property

        Public Property ImportesMaximos() As ContractoServicio.ImporteMaximo.SetImporteMaximo.Respuesta
            Get
                Return _ImporteMaximo
            End Get
            Set(value As ContractoServicio.ImporteMaximo.SetImporteMaximo.Respuesta)
                _ImporteMaximo = value
            End Set
        End Property
#End Region

    End Class
End Namespace

