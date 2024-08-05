
$LocalFolder=$Env:BUILD_REPOSITORY_LOCALPATH
$BuildNumber=$Env:BUILD_BUILDNUMBER

$AssemblyMajorVersion = [int]$Env:01_FixedMajorNumber
$AssemblyMinorVersion = [int]$Env:02_FixedMinorNumber
$AssemblyRevision = $Env:03_FixedRevisionNumber
$AssemblyBuildNumber = $Env:04_FixedBuildNumber
$AssemblyProductVersion = Get-Date -Format "yyMMdd"

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

#Variables para Genesis.sql y Reportes.sql
$PathGenesisScript            =     "$LocalFolder\Extras\BaseDatos\Genesis.sql"
$PathReportesScript           =     "$LocalFolder\Extras\BaseDatos\Reportes.sql"

#Variables para Grants_Synonyms.sql
$PathGrantsSynonymsReportesScript = "$LocalFolder\Extras\BaseDatos\Grants_Synonyms\Reportes\Grants_Synonyms.sql"
$PathGrantsSynonymsGenesisScript =  "$LocalFolder\Extras\BaseDatos\Grants_Synonyms\Genesis\Grants_Synonyms.sql"


#ScriptsEntrega.sql de Reportes
Write-Output "##### Generar ScriptsEntrega.sql de Reportes #####"
    if (Test-Path $PathEntregaReportesScript){
       #Eliminamos el archivo en caso de que exista
       Remove-Item -Force -Path $PathEntregaReportesScript
    }

    #Buscamos todos los archivos .sql y los unimos en el archivo ScriptEntrega.sql
    Get-ChildItem $PathEntregaReportes -Recurse -Include *.sql  |
    ForEach-Object { Get-content -Path $_ -Raw -Encoding UTF8 | out-file $PathEntregaReportesScript -Append }

    if (Test-Path $PathEntregaReportesScript){
        #Reemplazamos ###VERSION### por la version ej (0209)
        $archivoReporteEntrega = (Get-Content -Path $PathEntregaReportesScript -Raw -Encoding UTF8) -replace '###VERSION###', "$($AssemblyMajorVersion.ToString('00'))$($AssemblyMinorVersion.ToString('00'))"
        #Guardamos el cambio
        $archivoReporteEntrega | Set-Content -Encoding UTF8 -Path $PathEntregaReportesScript

        #Reemplazamos ###VERSION_COMP###  por la version ej (2.9.0.2105301)
        $archivoReporteEntrega = (Get-Content -Path $PathEntregaReportesScript -Raw -Encoding UTF8) -replace '###VERSION_COMP###', "$AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$AssemblyProductVersion$AssemblyBuildNumber"
        #Guardamos el cambio
        $archivoReporteEntrega | Set-Content -Encoding UTF8 -Path $PathEntregaReportesScript
    }

#ScriptsEntrega.sql de Genesis
Write-Output "##### Generar ScriptsEntrega.sql de Genesis #####"
    if (Test-Path $PathEntregaGenesisScript){
       #Eliminamos el archivo en caso de que exista
       Remove-Item -Force -Path $PathEntregaGenesisScript
    }

    #Buscamos todos los archivos .sql y los unimos en el archivo ScriptEntrega.sql
    Get-ChildItem $PathEntregaGenesis -Recurse -Include *.sql  |
    ForEach-Object { Get-content -Path $_ -Raw -Encoding UTF8 | out-file $PathEntregaGenesisScript -Append }

    if (Test-Path $PathEntregaGenesisScript){
        #Reemplazamos ###VERSION### por la version ej (0209)
        $archivoGenesisEntrega = (Get-Content -Path $PathEntregaGenesisScript -Raw -Encoding UTF8 ) -replace '###VERSION###', "$($AssemblyMajorVersion.ToString('00'))$($AssemblyMinorVersion.ToString('00'))"
        #Guardamos el cambio
        $archivoGenesisEntrega | Set-Content -Encoding UTF8 -Path $PathEntregaGenesisScript

        #Reemplazamos ###VERSION_COMP###  por la version ej (2.9.0.2105301)
        $archivoGenesisEntrega = (Get-Content -Path $PathEntregaGenesisScript -Raw -Encoding UTF8 ) -replace '###VERSION_COMP###', "$AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$AssemblyProductVersion$AssemblyBuildNumber"
        #Guardamos el cambio
        $archivoGenesisEntrega | Set-Content -Encoding UTF8 -Path $PathEntregaGenesisScript
    }

#Procedures.sql de Reportes
Write-Output "##### Generar Procedures.sql de Reportes #####"
    if (Test-Path $PathGeralReportesScript){
       #Eliminamos el archivo en caso de que exista
       Remove-Item -Force -Path $PathGeralReportesScript
    }

    #Buscamos todos los archivos .sql y los unimos en el archivo ScriptEntrega.sql
    Get-ChildItem $PathGeralReportes -Recurse -Include *.sql  |
    ForEach-Object { Get-content -Path $_ -Raw -Encoding UTF8 | out-file $PathGeralReportesScript -Append }

    if (Test-Path $PathGeralReportesScript){
        #Reemplazamos ###VERSION### por la version ej (0209)
        $archivoReporteGeral = (Get-Content -Path $PathGeralReportesScript -Raw -Encoding UTF8 ) -replace '###VERSION###', "$($AssemblyMajorVersion.ToString('00'))$($AssemblyMinorVersion.ToString('00'))"
        #Guardamos el cambio
        $archivoReporteGeral | Set-Content -Encoding UTF8 -Path $PathGeralReportesScript
        
        #Reemplazamos ###VERSION_COMP###  por la version ej (2.9.0.2105301)
        $archivoReporteGeral = (Get-Content -Path $PathGeralReportesScript -Raw -Encoding UTF8 ) -replace '###VERSION_COMP###', "$AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$AssemblyProductVersion$AssemblyBuildNumber"
        #Guardamos el cambio
        $archivoReporteGeral | Set-Content -Encoding UTF8 -Path $PathGeralReportesScript
    }

