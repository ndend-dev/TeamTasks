SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yeisson Rodriguez
-- Create date: 18/02/2026
-- Description:	Creacion de SP para obtener el resumen de carga por desarrollador
-- =============================================
CREATE PROCEDURE Core.sp_get_developer_workload
AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		U.FirstName + ' ' + U.LastName AS DeveloperName,
		COUNT(*) AS OpenTasksCount,
		AVG(CAST(EstimatedComplexity AS FLOAT)) AS AverageEstimatedComplexity
	FROM 
	Core.Developers U
	LEFT JOIN Core.Tasks T ON U.DeveloperId = T.AssignedId
	JOIN Core.TaskStatuses S ON T.StatusId = S.StatusId
	WHERE S.StatusId = (SELECT StatusId  FROM Core.TaskStatuses WHERE Name = 'Completed')
	GROUP BY U.FirstName, U.LastName
    ORDER BY AverageEstimatedComplexity DESC;
END
GO
