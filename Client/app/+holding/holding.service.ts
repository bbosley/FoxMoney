import { AddHoldingModel, AddIncomeModel, AddUnitModel } from './../core/models/holding-model';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

import { DataService } from '../core/services/data.service';

@Injectable()
export class HoldingService {
    public holdingApi = '/api/holding';

    constructor(public dataService: DataService) { }

    public getHolding(id: number) {
        return this.dataService.get(this.holdingApi + '/' + id);
    }

    public addHolding(model: AddHoldingModel) {
        return this.dataService.post(this.holdingApi, model);
    }

    public addIncome(model: AddIncomeModel) {
        return this.dataService.post(this.holdingApi + '/' + model.holdingId + '/IncomeTrans', model);
    }

    public addUnit(model: AddUnitModel) {
        return this.dataService.post(this.holdingApi + '/' + model.holdingId + '/UnitTrans', model);
    }
} 