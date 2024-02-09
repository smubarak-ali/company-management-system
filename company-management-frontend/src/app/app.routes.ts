import { Routes } from '@angular/router';
import { CompanyListComponent } from './components/company-list/company-list.component';
import { CompanySaveComponent } from './components/company-save/company-save.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { authGuard } from './guards/auth.guard';
import { guestGuard } from './guards/guest.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';

export const routes: Routes = [
    {
        title: "Home Page",
        component: HomePageComponent,
        path: "login",
        canActivate: [guestGuard]
    },
    {
        component: DashboardComponent,
        path: 'dashboard',
        canActivateChild: [authGuard],
        children: [
            { title: "List", component: CompanyListComponent, path: "list" },
            { title: "Save Company", component: CompanySaveComponent, path: "save" }
        ]
    },
    { path: '', redirectTo: 'login', pathMatch: 'full' }
];
