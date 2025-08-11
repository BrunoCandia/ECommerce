
1-Catalog

### Use Mongo Express to manage your MongoDB database.
http://localhost:8081

This basic security is part of the image, probably.
user: admin
password: pass

### SQL Server
Database=OrderDb
User Id=sa
Password=Password@1

### pgAdmin
user: admin@eCommerce.net
pass: Password@1

### rabbitMQ: http://localhost:15672/
user: guest
pass: guest

### Portainer
user: admin
pass: Password@123

### Entity Framework Core

- Add migration: add-migration <MigrationName> -StartupProject Order.API -Project Order.Infrastructure -Context OrderContext 
- Update database: update-database -StartupProject Order.API -Project Order.Infrastructure -Context OrderContext
- Revert the very first migration: update-database 0 -StartupProject Order.API -Project Order.Infrastructure -Context OrderContext
- Remove migration: remove-migration -StartupProject Order.API -Project Order.Infrastructure -Context OrderContext

## Create Migration

add-migration Initial-Migration -StartupProject Order.API -Project Order.Infrastructure

### Connecting to SQL Server running in a docker container from local machine using SQL Server Management Studio (SSMS)

- Server: localhost,1433
- User: sa
- Password: Password@1

### Testing basket checkout flow

http://localhost:8001/api/v1/Basket/CheckoutBasket
{
  "userName": "bruno",
  "totalPrice": 6000,
  "firstName": "bruno",
  "lastName": "candia",
  "emailAddress": "bruno@ecommerce.net",
  "addressLine": "test address",
  "country": "USA",
  "state": "IN",
  "zipCode": "4600",
  "cardName": "VISA",
  "cardNumber": "4242-4242-4242-4242",
  "expiration": "12/30",
  "cvv": "123",
  "paymentMethod": 1
}