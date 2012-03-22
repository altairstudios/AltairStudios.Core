#!/bin/bash
mkdir build
cd build

echo "----------------------------- APLICATION -----------------------------" 
mkdir -p Application
cp -r ../AltairStudios.Core/bin Application
zip -r AltairStudios.Core.Application.zip Application
rm -rf Application

echo "----------------------------- SOURCE -----------------------------"
mkdir -p Source/AltairStudios.Core
cp -r ../AltairStudios.Core Source/AltairStudios.Core
cp ../AltairStudios.Core.Source.sln Source
zip -r AltairStudios.Core.Source.zip Source
rm -rf Source


echo "----------------------------- TESTS -----------------------------"
mkdir -p Tests/AltairStudios.Core
cp -r ../Tests Tests/AltairStudios.Core
cp ../AltairStudios.Core.Tests.sln Tests
zip -r AltairStudios.Core.Tests.zip Tests
rm -rf Tests


echo "----------------------------- DOC -----------------------------" 
mkdir -p Doc
cp ../AltairStudios.Core/bin/AltairStudios.Core.xml Doc
zip -r AltairStudios.Core.Doc.zip Doc
rm -rf Doc


echo "----------------------------- RELEASE -----------------------------"
mkdir -p Release
cp -r ../AltairStudios.Core Release
cp -r ../Tests Release
cp ../AltairStudios.Core.sln Release
zip -r AltairStudios.Core.zip Release
rm -rf Release
