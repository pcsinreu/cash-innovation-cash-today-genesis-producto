Imports Prosegur.Genesis.Comon

''' <summary>
''' Classe que possue todos os métodos de extension utilizados para converter uma entidade do legado para a nova entidade de unificação Genesis
''' </summary>
''' <remarks></remarks>
Public Module ConversorTermino

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIac) As Clases.Termino

        Dim terminoUnificacion As New Clases.Termino() With
            {
                .Identificador = Nothing,
                .Codigo = entidadeLegado.Codigo,
                .Descripcion = entidadeLegado.Descripcion,
                .Observacion = entidadeLegado.Observacion,
                .Longitud = entidadeLegado.Longitud,
                .MostrarDescripcionConCodigo = entidadeLegado.MostrarCodigo,
                .TieneValoresPosibles = entidadeLegado.ValoresPosibles,
                .AceptarDigitacion = entidadeLegado.AceptarDigitiacion,
                .EstaActivo = entidadeLegado.Vigente,
                .Formato = New Clases.Formato() With {
                    .Codigo = entidadeLegado.CodigoFormato,
                    .Descripcion = entidadeLegado.DescripcionFormato
                },
                .Mascara = New Clases.Mascara() With {
                    .ExpresionRegular = entidadeLegado.ExpRegularMascara,
                    .Descripcion = entidadeLegado.DescripcionMascara,
                    .Codigo = entidadeLegado.CodigoMascara
                },
                .AlgoritmoValidacion = New Clases.AlgoritmoValidacion() With {
                    .Codigo = entidadeLegado.CodigoAlgoritmo,
                    .Descripcion = entidadeLegado.DescripcionAlgoritmo
                }
            }

        Return terminoUnificacion

    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac) As Clases.Termino

        Dim terminoUnificacion As New Clases.Termino() With
            {
                .Codigo = entidadeLegado.Codigo,
                .Descripcion = entidadeLegado.Descripcion,
                .Observacion = entidadeLegado.Observacion,
                .EstaActivo = entidadeLegado.Vigente
            }

        Return terminoUnificacion

    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac) As Clases.Termino

        Dim terminoUnificacion As New Clases.Termino() With
            {
                .Identificador = Nothing,
                .Codigo = entidadeLegado.CodigoTermino,
                .Descripcion = entidadeLegado.DescripcionTermino,
                .Observacion = entidadeLegado.ObservacionesTermino,
                .Longitud = entidadeLegado.LongitudTermino,
                .MostrarDescripcionConCodigo = entidadeLegado.MostarCodigo,
                .TieneValoresPosibles = entidadeLegado.AdmiteValoresPosibles,
                .AceptarDigitacion = entidadeLegado.esProtegido,
                .EstaActivo = entidadeLegado.VigenteTermino,
                .Formato = New Clases.Formato() With {
                    .Codigo = entidadeLegado.CodigoFormatoTermino,
                    .Descripcion = entidadeLegado.DescripcionFormatoTermino
                },
                .Mascara = New Clases.Mascara() With {
                    .ExpresionRegular = entidadeLegado.ExpRegularMascaraTermino,
                    .Descripcion = entidadeLegado.DescripcionMascaraTermino,
                    .Codigo = entidadeLegado.CodigoMascaraTermino
                },
                .AlgoritmoValidacion = New Clases.AlgoritmoValidacion() With {
                    .Codigo = entidadeLegado.CodigoAlgoritmoTermino,
                    .Descripcion = entidadeLegado.DescripcionAlgoritmoTermino
                }
            }

        Return terminoUnificacion

    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Iac.GetIac.TerminosIac) As Clases.Termino

        Dim terminoUnificacion As New Clases.Termino() With
            {
                .Identificador = Nothing,
                .Codigo = entidadeLegado.CodigoTermino,
                .Descripcion = entidadeLegado.DescripcionTermino
            }

        Return terminoUnificacion

    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion) As List(Of Clases.Termino)
        Return entidadeLegado.Select(Function(e) e.GenerarEntidadUnificada).ToList()
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadUnificada(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Iac.GetIac.TerminosIacColeccion) As List(Of Clases.Termino)
        Return entidadeLegado.Select(Function(e) e.GenerarEntidadUnificada).ToList()
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadesUnificadas(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion) As List(Of Clases.Termino)
        Return entidadeLegado.Select(Function(e) e.GenerarEntidadUnificada).ToList()
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function GenerarEntidadesUnificadas(entidadeLegado As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIacColeccion) As List(Of Clases.Termino)
        Return entidadeLegado.Select(Function(e) e.GenerarEntidadUnificada).ToList()
    End Function

End Module
