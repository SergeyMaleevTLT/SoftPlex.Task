DROP DATABASE TestDb;

GO

/*-----1.-------*/
CREATE DATABASE TestDb;

GO

USE TestDb;

GO

/*-----2.-------*/
CREATE TABLE Product (
                         ID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
                         Name NVARCHAR(255) NOT NULL UNIQUE,
                         Description NVARCHAR(MAX),
);

CREATE NONCLUSTERED INDEX IX_Product_Name ON Product(Name) 
       WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);

GO

/*-----3.-------*/
CREATE TABLE ProductVersion (
                                ID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
                                ProductID UNIQUEIDENTIFIER NOT NULL,
                                Name NVARCHAR(255) NOT NULL,
                                Description NVARCHAR(MAX),
                                CreatingDate DATETIME NOT NULL DEFAULT GETDATE(),
                                Width FLOAT NOT NULL,
                                Height FLOAT NOT NULL,
                                Length FLOAT NOT NULL,
                                CONSTRAINT FK_ProductVersion_Product FOREIGN KEY (ProductID) REFERENCES Product(ID) ON DELETE CASCADE
);

CREATE NONCLUSTERED INDEX IX_ProductVersion_Name ON ProductVersion(Name)
       WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
CREATE NONCLUSTERED INDEX IX_ProductVersion_CreatingDate ON ProductVersion(CreatingDate)
       WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
CREATE NONCLUSTERED INDEX IX_ProductVersion_Width ON ProductVersion(Width) 
       WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
CREATE NONCLUSTERED INDEX IX_ProductVersion_Height ON ProductVersion(Height) 
       WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
CREATE NONCLUSTERED INDEX IX_ProductVersion_Length ON ProductVersion(Length) 
       WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);

GO

/*-----4.-------*/
CREATE TABLE EventLog (
                          ID UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
                          EventDate DATETIME NOT NULL DEFAULT GETDATE(),
                          Description NVARCHAR(MAX),
);

CREATE NONCLUSTERED INDEX IX_EventLog_EventDate ON EventLog(EventDate)
       WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
GO
       
       
/*-----5.-------*/

CREATE TRIGGER trg_Product_Audit
    ON Product
    AFTER INSERT, UPDATE, DELETE
    AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EventDescription NVARCHAR(MAX);

    IF EXISTS (SELECT * FROM inserted)
BEGIN
        SET @EventDescription = 'Product record(s) inserted/updated.';
END
ELSE
BEGIN
        SET @EventDescription = 'Product record(s) deleted.';
END

INSERT INTO EventLog (ID, EventDate, Description)
VALUES (NEWID(), GETDATE(), @EventDescription);
END
GO

CREATE TRIGGER trg_ProductVersion_Audit
    ON ProductVersion
    AFTER INSERT, UPDATE, DELETE
    AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EventDescription NVARCHAR(MAX);

    IF EXISTS (SELECT * FROM inserted)
BEGIN
        SET @EventDescription = 'ProductVersion record(s) inserted/updated.';
END
ELSE
BEGIN
        SET @EventDescription = 'ProductVersion record(s) deleted.';
END

INSERT INTO EventLog (ID, EventDate, Description)
VALUES (NEWID(), GETDATE(), @EventDescription);
END
GO

/*-----6.-------*/
CREATE FUNCTION SearchProductVersions
(
    @ProductName NVARCHAR(100),
    @VersionName NVARCHAR(100),
    @MinVolume FLOAT,
    @MaxVolume FLOAT
)
    RETURNS TABLE
    AS
RETURN
(
    SELECT pv.ID, 
	p.Name AS ProductName, 
	pv.Name AS VersionName, 
	pv.Description, 
	pv.Width, 
	pv.Height, 
	pv.Length
	
    FROM Product p
    JOIN ProductVersion pv ON p.ID = pv.ProductID
    WHERE p.Name LIKE @ProductName
    AND pv.Name LIKE @VersionName
    AND (@MinVolume IS NULL OR (pv.Width * pv.Height * pv.Length) >= @MinVolume)
    AND (@MaxVolume IS NULL OR (pv.Width * pv.Height * pv.Length) <= @MaxVolume)
);

GO

/*-----7.-------*/
INSERT INTO Product (ID, Name, Description)
VALUES
    (NEWID(), 'Chair', 'Comfortable and stylish armchair'),
    (NEWID(), 'Table', 'A sturdy and elegant table'),
    (NEWID(), 'Sofa', 'A soft and cozy sofa'),
    (NEWID(), 'Wardrobe', 'Spacious storage cabinet'),
    (NEWID(), 'Armchair', 'A comfortable lounge chair'),
    (NEWID(), 'Bed', 'Comfortable bed for sleeping'),
    (NEWID(), 'Shelf', 'Functional bookshelf and decor'),
    (NEWID(), 'Rocking chair', 'Modern rocking chair'),
    (NEWID(), 'Desk', 'A comfortable desk for work and study'),
    (NEWID(), 'Padded stool', 'A soft and stylish padded stool for relaxing');


INSERT INTO ProductVersion (ID, ProductID, Name, Description, Width, Height, Length)
VALUES
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Chair'), 'Classic', 'Classic chair', 45.0, 55.0, 50.0),
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Table'), 'Oak', 'Wooden table made of oak', 120.0, 70.0, 90.0),
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Sofa'), 'Luxury', 'Luxury sofa with upholstered upholstery', 220.0, 100.0, 80.0),
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Wardrobe'), 'Wardrobe classic', 'Spacious wardrobe', 180.0, 200.0, 60.0),
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Armchair'), 'Recliner', 'Ergonomic relaxation chair', 80.0, 90.0, 100.0),
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Bed'), 'King Size', 'King Size bed with orthopedic mattress', 200.0, 180.0, 160.0),
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Shelf'), 'Modern', 'Modern metal and glass shelving', 100.0, 220.0, 40.0),
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Rocking chair'), 'Classic', 'Comfortable rocking chair with armrests', 60.0, 70.0, 80.0),
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Desk'), 'Workspace', 'Work desk with drawers', 150.0, 80.0, 70.0),
    (NEWID(), (SELECT ID FROM Product WHERE Name = 'Padded stool'), 'Round', 'Round pouf with textile upholstery', 50.0, 50.0, 50.0);