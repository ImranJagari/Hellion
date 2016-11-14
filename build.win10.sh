#!/bin/sh

cd src/Hellion.Login
dotnet restore
dotnet build -r win10-x64
dotnet publish -c release -r win10-x64