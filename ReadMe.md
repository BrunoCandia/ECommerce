
1-Catalog

Use Mongo Express to manage your MongoDB database.
http://localhost:8081

This basic security is part of the image, probably.
user: admin
password: pass

SQL Server
Database=OrderDb
User Id=sa
Password=Password@1

pgAdmin
admin@eCommerce.net
Password@1

Portainer
admin
Password@123

### Entity Framework Core
- Add migration: add-migration <MigrationName> -StartupProject Order.API -Project Order.Infrastructure -Context OrderContext 
- Update database: update-database -StartupProject Order.API -Project Order.Infrastructure -Context OrderContext
- Revert the very first migration: update-database 0 -StartupProject Order.API -Project Order.Infrastructure -Context OrderContext
- Remove migration: remove-migration -StartupProject Order.API -Project Order.Infrastructure -Context OrderContext
Password@123

### Entity Framework Core

## Create Migration

add-migration Initial-Migration -StartupProject Order.API -Project Order.Infrastructure