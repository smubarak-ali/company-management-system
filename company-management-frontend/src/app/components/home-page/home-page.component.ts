import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    })
  }
  

  onFormSubmit(event: SubmitEvent) {
    event.preventDefault();
    if (this.loginForm.controls['username'].value === 'mubarak' && this.loginForm.controls['password'].value === 'mubarak123') {
      this.authService.setUserAuthStatus(true);
      this.router.navigateByUrl('/dashboard/list');
    } else {
      this.authService.setUserAuthStatus(false);
    }
  }

}
