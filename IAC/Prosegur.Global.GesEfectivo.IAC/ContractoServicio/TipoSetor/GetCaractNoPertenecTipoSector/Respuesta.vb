Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSetor.GetCaractNoPertenecTipoSector

    <XmlType(Namespace:="urn:GetCaractNoPertenecTipoSector")> _
    <XmlRoot(Namespace:="urn:GetCaractNoPertenecTipoSector")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _TipoSetorNoPerte As TipoSectorNotPerceteColeccion
        Private _Resultado As String
#End Region

#Region "[PROPRIEDADES]"

        Public Property TipoSetorNoPerte() As TipoSectorNotPerceteColeccion
            Get
                Return _TipoSetorNoPerte
            End Get
            Set(value As TipoSectorNotPerceteColeccion)
                _TipoSetorNoPerte = value
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

#End Region


    End Class
End Namespace
