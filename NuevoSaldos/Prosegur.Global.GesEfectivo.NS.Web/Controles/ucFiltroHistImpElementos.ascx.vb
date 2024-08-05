Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis

Public Class ucFiltroHistImpElementos
    Inherits UcBase

#Region "[PROPRIEDADES]"

#End Region

#Region "[OVERRIDES]"

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

#End Region

#Region "[DELEGATE]"

    <Serializable()>
    Public Class FiltroActualizadoEventArgs
        Public Elementos As ObservableCollection(Of Comon.Clases.Elemento)
    End Class

    Public Event FiltroActualizado(sender As Object, e As FiltroActualizadoEventArgs)

#End Region

#Region "[METODOS]"

#End Region

End Class