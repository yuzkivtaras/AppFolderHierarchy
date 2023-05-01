CREATE PROCEDURE dbo.CreateDatabaseFolderHierarchyAndCatalogTable
AS
BEGIN
    SET NOCOUNT ON;

    -- Створення бази даних
    CREATE DATABASE FolderHierarchy;
    GO

    -- Використання бази даних
    USE FolderHierarchy;

    -- Створення таблиці "Catalogs"
    CREATE TABLE Catalogs (
        Id INT PRIMARY KEY,
        NameCatalog NVARCHAR(255) NOT NULL,
        ParentCatalogId INT,
        Level INT NOT NULL,
        FOREIGN KEY (ParentCatalogId) REFERENCES Catalogs(Id)
    )

    -- Додавання даних до таблиці "Catalogs"
    INSERT INTO Catalogs(Id, NameCatalog, ParentCatalogId, Level)
    VALUES (1, 'Creating Digital Images', NULL, 1),
           (2, 'Resources', 1, 2),
           (3, 'Evidence', 1, 2),
           (4, 'Graphic Products', 1, 2),
           (5, 'Primary Sources', 2, 3),
           (6, 'Secondary Sources', 2, 3),
           (7, 'Process', 4, 3),
           (8, 'Final Product', 4, 3);
END;
