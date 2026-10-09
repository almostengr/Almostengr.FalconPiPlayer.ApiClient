#!/bin/bash

for directory in Almostengr*/
do
    cd "$directory"
    dotnet package update
    dotnet restore
    cd ..
done

dotnet build
