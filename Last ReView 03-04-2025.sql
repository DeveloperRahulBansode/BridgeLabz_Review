
CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Balance DECIMAL(10,2) DEFAULT 0
);


INSERT INTO Customers VALUES('Rahul','rahul@Gmail.com',15000),('Pratik','pratik@gmail.com',12000),('Ameya','ameye@gmail.com',10000),('Pranav','pranav@gmail.com',13000)
,('Aniket','aniket@gmail.com',9000);


CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL
);

INSERT INTO Products VALUES('Fridge',12000,30),('Tv',10000,20),('AC',18000,25),('Moter',9000,22),('Fan',5000,30);

CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

--Q1--
CREATE PROCEDURE PlaceOrder @CustomerID INT,@ProductID INT,@Quantity INT
AS
BEGIN
    DECLARE @Stock INT;

    SELECT @Stock = Stock FROM Products WHERE ProductID = @ProductID;

    IF @Stock >= @Quantity
    BEGIN
        INSERT INTO Orders (CustomerID, ProductID, Quantity, OrderDate)
        VALUES (@CustomerID, @ProductID, @Quantity, GETDATE());

        UPDATE Products
        SET Stock = Stock - @Quantity
        WHERE ProductID = @ProductID;
    END
    ELSE
    BEGIN
        PRINT 'Not enough stock available.';
    END
END;

exec PlaceOrder 1,1,2

--Q2--

CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate DATE DEFAULT GETDATE(),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE PROCEDURE ProcessPayment @CustomerID INT,@Amount DECIMAL(10,2),@PaymentDate DATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Customers WHERE CustomerID = @CustomerID)
    BEGIN
        INSERT INTO Payments (CustomerID, Amount, PaymentDate)
        VALUES (@CustomerID, @Amount, @PaymentDate);

        DECLARE @OldBalance DECIMAL(10,2);
        SELECT @OldBalance = Balance FROM Customers WHERE CustomerID = @CustomerID;

        UPDATE Customers
        SET Balance = Balance - @Amount
        WHERE CustomerID = @CustomerID;

        PRINT 'Payment processed successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Customer does not exist.';
    END
END;

exec ProcessPayment 4,2500,'2025-04-03'


--Q3--

CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Position NVARCHAR(50),
    Salary DECIMAL(10,2)
);

INSERT INTO Employees VALUES('rahul','.Net Developer',1100000),('Pratik','Java Developer',900000),('Pranav','FrontEnd Developer',800000),('Krishna','C++ Developer',500000)
,('Vishal','Python Developer',70000)



CREATE TABLE EmployeeAudit (
    AuditID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT,
    OperationType NVARCHAR(10),
    OldValues NVARCHAR(MAX),
    NewValues NVARCHAR(MAX),
    ChangedAt DATETIME DEFAULT GETDATE()
);

CREATE TRIGGER EmployeeAuditTrigger
ON Employees
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        INSERT INTO EmployeeAudit (EmployeeID, OperationType, NewValues, ChangedAt)
        SELECT EmployeeID, 'INSERT', 
               CONCAT('Name: ', Name, ', Position: ', Position, ', Salary: ', Salary),
               GETDATE()
        FROM inserted;
    END

    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
    BEGIN
        INSERT INTO EmployeeAudit (EmployeeID, OperationType, OldValues, NewValues, ChangedAt)
        SELECT i.EmployeeID, 'UPDATE', 
               CONCAT('Old Name: ', d.Name, ', Old Position: ', d.Position, ', Old Salary: ', d.Salary),
               CONCAT('New Name: ', i.Name, ', New Position: ', i.Position, ', New Salary: ', i.Salary),
               GETDATE()
        FROM inserted i
        JOIN deleted d ON i.EmployeeID = d.EmployeeID;
    END

    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        INSERT INTO EmployeeAudit (EmployeeID, OperationType, OldValues, ChangedAt)
        SELECT EmployeeID, 'DELETE', 
               CONCAT('Name: ', Name, ', Position: ', Position, ', Salary: ', Salary),
               GETDATE()
        FROM deleted;
    END
END;


INSERT INTO Employees VALUES('Vinayak','C Developer',11000),('Prem','Python Developer',90000);
Update Employees set Salary=100000 where EmployeeId=7;



--@4--

DECLARE @CustomerID INT, @Amount DECIMAL(10,2), @OldBalance DECIMAL(10,2);

DECLARE payment_cursor CURSOR FOR 
SELECT CustomerID, Amount FROM Payments;

OPEN payment_cursor;

FETCH NEXT FROM payment_cursor INTO @CustomerID, @Amount;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Fetch old balance
    SELECT @OldBalance = Balance FROM Customers WHERE CustomerID = @CustomerID;

    -- Update balance
    UPDATE Customers
    SET Balance = Balance - @Amount
    WHERE CustomerID = @CustomerID;

    FETCH NEXT FROM payment_cursor INTO @CustomerID, @Amount;
END;

CLOSE payment_cursor;
DEALLOCATE payment_cursor;


--Q5--

CREATE FUNCTION CalculateAge (@DOB DATE)
RETURNS INT
AS
BEGIN
    RETURN DATEDIFF(YEAR, @DOB, GETDATE()) - 
           CASE WHEN (MONTH(@DOB) > MONTH(GETDATE())) 
                     OR (MONTH(@DOB) = MONTH(GETDATE()) AND DAY(@DOB) > DAY(GETDATE()))
                THEN 1 ELSE 0 
           END;
END;

SELECT dbo.CalculateAge('2001-11-28') as Age;

--Q6--

CREATE FUNCTION IsValidEmailOrNot (@Email NVARCHAR(255))
RETURNS BIT
AS
BEGIN
    RETURN CASE 
        WHEN @Email LIKE '_%@_%._%' AND @Email NOT LIKE '%[^a-zA-Z0-9@._-]%' THEN 1 
        ELSE 0 
    END;
END;

SELECT dbo.IsValidEmailOrNot('rahul@gmail.com') as Validation; 
SELECT dbo.IsValidEmailOrNot('@@-email') as Validation ; 




select*from Customers
select*from Products
select*from Orders
select*from Payments

select*from Employees
select*from EmployeeAudit