-- Script generated by Aqua Data Studio Schema Synchronization for MS SQL Server 2016 on Wed Mar 08 11:55:02 PST 2023
-- Execute this script on:
-- 		PSP_PIMS_S49_01/dbo - This database/schema will be modified
-- to synchronize it with MS SQL Server 2016:
-- 		PSP_PIMS_S49_00/dbo

-- We recommend backing up the database prior to executing the script.

SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop trigger dbo.PIMS_ACQCIT_I_S_U_TR
PRINT N'Drop trigger dbo.PIMS_ACQCIT_I_S_U_TR'
GO
DROP TRIGGER IF EXISTS [dbo].[PIMS_ACQCIT_I_S_U_TR]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop trigger dbo.PIMS_ACQCIT_I_S_I_TR
PRINT N'Drop trigger dbo.PIMS_ACQCIT_I_S_I_TR'
GO
DROP TRIGGER IF EXISTS [dbo].[PIMS_ACQCIT_I_S_I_TR]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Alter table dbo.PIMS_ACQ_CHKLST_SECTION_TYPE
PRINT N'Alter table dbo.PIMS_ACQ_CHKLST_SECTION_TYPE'
GO
ALTER TABLE [dbo].[PIMS_ACQ_CHKLST_SECTION_TYPE] ALTER COLUMN [DISPLAY_ORDER] smallint NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Alter table dbo.PIMS_ACQ_CHKLST_ITEM_TYPE
PRINT N'Alter table dbo.PIMS_ACQ_CHKLST_ITEM_TYPE'
GO
ALTER TABLE [dbo].[PIMS_ACQ_CHKLST_ITEM_TYPE] ALTER COLUMN [DISPLAY_ORDER] smallint NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[PIMS_ACQ_CHKLST_ITEM_TYPE]
	DROP COLUMN IF EXISTS [HINT]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Create trigger dbo.PIMS_ACQCIT_I_S_I_TR
PRINT N'Create trigger dbo.PIMS_ACQCIT_I_S_I_TR'
GO
CREATE TRIGGER [dbo].[PIMS_ACQCIT_I_S_I_TR] ON PIMS_ACQ_CHKLST_ITEM_TYPE INSTEAD OF INSERT AS
SET NOCOUNT ON
BEGIN TRY
  IF NOT EXISTS(SELECT * FROM inserted) 
    RETURN;

  
  insert into PIMS_ACQ_CHKLST_ITEM_TYPE ("ACQ_CHKLST_ITEM_TYPE_CODE",
      "ACQ_CHKLST_SECTION_TYPE_CODE",
      "DESCRIPTION",
      "IS_REQUIRED",
      "DISPLAY_ORDER",
      "EFFECTIVE_DATE",
      "EXPIRY_DATE",
      "CONCURRENCY_CONTROL_NUMBER")
    select "ACQ_CHKLST_ITEM_TYPE_CODE",
      "ACQ_CHKLST_SECTION_TYPE_CODE",
      "DESCRIPTION",
      "IS_REQUIRED",
      "DISPLAY_ORDER",
      "EFFECTIVE_DATE",
      "EXPIRY_DATE",
      "CONCURRENCY_CONTROL_NUMBER"
    from inserted;

END TRY
BEGIN CATCH
   IF @@trancount > 0 ROLLBACK TRANSACTION
   EXEC pims_error_handling
END CATCH;

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Create trigger dbo.PIMS_ACQCIT_I_S_U_TR
PRINT N'Create trigger dbo.PIMS_ACQCIT_I_S_U_TR'
GO
CREATE TRIGGER [dbo].[PIMS_ACQCIT_I_S_U_TR] ON PIMS_ACQ_CHKLST_ITEM_TYPE INSTEAD OF UPDATE AS
SET NOCOUNT ON
BEGIN TRY
  IF NOT EXISTS(SELECT * FROM deleted) 
    RETURN;

  -- validate concurrency control
  if exists (select 1 from inserted, deleted where inserted.CONCURRENCY_CONTROL_NUMBER != deleted.CONCURRENCY_CONTROL_NUMBER+1 AND inserted.ACQ_CHKLST_ITEM_TYPE_CODE = deleted.ACQ_CHKLST_ITEM_TYPE_CODE)
    raiserror('CONCURRENCY FAILURE.',16,1)


  -- update statement
  update PIMS_ACQ_CHKLST_ITEM_TYPE
    set "ACQ_CHKLST_ITEM_TYPE_CODE" = inserted."ACQ_CHKLST_ITEM_TYPE_CODE",
      "ACQ_CHKLST_SECTION_TYPE_CODE" = inserted."ACQ_CHKLST_SECTION_TYPE_CODE",
      "DESCRIPTION" = inserted."DESCRIPTION",
      "IS_REQUIRED" = inserted."IS_REQUIRED",
      "DISPLAY_ORDER" = inserted."DISPLAY_ORDER",
      "EFFECTIVE_DATE" = inserted."EFFECTIVE_DATE",
      "EXPIRY_DATE" = inserted."EXPIRY_DATE",
      "CONCURRENCY_CONTROL_NUMBER" = inserted."CONCURRENCY_CONTROL_NUMBER"
    , DB_LAST_UPDATE_TIMESTAMP = getutcdate()
    , DB_LAST_UPDATE_USERID = user_name()
    from PIMS_ACQ_CHKLST_ITEM_TYPE
    inner join inserted
    on (PIMS_ACQ_CHKLST_ITEM_TYPE.ACQ_CHKLST_ITEM_TYPE_CODE = inserted.ACQ_CHKLST_ITEM_TYPE_CODE);

END TRY
BEGIN CATCH
   IF @@trancount > 0 ROLLBACK TRANSACTION
   EXEC pims_error_handling
END CATCH;

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
   IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
   PRINT 'The database update failed'
END
GO