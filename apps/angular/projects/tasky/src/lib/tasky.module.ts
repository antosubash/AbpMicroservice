import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { TaskyComponent } from './components/tasky.component';
import { TaskyRoutingModule } from './tasky-routing.module';

@NgModule({
  declarations: [TaskyComponent],
  imports: [CoreModule, ThemeSharedModule, TaskyRoutingModule],
  exports: [TaskyComponent],
})
export class TaskyModule {
  static forChild(): ModuleWithProviders<TaskyModule> {
    return {
      ngModule: TaskyModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<TaskyModule> {
    return new LazyModuleFactory(TaskyModule.forChild());
  }
}
