import { ChangeDetectorRef, Component, Inject, NgZone } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../services/api-service';
import { CommonModule } from '@angular/common';
import { DialogRef } from '@angular/cdk/dialog';
import { ProjectsDto } from '../../interfaces/ProjectsDto';
import { LucideAngularModule } from 'lucide-angular';
import { DevelopersDto } from '../../interfaces/DevelopersDto';
import { PriorityDto } from '../../interfaces/PriorityDto';
import { TaskRequestDto } from '../../interfaces/TaskRequestDto';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-task-new',
  imports: [CommonModule, ReactiveFormsModule, LucideAngularModule, ],
  templateUrl: './task-new.html',
  styleUrl: './task-new.css',
})
export class TaskNew {
  taskForm: FormGroup;

  projectList: ProjectsDto[] = [];
  developerList: DevelopersDto[] = [];
  priorityList: PriorityDto[] = [];

  constructor(
    private api: ApiService,
    private fb: FormBuilder,
    private zone: NgZone,
    private cdr: ChangeDetectorRef,
    @Inject(DialogRef) public ref: DialogRef<any>,
    private toasts: ToastrService
  ) {
    this.taskForm = this.fb.group({
      projectId: ['', [Validators.required]],
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      assignedId: ['', [Validators.required]],
      priorityId: ['', [Validators.required]],
      estimatedComplexity: ['', [Validators.required, Validators.min(1), Validators.max(5)]],
      dueDate: ['', [Validators.required]],
    });
  }

  ngOnInit() {
    console.log('TaskNew component initialized');
    this.loadProjects();
    this.loadDevelopers();
    this.loadPriorities();
  }

  loadProjects() {
    this.api.get<ProjectsDto[]>('Project').subscribe({
      next: (data: ProjectsDto[]) => {
        this.zone.run(() => {        
          this.projectList = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error al cargar los proyectos:', error);
      },
    });
  }

  loadPriorities() {
    this.api.get<PriorityDto[]>('Priority/active').subscribe({
      next: (data: PriorityDto[]) => {
        this.zone.run(() => {
          this.priorityList = data;
          this.cdr.markForCheck();
          this.cdr.detectChanges();
        });
      },
      error: (error) => {
        console.error('Error al cargar las prioridades:', error);
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

  onSubmit() {

    console.log('Form submitted with values:', this.taskForm.value);

    if (this.taskForm.valid) {

      this.saveTask();

    } else {
      this.taskForm.markAllAsTouched();
      return;
    }
  }

  saveTask(){

    var taskRequest: TaskRequestDto = {
      projectId: this.taskForm.value.projectId,
      title: this.taskForm.value.title,
      description: this.taskForm.value.description,
      assignedId: this.taskForm.value.assignedId,
      priorityId: this.taskForm.value.priorityId,
      estimatedComplexity: this.taskForm.value.estimatedComplexity,
      dueDate: this.taskForm.value.dueDate
    };

    this.api.post('Tasks', taskRequest).subscribe({
      next: (response) => {

        this.toasts.success('Task created successfully', 'Success');
        this.ref.close(true);
      },
      error: (error) => {
        this.toasts.error('Failed to create task', 'Error');
      }
    });
  }
}
