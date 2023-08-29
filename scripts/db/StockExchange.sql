CREATE DATABASE StockExchange;

USE StockExchange;

CREATE TABLE TradeTransactions (
    TransactionId INT PRIMARY KEY,
    Symbol NVARCHAR(10) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Shares DECIMAL(18, 4) NOT NULL,
    BrokerId INT NOT NULL,
    TradeDateTime DATETIME NOT NULL
);