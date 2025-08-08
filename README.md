# ISPARK Mobile Application Backend Service (.NET Core Web API)

This project is a .NET Core Web API project that I developed during my internship at ISPARK. The main purpose of the project is to provide a backend service for ISPARK's mobile or web applications. Through this service, users can log into the system, view current campaigns, and read news. dbForge Studio for MySQL was used as the database, and the project was designed in accordance with _N-Tier (Multi-Layer Architecture)_ principles. This ensures a sustainable, manageable, and modular structure.

## 🏗 Architecture (N-Tier Architecture)

The project was developed using a layered architecture to ensure a clear separation of responsibilities and to build a modular structure. This approach improves code readability, testability, and ease of maintenance.

The _Presentation Layer (Controllers)_ is the layer where the API communicates with the outside world. It receives incoming HTTP requests, calls the relevant business layer services, and returns the results as HTTP responses in JSON format. Controllers such as _AuthController_ and _GetCampaignController_ are located in this layer.

The _Business Logic Layer (Business/Services)_ is where the core logic and business rules of the application reside. Operations such as validating incoming data, processing data retrieved from the data access layer, and preparing it for the presentation layer are performed here. Components such as _UserService_, _CampaignService_, and the specially developed _LoggerService_ are part of this layer.

The _Data Access Layer (Datas/Repositories)_ communicates directly with the database. Using the _Repository Pattern_, database operations are abstracted. _Entity Framework Core_ is used to connect to the MySQL database.

The _Model Layer (Model)_ contains the data models used throughout the application. This layer consists of two types of models. The Entities section includes the main entities that directly represent the database tables, such as _Campaign_, _News_, and _Users_. The _DTOs (Data Transfer Objects)_ section contains special models used for data exchange via the API, such as _CampaignListDto_ and _UserLoginDto_, which include only the necessary fields.

## 🔑 Key Features

The _Authentication & Authorization (JWT)_ mechanism provides a JSON Web Token–based authentication system for user login operations. When a user successfully logs in, a unique token is generated, which is then used for subsequent requests that require authorization. Specific endpoints, such as those for viewing campaign and news details, are protected with the [Authorize] attribute.

_Centralized Error Handling_ is implemented through a custom _ExceptionHandlingMiddleware_. This middleware ensures that unexpected errors in the application are returned to the client in a standard and understandable JSON format. The _ErrorCodes_ and _ErrorMessages_ classes provide consistent error management.

_Advanced Logging_ infrastructure is implemented to monitor the system’s operation and identify potential issues in detail. The _RequestResponseLoggingMiddleware_ logs all incoming requests and outgoing responses along with their duration. For security reasons, sensitive information such as passwords and tokens is masked in the logs. The _LoggerService_ logs specific business events such as user login attempts, page views, and data listings.

_Dependency Injection (DI)_ is used throughout the project for dependency management, utilizing ASP.NET Core’s built-in mechanism. This keeps the dependencies between layers and services loosely coupled.

The _Repository Pattern_ is used to separate the data access logic from the business logic. Communication is provided through interfaces such as _IUserRepository_ and _CampaignRepository_, creating a flexible structure in case the database technology needs to be changed in the future.
