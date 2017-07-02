import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { StoreModule } from '@ngrx/store';
import { ClarityModule } from 'clarity-angular';

import { SharedModule } from './app/shared/shared.module';
import { CoreModule } from './app/core/core.module';

import { routing } from './app/app.routes';
import { appReducer } from './app/app-store';
import { AppComponent } from './app/app.component'

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent
    ],
    imports: [
        routing,
        CoreModule.forRoot(),
        SharedModule.forRoot(),
        StoreModule.provideStore(appReducer),
        StoreDevtoolsModule.instrumentOnlyWithExtension(),
        ClarityModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },

            { path: '**', redirectTo: 'home' }
        ])
    ]
};
