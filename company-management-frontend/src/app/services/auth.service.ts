import { Injectable, computed, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private isAuthenticated$ = signal(false);
  
  public isAuthenticated = computed(() => this.isAuthenticated$);
  
  constructor() { }

  public setUserAuthStatus(isUserAuthenticated: boolean) {
    this.isAuthenticated$.set(isUserAuthenticated);
  }

}
