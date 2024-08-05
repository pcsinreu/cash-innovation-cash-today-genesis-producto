Namespace Clases

    ' ***********************************************************************
    '  Modulo:  TerminoIAC.vb
    '  Descripción: Clase definición TerminoIAC
    ' ***********************************************************************
    <Serializable()>
    Public Class TerminoIAC
        Inherits Termino

#Region "Variaveis"

        Private _BuscarParcial As Boolean
        Private _EsCampoClave As Boolean
        Private _EsObligatorio As Boolean
        Private _EsTerminoCopia As Boolean
        Private _EsProtegido As Boolean

#End Region

#Region "Propriedades"

        Public Property BuscarParcial As Boolean
            Get
                Return _BuscarParcial
            End Get
            Set(value As Boolean)
                SetProperty(_BuscarParcial, value, "BuscarParcial")
            End Set
        End Property

        Public Property EsCampoClave As Boolean
            Get
                Return _EsCampoClave
            End Get
            Set(value As Boolean)
                SetProperty(_EsCampoClave, value, "EsCampoClave")
            End Set
        End Property

        Public Property EsObligatorio As Boolean
            Get
                Return _EsObligatorio
            End Get
            Set(value As Boolean)
                SetProperty(_EsObligatorio, value, "EsObligatorio")
            End Set
        End Property

        Public Property EsTerminoCopia As Boolean
            Get
                Return _EsTerminoCopia
            End Get
            Set(value As Boolean)
                SetProperty(_EsTerminoCopia, value, "EsTerminoCopia")
            End Set
        End Property

        Public Property EsProtegido As Boolean
            Get
                Return _EsProtegido
            End Get
            Set(value As Boolean)
                SetProperty(_EsProtegido, value, "EsProtegido")
            End Set
        End Property


#End Region

    End Class

End Namespace
