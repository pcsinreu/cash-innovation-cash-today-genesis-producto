Namespace Comon.SetNumeroDeSerieBillete.Base

    ''' <summary>
    ''' Denominacion
    ''' </summary>
    ''' <history>
    ''' [mult.guilherme.silva] 17/07/2013 Criado
    ''' </history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Denominacion

#Region "Variáveis"

        Private _CodDenominacion As String
        Private _CodNumeroSerie As String

#End Region



#Region "Propriedades"

        Public Property CodNumeroSerie() As String
            Get
                Return _CodNumeroSerie
            End Get
            Set(value As String)
                _CodNumeroSerie = value
            End Set
        End Property


        Public Property CodDenominacion() As String
            Get
                Return _CodDenominacion
            End Get
            Set(value As String)
                _CodDenominacion = value
            End Set
        End Property


#End Region


    End Class

End Namespace
