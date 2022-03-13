import { Component, OnInit } from '@angular/core';
import { TaskyService } from '../services/tasky.service';

@Component({
  selector: 'lib-tasky',
  template: ` <p>tasky works!</p> `,
  styles: [],
})
export class TaskyComponent implements OnInit {
  constructor(private service: TaskyService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
