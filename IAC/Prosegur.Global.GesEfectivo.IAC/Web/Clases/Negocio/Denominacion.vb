Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Denominación
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 17/02/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Denominacion
        Inherits BaseEntidade

#Region "[Variáveis]"

        Private _codDenominacion As String
        Private _desDenominacion As String
        Private _bolBillete As Boolean
        Private _numValor As Decimal

#End Region

#Region "[Propriedades]"

        Public Property CodDenominacion As String
            Get
                Return _codDenominacion
            End Get
            Set(value As String)
                _codDenominacion = value
            End Set
        End Property

        Public Property DesDenominacion As String
            Get
                Return _desDenominacion
            End Get
            Set(value As String)
                _desDenominacion = value
            End Set
        End Property

        Public Property BolBillete As Boolean
            Get
                Return _bolBillete
            End Get
            Set(value As Boolean)
                _bolBillete = value
            End Set
        End Property

        Public Property NumValor As Decimal
            Get
                Return _numValor
            End Get
            Set(value As Decimal)
                _numValor = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()



        End Sub

        Public Sub New(Codido As String, Descripcion As String)

            _codDenominacion = Codido
            _desDenominacion = Descripcion

        End Sub

        Public Sub New(Obj As Integracion.ContractoServicio.GetMorfologiaDetail.Denominacion)

            If Obj Is Nothing Then
                Exit Sub
            End If

            _bolBillete = Obj.BolBillete
            _codDenominacion = Obj.CodDenominacion
            _desDenominacion = Obj.DesDenominacion
            _numValor = Obj.NumValor
            
        End Sub

#End Region

    End Class

End Namespace