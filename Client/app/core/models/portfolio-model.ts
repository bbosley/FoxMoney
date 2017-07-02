export interface Portfolio {
    id: number;
    name: string;
    return: number;
    capitalGain: number;
    income: number
}

export interface AddPortfolioModel {
    portfolioName: string;
    ownerId: number;
    yahooCode: string;
    purchaseAmount: number;
    purchaseDate: Date;
    purchasePrice: number;
    purchaseFees: number;
    customName: string;
}