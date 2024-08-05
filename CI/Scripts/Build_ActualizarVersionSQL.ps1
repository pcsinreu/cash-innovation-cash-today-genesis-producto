#Nombre: CI.Build_ActualizarVersionSQL.ps1
#Descripción: Script encargado de remplazar ###VERSION### Y ###VERSION_COMP### en los distintos archivos SQL y unificar los mismos.
#Version: 1.0
#Parámetros de entrada: 
#       0: BUILD_REPOSITORY_LOCALPATH
#       1: BUILD_BUILDNUMBER
#Parámetros de Salida: 
#      0: Ejecución correcta
#      1: Ejecución incorrecta
#Extensión del Script:

$LocalFolder=$Env:BUILD_REPOSITORY_LOCALPATH
$BuildNumber=$Env:BUILD_BUILDNUMBER

#Variables para ScriptsEntrega.sql
$PathEntregaReportes =              "$LocalFolder\Extras\BaseDatos\Entrega\Reportes"
$PathEntregaReportesScript =        "$LocalFolder\Extras\BaseDatos\Entrega\Reportes\ScriptsEntrega.sql"
$PathEntregaGenesis =               "$LocalFolder\Extras\BaseDatos\Entrega\Genesis"
$PathEntregaGenesisScript =         "$LocalFolder\Extras\BaseDatos\Entrega\Genesis\ScriptsEntrega.sql"

#Variables para Procedures.sql
$PathGeralReportes =                "$LocalFolder\Extras\BaseDatos\Geral\Reportes"
$PathGeralReportesScript =          "$LocalFolder\Extras\BaseDatos\Geral\Reportes\Procedures.sql"
$PathGeralGenesis =                 "$LocalFolder\Extras\BaseDatos\Geral\Genesis"
$PathGeralGenesisScript =           "$LocalFolder\Extras\BaseDatos\Geral\Genesis\Procedures.sql"

#Variables para Grants_Synonyms.sql
$PathGrantsSynonymsReportes =       "$LocalFolder\Extras\BaseDatos\Grants_Synonyms\Reportes"
$PathGrantsSynonymsReportesScript = "$LocalFolder\Extras\BaseDatos\Grants_Synonyms\Reportes\Reportes_GeS.sql"
$PathGrantsSynonymsGenesis =        "$LocalFolder\Extras\BaseDatos\Grants_Synonyms\Genesis"
$PathGrantsSynonymsGenesisScript =  "$LocalFolder\Extras\BaseDatos\Grants_Synonyms\Genesis\Genesis_GeS.sql"

#Variables para Genesis.sql y Reportes.sql
$PathGenesisScript            =     "$LocalFolder\Extras\BaseDatos\Genesis.sql"
$PathReportesScript           =     "$LocalFolder\Extras\BaseDatos\Reportes.sql"

$dllReportes = "$LocalFolder\$BuildNumber\Sitios\Reportes\bin\Prosegur.Global.GesEfectivo.Reportes.Web.dll"
$dllWeb = "$LocalFolder\$BuildNumber\Sitios\Web\bin\Prosegur.Genesis.Web.dll"