#Procedures.sql de Genesis
Write-Output "##### Generar Procedures.sql de Genesis #####"
    if (Test-Path $PathGeralGenesisScript){
       #Eliminamos el archivo en caso de que exista
       Remove-Item -Force -Path $PathGeralGenesisScript
    }

    #Buscamos todos los archivos .sql y los unimos en el archivo ScriptEntrega.sql
    Get-ChildItem $PathGeralGenesis -Recurse -Include *.sql  |
    ForEach-Object { Get-content -Path $_ -Raw -Encoding UTF8 | out-file $PathGeralGenesisScript -Append }

    if (Test-Path $PathGeralGenesisScript){
        #Reemplazamos ###VERSION### por la version ej (0209)
        $archivoGenesisGeral = (Get-Content -Path $PathGeralGenesisScript -Raw -Encoding UTF8 ) -replace '###VERSION###', "$($AssemblyMajorVersion.ToString('00'))$($AssemblyMinorVersion.ToString('00'))"
        #Guardamos el cambio
        $archivoGenesisGeral | Set-Content -Encoding UTF8 -Path $PathGeralGenesisScript

        #Reemplazamos ###VERSION_COMP###  por la version ej (2.9.0.2105301)
        $archivoGenesisGeral = (Get-Content -Path $PathGeralGenesisScript -Raw -Encoding UTF8 ) -replace '###VERSION_COMP###', "$AssemblyMajorVersion.$AssemblyMinorVersion.$AssemblyRevision.$AssemblyProductVersion$AssemblyBuildNumber"
        #Guardamos el cambio
        $archivoGenesisGeral | Set-Content -Encoding UTF8 -Path $PathGeralGenesisScript
    }


Write-Output "##### Prepara y copia para el directorio destino el archivo Genesis.sql #####"
    if (Test-Path $PathGenesisScript){
        #Eliminamos el archivo en caso de que exista
        Remove-Item -Force -Path $PathGenesisScript
    }
Write-Output "## Prepara el archivo Genesis.sql ##"
    Get-Item $PathEntregaGenesisScript | Get-content -Raw -Encoding UTF8 | out-file $PathGenesisScript -Append utf8
    Get-Item $PathGeralGenesisScript | Get-content -Raw -Encoding UTF8 | out-file $PathGenesisScript -Append utf8
Write-Output "## Copia el archivo Genesis.sql al directorio de destino ##"
    Invoke-Expression -Command "Copy-Item -Path `"$PathGenesisScript`" -Destination `"$LocalFolder\$BuildNumber\`" -Recurse -Force"

Write-Output "##### Prepara y copia para el directorio destino el archivo Reportes.sql #####"
    if (Test-Path $PathReportesScript){
        #Eliminamos el archivo en caso de que exista
        Remove-Item -Force -Path $PathReportesScript
    }
Write-Output "## Prepara el archivo Reportes.sql ##"
    Get-Item $PathEntregaReportesScript | Get-content -Raw -Encoding UTF8 | out-file $PathReportesScript -Append utf8
    Get-Item $PathGeralReportesScript | Get-content -Raw -Encoding UTF8 | out-file $PathReportesScript -Append utf8
Write-Output "## Copia el archivo Reportes.sql al directorio de destino ##"
    Invoke-Expression -Command "Copy-Item -Path `"$PathReportesScript`" -Destination `"$LocalFolder\$BuildNumber\`" -Recurse -Force"


Write-Output "##### Comienza copia para el directorio destino de los archivos Grants_Synonyms.sql #####"
    if(Test-Path $PathGrantsSynonymsReportesScript){
        Write-Output "## Copia para el directorio destino del archivo Reportes_GeS.sql ##"
        Invoke-Expression -Command "Copy-Item -Path `"$PathGrantsSynonymsReportesScript`" -Destination `"$LocalFolder\$BuildNumber\Reportes_GeS.sql`" -Recurse -Force"
    }
    else {
        Write-Output "## No encuentra el archivo $PathGrantsSynonymsReportesScript ##"
    }
    if(Test-Path $PathGrantsSynonymsGenesisScript){
        Write-Output "## Copia para el directorio destino del archivo Genesis_GeS.sql ##"
        Invoke-Expression -Command "Copy-Item -Path `"$PathGrantsSynonymsGenesisScript`" -Destination `"$LocalFolder\$BuildNumber\Genesis_GeS.sql`" -Recurse -Force"
    }
    else {
        Write-Output "## No encuentra el archivo $PathGrantsSynonymsGenesisScript ##"
    }



Write-Output "##### Copia para el directorio destino de la carpeta Reportes #####"
    if(Test-Path "$LocalFolder\Extras\Reportes"){
        Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\Extras\Reportes`" -Destination `"$LocalFolder\$BuildNumber\RS\`" -Recurse -Force"
    }

    
