import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { RouteGuard } from './core/guards/route.quard';
import { AuthGuard } from './core/guards/auth.quard';
import { RegisterComponent } from './pages/register/register.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';

export const routes: Routes = [
  {
    path: '',
    component: WelcomeComponent,
    pathMatch: 'full',
    canActivate: [RouteGuard],
  },
  { path: 'login', component: LoginComponent, canActivate: [AuthGuard] },
  { path: 'register', component: RegisterComponent },
  {
    path: 'categories',
    loadComponent: () =>
      import('./pages/categories/category-list.component').then(
        (c) => c.CategoryListComponent
      ),
    canActivate: [RouteGuard],
  },
  {
    path: 'products',
    loadComponent: () =>
      import('./pages/products/product-list.component').then(
        (c) => c.ProductListComponent
      ),
    canActivate: [RouteGuard],
  },
  { path: '**', redirectTo: '404' },
  { path: '404', pathMatch: 'full', component: NotFoundComponent },
];
