Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Divisa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  08/02/2011 criado
    ''' </history>
    <Serializable()> _
    Public Class Divisa
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _CodigoDivisa As String
        Private _DescripcionDivisa As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoDivisa As String
            Get
                Return _CodigoDivisa
            End Get
            Set(value As String)
                _CodigoDivisa = value
            End Set
        End Property

        Public Property DescripcionDivisa As String
            Get
                Return _DescripcionDivisa
            End Get
            Set(value As String)
                _DescripcionDivisa = value
            End Set
        End Property
        
#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()


        End Sub

#End Region

#Region "[MÉTODOS]"

    
#End Region

    End Class

End Namespace
