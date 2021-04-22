import {Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Task } from '../task';
import { TaskService } from '../task.service';
import { EmployeeService} from '../employee.service';

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent implements OnInit {
  task: Task;
  constructor(private route: ActivatedRoute, public taskService: TaskService, public employeeService: EmployeeService) { }

  ngOnInit() {
    let id = Number(this.route.snapshot.paramMap.get('id'));
    this.task = this.taskService.getTaskById(id);
  }

  updateTask() {
    this.taskService.updateTask(this.task.id, this.task.name, this.task.description, this.task.due_date).subscribe(
        val => {
        },
        response => {
        },
        () => {
            this.taskService.getTask().subscribe(e => this.taskService.tasks = e);
            console.log(this.taskService.updateTask(this.task.id, this.task.name, this.task.description, this.task.due_date));
        }
    );
  }
  deleteTask() {
    this.taskService.deleteTask(this.task.id).subscribe(
        value => {
            this.taskService.getTask().subscribe(e => this.taskService.tasks = e);
        },
        error => {
          alert('error');
        }
    );
  }

}
