import { AddPortfolioModel } from './../core/models/portfolio-model';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

import { DataService } from '../core/services/data.service';

@Injectable()
export class PortfolioService {

    public portfolioApi = '/api/portfolio';

    constructor(public dataService: DataService) { }

    public getPortfolios() {
        return this.dataService.get(this.portfolioApi);
    }

    public getPortfolio(id: number) {
        return this.dataService.get(this.portfolioApi + '/' + id);
    }

    public addPortfolio(model: AddPortfolioModel) {
        return this.dataService.post(this.portfolioApi, model);
    }

    public removeDraft(id: number) {
        return this.dataService.get(this.portfolioApi + '/' + id + '/removeDraft');
    }
}