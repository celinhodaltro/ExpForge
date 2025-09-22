# Caminhos
$Root = Split-Path -Parent $MyInvocation.MyCommand.Path
$Project = "$Root\..\src\ExperienceWidget.CLI\ExperienceWidget.CLI.csproj"
$NpmRoot = "$Root\npm-package"
$Dist = "$NpmRoot\dist"

# Limpa dist
if (Test-Path $Dist) { Remove-Item $Dist -Recurse -Force }

# Runtimes alvo
$runtimes = @("win-x64","linux-x64","osx-x64")

# Copia templates
Write-Host "Copiando templates..."
Copy-Item "$NpmRoot\templates" "$Dist\templates" -Recurse

# Incrementa versão do package.json
$packageJsonPath = "$NpmRoot\package.json"
$packageJsonDist = "$Dist\package.json"

$jsonText = Get-Content $packageJsonPath -Raw
$package = $jsonText | ConvertFrom-Json

$versionParts = $package.version.Split(".")
$versionParts[2] = ([int]$versionParts[2] + 1)  # incrementa patch
$newVersion = "$($versionParts[0]).$($versionParts[1]).$($versionParts[2])"
$jsonText = $jsonText -replace '"version":\s*".*"', '"version": "' + $newVersion + '"'

# Cria dist e salva package.json
New-Item -ItemType Directory -Path $Dist -Force | Out-Null
Set-Content "$Dist\package.json" $jsonText

# Publica executáveis isolados
foreach ($r in $runtimes) {
    $publishDir = "$Dist\$r"
    Write-Host "Publicando para $r..."
    dotnet publish $Project -c Release -r $r --self-contained true -p:PublishSingleFile=true -o $publishDir
}

Write-Host "npm-package/dist gerado com sucesso!"
