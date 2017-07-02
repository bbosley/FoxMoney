import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'portfolio', pathMatch: 'full' },
  {
    path: 'login', loadChildren: './+login/login.module#LoginModule'
  },
  {
    path: 'register', loadChildren: '../app/+register/register.module#RegisterModule'
  },
  {
    path: 'portfolio', loadChildren: '../app/+portfolio/portfolio.module#PortfolioModule'
  },
  {
    path: 'holding', loadChildren: '../app/+holding/holding.module#HoldingModule'
  }
];

export const routing = RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules });
