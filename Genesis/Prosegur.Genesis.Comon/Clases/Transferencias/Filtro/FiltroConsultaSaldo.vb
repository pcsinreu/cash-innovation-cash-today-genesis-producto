Imports System.Collections.ObjectModel

Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Elemento e Valor
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroConsultaSaldo
        Inherits BindableBase

        Public Sub New()

            identificadoresPlantas = New List(Of String)
            identificadoresSectores = New List(Of String)
            identificadoresClientes = New List(Of String)
            identificadoresSubClientes = New List(Of String)
            identificadoresPtoServicios = New List(Of String)
            identificadoresCanales = New List(Of String)
            identificadoresSubCanales = New List(Of String)

        End Sub

        Public Property identificadoresPlantas As List(Of String)
        Public Property identificadoresSectores As List(Of String)
        Public Property identificadoresClientes As List(Of String)
        Public Property identificadoresSubClientes As List(Of String)
        Public Property identificadoresPtoServicios As List(Of String)
        Public Property identificadoresCanales As List(Of String)
        Public Property identificadoresSubCanales As List(Of String)
        Public Property Disponibilidad As Enumeradores.Disponibilidad
        Public Property DiscriminarPor As Enumeradores.DiscriminarPor
        Public Property DetallarSaldoSectoresHijos As Boolean
        Public Property Version As String

    End Class

End Namespace

