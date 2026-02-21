SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yeisson Rodriguez
-- Create date: 18/02/2026
-- Description:	Creacion de SP par insertar tareas
-- =============================================
CREATE PROCEDURE Core.sp_insert_task
	@ProjectId UNIQUEIDENTIFIER,
	@Title NVARCHAR(100),
	@Description NVARCHAR(MAX),
	@DeveloperId UNIQUEIDENTIFIER, 
	@StatusId UNIQUEIDENTIFIER, 
	@PriorityId UNIQUEIDENTIFIER, 
	@EstimatedComplexity INT, 
	@DueDate DATETIME2
AS
BEGIN
	SET NOCOUNT ON;

	--vALIDAR ProjectId
	IF NOT EXISTS (SELECT 1 FROM Core.Projects WHERE ProjectId = @ProjectId)
	BEGIN
		RAISERROR('Error: El ProjectId proporcionado no existe.', 16, 1);
		RETURN;
	END

	--Validar AssigneeId
	IF NOT EXISTS (SELECT 1 FROM Core.Developers WHERE DeveloperId = @DeveloperId)
	BEGIN
		RAISERROR('Error: El AssigneeId proporcionado no existe.', 16, 1);
		RETURN;
	END

	--Validar complejidad
	IF @EstimatedComplexity < 1 OR @EstimatedComplexity > 5
	BEGIN
		RAISERROR('Error: La complejidad debe estar entre 1 y 5.', 16, 1);
		RETURN;
	END


	BEGIN TRY
		INSERT INTO Core.Tasks
		(ProjectId, Title, Description, AssignedId, StatusId, PriorityId, EstimatedComplexity, DueDate)
		VALUES
		(@ProjectId, @Title, @Description, @DeveloperId, @StatusId, @PriorityId, @EstimatedComplexity, @DueDate)
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
	END CATCH
 
END
GO




