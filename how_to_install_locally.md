1. set the version in [IdeaStatiCa.Dotnet.Templates.csproj](src/IdeaStatiCa.Dotnet.Templates.csproj)
2. build the project 'IdeaStatiCa.Dotnet.Templates.csproj0

```
dotnet build src/IdeaStatiCa.Dotnet.Templates.csproj --configuration Release 
dotnet pack src/IdeaStatiCa.Dotnet.Templates.csproj --configuration Release -o nuget --no-restore
```

install the project templates from the local dir

```
dotnet new install ./nuget/IdeaStatiCa.Dotnet.Templates.*.nupkg
```

Uninstalling the templates 

```
dotnet new uninstall IdeaStatiCa.Dotnet.Templates
```