import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from './views/login/login.component'
import {DashboardComponent} from './views/dashboard/dashboard.component'
import {EditarComponent} from './views/editar/editar.component'
import {NuevoComponent} from './views/nuevo/nuevo.component'
const routes: Routes = [
  {path:'',redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component:LoginComponent},
  {path: 'dashboard', component:DashboardComponent},
  {path: 'nuevo', component:NuevoComponent},
  {path: 'editar', component:EditarComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponets = [LoginComponent,DashboardComponent,EditarComponent,NuevoComponent]