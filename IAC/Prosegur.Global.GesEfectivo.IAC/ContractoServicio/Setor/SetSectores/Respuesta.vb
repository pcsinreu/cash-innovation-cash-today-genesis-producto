Imports System.Xml.Serialization
Imports System.Xml

Namespace Setor.SetSectores

    '''<sumary>
    ''' Classe de Repuesta da Peticion
    ''' </sumary>
    ''' <remarks></remarks>
    ''' <history>
    ''' poncalves 08/03/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetSectores")> _
    <XmlRoot(Namespace:="urn:SetSectores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"
        Private _codSector As String
        Private _Resultado As String
        Private _codigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta
        Private _ImporteMaximo As ContractoServicio.ImporteMaximo.SetImporteMaximo.Respuesta
#End Region

#Region "[PROPRIEDADE]"

        Public Property codSector() As String
            Get
                Return _codSector
            End Get
            Set(value As String)
                _codSector = value
            End Set
        End Property

        Public Property Resultado() As String
            Get
                Return _Resultado
            End Get
            Set(value As String)
                _Resultado = value
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

