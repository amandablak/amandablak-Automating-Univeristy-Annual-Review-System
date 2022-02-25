# PR Process
+ Create PR. PR should not be approved without appropriate tests
+ PR must be reviewed by at least 2 code owners
+ PR must pass Travis CI tests
+ Merge PR after all checks pass and PR is approved

# How To

## Build a solution for Visual Studio or other IDE

### Linux/MacOS
`dotnet new sln -n APRServer`

`dotnet sln add APRServer.sln **/*.csproj`

### Windows Command Prompt
`dotnet new sln -n APRServer`

`FOR /R %i IN (*.csproj) DO dotnet sln APRServer.sln add "%i"`

### Windows PowerShell
```
$projects = Get-ChildItem -Recurse | Where-Object { $_.Name -match '^.+\.(csproj|vbproj)$' }

$uniqueProjects = $projects | Group-Object -Property Name | Where Count -EQ 1 | select -ExpandProperty Group | % { $_.FullName }

Invoke-Expression -Command "dotnet new sln -n APRServer"

$uniqueProjects | % { Invoke-Expression -Command "dotnet sln APRServer.sln add ""$_""" }
```

## Reference new package
If you are referencing other packages in the API add a reference. Ex:

`dotnet add FacultyAPR.API/FacultyAPR.API.csproj reference lib/FacultyAPR.Models/FacultyAPR.Models.csproj`

## Create new library
ex: 

`dotnet new classlib -n FacultyAPR.Models`

## How to Clone this Repository for Local Development
+ Click the clone button at the root of the repository
+ Choose HTTPS and copy the url
+ In your terminal, navigate to the directory you want to clone into
+ `git clone {url}`
+ `cd CS-21.16` 

## How to create a branch for your feature / bug fix
First off, branch management allows us to not worry about introducing bugs and other problems into the main branch.

+ `cd` into project
+ `git checkout main`
+ `git pull`
+ `git checkout -b {branchname}`
+ You are now in your own branch!

## Update your branch / pull in remote changes from GitHub
+ `git pull`

## Pull in changes from main into your repo
+ `git pull origin main`

## Commit your changes
+ `git add {files}`
+ `git commit -m {commit_msg}`

## Create Pull Request for Team Review
+ `git push`
+ Copy the pr link, navigate to it in your browser and add your teammates as reviewers

## Skip a Travis-CI Build
add the phrase [skip ci] to a commit to skip a build
+ `git commit -m "[skip ci] Update README.md"`
> Note that in case multiple commits are pushed together, the skip command is effective only if present in the commit message of the HEAD commit.

## Full git workflow
+ Update main
+ Checkout your new branch
+ Make code additions / deletions
+ Commit changes incrementally!
+ When things are all done and tested, make a PR
+ Add your teammates as reviewers
+ Once 2+ people have reviewed and approved you can merge into main

## How to stop GitHub from asking for your password every time by caching your credentials
+ `cd` into project
+ `git config --global credential.helper cache`

## How to use PlantUML
+ [PlantUML docs](https://plantuml.com/)