import {Component, OnInit} from '@angular/core';
import {DepartmentsService} from './departments.service';
import {TaskService} from './task.service';
import {EmployeeService} from './employee.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [DepartmentsService, TaskService, EmployeeService]
})
export class AppComponent implements OnInit {
  title = 'web2-apa';

  query = '';

  constructor(private departmentsService: DepartmentsService, private taskService: TaskService, private employeeService: EmployeeService) {}

  ngOnInit() {
    this.departmentsService.getDepartments();
    this.taskService.getTask();
    this.employeeService.getEmployees();
  }
}
