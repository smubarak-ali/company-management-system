import { inject } from '@angular/core';
import { CanActivateChildFn, CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateChildFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const isUserAuth = authService.isAuthenticated();
  console.log("isuserauth: ", isUserAuth());
  if (isUserAuth()) {
    return true;
  }
  else {
    return router.navigateByUrl("/login");
  }
};
