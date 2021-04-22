import { Component, OnInit } from '@angular/core';
import { TaskService } from '../task.service';
import { DepartmentsService } from '../departments.service';
import {EmployeeService} from '../employee.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: [ './dashboard.component.css' ]
})
export class DashboardComponent implements OnInit {

  constructor(public departmentsService: DepartmentsService, public taskService: TaskService, public employeeService: EmployeeService) { }

  ngOnInit() {
    this.getDepartments();
    this.getTasks();
    this.getEmployees();
  }

  getDepartments(): void {
    this.departmentsService.getDepartments().subscribe(e => this.departmentsService.departments = e);
  }
  getTasks(): void {
    this.taskService.getTask().subscribe(e => this.taskService.tasks = e);
  }
  getEmployees(): void {
    this.employeeService.getEmployees().subscribe(e => this.employeeService.employees = e);
  }
}
