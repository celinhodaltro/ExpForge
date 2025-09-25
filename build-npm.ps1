# Caminhos
$Root = Split-Path -Parent $MyInvocation.MyCommand.Path
$Project = "$Root\src\ExpForge.CLI\ExpForge.CLI.csproj"
$NpmRoot = "$Root\npm-package"
$Dist = "$NpmRoot\dist"

# Limpa dist, mantendo launch.js
if (Test-Path $Dist) {
    Get-ChildItem $Dist -Force | ForEach-Object {
        if ($_.Name -ne "launcher.js") {
            Remove-Item $_.FullName -Recurse -Force
        }
    }
} else {
    New-Item -ItemType Directory -Path $Dist -Force | Out-Null
}

# Lê a versão do csproj
[xml]$csproj = Get-Content $Project
$version = $csproj.Project.PropertyGroup.Version
if (-not $version) {
    Write-Error "Não foi possível ler a versão do csproj!"
    exit 1
}

Write-Host "Versão do CLI: $version"

# Copia templates
Write-Host "Copiando templates..."
Copy-Item "$NpmRoot\templates" "$Dist\templates" -Recurse -Force

# Atualiza package.json na raiz do npm-package
$packageJsonPath = "$NpmRoot\package.json"
$jsonText = Get-Content $packageJsonPath -Raw
$jsonText = $jsonText -replace '"version":\s*".*"', "`"version`": `"$version`""

# Salva sem adicionar espaço/linha no final
$jsonText | Out-File -FilePath $packageJsonPath -Encoding utf8 -NoNewline

# Runtimes alvo
$runtimes = @("win-x64","linux-x64","osx-x64")

# Publica executáveis isolados
foreach ($r in $runtimes) {
    $publishDir = "$Dist\$r"
    Write-Host "Publicando para $r..."
    dotnet publish $Project `
        -c Release `
        -r $r `
        --self-contained true `
        -p:PublishSingleFile=true `
        -p:SkipNpmBuild=true `
        -o $publishDir
}

Write-Host "npm-package/dist gerado com sucesso!"
exit 0
