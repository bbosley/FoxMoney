import { ActivatedRoute } from '@angular/router';
import { HoldingService } from './../holding.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'appc-holding-view',
    templateUrl: './holding-view.component.html'
})
export class HoldingViewComponent implements OnInit {
    id: number;
    holding;

    constructor(public holdingService: HoldingService, private route: ActivatedRoute) {}

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.id = +params['id'];

            this.holdingService.getHolding(this.id)
                .subscribe(h => {
                    this.holding = h;
                    // console.log(h);
                });
        });
    }
}
