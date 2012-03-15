#!/bin/bash
rm ./*.zip

mkdir -p tmp/Application
cp -r AltairStudios.Core/bin tmp/Application
zip -r AltairStudios.Core.Application.zip tmp/Application
rm -rf tmp/Application

mkdir -p tmp/Source/AltairStudios.Core
cp -r AltairStudios.Core tmp/Source/AltairStudios.Core
cp AltairStudios.Core.Source.sln tmp/Source
zip -r AltairStudios.Core.Source.zip tmp/Source
rm -rf tmp/Source


mkdir -p tmp/Tests/AltairStudios.Core
cp -r Tests tmp/Tests/AltairStudios.Core
cp AltairStudios.Core.Tests.sln tmp/Tests
zip -r AltairStudios.Core.Tests.zip tmp/Tests
rm -rf tmp/Tests


mkdir -p tmp/Doc
cp AltairStudios.Core/bin/AltairStudios.Core.xml tmp/Doc
zip -r AltairStudios.Core.Doc.zip tmp/Doc
rm -rf tmp/Doc


mkdir -p tmp/Release
cp -r AltairStudios.Core tmp/Release
cp -r Tests tmp/Release tmp/Release
cp AltairStudios.Core.sln tmp/Release
zip -r AltairStudios.Core.zip tmp/Release
rm -rf tmp/Release


rm -rf tmp