/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/
--Reference Data for AddressType 
MERGE INTO [dbo].[owners] AS [target]
    USING (VALUES ('00000000-0000-0000-0000-000000000000', N'Root Owner')) AS [source] ([owner_id], [name])
    ON [target].[owner_id] = [source].[owner_id]

    -- update matched rows 
    WHEN MATCHED THEN
        UPDATE SET [name] = [source].[name]
    -- insert new rows 
    WHEN NOT MATCHED THEN
        INSERT ([owner_id], [name]) VALUES ([owner_id], [name])
    -- delete rows that are in the target but not the source 
    WHEN NOT MATCHED BY SOURCE THEN DELETE;