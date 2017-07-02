import { Routes, RouterModule } from '@angular/router';
import { HoldingViewComponent } from './+view/holding-view.component';
import { HoldingAddComponent } from './+add/holding-add.component';
import { HoldingUnitAddComponent } from './+unit-add/holding-unit-add.component';
import { HoldingIncomeAddComponent } from './+income-add/holding-income-add.component';

const routes: Routes = [
    { path: ':id', component: HoldingViewComponent },
    { path: 'add/:id', component: HoldingAddComponent },
    { path: 'add/unit/:id', component: HoldingUnitAddComponent },
    { path: 'add/income/:id', component: HoldingIncomeAddComponent }
];

export const routing = RouterModule.forChild(routes);