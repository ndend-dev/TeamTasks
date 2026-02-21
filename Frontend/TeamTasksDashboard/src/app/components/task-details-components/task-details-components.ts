import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { LucideAngularModule } from 'lucide-angular';
import { ProjectTasksDto } from '../../interfaces/ProjectTasksDto';
import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';

@Component({
  selector: 'app-task-details-components',
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './task-details-components.html',
  styleUrl: './task-details-components.css',
})
export class TaskDetailsComponents {

  constructor(@Inject(DialogRef) public ref: DialogRef<any>, @Inject(DIALOG_DATA) public data: any) {
    console.log('Data received in TaskDetailsComponents:', data);
  }

  closeModal() {
    this.ref.close();
  }

}
