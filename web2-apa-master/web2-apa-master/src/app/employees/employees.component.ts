import { Component, OnInit } from '@angular/core';
//import { EMPLOYEES } from '../mock-employees';
import {Employee} from '../employees';
import { EmployeeService } from '../employee.service';
import { DepartmentsService } from '../departments.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

//  employees = EMPLOYEES;
  employees:Employee[];
  employeeFirstName: string;
  employeeLastName:string;
  departmentId:number;
  birth_date:string;
  selectedEmployee: Employee;
  constructor(public employeeService: EmployeeService, public departmentsService: DepartmentsService) { }

  ngOnInit() {
    this.getEmployees();
    this.getDepartments();
  }

//  getEmployees(): void {
  //  this.employees = this.employeeService.getEmployees();

  getEmployees(): void {
    this.employeeService.getEmployees().subscribe(e => this.employeeService.employees = e);
  }

  getDepartments(): void {
    this.departmentsService.getDepartments().subscribe(e => this.departmentsService.departments = e);
  }

  // addEmployee() {
  //   this.employeeService.getEmployees().push(new Employee(this.employees.length + 1,this.departmentId,this.employeeFirstName,this.employeeLastName,this.birth_date));
  //   //this.employeeFirstName = '';
  // }
  addEmployee() {
    this.employeeService.addEmployee(this.employeeService.newFirstName, this.employeeService.newLastName,this.employeeService.newDateOfBirth,this.employeeService.newDepartmentId).subscribe(
      e => this.getEmployees()
    );
  }

  onSelect(e: Employee) {
    this.selectedEmployee = e;
  }

  // getEmployeeIdByName(name: string) {
  //   for (let i = 0; i < this.employees.length; i++) {
  //     if (name == this.employees[i].first_name) {
  //       return i;
  //     }
  //   }
  // }


}
