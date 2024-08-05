Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Text

Namespace Genesis

    Public Class Elemento

        Public Shared Function poblarElementos(ds As DataSet,
                                             Optional ByRef _identificadorDocumento As String = "",
                                             Optional _cuentas As List(Of Clases.Cuenta) = Nothing) As ObservableCollection(Of Clases.Elemento)

            If _cuentas Is Nothing Then

                Dim _identificadorDelegacion As New Dictionary(Of String, String)
                Dim _delegaciones As List(Of Clases.Delegacion) = GenesisSaldos.Documento.CargarDelegacion(ds)
                Dim _tipoSectores As List(Of Clases.TipoSector) = GenesisSaldos.Documento.CargarTipoSector(ds)
                Dim _plantas As List(Of Clases.Planta) = GenesisSaldos.Documento.CargarPlanta(ds, _identificadorDelegacion, _tipoSectores)
                Dim _sectores As List(Of Clases.Sector) = GenesisSaldos.Documento.CargarSector(ds, _identificadorDelegacion, _delegaciones, _tipoSectores, _plantas)

                _cuentas = New List(Of Clases.Cuenta)
                _cuentas = GenesisSaldos.Documento.CargarCuenta(ds)

            End If

            Dim _elementos As ObservableCollection(Of Clases.Elemento) = Nothing

            If ds.Tables.Contains("ele_rc_elementos") AndAlso ds.Tables("ele_rc_elementos").Rows.Count > 0 AndAlso _
                ((ds.Tables.Contains("ele_rc_cuentas") AndAlso ds.Tables("ele_rc_cuentas").Rows.Count > 0 AndAlso _
                ds.Tables.Contains("ele_rc_carac_tipo_sector") AndAlso ds.Tables("ele_rc_carac_tipo_sector").Rows.Count > 0) OrElse _
                (_cuentas IsNot Nothing AndAlso _cuentas.Count > 0)) Then

                Dim dtElementos As DataTable = ds.Tables("ele_rc_elementos")

                Dim result = dtElementos.Select("C_OID_CONTENEDOR <> ''")

                If result IsNot Nothing AndAlso result.Count > 0 Then
                    Dim _contenedores As ObservableCollection(Of Clases.Contenedor) = Genesis.Contenedor.poblarContenedores(ds, _identificadorDocumento, _cuentas)
                    If _contenedores IsNot Nothing Then
                        _elementos = New ObservableCollection(Of Clases.Elemento)
                        _contenedores.Foreach(Sub(r) _elementos.Add(r))
                    End If
                Else
                    Dim _remesas As ObservableCollection(Of Clases.Remesa) = Genesis.Remesa.poblarRemesas(ds, _identificadorDocumento, _cuentas)
                    If _remesas IsNot Nothing Then
                        _elementos = New ObservableCollection(Of Clases.Elemento)
                        _remesas.Foreach(Sub(r) _elementos.Add(r))
                    End If
                End If

            End If

            Return _elementos
        End Function

    End Class

End Namespace

