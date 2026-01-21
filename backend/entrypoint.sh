#!/bin/bash
set -e

echo "Running database migrations..."
dotnet API.dll --migrate

echo "Starting application..."
exec dotnet API.dll
