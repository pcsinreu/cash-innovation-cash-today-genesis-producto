Namespace CargaPreviaEletronica.GetConfiguraciones

    <Serializable()>
    Public Class Configuracion_CPColeccion
        Inherits List(Of Configuracion_CP)

        Public Sub New()

        End Sub
        Public Sub New(capacity As Integer)
            MyBase.New(capacity)

        End Sub
        Public Sub New(collection As IEnumerable(Of Configuracion_CP))
            MyBase.New(collection)

        End Sub


    End Class


End Namespace