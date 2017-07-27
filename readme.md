## Installation 

1. Add the tool to your `csproj` file:

```
<ItemGroup>
    <DotNetCliToolReference Include="Dotnet.Harness" Version="0.1.0-*" />
</ItemGroup>
```

2. Setup your `csproj` to copy dependencies on build:

```
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netcoreapp1.0</TargetFramework>
    <AssemblyName>MyAssembly</AssemblyName>
    
    <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
    <!-- Add this ^^^ -->

  </PropertyGroup>
</Project>
```

## Running

```
dotnet harness MyAssembly "Namespace.Class" Method '{"key1": "value1"}'
```