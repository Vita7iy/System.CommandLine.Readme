#!/bin/bash

echo "Building test project..."
dotnet build CommandLine.CreateReadme.Tests/System.CommandLine.Readme.Tests.csproj

if [ $? -eq 0 ]; then
    echo "Build successful! Running tests..."
    dotnet test CommandLine.CreateReadme.Tests/System.CommandLine.Readme.Tests.csproj --no-build --verbosity normal
else
    echo "Build failed!"
    exit 1
fi

