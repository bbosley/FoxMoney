import { HoldingViewComponent } from './+view/holding-view.component';
import { HoldingAddComponent } from './+add/holding-add.component';
import { HoldingUnitAddComponent } from './+unit-add/holding-unit-add.component';
import { HoldingIncomeAddComponent } from './+income-add/holding-income-add.component';
import { ClarityModule } from 'clarity-angular';
import { NgModule } from '@angular/core';
import { ChartistModule } from 'ng-chartist';

import { SharedModule } from '../shared/shared.module';
import { routing } from './holding.routes';
import { HoldingService } from './holding.service';

@NgModule({
    imports: [routing, SharedModule, ClarityModule, ChartistModule],
    declarations: [HoldingViewComponent, HoldingAddComponent, HoldingUnitAddComponent, HoldingIncomeAddComponent],
    providers: [HoldingService]
})
export class HoldingModule {}
