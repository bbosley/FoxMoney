import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpModule, JsonpModule } from '@angular/http';
import { ClarityModule } from 'clarity-angular';

import { PageHeadingComponent } from './directives/page-heading.directive';
import { DynamicFormComponent } from './forms/dynamic-form.component';
import { DynamicFormControlComponent } from './forms/dynamic-form-control.component';
import { ErrorMessageComponent } from './forms/error-message.component';
import { ErrorSummaryComponent } from './forms/error-summary.component';
import { FormControlService } from './forms/form-control.service';


import { HeaderComponent } from './layout/header.component';
//import { FooterComponent } from './layout/footer.component';

import { UppercasePipe } from './pipes/uppercase.pipe';

import {PrettyJsonModule} from 'angular2-prettyjson';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    // No need to export as these modules don't expose any components/directive etc'
    HttpModule,
    JsonpModule,
    ClarityModule,
    PrettyJsonModule
  ],
  declarations: [
    DynamicFormComponent,
    DynamicFormControlComponent,
    ErrorMessageComponent,
    ErrorSummaryComponent,
    //FooterComponent,
    HeaderComponent,
    PageHeadingComponent,
    UppercasePipe
  ],
  exports: [
    // Modules
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    // Providers, Components, directive, pipes
    DynamicFormComponent,
    DynamicFormControlComponent,
    ErrorSummaryComponent,
    ErrorMessageComponent,
    //FooterComponent,
    HeaderComponent,
    PageHeadingComponent,
    UppercasePipe
  ]

})
export class SharedModule {
  public static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [
        FormControlService
      ]
    };
  }
}
