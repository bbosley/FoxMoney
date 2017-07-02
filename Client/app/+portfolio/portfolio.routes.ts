import { Routes, RouterModule } from '@angular/router';

import { PortfolioListComponent } from './+list/portfolio-list.component';
import { PortfolioViewComponent } from "./+view/portfolio-view.component";
import { PortfolioAddComponent } from './+add/portfolio-add.component';

const routes: Routes = [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', component: PortfolioListComponent },
    { path: 'add', component: PortfolioAddComponent },
    { path: ':id', component: PortfolioViewComponent }
];

export const routing = RouterModule.forChild(routes);