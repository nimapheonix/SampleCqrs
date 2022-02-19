import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'people',
        pathMatch: 'full',
    },
    {
        path: '',
        loadChildren: () => import('./features/people/people.module').then(m => m.PeopleModule)
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {
      useHash: true,
      enableTracing: false
    })],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }