Namespace Comon.SetNumeroDeSerieBillete.Base

    ''' <summary>
    ''' Archivo
    ''' </summary>
    ''' <history>
    ''' [mult.guilherme.silva] 17/07/2013 Criado
    ''' </history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Archivo

#Region "Variáveis"

        Private _NombreArchivo As String
        Private _Divisas As DivisaColeccion


#End Region

#Region "Propriedades"

        Public Property NombreArchivo() As String
            Get
                Return _NombreArchivo
            End Get
            Set(value As String)
                _NombreArchivo = value
            End Set
        End Property

        Public Property Divisas() As DivisaColeccion

            Get
                Return _Divisas
            End Get
            Set(value As DivisaColeccion
)
                _Divisas = value
            End Set
        End Property

#End Region

    End Class

End Namespace
