param(
    [int][Parameter(Mandatory=$false)] $Year = 2022,
    [int][Parameter(Mandatory=$true)] $Day
)

$projectName = ("Day" + "$Day".PadLeft(2, '0'))
$projectPath = (Join-Path $Year $projectName)

Write-Host $projectPath

if( test-path $projectPath ) {
    Write-Error "Path ${projectPath} has already been created"
    exit
}

dotnet new console -n $projectName -o $projectPath
dotnet sln add $projectPath