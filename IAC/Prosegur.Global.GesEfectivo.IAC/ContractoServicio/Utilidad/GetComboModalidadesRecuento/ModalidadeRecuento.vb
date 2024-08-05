Namespace Utilidad.GetComboModalidadesRecuento

    <Serializable()> _
    Public Class ModalidadeRecuento

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _admiteIac As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property AdmiteIac() As Boolean
            Get
                Return _admiteIac
            End Get
            Set(value As Boolean)
                _admiteIac = value
            End Set
        End Property

#End Region

    End Class

End Namespace