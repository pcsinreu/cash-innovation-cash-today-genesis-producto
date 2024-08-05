Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.NuevoSalidas.Remesa.GenerarNuevoCodigoReciboRemesa

    <XmlType(Namespace:="urn:GenerarNuevoCodigoReciboRemesa")> _
       <XmlRoot(Namespace:="urn:GenerarNuevoCodigoReciboRemesa")> _
       <Serializable()> _
    Public NotInheritable Class Peticion
        Inherits BasePeticion

#Region "[VARIÁVEIS]"

        Public Property IdentificadorRemesaLegado As String
        Public Property CodigoDelegacion As String
        Public Property CodigoUsuario As String
        Public Property PrefixoReciboRemesa As String
        Public Property FyhActualizacion As DateTime

        Public Property codReciboRemesa As String


#End Region

    End Class

End Namespace