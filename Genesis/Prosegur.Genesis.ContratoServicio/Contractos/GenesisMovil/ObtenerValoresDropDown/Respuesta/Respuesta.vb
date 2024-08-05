Namespace Contractos.GenesisMovil.ObtenerValoresDropDown

    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Sub New()

            Me.ListaCliente = New List(Of Iten)
            Me.ListaCanal = New List(Of Iten)
            Me.ListaDenominacion = New List(Of Iten)
            Me.ListaDivisa = New List(Of Iten)
            Me.ListaSubCanal = New List(Of Iten)
            Me.ListaTipoContenedor = New List(Of Iten)

        End Sub

#Region "[PROPRIEDADES]"

        Public Property ListaCliente As List(Of Iten)

        Public Property ListaCanal As List(Of Iten)

        Public Property ListaSubCanal As List(Of Iten)

        Public Property ListaDivisa As List(Of Iten)

        Public Property ListaDenominacion As List(Of Iten)

        Public Property ListaTipoContenedor As List(Of Iten)

#End Region

    End Class

End Namespace

