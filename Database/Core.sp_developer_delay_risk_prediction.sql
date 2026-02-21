SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yeisson Rodriguez
-- Create date: 19/02/2026
-- Description:	Creacion SP para obtener el riesgo de retraso por desarrollador
-- =============================================
CREATE PROCEDURE Core.sp_developer_delay_risk_prediction
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT 
	D.FirstName + ' ' + D.LastName AS DeveloperName,
	COUNT(CASE WHEN S.Name <> 'Completed' THEN 1 END) AS OpenTasksCount,
	ISNULL(AVG(CAST(CASE WHEN S.Name = 'Completed' AND T.DueDate < T.CompletionDate THEN DATEDIFF(DAY, T.DueDate, GETDATE()) ELSE 0 END AS FLOAT)), 0) AS AvgDelayDays,
	MIN(CASE WHEN S.Name <> 'Completed' THEN T.DueDate END) AS NearestDueDate,
	MAX(CASE WHEN S.Name <> 'Completed' THEN T.DueDate END) AS LatestDueDate,
	DATEADD(DAY, CAST(ISNULL(AVG(CASE WHEN S.Name = 'Completed' THEN DATEDIFF(DAY, T.DueDate, GETDATE()) ELSE 0 END), 0) AS INT), MAX(CASE WHEN S.Name <> 'Completed' THEN T.DueDate END)) AS  PredictedCompletionDate,
	CASE WHEN (DATEADD(DAY, ISNULL(AVG(CASE WHEN S.Name = 'Completed' THEN DATEDIFF(DAY, T.DueDate, GETDATE()) ELSE 0 END), 0), MAX(CASE WHEN S.Name <> 'Completed' THEN T.DueDate END)) > MAX(CASE WHEN S.Name <> 'Completed' THEN T.DueDate END)) OR ISNULL(AVG(CASE WHEN S.Name = 'Completed' THEN DATEDIFF(DAY, T.DueDate, GETDATE()) ELSE 0 END), 0) > 3 THEN 1 ELSE 0 END AS HighRiskFlag
	FROM Core.Developers D
	LEFT JOIN Core.Tasks T ON D.DeveloperId = T.AssignedId 
	LEFT JOIN Core.TaskStatuses S ON T.StatusId = S.StatusId
	WHERE D.IsActive = 1
	GROUP BY D.FirstName, D.LastName
	ORDER BY HighRiskFlag DESC
END
GO
