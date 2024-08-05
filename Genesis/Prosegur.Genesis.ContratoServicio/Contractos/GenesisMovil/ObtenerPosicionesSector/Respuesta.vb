Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Contractos.GenesisMovil.ObtenerPosicionesSector
    <Serializable()> _
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property PosicionesSector As List(Of PosicionSector)

    End Class
End Namespace
