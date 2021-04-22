import { Injectable } from '@angular/core';
import { Employee} from './employees';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
 newDepartmentId: number;
 newFirstName: string;
 newLastName: string;
 newDateOfBirth: string;

  constructor(private http: HttpClient) { }

  employees = [];



getEmployees(): Observable <Employee[]> {
  return this.http.get<Employee[]>('http://i875395.hera.fhict.nl/api/3680339/employee');
}

addEmployee( first_name: string, last_name: string, birth_date: string,department_id: number): Observable <Employee> {
  let newEmployee = {first_name, last_name, birth_date, department_id};
  newEmployee = JSON.parse(JSON.stringify(newEmployee));
  return this.http.post<Employee>('http://i875395.hera.fhict.nl/api/3680339/employee', newEmployee);
}
deleteEmployee(id: number): Observable<Employee> {
  return this.http.delete<Employee>('http://i875395.hera.fhict.nl/api/3680339/employee?id='+ id);
}
  getEmployeeById(id: number) {
    for (let e of this.employees) {
      if (e.id == id) {
        return e;
      }
    }
}

saveEmployee(id: number, first_name?: string, department_id?: number) {
  let obekt = {first_name, department_id};
  obekt = JSON.parse(JSON.stringify(obekt));
  return this.http.put('http://i875395.hera.fhict.nl/api/3680339/employee?id='+id, obekt);
}

  getEmployeesByName(name: string) {
    let matchingEmployees = [];
    for (let e of this.employees) {
      if (e.first_name.toLowerCase().includes(name.toLowerCase()) || e.last_name.toLowerCase().includes(name.toLowerCase())) {
        matchingEmployees.push(e);
      }
    }
    return matchingEmployees;
  }
}
