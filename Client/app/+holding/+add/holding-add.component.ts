import { Router, ActivatedRoute } from '@angular/router';
import { ControlTextbox } from './../../shared/forms/control-textbox';
import { ControlBase } from './../../shared/forms/control-base';
import { HoldingService } from './../holding.service';
import { Component, OnInit } from '@angular/core';
import { AddHoldingModel } from './../../core/models/holding-model';


@Component({
    selector: 'appc-holding-add',
    templateUrl: './holding-add.component.html'
})
export class HoldingAddComponent implements OnInit {
    public controls: Array<ControlBase<any>>;
    public portfolioId: number;

    constructor(public holdingService: HoldingService,
        public router: Router,
        public route: ActivatedRoute) {}

    public ngOnInit() {
        this.route.params.subscribe(params => {
            this.portfolioId = +params['id'];
        });

        const controls: Array<ControlBase<any>> = [
            new ControlTextbox({
                key: 'yahooCode',
                label: 'Yahoo Code',
                placeholder: 'Yahoo Code',
                value: '',
                type: 'textbox',
                required: true,
                order: 1
            }),
            new ControlTextbox({
                key: 'customName',
                label: 'Custom Name',
                placeholder: 'Custom Name',
                value: '',
                type: 'textbox',
                required: true,
                order: 2
            }),
            new ControlTextbox({
                key: 'purchaseAmount',
                label: 'Units Purchased',
                placeholder: 'Units Purchased',
                value: '',
                type: 'number',
                required: true,
                order: 3
            }),
            new ControlTextbox({
                key: 'purchaseDate',
                label: 'Date Purchased',
                placeholder: 'Date Purchased',
                value: '',
                type: 'date',
                required: true,
                order: 4
            }),
            new ControlTextbox({
                key: 'purchasePrice',
                label: 'Purchase Price',
                placeholder: 'Purchase Price',
                value: '',
                type: 'number',
                required: true,
                order: 5
            }),
            new ControlTextbox({
                key: 'purchaseFees',
                label: 'Brokerage',
                placeholder: 'Brokerage',
                value: '',
                type: 'number',
                required: true,
                order: 6
            })
        ];

        this.controls = controls;
    }

    addHolding(model: AddHoldingModel) {
        model.portfolioId = this.portfolioId;
        // console.log(model);
        this.holdingService.addHolding(model)
            .subscribe((res: any) => {
                // console.log(res);
                this.router.navigate(['/portfolio/', this.portfolioId]);
            });
    }
}
