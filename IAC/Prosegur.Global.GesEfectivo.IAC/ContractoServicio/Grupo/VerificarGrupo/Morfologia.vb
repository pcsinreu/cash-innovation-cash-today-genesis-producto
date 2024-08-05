
Namespace Grupo.VerificarGrupo

    ''' <summary>
    ''' Classe Morfologia
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 13/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Morfologia

#Region "[Variáveis]"

        Private _bolExiste As Boolean

#End Region

#Region "[Propriedades]"

        Public Property BolExiste() As Boolean
            Get
                Return _bolExiste
            End Get
            Set(value As Boolean)
                _bolExiste = value
            End Set
        End Property

#End Region

    End Class

End Namespace