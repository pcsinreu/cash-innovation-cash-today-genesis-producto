#Nombre: CI.Build_UpdateVersion_PS.ps1
#Descripción: Script encargado de actualizar versión del GlobalAssemblyInfo.
#Version: 1.0
#Parámetros de entrada:
#       0: 01_FixedMajorNumber
#       1: 02_FixedMinorNumber
#       2: 03_FixedBuildNumber
#       3: 04_FixedRevisionNumber
#       4: BUILD_REPOSITORY_LOCALPATH
#       5: GlobalAssemblyInfo
#Parámetros de Salida:
#      0: Ejecución correcta
#      1: Ejecución incorrecta
#Extensión del Script: N/A

$AssemblyMajorVersion = $Env:01_FixedMajorNumber
$AssemblyMinorVersion = $Env:02_FixedMinorNumber
$AssemblyRevision = $Env:03_FixedRevisionNumber
$AssemblyBuildNumber = $Env:04_FixedBuildNumber

$Date = Get-Date -Format "yyMMdd"
$AssemblyProductVersion = "$AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$Date$AssemblyBuildNumber"

$PathGlobalAssemblyInfo= "$Env:BUILD_REPOSITORY_LOCALPATH\$Env:GlobalAssemblyInfo"

Try {
    Write-Output "Cambia la versión del archivo GlobalAssemblyInfo."
    #Leer el archivo GlobalAssemblyInfo.vb
    $GlobalAssemblyInfo = (Get-Content -Path $PathGlobalAssemblyInfo)

    Write-Output "versión actual: " $GlobalAssemblyInfo

    #Reemplazar AssemblyVersion
    $GlobalAssemblyInfo = $GlobalAssemblyInfo -replace "AssemblyVersion(.*)","AssemblyVersion(""$AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$AssemblyBuildNumber"")>"

    #Reemplazar AssemblyFileVersion
    $GlobalAssemblyInfo = $GlobalAssemblyInfo -replace "AssemblyFileVersion(.*)","AssemblyFileVersion(""$AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$AssemblyBuildNumber"")>"

    #Reemplazar AssemblyInformationalVersion
    $GlobalAssemblyInfo = $GlobalAssemblyInfo -replace "AssemblyInformationalVersion(.*)","AssemblyInformationalVersion(""$AssemblyProductVersion"")>"

    #Guardar el archivo GlobalAssemblyInfo.vb
    $GlobalAssemblyInfo | Set-Content -Path $PathGlobalAssemblyInfo

    Write-Output "versión modificada: " $GlobalAssemblyInfo

    Write-Output "Termina el cambio"
    exit 0
}Catch {
    Write-Output "ERROR: error en la ejucución del proceso: CI.Build_UpdateVersion: $_"
    Write-Host $_.Exception.Message
    
    exit 1
}