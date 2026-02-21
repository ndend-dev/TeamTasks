SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yeisson Rodriguez
-- Create date: 18/02/2026
-- Description:	Creacion de SP para consultar el resumen de estado por proyecto
-- =============================================
CREATE PROCEDURE Core.sp_get_project_status_sumamry
AS
BEGIN

	SET NOCOUNT ON;

    SELECT 
	P.Name AS ProjectName,
	COUNT(T.TaskId) AS TotalTask,
	COUNT(CASE WHEN S.Name <> 'Completed' THEN 1 END) AS OpenTask,
	COUNT(CASE WHEN S.Name = 'Completed' THEN 1 END) AS CompleteTask
	FROM 
	Core.Projects P
	LEFT JOIN Core.Tasks T ON P.ProjectId = T.ProjectId
	LEFT JOIN Core.TaskStatuses S ON T.StatusId = S.StatusId
	GROUP BY P.Name
END
GO
