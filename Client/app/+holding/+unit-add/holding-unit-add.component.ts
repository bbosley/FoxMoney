import { Router, ActivatedRoute } from '@angular/router';
import { ControlTextbox } from './../../shared/forms/control-textbox';
import { ControlBase } from './../../shared/forms/control-base';
import { HoldingService } from './../holding.service';
import { Component, OnInit } from '@angular/core';
import { AddUnitModel } from './../../core/models/holding-model';
import { ControlDropdown } from '../../shared/forms/control-dropdown';


@Component({
    selector: 'appc-holding-unit-add',
    templateUrl: './holding-unit-add.component.html'
})
export class HoldingUnitAddComponent implements OnInit {
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
                key: 'units',
                label: 'Units Purchased',
                placeholder: 'Units Purchased',
                value: '',
                type: 'number',
                required: true,
                order: 1
            }),
            new ControlTextbox({
                key: 'transactionDate',
                label: 'Date Purchased',
                placeholder: 'Date Purchased',
                value: '',
                type: 'date',
                required: true,
                order: 2
            }),
            new ControlTextbox({
                key: 'unitPrice',
                label: 'Purchase Price',
                placeholder: 'Purchase Price',
                value: '',
                type: 'number',
                required: true,
                order: 3
            }),
            new ControlTextbox({
                key: 'brokerage',
                label: 'Brokerage',
                placeholder: 'Brokerage',
                value: '',
                type: 'number',
                required: true,
                order: 4
            }),
            new ControlDropdown({
                key: 'transactionType',
                label: 'Transaction Type',
                options: [{key: 'Buy', value: 'Buy'}, {key: 'Sell', value: 'Sell'}],
                order: 5
            }),
        ];

        this.controls = controls;
    }

    addUnitHolding(model: AddUnitModel) {
        model.holdingId = this.holdingId;

        // console.log(model);
        this.holdingService.addUnit(model)
            .subscribe((res: any) => {
                this.router.navigate(['/holding/', this.holdingId]);
            });
    }
}