Try { 
    Write-Output "##### Ejecuta el comando que crea los archivos SQL #####"
        if(Test-Path "$LocalFolder\$BuildNumber\Sitios\Reportes"){
            Write-Output "## Crea los archivos ScriptsEntrega.sql de Reportes ##" 
            
            #Invoke-Expression -Command "& `"$Env:GenesisProcedureVersionNuevo`" $dllReportes $PathEntregaReportes ###VERSION### false ScriptsEntrega "
            #Start-Process -NoNewWindow -FilePath $Env:GenesisProcedureVersionNuevo -ArgumentList $dllReportes,$PathEntregaReportes,'###VERSION###', 'false', 'ScriptsEntrega'
            [System.Diagnostics.Process]::Start("$Env:GenesisProcedureVersionNuevo","$dllReportes $PathEntregaReportes `"`" false ScriptsEntrega")
    
            Write-Output "## Crea los archivos Procedures.sql de Reportes ##" 
            #Invoke-Expression -Command "& `"$Env:GenesisProcedureVersionNuevo`" $dllReportes $PathGeralReportes ###VERSION### false Procedures "
            #Start-Process -NoNewWindow -FilePath $Env:GenesisProcedureVersionNuevo -ArgumentList $dllReportes,$PathGeralReportes,'###VERSION###', 'false', 'Procedures'
            [System.Diagnostics.Process]::Start("$Env:GenesisProcedureVersionNuevo","$dllReportes $PathGeralReportes `"`" false Procedures")

            Write-Output "## Crea los archivos Reportes_GeS.sql de Reportes ##" 
            #Invoke-Expression -Command "& `"$Env:GenesisProcedureVersionNuevo`" $dllReportes $PathGrantsSynonymsReportes ###VERSION### false Reportes_GeS "
            #Start-Process -NoNewWindow -FilePath $Env:GenesisProcedureVersionNuevo -ArgumentList $dllReportes,$PathGrantsSynonymsReportes,'###VERSION###', 'false', 'Reportes_GeS'
            [System.Diagnostics.Process]::Start("$Env:GenesisProcedureVersionNuevo","$dllReportes $PathGrantsSynonymsReportes `"`" false Reportes_GeS")

        }
        else {
            Write-Output "## No encuentra la carpeta $LocalFolder\$BuildNumber\Sitios\Reportes ##" 
        }
        if(Test-Path "$LocalFolder\$BuildNumber\Sitios\Web"){ 
            Write-Output "## Crea los archivos ScriptsEntrega.sql de Genesis ##" 
            #Invoke-Expression -Command "& `"$Env:GenesisProcedureVersionNuevo`" $dllWeb $PathEntregaGenesis ###VERSION### false ScriptsEntrega "
            #Start-Process -NoNewWindow -FilePath $Env:GenesisProcedureVersionNuevo -ArgumentList $dllWeb,$PathEntregaGenesis,'###VERSION###', 'false', 'ScriptsEntrega'
            [System.Diagnostics.Process]::Start("$Env:GenesisProcedureVersionNuevo","$dllWeb $PathEntregaGenesis `"`" false ScriptsEntrega")

            Write-Output "## Crea los archivos Procedures.sql de Genesis ##" 
            #Invoke-Expression -Command "& `"$Env:GenesisProcedureVersionNuevo`" $dllWeb $PathGeralGenesis ###VERSION### false Procedures "
            #Start-Process -NoNewWindow -FilePath $Env:GenesisProcedureVersionNuevo -ArgumentList $dllWeb,$PathGeralGenesis,'###VERSION###', 'false', 'Procedures'
            [System.Diagnostics.Process]::Start("$Env:GenesisProcedureVersionNuevo","$dllWeb $PathGeralGenesis `"`" false Procedures")

            Write-Output "## Crea los archivos Genesis_GeS.sql de Genesis ##" 
            #Invoke-Expression -Command "& `"$Env:GenesisProcedureVersionNuevo`" $dllWeb $PathGrantsSynonymsGenesis ###VERSION### false Genesis_GeS "
            #Start-Process -NoNewWindow -FilePath $Env:GenesisProcedureVersionNuevo -ArgumentList $dllWeb,$PathGrantsSynonymsGenesis,'###VERSION###', 'false', 'Genesis_GeS'
            [System.Diagnostics.Process]::Start("$Env:GenesisProcedureVersionNuevo","$dllWeb $PathGrantsSynonymsGenesis `"`" false Genesis_GeS")

        }
        else {
            Write-Output "## No encuentra la carpeta $LocalFolder\$BuildNumber\Sitios\Web ##" 
        }

    Start-Sleep -Seconds 30

    #Copiamos los archivos SQL generados

    Write-Output "##### Prepara y copia para el directorio destino el archivo Genesis.sql #####"
        if (Test-Path $PathGenesisScript){
            #Eliminamos el archivo en caso de que exista
            Remove-Item -Force -Path $PathGenesisScript
        }
    Write-Output "## Prepara el archivo Genesis.sql ##"
        Get-Item $PathEntregaGenesisScript | Get-content | out-file $PathGenesisScript -Append
        Get-Item $PathGeralGenesisScript | Get-content | out-file $PathGenesisScript -Append
    Write-Output "## Copia el archivo Genesis.sql al directorio de destino ##"
        Invoke-Expression -Command "Copy-Item -Path `"$PathGenesisScript`" -Destination `"$LocalFolder\$BuildNumber\`" -Recurse -Force"

    Write-Output "##### Prepara y copia para el directorio destino el archivo Reportes.sql #####"
        if (Test-Path $PathReportesScript){
            #Eliminamos el archivo en caso de que exista
            Remove-Item -Force -Path $PathReportesScript
        }
    Write-Output "## Prepara el archivo Reportes.sql ##"
        Get-Item $PathEntregaReportesScript | Get-content | out-file $PathReportesScript -Append
        Get-Item $PathGeralReportesScript | Get-content | out-file $PathReportesScript -Append
    Write-Output "## Copia el archivo Reportes.sql al directorio de destino ##"
        Invoke-Expression -Command "Copy-Item -Path `"$PathReportesScript`" -Destination `"$LocalFolder\$BuildNumber\`" -Recurse -Force"

    Write-Output "##### Comienza copia para el directorio destino de los archivos Grants_Synonyms.sql #####"
        if(Test-Path $PathGrantsSynonymsReportesScript){
            Write-Output "## Copia para el directorio destino del archivo Reportes_GeS.sql ##"
            Invoke-Expression -Command "Copy-Item -Path `"$PathGrantsSynonymsReportesScript`" -Destination `"$LocalFolder\$BuildNumber\`" -Recurse -Force"
        }
        else {
            Write-Output "## No encuentra el archivo $PathGrantsSynonymsReportesScript ##"
        }
        if(Test-Path $PathGrantsSynonymsGenesisScript){
            Write-Output "## Copia para el directorio destino del archivo Genesis_GeS.sql ##"
            Invoke-Expression -Command "Copy-Item -Path `"$PathGrantsSynonymsGenesisScript`" -Destination `"$LocalFolder\$BuildNumber\`" -Recurse -Force"
        }
        else {
            Write-Output "## No encuentra el archivo $PathGrantsSynonymsGenesisScript ##"
        }

    Write-Output "##### Copia para el directorio destino de la carpeta Reportes #####"
        if(Test-Path "$LocalFolder\Extras\Reportes"){
            Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\Extras\Reportes`" -Destination `"$LocalFolder\$BuildNumber\RS\`" -Recurse -Force"
        }

    exit 0
}
Catch {
    Write-Output "ERROR: Error al actualizar Version a archivos SQL: $_"
    Write-Host $_.Exception.Message 
    exit 1
}
    
