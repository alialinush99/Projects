import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TasksComponent } from './tasks/tasks.component';
import { EmployeesComponent} from './employees/employees.component';
import { DepartmentsComponent} from './departments/departments.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import {DepartmentDetailComponent} from './department-detail/department-detail.component';
import {TaskDetailComponent} from './task-detail/task-detail.component';
import {EmployeeDetailComponent} from './employee-detail/employee-detail.component';
import {SearchComponent} from './search/search.component';

const routes: Routes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'departments', component: DepartmentsComponent },
  { path: 'tasks', component: TasksComponent },
  { path: 'employees', component: EmployeesComponent },
  { path: 'department/:id', component: DepartmentDetailComponent },
  { path: 'task/:id', component: TaskDetailComponent },
  { path: 'employee/:id', component: EmployeeDetailComponent},
  { path: 'search/:query', component: SearchComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
