import {Injectable, OnInit} from '@angular/core';
import {Department} from './department';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentsService {

  newDepartmentName: string;
  newDepartmentBuilding: string;
  selectedDepartment: Department;

  constructor(private http: HttpClient) { }

  departments = [];

  getDepartmentById(id: number) {
    for (let d of this.departments) {
      if (d.id == id) {
        return d;
      }
    }
  }

  getDepartmentsByName(name: string) {
    let matchingDepartments = [];
    for (let d of this.departments) {
      if (d.name.toLowerCase().includes(name.toLowerCase())) {
        matchingDepartments.push(d);
      }
    }
    return matchingDepartments;
  }

  getDepartments(): Observable <Department[]> {
    // console.log(this.http.get('http://i875395.hera.fhict.nl/api/3680339/department'));
    return this.http.get<Department[]>('http://i875395.hera.fhict.nl/api/3680339/department');
  }

  addDepartment(name: string, building: string): Observable <Department> {
    const kur = [123, 22];
    let obekt = {name, building, kur};
    obekt = JSON.parse(JSON.stringify(obekt));
    return this.http.post<Department>('http://i875395.hera.fhict.nl/api/3680339/department', obekt);
  }

  deleteDepartment(id: number): Observable<Department> {
    return this.http.delete<Department>('http://i875395.hera.fhict.nl/api/3680339/department?id=' + id);
  }

  saveDepartment(id: number, name?: string, building?: string) {
    let obekt = {name, building};
    obekt = JSON.parse(JSON.stringify(obekt));
    return this.http.put('http://i875395.hera.fhict.nl/api/3680339/department?id=' + id, obekt);
  }
}
