Namespace Legado.GeneracionF22

    ''' <summary>
    ''' Classe Bulto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [abueno] 13/07/2010 - Criado
    ''' </history>
    <Serializable()> _
    Public Class Bulto

#Region "[VARIÁVEIS]"

        Private _CodigoPrecinto As String
        Private _BolPicos As Boolean
        Private _CodTipoBulto As String
        Private _Efeitivos As EfectivoColeccion

#End Region

#Region "[PROPRIEDADES]"
        ''' <summary>
        ''' Propriedad CodigoPrecinto
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoPrecinto() As String
            Get
                Return _CodigoPrecinto
            End Get
            Set(Value As String)
                _CodigoPrecinto = Value
            End Set
        End Property


        ''' <summary>
        ''' Propriedad Efeitivo
        ''' </summary>
        ''' <value>EfectivoColeccion</value>
        ''' <returns>EfectivoColeccion</returns>
        ''' <remarks></remarks>
        Public Property Efeitivos() As EfectivoColeccion
            Get
                Return _Efeitivos
            End Get
            Set(Value As EfectivoColeccion)
                _Efeitivos = Value
            End Set
        End Property


        ''' <summary>
        ''' Propriedad BolPicos
        ''' </summary>
        ''' <value>Boolean</value>
        ''' <returns>Boolean</returns>
        ''' <remarks></remarks>
        Public Property BolPicos() As Boolean
            Get
                Return _BolPicos
            End Get
            Set(Value As Boolean)
                _BolPicos = Value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodTipoBulto
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodTipoBulto() As String
            Get
                Return _CodTipoBulto
            End Get
            Set(Value As String)
                _CodTipoBulto = Value
            End Set
        End Property

#End Region

    End Class

End Namespace