import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ContentListComponent } from './components/content-list/content-list.component';
import { PictureUploadComponent } from './components/picture-upload/picture-upload.component';
import { PictureCreateDto as PictureEditComponent } from './components/edit-item/edit-item.component';
import { SalesUploadComponent } from './components/sales-upload/sales-upload.component';
import { MarketplaceComponent } from './components/marketplace/marketplace.component';
import { MarketplaceEditComponent } from './components/marketplace-edit/marketplace-edit.component';
import { InspectItemComponent } from './components/inspect-item/inspect-item.component';
export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'list', component: ContentListComponent },
  { path: 'upload', component: PictureUploadComponent },
  { path: 'edit/:id', component: PictureEditComponent },
  { path: 'salesupload', component: SalesUploadComponent },
  { path: 'marketplace', component: MarketplaceComponent },
  { path: 'editItem/:id', component: MarketplaceEditComponent },
  { path: 'inspectItem/:id', component: InspectItemComponent }
];