USE TestDb;

CREATE TABLE AuthUser (
                          ID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
                          Login NVARCHAR(255) NOT NULL UNIQUE,
                          PasswordSalt VARBINARY(MAX),
                          PasswordHash VARBINARY(MAX)
);

CREATE NONCLUSTERED INDEX IX_Product_Login ON AuthUser(Login)
    WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);

INSERT INTO AuthUser (ID, Login, PasswordSalt, PasswordHash);

GO