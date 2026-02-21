import { Routes } from '@angular/router';

export const routes: Routes = [
    {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    {path: 'dashboard', loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)},
    {path: 'projects', loadComponent: () => import('./pages/projects/projects').then(m => m.Projects)},
    {path: 'project-tasks', loadComponent: () => import('./pages/project-tasks/project-tasks').then(m => m.ProjectTasks)},
    {path: 'developers', loadComponent: () => import('./pages/developers/developers').then(m => m.Developers)},
    {path: 'task-status', loadComponent: () => import('./pages/task-status/task-status').then(m => m.TaskStatus)},
    {path: 'project-status', loadComponent: () => import('./pages/project-status/project-status').then(m => m.ProjectStatus)}
];
