import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../services/api-service';
import { CommonModule } from '@angular/common';
import { title } from 'process';
import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';

@Component({
  selector: 'app-task-new',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-new.html',
  styleUrl: './task-new.css',
})
export class TaskNew {
  taskForm: FormGroup;

  constructor(
    private api: ApiService,
    private fb: FormBuilder,
    @Inject(DialogRef) public ref: DialogRef<any>, @Inject(DIALOG_DATA) public data: any
  ) {
    this.taskForm = this.fb.group({
      projectId: ['', [Validators.required]],
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      assignedId: ['', [Validators.required]],
      statusId: ['', [Validators.required]],
      priorityId: ['', [Validators.required]],
      estimatedComplexity: ['', [Validators.required, Validators.min(1)]],
      dueDate: ['', [Validators.required]],
    });
  }

  onSubmit(){
    
  }
}
