Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Public Class HelperDocumento

    Public Shared Function ObtenerPadreDocumentoRelacionado(documentoRaiz As Clases.Documento, documentoRelacionado As Clases.Documento) As Clases.Documento
        Dim documentoPai As Clases.Documento = Nothing
        If documentoRaiz.DocumentosRelacionados.Find(Function(d) d.Identificador = documentoRelacionado.Identificador) IsNot Nothing Then
            documentoPai = documentoRaiz
        Else
            For Each documentoFilho As Clases.Documento In documentoRaiz.DocumentosRelacionados
                documentoPai = ObtenerPadreDocumentoRelacionado(documentoFilho, documentoRelacionado)
                If documentoPai IsNot Nothing Then
                    Exit For
                End If
            Next
        End If
        Return documentoPai
    End Function

    Public Shared Function ObtenerDocumentoElemento(documentoRaiz As Clases.Documento, elemento As Clases.Elemento) As Clases.Documento
        Dim documento As Clases.Documento = Nothing
        If documentoRaiz.Elemento.Identificador = elemento.Identificador Then
            documento = documentoRaiz
        Else
            For Each documentoFilho As Clases.Documento In documentoRaiz.DocumentosRelacionados
                documento = ObtenerDocumentoElemento(documentoFilho, elemento)
                If documento IsNot Nothing Then
                    Exit For
                End If
            Next
        End If
        Return documento
    End Function

    Public Shared Function ObtenerGenerarDocumentoElemento(documentoRaiz As Clases.Documento, elemento As Clases.Elemento) As Clases.Documento
        Dim documentoElemento As Clases.Documento = ObtenerDocumentoElemento(documentoRaiz, elemento)
        If documentoElemento Is Nothing Then
            Dim elementoPadre As Clases.Elemento = ObtenerPadreElemento(documentoRaiz.Elemento, elemento)
            Dim documentoPadre As Clases.Documento = ObtenerGenerarDocumentoElemento(documentoRaiz, elementoPadre)
            documentoElemento = documentoPadre.Clonar()
            documentoElemento.Identificador = System.Guid.NewGuid().ToString()
            documentoElemento.Elemento = elemento
            documentoPadre.DocumentosRelacionados.Add(documentoElemento)
        End If
        Return documentoElemento
    End Function

    Public Shared Sub ActualizarDocumento(documentoPadre As Clases.Documento, documento As Clases.Documento)
        documentoPadre.DocumentosRelacionados.Remove(documentoPadre.DocumentosRelacionados.Find(Function(d) d.Identificador = documento.Identificador))
        documentoPadre.DocumentosRelacionados.Add(documento)
    End Sub

    Public Shared Sub ActualizarElemento(elementoPadre As Clases.Elemento, elemento As Clases.Elemento)
        Select Case ObtenerTipoElemento(elementoPadre)
            Case Enumeradores.TipoElemento.Contenedor
                Dim contenedor As Clases.Contenedor = DirectCast(elementoPadre, Clases.Contenedor)
                contenedor.Elementos.Remove(contenedor.Elementos.Find(Function(e) e.Identificador = elemento.Identificador))
                contenedor.Elementos.Add(elemento)

            Case Enumeradores.TipoElemento.Remesa
                Dim remesa As Clases.Remesa = DirectCast(elementoPadre, Clases.Remesa)
                remesa.Bultos.Remove(remesa.Bultos.Find(Function(b) b.Identificador = elemento.Identificador))
                remesa.Bultos.Add(elemento)

            Case Enumeradores.TipoElemento.Bulto
                Dim bulto As Clases.Bulto = DirectCast(elementoPadre, Clases.Bulto)
                bulto.Parciales.Remove(bulto.Parciales.Find(Function(p) p.Identificador = elemento.Identificador))
                bulto.Parciales.Add(elemento)

        End Select
    End Sub

    Public Shared Function ObtenerPadreElemento(elementoRaiz As Clases.Elemento, elementoHijo As Clases.Elemento) As Clases.Elemento
        Dim elementoPai As Clases.Elemento = Nothing
        Select Case ObtenerTipoElemento(elementoRaiz)
            Case Enumeradores.TipoElemento.Contenedor
                If DirectCast(elementoRaiz, Clases.Contenedor).Elementos.Find(Function(e) e.Identificador = elementoHijo.Identificador) IsNot Nothing Then
                    elementoPai = elementoRaiz
                End If

            Case Enumeradores.TipoElemento.Remesa
                If DirectCast(elementoRaiz, Clases.Remesa).Bultos.Find(Function(b) b.Identificador = elementoHijo.Identificador) IsNot Nothing Then
                    elementoPai = elementoRaiz
                End If

            Case Enumeradores.TipoElemento.Bulto
                If DirectCast(elementoRaiz, Clases.Bulto).Parciales.Find(Function(p) p.Identificador = elementoHijo.Identificador) IsNot Nothing Then
                    elementoPai = elementoRaiz
                End If

        End Select
        If elementoPai Is Nothing Then
            For Each itemElemento As Clases.Elemento In ObtenerElementosHijos(elementoRaiz)
                If ObtenerTipoElemento(itemElemento) <> Enumeradores.TipoElemento.Parcial Then
                    elementoPai = ObtenerPadreElemento(itemElemento, elementoHijo)
                    If elementoPai IsNot Nothing Then
                        Exit For
                    End If
                End If
            Next
        End If
        Return elementoPai
    End Function

    Public Shared Function ObtenerElementosHijos(elemento As Clases.Elemento) As IEnumerable(Of Clases.Elemento)
        Select Case True

            Case TypeOf elemento Is Clases.Contenedor
                Dim elementoContenedor As Clases.Contenedor = DirectCast(elemento, Clases.Contenedor)
                If elementoContenedor.Elementos Is Nothing Then
                    elementoContenedor.Elementos = New ObservableCollection(Of Clases.Elemento)()
                End If
                Return elementoContenedor.Elementos.Cast(Of Clases.Elemento).AsEnumerable()

            Case TypeOf elemento Is Clases.Remesa
                Dim elementoRemesa As Clases.Remesa = DirectCast(elemento, Clases.Remesa)
                If elementoRemesa.Bultos Is Nothing Then
                    elementoRemesa.Bultos = New ObservableCollection(Of Clases.Bulto)()
                End If
                Return elementoRemesa.Bultos.FindAll(Function(x) x.Estado <> Enumeradores.EstadoBulto.Eliminado).Cast(Of Clases.Elemento).AsEnumerable()

            Case TypeOf elemento Is Clases.Bulto
                Dim elementoBulto As Clases.Bulto = DirectCast(elemento, Clases.Bulto)
                If elementoBulto.Parciales Is Nothing Then
                    elementoBulto.Parciales = New ObservableCollection(Of Clases.Parcial)
                End If
                Return elementoBulto.Parciales.FindAll(Function(x) x.Estado <> Enumeradores.EstadoBulto.Eliminado).Cast(Of Clases.Elemento).AsEnumerable()

            Case TypeOf elemento Is Clases.Parcial
                Return Nothing

            Case Else
                Throw New NotImplementedException()

        End Select
    End Function

    Public Shared Function ObtenerTipoElemento(elemento As Clases.Elemento) As Enumeradores.TipoElemento
        Dim tipoElemento As Enumeradores.TipoElemento
        Select Case True
            Case TypeOf elemento Is Clases.Contenedor
                tipoElemento = Enumeradores.TipoElemento.Contenedor
            Case TypeOf elemento Is Clases.Remesa
                tipoElemento = Enumeradores.TipoElemento.Remesa
            Case TypeOf elemento Is Clases.Bulto
                tipoElemento = Enumeradores.TipoElemento.Bulto
            Case TypeOf elemento Is Clases.Parcial
                tipoElemento = Enumeradores.TipoElemento.Parcial
        End Select
        Return tipoElemento
    End Function

End Class
