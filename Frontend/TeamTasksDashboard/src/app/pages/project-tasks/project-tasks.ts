import { ChangeDetectorRef, Component, NgZone } from '@angular/core';
import { Sidebar } from '../../components/sidebar/sidebar';
import { DinamicTable } from '../../components/dinamic-table/dinamic-table';

import { LucideAngularModule } from 'lucide-angular';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api-service';
import { ProjectTasksDto } from '../../interfaces/ProjectTasksDto';
import { ConfigColumns } from '../../interfaces/ConfigColumns';

@Component({
  selector: 'app-project-tasks',
  imports: [Sidebar, DinamicTable, LucideAngularModule],
  templateUrl: './project-tasks.html',
  styleUrl: './project-tasks.css',
})
export class ProjectTasks {
  constructor(
    private api: ApiService,
    private zone: NgZone,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute,
  ) {}

  projectId: string = '';

  projectTasks: ProjectTasksDto[] = [];

  projectTasksConfig: ConfigColumns[] = [
    { key: 'title', label: 'Title' },
    { key: 'assigned.firstName assigned.lastName', label: 'Assigned' },
    { key: 'status.name', label: 'Status' },
    { key: 'priority.name', label: 'Priority' },
    { key: 'estimatedComplexity', label: 'Estimated Complexity' },
    { key: 'createdAt', label: 'Created At', type: 'date' },
    { key: 'dueDate', label: 'Due Date', type: 'date' },
    {key: '', actionIcon: 'notebook-tabs', actionRoute: '', label: '', type: 'action' , action: (row) => this.viewTaskDetails(row)}, 
  ];

  ngOnInit() {
    this.route.queryParams.subscribe((params: any) => {
      this.projectId = params['projectId'];
      console.log('Parámetros recibidos:', params);
    });

    this.getTask(this.projectId, null, null);
  }

  getTask(projectId: string, statusId: string | null, developerId: string | null) {
    let params: any = {
      ProjectId: this.projectId,
    };

    if (statusId) {
      params = { ...params, StatusId: statusId };
    }

    if (developerId) {
      params = { ...params, DeveloperId: developerId };
    }

    this.api.get<ProjectTasksDto[]>('Project/tasks', params).subscribe({
      next: (data: ProjectTasksDto[]) => {
        this.zone.run(() => {
          this.projectTasks = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error al cargar las tareas del proyecto:', error);
      },
    });
  }

  openCreateModal() {}

  viewTaskDetails(row: ProjectTasksDto) {
    console.log('View task details:', row);
  }
}
