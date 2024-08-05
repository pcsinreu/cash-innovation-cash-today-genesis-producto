Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.BorrarBulto

    <XmlType(Namespace:="urn:BorrarBulto")> _
    <XmlRoot(Namespace:="urn:BorrarBulto")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorRemesa As String
        Public Property IdentificadorBulto As String
        ''' <summary>
        ''' Identificador do malote que receberá os efectivos do bulto excluido
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IdentificadorBultoRecibirEfectivo As String

        Public Property UsuarioModificacion As String

    End Class

End Namespace