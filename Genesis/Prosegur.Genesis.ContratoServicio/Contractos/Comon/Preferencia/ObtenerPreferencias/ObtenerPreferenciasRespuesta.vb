Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Preferencia.ObtenerPreferencias
    <Serializable()>
    Public NotInheritable Class ObtenerPreferenciasRespuesta
        Inherits BaseRespuesta

        Public Property Preferencias As ObservableCollection(Of PreferenciaUsuario)
    End Class
End Namespace