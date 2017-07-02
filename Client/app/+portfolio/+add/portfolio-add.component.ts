import { AuthState } from './../../core/auth-store/auth.store';
import { AppState } from './../../app-store';
import { Observable } from 'rxjs/Observable';
import { AccountService } from './../../core/account/account.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AddPortfolioModel } from './../../core/models/portfolio-model';
import { ControlTextbox } from './../../shared/forms/control-textbox';
import { ControlBase } from './../../shared/forms/control-base';
import { PortfolioService } from './../portfolio.service';
import { Component, OnInit } from '@angular/core';
import { Store } from "@ngrx/store";

@Component({
    selector: 'appc-portfolio-add',
    templateUrl: './portfolio-add.component.html'
})
export class PortfolioAddComponent implements OnInit {
    public controls: Array<ControlBase<any>>;

    public authState$: Observable<AuthState>;

    constructor(public portfolioService: PortfolioService, 
        public router: Router, 
        public route: ActivatedRoute, 
        public store: Store<AppState>) {}

    public ngOnInit() {
        this.authState$ = this.store.select(state => state.auth);

        const controls: Array<ControlBase<any>> = [
            new ControlTextbox({
                key: 'portfolioName',
                label: 'Portfolio Name',
                placeholder: 'Portfolio Name',
                value: '',
                type: 'textbox',
                required: true,
                order: 1
            }),
            new ControlTextbox({
                key: 'yahooCode',
                label: 'Yahoo Code',
                placeholder: 'Yahoo Code',
                value: '',
                type: 'textbox',
                required: true,
                order: 2
            }),
            new ControlTextbox({
                key: 'customName',
                label: 'Custom Name for Security',
                placeholder: 'Custom Name for Security',
                value: '',
                type: 'textbox',
                required: true,
                order: 3
            }),
            new ControlTextbox({
                key: 'purchaseAmount',
                label: 'Units Purchased',
                placeholder: 'Units Purchased',
                value: '',
                type: 'number',
                required: true,
                order: 4
            }),
            new ControlTextbox({
                key: 'purchaseDate',
                label: 'Date Purchased',
                placeholder: 'Date Purchased',
                value: '',
                type: 'date',
                required: true,
                order: 5
            }),
            new ControlTextbox({
                key: 'purchasePrice',
                label: 'Purchase Price',
                placeholder: 'Purchase Price',
                value: '',
                type: 'number',
                required: true,
                order: 6
            }),
            new ControlTextbox({
                key: 'purchaseFees',
                label: 'Brokerage',
                placeholder: 'Brokerage',
                value: '',
                type: 'number',
                required: true,
                order: 7
            })
        ];

        this.controls = controls;
    }

    addPortfolio(model: AddPortfolioModel): void {
        this.authState$
            .subscribe(res => model.ownerId = Number(res.profile.sub));
        this.portfolioService.addPortfolio(model)
            .subscribe((res: any) => {
                this.router.navigate(['/portfolio/list']);
            });
    }
}