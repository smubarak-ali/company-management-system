import { Injectable, computed, effect, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private isAuthenticated$ = signal(false);

  public isAuthenticated = computed(() => this.isAuthenticated$);

  constructor() {
    effect(() => {
      const currentStatus = sessionStorage.getItem("auth_status");
      console.log(" getting current status: ", currentStatus);
      if (currentStatus) {
        this.isAuthenticated$.set(currentStatus === "true" ? true : false);
      }
    }, {
      allowSignalWrites: true
    });

    effect(() => {
      console.log(" set session storage");
      sessionStorage.setItem("auth_status", this.isAuthenticated$() === true ? "true" : "false");
    });
  }

  public setUserAuthStatus(isUserAuthenticated: boolean) {
    this.isAuthenticated$.set(isUserAuthenticated);
  }

}
