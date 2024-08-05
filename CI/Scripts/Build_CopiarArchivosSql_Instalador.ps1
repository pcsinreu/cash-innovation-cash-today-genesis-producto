#Nombre: Build_CopiarArchivosSql_Instalador.ps1
#Descripción: Script encargado de copiar los archivos del instalador y los archivos sql utilizados por el instalador.
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

try{   
    Write-Host "### Copia de archivos sql ###"
    Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\$BuildNumber\*.sql`" -Destination `"$LocalArtifactory\`" -Recurse -Force"

    Write-Host "### Copia de archivos del instalador ###"
    Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\Instalador\*`" -Destination `"$LocalArtifactory\`" -Recurse -Force"

    exit 0
}
Catch{
    Write-Output "ERROR: Error en la copia de archivos: $_"
    Write-Host $_.Exception.Message
    exit 1
}