import { ChangeDetectorRef, Component, NgZone } from '@angular/core';
import { Sidebar } from '../../components/sidebar/sidebar';
import { ApiService } from '../../services/api-service';
import { ConfigColumns } from '../../interfaces/ConfigColumns';
import { DinamicTable } from '../../components/dinamic-table/dinamic-table';

@Component({
  selector: 'app-project-status',
  imports: [Sidebar, DinamicTable],
  templateUrl: './project-status.html',
  styleUrl: './project-status.css',
})
export class ProjectStatus {
  constructor(
    private api: ApiService,
    private zone: NgZone,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.loadProjectStatuses();
  }

  statusesList: ProjectStatus[] = [];
  statusConfig: ConfigColumns[] = [
    {key: 'name', label: 'Name'},
    {key: 'isActive', label: 'Active'},
    {key: 'createdAt', label: 'Created At', type: 'date'},
  ];

  loadProjectStatuses() {
    this.api.get<ProjectStatus[]>('ProjectStatus').subscribe({
      next: (data: ProjectStatus[]) => {
        this.zone.run(() => {
          console.log('Projects loaded:', data);
          this.statusesList = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error al cargar los proyectos:', error);
      },
    });
  }
}
