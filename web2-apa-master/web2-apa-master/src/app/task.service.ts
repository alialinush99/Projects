import { Injectable, OnInit } from '@angular/core';
import { Task } from './task';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
   taskId: number;
   name: string;
  // tslint:disable-next-line:variable-name
   due_date: string;
   selectedTask: Task;
   description: string;
  // tslint:disable-next-line:variable-name
   department_id: number;
  constructor(private http: HttpClient) {}

  tasks = [];

  onSelect(t: Task) {
    this.selectedTask = t;
  }
  getTaskById(id: number) {
    for (let t of this.tasks) {
      if (t.id == id) {
        return t;
      }
    }
  }

  getTask(): Observable <Task[]> {
    return this.http.get<Task[]>('http://i875395.hera.fhict.nl/api/3680339/task');

  }

  addTask(department_id: number, name: string, due_date: string): Observable<Task> {
    let object = {department_id, name, due_date};
    object = JSON.parse(JSON.stringify(object));
    console.log(object);
    return this.http.post<Task>('http://i875395.hera.fhict.nl/api/3680339/task', object);
  }

  deleteTask(id: number): Observable<Task> {
    return this.http.delete<Task>('http://i875395.hera.fhict.nl/api/3680339/task?id=' + id);
  }

  updateTask(id: number, name: string, description: string, due_date: string) {
    let object = {name, description, due_date};
    object = JSON.parse(JSON.stringify(object));
    return this.http.put<Task>('http://i875395.hera.fhict.nl/api/3680339/task?id=' + id , object);
  }

  getTasksByName(name: string) {
    let matchingTasks = [];
    for (let t of this.tasks) {
      if (t.name.toLowerCase().includes(name.toLowerCase())) {
        matchingTasks.push(t);
      }
    }
    return matchingTasks;
  }

}
