# Pansy Backend Libraries

This repository contains the collection of common libraries used across all PansyDev backends.

Each project consist of four layers:

- **Domain** - Layer with pure business logic. Entities, repositories, specifications and domain services are located
  here
- **Application** - Layer which defines DTO's and encapsulates the domain logic as application services
- **Infrastructure** - Layer which implements all services from two above layers
- **Web** - Layer which exposes application services as REST and GraphQL endpoints

## Usage

Just reference this repo as a git submodule:

```shell
git submodule add https://github.com/pansydev/backend-libraries src/PansyDev.Common
```

Create `Directory.Build.props` with this content:

```xml

<Project>
    <PropertyGroup>
        <SolutionDir>$(MSBuildThisFileDirectory)</SolutionDir>
        <PansyDevCommonPath>$(SolutionDir)src\PansyDev.Common\src</PansyDevCommonPath>
    </PropertyGroup>
</Project>
```

And link packages like this:

```xml

<ItemGroup>
    <ProjectReference Include="$(PansyDevCommonPath)\PansyDev.Common.Domain\PansyDev.Common.Domain.csproj"/>
</ItemGroup>
```

See [Shetter Backend](https://github.com/pansydev/shetter-backend) as a code example.
