﻿Imports System.Xml.Serialization

Namespace Legado.GeneracionF22

    ''' <summary>
    ''' Lista entidades InfoRemesa
    ''' </summary>
    ''' <history>[abueno] 13/07/2010 Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class InfoRemesaColeccion
        Inherits List(Of InfoRemesa)

    End Class

End Namespace