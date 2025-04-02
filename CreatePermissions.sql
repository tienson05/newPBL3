CREATE TABLE Permissions (
    PermissionID INT IDENTITY(1,1) PRIMARY KEY,  
    PermissionName NVARCHAR(255) NOT NULL,        
    Description NVARCHAR(MAX)          
);

CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,         
    RoleName NVARCHAR(255) NOT NULL,              
    Description NVARCHAR(MAX)                     
);

CREATE TABLE UserPermissions (
    UserID INT,
    PermissionID INT,
    PRIMARY KEY (UserID, PermissionID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (PermissionID) REFERENCES Permissions(PermissionID)
);

CREATE TABLE RolePermissions (
    RoleID INT,                                   
    PermissionID INT,                             
    PRIMARY KEY (RoleID, PermissionID),           
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),          
    FOREIGN KEY (PermissionID) REFERENCES Permissions(PermissionID)
);

---------------------------------------------------------------
-- Thêm quyền liên quan đến sản phẩm vào bảng Permissions
INSERT INTO Permissions (PermissionName, Description)
VALUES
    ('View Products', 'View the details of products'),
    ('Create Product', 'Create a new product listing'),
    ('Update Product', 'Update an existing product listing'),
    ('Delete Product', 'Delete a product listing'),
    ('Approve Product', 'Approve a product before it is displayed on the platform')

-- Thêm quyền liên quan đến người dùng vào bảng Permissions
INSERT INTO Permissions (PermissionName, Description)
VALUES
    ('View Users', 'View the details of other users'),
	('Create Manager', 'Create a new manager account'),
    ('Update User', 'Update an existing user account'),
    ('Delete User', 'Delete a user account'),
    ('Lock User Account', 'Lock a user account due to violations or other reasons'),
    ('Reset User Password', 'Allow admin to reset a user'),
    ('Manage User Roles', 'Assign or change roles for users'),
	('View Revenue', 'View revenue data for the platform');


---------------------------------------------------------------
INSERT INTO Roles (RoleName, Description)
VALUES
    ('Admin', 'Administrator with full privileges'),
    ('Buyer', 'User who can purchase products'),
    ('Seller', 'User who can sell products'),
    ('Manager', 'Assistant role for Admin, with limited privileges');

INSERT INTO RolePermissions (RoleID, PermissionID)
VALUES
    (1, 1),  
    (1, 2),  
    (1, 3),  
    (1, 4),  
    (1, 5), 
    (1, 6), 
    (1, 7),  
    (1, 8),  
    (1, 9),  
    (1, 10), 
    (1, 11), 
    (1, 12), 
    (1, 13) 

INSERT INTO RolePermissions (RoleID, PermissionID)
VALUES
    (3, 1),  
    (3, 2),  
    (3, 3),  
    (3, 4),  
    (3, 6), 
    (3, 13);

INSERT INTO RolePermissions (RoleID, PermissionID)
VALUES
    (2, 1)

---------------------------------------------------------------------------------------
ALTER TABLE Users
ADD Role INT NOT NULL DEFAULT 1;

------------
-- Tạo thêm bảng UserPermissions là để Admin có thể tùy chỉnh các quyền cho mỗi Manager