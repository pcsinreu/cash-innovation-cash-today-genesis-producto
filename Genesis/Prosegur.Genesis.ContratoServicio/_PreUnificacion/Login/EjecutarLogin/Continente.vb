Namespace Login.EjecutarLogin

    ''' <summary>
    ''' Continente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>
    <Serializable()> _
    Public Class Continente

#Region " Variáveis "

        Private _Nombre As String
        Private _Paises As New List(Of Pais)

#End Region

#Region "Propriedades"

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Paises() As List(Of Pais)
            Get
                Return _Paises
            End Get
            Set(value As List(Of Pais))
                _Paises = value
            End Set
        End Property

#End Region

    End Class

End Namespace