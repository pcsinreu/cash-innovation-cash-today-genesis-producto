Namespace Utilidad.GetComboSectores

    ''' <summary>
    ''' Classe Sector
    ''' </summary>
    ''' <remarks></remarks>
    
    <Serializable()> _
    Public Class Sector1

#Region "[Variáveis]"

        Private _OidSector As String
        Private _Codigo As String
        Private _Descripcion As String


#End Region

#Region "[Propriedades]"

        Public Property OidSector() As String
            Get
                Return _OidSector
            End Get
            Set(value As String)
                _OidSector = value
            End Set

        End Property

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



#End Region

    End Class

End Namespace