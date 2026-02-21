import { ChangeDetectorRef, Component, NgZone } from '@angular/core';
import { Sidebar } from '../../components/sidebar/sidebar';
import { DevelopersDto } from '../../interfaces/DevelopersDto';
import { ConfigColumns } from '../../interfaces/ConfigColumns';
import { ApiService } from '../../services/api-service';
import { DinamicTable } from '../../components/dinamic-table/dinamic-table';

@Component({
  selector: 'app-developers',
  imports: [Sidebar, DinamicTable],
  templateUrl: './developers.html',
  styleUrl: './developers.css',
})
export class Developers {
  constructor(
    private api: ApiService,
    private zone: NgZone,
    private cdr: ChangeDetectorRef,
  ) {}

  developerList: DevelopersDto[] = [];

  developerConfig: ConfigColumns[] = [
    { key: 'firstName', label: 'First Name' },
    { key: 'lastName', label: 'Last Name' },
    { key: 'email', label: 'Email' },
    { key: 'isActive', label: 'Active' },
  ];

  ngOnInit() {
    this.loadDevelopers();
  }

  loadDevelopers() {
    this.api.get<DevelopersDto[]>('Developer').subscribe({
      next: (data: DevelopersDto[]) => {
        this.zone.run(() => {

          console.log('Developers loaded:', data);
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
}
