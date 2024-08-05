#Nombre: CI.Build_UpdateVersion.ps1
#Descripción: Script encargado de actualizar versión del GlobalAssemblyInfo.
#Version: 1.0
#Parámetros de entrada:
#       0: GlobalAssemblyInfo
#       1: 01_FixedMajorNumber
#       2: 02_FixedMinorNumber
#       3: 03_FixedBuildNumber
#       4: 04_FixedRevisionNumber
#       5: BUILD_REPOSITORY_LOCALPATH
#Parámetros de Salida:
#      0: Ejecución correcta
#      1: Ejecución incorrecta
#Extensión del Script: N/A


$GlobalAssemblyInfo = $Env:GlobalAssemblyInfo

$AssemblyMajorVersion = $Env:01_FixedMajorNumber
$AssemblyMinorVersion = $Env:02_FixedMinorNumber
$AssemblyRevision = $Env:03_FixedRevisionNumber
$AssemblyBuildNumber = $Env:04_FixedBuildNumber

$Date = Get-Date -Format "yyMMdd"
$AssemblyProductVersion = "$AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$Date$AssemblyBuildNumber"

$LocalFolder="$Env:BUILD_REPOSITORY_LOCALPATH"


Try {
    
    Write-Output "Cambia la versión del archivo GlobalAssemblyInfo."
    
    Invoke-Expression -Command "& `"$Env:AssemblyInfoWriterCash`" `"$LocalFolder\$GlobalAssemblyInfo`" `"$AssemblyMajorVersion`" `"$AssemblyMinorVersion`" `"$AssemblyRevision`" `"$AssemblyBuildNumber`" `"$AssemblyProductVersion`""
    
    Write-Output "Termina el cambio"
    exit 0
}Catch {
    Write-Output "ERROR: error en la ejucución del proceso: CI.Build_UpdateVersion: $_"
    Write-Host $_.Exception.Message
    
    exit 1
}