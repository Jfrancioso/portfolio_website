-- Insert a user
INSERT INTO users (username, password_hash, email, first_name, last_name)
VALUES ('admin', '$2b$12$BxuKkesRo8mTddOHeAqgvOb1a13qwwHjWwGeIsqsvN/deVocsW0eK', 'joefrancioso@gmail.com', 'Admin', 'User'); 

-- Insert an admin role
INSERT INTO roles (role_name)
VALUES ('admin'); 

-- Assign the admin role to the user
DECLARE @UserId INT, @RoleId INT;

SELECT @UserId = id FROM users WHERE username = 'admin';
SELECT @RoleId = id FROM roles WHERE role_name = 'admin';

INSERT INTO user_roles (user_id, role_id)
VALUES (@UserId, @RoleId);

-- Insert a user
DECLARE @UserId INT, @PostId INT;

INSERT INTO users (username, password_hash, email, first_name, last_name)
VALUES ('user', '$2b$12$v4se71qbLq21mD1UjvwV0eoxF0vslINonJy1ZvZEZ1oOS2BeCxZ3K', '68w440@gmail.com', 'user', 'One'); 

SET @UserId = SCOPE_IDENTITY();

-- Insert posts
INSERT INTO posts (user_id, title, content)
VALUES (@UserId, 'First Post', 'This is the first post.');

SET @PostId = SCOPE_IDENTITY();

-- Insert comments
INSERT INTO comments (post_id, user_id, content)
VALUES (@PostId, @UserId, 'This is a comment on the first post by user two.');

-- Insert projects
INSERT INTO projects (user_id, title, description)
VALUES (@UserId, 'First Project', 'This is the first project.');


UPDATE users
SET username = 'user',
password_hash = '$2b$12$v4se71qbLq21mD1UjvwV0eoxF0vslINonJy1ZvZEZ1oOS2BeCxZ3K',
first_name = 'user',
last_name = 'One'
WHERE email = '68w440@gmail.com';


-- Query all users
SELECT * FROM users;
