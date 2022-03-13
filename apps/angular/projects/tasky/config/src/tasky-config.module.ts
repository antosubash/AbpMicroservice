import { ModuleWithProviders, NgModule } from '@angular/core';
import { TASKY_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class TaskyConfigModule {
  static forRoot(): ModuleWithProviders<TaskyConfigModule> {
    return {
      ngModule: TaskyConfigModule,
      providers: [TASKY_ROUTE_PROVIDERS],
    };
  }
}
