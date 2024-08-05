Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Contenedor

        Public Shared Function poblarContenedores(ds As DataSet,
                                             Optional ByRef _identificadorDocumento As String = "",
                                             Optional _cuentas As List(Of Clases.Cuenta) = Nothing) As ObservableCollection(Of Clases.Contenedor)

            If _cuentas Is Nothing Then

                Dim _identificadorDelegacion As New Dictionary(Of String, String)
                Dim _delegaciones As List(Of Clases.Delegacion) = GenesisSaldos.Documento.CargarDelegacion(ds)
                Dim _tipoSectores As List(Of Clases.TipoSector) = GenesisSaldos.Documento.CargarTipoSector(ds)
                Dim _plantas As List(Of Clases.Planta) = GenesisSaldos.Documento.CargarPlanta(ds, _identificadorDelegacion, _tipoSectores)
                Dim _sectores As List(Of Clases.Sector) = GenesisSaldos.Documento.CargarSector(ds, _identificadorDelegacion, _delegaciones, _tipoSectores, _plantas)

                _cuentas = New List(Of Clases.Cuenta)
                _cuentas = GenesisSaldos.Documento.CargarCuenta(ds)

            End If

            Dim _contenedores As ObservableCollection(Of Clases.Contenedor) = Nothing

            If ds.Tables.Contains("ele_rc_elementos") AndAlso ds.Tables("ele_rc_elementos").Rows.Count > 0 AndAlso _
                ((ds.Tables.Contains("ele_rc_cuentas") AndAlso ds.Tables("ele_rc_cuentas").Rows.Count > 0 AndAlso _
                ds.Tables.Contains("ele_rc_carac_tipo_sector") AndAlso ds.Tables("ele_rc_carac_tipo_sector").Rows.Count > 0) OrElse _
                (_cuentas IsNot Nothing AndAlso _cuentas.Count > 0)) Then

                Dim dtContenedores As DataTable = ds.Tables("ele_rc_elementos")

                For Each rowContenedor In dtContenedores.Rows

                    Dim _identificadorContenedor As String = Util.AtribuirValorObj(rowContenedor("C_OID_CONTENEDOR"), GetType(String))
                    Dim _identificadorDocumentoContenedor As String = Util.AtribuirValorObj(rowContenedor("C_OID_DOCUMENTO_DE"), GetType(String))

                    If String.IsNullOrEmpty(_identificadorDocumento) OrElse (Not String.IsNullOrEmpty(_identificadorDocumento) AndAlso _identificadorDocumento = _identificadorDocumentoContenedor) Then

                        If _contenedores Is Nothing Then
                            _contenedores = New ObservableCollection(Of Clases.Contenedor)
                        End If

                        If _contenedores.FirstOrDefault(Function(c) c.Identificador = _identificadorContenedor AndAlso c.IdentificadorDocumento = _identificadorDocumentoContenedor) Is Nothing Then

                            Dim _contenedor As New Clases.Contenedor
                            Dim objValoresTermino As List(Of KeyValuePair(Of String, String)) = Nothing
                            Dim IdentificadorRemesaOrigen As String = String.Empty

                            With _contenedor

                                'C_SECTOR,
                                'C_OID_GRUPO_DOCUMENTO,
                                'C_BOL_PACK_MODULAR,
                                'C_COD_TIPO_ELEMENTO,
                                'C_OID_CONTENEDOR_MAYOR_NIVEL,

                                .Identificador = _identificadorContenedor
                                .Codigo = Util.AtribuirValorObj(rowContenedor("C_COD_CONTENEDOR"), GetType(String))
                                .AgrupaContenedor = Util.AtribuirValorObj(rowContenedor("C_BOL_AGRUPA_CONTENEDOR"), GetType(Boolean))
                                .TipoContenedor = New Clases.TipoContenedor With {.Identificador = Util.AtribuirValorObj(rowContenedor("C_OID_TIPO_CONTENEDOR"), GetType(String))}
                                .FechaVencimiento = Util.AtribuirValorObj(rowContenedor("C_FEC_VENCIMIENTO"), GetType(DateTime))
                                .Cuenta = _cuentas.FirstOrDefault(Function(c) c.Identificador = rowContenedor("C_OID_CUENTA"))
                                .CuentaSaldo = _cuentas.FirstOrDefault(Function(c) c.Identificador = If(rowContenedor.Table.Columns.Contains("C_OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowContenedor("C_OID_CUENTA_SALDO"), GetType(String))), rowContenedor("C_OID_CUENTA_SALDO"), Nothing))
                                .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoContenedor)(Util.AtribuirValorObj(rowContenedor("C_COD_ESTADO"), GetType(String)))
                                .FechaHoraCreacion = Util.AtribuirValorObj(rowContenedor("C_GMT_CREACION"), GetType(DateTime))
                                .FechaHoraModificacion = Util.AtribuirValorObj(rowContenedor("C_GMT_MODIFICACION"), GetType(DateTime))
                                .IdentificadorDocumento = Util.AtribuirValorObj(rowContenedor("C_OID_DOCUMENTO_DE"), GetType(String))
                                .UsuarioCreacion = Util.AtribuirValorObj(rowContenedor("C_DES_USUARIO_CREACION"), GetType(String))
                                .UsuarioModificacion = Util.AtribuirValorObj(rowContenedor("C_DES_USUARIO_MODIFICACION"), GetType(String))
                                .EstadoDocumentoElemento = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumentoElemento)(Util.AtribuirValorObj(rowContenedor("C_COD_ESTADO_DOCXELEMENTO"), GetType(String)))

                                If (Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowContenedor("C_OID_CONTENEDOR_PADRE"), GetType(String)))) Then
                                    .ElementoPadre = New Clases.Remesa With {.Identificador = Util.AtribuirValorObj(rowContenedor("C_OID_CONTENEDOR_PADRE"), GetType(String))}
                                End If
                                .Divisas = New ObservableCollection(Of Clases.Divisa)

                                If ds.Tables.Contains("ele_rc_cont_precintos") AndAlso ds.Tables("ele_rc_cont_precintos").Rows.Count > 0 Then

                                    Dim dtPrecintos As DataTable = ds.Tables("ele_rc_cont_precintos")
                                    Dim result = dtPrecintos.Select("OID_CONTENEDOR = '" & _identificadorContenedor & "'")

                                    If result IsNot Nothing AndAlso result.Count > 0 Then

                                        .Precintos = New ObservableCollection(Of String)
                                        For Each rowPrecinto In result.CopyToDataTable().Rows
                                            If Util.AtribuirValorObj(rowPrecinto("BOL_PRECINTO_AUTOMATICO"), GetType(Boolean)) Then
                                                .Codigo = Util.AtribuirValorObj(rowPrecinto("COD_PRECINTO"), GetType(String))
                                            Else
                                                .Precintos.Add(Util.AtribuirValorObj(rowPrecinto("COD_PRECINTO"), GetType(String)))
                                            End If
                                        Next

                                    End If
                                    
                                End If
                            End With

                            _contenedores.Add(_contenedor)

                        End If
                    End If

                Next

                'poblarValoresContenedores(_contenedores, ds)

            End If

            Return _contenedores
        End Function


