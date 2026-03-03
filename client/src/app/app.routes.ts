
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Login } from './components/login/login.component';

export const routes: Routes = [
    { path: '', component: Login},

    { path: '**', redirectTo: '/', pathMatch: 'full'}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}