import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import { Department } from '../department';
import { DepartmentsService } from '../departments.service';
import { Location } from '@angular/common';
import {EmployeeService} from '../employee.service';

@Component({
  selector: 'app-department-detail',
  templateUrl: './department-detail.component.html',
  styleUrls: ['./department-detail.component.css']
})
export class DepartmentDetailComponent implements OnInit {
  department: Department;
  oldDepartmentName: string;

  constructor(private route: ActivatedRoute, public departmentsService: DepartmentsService, private router: Router,
              private location: Location, public employeeService: EmployeeService) { }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.department = this.departmentsService.getDepartmentById(id);
    this.oldDepartmentName = this.department.name;
    this.getEmployees();
  }

  updateDepartment() {
    this.departmentsService.saveDepartment(this.department.id, this.department.name, this.department.building).subscribe(
      (data) => {
        this.router.navigate(['/departments']);
        this.departmentsService.getDepartments().subscribe(e => this.departmentsService.departments = e);
      },
      response => {
        this.oldDepartmentName = this.department.name;
      },
      () => {}
    );
  }

  removeDepartment(id: number) {
    this.departmentsService.deleteDepartment(id).subscribe(
      (data) => {
        this.router.navigate(['/departments']);
        this.departmentsService.getDepartments().subscribe(e => this.departmentsService.departments = e);
      }
    );
  }

  cancel() {
    this.location.back();
  }

  getEmployees(): void {
    this.employeeService.getEmployees().subscribe(e => this.employeeService.employees = e);
  }

}
