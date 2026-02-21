import { ProjectStatus } from "../pages/project-status/project-status"

export interface ProjectsDto {
    projectId: string, 
    name: string,
    clientName: string,
    startDate: string,
    endDate: string,
    statusId: string,
    statud: ProjectStatus
}
