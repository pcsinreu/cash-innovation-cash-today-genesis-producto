#Nombre: CI.Build_ComprimirArchivosParaArtifacts.ps1
#Descripción: Script encargado de preparar los ficheros ZIP con los artefactos auxiliares necesarios en pasos posteriores.
#Version: 1.0
#Parámetros de entrada:
#       0: BUILD_REPOSITORY_LOCALPATH
#       1: BUILD_ARTIFACTSTAGINGDIRECTORY
#       2: BUILD_BUILDNUMBER
#       3: 01_FixedMajorNumber
#       4: 02_FixedMinorNumber
#Parámetros de Salida:
#      0: Ejecución correcta
#      1: Ejecución incorrecta
#Extensión del Script: N/A

$LocalFolder=$Env:BUILD_REPOSITORY_LOCALPATH
$LocalArtifactory=$Env:BUILD_ARTIFACTSTAGINGDIRECTORY
$BuildNumber=$Env:BUILD_BUILDNUMBER
$AppVersion="$Env:01_FixedMajorNumber.$Env:02_FixedMinorNumber"

try{   
    
    
    if(Test-Path "$LocalFolder\$BuildNumber\Sitios\Web"){ 
        Write-Host "### Compress files Web ###"
            Compress-Archive -Path "$LocalFolder\$BuildNumber\Sitios\Web\*" -DestinationPath "$LocalArtifactory\Prosegur.Genesis.Web.$AppVersion.zip"
            if  ($LastExitCode -eq "1" ){
                throw "Preparación de binarios auxiliares ERROR - Web"
            }
        Write-Host "### End compress files Web ###"
    }
    

    if(Test-Path "$LocalFolder\$BuildNumber\Sitios\IAC"){ 
        Write-Host "### Compress files IAC ###"
            Compress-Archive -Path "$LocalFolder\$BuildNumber\Sitios\IAC\*" -DestinationPath "$LocalArtifactory\Prosegur.Genesis.IAC.$AppVersion.zip"
            if  ($LastExitCode -eq "1" ){
                throw "Preparación de binarios auxiliares ERROR - IAC"
            }
        Write-Host "### End compress files IAC ###"
    }
    
    if(Test-Path "$LocalFolder\$BuildNumber\Sitios\NuevoSaldos"){ 
        Write-Host "### Compress files NuevoSaldos ###"
            Compress-Archive -Path "$LocalFolder\$BuildNumber\Sitios\NuevoSaldos\*" -DestinationPath "$LocalArtifactory\Prosegur.Genesis.NuevoSaldos.$AppVersion.zip"
            if  ($LastExitCode -eq "1" ){
                throw "Preparación de binarios auxiliares ERROR - NuevoSaldos"
            }
        Write-Host "### End compress files NuevoSaldos ###"
    }

    if(Test-Path "$LocalFolder\$BuildNumber\Sitios\Reportes"){ 
        Write-Host "### Compress files Reportes ###"
            Compress-Archive -Path "$LocalFolder\$BuildNumber\Sitios\Reportes\*" -DestinationPath "$LocalArtifactory\Prosegur.Genesis.Reportes.$AppVersion.zip"
            if  ($LastExitCode -eq "1" ){
                throw "Preparación de binarios auxiliares ERROR - Reportes"
            }
        Write-Host "### End compress files Reportes ###"
    }

    if(Test-Path "$LocalFolder\$BuildNumber\Sitios\Servicio"){ 
        Write-Host "### Compress files Servicio ###"
            Compress-Archive -Path "$LocalFolder\$BuildNumber\Sitios\Servicio\*" -DestinationPath "$LocalArtifactory\Prosegur.Genesis.Servicio.$AppVersion.zip"
            if  ($LastExitCode -eq "1" ){
                throw "Preparación de binarios auxiliares ERROR - Servicio"
            }
        Write-Host "### End compress files Servicio ###"
    }

    if(Test-Path "$LocalFolder\$BuildNumber\Sitios\ConsultaLocal"){ 
        Write-Host "### Compress files ConsultaLocal ###"
            Compress-Archive -Path "$LocalFolder\$BuildNumber\Sitios\ConsultaLocal\*" -DestinationPath "$LocalArtifactory\Prosegur.Genesis.ConsultaLocal.$AppVersion.zip"
            if  ($LastExitCode -eq "1" ){
                throw "Preparación de binarios auxiliares ERROR - ConsultaLocal"
            }
        Write-Host "### End compress files ConsultaLocal ###"
    }

    if(Test-Path "$LocalFolder\$BuildNumber\RS"){ 
        Write-Host "### Compress files Reportes ###"
            Compress-Archive -Path "$LocalFolder\$BuildNumber\RS\*" -DestinationPath "$LocalArtifactory\Prosegur.Genesis.RS.$AppVersion.zip"
            if  ($LastExitCode -eq "1" ){
                throw "Preparación de binarios auxiliares ERROR - Reportes"
            }
        Write-Host "### End compress files Reportes ###"
    }

    exit 0
}
Catch{
    Write-Output "ERROR: Error en la preparación de binarios: $_"
    Write-Host $_.Exception.Message
    exit 1
}