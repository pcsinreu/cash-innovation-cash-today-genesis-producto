Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Data

Namespace Genesis

    Public Class ValorTerminoIAC

        ''' <summary>
        ''' Cargar Valor Termino IAC
        ''' </summary>
        ''' <param name="tdValorTerminoIAC"></param>
        ''' <param name="objGrupoTerminosIAC"></param>
        ''' <remarks></remarks>
        Public Shared Sub cargarValorTerminoIAC(tdValorTerminoIAC As DataTable, ByRef objGrupoTerminosIAC As Clases.GrupoTerminosIAC)

            If tdValorTerminoIAC IsNot Nothing AndAlso tdValorTerminoIAC.Rows.Count > 0 Then
                For Each objRow In tdValorTerminoIAC.Rows
                    objGrupoTerminosIAC.TerminosIAC.FirstOrDefault(Function(x) x.Codigo = objRow("COD_TERMINO_IAC")).Valor = Util.AtribuirValorObj(objRow("DES_VALOR_IAC"), GetType(String))
                Next
            End If

        End Sub

        ''' <summary>
        ''' Obtener Valor Termino Por Elemento
        ''' </summary>
        ''' <param name="identificadorElemento"></param>
        ''' <param name="TipoElemento"></param>
        ''' <param name="objGrupoTerminosIAC"></param>
        ''' <remarks></remarks>
        Public Shared Sub ObtenerValorTerminoPorElemento(identificadorElemento As String, TipoElemento As Enumeradores.TipoElemento, ByRef objGrupoTerminosIAC As Clases.GrupoTerminosIAC)
            Dim tdValorTerminos As DataTable = AccesoDatos.Genesis.ValorTerminoIAC.ObtenerValorTerminoPorElemento(identificadorElemento, TipoElemento)
            cargarValorTerminoIAC(tdValorTerminos, objGrupoTerminosIAC)
        End Sub















        Public Shared Function RecuperarValoresTerminosIAC(identificadorTerminoIAC As String) As ObservableCollection(Of Clases.TerminoValorPosible)

            Dim listaTerminoValorPosible As ObservableCollection(Of Clases.TerminoValorPosible) = Nothing

            Try
                listaTerminoValorPosible = AccesoDatos.Genesis.ValorTerminoIAC.RecuperarValoresTerminosIAC(identificadorTerminoIAC)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return listaTerminoValorPosible
        End Function

#Region "ValorTerminoGrupoDocumentos"

        ''' <summary>
        ''' Deleta os valores de termino para o grupo de documento informado
        ''' </summary>
        ''' <param name="GrupoDocumento"></param>
        ''' <remarks></remarks>
        Public Shared Sub DeletarValoresTerminoGrupoDocumento(GrupoDocumento As Clases.GrupoDocumentos)

            Try

                AccesoDatos.Genesis.ValorTerminoGrupoDocumento.ValorTerminoGrupoDocumentoExcluir(GrupoDocumento.Identificador)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub

        ''' <summary>
        ''' Insere os valores para os termino do grupo de documentos.
        ''' </summary>
        ''' <param name="GrupoTerminosIAC"></param>
        ''' <remarks></remarks>
        Public Shared Sub InserirValorTerminoGrupoDocumentos(GrupoTerminosIAC As Clases.GrupoTerminosIAC, _
                                                             IdentificadorGrupoDocumentos As String, usuario As String)

            Try

                If GrupoTerminosIAC IsNot Nothing AndAlso GrupoTerminosIAC.TerminosIAC IsNot Nothing AndAlso GrupoTerminosIAC.TerminosIAC.Count > 0 Then

                    For Each Termino In GrupoTerminosIAC.TerminosIAC.Where(Function(w) Not String.IsNullOrWhiteSpace(w.Valor))
                        AccesoDatos.Genesis.ValorTerminoGrupoDocumento.ValorTerminoGrupoDocumentoInserir(IdentificadorGrupoDocumentos, Termino, usuario)
                    Next

                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub

#End Region

    End Class

End Namespace

