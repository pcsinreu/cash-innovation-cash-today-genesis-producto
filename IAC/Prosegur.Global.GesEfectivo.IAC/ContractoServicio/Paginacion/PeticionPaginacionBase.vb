Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon.Paginacion

Namespace Paginacion

    <Serializable()> _
    Public MustInherit Class PeticionPaginacionBase
        Implements IPeticionPaginacion

        Private _ParametrosPaginacion As New ParametrosPeticionPaginacion()
        Public Property ParametrosPaginacion As ParametrosPeticionPaginacion Implements IPeticionPaginacion.ParametrosPaginacion
            Get
                Return _ParametrosPaginacion
            End Get
            Set(value As ParametrosPeticionPaginacion)
                _ParametrosPaginacion = value
            End Set
        End Property
    End Class

End Namespace