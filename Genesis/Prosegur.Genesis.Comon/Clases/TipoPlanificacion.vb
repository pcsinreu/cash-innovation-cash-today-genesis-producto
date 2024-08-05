Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases

    <Serializable()>
    Public Class TipoPlanificacion
        Inherits BindableBase
        Implements IEntidadeHelper

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String


#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Codigo As String Implements IEntidadeHelper.Codigo
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String Implements IEntidadeHelper.Descripcion
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

#End Region

    End Class

End Namespace


