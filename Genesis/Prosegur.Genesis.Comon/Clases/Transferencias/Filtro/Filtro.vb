Imports System.Collections.ObjectModel

Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Elemento e Valor
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class Filtro
        Inherits BindableBase

#Region "Variaveis"

        Private _Sector As Comon.Clases.Sector
        Private _Cliente As Comon.Clases.Cliente
        Private _Canais As ObservableCollection(Of Comon.Clases.Canal)
        Private _Divisas As ObservableCollection(Of Comon.Clases.Divisa)
        Private _Documento As ObservableCollection(Of FiltroDocumento)
        Private _Contenedor As ObservableCollection(Of FiltroContenedor)
        Private _Remesa As ObservableCollection(Of FiltroRemesa)
        Private _Bulto As ObservableCollection(Of FiltroBulto)
        Private _Parcial As ObservableCollection(Of FiltroParcial)
        Private _esGestionBulto As Boolean?
        Private _esUmBultoPorRemesa As Boolean
        Private _TipoValores As String
        Private _ExcluirSectoresHijos As Boolean
        Private _RestringirEstados As Boolean
        Private _Delegacion As Clases.Delegacion
        Private _esConsiderarSomaZero As Boolean
        Private _usuario As String

#End Region

#Region "Propriedades"

        Public Property Delegacion As Comon.Clases.Delegacion
            Get
                Return _Delegacion
            End Get
            Set(value As Comon.Clases.Delegacion)
                SetProperty(_Delegacion, value, "Delegacion")
            End Set
        End Property

        Public Property Sector As Comon.Clases.Sector
            Get
                Return _Sector
            End Get
            Set(value As Comon.Clases.Sector)
                SetProperty(_Sector, value, "Sector")
            End Set
        End Property

        Public Property Cliente As Comon.Clases.Cliente
            Get
                Return _Cliente
            End Get
            Set(value As Comon.Clases.Cliente)
                SetProperty(_Cliente, value, "Cliente")
            End Set
        End Property

        Public Property Canais As ObservableCollection(Of Comon.Clases.Canal)
            Get
                Return _Canais
            End Get
            Set(value As ObservableCollection(Of Comon.Clases.Canal))
                SetProperty(_Canais, value, "Canais")
            End Set
        End Property

        ' Datos de Valores
        Public Property Divisas As ObservableCollection(Of Comon.Clases.Divisa)
            Get
                Return _Divisas
            End Get
            Set(value As ObservableCollection(Of Comon.Clases.Divisa))
                SetProperty(_Divisas, value, "Divisas")
            End Set
        End Property

        ' Datos de Elementos
        Public Property Documento As ObservableCollection(Of FiltroDocumento)
            Get
                Return _Documento
            End Get
            Set(value As ObservableCollection(Of FiltroDocumento))
                SetProperty(_Documento, value, "Documento")
            End Set
        End Property

        Public Property Contenedor As ObservableCollection(Of FiltroContenedor)
            Get
                Return _Contenedor
            End Get
            Set(value As ObservableCollection(Of FiltroContenedor))
                SetProperty(_Contenedor, value, "Contenedor")
            End Set
        End Property

        Public Property Remesa As ObservableCollection(Of FiltroRemesa)
            Get
                Return _Remesa
            End Get
            Set(value As ObservableCollection(Of FiltroRemesa))
                SetProperty(_Remesa, value, "Remesa")
            End Set
        End Property

        Public Property Bulto As ObservableCollection(Of FiltroBulto)
            Get
                Return _Bulto
            End Get
            Set(value As ObservableCollection(Of FiltroBulto))
                SetProperty(_Bulto, value, "Bulto")
            End Set
        End Property

        Public Property Parcial As ObservableCollection(Of FiltroParcial)
            Get
                Return _Parcial
            End Get
            Set(value As ObservableCollection(Of FiltroParcial))
                SetProperty(_Parcial, value, "Parcial")
            End Set
        End Property

        Public Property EsGestionBulto As Boolean?
            Get
                Return _esGestionBulto
            End Get
            Set(value As Boolean?)
                SetProperty(_esGestionBulto, value, "EsGestionBulto")
            End Set
        End Property

        Public Property EsUmBultoPorRemesa As Boolean
            Get
                Return _esUmBultoPorRemesa
            End Get
            Set(value As Boolean)
                SetProperty(_esUmBultoPorRemesa, value, "EsUmBultoPorRemesa")
            End Set
        End Property

        Public Property TipoValores As String
            Get
                Return _TipoValores
            End Get
            Set(value As String)
                SetProperty(_TipoValores, value, "TipoValores")
            End Set
        End Property

        Public Property ExcluirSectoresHijos As Boolean
            Get
                Return _ExcluirSectoresHijos
            End Get
            Set(value As Boolean)
                SetProperty(_ExcluirSectoresHijos, value, "excluirSectoresHijos")
            End Set
        End Property

        Public Property RestringirEstados As Boolean
            Get
                Return _RestringirEstados
            End Get
            Set(value As Boolean)
                SetProperty(_RestringirEstados, value, "RestringirEstados")
            End Set
        End Property

        Public Property EsConsiderarSomaZero As Boolean
            Get
                Return _esConsiderarSomaZero
            End Get
            Set(value As Boolean)
                SetProperty(_esConsiderarSomaZero, value, "EsConsiderarSomaZero")
            End Set
        End Property

        Public Property Usuario As String
            Get
                Return _usuario
            End Get
            Set(value As String)
                SetProperty(_usuario, value, "Usuario")
            End Set
        End Property
#End Region

    End Class

End Namespace