Namespace Utilidad.GetComboDelegacionesPorPais

    ''' <summary>
    ''' Classe Delegacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 05/04/2012 Criado
    ''' </history>
    <Serializable()> _
    Public Class Delegacion

#Region "[Variáveis]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _CodPais As String

#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property CodPais() As String
            Get
                Return _CodPais
            End Get
            Set(value As String)
                _CodPais = value
            End Set
        End Property

#End Region

    End Class

End Namespace