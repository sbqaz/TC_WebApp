EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'API';


GO
EXECUTE sp_addrolemember @rolename = N'db_accessadmin', @membername = N'API';


GO
EXECUTE sp_addrolemember @rolename = N'db_securityadmin', @membername = N'API';


GO
EXECUTE sp_addrolemember @rolename = N'db_ddladmin', @membername = N'API';


GO
EXECUTE sp_addrolemember @rolename = N'db_backupoperator', @membername = N'API';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'API';


GO
EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'API';


GO
EXECUTE sp_addrolemember @rolename = N'db_denydatareader', @membername = N'API';


GO
EXECUTE sp_addrolemember @rolename = N'db_denydatawriter', @membername = N'API';

