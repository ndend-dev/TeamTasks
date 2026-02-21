import { ChangeDetectorRef, Component, Inject, NgZone } from '@angular/core';
import { Sidebar } from '../../components/sidebar/sidebar';
import { DinamicTable } from '../../components/dinamic-table/dinamic-table';
import { ConfigColumns } from '../../interfaces/ConfigColumns';
import { LucideAngularModule } from 'lucide-angular';
import { ApiService } from '../../services/api-service';
import { ProjectsDto } from '../../interfaces/ProjectsDto';
import { Router } from '@angular/router';
import { Dialog } from '@angular/cdk/dialog';
import { TaskNew } from '../../components/task-new/task-new';

@Component({
  selector: 'app-projects',
  imports: [Sidebar, DinamicTable, LucideAngularModule],
  templateUrl: './projects.html',
  styleUrl: './projects.css',
})
export class Projects {
  constructor(
    private api: ApiService,
    private zone: NgZone,
    private cdr: ChangeDetectorRef,
    private router: Router,
    @Inject(Dialog) private dialog: Dialog,
  ) {}

  projectData: ProjectsDto[] = [];

  projectTasksConfig: ConfigColumns[] = [
    { key: 'name', label: 'Project Name' },
    { key: 'clientName', label: 'Client Name' },
    { key: 'startDate', label: 'Start Date', type: 'date' },
    { key: 'endDate', label: 'End Date', type: 'date' },
    { key: 'statud.name', label: 'Status' },
    {
      key: '',
      actionIcon: 'clipboard-list',
      actionRoute: '/projects',
      label: '',
      type: 'action',
      action: (row) => this.viewTask(row),
    },
  ];

  ngOnInit() {
    this.loadProjectData();
  }

  loadProjectData() {
    this.api.get<ProjectsDto[]>('Project').subscribe({
      next: (data: ProjectsDto[]) => {
        this.zone.run(() => {
          this.projectData = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error fetching project data:', error);
      },
    });
  }

  openCreateModal() {
    const dialogRef = this.dialog.open(TaskNew, {
      width: '400px',
      disableClose: false,
      backdropClass: [
        'bg-slate-900',
        'bg-opacity-50',
        'backdrop-blur-sm',
        'flex',
        'items-center',
        'justify-center',
      ],
    });
  }

  viewTask(row: ProjectsDto) {
    this.router.navigate(['/project-tasks'], { queryParams: { projectId: row.projectId } });
  }
}
