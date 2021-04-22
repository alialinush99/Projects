
import { Component, OnInit, Input } from '@angular/core';
import { Employee } from '../employees';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../employee.service';
import {DepartmentsService} from '../departments.service';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

   employee: Employee;

  constructor(private route:ActivatedRoute, public employeeService:EmployeeService,public departmentsService:DepartmentsService) { }

  ngOnInit() {
    let id = Number(this.route.snapshot.paramMap.get('id'));
    this.employee = this.employeeService.getEmployeeById(id);

  }

//  updateEmployee() {
//    this.employeeService.saveDepartment(this.department.id, this.department.name, this.department.building).subscribe(
//      val => {},
//      response => {
//        alert('error');
//      },
  //    () => {
  //      alert('success');
  //    }
//    );
deleteEmployee(id:number)
{
 this.employeeService.deleteEmployee(id).subscribe( );
}

updateEmployee() {
   this.employeeService.saveEmployee(this.employee.id,this.employee.first_name, this.employee.department_id).subscribe(
       val => {},
     response => {
       alert('error');
     },
     () => {
      alert('success');
       }
   );

}
}
