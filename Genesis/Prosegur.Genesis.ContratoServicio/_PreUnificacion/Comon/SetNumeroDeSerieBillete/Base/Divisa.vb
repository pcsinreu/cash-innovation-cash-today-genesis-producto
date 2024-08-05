Namespace Comon.SetNumeroDeSerieBillete.Base

    ''' <summary>
    ''' Divisa
    ''' </summary>
    ''' <history>
    ''' [mult.guilherme.silva] 17/07/2013 Criado
    ''' </history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Divisa
#Region "Variáveis"

        Private _CodDivisa As String
        Private _Denominaciones As DenominacionColeccion

#End Region

#Region "Propriedades"

        Public Property CodDivisa() As String
            Get
                Return _CodDivisa
            End Get
            Set(value As String)
                _CodDivisa = value
            End Set
        End Property

        Public Property Denominaciones() As DenominacionColeccion
            Get
                Return _Denominaciones
            End Get
            Set(value As DenominacionColeccion)
                _Denominaciones = value
            End Set
        End Property

#End Region




    End Class

End Namespace
