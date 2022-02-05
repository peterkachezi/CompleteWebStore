
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/22/2021 12:44:03
-- Generated from EDMX file: D:\2021 Projects\WebStore\WebStore.Data\EDMX\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WinkadStore];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_Customers_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customers] DROP CONSTRAINT [FK_Customers_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Electronics_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Electronics] DROP CONSTRAINT [FK_Electronics_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Expenses_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expenses] DROP CONSTRAINT [FK_Expenses_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_ExpenseTypes_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExpenseTypes] DROP CONSTRAINT [FK_ExpenseTypes_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Furnitures_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Furnitures] DROP CONSTRAINT [FK_Furnitures_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Furnitures_AspNetUsers1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Furnitures] DROP CONSTRAINT [FK_Furnitures_AspNetUsers1];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sales] DROP CONSTRAINT [FK_Orders_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductTypes] DROP CONSTRAINT [FK_Product_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductPackagings_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductPackagings] DROP CONSTRAINT [FK_ProductPackagings_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesDetails_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesDetails] DROP CONSTRAINT [FK_SalesDetails_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Stock_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Stock_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Suppliers_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Suppliers] DROP CONSTRAINT [FK_Suppliers_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_UnitOfMeasures_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UnitOfMeasures] DROP CONSTRAINT [FK_UnitOfMeasures_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Vehicles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehicles] DROP CONSTRAINT [FK_Vehicles_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Companies_Countries]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Companies] DROP CONSTRAINT [FK_Companies_Countries];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerOrders_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrders] DROP CONSTRAINT [FK_CustomerOrders_Customers];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerOrders_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrders] DROP CONSTRAINT [FK_CustomerOrders_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesOrder_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sales] DROP CONSTRAINT [FK_SalesOrder_Customers];
GO
IF OBJECT_ID(N'[dbo].[FK_Expenses_ExpenseTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expenses] DROP CONSTRAINT [FK_Expenses_ExpenseTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_ProductPackagings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_ProductPackagings];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesDetails_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesDetails] DROP CONSTRAINT [FK_SalesDetails_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_UnitOfMeasures]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductTypes] DROP CONSTRAINT [FK_Products_UnitOfMeasures];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesDetails_Orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesDetails] DROP CONSTRAINT [FK_SalesDetails_Orders];
GO
IF OBJECT_ID(N'[dbo].[FK_Vehicles_VehicleMakes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehicles] DROP CONSTRAINT [FK_Vehicles_VehicleMakes];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRole];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Branches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Branches];
GO
IF OBJECT_ID(N'[dbo].[Brands]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Brands];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[Counties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Counties];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[CustomerOrders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerOrders];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[DeliveryFees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliveryFees];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Electronics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Electronics];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Expenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Expenses];
GO
IF OBJECT_ID(N'[dbo].[ExpenseTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExpenseTypes];
GO
IF OBJECT_ID(N'[dbo].[Furnitures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Furnitures];
GO
IF OBJECT_ID(N'[dbo].[Invoices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invoices];
GO
IF OBJECT_ID(N'[dbo].[MpesaExpressResponses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MpesaExpressResponses];
GO
IF OBJECT_ID(N'[dbo].[MpesaPaymentSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MpesaPaymentSettings];
GO
IF OBJECT_ID(N'[dbo].[OrderPayments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderPayments];
GO
IF OBJECT_ID(N'[dbo].[PaymentModes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentModes];
GO
IF OBJECT_ID(N'[dbo].[Payments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payments];
GO
IF OBJECT_ID(N'[dbo].[ProductPackagings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductPackagings];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ProductTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductTypes];
GO
IF OBJECT_ID(N'[dbo].[Returns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Returns];
GO
IF OBJECT_ID(N'[dbo].[Sales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sales];
GO
IF OBJECT_ID(N'[dbo].[SalesDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesDetails];
GO
IF OBJECT_ID(N'[dbo].[SmsResponses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SmsResponses];
GO
IF OBJECT_ID(N'[dbo].[Suppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Suppliers];
GO
IF OBJECT_ID(N'[dbo].[Taxes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Taxes];
GO
IF OBJECT_ID(N'[dbo].[UnitOfMeasures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UnitOfMeasures];
GO
IF OBJECT_ID(N'[dbo].[VehicleMakes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VehicleMakes];
GO
IF OBJECT_ID(N'[dbo].[Vehicles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehicles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [CreateDate] datetime  NOT NULL,
    [IsApproved] tinyint  NULL,
    [RoleId] nvarchar(max)  NULL,
    [AccountStatus] bigint  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Branches'
CREATE TABLE [dbo].[Branches] (
    [Id] uniqueidentifier  NOT NULL,
    [StoreName] nvarchar(100)  NULL,
    [Town] nvarchar(100)  NULL,
    [CountyId] uniqueidentifier  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Brands'
CREATE TABLE [dbo].[Brands] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(150)  NULL,
    [Address] nvarchar(150)  NULL,
    [PhoneNumber] nvarchar(150)  NULL,
    [Email] nvarchar(150)  NULL,
    [Website] nvarchar(150)  NULL,
    [PhysicalAddress] nvarchar(150)  NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [CountryId] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Counties'
CREATE TABLE [dbo].[Counties] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [CreateDate] datetime  NULL
);
GO

-- Creating table 'CustomerOrders'
CREATE TABLE [dbo].[CustomerOrders] (
    [Id] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL,
    [OrderNumber] nvarchar(50)  NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [OrderNotes] nvarchar(max)  NULL,
    [CustomerId] uniqueidentifier  NOT NULL,
    [Quantity] int  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(100)  NULL,
    [MiddleName] nvarchar(100)  NULL,
    [LastName] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NOT NULL,
    [PhoneNumber] nvarchar(100)  NULL,
    [CreatedBy] nvarchar(128)  NULL,
    [CustomerNumber] nvarchar(100)  NOT NULL,
    [DeliveryLocation] nvarchar(100)  NULL,
    [Apartment] nvarchar(100)  NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'DeliveryFees'
CREATE TABLE [dbo].[DeliveryFees] (
    [Id] nchar(10)  NOT NULL,
    [Amount] decimal(18,2)  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Electronics'
CREATE TABLE [dbo].[Electronics] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [ModelNumber] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [Status] bigint  NOT NULL,
    [SerialNumber] nvarchar(max)  NOT NULL,
    [UpdatedBy] nvarchar(128)  NOT NULL,
    [UpdatedDate] datetime  NOT NULL,
    [Quantity] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Position] nvarchar(100)  NULL,
    [Location] nvarchar(100)  NULL,
    [Age] nvarchar(100)  NULL,
    [CreateDate] datetime  NULL,
    [StartDate] datetime  NULL,
    [Department] nvarchar(100)  NULL,
    [Salary] int  NULL,
    [StartDateString] nvarchar(100)  NULL,
    [CreatedBy] datetime  NULL
);
GO

-- Creating table 'Expenses'
CREATE TABLE [dbo].[Expenses] (
    [Id] uniqueidentifier  NOT NULL,
    [ExpenseTypeId] uniqueidentifier  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [ExpenseDate] datetime  NOT NULL,
    [IsRecurring] bigint  NOT NULL,
    [RecurringExpense] nvarchar(50)  NULL,
    [Description] nvarchar(100)  NOT NULL,
    [ReceiptAttachment] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [ReceiptAttachmentDesc] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ExpenseTypes'
CREATE TABLE [dbo].[ExpenseTypes] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Furnitures'
CREATE TABLE [dbo].[Furnitures] (
    [Id] uniqueidentifier  NOT NULL,
    [ItemName] nvarchar(50)  NOT NULL,
    [ItemNumber] nvarchar(max)  NOT NULL,
    [Manufacturer] nvarchar(50)  NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Quantity] nvarchar(50)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [Status] bigint  NOT NULL,
    [UpdatedBy] nvarchar(128)  NOT NULL,
    [UpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'Invoices'
CREATE TABLE [dbo].[Invoices] (
    [Id] uniqueidentifier  NOT NULL,
    [CustomerId] uniqueidentifier  NOT NULL,
    [OrderId] uniqueidentifier  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'MpesaExpressResponses'
CREATE TABLE [dbo].[MpesaExpressResponses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MerchantRequestID] nvarchar(50)  NULL,
    [CheckoutRequestID] nvarchar(50)  NULL,
    [ResponseCode] nvarchar(50)  NULL,
    [ResponseDescription] nvarchar(50)  NULL,
    [CustomerMessage] nvarchar(50)  NULL
);
GO

-- Creating table 'MpesaPaymentSettings'
CREATE TABLE [dbo].[MpesaPaymentSettings] (
    [Id] uniqueidentifier  NOT NULL,
    [MpesaKey] nvarchar(max)  NOT NULL,
    [Secrete] nvarchar(max)  NOT NULL,
    [BusinessShortCode] nvarchar(max)  NOT NULL,
    [passkey] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [UpdatedBy] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'OrderPayments'
CREATE TABLE [dbo].[OrderPayments] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] uniqueidentifier  NULL,
    [Amount] decimal(18,2)  NULL,
    [MpesaReference] varchar(50)  NULL,
    [PhoneNumber] varchar(50)  NULL,
    [MpesaCheckoutRequestId] varchar(50)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdateBy] varchar(50)  NULL,
    [UpdatedDate] datetime  NULL,
    [PaymentMode] tinyint  NULL,
    [ResponseCode] nvarchar(50)  NULL,
    [ResponseDescription] nvarchar(50)  NULL,
    [ResultCode] nvarchar(50)  NULL,
    [ResultDescription] nvarchar(50)  NULL,
    [TotalAmountPaidInMpesa] decimal(18,2)  NULL,
    [InsuranceAmount] decimal(18,2)  NULL
);
GO

-- Creating table 'PaymentModes'
CREATE TABLE [dbo].[PaymentModes] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MerchantRequestID] nvarchar(50)  NULL,
    [CheckoutRequestID] nvarchar(50)  NULL,
    [ResultCode] int  NULL,
    [ResultDesc] nvarchar(50)  NULL,
    [Amount] decimal(18,0)  NULL,
    [TransactionNumber] nvarchar(50)  NULL,
    [Balance] decimal(18,0)  NULL,
    [TransactionDate] nvarchar(50)  NULL,
    [PhoneNumber] nvarchar(50)  NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL
);
GO

-- Creating table 'ProductPackagings'
CREATE TABLE [dbo].[ProductPackagings] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Unit] nvarchar(50)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [ProductCode] nvarchar(max)  NOT NULL,
    [Quantity] int  NOT NULL,
    [CostPrice] decimal(18,2)  NOT NULL,
    [SellingPrice] decimal(18,2)  NOT NULL,
    [ReOrderLevel] int  NOT NULL,
    [ExpectedProfit] decimal(18,2)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [UpdateBy] nvarchar(128)  NOT NULL,
    [UpdatedDate] datetime  NOT NULL,
    [Status] bit  NOT NULL,
    [PackagingId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ProductTypes'
CREATE TABLE [dbo].[ProductTypes] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [UnitOfMeasureId] uniqueidentifier  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [UpdatedBy] nvarchar(128)  NOT NULL,
    [UpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'Returns'
CREATE TABLE [dbo].[Returns] (
    [Id] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL,
    [Quantity] int  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [CreateBy] nvarchar(128)  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Sales'
CREATE TABLE [dbo].[Sales] (
    [Id] uniqueidentifier  NOT NULL,
    [CustomerId] uniqueidentifier  NULL,
    [OrderDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [TotalAmount] decimal(18,2)  NOT NULL,
    [OrderNumber] nvarchar(max)  NOT NULL,
    [Change] decimal(18,2)  NOT NULL,
    [CashPaid] decimal(18,2)  NOT NULL
);
GO

-- Creating table 'SalesDetails'
CREATE TABLE [dbo].[SalesDetails] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL,
    [Quantity] int  NOT NULL,
    [SellingPrice] decimal(18,2)  NOT NULL,
    [Discount] decimal(18,2)  NOT NULL,
    [Total] decimal(18,2)  NOT NULL,
    [OrderNumber] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [TaxAmount] nvarchar(100)  NULL,
    [PaymentStatus] int  NULL
);
GO

-- Creating table 'SmsResponses'
CREATE TABLE [dbo].[SmsResponses] (
    [Id] uniqueidentifier  NOT NULL,
    [Number] nvarchar(150)  NOT NULL,
    [Status] nvarchar(150)  NOT NULL,
    [MessageId] nvarchar(150)  NOT NULL,
    [Cost] nvarchar(150)  NOT NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'Suppliers'
CREATE TABLE [dbo].[Suppliers] (
    [Id] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(100)  NOT NULL,
    [MiddleName] nvarchar(100)  NULL,
    [LastName] nvarchar(100)  NOT NULL,
    [Email] nvarchar(100)  NULL,
    [PhoneNumber] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [Website] nvarchar(100)  NULL,
    [AttachmentFileName] nvarchar(100)  NULL,
    [CountryId] uniqueidentifier  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [SupplierNumber] nvarchar(100)  NULL
);
GO

-- Creating table 'Taxes'
CREATE TABLE [dbo].[Taxes] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'UnitOfMeasures'
CREATE TABLE [dbo].[UnitOfMeasures] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Unit] nvarchar(50)  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'VehicleMakes'
CREATE TABLE [dbo].[VehicleMakes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Vehicles'
CREATE TABLE [dbo].[Vehicles] (
    [Id] uniqueidentifier  NOT NULL,
    [ModelName] nvarchar(max)  NOT NULL,
    [ModelYear] nvarchar(max)  NOT NULL,
    [PlateNumber] nvarchar(max)  NOT NULL,
    [MakeId] int  NOT NULL,
    [Capacity] int  NOT NULL,
    [Owner] nvarchar(50)  NOT NULL,
    [DatePurchased] datetime  NOT NULL,
    [Status] bigint  NOT NULL,
    [CreatedBy] nvarchar(128)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [UpdatedBy] nvarchar(128)  NOT NULL,
    [UpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Branches'
ALTER TABLE [dbo].[Branches]
ADD CONSTRAINT [PK_Branches]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Brands'
ALTER TABLE [dbo].[Brands]
ADD CONSTRAINT [PK_Brands]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Counties'
ALTER TABLE [dbo].[Counties]
ADD CONSTRAINT [PK_Counties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerOrders'
ALTER TABLE [dbo].[CustomerOrders]
ADD CONSTRAINT [PK_CustomerOrders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeliveryFees'
ALTER TABLE [dbo].[DeliveryFees]
ADD CONSTRAINT [PK_DeliveryFees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Electronics'
ALTER TABLE [dbo].[Electronics]
ADD CONSTRAINT [PK_Electronics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Expenses'
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [PK_Expenses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExpenseTypes'
ALTER TABLE [dbo].[ExpenseTypes]
ADD CONSTRAINT [PK_ExpenseTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Furnitures'
ALTER TABLE [dbo].[Furnitures]
ADD CONSTRAINT [PK_Furnitures]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [PK_Invoices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MpesaExpressResponses'
ALTER TABLE [dbo].[MpesaExpressResponses]
ADD CONSTRAINT [PK_MpesaExpressResponses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MpesaPaymentSettings'
ALTER TABLE [dbo].[MpesaPaymentSettings]
ADD CONSTRAINT [PK_MpesaPaymentSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderPayments'
ALTER TABLE [dbo].[OrderPayments]
ADD CONSTRAINT [PK_OrderPayments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentModes'
ALTER TABLE [dbo].[PaymentModes]
ADD CONSTRAINT [PK_PaymentModes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductPackagings'
ALTER TABLE [dbo].[ProductPackagings]
ADD CONSTRAINT [PK_ProductPackagings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [PK_ProductTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Returns'
ALTER TABLE [dbo].[Returns]
ADD CONSTRAINT [PK_Returns]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [PK_Sales]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SalesDetails'
ALTER TABLE [dbo].[SalesDetails]
ADD CONSTRAINT [PK_SalesDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SmsResponses'
ALTER TABLE [dbo].[SmsResponses]
ADD CONSTRAINT [PK_SmsResponses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [PK_Suppliers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Taxes'
ALTER TABLE [dbo].[Taxes]
ADD CONSTRAINT [PK_Taxes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UnitOfMeasures'
ALTER TABLE [dbo].[UnitOfMeasures]
ADD CONSTRAINT [PK_UnitOfMeasures]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VehicleMakes'
ALTER TABLE [dbo].[VehicleMakes]
ADD CONSTRAINT [PK_VehicleMakes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Vehicles'
ALTER TABLE [dbo].[Vehicles]
ADD CONSTRAINT [PK_Vehicles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [CreatedBy] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_Customers_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Customers_AspNetUsers'
CREATE INDEX [IX_FK_Customers_AspNetUsers]
ON [dbo].[Customers]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Electronics'
ALTER TABLE [dbo].[Electronics]
ADD CONSTRAINT [FK_Electronics_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Electronics_AspNetUsers'
CREATE INDEX [IX_FK_Electronics_AspNetUsers]
ON [dbo].[Electronics]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Expenses'
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [FK_Expenses_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Expenses_AspNetUsers'
CREATE INDEX [IX_FK_Expenses_AspNetUsers]
ON [dbo].[Expenses]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'ExpenseTypes'
ALTER TABLE [dbo].[ExpenseTypes]
ADD CONSTRAINT [FK_ExpenseTypes_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExpenseTypes_AspNetUsers'
CREATE INDEX [IX_FK_ExpenseTypes_AspNetUsers]
ON [dbo].[ExpenseTypes]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Furnitures'
ALTER TABLE [dbo].[Furnitures]
ADD CONSTRAINT [FK_Furnitures_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Furnitures_AspNetUsers'
CREATE INDEX [IX_FK_Furnitures_AspNetUsers]
ON [dbo].[Furnitures]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Furnitures'
ALTER TABLE [dbo].[Furnitures]
ADD CONSTRAINT [FK_Furnitures_AspNetUsers1]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Furnitures_AspNetUsers1'
CREATE INDEX [IX_FK_Furnitures_AspNetUsers1]
ON [dbo].[Furnitures]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_Orders_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_AspNetUsers'
CREATE INDEX [IX_FK_Orders_AspNetUsers]
ON [dbo].[Sales]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [FK_Product_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_AspNetUsers'
CREATE INDEX [IX_FK_Product_AspNetUsers]
ON [dbo].[ProductTypes]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'ProductPackagings'
ALTER TABLE [dbo].[ProductPackagings]
ADD CONSTRAINT [FK_ProductPackagings_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPackagings_AspNetUsers'
CREATE INDEX [IX_FK_ProductPackagings_AspNetUsers]
ON [dbo].[ProductPackagings]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'SalesDetails'
ALTER TABLE [dbo].[SalesDetails]
ADD CONSTRAINT [FK_SalesDetails_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesDetails_AspNetUsers'
CREATE INDEX [IX_FK_SalesDetails_AspNetUsers]
ON [dbo].[SalesDetails]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Stock_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Stock_AspNetUsers'
CREATE INDEX [IX_FK_Stock_AspNetUsers]
ON [dbo].[Products]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [FK_Suppliers_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Suppliers_AspNetUsers'
CREATE INDEX [IX_FK_Suppliers_AspNetUsers]
ON [dbo].[Suppliers]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'UnitOfMeasures'
ALTER TABLE [dbo].[UnitOfMeasures]
ADD CONSTRAINT [FK_UnitOfMeasures_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UnitOfMeasures_AspNetUsers'
CREATE INDEX [IX_FK_UnitOfMeasures_AspNetUsers]
ON [dbo].[UnitOfMeasures]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Vehicles'
ALTER TABLE [dbo].[Vehicles]
ADD CONSTRAINT [FK_Vehicles_AspNetUsers]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vehicles_AspNetUsers'
CREATE INDEX [IX_FK_Vehicles_AspNetUsers]
ON [dbo].[Vehicles]
    ([CreatedBy]);
GO

-- Creating foreign key on [CountryId] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [FK_Companies_Countries]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Companies_Countries'
CREATE INDEX [IX_FK_Companies_Countries]
ON [dbo].[Companies]
    ([CountryId]);
GO

-- Creating foreign key on [CustomerId] in table 'CustomerOrders'
ALTER TABLE [dbo].[CustomerOrders]
ADD CONSTRAINT [FK_CustomerOrders_Customers]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrders_Customers'
CREATE INDEX [IX_FK_CustomerOrders_Customers]
ON [dbo].[CustomerOrders]
    ([CustomerId]);
GO

-- Creating foreign key on [ProductId] in table 'CustomerOrders'
ALTER TABLE [dbo].[CustomerOrders]
ADD CONSTRAINT [FK_CustomerOrders_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrders_Products'
CREATE INDEX [IX_FK_CustomerOrders_Products]
ON [dbo].[CustomerOrders]
    ([ProductId]);
GO

-- Creating foreign key on [CustomerId] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_SalesOrder_Customers]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesOrder_Customers'
CREATE INDEX [IX_FK_SalesOrder_Customers]
ON [dbo].[Sales]
    ([CustomerId]);
GO

-- Creating foreign key on [ExpenseTypeId] in table 'Expenses'
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [FK_Expenses_ExpenseTypes]
    FOREIGN KEY ([ExpenseTypeId])
    REFERENCES [dbo].[ExpenseTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Expenses_ExpenseTypes'
CREATE INDEX [IX_FK_Expenses_ExpenseTypes]
ON [dbo].[Expenses]
    ([ExpenseTypeId]);
GO

-- Creating foreign key on [PackagingId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_ProductPackagings]
    FOREIGN KEY ([PackagingId])
    REFERENCES [dbo].[ProductPackagings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_ProductPackagings'
CREATE INDEX [IX_FK_Products_ProductPackagings]
ON [dbo].[Products]
    ([PackagingId]);
GO

-- Creating foreign key on [ProductId] in table 'SalesDetails'
ALTER TABLE [dbo].[SalesDetails]
ADD CONSTRAINT [FK_SalesDetails_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesDetails_Products'
CREATE INDEX [IX_FK_SalesDetails_Products]
ON [dbo].[SalesDetails]
    ([ProductId]);
GO

-- Creating foreign key on [UnitOfMeasureId] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [FK_Products_UnitOfMeasures]
    FOREIGN KEY ([UnitOfMeasureId])
    REFERENCES [dbo].[UnitOfMeasures]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_UnitOfMeasures'
CREATE INDEX [IX_FK_Products_UnitOfMeasures]
ON [dbo].[ProductTypes]
    ([UnitOfMeasureId]);
GO

-- Creating foreign key on [OrderId] in table 'SalesDetails'
ALTER TABLE [dbo].[SalesDetails]
ADD CONSTRAINT [FK_SalesDetails_Orders]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Sales]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesDetails_Orders'
CREATE INDEX [IX_FK_SalesDetails_Orders]
ON [dbo].[SalesDetails]
    ([OrderId]);
GO

-- Creating foreign key on [MakeId] in table 'Vehicles'
ALTER TABLE [dbo].[Vehicles]
ADD CONSTRAINT [FK_Vehicles_VehicleMakes]
    FOREIGN KEY ([MakeId])
    REFERENCES [dbo].[VehicleMakes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vehicles_VehicleMakes'
CREATE INDEX [IX_FK_Vehicles_VehicleMakes]
ON [dbo].[Vehicles]
    ([MakeId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUser'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUser]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------