ALTER TABLE Register
ADD 
    LastLoginDate DATETIME NULL,
    FailedLoginAttempts INT DEFAULT 0;
