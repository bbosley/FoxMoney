import { Router, ActivatedRoute } from '@angular/router';
import { ControlTextbox } from './../../shared/forms/control-textbox';
import { ControlBase } from './../../shared/forms/control-base';
import { HoldingService } from './../holding.service';
import { Component, OnInit } from '@angular/core';
import { AddIncomeModel } from './../../core/models/holding-model';
import { ControlDropdown } from '../../shared/forms/control-dropdown';


@Component({
    selector: 'appc-holding-income-add',
    templateUrl: './holding-income-add.component.html'
})
export class HoldingIncomeAddComponent implements OnInit {
    public controls: Array<ControlBase<any>>;
    public holdingId: number;

    constructor(public holdingService: HoldingService,
        public router: Router,
        public route: ActivatedRoute) {}

    public ngOnInit() {
        this.route.params.subscribe(params => {
            this.holdingId = +params['id'];
        });

        const controls: Array<ControlBase<any>> = [
            new ControlTextbox({
                key: 'incomeAmount',
                label: 'Income Amount',
                placeholder: 'Income Amount',
                value: '',
                type: 'number',
                required: true,
                order: 1
            }),
            new ControlTextbox({
                key: 'transactionDate',
                label: 'Transaction Date',
                placeholder: 'Transaction Date',
                value: '',
                type: 'date',
                required: true,
                order: 2
            }),
            new ControlDropdown({
                key: 'incomeReinvested',
                label: 'Income Reinvested?',
                options: [{key: true, value: 'Yes'}, {key: false, value: 'No'}],
                order: 3
            }),
            new ControlTextbox({
                key: 'unitPrice',
                label: 'Unit Price',
                placeholder: 'Unit Price',
                value: '0',
                type: 'number',
                required: false,
                order: 4
            }),
            new ControlTextbox({
                key: 'units',
                label: 'Units',
                placeholder: 'Units',
                value: '0',
                type: 'number',
                required: false,
                order: 5
            })
        ];

        this.controls = controls;
    }

    addIncome(model: AddIncomeModel) {
        model.holdingId = this.holdingId;
        if (!model.incomeReinvested) {
            model.unitPrice = 0;
            model.units = 0;
        }
        // console.log(model);
        this.holdingService.addIncome(model)
            .subscribe((res: any) => {
                this.router.navigate(['/holding/', this.holdingId]);
            });
    }
}
