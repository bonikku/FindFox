import { HttpClient } from '@angular/common/http'
import { Component, Input } from '@angular/core'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  registerMode = false
  users: any

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers()
  }

  registerToggle() {
    this.registerMode = !this.registerMode
  }

  getUsers() {
    this.http.get('https://localhost:7026/api/users').subscribe({
      next: response => (this.users = response),
      error: error => console.log(error),
      complete: () => console.log('Requested completed!'),
    })
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event
  }
}
