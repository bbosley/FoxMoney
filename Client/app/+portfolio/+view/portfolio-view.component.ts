import { Portfolio } from './../../core/models/portfolio-model';
import { ActivatedRoute } from '@angular/router';
import { PortfolioService } from './../portfolio.service';
import { Component, OnInit } from '@angular/core';
@Component({
    selector: 'appc-portfolio-view',
    templateUrl: './portfolio-view.component.html'
})
export class PortfolioViewComponent implements OnInit {
    portfolio;
    id: number;

    constructor(public portfolioService: PortfolioService, private route: ActivatedRoute) {}

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.id = +params['id'];

            this.portfolioService.getPortfolio(this.id)
                .subscribe(p => {
                    this.portfolio = p;
                });
        });
    }

    removeDraft() {
        this.portfolioService.removeDraft(this.id)
            .subscribe(p => console.log(p));
    }
}
