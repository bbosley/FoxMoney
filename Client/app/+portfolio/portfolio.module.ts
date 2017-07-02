import { PortfolioViewComponent } from './+view/portfolio-view.component';
import { PortfolioAddComponent } from './+add/portfolio-add.component';
import { ClarityModule } from 'clarity-angular';
import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { routing } from './portfolio.routes';
import { PortfolioService } from './portfolio.service';
import { PortfolioListComponent } from './+list/portfolio-list.component';

@NgModule({
    imports: [routing, SharedModule, ClarityModule],
    declarations: [PortfolioListComponent, PortfolioAddComponent, PortfolioViewComponent],
    providers: [PortfolioService]
})
export class PortfolioModule {}