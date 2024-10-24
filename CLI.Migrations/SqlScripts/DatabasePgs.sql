/*-----1.-------*/
CREATE DATABASE TestDb;


/*-----2.-------*/
CREATE TABLE Product (
                         ID UUID DEFAULT gen_random_uuid() PRIMARY KEY NOT NULL,
                         Name VARCHAR(255) NOT NULL UNIQUE,
                         Description TEXT
);

CREATE INDEX IX_Product_Name ON Product(Name)


/*-----3.-------*/
CREATE TABLE ProductVersion (
                                ID UUID PRIMARY KEY NOT NULL,
                                ProductID UUID NOT NULL,
                                Name VARCHAR(255) NOT NULL,
                                Description TEXT,
                                CreatingDate DATE NOT NULL DEFAULT CURRENT_DATE,
                                Width FLOAT NOT NULL,
                                Height FLOAT NOT NULL,
                                Length FLOAT NOT NULL,
                                CONSTRAINT FK_ProductVersion_Product FOREIGN KEY (ProductID) REFERENCES Product(ID) ON DELETE CASCADE
);

CREATE INDEX IX_ProductVersion_Name ON ProductVersion(Name);
CREATE INDEX IX_ProductVersion_CreatingDate ON ProductVersion(CreatingDate);
CREATE INDEX IX_ProductVersion_Width ON ProductVersion(Width);
CREATE INDEX IX_ProductVersion_Height ON ProductVersion(Height);
CREATE INDEX IX_ProductVersion_Length ON ProductVersion(Length);

/*-----4.-------*/
CREATE TABLE EventLog (
                          ID UUID PRIMARY KEY NOT NULL,
                          EventDate DATE NOT NULL DEFAULT CURRENT_DATE,
                          Description TEXT
);

CREATE INDEX IX_EventLog_EventDate ON EventLog(EventDate)


/*-----5.-------*/

CREATE OR REPLACE FUNCTION audit_information() RETURNS TRIGGER AS
$$
BEGIN

IF (TG_OP = 'INSERT') THEN
   INSERT INTO EventLog (ID, EventDate, Description) VALUES (gen_random_uuid(), CURRENT_DATE, CONCAT (TG_OP, ' Product ID: ', New.ID, ' Name: ', New.Name, ' Description: ', New.Description));
ELSE
   INSERT INTO EventLog (ID, EventDate, Description) VALUES (gen_random_uuid(), CURRENT_DATE, CONCAT (TG_OP, ' Product ID: ', OLD.ID, ' Name: ', OLD.Name, ' Description: ', OLD.Description));
END IF;
RETURN NULL;
END;
$$
language plpgsql;


CREATE TRIGGER tr_AuditProduct
    AFTER INSERT OR UPDATE OR DELETE ON Product
    FOR EACH ROW
    execute procedure audit_information();

CREATE TRIGGER tr_AuditProductVersion
    AFTER INSERT OR UPDATE OR DELETE ON ProductVersion
    FOR EACH ROW
    execute procedure audit_information();


/*-----6.-------*/
CREATE FUNCTION SearchProductVersions
(
    IN ProductName VARCHAR(255),
    IN VersionName VARCHAR(255),
    IN MinVolume FLOAT,
    IN MaxVolume FLOAT,
    OUT ID UUID,
    OUT ProductName_Result VARCHAR(255),
    OUT VersionName_Result VARCHAR(255),
    OUT Width FLOAT,
    OUT Length FLOAT,
    OUT Height FLOAT
)
    LANGUAGE 'sql'
  AS 
  $$

SELECT
    pv.ID,
    p.Name AS ProductName_Result,
    pv.Name AS ProductVersionName_Result,
    pv.Width,
    pv.Length,
    pv.Height

FROM Product p
         JOIN ProductVersion pv ON p.ID = pv.ProductID
WHERE p.Name ILIKE ProductName
    AND pv.Name ILIKE VersionName
    AND (MinVolume IS NULL OR (pv.Width * pv.Height * pv.Length) >= MinVolume)
    AND (MaxVolume IS NULL OR (pv.Width * pv.Height * pv.Length) <= MaxVolume);

$$;


/*-----7.-------*/
INSERT INTO Product (ID, Name, Description)
VALUES
    (gen_random_uuid(), 'Chair', 'Comfortable and stylish armchair'),
    (gen_random_uuid(), 'Table', 'A sturdy and elegant table'),
    (gen_random_uuid(), 'Sofa', 'A soft and cozy sofa'),
    (gen_random_uuid(), 'Wardrobe', 'Spacious storage cabinet'),
    (gen_random_uuid(), 'Armchair', 'A comfortable lounge chair'),
    (gen_random_uuid(), 'Bed', 'Comfortable bed for sleeping'),
    (gen_random_uuid(), 'Shelf', 'Functional bookshelf and decor'),
    (gen_random_uuid(), 'Rocking chair', 'Modern rocking chair'),
    (gen_random_uuid(), 'Desk', 'A comfortable desk for work and study'),
    (gen_random_uuid(), 'Padded stool', 'A soft and stylish padded stool for relaxing');


INSERT INTO ProductVersion (ID, ProductID, Name, Description, Width, Height, Length)
VALUES
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Chair'), 'Classic', 'Classic chair', 45.0, 55.0, 50.0),
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Table'), 'Oak', 'Wooden table made of oak', 120.0, 70.0, 90.0),
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Sofa'), 'Luxury', 'Luxury sofa with upholstered upholstery', 220.0, 100.0, 80.0),
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Wardrobe'), 'Wardrobe classic', 'Spacious wardrobe', 180.0, 200.0, 60.0),
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Armchair'), 'Recliner', 'Ergonomic relaxation chair', 80.0, 90.0, 100.0),
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Bed'), 'King Size', 'King Size bed with orthopedic mattress', 200.0, 180.0, 160.0),
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Shelf'), 'Modern', 'Modern metal and glass shelving', 100.0, 220.0, 40.0),
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Rocking chair'), 'Classic', 'Comfortable rocking chair with armrests', 60.0, 70.0, 80.0),
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Desk'), 'Workspace', 'Work desk with drawers', 150.0, 80.0, 70.0),
    (gen_random_uuid(), (SELECT ID FROM Product WHERE Name = 'Padded stool'), 'Round', 'Round pouf with textile upholstery', 50.0, 50.0, 50.0);