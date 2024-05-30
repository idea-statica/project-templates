# IDEA StatiCa project templates

Templates for creating projects for Idea StatiCa BIM link plugins

Installation

In terminal run  :
```
dotnet new install IdeaStatiCa.Dotnet.Templates
```

It installs nuget package
[IdeaStatiCa.Dotnet.Templates](https://www.nuget.org/packages/IdeaStatiCa.Dotnet.Templates/)

It allows to create a new c# project (or all a solution) for new BIM plugins for IDEA StatiCa in [.NET CLI](https://learn.microsoft.com/en-us/dotnet/core/tools/) or in Microsoft Visual Studio

To check installation of new templates

```
dotnet new list
```

You should get a list of all templates which are installed on a PC. The should be also

```
IDEA StatiCa Checkbot Client CAD App          bimapicadapp      [C#]      ideastatica/checkbot/cad
IDEA StatiCa Checkbot Client FEA App          bimapifeaapp      [C#]      ideastatica/checkbot/fea
IDEA StatiCa RCS Rest API client console App  rcsclientconsole  [C#]      ideastatica/rcs/api
```

commands  
dotnet new bimapifeaapp /?

or 

dotnet new rcsclientconsole /?

show how to use it in .NET CLI

```
PS C:\git\project-templates\src\bin\Release> dotnet new bimapifeaapp /?
IDEA StatiCa Checkbot Client FEA App (C#)
Author: IDEA StatiCa

Usage:
  dotnet new bimapifeaapp [options] [template options]

Options:
  -n, --name <name>       The name for the output being created. If no name is specified, the name of the output directory is used.
  -o, --output <output>   Location to place the generated output.
  --dry-run               Displays a summary of what would happen if the given command line were run if it would result in a template creation.
  --force                 Forces content to be generated even if it would change existing files.
  --no-update-check       Disables checking for the template package updates when instantiating a template.
  --project <project>     The project that should be used for context evaluation.
  -lang, --language <C#>  Specifies the template language to instantiate.
  --type <solution>       Specifies the template type to instantiate.

Template options:
  -p, --projectBimApiFeaApp <projectBimApiFeaApp>        WPF application which simulates a FEA application
                                                         Type: string
                                                         Default: BimApiFeaApp
  -pr, --projectBimApiFeaLink <projectBimApiFeaLink>     Implementation of BIM API for converting objects in a FEA Model to BIM API objects
                                                         Type: string
                                                         Default: BimApiFeaLink
  -p:p, --projectCheckbotClient <projectCheckbotClient>  Responsible for running and communicating with IDEA Checkbot
                                                         Type: string
                                                         Default: CheckbotClient
  -p:pr, --projectFeaApi <projectFeaApi>                 Mock of a FEA API
                                                         Type: string
                                                         Default: FeaApi
  -F, --Framework <net48|net6.0-windows>                 The target framework for the project.
                                                         Type: choice
                                                           net6.0-windows  Target net6.0-windows
                                                           net48           Target net48
                                                         Default: net6.0-windows
```

To create a new WPF application *MyCheckbotFeaPluginApp* which runs Idea StatiCa Checkbot and implements basic BIM Api importers in current directory run 

```
dotnet new bimapifeaapp -F net48 -o MyCheckbotFeaPluginApp
```

 (supported frameworks are net48 or net6.0-windows)

 it is possible to run it from Microsoft Visual Studio. See

 ![Create new project](media/new-project-vs-wizard.png?raw=true "Create new project")

  ![MyCheckbotFeaPluginApp](media/MyCheckbotFeaPluginApp.png?raw=true "MyCheckbotFeaPluginApp")

the second command creates only csprj project (not whole application)

```
dotnet new bimapifeaclient -o MyCheckbotFeaPlugin
```

# Pack and install

[See MSDN documentation](https://learn.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-template-package?pivots=dotnet-8-0)

```
dotnet pack
```
