#Nombre: CI.Build_PushVersion.ps1
#Descripción: Script encargado de hacer push de GlobalAssemblyInfo
#Version: 1.0
#Parámetros de entrada: 
#       0: GlobalAssemblyInfo
#       1: 01_FixedMajorNumber
#       2: 02_FixedMinorNumber
#       3: 03_FixedBuildNumber
#       4: 04_FixedRevisionNumber
#       5: BUILD_REPOSITORY_LOCALPATH
#       6: GenesisProductoRepository
#       7: BUILD_SOURCEBRANCHNAME
#Parámetros de Salida: 
#      0: Ejecución correcta
#      1: Ejecución incorrecta
#Extensión del Script:


$GlobalAssemblyInfo = $Env:GlobalAssemblyInfo

$AssemblyMajorVersion = $Env:01_FixedMajorNumber
$AssemblyMinorVersion = $Env:02_FixedMinorNumber
$AssemblyRevision = $Env:03_FixedRevisionNumber
$AssemblyBuildNumber = $Env:04_FixedBuildNumber

$Date = Get-Date -Format "yyMMdd"
$AssemblyProductVersion = "$AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$Date$AssemblyBuildNumber"

$LocalFolder="$Env:BUILD_REPOSITORY_LOCALPATH"
$Repo = "$Env:GenesisProductoRepository"
$Branch = "$Env:BUILD_SOURCEBRANCHNAME"

Try { 
    Invoke-Expression -Command "git checkout `"$Branch`""

    Write-Output "Updating Version to $AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$AssemblyBuildNumber"
      
    Invoke-Expression -Command "& `"$Env:AssemblyInfoWriterCash`" `"$LocalFolder\$GlobalAssemblyInfo`" `"$AssemblyMajorVersion`" `"$AssemblyMinorVersion`" `"$AssemblyRevision`" `"$AssemblyBuildNumber`" `"$AssemblyProductVersion`""

    Invoke-Expression -Command "git add `"$GlobalAssemblyInfo`""

    Write-Output "Git Commit - `"Auto-Build: $AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$AssemblyBuildNumber`""    
    
    Invoke-Expression -Command "git commit -m `"Auto-Build: $AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$AssemblyBuildNumber`""               

    Write-Output "Git Push"
    Invoke-Expression -Command "git push `"$Repo`""

    exit 0
}
Catch {
    Write-Output "ERROR: Error en crear la release in repositorio: $_"
    Write-Host $_.Exception.Message 
    exit 1
}