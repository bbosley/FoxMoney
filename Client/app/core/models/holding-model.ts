export interface AddHoldingModel {
    customName: string;
    portfolioId: number;
    yahooCode: string;
    purchaseAmount: number;
    purchaseDate: Date;
    purchasePrice: number;
    purchaseFees: number;
};

export interface AddIncomeModel {
    holdingId: number;
    incomeAmount: number;
    transactionDate: Date;
    incomeReinvested: boolean;
    unitPrice: number;
    units: number;
}

export interface AddUnitModel {
    holdingId: number;
    units: number;
    transactionDate: Date;
    unitPrice: number;
    brokerage: number;
    transactionType: string;
}