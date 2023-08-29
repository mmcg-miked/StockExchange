# Introduction 
London Stock Exchange API that gives authorised brokers the ability to submit real-time trade notifications and provide them with up to date stock prices.

This README will guide you through the features, system design, data models, enhancements, and future options of the API.


# System Overview

## Components 
The London Stock Exchange API comprises the following key components:

**API Layer:** This layer handles incoming HTTP requests with minor validation. It exposes two endpoints:
- **POST api/v1/trades:** Authorized brokers can submit trade notifications.
- **GET api/v1/stocks:** Provides stock-related information, including the current stock value, values for all stocks, and values for a range of stocks (average price).

The API contains a basic Swagger doc describing the endpoints and models

**Service Layer:** Responsible data processing, orchestration, price calculation and response models. This layer consists of 2 parts - the interfacing from the API layer and a domain layer (for reusablity) that calls out to the database layer using the repository pattern. 

**Database Layer:** Utilizes Microsoft SQL Server Express as the relational database to store transaction data. It can be replaced with other relational databases effortlessly. Entity Framework Core facilitates object-relational mapping. For simplicity the database only consists of a single table for storing trades and stock prices. 

The app also consists of a 'core' component (shared library) that could provide some common fetaures like reusable extention methods, an API response builder to ensure any and all API responses conform to the same structure and a basic validation filter to ensure all models conform to the masic structure and contraints.

## Data Model
The core data models shared across the application consists of two main entities:

**TradeTransaction:**
- *TransactionId* (int, unique value of the transaction and primary key)
- *Symbol* (string, ticker symbol)
- *Price* (decimal, trade price)
- *Shares* (decimal, number of shares)
- *BrokerId* (int, unique ID of the authorized broker)
- *TradeDateTime* (dateTime, trade timestamp)

**StockValue:**
- *Symbol* (string, ticker symbol)
- *Value* (decimal, average stock value across transactions)

# Enhancements
For enhanced performance, security, and scalability, the recommended approach is to deploy the API to Azure using the following components:

**Azure Active Directory (Azure AD):** For robust authentication and authorization, leverage Azure AD with an app registration containing read/write roles.

**Azure Key Vault:** Securely manage and retrieve secrets using Azure Key Vault.

**Azure App Service:** Utilize Azure App Service for automatic scaling, resiliency, easy authentication, and other security features.

**Azure API Management (APIM):** Employ APIM as a front-end facade to the backend service, offering credential verification, rate limits, caching, metrics, logs, traces, and message validation.

**Azure SQL Database:** Store transaction and stock data in Azure SQL Database, providing reliability and performance.

**Application Insights:** Enhance logging, application performance monitoring, diagnostics, and dashboards using Application Insights.



### Additional enhancements include:
1. Integrate middleware functionality for claim token validation to ensure proper authorization.
2. Improve message models by using GUIDs for TransactionId and BrokerId.
3. Utilize an enum for the Symbol field, validating against known ticker symbols.
4. Database design for normalised data.
5. Segregate API and database models, introducing mappers for seamless translation.
6. Enhance code documentation with XML comments to improve Swagger documentation and developer experience.
7. Implement integration tests to ensure robust functionality.
8. Refactor project structure in alignment with the 'Clean Architecture' principles for better maintainability and separation of concerns.
