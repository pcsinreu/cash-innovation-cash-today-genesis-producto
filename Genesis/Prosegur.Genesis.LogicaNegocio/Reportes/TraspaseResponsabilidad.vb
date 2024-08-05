Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Transactions
Imports System.Data
Imports Prosegur.Genesis.Comon
Imports AccesoDatosIAC = Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports ContractoServicioIAC = Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

Namespace Reportes
    Public Class TraspaseResponsabilidad

        Public Shared Function GrabarTraspaseResponsabilidad(objPeticion As Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadPeticion) As Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadRespuesta

            Dim respuesta As New Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadRespuesta

            Try
                'Verifica se foi informado a recpecionRuta
                If objPeticion Is Nothing OrElse objPeticion.RecepcionesRuta Is Nothing OrElse objPeticion.RecepcionesRuta.Count = 0 Then
                    'Ruta não informada
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("RECEPCION_RUTA_NAO_INFORMADA"))
                End If

                If objPeticion.RecepcionesRuta IsNot Nothing AndAlso objPeticion.RecepcionesRuta.Count > 0 Then

                    Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted})

                        'Busca formatos
                        Dim identificadoresRemesa As List(Of String) = objPeticion.RecepcionesRuta.Where(Function(a) a.IdentificadorRemesa IsNot Nothing).Select(Function(a) a.IdentificadorRemesa).Distinct().ToList()
                        Dim dtTipoFormato = AccesoDatos.Genesis.TipoFormato.ObtenerTipoServicioDeElementos(identificadoresRemesa)
                        Dim lstCodEmbalaje As New List(Of String)
                        If objPeticion.GenerarPrecintoModuloAutomatico Then
                            Dim lstModulos = AccesoDatosIAC.Modulo.GetModulo(New ContractoServicioIAC.Modulo.GetModulo.Peticion With {.BolActivo = True})
                            If lstModulos IsNot Nothing AndAlso lstModulos.Modulos IsNot Nothing AndAlso lstModulos.Modulos.Count > 0 Then
                                lstCodEmbalaje = lstModulos.Modulos.Where(Function(a) Not String.IsNullOrEmpty(a.CodEmbalaje)).Select(Function(b) b.CodEmbalaje).Distinct().ToList()
                            End If
                        End If
                        ' Preenche valores Remesa
                        For Each objRecepRuta In objPeticion.RecepcionesRuta

                            'objRecepRuta.Formato = cargarTipoFormato(dtTipoFormato, objRecepRuta.IdentificadorRemesa, objRecepRuta.IdentificadorBulto, Nothing)

                            If Not String.IsNullOrEmpty(objRecepRuta.Formato) AndAlso lstCodEmbalaje.Contains(objRecepRuta.Formato) Then
                                objRecepRuta.Precinto = String.Empty
                            End If

                        Next

                        For Each objRecepcionesRuta In objPeticion.RecepcionesRuta
                            AccesoDatos.Reportes.TraspaseResponsabilidad.GrabarTraspaseResponsabilidad(objRecepcionesRuta)
                        Next

                        ' se não deu erro então realiza a transação.
                        transaction.Complete()

                    End Using

                End If

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Descricao)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)
            End Try

            Return respuesta

        End Function

        Private Shared Function cargarTipoFormato(ByRef dtTipoFormato As DataTable, ByRef identificadorRemesa As String, ByRef identificadorBulto As String, ByRef identificadorParcial As String) As String

            Dim _tipoFormato As String = String.Empty

            If dtTipoFormato IsNot Nothing AndAlso Not String.IsNullOrEmpty(identificadorRemesa) AndAlso Not String.IsNullOrEmpty(identificadorBulto) Then

                ' Crea filtro
                Dim consulta As String = "OID_REMESA = '" & identificadorRemesa & "'"
                If Not String.IsNullOrEmpty(identificadorBulto) Then
                    consulta &= " AND OID_BULTO_EXTERNO = '" & identificadorBulto & "'"
                Else
                    consulta &= " AND OID_BULTO IS NULL "
                End If
                If Not String.IsNullOrEmpty(identificadorParcial) Then
                    consulta &= " AND OID_PARCIAL = '" & identificadorParcial & "'"
                Else
                    consulta &= " AND OID_PARCIAL IS NULL "
                End If

                Dim _tipos = dtTipoFormato.Select(consulta)

                If _tipos IsNot Nothing AndAlso _tipos.Count > 0 Then

                    _tipoFormato = Util.AtribuirValorObj(_tipos(0)("DES_VALOR"), GetType(String))

                End If

            End If

            Return _tipoFormato

        End Function

    End Class
End Namespace

