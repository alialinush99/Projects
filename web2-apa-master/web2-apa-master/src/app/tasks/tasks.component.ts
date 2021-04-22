import { Component, OnInit } from '@angular/core';
import { TaskService } from '../task.service';
import { DepartmentsService} from '../departments.service';
import { TaskDetailComponent} from '../task-detail/task-detail.component';
import { EmployeeService} from '../employee.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
    constructor(public taskService: TaskService, public departmentsService: DepartmentsService, public employeeService: EmployeeService) {}
    ngOnInit() {
        this.getDepartments();
        this.getTask();
        this.getEmployees();
    }

    getEmployees(): void {
        this.employeeService.getEmployees().subscribe(e => this.employeeService.employees = e);
    }
    getDepartments(): void {
        this.departmentsService.getDepartments().subscribe(e => this.departmentsService.departments = e);
    }
    // imaginary employees
    // getEmployees(): void {
    //     this.employeesService.getEmployees().subscribe(e => this.employeesService.employees = e);
    // }

    getTask(): void {
        this.taskService.getTask().subscribe(t => this.taskService.tasks = t);
    }

    addTask() {
        // tslint:disable-next-line:max-line-length
        this.taskService.addTask(this.taskService.department_id, this.taskService.name, this.taskService.due_date).subscribe(
            e => this.getTask()
        );
    }

    removeTask(id: number) {
        this.taskService.deleteTask(id).subscribe(
            e => this.getTask()
        );
    }
}
