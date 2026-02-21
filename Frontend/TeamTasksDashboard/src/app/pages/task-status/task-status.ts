import { ChangeDetectorRef, Component, NgZone } from '@angular/core';
import { Sidebar } from '../../components/sidebar/sidebar';
import { ApiService } from '../../services/api-service';
import { TaskStatusesDto } from '../../interfaces/TaskStatusesDto';
import { ConfigColumns } from '../../interfaces/ConfigColumns';
import { DinamicTable } from '../../components/dinamic-table/dinamic-table';

@Component({
  selector: 'app-task-status',
  imports: [Sidebar, DinamicTable],
  templateUrl: './task-status.html',
  styleUrl: './task-status.css',
})
export class TaskStatus {
  constructor(
    private api: ApiService,
    private zone: NgZone,
    private cdr: ChangeDetectorRef,
  ) {}

  taskStatusList: TaskStatusesDto[] = [];
  taskStatusConfig: ConfigColumns[] = [
    { key: 'name', label: 'Name' },
    { key: 'isActive', label: 'Active' },
    { key: 'createdAt', label: 'Created At', type: 'date' },
  ];

  ngOnInit() {
    this.loadTaskStatuses();
  }

  loadTaskStatuses() {
    this.api.get<TaskStatusesDto[]>('TaskStatus').subscribe({
      next: (data: TaskStatusesDto[]) => {
        this.zone.run(() => {
          console.log('Task Statuses loaded:', data);
          this.taskStatusList = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error al cargar los estados de tareas:', error);
      },
    });
  }
}
