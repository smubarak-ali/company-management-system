import { Routes } from '@angular/router';
import { CompanyListComponent } from './components/company-list/company-list.component';
import { CompanySaveComponent } from './components/company-save/company-save.component';

export const routes: Routes = [
    { title: "List", component: CompanyListComponent, path: "" },
    { title: "Save Company", component: CompanySaveComponent, path: "save"}
];
