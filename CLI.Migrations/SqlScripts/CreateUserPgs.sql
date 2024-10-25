CREATE TABLE "AuthUser" (
                            "ID" UUID DEFAULT gen_random_uuid() PRIMARY KEY NOT NULL,
                            "Login" VARCHAR(255) NOT NULL UNIQUE,
                            "PasswordSalt" BYTEA,
                            "PasswordHash" BYTEA
);

CREATE INDEX IX_Product_Login ON "AuthUser"("Login")