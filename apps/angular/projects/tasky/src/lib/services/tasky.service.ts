import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class TaskyService {
  apiName = 'Tasky';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/Tasky/sample' },
      { apiName: this.apiName }
    );
  }
}
