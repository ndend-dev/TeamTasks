import { ChangeDetectorRef, Component, NgZone } from '@angular/core';
import { Sidebar } from '../../components/sidebar/sidebar';
import { ApiService } from '../../services/api-service';
import { DeveloperWorkload } from '../../interfaces/DeveloperWorkload';
import { DinamicTable } from '../../components/dinamic-table/dinamic-table';
import { ConfigColumns } from '../../interfaces/ConfigColumns';
import { ProjectHealth } from '../../interfaces/ProjectHealth';
import { UpcomingDeadLines } from '../../interfaces/UpcomingDeadLines';
import { DeveloperDelayRisk } from '../../interfaces/DeveloperDelayRisk';

@Component({
  selector: 'app-dashboard',
  imports: [Sidebar, DinamicTable],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  constructor(
    private api: ApiService,
    private zone: NgZone,
    private cdr: ChangeDetectorRef,
  ) {}

  developerWorkload: DeveloperWorkload[] = [];
  projectHealth: ProjectHealth[] = [];
  upcomingDeadlines: UpcomingDeadLines[] = [];
  developerDelayRisk: DeveloperDelayRisk[] = [];

  developerWorkLoadConfig: ConfigColumns[] = [
    { key: 'developerName', label: 'Developer Name' },
    { key: 'openTasksCount', label: 'Open Tasks Count' },
    { key: 'averageEstimatedComplexity', label: 'Average Estimated Complexity', type: 'number' },
    // { key: '', actionIcon: 'trash', actionRoute: '/developers', label: '', type: 'action' , action: (row) => this.prueba(row)}
  ];

  projectHealthConfig: ConfigColumns[] = [
    { key: 'projectName', label: 'Project Name' },
    { key: 'totalTask', label: 'Total Tasks', type: 'number' },
    { key: 'openTask', label: 'Open Tasks', type: 'number' },
    { key: 'completeTask', label: 'Complete Tasks', type: 'number' },
  ];

  upcomingDeadlinesConfig: ConfigColumns[] = [
    { key: 'projectName', label: 'Project Name' },
    { key: 'taskTile', label: 'Task Title' },
    { key: 'dueDate', label: 'Due Date', type: 'date' },
    { key: 'statusName', label: 'Status' },
    { key: 'developerName', label: 'Developer Name' },
    { key: 'daysRemaining', label: 'Days Remaining' },
  ];

  developerDelayRiskConfig: ConfigColumns[] = [
    { key: 'developerName', label: 'Developer Name' },
    { key: 'openTasksCount', label: 'Open Tasks Count' },
    { key: 'avgDelayDays', label: 'Average Delay Days', type: 'number' },
    { key: 'nearestDueDate', label: 'Nearest Due Date', type: 'date' },
    { key: 'latestDueDate', label: 'Latest Due Date', type: 'date' },
    { key: 'predictedCompletionDate', label: 'Predicted Completion Date', type: 'date' },
    { key: 'highRiskFlag', label: 'High Risk Flag' },
  ];

  ngOnInit() {
    console.log('Dashboard component initialized');
    this.loadDeveloperWorkload();
    this.loadProjectHealth();
    this.loadUpcomingDeadlines();
    this.loadDeveloperDelayRisk();
  }

  loadDeveloperWorkload() {
    this.api.get<DeveloperWorkload[]>('Dashboard/developer-workload').subscribe({
      next: (data: DeveloperWorkload[]) => {
        this.zone.run(() => {
          this.developerWorkload = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error loading developer workload:', error);
      },
    });
  }

  loadProjectHealth() {
    this.api.get<ProjectHealth[]>('Dashboard/project-health').subscribe({
      next: (data: ProjectHealth[]) => {
        this.zone.run(() => {
          this.projectHealth = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error loading project health:', error);
      },
    });
  }

  loadUpcomingDeadlines() {
    this.api.get<UpcomingDeadLines[]>('Dashboard/upcoming-deadlines').subscribe({
      next: (data: UpcomingDeadLines[]) => {
        this.zone.run(() => {
          this.upcomingDeadlines = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error loading upcoming deadlines:', error);
      },
    });
  }

  loadDeveloperDelayRisk() {
    this.api.get<DeveloperDelayRisk[]>('Dashboard/developer-delay-risk').subscribe({
      next: (data: DeveloperDelayRisk[]) => {
        this.zone.run(() => {
          this.developerDelayRisk = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error loading developer delay risk:', error);
      },
    });
  }

  //Evento prueba ejecutar desde la tabla dinámica
  prueba(row: any) {
    console.log('Action triggered for row:', row);
  }
}
