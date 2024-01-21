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
dotnet new --list
```

You should get a list of all templates which are installed on a PC. The should be also

```
IDEA StatiCa Checkbot Client                  bimapifeaclient      [C#]        ideastatica/checkbot
IDEA StatiCa Checkbot Client FEA App          bimapifeaapp         [C#]        ideastatica/checkbot
```

commands  
dotnet new bimapifeaclient /?

or 

dotnet new bimapifeaapp /?

show how to use it in .NET CLI

```
PS C:\git\project-templates> dotnet new bimapifeaclient /?
IDEA StatiCa Checkbot Client (C#)
Author: IDEA StatiCa

Usage:
  dotnet new bimapifeaclient [options] [template options]

Options:
  -n, --name <name>       The name for the output being created. If no name is specified, the name of the output directory is used.
  -o, --output <output>   Location to place the generated output.
  --dry-run               Displays a summary of what would happen if the given command line were run if it would result in a template creation.
  --force                 Forces content to be generated even if it would change existing files.
  --no-update-check       Disables checking for the template package updates when instantiating a template.
  --project <project>     The project that should be used for context evaluation.
  -lang, --language <C#>  Specifies the template language to instantiate.
  --type <project>        Specifies the template type to instantiate.

Template options:
  -F, --Framework <net48|net6.0-windows>  The target framework for the project.
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