import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const guestGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const isUserAuth = authService.isAuthenticated();
  console.log("isUserAuth: ", isUserAuth());
  if (isUserAuth()) {
    return router.navigateByUrl("/dashboard/list");
  }
  else {
    return true
  }
};
