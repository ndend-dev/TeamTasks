import { Component } from '@angular/core';
import { MenuItem } from '../../interfaces/MenuItem';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-sidebar',
  imports: [CommonModule, RouterModule, LucideAngularModule],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css',
})
export class Sidebar {

  isMenuOpen: boolean = false;

  menuItems: MenuItem[] = [
    { title: 'Dashboard', icon: 'home', route: '/dashboard' },
    // { title: 'Task', icon: 'calendar-check-2', route: '/task' },
    { title: 'Projects', icon: 'folder', route: '/projects' },
    { title: 'Developers', icon: 'users', route: '/developers' },
    { title: 'Task Status', icon: 'list-todo', route: '/task-status' },
    { title: 'Project Status', icon: 'list-todo', route: '/project-status' },
    // { title: 'Settings', icon: 'settings', route: '/settings' }
  ];

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
    console.log('Menu toggled:', this.isMenuOpen);
  }
}