#Region "Consultas"

        ''' <summary>
        ''' Verifica se a remesa está dentro de um contenedor.
        ''' </summary>
        ''' <param name="Remesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function VerificarRemesaTieneContenedor(Remesa As Clases.Remesa) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ContenedorVerificarRemesaTieneContenedor)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, Remesa.Identificador))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        ''' <summary>
        ''' Adiciona os contenedores filhos
        ''' </summary>
        ''' <param name="Contenedores"></param>
        ''' <param name="ContenedorPadre"></param>
        ''' <param name="ContenedorHijo"></param>
        ''' <remarks></remarks>
        Private Shared Sub AdicionarElementoHijo(ByRef Contenedores As List(Of Clases.Contenedor), ByRef ContenedorPadre As Clases.Contenedor, _
                                                 ContenedorHijo As Clases.Contenedor, IdentificadorContenedorPadre As String, AnadirContenedor As Boolean)

            If ContenedorPadre.Elementos IsNot Nothing AndAlso ContenedorPadre.Elementos.Count > 0 Then

                'Verifica se o contenedor corrente é o contenedor que deve ser adicionado o elemento.
                If IdentificadorContenedorPadre = ContenedorPadre.Identificador Then
                    ContenedorPadre.Elementos.Add(ContenedorHijo)
                Else

                    'Percorre a coleção de elementos e verifica qual elemento correto deve ser adicionado o contenedor.
                    For Each elemento In ContenedorPadre.Elementos

                        If elemento.GetType().Equals(GetType(Clases.Contenedor)) Then
                            AdicionarElementoHijo(Contenedores, elemento, ContenedorHijo, IdentificadorContenedorPadre, False)
                        End If

                    Next

                End If

            Else
                ContenedorPadre.Elementos = New ObservableCollection(Of Clases.Elemento)
                ContenedorPadre.Elementos.Add(ContenedorHijo)
            End If

            'Adiciona o pai na coleção de contenedores.
            If AnadirContenedor Then Contenedores.Add(ContenedorPadre)

        End Sub

        ''' <summary>
        ''' Recupera o contenedor filho corrente
        ''' </summary>
        ''' <param name="Contenedores"></param>
        ''' <param name="Contenedor"></param>
        ''' <param name="IdentificadorContenedor"></param>
        ''' <remarks></remarks>
        Private Shared Sub RecuperarContenedorHijo(ByRef Contenedores As List(Of Clases.Contenedor), ByRef Contenedor As Clases.Contenedor, _
                                                   IdentificadorContenedor As String)

            Contenedor = (From objCont In Contenedores Where objCont.Identificador = IdentificadorContenedor).FirstOrDefault

            If Contenedor Is Nothing Then

                For Each objContenedor In Contenedores

                    If objContenedor.Elementos IsNot Nothing AndAlso objContenedor.Elementos.Count > 0 Then

                        For Each Elemento In objContenedor.Elementos

                            If Elemento.GetType().Equals(GetType(Clases.Contenedor)) Then

                                If Elemento.Identificador = IdentificadorContenedor Then
                                    Contenedor = DirectCast(Elemento, Clases.Elemento)
                                Else
                                    RecuperarElementoHijo(DirectCast(Elemento, Clases.Contenedor).Elementos, Contenedor, IdentificadorContenedor)
                                End If

                            End If

                        Next

                    End If

                Next

            End If

        End Sub

        ''' <summary>
        ''' Recupera o elemento filho corrente.
        ''' </summary>
        ''' <param name="Elementos"></param>
        ''' <param name="Contenedor"></param>
        ''' <param name="IdentificadorContenedor"></param>
        ''' <remarks></remarks>
        Private Shared Sub RecuperarElementoHijo(ByRef Elementos As ObservableCollection(Of Clases.Elemento), ByRef Contenedor As Clases.Contenedor, _
                                                 IdentificadorContenedor As String)


            If Elementos IsNot Nothing AndAlso Elementos.Count > 0 Then

                For Each Elemento In Elementos

                    If Elemento.GetType().Equals(GetType(Clases.Contenedor)) Then

                        If Elemento.Identificador = IdentificadorContenedor Then
                            Contenedor = DirectCast(Elemento, Clases.Elemento)
                        Else
                            RecuperarElementoHijo(DirectCast(Elemento, Clases.Contenedor).Elementos, Contenedor, IdentificadorContenedor)
                        End If

                    End If

                Next

            End If

        End Sub

#End Region

    End Class

End Namespace