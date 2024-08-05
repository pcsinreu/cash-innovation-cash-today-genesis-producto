Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado

    <XmlType(Namespace:="urn:RecuperarSaldoExpuestoxDetallado")> _
    <XmlRoot(Namespace:="urn:RecuperarSaldoExpuestoxDetallado")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Saldos As ObservableCollection(Of Clases.Saldo)

        'Propriedade adicionada para adaptar o serviço, porque há telas no novo salidas que necessita de mais informações
        'que não há na estrutura das classes
        Public Property InfoAdicionalesElementos As ObservableCollection(Of InformacionesAdicionales)

    End Class

End Namespace