USE portfolio;

IF OBJECT_ID('dbo.user_roles', 'U') IS NOT NULL 
  DROP TABLE dbo.user_roles;

IF OBJECT_ID('dbo.roles', 'U') IS NOT NULL 
  DROP TABLE dbo.roles;

IF OBJECT_ID('dbo.comments', 'U') IS NOT NULL 
  DROP TABLE dbo.comments;

IF OBJECT_ID('dbo.projects', 'U') IS NOT NULL 
  DROP TABLE dbo.projects;

IF OBJECT_ID('dbo.posts', 'U') IS NOT NULL 
  DROP TABLE dbo.posts;

IF OBJECT_ID('dbo.categories', 'U') IS NOT NULL 
  DROP TABLE dbo.categories;

IF OBJECT_ID('dbo.users', 'U') IS NOT NULL 
  DROP TABLE dbo.users;

CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(255) NOT NULL UNIQUE,
    password_hash NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NOT NULL UNIQUE,
    first_name NVARCHAR(255) NOT NULL,
    last_name NVARCHAR(255) NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE TABLE categories (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL UNIQUE,
    description NVARCHAR(MAX)
);

CREATE TABLE posts (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    category_id INT,
    title NVARCHAR(255) NOT NULL,
    content NVARCHAR(MAX) NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2 NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (category_id) REFERENCES categories(id)
);

CREATE TABLE projects (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    title NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    image_url NVARCHAR(255),
    project_url NVARCHAR(255),
    source_code_url NVARCHAR(255),
    created_at DATETIME2 NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2 NULL,
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE comments (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    post_id INT NOT NULL,
    content NVARCHAR(MAX) NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (post_id) REFERENCES posts(id)
);

CREATE TABLE roles (
    id INT IDENTITY(1,1) PRIMARY KEY,
    role_name NVARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE user_roles (
    user_id INT NOT NULL,
    role_id INT NOT NULL,
    PRIMARY KEY(user_id, role_id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (role_id) REFERENCES roles(id)
);

-- Insert data into the tables
DECLARE @UserId INT, @PostId INT, @RoleId INT;

-- Insert a user
INSERT INTO users (username, password_hash, email, first_name, last_name)
VALUES ('admin', '$2b$12$BxuKkesRo8mTddOHeAqgvOb1a13qwwHjWwGeIsqsvN/deVocsW0eK', 'joefrancioso@gmail.com', 'Admin', 'User'); 

-- Insert an admin role
INSERT INTO roles (role_name)
VALUES ('admin'); 

-- Assign the admin role to the user
SELECT @UserId = id FROM users WHERE username = 'admin';
SELECT @RoleId = id FROM roles WHERE role_name = 'admin';

INSERT INTO user_roles (user_id, role_id)
VALUES (@UserId, @RoleId);

-- Insert a user
INSERT INTO users (username, password_hash, email, first_name, last_name)
VALUES ('user', '$2b$12$v4se71qbLq21mD1UjvwV0eoxF0vslINonJy1ZvZEZ1oOS2BeCxZ3K', '68w440@gmail.com', 'user', 'One'); 

SELECT @UserId = SCOPE_IDENTITY();

-- Insert posts
INSERT INTO posts (user_id, title, content)
VALUES (@UserId, 'First Post', 'This is the first post.');

SELECT @PostId = SCOPE_IDENTITY();

-- Insert comments
INSERT INTO comments (post_id, user_id, content)
VALUES (@PostId, @UserId, 'This is a comment on the first post by user two.');

-- Insert projects
INSERT INTO projects (user_id, title, description)
VALUES (@UserId, 'First Project', 'This is the first project.');

-- Update user information
UPDATE users
SET username = 'user',
password_hash = '$2b$12$v4se71qbLq21mD1UjvwV0eoxF0vslINonJy1ZvZEZ1oOS2BeCxZ3K',
first_name = 'user',
last_name = 'One'
WHERE email = '68w440@gmail.com';

USE portfolio;

-- Insert sample categories
INSERT INTO categories (name, description)
VALUES ('Technology', 'Posts related to technology and gadgets.'),
       ('Travel', 'Posts about travel destinations and experiences.'),
       ('Food', 'Posts about different types of food and recipes.'),
       ('Fitness', 'Posts about fitness and workouts.'),
       ('Fashion', 'Posts about fashion and style.');

-- Query all categories
SELECT * FROM categories;


-- Query all users
SELECT * FROM users;

-- Query the categories table
SELECT * FROM categories;
