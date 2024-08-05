Imports System.Collections.ObjectModel

Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Elemento e Valor
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroConsultaFormulariosCertificados
        Inherits BindableBase

        Public Sub New()

            identificadoresClientes = New List(Of String)
            identificadoresTipoClientes = New List(Of String)

        End Sub

       
        Public Property identificadoresClientes As List(Of String)
        Public Property identificadoresTipoClientes As List(Of String)
        Public Property Version As String

    End Class

End Namespace

