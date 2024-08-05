Namespace Clases
    Public Class DatoBancarioComparativo
        Inherits BindableBase


#Region "fields"
        Private _datoBancarioOriginal As DatoBancario
        Private _datoBancarioCambio As DatoBancario

#End Region

#Region "properties"
        Public Property DatoBancarioOriginal As DatoBancario
            Get
                Return _datoBancarioOriginal
            End Get
            Set(value As DatoBancario)
                SetProperty(_datoBancarioOriginal, value, "DatoBancarioOriginal")
            End Set
        End Property
        Public Property DatoBancarioCambio As DatoBancario
            Get
                Return _datoBancarioCambio
            End Get
            Set(value As DatoBancario)
                SetProperty(_datoBancarioCambio, value, "DatoBancarioCambio")
            End Set
        End Property
#End Region
    End Class
End Namespace
