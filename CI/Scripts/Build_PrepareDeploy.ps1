#Nombre: CI.Build_PrepareDeploy.ps1
#Descripción: Script encargado de preparar los ficheros con los artefactos Script Desktop.
#Version: 1.0
#Parámetros de entrada:
#       0: BUILD_REPOSITORY_LOCALPATH
#       1: BUILD_BUILDNUMBER
#Parámetros de Salida:
#      0: Ejecución correcta
#      1: Ejecución incorrecta
#Extensión del Script: N/A

$LocalFolder=$Env:BUILD_REPOSITORY_LOCALPATH
$BuildNumber=$Env:BUILD_BUILDNUMBER

try{   

    Write-Output "Copia compilados de las aplicaciones para las distintas carpetas"

    if(-Not (Test-Path "$LocalFolder\$BuildNumber")){
        Invoke-Expression -Command "New-Item -Path `"$LocalFolder`" -Name `"$BuildNumber`" -ItemType `"directory`""
    }

    Write-Output "##### Inicio Prepare Deploy Genesis Producto #####"

   
    Invoke-Expression -Command "New-Item -Path `"$LocalFolder\$BuildNumber\`" -Name `"Sitios\Web`" -ItemType `"directory`""
    if(Test-Path "$LocalFolder\Publish\Prosegur.Genesis.Web"){
        Write-Output "##### Copia Web #####"
        Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\Publish\Prosegur.Genesis.Web\*`" -Destination `"$LocalFolder\$BuildNumber\Sitios\Web\`" -Recurse -Force"
    }

    Invoke-Expression -Command "New-Item -Path `"$LocalFolder\$BuildNumber\`" -Name `"Sitios\IAC`" -ItemType `"directory`""
    if(Test-Path "$LocalFolder\Publish\Prosegur.Genesis.IAC"){
        Write-Output "##### Copia IAC #####"
        Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\Publish\Prosegur.Genesis.IAC\*`" -Destination `"$LocalFolder\$BuildNumber\Sitios\IAC\`" -Recurse -Force"
    }

    Invoke-Expression -Command "New-Item -Path `"$LocalFolder\$BuildNumber\`" -Name `"Sitios\NuevoSaldos`" -ItemType `"directory`""
    if(Test-Path "$LocalFolder\Publish\Prosegur.Genesis.Saldos"){
        Write-Output "##### Copia NuevoSaldos #####"
        Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\Publish\Prosegur.Genesis.Saldos\*`" -Destination `"$LocalFolder\$BuildNumber\Sitios\NuevoSaldos\`" -Recurse -Force"
    }

    Invoke-Expression -Command "New-Item -Path `"$LocalFolder\$BuildNumber\`" -Name `"Sitios\Reportes`" -ItemType `"directory`""
    if(Test-Path "$LocalFolder\Publish\Prosegur.Genesis.Reportes"){
        Write-Output "##### Copia Reportes #####"
        Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\Publish\Prosegur.Genesis.Reportes\*`" -Destination `"$LocalFolder\$BuildNumber\Sitios\Reportes\`" -Recurse -Force"
    }

    
    Invoke-Expression -Command "New-Item -Path `"$LocalFolder\$BuildNumber\`" -Name `"Sitios\Servicio`" -ItemType `"directory`""
    if(Test-Path "$LocalFolder\Publish\Prosegur.Genesis.Servicio"){
        Write-Output "##### Copia Servicio #####"
        Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\Publish\Prosegur.Genesis.Servicio\*`" -Destination `"$LocalFolder\$BuildNumber\Sitios\Servicio\`" -Recurse -Force"
    }

    
    Invoke-Expression -Command "New-Item -Path `"$LocalFolder\$BuildNumber\`" -Name `"Sitios\ConsultaLocal`" -ItemType `"directory`""
    if(Test-Path "$LocalFolder\Publish\Prosegur.Genesis.ConsultaLocal"){
        Write-Output "##### Copia Consulta Local #####"
        Invoke-Expression -Command "Copy-Item -Path `"$LocalFolder\Publish\Prosegur.Genesis.ConsultaLocal\*`" -Destination `"$LocalFolder\$BuildNumber\Sitios\ConsultaLocal\`" -Recurse -Force"
    }

    Write-Output "##### Elimina archivos del proyecto Reportes #####"
        
    if(Test-Path "$LocalFolder\$BuildNumber\Sitios\Reportes\bin\log4net.dll"){
        Write-Output "##### Elimina el archivo log4net.dll del proyecto Reportes #####"
        Invoke-Expression -Command "Remove-Item -Path `"$LocalFolder\$BuildNumber\Sitios\Reportes\bin\log4net.dll`" -Force"
    }
    
    if(Test-Path "$LocalFolder\$BuildNumber\Sitios\Reportes\bin\stdole.dll"){
        Write-Output "##### Elimina el archivo stdole.dll del proyecto Reportes #####"
        Invoke-Expression -Command "Remove-Item -Path `"$LocalFolder\$BuildNumber\Sitios\Reportes\bin\stdole.dll`" -Force"
    }

    Write-Output "##### Elimina archivo Oracle.DataAccess.dll de los proyectos #####"
        Invoke-Expression -Command "Remove-Item -Path `"$LocalFolder\$BuildNumber\Sitios\*\bin\Oracle.DataAccess.dll`" -Force -ErrorAction SilentlyContinue"
     
    
    Write-Output "##### Elimina los archivos *.pdb, *.xml y *.dbc de la carpeta bin de los proyectos #####"
        Invoke-Expression -Command "Remove-Item -Path `"$LocalFolder\$BuildNumber\Sitios\*\bin\*.pdb`" -Force -ErrorAction SilentlyContinue"
        Invoke-Expression -Command "Remove-Item -Path `"$LocalFolder\$BuildNumber\Sitios\*\bin\*.xml`" -Force -ErrorAction SilentlyContinue"
        Invoke-Expression -Command "Remove-Item -Path `"$LocalFolder\$BuildNumber\Sitios\*\bin\*.dbc`" -Force -ErrorAction SilentlyContinue"

    Write-Output "##### Elimina los archivos web.config de los proyectos #####"
        Invoke-Expression -Command "Remove-Item -Path `"$LocalFolder\$BuildNumber\Sitios\*\web.config`" -Force -ErrorAction SilentlyContinue"
     
     
    Write-Output "##### Fin Prepare Deploy Genesis Producto  #####"

    exit 0
}Catch{
    Write-Output "ERROR: Error en la preparación de binarios: $_"
    Write-Host $_.Exception.Message
    exit 1
}
