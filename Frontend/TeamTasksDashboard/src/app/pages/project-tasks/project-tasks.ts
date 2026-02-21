import { ChangeDetectorRef, Component, Inject, NgZone } from '@angular/core';
import { Sidebar } from '../../components/sidebar/sidebar';
import { DinamicTable } from '../../components/dinamic-table/dinamic-table';
import { LucideAngularModule } from 'lucide-angular';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api-service';
import { ProjectTasksDto } from '../../interfaces/ProjectTasksDto';
import { ConfigColumns } from '../../interfaces/ConfigColumns';
import { Dialog } from '@angular/cdk/dialog';
import { TaskDetailsComponents } from '../../components/task-details-components/task-details-components';
import { TaskStatusesDto } from '../../interfaces/TaskStatusesDto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DevelopersDto } from '../../interfaces/DevelopersDto';
import { TaskNew } from '../../components/task-new/task-new';

@Component({
  selector: 'app-project-tasks',
  imports: [CommonModule, FormsModule, Sidebar, DinamicTable, LucideAngularModule],
  templateUrl: './project-tasks.html',
  styleUrl: './project-tasks.css',
})
export class ProjectTasks {
  constructor(
    private api: ApiService,
    private zone: NgZone,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute,
    @Inject(Dialog) private dialog: Dialog,
  ) {}

  selectedStatus: string = '';
  selectedDeveloper: string = '';

  statusList: TaskStatusesDto[] = [];
  developerList: DevelopersDto[] = [];

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
    {
      key: '',
      actionIcon: 'notebook-tabs',
      actionRoute: '',
      label: '',
      type: 'action',
      action: (row) => this.viewTaskDetails(row),
    },
  ];

  ngOnInit() {
    this.route.queryParams.subscribe((params: any) => {
      this.projectId = params['projectId'];
      console.log('Parámetros recibidos:', params);
    });

    this.getTask(this.projectId, null, null);
    this.loadStates();
    this.loadDevelopers();
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

  loadStates() {
    this.api.get<TaskStatusesDto[]>('TaskStatus/active').subscribe({
      next: (data: TaskStatusesDto[]) => {
        this.zone.run(() => {
          this.statusList = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error al cargar los estados de las tareas:', error);
      },
    });
  }

  loadDevelopers() {
    this.api.get<DevelopersDto[]>('Developer/active').subscribe({
      next: (data: DevelopersDto[]) => {
        this.zone.run(() => {
          this.developerList = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error al cargar los desarrolladores:', error);
      },
    });
  }

  

  viewTaskDetails(row: ProjectTasksDto) {
    const dialogRef = this.dialog.open(TaskDetailsComponents, {
      width: '400px',
      data: row,
      disableClose: false,
      backdropClass: ['bg-slate-900', 'bg-opacity-50', 'backdrop-blur-sm'],
    });

    dialogRef.closed.subscribe((result) => {
      console.log('Dialog closed with result:', result);
    });
  }

  applyFilters() {
    const statusId = this.selectedStatus || null;
    const developerId = this.selectedDeveloper || null;

    this.getTask(this.projectId, statusId, developerId);
  }

  resetFilters() {
    this.selectedStatus = '';
    this.selectedDeveloper = '';
  }
}
