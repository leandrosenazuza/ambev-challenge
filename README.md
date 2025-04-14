# How to run my project! 

## Clone the project

   Execute: "git clone git@github.com:leandrosenazuza/ambev-challenge.git"

## First - Run the docker!

   Inside the project directory, you have to initiate the docker.

   After, you have to up your docker image. So run: "docker-compose up -d", insite this first directory "ambev-challenge"

## Second - Run the migrations!

   Inside the API directory, run: 
   1. dotnet ef migrations add InitialCreate
   2. dotnet ef database update --context DefaultContext


## What I think I achieved with this project:

1. Proficiency in C# and .NET 8.0 development ✅
2. Project Layer Separation ✅
3. Database skills with both PostgreSQL | MongoDB ✅ | ❌
4. Understanding and implementation of design patterns (e.g., Mediator pattern) ✅
5. Ability to work with object-relational mapping tools (EF Core) ✅ 
6. Proficiency in writing and maintaining unit tests using xUnit 🚧
7. Experience with mocking frameworks like NSubstitute 🚧
8. Familiarity with object mapping libraries such as AutoMapper 🚧
9. API design and RESTful service implementation ✅
10. Version control with Git  ✅
11. Understanding of both relational and non relational database systems 🚧
12. Data generation and management for testing purposes (using Faker) 🚧
13. Code organization and project structure 🚧
14. Implementation of pagination, filtering, and sorting in APIs🚧
15. Error handling and API response formatting 🚧
16. Use of Git Flow and Semantic Commits ✅
17. Performance optimization for database queries and API responses ✅
18. Understanding of asynchronous programming patterns ✅
19. Code quality and adherence to best practices ✅
20. Problem-solving and analytical skills ✅
21. Attention to detail in implementing business logic 🚧
22. Ability to work with and integrate multiple technologies and frameworks ✅

# Developer Evaluation Project

`READ CAREFULLY`

## Instructions
**The test below will have up to 7 calendar days to be delivered from the date of receipt of this manual.**

- The code must be versioned in a public Github repository and a link must be sent for evaluation once completed
- Upload this template to your repository and start working from it
- Read the instructions carefully and make sure all requirements are being addressed
- The repository must provide instructions on how to configure, execute and test the project
- Documentation and overall organization will also be taken into consideration

## Use Case
**You are a developer on the DeveloperStore team. Now we need to implement the API prototypes.**

As we work with `DDD`, to reference entities from other domains, we use the `External Identities` pattern with denormalization of entity descriptions.

Therefore, you will write an API (complete CRUD) that handles sales records. The API needs to be able to inform:

* Sale number
* Date when the sale was made
* Customer
* Total sale amount
* Branch where the sale was made
* Products
* Quantities
* Unit prices
* Discounts
* Total amount for each item
* Cancelled/Not Cancelled

It's not mandatory, but it would be a differential to build code for publishing events of:
* SaleCreated
* SaleModified
* SaleCancelled
* ItemCancelled

If you write the code, **it's not required** to actually publish to any Message Broker. You can log a message in the application log or however you find most convenient.

### Business Rules

* Purchases above 4 identical items have a 10% discount
* Purchases between 10 and 20 identical items have a 20% discount
* It's not possible to sell above 20 identical items
* Purchases below 4 items cannot have a discount

These business rules define quantity-based discounting tiers and limitations:

1. Discount Tiers:
   - 4+ items: 10% discount
   - 10-20 items: 20% discount

2. Restrictions:
   - Maximum limit: 20 items per product
   - No discounts allowed for quantities below 4 items

## Overview
This section provides a high-level overview of the project and the various skills and competencies it aims to assess for developer candidates. 

See [Overview](/.doc/overview.md)

## Tech Stack
This section lists the key technologies used in the project, including the backend, testing, frontend, and database components. 

See [Tech Stack](/.doc/tech-stack.md)

## Frameworks
This section outlines the frameworks and libraries that are leveraged in the project to enhance development productivity and maintainability. 

See [Frameworks](/.doc/frameworks.md)

<!-- 
## API Structure
This section includes links to the detailed documentation for the different API resources:
- [API General](./docs/general-api.md)
- [Products API](/.doc/products-api.md)
- [Carts API](/.doc/carts-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)
-->

## Project Structure
This section describes the overall structure and organization of the project files and directories. 

See [Project Structure](/.doc/project-structure.md)