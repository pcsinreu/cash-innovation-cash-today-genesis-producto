#Nombre: CI.Build_ComprimirArchivosParaArtifacts.ps1
#Descripción: Script encargado de preparar los ficheros ZIP para exportar a Veracode.
#Version: 1.0
#Parámetros de entrada:
#       0: BUILD_REPOSITORY_LOCALPATH
#       1: BUILD_ARTIFACTSTAGINGDIRECTORY
#       2: BUILD_BUILDNUMBER
#Parámetros de Salida:
#      0: Ejecución correcta
#      1: Ejecución incorrecta
#Extensión del Script: N/A

$LocalFolder=$Env:BUILD_REPOSITORY_LOCALPATH
$LocalArtifactory=$Env:BUILD_ARTIFACTSTAGINGDIRECTORY
$BuildNumber=$Env:BUILD_BUILDNUMBER
$TargetDir="Veracode"
$TargetDirPath="$LocalArtifactory\$TargetDir"
$ProductName="GenesisProductoInstaller_$BuildNumber.zip"

try{   
    
    
    if(Test-Path "$LocalArtifactory\"){
        Write-Host "### Create Target Folder: $TargetDirPath ###"
        New-Item -Path "$TargetDirPath" -ItemType Directory

        Write-Host "### Compress and move files to $TargetDirPath ###"
        #Invoke-Expression -Command "Copy-Item -Path `"$LocalArtifactory\$ProductName`" -Destination `"$TargetDirPath\`" -Recurse -Force"
        Get-Childitem -Path "$LocalArtifactory\"
        
        Write-Host "### Expand and move files to $TargetDirPath ###"
        Expand-Archive "$LocalArtifactory\$ProductName" -DestinationPath "$TargetDirPath\"
        Get-Childitem -Path "$TargetDirPath\"
        
        
        if  ($LastExitCode -eq "1" ){
            throw "Preparación de binarios auxiliares ERROR - Web"
        }
        Write-Host "### End compressing files ###"
    }

    Write-Host "### Files archived and ready for scanning ###"

    exit 0
}
Catch{
    Write-Output "ERROR: Error en la preparación de binarios: $_"
    Write-Host $_.Exception.Message
    exit 1
}