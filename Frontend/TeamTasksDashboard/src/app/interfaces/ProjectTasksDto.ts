import { AssignedDto } from "./AssignedDto"
import { PriorityDto } from "./PriorityDto"
import { TaskStatusesDto } from "./TaskStatusesDto"

export interface ProjectTasksDto {
    taskId: string,
    projectId: string,
    title:string,
    description: string,
    assignedId: string,
    statusId: string,
    priorityId: string,
    estimatedComplexity: string,
    dueDate: string,
    completionDate: string,
    assigned: AssignedDto,
    priority: PriorityDto,
    status: TaskStatusesDto
}