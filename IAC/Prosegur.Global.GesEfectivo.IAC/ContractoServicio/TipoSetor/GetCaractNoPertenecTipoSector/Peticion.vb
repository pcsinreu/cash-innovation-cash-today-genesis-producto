Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSetor.GetCaractNoPertenecTipoSector

    <XmlType(Namespace:="urn:GetCaractNoPertenecTipoSector")> _
    <XmlRoot(Namespace:="urn:GetCaractNoPertenecTipoSector")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _codTipoSector As String
#End Region

        Public Property codTipoSector() As String
            Get
                Return _codTipoSector
            End Get
            Set(value As String)
                _codTipoSector = value
            End Set
        End Property

    End Class
End Namespace