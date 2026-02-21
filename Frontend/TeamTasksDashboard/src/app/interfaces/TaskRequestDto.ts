export interface TaskRequestDto {
    projectId: string;
    title: string;
    description?: string;
    assignedId: string;
    priorityId: string;
    estimatedComplexity: number;
    dueDate: Date;
}
