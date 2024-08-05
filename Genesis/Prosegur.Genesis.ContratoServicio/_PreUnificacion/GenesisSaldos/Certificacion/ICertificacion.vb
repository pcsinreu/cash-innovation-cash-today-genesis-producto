Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos

Namespace GenesisSaldos.Certificacion

    Public Interface ICertificacion

        Function GenerarCertificado(Peticion As GenesisSaldos.Certificacion.GenerarCertificado.Peticion) As GenesisSaldos.Certificacion.GenerarCertificado.Respuesta

        Function ValidarCertificacion(Peticion As GenesisSaldos.Certificacion.ValidarCertificacion.Peticion) As GenesisSaldos.Certificacion.ValidarCertificacion.Respuesta

        Function ObtenerCertificado(Peticion As GenesisSaldos.Certificacion.ObtenerCertificado.Peticion) As GenesisSaldos.Certificacion.ObtenerCertificado.Respuesta

        Function ObtenerNivelSaldos(Peticion As GenesisSaldos.Certificacion.ObtenerNivelSaldos.Peticion) As GenesisSaldos.Certificacion.ObtenerNivelSaldos.Respuesta

        Function GenerarCodigoCertificado(Peticion As GenesisSaldos.Certificacion.GenerarCodigoCertificado.Peticion) As GenesisSaldos.Certificacion.GenerarCodigoCertificado.Respuesta

        Function RecuperarFiltrosCertificado(Peticion As GenesisSaldos.Certificacion.RecuperarFiltrosCertificado.Peticion) As GenesisSaldos.Certificacion.RecuperarFiltrosCertificado.Respuesta


        Function Test() As Genesis.ContractoServicio.Test.Respuesta

    End Interface

End Namespace