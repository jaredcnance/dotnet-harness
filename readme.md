## Installation 

Add the tool to your `csproj` file:

```
<ItemGroup>
    <DotNetCliToolReference Include="Dotnet.Harness" Version="0.1.0-*" />
</ItemGroup>
```

## Running

```
dotnet harness MyAssembly "Namespace.Class" Method '{"key1": "value1"}'
```