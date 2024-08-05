Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSetor.SetTiposSectores

    <XmlType(Namespace:="urn:SetTiposSectores")> _
    <XmlRoot(Namespace:="urn:SetTiposSectores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _codTipoSector As String
        Private _codigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta
#End Region

#Region "[PROPRIEDADE]"

        Public Property codTipoSector() As String
            Get
                Return _codTipoSector
            End Get
            Set(value As String)
                _codTipoSector = value
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

#End Region

    End Class
End Namespace
