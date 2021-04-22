import { Component, OnInit } from '@angular/core';
import {
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef
} from '@angular/core';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours
} from 'date-fns';
import { Subject } from 'rxjs';
import {
  CalendarEvent,
  CalendarView
} from 'angular-calendar';
import {TaskService} from '../task.service';
import { Task } from '../task';
import {Router} from '@angular/router';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  constructor(private taskService: TaskService, private router: Router) { }

  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  refresh: Subject<any> = new Subject();

  tasks: Task[];

  events: CalendarEvent[] = [];

  activeDayIsOpen = true;

  ngOnInit() {
    this.taskService.getTask().subscribe(
      t => this.tasks = t,
      response => {},
    () => this.showTasks());
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  handleEvent(task: CalendarEvent): void {
    this.router.navigate(['/task/' + task.id]);
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }

  showTasks(): void {
    for (const t of this.tasks) {
      this.events.push(
        {
          id: t.id,
          start: new Date(t.due_date),
          end: new Date(t.due_date),
          title: t.name,
          allDay: true
        }
      );
    }
  }
}
