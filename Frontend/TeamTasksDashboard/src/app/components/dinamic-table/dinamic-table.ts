import { CommonModule } from '@angular/common';
import { Component, Input, signal, computed, Output, EventEmitter, input } from '@angular/core';
import { LucideAngularModule } from 'lucide-angular';
import { ConfigColumns } from '../../interfaces/ConfigColumns';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dinamic-table',
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './dinamic-table.html',
  styleUrl: './dinamic-table.css',
})
export class DinamicTable {
  constructor(private router: Router) {}

  data = input<any[]>([]);
  configColumns = input<ConfigColumns[]>([]);

  @Output() actionTriggered = new EventEmitter<{ col: ConfigColumns; row: any }>();

  displayColumns = computed<ConfigColumns[]>(() => {
    const config = this.configColumns();
    const currentData = this.data();

    if (config && config.length > 0) return config;

    if (currentData && currentData.length > 0) {
      return Object.keys(currentData[0]).map((key) => ({
        key,
        label: key.replace(/_/g, ' ').toUpperCase(),
        type: 'text',
      }));
    }
    return [];
  });

  currentPage = signal(1);
  pageSize = signal(10);
  sortKey = signal<string>('');
  sortDirection = signal<'asc' | 'desc'>('asc');

  sortedData = computed(() => {
    const items = [...this.data()];
    const key = this.sortKey();
    const direction = this.sortDirection();

    if (key) {
      items.sort((a, b) => {
        const valA = a[key];
        const valB = b[key];
        return direction === 'asc' ? (valA > valB ? 1 : -1) : valA < valB ? 1 : -1;
      });
    }
    return items;
  });

  paginatedData = computed(() => {
    const start = (this.currentPage() - 1) * this.pageSize();
    return this.sortedData().slice(start, start + this.pageSize());
  });

  totalPages = computed(() => {
    if (this.data.length === 0) return 1;

    return Math.ceil(this.data.length / this.pageSize());
  });

  getCellValue(row: any, col: ConfigColumns): any {
  const key = col.key;

  if (key.includes(' ') || key.includes(',')) {
    const keys = key.split(/[ ,]+/).filter(k => k.trim() !== '');
    
    return keys.map(k => {
      if (k.includes('.')) {
        return k.split('.').reduce((acc, part) => acc && acc[part], row);
      }
      return row[k];
    }).join(' ');
  }

  if (key.includes('.')) {
    return key.split('.').reduce((acc, part) => acc && acc[part], row);
  }

  return row[key];
}

  toggleSort(key: string) {
    if (this.sortKey() === key) {
      this.sortDirection.update((d) => (d === 'asc' ? 'desc' : 'asc'));
    } else {
      this.sortKey.set(key);
      this.sortDirection.set('asc');
    }
  }

  changePage(delta: number) {
    const total = this.totalPages();

    this.currentPage.update((p: number): number => {
      const next = p + delta;

      if (next < 1) return 1;
      if (next > total) return total;

      return next;
    });
  }

  handleNavigation(col: ConfigColumns) {
    if (col.actionRoute) {
      const segments = [col.actionRoute, col.id].filter((s) => s != null);
      this.router.navigate(segments);
    }
  }

  handleAction(col: ConfigColumns, row: any) {
    if (col.action) {
      col.action(row);
    }
  }
}
