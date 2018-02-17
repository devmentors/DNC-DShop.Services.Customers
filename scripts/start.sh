#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/DShop.Services.Customers
dotnet run --no-restore