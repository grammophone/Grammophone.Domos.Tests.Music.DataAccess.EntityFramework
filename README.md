# Grammophone.Domos.Tests.Music.DataAccess.EntityFramework

Entity Framework 6 data access provider for the Domos music test application.

This project implements the EF6 domain container and adapter for `IMusicDomosDomainContainer`, including the mapping needed by the shared music security and workflow tests.

## Target Framework

- `net472`

## Required Projects

This project expects these sibling projects to be available when building from the solution or from extracted submodules:

Direct project references:

- `Grammophone.DataAccess.EntityFramework`
- `Grammophone.DataAccess.EntityFramework.Plus`
- `Grammophone.Domos.DataAccess.EntityFramework`
- `Grammophone.Domos.Tests.Music.DataAccess`

Additional transitive project references:

- `Grammophone.DataAccess`
- `Grammophone.Domos.DataAccess`
- `Grammophone.Domos.Domain`
- `Grammophone.Domos.Tests.Music.Domain`
- `Grammophone.GenericContentModel`
