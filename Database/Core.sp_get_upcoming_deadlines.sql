
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yeisson Rodriguez
-- Create date: 18/02/2026
-- Description:	Creación de SP para obtener las tareas proximas a vencer
-- =============================================
CREATE PROCEDURE Core.sp_get_upcoming_deadlines 
AS
BEGIN

	SET NOCOUNT ON;

    DECLARE @DaysRange INT = 7

	SELECT 
		P.Name AS ProjectName,
		T.Title AS TaskTile, 
		T.DueDate,
		S.Name AS  StatusName, 
		D.FirstName + ' ' + D.LastName AS DeveloperName,
		DATEDIFF(day,GETDATE(),T.DueDate) AS DaysRemaining
	FROM 
	Core.Tasks T
	INNER JOIN Core.Projects P ON T.ProjectId = P.ProjectId
	INNER JOIN Core.TaskStatuses S ON T.StatusId = S.StatusId
	LEFT JOIN Core.Developers D ON T.AssignedId = D.DeveloperId
	WHERE S.Name <> 'Completed'
	AND T.DueDate >= GETDATE() 
	AND T.DueDate <= DATEADD(DAY, @DaysRange, GETDATE())

END
GO
