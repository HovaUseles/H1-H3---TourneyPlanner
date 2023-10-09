import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    // component: HomePageComponent
  },
  {
    path: 'Tournament',
    // component: HomePageComponent
  },
  {
    path: 'Team',
    // component: MoviePageComponent
  },
  {
    path: 'Settings',
    // component: MoviePageComponent
  },
  {
    path: 'User/Tournaments',
    // component: MoviePageComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
