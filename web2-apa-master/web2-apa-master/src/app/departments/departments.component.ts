import { Component, OnInit } from '@angular/core';
import {DepartmentsService} from '../departments.service';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css']
})
export class DepartmentsComponent implements OnInit {

  constructor(public departmentsService: DepartmentsService) {}

  ngOnInit() {
    this.getDepartments();
  }

  getDepartments(): void {
    this.departmentsService.getDepartments().subscribe(e => this.departmentsService.departments = e);
  }

  addDepartment() {
    this.departmentsService.addDepartment(this.departmentsService.newDepartmentName, this.departmentsService.newDepartmentBuilding).subscribe(
      val => {this.getDepartments();},
      response => {},
      () => {
        this.departmentsService.newDepartmentBuilding = '';
        this.departmentsService.newDepartmentName = '';
      }
    );
  }
}
