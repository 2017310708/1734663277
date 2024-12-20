#!/usr/bin/env bash
DIR=$(dirname "$(readlink -f "$0")")
cd "$DIR"
function structure() {
    mkdir Models
    touch Models/{Project.cs,Task.cs}
    mkdir Data
    touch Data/ApplicationDbContext.cs
    mkdir DTOs
    touch DTOs/{ProjectDto,TaskDto,ResponseDto}.cs
    mkdir Validations
    touch Validations/{ProjectValidation,TaskValidation}.cs
    tree
}
function uninstall() {
    installedpackages=`dotnet list package | awk '/> /{print $2}'`
    for package in $installedpackages; do
        dotnet remove package $package
    done
}
function install() {
    dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.3
    dotnet add package Microsoft.EntityFrameworkCore --version 8.0.3
    dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.3
    dotnet add package MySql.EntityFrameworkCore --version 8.0.2
    dotnet add package Swashbuckle.AspNetCore --version 7.1.0
    # dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.2
    dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.2
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.3
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.2
    dotnet add package Microsoft.EntityFrameworkCore.Relational --version 8.0.3
}
# uninstall
install