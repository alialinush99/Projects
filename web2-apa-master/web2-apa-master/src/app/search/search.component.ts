import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {DepartmentsService} from '../departments.service';
import {TaskService} from '../task.service';
import {EmployeeService} from '../employee.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  query: string;

  constructor(private route: ActivatedRoute, public departmentsService: DepartmentsService, public taskService: TaskService, public employeeService: EmployeeService) { }

  ngOnInit() {
    this.query = this.route.snapshot.paramMap.get('query');
    this.route.paramMap.subscribe(params => {
      this.query = this.route.snapshot.paramMap.get('query');
    });
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
