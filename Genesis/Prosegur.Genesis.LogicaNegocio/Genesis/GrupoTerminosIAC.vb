Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Data

Namespace Genesis

    Public Class GrupoTerminosIAC

        Public Shared Function cargarGrupoTerminosIAC(tdGrupoTerminosIAC As DataTable) As ObservableCollection(Of Clases.GrupoTerminosIAC)

            Dim objGrupoTerminosIAC As New ObservableCollection(Of Clases.GrupoTerminosIAC)

            If tdGrupoTerminosIAC IsNot Nothing AndAlso tdGrupoTerminosIAC.Rows.Count Then

                For Each objRow In tdGrupoTerminosIAC.Rows

                    Dim objGrupo = New Clases.GrupoTerminosIAC

                    With objGrupo
                        .Identificador = Util.AtribuirValorObj(objRow("OID_IAC"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(objRow("COD_IAC"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(objRow("DES_IAC"), GetType(String))
                        .Observacion = Util.AtribuirValorObj(objRow("OBS_IAC"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(objRow("BOL_VIGENTE"), GetType(Boolean))
                        .EsInvisible = Util.AtribuirValorObj(objRow("BOL_INVISIBLE"), GetType(Boolean))
                        .CodigoUsuario = Util.AtribuirValorObj(objRow("COD_USUARIO"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(objRow("FYH_ACTUALIZACION"), GetType(DateTime))
                        .CopiarDeclarados = Util.AtribuirValorObj(objRow("BOL_COPIA_DECLARADOS"), GetType(Boolean))
                        .TerminosIAC = TerminoIAC.ObtenerTerminosIACPorIdentificador(.Identificador)
                    End With

                    objGrupoTerminosIAC.Add(objGrupo)
                Next


            End If
            Return objGrupoTerminosIAC
        End Function

        ''' <summary>
        ''' Obtener el GrupoTerminosIAC por lo Identificador
        ''' </summary>
        ''' <param name="identificadorGrupoTerminosIAC"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerGrupoTerminosIACPorIdentificador(identificadorGrupoTerminosIAC As String, _
                                ByRef TerminosIACPosibles As ObservableCollection(Of Clases.GrupoTerminosIAC)) As Clases.GrupoTerminosIAC

            Dim objGrupoTerminosIAC As Clases.GrupoTerminosIAC = Nothing

            Try
                objGrupoTerminosIAC = TerminosIACPosibles.FirstOrDefault(Function(x) x.Identificador = identificadorGrupoTerminosIAC)

                If objGrupoTerminosIAC Is Nothing Then
                    Dim tdGrupoTerminosIACs As DataTable = AccesoDatos.Genesis.GrupoTerminosIAC.ObtenerGrupoTerminosIACPorIdentificador(identificadorGrupoTerminosIAC)
                    Dim objGrupoTerminosIACs As ObservableCollection(Of Clases.GrupoTerminosIAC) = cargarGrupoTerminosIAC(tdGrupoTerminosIACs)
                    If objGrupoTerminosIACs IsNot Nothing AndAlso objGrupoTerminosIACs.Count > 0 Then
                        objGrupoTerminosIAC = objGrupoTerminosIACs.FirstOrDefault
                        TerminosIACPosibles.Add(objGrupoTerminosIAC)
                    Else
                        Return Nothing
                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return objGrupoTerminosIAC
        End Function

    End Class

End Namespace