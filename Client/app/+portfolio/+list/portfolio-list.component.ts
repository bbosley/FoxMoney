import { Portfolio } from './../../core/models/portfolio-model';
import { PortfolioService } from './../portfolio.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'appc-portfolio-list',
    templateUrl: './portfolio-list.component.html'
})
export class PortfolioListComponent implements OnInit {
    portfolios;
    
    constructor(public portfolioService: PortfolioService) {}

    public ngOnInit() {
        this.portfolioService.getPortfolios()
            .subscribe(portfolio => this.portfolios = portfolio);
    }
}