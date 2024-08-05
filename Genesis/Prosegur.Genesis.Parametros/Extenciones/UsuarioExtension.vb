Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin

Namespace Extenciones

    Public Module UsuarioExtension

        <Runtime.CompilerServices.Extension()>
        Public Function GetPermisos(usuario As Usuario) As List(Of Permiso)
            Dim retorno As New List(Of Permiso)()
            If usuario.Continentes IsNot Nothing AndAlso usuario.Continentes.Count > 0 Then
                Dim continente = usuario.Continentes(0)
                If continente.Paises IsNot Nothing AndAlso continente.Paises.Count > 0 Then
                    Dim pais = continente.Paises(0)
                    If pais.Delegaciones IsNot Nothing AndAlso pais.Delegaciones.Count > 0 Then
                        Dim delegacion = pais.Delegaciones(0)
                        If TypeOf delegacion Is DelegacionPlanta Then
                            If DirectCast(delegacion, DelegacionPlanta).Plantas IsNot Nothing AndAlso DirectCast(delegacion, DelegacionPlanta).Plantas.Count > 0 Then
                                Dim planta = DirectCast(delegacion, DelegacionPlanta).Plantas(0)
                                If planta.TiposSectores IsNot Nothing AndAlso planta.TiposSectores.Count > 0 Then
                                    Dim tipoSector = planta.TiposSectores(0)
                                    If tipoSector.Permisos IsNot Nothing AndAlso tipoSector.Permisos.Count > 0 Then
                                        retorno.AddRange(tipoSector.Permisos)
                                    End If
                                End If
                            End If
                        Else
                            If delegacion.Sectores IsNot Nothing AndAlso delegacion.Sectores.Count > 0 Then
                                Dim sector = delegacion.Sectores(0)
                                If sector.Permisos IsNot Nothing AndAlso sector.Permisos.Count > 0 Then
                                    retorno.AddRange(sector.Permisos)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            Return retorno
        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function GetRoles(usuario As Usuario) As List(Of Role)
            Dim retorno As New List(Of Role)()
            If usuario.Continentes IsNot Nothing AndAlso usuario.Continentes.Count > 0 Then
                Dim continente = usuario.Continentes(0)
                If continente.Paises IsNot Nothing AndAlso continente.Paises.Count > 0 Then
                    Dim pais = continente.Paises(0)
                    If pais.Delegaciones IsNot Nothing AndAlso pais.Delegaciones.Count > 0 Then
                        Dim delegacion = pais.Delegaciones(0)
                        If delegacion.Sectores IsNot Nothing AndAlso delegacion.Sectores.Count > 0 Then
                            Dim sector = delegacion.Sectores(0)
                            If sector.Roles IsNot Nothing AndAlso sector.Roles.Count > 0 Then
                                retorno.AddRange(sector.Roles)
                            End If
                        End If
                    End If
                End If
            End If
            Return retorno
        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function GetCodigoIsoDivisaLocal(usuario As Usuario) As String
            Dim retorno As String = Nothing
            If usuario.Continentes IsNot Nothing AndAlso usuario.Continentes.Count > 0 Then
                Dim continente = usuario.Continentes(0)
                If continente.Paises IsNot Nothing AndAlso continente.Paises.Count > 0 Then
                    Dim pais = continente.Paises(0)
                    retorno = pais.CodigoISODivisa
                End If
            End If
            Return retorno
        End Function
    End Module

End Namespace