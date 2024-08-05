Namespace Iac.GetIac

    <Serializable()> _
    Public Class TerminosIac

#Region "Variáveis"

        Private _codigoTermino As String
        Private _descripcionTermino As String

#End Region

#Region "Propriedades"
        Public Property CodigoTermino() As String
            Get
                Return _codigoTermino
            End Get
            Set(value As String)
                _codigoTermino = value
            End Set
        End Property

        Public Property DescripcionTermino() As String
            Get
                Return _descripcionTermino
            End Get
            Set(value As String)
                _descripcionTermino = value
            End Set
        End Property
#End Region

    End Class
End Namespace