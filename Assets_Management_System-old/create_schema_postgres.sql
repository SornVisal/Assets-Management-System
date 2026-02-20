-- RUN THIS SCRIPT IN YOUR POSTGRES DATABASE TOOL
-- This will create the necessary tables for the application

-- 0. Create and use OOAD schema
CREATE SCHEMA IF NOT EXISTS ooad;
SET search_path TO ooad;

-- 1. Create Users table (for Login)
-- 'Users' vs 'users' (unquoted defaults to lowercase in Postgres)
CREATE TABLE IF NOT EXISTS Users (
    Id SERIAL PRIMARY KEY,
    Username TEXT NOT NULL,
    Password TEXT NOT NULL,
    Role TEXT
);

-- Insert a default admin user if table is empty
INSERT INTO Users (Username, Password, Role)
SELECT 'admin', 'admin123', 'Admin'
WHERE NOT EXISTS (SELECT 1 FROM Users);


-- 2. Create Assets table
CREATE TABLE IF NOT EXISTS Assets (
    Id SERIAL PRIMARY KEY,
    AssetCode TEXT,
    Name TEXT NOT NULL,
    Category TEXT,
    SerialNumber TEXT,
    PurchaseDate TIMESTAMP,
    Price DECIMAL(18, 2),
    Status TEXT,
    ImagePath TEXT,
    Notes TEXT
);


-- 3. Create Transactions table
CREATE TABLE IF NOT EXISTS Transactions (
    Id SERIAL PRIMARY KEY,
    AssetId INT NOT NULL,
    TransactionType TEXT,
    EmployeeName TEXT,
    TransactionDate TIMESTAMP,
    Note TEXT,
    CONSTRAINT FK_Transactions_Assets FOREIGN KEY (AssetId) REFERENCES Assets(Id)
);
